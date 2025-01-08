using EventFlowAPI.Logic.DTO.Statistics.ResponseDto;
using EventFlowAPI.Logic.Helpers.Enums;
using EventFlowAPI.Logic.Helpers.PdfOptions.PdfCommonOptions;
using EventFlowAPI.Logic.Helpers.PdfOptions.PdfContentOptions;
using EventFlowAPI.Logic.Helpers.PdfOptions.PdfPictureOptions;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
namespace EventFlowAPI.Logic.Extensions.PdfBuilderExtensions
{
    public static class StatisticsContent
    {
        public static void AddStatisticsHeader(this ColumnDescriptor column, string headerText, ContentStatisticsOptions options)
        {
            column.Item().PaddingTop(12);
            column.AddTextItem(options.GetLabelValueString(label: headerText, value: "", fontSize: 18f));
            column.Item().AddBottomLine(new CommonOptions());
        }
        public static void AddIncomeStatistics(this IContainer column, TotalIncomeStatistics statistics, List<(byte[] Bitmap, PlotType PlotType)> plotsBitmaps, ContentStatisticsOptions options, PictureOptions pictureOptions)
        {
            column
           .PaddingLeft(options.PadLeft)
           .ExtendHorizontal() 
           .Column(column =>
           {
               column.Item()
               .EnsureSpace(200)
               .Column(column =>
               {
                   column.AddStatisticsHeader("Całkowity przychód", options);
                   column.AddTextItem(options.GetLabelValueString(label: "Całkowity przychód: ", value: $"{statistics.TotalIncome} {Currency.PLN}", fontSize: 12f));
                   column.AddTextItem(options.GetLabelValueString(label: "Przychód z rezerwacji miejsc: ", value: $"{statistics.ReservationsIncome} {Currency.PLN}", fontSize: 12f));
                   column.AddTextItem(options.GetLabelValueString(label: "Przychód z rezerwacji sal: ", value: $"{statistics.HallRentsIncome} {Currency.PLN}", fontSize: 12f));
                   column.AddTextItem(options.GetLabelValueString(label: "Przychód z karnetów: ", value: $"{statistics.EventPassesIncome} {Currency.PLN}", fontSize: 12f));
               });
               column.Item().Column(column =>
               {
                   foreach (var picture in plotsBitmaps)
                   {
                       column.Item()
                       .AlignCenter().AddPlotPicture(picture.Bitmap, picture.PlotType, pictureOptions);
                   }
               });
           });
        }


        public static void AddPaymentStatistics(this IContainer column, PaymentStatistics statistics, List<(byte[] Bitmap, PlotType PlotType)> plotsBitmaps, ContentStatisticsOptions options, PictureOptions pictureOptions)
        {
            column
           .PaddingLeft(options.PadLeft)
           .ExtendHorizontal()
           .Column(column =>
           {
               column.Item()
               .EnsureSpace(200)
               .Column(column =>
               {
                   column.AddStatisticsHeader("Płatności", options);
                   column.AddTextItem(options.GetLabelValueString(label: "Całkowita liczba transakcji: ", value: $"{statistics.PaymentsCount}", fontSize: 12.5f));
                   column.AddTextItem(options.GetLabelValueString(label: "Całkowity koszt wszytskich transakcji: ", value: $"{Math.Round(statistics.TotalTransactionsCost, 2)}", fontSize: 12.5f));
               });
               column.Item().Column(column =>
               {
                   foreach (var picture in plotsBitmaps)
                   {
                       column.Item()
                       .AlignCenter().AddPlotPicture(picture.Bitmap, picture.PlotType, pictureOptions);
                   }
               });
           });
        }


        public static void AddUserStatistics(this IContainer column, UserStatistics statistics, List<(byte[] Bitmap, PlotType PlotType)> plotsBitmaps, ContentStatisticsOptions options, PictureOptions pictureOptions)
        {
            column
           .PaddingLeft(options.PadLeft)
           .ExtendHorizontal()
           .Column(column =>
           {
               column.Item()
               .EnsureSpace(200)
               .Column(column =>
               {
                   column.AddStatisticsHeader("Użytkownicy", options);
                   column.AddTextItem(options.GetLabelValueString(label: "Całkowita liczba użytkowników: ", value: $"{statistics.TotalUsersCount}", fontSize: 12.5f));
                   column.AddTextItem(options.GetLabelValueString(label: "Liczba nowych użytkowników: ", value: $"{statistics.NewRegistredUsersCount}", fontSize: 12.5f));
                   column.AddTextItem(options.GetLabelValueString(label: "Średnia wieku użytkowników: ", value: $"{statistics.UsersAgeAvg}", fontSize: 12.5f));
               });
               column.Item().Column(column =>
               {
                   foreach (var picture in plotsBitmaps)
                   {
                       column.Item()
                       .AlignCenter().AddPlotPicture(picture.Bitmap, picture.PlotType, pictureOptions);
                   }
               });
           });
        }


