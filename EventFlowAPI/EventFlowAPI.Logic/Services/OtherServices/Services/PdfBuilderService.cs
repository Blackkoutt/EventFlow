using EventFlowAPI.DB.Entities;
using EventFlowAPI.Logic.Extensions.PdfBuilderExtensions;
using EventFlowAPI.Logic.Helpers.Enums;
using EventFlowAPI.Logic.Helpers.PdfOptions;
using EventFlowAPI.Logic.Helpers.PdfOptions.PdfCommonOptions;
using EventFlowAPI.Logic.Helpers.PdfOptions.PdfFestivalEventInfoOptions;
using EventFlowAPI.Logic.Helpers.PdfOptions.PdfInfoAndStatuteOptions;
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
        public async Task<byte[]> CreateTicketPdf(Reservation reservation, List<byte[]> tickets)
        {
            var seatTypes = (await _unitOfWork.GetRepository<SeatType>().GetAllAsync()).ToList();
            var ticketTypes = (await _unitOfWork.GetRepository<TicketType>().GetAllAsync()).ToList();
            var logoSmall = await _assetService.GetPictureAsBitmap(Picture.EventFlowLogo_Small, Helpers.Enums.ImageFormat.PNG);

            FestivalEventInfoOptions festivalEventInfoOptions = new(reservation);
            TicketPictureOptions ticketPictureOptions = new();
            SummaryOptions summaryOptions = new(reservation, seatTypes, ticketTypes);
            ReservationInfoOptions reservationInfoOptions = new(reservation);
            PageOptions pageOptions = new();
            CommonOptions commonOptions = new();
            HeaderOptions headerOptions = new();
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
                            .AddReservationInfo(reservationInfoOptions);

                            column.Item()
                            .AddEventOrFestivalInfo(festivalEventInfoOptions);

                            column.Item()
                            .AddTicketPictures(tickets, ticketPictureOptions, commonOptions);

                            column.Item()
                            .AddBottomLine(commonOptions)
                            .AddSummaryContainer(summaryOptions);

                            column.Item()
                            .AddInfoAndStatute(infoAndStatuteOptions);
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
