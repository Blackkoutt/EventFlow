using EventFlowAPI.DB.Entities;
using EventFlowAPI.Logic.Services.OtherServices.Interfaces;
using QRCoder;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace EventFlowAPI.Logic.Services.OtherServices.Services
{
    public class QRCodeGeneratorService : IQRCodeGeneratorService
    {
        public Image GenerateQRCode(Reservation reservation)
        {
            using (var qrCodegenerator = new QRCodeGenerator())
            {
                QRCodeData qrCodeData = qrCodegenerator.CreateQrCode(reservation.ReservationUniqueId.ToString(), QRCodeGenerator.ECCLevel.Q);
                PngByteQRCode qrCode = new PngByteQRCode(qrCodeData);
                var qrCodeBytes = qrCode.GetGraphic(9);
                using (var memoryStream = new MemoryStream(qrCodeBytes))
                {
                    return Image.Load<Rgba32>(memoryStream);
                }
            }      
        }       
    }
}