        public static void AddReservationStatistics(this IContainer column, ReservationStatistics statistics, List<(byte[] Bitmap, PlotType PlotType)> plotsBitmaps, ContentStatisticsOptions options, PictureOptions pictureOptions)
        {
            column
           .PaddingLeft(options.PadLeft)
           .ExtendHorizontal()
           .Column(column =>
           {
               column.Item()
               .EnsureSpace(200)
               .Column(column =>
               {
                   column.AddStatisticsHeader("Rezerwacje", options);
                   column.AddTextItem(options.GetLabelValueString(label: "Całkowita liczba nowych rezerwacji: ", value: $"{statistics.AllNewReservationsCount}", fontSize: 12.5f));
                   column.AddTextItem(options.GetLabelValueString(label: "Liczba nowych rezerwacji na wydarzenia: ", value: $"{statistics.AllNewEventReservationsCount}", fontSize: 12.5f));
                   column.AddTextItem(options.GetLabelValueString(label: "Liczba nowych rezerwacji na festiwale: ", value: $"{statistics.AllNewFestivalReservationsCount}", fontSize: 12.5f));
                   column.AddTextItem(options.GetLabelValueString(label: "Liczba odwołanych rezerwacji: ", value: $"{statistics.AllCanceledReservationsCount}", fontSize: 12.5f));
               });
               column.Item().Column(column =>
               {
                   foreach (var picture in plotsBitmaps)
                   {
                       column.Item()
                       .AlignCenter().AddPlotPicture(picture.Bitmap, picture.PlotType, pictureOptions);
                   }
               });
           });
        }

        public static void AddHallRentStatistics(this IContainer column, HallRentStatistics statistics, List<(byte[] Bitmap, PlotType PlotType)> plotsBitmaps, ContentStatisticsOptions options, PictureOptions pictureOptions)
        {
            column
           .PaddingLeft(options.PadLeft)
           .ExtendHorizontal()
           .Column(column =>
           {
               column.Item()
               .EnsureSpace(200)
               .Column(column =>
               {
                   column.AddStatisticsHeader("Wynajem sal", options);
                   column.AddTextItem(options.GetLabelValueString(label: "Liczba nowych rezerwacji sal: ", value: $"{statistics.AllAddedHallRentsCount}", fontSize: 12.5f));
                   column.AddTextItem(options.GetLabelValueString(label: "Liczba odwołanych rezerwacji sal: ", value: $"{statistics.AllCanceledHallRentsCount}", fontSize: 12.5f));
                   column.AddTextItem(options.GetLabelValueString(label: "Liczba zakończonych lub trwających rezerwacji sal: ", value: $"{statistics.AllHallRentsThatTookPlaceInTimePeriod}", fontSize: 12.5f));
                   column.AddTextItem(options.GetLabelValueString(label: "Średnia długość rezerwacji: ", value: $"{statistics.DurationAvg/60} h {statistics.DurationAvg} min", fontSize: 12.5f));
                   column.AddTextItem(options.GetLabelValueString(label: "Całkowity przychód z rezerwacji sal: ", value: $"{statistics.TotalHallRentsIncome} {Currency.PLN}", fontSize: 12.5f));
               });
               column.Item().Column(column =>
               {
                   foreach (var picture in plotsBitmaps)
                   {
                       column.Item()
                       .AlignCenter().AddPlotPicture(picture.Bitmap, picture.PlotType, pictureOptions);
                   }
               });
           });
        }

