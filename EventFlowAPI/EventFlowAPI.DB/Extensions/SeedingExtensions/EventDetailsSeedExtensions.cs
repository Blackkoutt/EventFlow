using EventFlowAPI.DB.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventFlowAPI.DB.Extensions.SeedingExtensions
{
    public static class EventDetailsSeedExtensions
    {
        public static void Seed(this EntityTypeBuilder<EventDetails> entityBuilder)
        {
            entityBuilder.HasData(
                new EventDetails
                {
                    Id = 1,
                    LongDescription = "Koncert Mystic Waves to wyjątkowe wydarzenie muzyczne," +
                    " które przeniesie Cię w świat atmosferycznych dźwięków." +
                    " Zespół wystąpi z premierowym materiałem, łączącym elementy muzyki elektronicznej" +
                    " i ambientowej. Koncert odbędzie się w klimatycznej sali," +
                    " z wykorzystaniem nowoczesnej technologii dźwiękowej i wizualnej," +
                    " co zapewni niezapomniane przeżycia. Muzycy znani z licznych występów" +
                    " na scenach festiwalowych zaprezentują swoją najnowszą produkcję, która" +
                    " z pewnością oczaruje fanów muzyki eksperymentalnej."
                },
                new EventDetails
                {
                    Id = 2,
                    LongDescription = "Spektakl 'Cień Przeszłość' to wciągająca produkcja teatralna," +
                    " która porusza tematykę przemijania i refleksji nad przeszłością." +
                    " Historia opowiada o dwóch postaciach, które spotykają się po latach," +
                    " by skonfrontować swoje wspomnienia i tajemnice. Reżyseria, mistrzowskie" +
                    " aktorstwo oraz emocjonalna głębia tej sztuki sprawiają, że jest to wydarzenie," +
                    " które zostanie z widzami na długo. Spektakl jest pełen symboliki i zaskakujących" +
                    " zwrotów akcji, które skłonią do przemyśleń o upływającym czasie."
                },
                new EventDetails
                {
                    Id = 3,
                    LongDescription = "Film 'Królestwo planety małp' to spektakularna produkcja" +
                    " science-fiction, która kontynuuje historię walczących o przetrwanie małp." +
                    " Akcja filmu toczy się w post-apokaliptycznym świecie, w którym małpy zaczynają" +
                    " dominować nad ludźmi. W filmie zobaczymy dynamiczne sceny bitewne," +
                    " dramatyczne decyzje bohaterów i wciągającą fabułę, która porusza kwestie" +
                    " moralności i przyszłości ludzkości. Wysoka jakość efektów specjalnych oraz" +
                    " rewelacyjna gra aktorska sprawiają, że jest to jeden z najważniejszych filmów" +
                    " tego roku."
                },
                new EventDetails
                {
                    Id = 4,
                    LongDescription = "Wystawa 'Nowe inspiracje' to wyjątkowa ekspozycja" +
                    " sztuki współczesnej, która gromadzi prace młodych artystów z całego" +
                    " świata. Wystawa skupia się na nowych nurtach artystycznych, które łączą" +
                    " tradycję z nowoczesnością. Można tu zobaczyć prace w różnych mediach," +
                    " od malarstwa po instalacje interaktywne. Artyści eksperymentują z formą" +
                    " i materiałami, zapraszając widzów do interakcji z dziełami. Jest to przestrzeń," +
                    " która otwiera umysł na nowe pomysły i inspiracje, zachęcając do refleksji" +
                    " nad współczesnym światem sztuki."
                },
                new EventDetails
                {
                    Id = 5,
                    LongDescription = "Koncert 'New Era' to wyjątkowe wydarzenie muzyczne," +
                    " które łączy najnowsze trendy w muzyce elektronicznej z klasycznymi brzmieniami." +
                    " Zespół zaprezentuje utwory z debiutanckiego albumu, który zdobył uznanie wśród" +
                    " krytyków muzycznych. Występ będzie pełen energetycznych melodii i przejmujących" +
                    " dźwięków, które z pewnością poruszą serca słuchaczy. Nowoczesne technologie" +
                    " audio-wizualne, wykorzystywane podczas koncertu, sprawią, że każdy utwór nabierze" +
                    " nowego wymiaru. Warto być częścią tego muzycznego doświadczenia!"
                },
                new EventDetails
                {
                    Id = 6,
                    LongDescription = "Film 'Gladiator' to epicka opowieść o wojowniku," +
                    " który staje do walki nie tylko o swoje życie, ale także o wolność" +
                    " i sprawiedliwość. Akcja rozgrywa się w starożytnym Rzymie, gdzie główny" +
                    " bohater, Maximus, zostaje zdradzony przez cesarza, a jego rodzina zostaje" +
                    " zamordowana. Maximus trafia do areny, gdzie walczy o przetrwanie i zemstę." +
                    " Film pełen jest emocjonujących walk, dramatycznych zwrotów akcji i wciągającej" +
                    " fabuły, która porusza tematy honoru, lojalności i zemsty."
                },
                new EventDetails
                {
                    Id = 7,
                    LongDescription = "Wystawa 'Nowa sztuka' to prezentacja młodych artystów," +
                    " którzy swoją twórczość ukierunkowali na przekroczenie granic tradycyjnej" +
                    " sztuki. Na wystawie można zobaczyć prace w różnorodnych technikach:" +
                    " od klasycznych obrazów po awangardowe instalacje. Artyści bawią się konwencjami," +
                    " angażując widza w interaktywną formę prezentacji. Wystawa jest" +
                    " zaproszeniem do refleksji nad tym, czym współczesna sztuka może być " +
                    "i jakie emocje potrafi wywołać. To wydarzenie, które pokazuje, jak sztuka może" +
                    " zmieniać nasze postrzeganie świata."
                },
                new EventDetails
                {
                    Id = 8,
                    LongDescription = "Cień Nad Miastem to mroczny thriller teatralny, który przenosi widza w" +
                    " świat pełen tajemniczych wydarzeń, które zaczynają wstrząsać miastem po zmroku." +
                    " Główna fabuła opowiada historię, w której pozornie spokojne miasto staje się areną niebezpiecznych" +
                    " wydarzeń, których rozwiązanie wciąga nie tylko bohaterów, ale również publiczność. W spektaklu dominują" +
                    " napięcie i atmosfera niepokoju, które potęgują świetnie dobrane efekty wizualne oraz świetne aktorstwo." +
                    " Każda scena jest przemyślana, a dramaturgia składa się z zagadek i zwrotów akcji, które trzymają widza" +
                    " w ciągłym napięciu. To przedstawienie nie tylko o mrocznych wydarzeniach, ale także o ludzkich emocjach," +
                    " które rodzą się w sytuacjach kryzysowych, zmieniając życie bohaterów na zawsze. W trakcie spektaklu" +
                    " widzowie będą mieli okazję odkryć tajemnice miasta i skonfrontować się z pytaniem, jak cienka jest" +
                    " granica między rzeczywistością a tym, co wytworzyła wyobraźnia."
                },
                new EventDetails
                {
                    Id = 9,
                    LongDescription = "Jazzowe Brzmienia to wyjątkowy koncert, który przenosi uczestników w świat żywego jazzu." +
                    " Podczas wydarzenia wystąpią znani artyści jazzowi, którzy zabiorą widzów w podróż po różnorodnych stylach tego gatunku." +
                    " Atmosfera pełna emocji, improwizacji i magicznych dźwięków sprawia, że każda chwila staje się niezapomnianym doświadczeniem." +
                    " Koncert jest idealną okazją, by poczuć autentyczną magię jazzu w wyjątkowym wykonaniu na żywo, a publiczność wciągnie" +
                    " się w niepowtarzalny klimat muzycznej ekspresji."
                },
                new EventDetails
                {
                    Id = 10,
                    LongDescription = "Interstellar to epicka podróż w głąb kosmosu, która bada granice ludzkiej wytrzymałości, miłości i poświęcenia." +
                    " Grupa astronautów, wyruszając na misję w poszukiwaniu nowego domu dla ludzkości, zmierzy się z niewyobrażalnymi wyzwaniami." +
                    " Film porusza tematykę czasu, przestrzeni i ofiary, ukazując niesamowite krajobrazy kosmiczne oraz ludzką determinację w walce" +
                    " o przetrwanie. Każdy moment to prawdziwa uczta dla zmysłów, a historia miłości i poświęcenia łączy się z naukową fascynacją," +
                    " tworząc niezapomniane doświadczenie filmowe."
                },
                new EventDetails
                {
                    Id = 11,
                    LongDescription = "Spektakl 'Zatrzymać Przeznaczenie' to głęboka opowieść o postaci, która staje przed obliczem nieuchronnego przeznaczenia." +
                    " Bohaterowie zmagają się z wyborem, który może zmienić bieg wydarzeń, a ich działania prowadzą do nieodwracalnych konsekwencji." +
                    " Spektakl skłania do refleksji nad wolną wolą, nieuchronnością losu i odpowiedzialnością za własne wybory. Z pomocą wciągającej dramaturgii" +
                    " oraz poruszających dialogów, spektakl ukazuje, jak delikatna jest granica między tym, co możemy kontrolować, a tym, co jest zapisane w naszych losach."
                },
                new EventDetails
                {
                    Id = 12,
                    LongDescription = "Wystawa 'Nowe Perspektywy' zaprasza do odkrywania sztuki współczesnej z zupełnie nowych punktów widzenia." +
                    " Artyści wykorzystują różnorodne techniki, od tradycyjnych obrazów po innowacyjne instalacje, które angażują widza w interaktywną" +
                    " formę prezentacji. Każda praca na wystawie to zaproszenie do zastanowienia się nad tym, jak sztuka może zmieniać nasze postrzeganie" +
                    " rzeczywistości. To także próba przekroczenia granic tradycyjnych konwencji, poszukiwania nowych środków wyrazu i głębokiej refleksji" +
                    " nad naturą współczesnej sztuki."
                }
            );
        }
    }
}
