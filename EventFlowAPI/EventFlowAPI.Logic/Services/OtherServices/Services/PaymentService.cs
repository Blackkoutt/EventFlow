using EventFlowAPI.Logic.DTO.RequestDto;
using EventFlowAPI.Logic.Errors;
using EventFlowAPI.Logic.Helpers.Enums;
using EventFlowAPI.Logic.Helpers.PayU;
using EventFlowAPI.Logic.ResultObject;
using EventFlowAPI.Logic.Services.OtherServices.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Serilog;
using System.Text;
using System.Text.Json;

namespace EventFlowAPI.Logic.Services.OtherServices.Services
{
    public class PaymentService(IConfiguration configuration, IHttpContextAccessor httpContextAccessor) : IPaymentService
    {
        private readonly IConfiguration _configuration = configuration;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

        public async Task<Result<TRequestDto>> CheckNewTransactionStatus<TRequestDto>(string key) where TRequestDto : class
        {
            var rentRequestString = _httpContextAccessor.HttpContext!.Session.GetString(key);
            if (rentRequestString == null)
                return Result<TRequestDto>.Failure(Error.SessionError);
            else
                _httpContextAccessor.HttpContext!.Session.Remove(key);

            var isTransactionCompleteResult = await CheckTransactionStatus();
            if (!isTransactionCompleteResult.IsSuccessful)
                return Result<TRequestDto>.Failure(isTransactionCompleteResult.Error);

            var requestDto = JsonSerializer.Deserialize<TRequestDto>(rentRequestString);
            if (requestDto == null) return Result<TRequestDto>.Failure(Error.SerializationError);

            return Result<TRequestDto>.Success(requestDto);
        }

        public async Task<Result<object>> CheckTransactionStatus()
        {
            var transactionId = _httpContextAccessor.HttpContext!.Session.GetString("TransactionId");
            if (transactionId == null)
                return Result<object>.Failure(EventPassError.SessionError);
            else
                _httpContextAccessor.HttpContext!.Session.Remove("TransactionId");

            const int maxRetries = 5;
            const int delayBetweenRetries = 1000;

            string transactionStatus = string.Empty;

            for (int attempt = 1; attempt <= maxRetries; attempt++)
            {
                var transactionStatusResult = await GetTransactionStatus(transactionId);
                if (!transactionStatusResult.IsSuccessful)
                    return Result<object>.Failure(transactionStatusResult.Error);

                transactionStatus = transactionStatusResult.Value.Status;
                Log.Information($"Attempt {attempt}: {transactionStatus} \n\n\n\n\n");

                if (transactionStatus == PayUTransactionStatus.PENDING.ToString())
                {
                    if (attempt < maxRetries)
                    {
                        Log.Information("Transaction status is pending. Retrying...");
                        await Task.Delay(delayBetweenRetries);
                    }
                    else
                    {
                        return Result<object>.Failure(PaymentError.TransactionIsPendingTooLong);
                    }
                }
                else
                {
                    break;
                }

            }

            if (transactionStatus != PayUTransactionStatus.COMPLETED.ToString())
                return Result<object>.Failure(PaymentError.TransactionIsNotCompleted);

            return Result<object>.Success();
        }


