using EventFlowAPI.Logic.Helpers.TicketOptions;
using EventFlowAPI.Logic.Services.OtherServices.Interfaces;
using EventFlowAPI.Logic.Services.OtherServices.Interfaces.TicketConfiguration.Abstract;
using SixLabors.Fonts;
using SixLabors.ImageSharp;

namespace EventFlowAPI.Logic.Services.OtherServices.Services.TicketConfiguration.Abstract
{
    public abstract class TicketConfiguration<TEntity>(
        IAssetService assetService) : ITicketConfiguration<TEntity> where TEntity : class
    {
        protected IAssetService _assetService = assetService;

        public TicketTitlePrintingOptions GetTitlePrintingOptions(TEntity entity)
        {
            var text = GetTitle(entity);
            int maxSpaceForText = TitleRightMax - TitleLeftMax;

            int maxCharCountInOneLine = maxSpaceForText / TitleEstimatedCharWidth;
            var firstLineY = TitleFirstLineY;
            var secondLineY = firstLineY + TitleEstimatedMaxCharHeight + TitleLineHeight;
            int secondLineStartPositionToBeCentred = 0;

            string firstLine = text;
            string secondLine = string.Empty;

            var charCount = text.Length;
            int firstLineWidth = charCount * TitleEstimatedCharWidth;
            int firstLineStartPositionToBeCentred = TitleLeftMax + (maxSpaceForText - firstLineWidth) / 2;

            if (firstLineWidth > maxSpaceForText)
            {
                var lastSpaceIndex = text.Substring(0, maxCharCountInOneLine).LastIndexOf(' ');
                firstLine = text.Substring(0, lastSpaceIndex).TrimEnd();
                secondLine = text.Substring(lastSpaceIndex).TrimStart();

                firstLineWidth = firstLine.Length * TitleEstimatedCharWidth;
                firstLineStartPositionToBeCentred = TitleLeftMax + (maxSpaceForText - firstLineWidth) / 2;

                int secondLineWidth = secondLine.Length * TitleEstimatedCharWidth;
                secondLineStartPositionToBeCentred = TitleLeftMax + (maxSpaceForText - secondLineWidth) / 2;
            }

            return new TicketTitlePrintingOptions
            {
                TitleFirstLine = firstLine,
                TitleSecondLine = secondLine,
                FirstLineLocation = new PointF(firstLineStartPositionToBeCentred, firstLineY),
                SecondLineLocation = new PointF(secondLineStartPositionToBeCentred, secondLineY),
                Font = TitleFont,
                BrushColor = TitleColor
            };
        }

        public TicketDatePrintingOptions GetDatePrintingOptions()
        {
            return new TicketDatePrintingOptions
            {
                Font = DateFont,
                BrushColor = DateColor,
                Location = DateLocation,
                DateFormat = FormatDate,
            };
        }

        public TicketQRCodePrintingOptions GetQRCodePrintingOptions()
        {
            return new TicketQRCodePrintingOptions
            {
                Location = QRCodeLocation,
                Opacity = QRCodeOpacity,
                Size = QRCodeSize
            };
        }

        // Title abstract params
        protected abstract string GetTitle(TEntity entity);
        protected abstract short TitleFirstLineY { get; }
        protected abstract short TitleLineHeight { get; }
        protected abstract short TitleLeftMax { get; }
        protected abstract short TitleRightMax { get; }
        protected abstract short TitleEstimatedCharWidth { get; }
        protected abstract short TitleEstimatedMaxCharHeight { get; }
        protected abstract Font TitleFont { get; }
        protected abstract Color TitleColor { get; }


        // Date abstract params
        protected abstract Font DateFont { get; }
        protected abstract Color DateColor { get; }
        protected abstract PointF DateLocation { get; }
        protected abstract string FormatDate { get; }


        // QR Code abstract params
        protected abstract byte QRCodeSize { get; }
        protected abstract float QRCodeOpacity { get; }
        protected abstract Point QRCodeLocation { get; }
    }
}
