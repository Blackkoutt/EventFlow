using EventFlowAPI.DB.Entities;
using EventFlowAPI.Logic.ResultObject;

namespace EventFlowAPI.Logic.Services.OtherServices.Interfaces
{
    public interface ICopyMakerService
    {
        Task<Result<Hall>> MakeCopyOfHall(int hallId);
    }
}