        public static void AddEventStatistics(this IContainer column, EventStatistics statistics, List<(byte[] Bitmap, PlotType PlotType)> plotsBitmaps, ContentStatisticsOptions options, PictureOptions pictureOptions)
        {
            column
           .PaddingLeft(options.PadLeft)
           .ExtendHorizontal()
           .Column(column =>
           {
               column.Item()
               .EnsureSpace(200)
               .Column(column =>
               {
                   column.AddStatisticsHeader("Wydarzenia", options);
                   column.AddTextItem(options.GetLabelValueString(label: "Liczba nowych wydarzeń: ", value: $"{statistics.AllAddedEventsCount}", fontSize: 12.5f));
                   column.AddTextItem(options.GetLabelValueString(label: "Liczba odwołanych wydarzeń: ", value: $"{statistics.AllCanceledEventsCount}", fontSize: 12.5f));
                   column.AddTextItem(options.GetLabelValueString(label: "Liczba zakończonych lub trwających wydarzeń: ", value: $"{statistics.AllEventsThatTookPlaceInTimePeriod}", fontSize: 12.5f));
                   column.AddTextItem(options.GetLabelValueString(label: "Średnia długość wydarzeń: ", value: $"{statistics.DurationAvg/60} h {statistics.DurationAvg} min", fontSize: 12.5f));
                   column.AddTextItem(options.GetLabelValueString(label: "Całkowity przychód z wydarzeń: ", value: $"{statistics.TotalEventsIncome} {Currency.PLN}", fontSize: 12.5f));
               });
               column.Item().Column(column =>
               {
                   foreach (var picture in plotsBitmaps)
                   {
                       column.Item()
                       .AlignCenter().AddPlotPicture(picture.Bitmap, picture.PlotType, pictureOptions);
                   }
               });
           });
        }

        public static void AddFestivalStatistics(this IContainer column, FestivalStatistics statistics, List<(byte[] Bitmap, PlotType PlotType)> plotsBitmaps, ContentStatisticsOptions options, PictureOptions pictureOptions)
        {
            column
           .PaddingLeft(options.PadLeft)
           .ExtendHorizontal()
           .Column(column =>
           {
               column.Item()
               .EnsureSpace(200)
               .Column(column =>
               {
                   column.AddStatisticsHeader("Festiwale", options);
                   column.AddTextItem(options.GetLabelValueString(label: "Liczba nowych festiwali: ", value: $"{statistics.AllAddedFestivalsCount}", fontSize: 12.5f));
                   column.AddTextItem(options.GetLabelValueString(label: "Liczba odwołanych festiwali: ", value: $"{statistics.AllCanceledFestivalsCount}", fontSize: 12.5f));
                   column.AddTextItem(options.GetLabelValueString(label: "Liczba zakończonych lub trwających festiwali: ", value: $"{statistics.AllFestivalsThatTookPlaceInTimePeriod}", fontSize: 12.5f));
                   column.AddTextItem(options.GetLabelValueString(label: "Średnia długość festiwali: ", value: $"{statistics.DurationAvg/60} h {statistics.DurationAvg} min", fontSize: 12.5f));
                   column.AddTextItem(options.GetLabelValueString(label: "Średnia ilość wydarzeń w festiwalach: ", value: $"{statistics.EventCountAvg}", fontSize: 12.5f));
                   column.AddTextItem(options.GetLabelValueString(label: "Całkowity przychód z festiwali: ", value: $"{statistics.TotalFestivalsIncome} {Currency.PLN}", fontSize: 12.5f));
               });
               column.Item().Column(column =>
               {
                   foreach (var picture in plotsBitmaps)
                   {
                       column.Item()
                       .AlignCenter().AddPlotPicture(picture.Bitmap, picture.PlotType, pictureOptions);
                   }
               });
           });
        }

        public static void AddEventPassStatistics(this IContainer column, EventPassStatistics statistics, List<(byte[] Bitmap, PlotType PlotType)> plotsBitmaps, ContentStatisticsOptions options, PictureOptions pictureOptions)
        {
            column
           .PaddingLeft(options.PadLeft)
           .ExtendHorizontal()
           .Column(column =>
           {
               column.Item()
               .EnsureSpace(200)
               .Column(column =>
               {
                   column.AddStatisticsHeader("Karnety", options);
                   column.AddTextItem(options.GetLabelValueString(label: "Liczba nowo zakupionych karnetów: ", value: $"{statistics.AllBougthEventPassesCount}", fontSize: 12.5f));
                   column.AddTextItem(options.GetLabelValueString(label: "Liczba przedłużonych karnetów: ", value: $"{statistics.AllRenewedEventPassesCount}", fontSize: 12.5f));
                   column.AddTextItem(options.GetLabelValueString(label: "Liczba anulowanych karnetów: ", value: $"{statistics.AllCanceledEventPassesCount}", fontSize: 12.5f));
                   column.AddTextItem(options.GetLabelValueString(label: "Całkowity przychód z karnetów: ", value: $"{statistics.TotalEventPassesIncome} {Currency.PLN}", fontSize: 12.5f));

               });
               column.Item().Column(column =>
               {
                   foreach (var picture in plotsBitmaps)
                   {
                       column.Item()
                       .AlignCenter().AddPlotPicture(picture.Bitmap, picture.PlotType, pictureOptions);
                   }
               });
           });
        }
    }
}
