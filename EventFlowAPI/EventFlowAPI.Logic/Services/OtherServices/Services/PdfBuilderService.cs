using EventFlowAPI.DB.Entities;
using EventFlowAPI.Logic.Extensions.PdfBuilderExtensions;
using EventFlowAPI.Logic.Helpers.Enums;
using EventFlowAPI.Logic.Helpers.PdfOptions;
using EventFlowAPI.Logic.Helpers.PdfOptions.PdfCommonOptions;
using EventFlowAPI.Logic.Helpers.PdfOptions.PdfContentOptions;
using EventFlowAPI.Logic.Helpers.PdfOptions.PdfInfoAndStatuteOptions;
using EventFlowAPI.Logic.Helpers.PdfOptions.PdfInfoOptions;
using EventFlowAPI.Logic.Helpers.PdfOptions.PdfSummaryOptions;
using EventFlowAPI.Logic.Services.OtherServices.Interfaces;
using EventFlowAPI.Logic.UnitOfWork;
using QuestPDF.Fluent;

namespace EventFlowAPI.Logic.Services.OtherServices.Services
{
    public class PdfBuilderService(IAssetService assetService, IUnitOfWork unitOfWork) : IPdfBuilderService
    {
        private readonly IAssetService _assetService = assetService;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<byte[]> CreateEventPassPdf(EventPass eventPass, byte[] eventPassJPGBitmap)
        {
            var logoSmall = await _assetService.GetPictureAsBitmap(Helpers.Enums.Picture.EventFlowLogo_Small, ImageFormat.PNG);
            var eventPassTypes = (await _unitOfWork.GetRepository<EventPassType>().GetAllAsync()).ToList();
            EventPass? oldEventPass = null;
            if(eventPass.RenewalDate != null)
            {
                oldEventPass = await _unitOfWork.GetRepository<EventPass>().GetOneAsync(eventPass.Id);
            }
            

            PageOptions pageOptions = new();
            HeaderOptions headerOptions = new();
            CommonOptions commonOptions = new();
            EventPassInfoOptions eventPassInfoOptions = new(eventPass, oldEventPass);
            PictureOptions eventPassPictureOptions = new();
            EventPassSummaryOptions summaryOptions = new(eventPass, eventPassTypes);
            InfoAndStatuteOptions infoAndStatuteOptions = new();
            FooterOptions footerOptions = new();

            using (var memoryStream = new MemoryStream())
            {
                Document.Create(container =>
                {
                    container.Page(page =>
                    {
                        page.ConfigurePage(pageOptions);

                        page.Header()
                        .AddBottomLine(commonOptions)
                        .AddHeaderLogo(logoSmall, headerOptions);

                        page.Content()
                        .Column(column =>
                        {
                            column.Item()
                            .AddBottomLine(commonOptions)
                            .AddOrderInfo(eventPassInfoOptions);

                            column.Item()
                            .AddBottomLine(commonOptions)
                            .AddPicture(eventPassJPGBitmap, eventPassPictureOptions);

                            column.Item()
                            .AddBottomLine(commonOptions)
                            .AddSummaryContainer(summaryOptions);

                            column.Item()
                            .AddEventPassInfoAndStatute(infoAndStatuteOptions);
                        });

                        page.Footer()
                        .AddTopLine(commonOptions)
                        .AddFooterLogoAndPageNumber(logoSmall, footerOptions);
                    });
                }).GeneratePdf(memoryStream);

                return memoryStream.ToArray();
            }            
        }
        public async Task<byte[]> CreateTicketPdf(Reservation reservation, List<byte[]> tickets)
        {
            var seatTypes = (await _unitOfWork.GetRepository<SeatType>().GetAllAsync()).ToList();
            var ticketTypes = (await _unitOfWork.GetRepository<TicketType>().GetAllAsync()).ToList();
            var logoSmall = await _assetService.GetPictureAsBitmap(Helpers.Enums.Picture.EventFlowLogo_Small, ImageFormat.PNG);

            PageOptions pageOptions = new();
            HeaderOptions headerOptions = new();
            CommonOptions commonOptions = new();
            ReservationInfoOptions reservationInfoOptions = new(reservation);
            FestivalEventInfoOptions festivalEventInfoOptions = new(reservation);
            PictureOptions ticketPictureOptions = new();
            ReservationSummaryOptions summaryOptions = new(reservation, seatTypes, ticketTypes);                                
            FooterOptions footerOptions = new();  
            InfoAndStatuteOptions infoAndStatuteOptions = new();

            using(var memoryStream = new MemoryStream())
            {
                Document.Create(container =>
                {
                    container.Page(page =>
                    {
                        page.ConfigurePage(pageOptions);

                        page.Header()
                        .AddBottomLine(commonOptions)
                        .AddHeaderLogo(logoSmall, headerOptions);

                        page.Content()
                        .Column(column =>
                        {
                            column.Item()
                            .AddBottomLine(commonOptions)
                            .AddOrderInfo(reservationInfoOptions);

                            column.Item()
                            .AddEventOrFestivalInfo(festivalEventInfoOptions);

                            column.Item()
                            .AddTicketPictures(tickets, ticketPictureOptions, commonOptions);

                            column.Item()
                            .AddBottomLine(commonOptions)
                            .AddSummaryContainer(summaryOptions);

                            column.Item()
                            .AddTicketInfoAndStatute(infoAndStatuteOptions);
                        });

                        page.Footer()
                        .AddTopLine(commonOptions)
                        .AddFooterLogoAndPageNumber(logoSmall, footerOptions);
                    });
                }).GeneratePdf(memoryStream);

                return memoryStream.ToArray();
            }
        }
    }
}
