﻿using EventFlowAPI.DB.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventFlowAPI.DB.Extensions.SeedingExtensions
{
    public static class HallSeedExtensions
    {
        public static void Seed(this EntityTypeBuilder<Hall> entityBuilder)
        {
            entityBuilder.HasData(
                new Hall
                {
                    Id = 1,
                    DefaultId = 1,
                    HallNr = 1,
                    RentalPricePerHour = 120.99m,
                    Floor = 2,
                    IsCopy = false,
                    IsVisible = true,
                    HallTypeId = 1
                },
                new Hall
                {
                    Id = 2,
                    DefaultId = 2,
                    HallNr = 2,
                    RentalPricePerHour = 89.99m,
                    Floor = 1,
                    IsCopy = false,
                    IsVisible = true,
                    HallTypeId = 2
                },
                new Hall
                {
                    Id = 3,
                    DefaultId = 3,
                    HallNr = 3,
                    RentalPricePerHour = 179.99m,
                    Floor = 2,
                    IsCopy = false,
                    IsVisible = true,
                    HallTypeId = 3
                },
                new Hall
                {
                    Id = 4,
                    DefaultId = 4,
                    HallNr = 4,
                    RentalPricePerHour = 199.99m,
                    Floor = 1,
                    IsCopy = false,
                    IsVisible = true,
                    HallTypeId = 4
                },
                new Hall
                {
                    Id = 5,
                    DefaultId = 1,
                    HallNr = 1,
                    RentalPricePerHour = 120.99m,
                    Floor = 2,
                    IsCopy = true,
                    IsVisible = false,
                    HallTypeId = 1
                },
                new Hall
                {
                    Id = 6,
                    DefaultId = 2,
                    HallNr = 2,
                    RentalPricePerHour = 89.99m,
                    Floor = 1,
                    IsCopy = true,
                    IsVisible = false,
                    HallTypeId = 2
                },
                new Hall
                {
                    Id = 7,
                    DefaultId = 3,
                    HallNr = 3,
                    RentalPricePerHour = 179.99m,
                    Floor = 2,
                    IsCopy = true,
                    IsVisible = false,
                    HallTypeId = 3
                },
                new Hall
                {
                    Id = 8,
                    DefaultId = 4,
                    HallNr = 4,
                    RentalPricePerHour = 199.99m,
                    Floor = 1,
                    IsCopy = true,
                    IsVisible = false,
                    HallTypeId = 4
                },
                 new Hall
                 {
                     Id = 9,
                     DefaultId = 1,
                     HallNr = 1,
                     RentalPricePerHour = 120.99m,
                     Floor = 2,
                     IsCopy = true,
                     IsVisible = false,
                     HallTypeId = 1
                 },
                new Hall
                {
                    Id = 10,
                    DefaultId = 2,
                    HallNr = 2,
                    RentalPricePerHour = 89.99m,
                    Floor = 1,
                    IsCopy = true,
                    IsVisible = false,
                    HallTypeId = 2
                },
                new Hall
                {
                    Id = 11,
                    DefaultId = 4,
                    HallNr = 4,
                    RentalPricePerHour = 199.99m,
                    Floor = 1,
                    IsCopy = true,
                    IsVisible = false,
                    HallTypeId = 4
                },
                 new Hall
                 {
                     Id = 12,
                     DefaultId = 1,
                     HallNr = 1,
                     RentalPricePerHour = 120.99m,
                     Floor = 2,
                     IsCopy = true,
                     IsVisible = false,
                     HallTypeId = 1
                 },
                new Hall
                {
                    Id = 13,
                    DefaultId = 2,
                    HallNr = 2,
                    RentalPricePerHour = 89.99m,
                    Floor = 1,
                    IsCopy = true,
                    IsVisible = false,
                    HallTypeId = 2
                },
                new Hall
                {
                    Id = 14,
                    DefaultId = 3,
                    HallNr = 3,
                    RentalPricePerHour = 179.99m,
                    Floor = 2,
                    IsCopy = true,
                    IsVisible = false,
                    HallTypeId = 3
                },
                new Hall
                {
                    Id = 15,
                    DefaultId = 4,
                    HallNr = 4,
                    RentalPricePerHour = 199.99m,
                    Floor = 1,
                    IsCopy = true,
                    IsVisible = false,
                    HallTypeId = 4
                }
            );
        }
    }
}