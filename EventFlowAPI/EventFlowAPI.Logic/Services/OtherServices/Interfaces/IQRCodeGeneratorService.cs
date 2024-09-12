using EventFlowAPI.DB.Entities;
using SixLabors.ImageSharp;

namespace EventFlowAPI.Logic.Services.OtherServices.Interfaces
{
    public interface IQRCodeGeneratorService 
    {
        Image GenerateQRCode(Reservation reservation);
    }
}