        public async Task<Result<PayUTransactionStatusDto>> GetTransactionStatus(string transactionId)
        {
            if (string.IsNullOrEmpty(transactionId))
                return Result<PayUTransactionStatusDto>.Failure(PaymentError.BadTransactionId);

            var accessToken = _httpContextAccessor.HttpContext!.Session.GetString("PayUAccessToken");

            if (string.IsNullOrEmpty(accessToken))
            {
                var loginError = await LoginToPayU();
                if (loginError != Error.None)
                    return Result<PayUTransactionStatusDto>.Failure(loginError);

                accessToken = _httpContextAccessor.HttpContext!.Session.GetString("PayUAccessToken");
            }

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");
                client.DefaultRequestHeaders.Add("Accept", "application/json");

                var checkTransactionStatusEndpoint = _configuration.GetSection("PayU")["CheckTransactionStatusEndpoint"]!;
                var fullEndpoint = checkTransactionStatusEndpoint + transactionId;

                var response = await client.GetAsync(fullEndpoint);

                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    var loginError = await LoginToPayU();
                    if (loginError != Error.None)
                        return Result<PayUTransactionStatusDto>.Failure(loginError);

                    accessToken = _httpContextAccessor.HttpContext!.Session.GetString("PayUAccessToken");
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
                    response = await client.GetAsync(fullEndpoint);
                }

                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    using (JsonDocument doc = JsonDocument.Parse(responseBody))
                    {
                        var orderStatus = doc.RootElement
                                             .GetProperty("orders")
                                             .EnumerateArray()
                                             .FirstOrDefault()
                                             .GetProperty("status")
                                             .GetString();
                        if (!string.IsNullOrEmpty(orderStatus))
                        {
                            var paymentStatus = new PayUTransactionStatusDto
                            {
                                Status = orderStatus
                            };
                            return Result<PayUTransactionStatusDto>.Success(paymentStatus);
                        }
                        else return Result<PayUTransactionStatusDto>.Failure(PaymentError.OrderStatusIsEmptyOrNull);
                    }
                }
                else
                    return Result<PayUTransactionStatusDto>.Failure(PaymentError.GetTransactionStatusBadRequest);
            }
        }

        public async Task<Result<PayUCreatePaymentResponseDto>> CreatePayment(PayURequestPaymentDto requestDto)
        {
            var validationError = ValidatePaymentRequest(requestDto);
            if (validationError != Error.None)
                return Result<PayUCreatePaymentResponseDto>.Failure(validationError);

            var accessToken = _httpContextAccessor.HttpContext!.Session.GetString("PayUAccessToken");
            Log.Information("Hello");
            Log.Information(accessToken);
            if (string.IsNullOrEmpty(accessToken))
            {
                var loginError = await LoginToPayU();
                if (loginError != Error.None)
                    return Result<PayUCreatePaymentResponseDto>.Failure(loginError);

                accessToken = _httpContextAccessor.HttpContext!.Session.GetString("PayUAccessToken");
                Log.Information("Hello2");
                Log.Information(accessToken);
            }

            using var handler = new HttpClientHandler
            {
                AllowAutoRedirect = false
            };
            using var client = new HttpClient(handler);

            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");
            client.DefaultRequestHeaders.Add("Accept", "application/json");

            var bodyJSON = JsonSerializer.Serialize(requestDto);
            Log.Information("Request Body: {BodyContent}", bodyJSON);
            var body = new StringContent(bodyJSON, Encoding.UTF8, "application/json");

            var createTransactionEndpoint = _configuration.GetSection("PayU")["CreateTransactionEndpoint"]!;

            var response = await client.PostAsync(createTransactionEndpoint, body);

            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                var loginError = await LoginToPayU();
                if (loginError != Error.None)
                    return Result<PayUCreatePaymentResponseDto>.Failure(loginError);

                accessToken = _httpContextAccessor.HttpContext!.Session.GetString("PayUAccessToken");
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
                response = await client.PostAsync(createTransactionEndpoint, body);
            }

            if (response.StatusCode == System.Net.HttpStatusCode.Found)
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                var paymentResponse = JsonSerializer.Deserialize<PayUCreatePaymentResponseDto>(responseBody, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                if (paymentResponse != null)
                {
                    _httpContextAccessor.HttpContext!.Session.SetString("TransactionId", paymentResponse.OrderId);
                    return Result<PayUCreatePaymentResponseDto>.Success(paymentResponse);
                }

                else
                    return Result<PayUCreatePaymentResponseDto>.Failure(PaymentError.SerializeError);
            }
            else
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                Log.Information("CreateTransaction failed. Response: {ResponseBody}, StatusCode: {StatusCode}", responseBody, response.StatusCode);

                return Result<PayUCreatePaymentResponseDto>.Failure(PaymentError.CreateTransactionBadRequest);
            }
        }

        private async Task<Error> LoginToPayU()
        {
            Log.Information("Login");
            var loginEndpoint = _configuration.GetSection("PayU")["LoginEndpoint"]!;
            var clientId = _configuration.GetSection("PayU")["clientId"]!;
            var clientSecret = _configuration.GetSection("PayU")["clientSecret"]!;
            var grantType = _configuration.GetSection("PayU")["grant_type"]!;

            if (string.IsNullOrEmpty(clientId) || string.IsNullOrEmpty(clientSecret))
                return PaymentError.ClientIdOrSecretError;

            using (HttpClient client = new HttpClient())
            {
                var formData = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("grant_type", grantType),
                    new KeyValuePair<string, string>("client_id", clientId),
                    new KeyValuePair<string, string>("client_secret", clientSecret)
                });

                HttpResponseMessage response = await client.PostAsync(loginEndpoint, formData);

                string responseBody1 = await response.Content.ReadAsStringAsync();
                
                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    var authResponse = JsonSerializer.Deserialize<PayULoginResponseDto>(responseBody, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                    Log.Information("Auth. Response: {ResponseBody}, StatusCode: {StatusCode}", responseBody, response.StatusCode);

                    if (authResponse != null && !string.IsNullOrEmpty(authResponse.AccessToken))
                    {
                        _httpContextAccessor.HttpContext!.Session.SetString("PayUAccessToken", authResponse.AccessToken);
                    }

                }
                else
                    return PaymentError.BadClientCredentials;
            }
            return Error.None;
        }

        private Error ValidatePaymentRequest(PayURequestPaymentDto requestDto)
        {
            if (string.IsNullOrEmpty(requestDto.Description))
                return PaymentError.PaymentDescriptionEmpty;
            if (string.IsNullOrEmpty(requestDto.ContinueUrl))
                return PaymentError.PaymentContinueUrlEmpty;
            if (requestDto.TotalAmount == 0)
                return PaymentError.TotalAmountIsZero;
            if (!requestDto.Products.Any())
                return PaymentError.ProductArrayEmpty;
            if (requestDto.Products.Any(p => string.IsNullOrEmpty(p.Name) || p.Quanitity == 0))
                return PaymentError.ProductBadRequest;
            if (string.IsNullOrEmpty(requestDto.Buyer.FirstName) || string.IsNullOrEmpty(requestDto.Buyer.LastName) || string.IsNullOrEmpty(requestDto.Buyer.Email))
                return PaymentError.BuyerBadRequest;

            return Error.None;
        }
    }
}
