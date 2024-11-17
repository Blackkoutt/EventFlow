using EventFlowAPI.DB.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventFlowAPI.DB.Extensions.SeedingExtensions
{
    public static class FAQSeedExtensions
    {
        public static void Seed(this EntityTypeBuilder<FAQ> entityBuilder)
        {
            entityBuilder.HasData(
                new FAQ
                {
                    Id = 1,
                    Question = "W jaki sposób mogę kupić bilet na wydarzenie ?",
                    Answer = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed justo erat, tempor quis sagittis et, sodales id nibh. Phasellus diam enim, sodales pharetra neque eget, sollicitudin vulputate est. Morbi ac velit sed arcu malesuada feugiat. Suspendisse potenti. Donec commodo mauris nisi. Nulla ut mi eu lectus consequat porta at commodo."
                },
                new FAQ
                {
                    Id = 2,
                    Question = "Jak mogę wynająć salę ?",
                    Answer = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed justo erat, tempor quis sagittis et, sodales id nibh. Phasellus diam enim, sodales pharetra neque eget, sollicitudin vulputate est. Morbi ac velit sed arcu malesuada feugiat. Suspendisse potenti. Donec commodo mauris nisi. Nulla ut mi eu lectus consequat porta at commodo."
                },
                new FAQ
                {
                    Id = 3,
                    Question = "Co ile czasu muszę przedłużać karnet ?",
                    Answer = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed justo erat, tempor quis sagittis et, sodales id nibh. Phasellus diam enim, sodales pharetra neque eget, sollicitudin vulputate est. Morbi ac velit sed arcu malesuada feugiat. Suspendisse potenti. Donec commodo mauris nisi. Nulla ut mi eu lectus consequat porta at commodo."
                },
                new FAQ
                {
                    Id = 4,
                    Question = "Jak zwrócić zakupiony bilet ?",
                    Answer = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed justo erat, tempor quis sagittis et, sodales id nibh. Phasellus diam enim, sodales pharetra neque eget, sollicitudin vulputate est. Morbi ac velit sed arcu malesuada feugiat. Suspendisse potenti. Donec commodo mauris nisi. Nulla ut mi eu lectus consequat porta at commodo."
                }
            );
        }
    }
}
