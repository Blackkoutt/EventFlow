using EventFlowAPI.DB.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventFlowAPI.DB.Extensions.SeedingExtensions
{
    public static class FestivalDetailsSeedExtentions
    {
        public static void Seed(this EntityTypeBuilder<FestivalDetails> entityBuilder)
        {
            entityBuilder.HasData(
                new FestivalDetails
                {
                    Id = 1,
                    LongDescription = "Festiwal Muzyki Hip-Hop to coroczne wydarzenie dedykowane nowoczesnym" +
                    " brzmieniom i eksperymentalnym kompozycjom." +
                    " Na scenie pojawiają się zarówno uznani artyści, jak i młode talenty," +
                    " prezentując innowacyjne podejście do muzyki. W programie znajdują się koncerty," +
                    " warsztaty, spotkania z twórcami oraz panele dyskusyjne dotyczące przyszłości muzyki." +
                    " Festiwal przyciąga miłośników dźwięków elektronicznych, jazzowych improwizacji, minimalistycznych" +
                    " kompozycji i nowych form ekspresji dźwiękowej. To wyjątkowa okazja, by odkryć najnowsze trendy muzyczne" +
                    " i poszerzyć swoje horyzonty artystyczne."
                },
                new FestivalDetails
                {
                    Id = 2,
                    LongDescription = "Festiwal Filmowy to wyjątkowe święto kina, które gromadzi pasjonatów filmów" +
                    " z różnych zakątków świata. W repertuarze znajdują się zarówno filmy fabularne, dokumentalne," +
                    " jak i krótkometrażowe, reprezentujące różne nurty i stylistyki. Organizowane są spotkania z reżyserami," +
                    " aktorami oraz warsztaty dla młodych filmowców. Każda edycja festiwalu poświęcona jest określonemu" +
                    " tematowi przewodniemu, który inspiruje twórców do dyskusji nad istotnymi kwestiami społecznymi," +
                    " kulturowymi i artystycznymi. To doskonała okazja, by zobaczyć premiery, odkryć nowe talenty i zanurzyć się" +
                    " w magiczny świat kinematografii."
                },
                new FestivalDetails
                {
                    Id = 3,
                    LongDescription = "Festiwal Sztuki to wydarzenie celebrujące kreatywność" +
                    " i nieograniczoną ekspresję artystyczną. W ramach festiwalu prezentowane są prace artystów," +
                    " którzy eksperymentują z formą, kolorem i strukturą, tworząc unikalne dzieła sztuki nowoczesnej." +
                    " Oprócz wystaw odbywają się warsztaty, podczas których uczestnicy mogą spróbować swoich sił w malarstwie," +
                    " rzeźbie czy instalacjach multimedialnych. Festiwal to także przestrzeń do rozmów o współczesnych" +
                    " trendach w sztuce, spotkania z artystami oraz wykłady ekspertów. To niepowtarzalna okazja, by odkryć" +
                    " nowe inspiracje i spojrzeć na sztukę z innej perspektywy."
                }
            );
        }
    }
}
