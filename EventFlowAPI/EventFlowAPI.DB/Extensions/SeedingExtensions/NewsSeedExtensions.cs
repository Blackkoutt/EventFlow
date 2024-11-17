using EventFlowAPI.DB.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventFlowAPI.DB.Extensions.SeedingExtensions
{
    public static class NewsSeedExtensions
    {
        public static void Seed(this EntityTypeBuilder<News> entityBuilder, DateTime today)
        {
            entityBuilder.HasData(
               new News
               {
                   Id = 1,
                   Title = "Finał konkursu artystycznego",
                   NewsGuid = Guid.NewGuid(),
                   PublicationDate = today,
                   ShortDescription = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. " +
                   "Ut id nibh ornare, luctus velit ac, feugiat turpis. " +
                   "Vestibulum fermentum placerat mi nec scelerisque. " +
                   "Ut id nibh ornare, luctus velit ac, feugiat turpis. " +
                   "Vestibulum fermentum. Vestibulum fermentum placerat mi nec. " +
                   "Ut id nibh ornare, luctus velit ac, feugiat turpis." +
                   "Vestibulum fermentum. Vestibulum fermentum.",
                   LongDescription = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. " +
                   "Ut id nibh ornare, luctus velit ac, feugiat turpis. " +
                   "Vestibulum fermentum placerat mi nec scelerisque. " +
                   "Ut id nibh ornare, luctus velit ac, feugiat turpis. " +
                   "Vestibulum fermentum. Vestibulum fermentum placerat mi nec. " +
                   "Ut id nibh ornare, luctus velit ac, feugiat turpis." +
                   "Vestibulum fermentum. Vestibulum fermentum.",
                   PhotoName = "konkurs_artystyczny.png"
               },
              new News
              {
                  Id = 2,
                  Title = "Relacja z koncertu zespołu Lunar Vibes",
                  NewsGuid = Guid.NewGuid(),
                  PublicationDate = today,
                  ShortDescription = "Lorem ipsum dolor sit amet, " +
                  "consectetur adipiscing elit. Ut id nibh ornare, luctus...",
                  LongDescription = "Lorem ipsum dolor sit amet, " +
                  "consectetur adipiscing elit. Ut id nibh ornare, luctus...",
                  PhotoName = "koncert_lunar_vibes.png"
              },
              new News
              {
                  Id = 3,
                  Title = "Modernizacja sali koncertowej",
                  NewsGuid = Guid.NewGuid(),
                  PublicationDate = today,
                  ShortDescription = "Lorem ipsum dolor sit amet, consectetur adipiscing elit." +
                  " Ut id nibh ornare, luctus...",
                  LongDescription = "Lorem ipsum dolor sit amet, " +
                  "consectetur adipiscing elit. Ut id nibh ornare, luctus...",
                  PhotoName = "modernizacja sali.png"
              },
               new News
               {
                   Id = 4,
                   Title = "Zniżka 20% na zakup karnetów",
                   NewsGuid = Guid.NewGuid(),
                   PublicationDate = today,
                   ShortDescription = "Lorem ipsum dolor sit amet, consectetur adipiscing elit." +
                  " Ut id nibh ornare, luctus...",
                   LongDescription = "Lorem ipsum dolor sit amet, " +
                  "consectetur adipiscing elit. Ut id nibh ornare, luctus...",
                   PhotoName = "znizka.png"
               },
               new News
               {
                   Id = 5,
                   Title = "Noc Filmowa z Klasykami Kina",
                   NewsGuid = Guid.NewGuid(),
                   PublicationDate = today,
                   ShortDescription = "Lorem ipsum dolor sit amet, consectetur adipiscing elit." +
                  " Ut id nibh ornare, luctus...",
                   LongDescription = "Lorem ipsum dolor sit amet, " +
                  "consectetur adipiscing elit. Ut id nibh ornare, luctus...",
                   PhotoName = "noc_filmowa.png"
               },
               new News
               {
                   Id = 6,
                   Title = "Wernisaż: Nowe inspiracje",
                   NewsGuid = Guid.NewGuid(),
                   PublicationDate = today,
                   ShortDescription = "Lorem ipsum dolor sit amet, consectetur adipiscing elit." +
                  " Ut id nibh ornare, luctus...",
                   LongDescription = "Lorem ipsum dolor sit amet, " +
                  "consectetur adipiscing elit. Ut id nibh ornare, luctus...",
                   PhotoName = "wernisaz.png"
               }
            );
        }
    }
}
