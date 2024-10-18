﻿using EventFlowAPI.DB.Entities;
using EventFlowAPI.Logic.Errors;
using EventFlowAPI.Logic.ResultObject;
using EventFlowAPI.Logic.Services.OtherServices.Interfaces;
using EventFlowAPI.Logic.UnitOfWork;

namespace EventFlowAPI.Logic.Services.OtherServices.Services
{
    public class CopyMakerService(IUnitOfWork unitOfWork) : ICopyMakerService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        public async Task<Result<Hall>> MakeCopyOfHall(int hallId)
        {
            var _hallRepository = _unitOfWork.GetRepository<Hall>();
            var hallEntity = await _hallRepository.GetOneAsync(hallId);
            if (hallEntity == null || !hallEntity.IsVisible)
                return Result<Hall>.Failure(Error.NotFound);

            hallEntity.DefaultId = hallEntity.Id;

            // Copy Hall
            _hallRepository.Detach(hallEntity);
            hallEntity.Id = 0;

            // Copy Hall Details
            _unitOfWork.GetRepository<HallDetails>().Detach(hallEntity.HallDetails!);
            hallEntity.HallDetails!.Id = 0;

            // Copy Seats
            List<Seat> newSeatList = [];
            foreach (var seat in hallEntity.Seats)
            {
                var newSeat = new Seat
                {
                    SeatNr = seat.SeatNr,
                    Row = seat.Row,
                    GridRow = seat.GridRow,
                    Column = seat.Column,
                    GridColumn = seat.GridColumn,
                    SeatTypeId = seat.SeatTypeId,
                };
                newSeatList.Add(newSeat);
            }
            hallEntity.Seats = newSeatList;
            hallEntity.Events = [];
            hallEntity.Rents = [];
            hallEntity.IsCopy = true;
            hallEntity.IsVisible = false;

            await _hallRepository.AddAsync(hallEntity);
            await _unitOfWork.SaveChangesAsync();

            return Result<Hall>.Success(hallEntity);
        }
    }
}
