using Accord.Math;
using BLL;
using DAL;
using NAudio.Wave;
using System.Collections;

namespace WebAPI
{
    /// <summary>
    /// Класс создания предопределенных данных в БД
    /// </summary>
    public static class AudioRecognitionSeed
    {
        // TODO: resolve dependency
        public static async Task SeedAsync(AudioFingerprintContext context)
        {
            try 
            {
                context.Database.EnsureCreated();

                if (context.Genre.Any())
                    return;

                var genres = new DAL.Genre[]
                {
                    new DAL.Genre { Name = "Rock" },        // 1
                    new DAL.Genre { Name = "Jazz" },        // 2
                    new DAL.Genre{ Name = "Pop" },          // 3
                    new DAL.Genre { Name = "Rap" },         // 4
                    new DAL.Genre { Name = "Hip-Hop" },     // 5
                    new DAL.Genre { Name = "Classic" },     // 6
                    new DAL.Genre { Name = "Electronic" },  // 7
                };

                foreach (var genre in genres)
                    context.Genre.Add(genre);

                await context.SaveChangesAsync();

                if (context.Artist.Any())
                    return;

                var artists = new DAL.Artist[]
                {
                    new DAL.Artist { // 1
                        Name = "Ice Nine Kills", 
                        Description = "Ice Nine Kills (sometimes stylized in all capital letters or abbreviated to INK, and formerly known as Ice Nine) is an American heavy metal band from Boston, Massachusetts, who are signed to Fearless Records. Best known for its horror-inspired lyrics, Ice Nine Kills formed in its earliest incarnation in 2000 by high school friends Spencer Charnas and Jeremy Schwartz. Charnas is currently the only remaining founding member.Ice Nine Kills has released three EPs along with six full-length studio albums: Last Chance to Make Amends, Safe Is Just a Shadow, The Predator Becomes the Prey, Every Trick in the Book, which peaked at number 122 on the US Billboard 200; The Silver Scream, which peaked at number 29 and their latest, The Silver Scream 2: Welcome to Horrorwood which peaked at number 18. Their band name is derived from the fictional substance ice-nine from the 1963 novel Cat's Cradle by Kurt Vonnegut", 
                        StartDate = new DateTime(2000, 06, 06), 
                        Cover=File.ReadAllBytes("G:\\audioDB\\Artists\\Ice_Nine_Kills.png") 
                    },
                    new DAL.Artist { // 2
                        Name = "Eminem",
                        Description = "Marshall Bruce Mathers III (born October 17, 1972), known professionally as Eminem, is an American rapper. He is credited with popularizing hip hop in Middle America and is widely regarded as one of the greatest rappers of all time. His global success is considered to have broken racial barriers to the acceptance of white rappers in popular music. While much of his transgressive work during the late 1990s and early 2000s made him a controversial figure, he came to be a representation of popular angst of the American underclass and has been cited as an influence by and upon many artists working in various genres",
                        StartDate = new DateTime(1998, 03, 05),
                        Cover=File.ReadAllBytes("G:\\audioDB\\Artists\\Eminem.png")
                    },
                    new DAL.Artist { // 3
                        Name = "Epidemic Sound",
                        Description = "Epidemic Sound is a global royalty-free soundtrack providing company based in Stockholm, Sweden. The company was established in 2009 by Peer Åström, David Stenmarck, Oscar Höglund, Hjalmar Winbladh and Jan Zachrisson. It has a library of over 40,000 soundtrack music and 90,000 sound effects. All Epidemic Sound tracks come in stems, which lets users remove certain layers of a track, like drums, bass, or the melody. Epidemic Sound music is also played in public spaces like restaurants, hotels, shopping malls and car parks. Epidemic Sound’s tracks are produced by a roster of music creators who get paid upfront for each track and half of the track’s streaming revenue",
                        StartDate = new DateTime(2009, 06, 18),
                        Cover=File.ReadAllBytes("G:\\audioDB\\Artists\\Epidemic_Sound.png")
                    },
                    new DAL.Artist { // 4
                        Name = "Erik Satie",
                        Description = "Eric Alfred Leslie Satie (17 May 1866 – 1 July 1925), who signed his name Erik Satie after 1884, was a French composer and pianist. He was the son of a French father and a British mother. He studied at the Paris Conservatoire, but was an undistinguished student and obtained no diploma. In the 1880s he worked as a pianist in café-cabaret in Montmartre, Paris, and began composing works, mostly for solo piano, such as his Gymnopédies and Gnossiennes. He also wrote music for a Rosicrucian sect to which he was briefly attached",
                        StartDate = new DateTime(1866, 05, 17),
                        Cover=File.ReadAllBytes("G:\\audioDB\\Artists\\Erik_Satie.png")
                    },
                    new DAL.Artist { // 5
                        Name = "Falling in Reverse",
                        Description = "Falling in Reverse is an American rock band that formed in 2008 by lead vocalist Ronnie Radke while he was incarcerated. The band's original name was \"From Behind These Walls\", but it was quickly renamed to Falling in Reverse shortly after formation. They are currently signed to Epitaph Records. The band has undergone numerous lineup changes, with Radke being the only remaining original member. The band is currently led by lead vocalist Radke, alongside guitarists Max Georgiev and Christian Thompson and bassist Tyler Burgess. The group released its debut album, The Drug in Me Is You, on July 26, 2011, which peaked at No. 19 on the Billboard 200, selling 18,000 copies in its first week. On December 17, 2019, the album was certified gold by RIAA equivalent to 500,000 copies sold. The band's second studio album, Fashionably Late, was released on June 18, 2013, which peaked at No. 17 on the Billboard 200. The band released their third album Just Like You on February 24, 2015. Their fourth album, Coming Home, was released on April 7, 2017. Their fifth album, \"Popular Monster\", is set to be released on July 26, 2024",
                        StartDate = new DateTime(2008, 09, 18),
                        Cover=File.ReadAllBytes("G:\\audioDB\\Artists\\Falling_In_Reverse.png")
                    },
                    new DAL.Artist { // 6
                        Name = "IVOXYGEN",
                        Description = "IVOXYGEN is creating ambient and dreamy style songs. Lyrics are about being out of the system, being in love, addicted or rejected and living the way a person wants",
                        StartDate = new DateTime(2019, 10, 09),
                        Cover=File.ReadAllBytes("G:\\audioDB\\Artists\\IVOXYGEN.png")
                    },
                    new DAL.Artist { // 7
                        Name = "Kishlak",
                        Description = "Фисенко Максим Сергеевич (родился 14 декабря, 1998 г.), более известный как Кишлак — основатель ранее существующего проекта Автостопом по фазе сна из Североморска, Мурманская область. Проект Автостопом по фазе сна был закрыт, просуществовав чуть больше года. После закрытия АПФС Максим сменил свой псевдоним на Кишлак. В 2021 году Макс переехал жить в Москву и стал усердно работать над новыми проектами.",
                        StartDate = new DateTime(2020, 08, 17),
                        Cover=File.ReadAllBytes("G:\\audioDB\\Artists\\Kishlak.png")
                    },
                    new DAL.Artist { // 8
                        Name = "MAYBE BABY",
                        Description = "Viktoriya Vladimirovna Lysyuk (Russian: Викто́рия Влади́мировна Лысю́к; born 27 September 1995), known professionally as Maybe Baby (Мэйби Бэйби), is a Belarusian-born Russian pop singer and rap artist",
                        StartDate = new DateTime(2018, 06, 13),
                        Cover=File.ReadAllBytes("G:\\audioDB\\Artists\\Maybe_Baby.png")
                    },
                    new DAL.Artist { // 9
                        Name = "Neverlove",
                        Description = "российская рок-группа из Москвы, ставшая популярной благодаря платформе TikTok. Свой стиль группа характеризует как «лав-метал» а также «глэм-метал»",
                        StartDate = new DateTime(2019, 07, 01),
                        Cover=File.ReadAllBytes("G:\\audioDB\\Artists\\Neverlove.png")
                    },
                    new DAL.Artist { // 10
                        Name = "Slava Marlow",
                        Description = "Artyom Artyomovich Gotlib (born October 27, 1999, in Novosibirsk), better known as Slava Marlow, is a Russian rapper, music producer and YouTuber. He first gained popularity after producing music for Russian rap star Morgenshtern in 2019. Since 2020, he has had a successful solo career",
                        StartDate = new DateTime(1998, 03, 05),
                        Cover=File.ReadAllBytes("G:\\audioDB\\Artists\\Slava_Marlow.png")
                    },
                    new DAL.Artist { // 11
                        Name = "Three Days of Rain",
                        Description = "Три дня дождя — российская рок-группа, основанная Глебом Викторовым в 2019 году. Является одной из самых успешных в новом поколении. Название Глеб взял в честь одноимённого клуба в Красноярске, про который ему рассказала старшая сестра.",
                        StartDate = new DateTime(2019, 10, 23),
                        Cover=File.ReadAllBytes("G:\\audioDB\\Artists\\Three_Days_Of_Rain.png")
                    },
                    new DAL.Artist { // 12
                        Name = "VOJ",
                        Description = "Mysterious artist",
                        StartDate = new DateTime(1998, 03, 05),
                        Cover=File.ReadAllBytes("G:\\audioDB\\Artists\\VOJ.png")
                    },
                    new DAL.Artist { // 13
                        Name = "Weeklyn",
                        Description = "Weeklyn is a musician born in 2003 and located in Minsk, Belarus. He started making music back in 2018. It was a good way for him to express his thoughts and problems. weeklyn was using SoundCloud to show his songs to the world. A couple of years later, he gained his first audience on Spotify. Weeklyn’s music and voice is often compared with Lil Peep, one of his biggest inspirations, who sadly passed away due to a drug overdose in 2017. On August 27, 2021 he released his biggest single, called Lithromantic, featuring Crymode. The song was added to the official Spotify playlist “Tear Drop”, with more than 1 million followers. Now the song has over 1.5 million streams. In 2022 he performed his first shows in his hometown, Minsk. It was a significant experience for the young artist, one that motivated him to further advance his musical career, share his experience with depression, and freely express his emotions with his small, but devoted fan base",
                        StartDate = new DateTime(1998, 03, 05),
                        Cover=File.ReadAllBytes("G:\\audioDB\\Artists\\Weeklyn.png")
                    },
                };

                foreach (var artist in artists)
                    context.Artist.Add(artist);

                await context.SaveChangesAsync();

                if (context.Album.Any())
                    return;

                var albums = new DAL.Album[]
                {
                    new DAL.Album { // 1
                        Name = "Bipolar", 
                        ArtistId = 11, 
                        PublishDate = new DateTime(2022, 10, 21), 
                        Cover=File.ReadAllBytes("G:\\audioDB\\Albums\\Bipolar.png")
                    },
                    new DAL.Album { // 2
                        Name = "Melancholy",
                        ArtistId = 11,
                        PublishDate = new DateTime(2023, 10, 20),
                        Cover=File.ReadAllBytes("G:\\audioDB\\Albums\\Melancholy.png")
                    },
                    new DAL.Album { // 3
                        Name = "Music To Be Murdered By",
                        ArtistId = 2,
                        PublishDate = new DateTime(2022, 10, 31),
                        Cover=File.ReadAllBytes("G:\\audioDB\\Albums\\Music_To_Be_Murdered_By.png")
                    },
                    new DAL.Album { // 4
                        Name = "Next Level",
                        ArtistId = 9,
                        PublishDate = new DateTime(2024, 03, 01),
                        Cover=File.ReadAllBytes("G:\\audioDB\\Albums\\Next_Level.png")
                    },
                    new DAL.Album { // 5
                        Name = "SHIK2",
                        ArtistId = 8,
                        PublishDate = new DateTime(2023, 11, 10),
                        Cover=File.ReadAllBytes("G:\\audioDB\\Albums\\SHIK.png")
                    },
                    new DAL.Album { // 6
                        Name = "TUZIK",
                        ArtistId = 10,
                        PublishDate = new DateTime(2022, 12, 16),
                        Cover=File.ReadAllBytes("G:\\audioDB\\Albums\\TUZIK.png")
                    },
                    new DAL.Album { // 7
                        Name = "Welcome To Horrorwood",
                        ArtistId = 1,
                        PublishDate = new DateTime(2021, 10, 20),
                        Cover=File.ReadAllBytes("G:\\audioDB\\Albums\\Welcome_To_Horrorwood.png")
                    },
                    new DAL.Album { // 8
                        Name = "Shawty",
                        ArtistId = 8,
                        PublishDate = new DateTime(2023, 11, 17),
                        Cover=File.ReadAllBytes("G:\\audioDB\\Albums\\Shawty.png")
                    },
                };

                foreach (var album in albums)
                    context.Album.Add(album);

                await context.SaveChangesAsync();

                if (context.Track.Any())
                    return;

                var tracks = new DAL.Track[]
                {
                    new DAL.Track { 
                        Title = "Encoding",
                        PublishDate = new DateTime(2023, 11, 10), 
                        Description = "Если следовать нити, что идет сквозь весь альбом, теперь мы приходим к тому что Максим ввязался наконец в борьбу с зависимостью и хочет признания",
                        Fingerprint = CreateFingerprint(File.ReadAllBytes("G:\\audioDB\\Tracks\\Encoding.mp3")), 
                        LSH = CreateLSH(File.ReadAllBytes("G:\\audioDB\\Tracks\\Encoding.mp3")), 
                        Lyrics = "",
                        GenreId = 1, 
                        ArtistId = 7, 
                        AlbumId = 5, 
                        Cover=File.ReadAllBytes("G:\\audioDB\\Songs\\Encoding.png") 
                    },
                    new DAL.Track {
                        Title = "Follow You",
                        PublishDate = new DateTime(2023, 10, 23),
                        Description = "«Иду за тобой» — история о парне и его зависимости от веществ. Он одинок и обессилен, а холод и тревога — его обычное состояние. Он заблудился в себе и ненавидит своё существование. Лирический герой хочет найти свой дом — место, где он мог бы почувствовать себя хорошо, где есть тепло и нет боли и одиночества. Но, не в состоянии справиться с эмоциональной ямой, поглощённый своими переживаниями, он уверен, что может найти всё это только в веществах. Вновь и вновь он прибегает к тому, что облегчит его страдание, лелея свою зависимость, которая разрушает его ещё больше. Он готов отдать себя чему угодно, лишь бы найти минутное утешение и забыться, сбежать от омрачающего одиночества и тяжёлых чувств, которые, несомненно, настигнут его снова. После эйфории он вернётся в ту же точку, ведь его жизнь по-прежнему осталась без изменений. Осознавая, что каждый раз попадает в замкнутый круг, герой не хочет приходить в сознание, не хочет возвращаться.",
                        Fingerprint = CreateFingerprint(File.ReadAllBytes("G:\\audioDB\\Tracks\\Follow_You.mp3")),
                        LSH = CreateLSH(File.ReadAllBytes("G:\\audioDB\\Tracks\\Follow_You.mp3")),
                        Lyrics = "",
                        GenreId = 1,
                        ArtistId = 11,
                        AlbumId = 2,
                        Cover=File.ReadAllBytes("G:\\audioDB\\Songs\\Follow_You.png")
                    },
                    new DAL.Track {
                        Title = "Godzilla",
                        PublishDate = new DateTime(2020, 01, 17),
                        Description = "Eminem and late rapper Juice WRLD team up for the first time on “Godzilla,” where they compare themselves to monsters. In particular, they become Godzilla, a fictional sea monster that is known for its mass destruction and endless killing. While this track marks the first collaboration between the two artists, Juice previously listed Em as one of his biggest influences and has frequently referenced his music, most notably on his May 2018 track, “Lean Wit Me.” In October 2018, Juice joined Tim Westwood TV, where he freestyled close to an hour over different Eminem beats. “Godzilla” also serves as Juice WRLD’s first posthumous release since his passing on December 8, 2019—six days after his 21st birthday. Juice reportedly recorded the chorus for the track before his untimely death, although he was unable to complete his actual verse for the song in time. Later in January 2020, a picture surfaced on XXL that depicted Juice WRLD and his friends after he recorded the chorus.",
                        Fingerprint = CreateFingerprint(File.ReadAllBytes("G:\\audioDB\\Tracks\\Godzilla.mp3")),
                        LSH = CreateLSH(File.ReadAllBytes("G:\\audioDB\\Tracks\\Godzilla.mp3")),
                        Lyrics = "",
                        GenreId = 4,
                        ArtistId = 2,
                        AlbumId = 3,
                        Cover=File.ReadAllBytes("G:\\audioDB\\Songs\\Godzilla.png")
                    },
                    new DAL.Track {
                        Title = "Gymnopedie",
                        PublishDate = new DateTime(1888, 12, 12),
                        Description = "",
                        Fingerprint = CreateFingerprint(File.ReadAllBytes("G:\\audioDB\\Tracks\\Gymnopedie.mp3")),
                        LSH = CreateLSH(File.ReadAllBytes("G:\\audioDB\\Tracks\\Gymnopedie.mp3")),
                        Lyrics = "",
                        GenreId = 6,
                        ArtistId = 4,
                        AlbumId = null,
                        Cover=File.ReadAllBytes("G:\\audioDB\\Songs\\Gymnopedie.png")
                    },
                    new DAL.Track {
                        Title = "I Dont Wanna Talk To People",
                        PublishDate = new DateTime(2022, 07, 22),
                        Description = "",
                        Fingerprint = CreateFingerprint(File.ReadAllBytes("G:\\audioDB\\Tracks\\I_Dont_Wanna_Talk_To_People.mp3")),
                        LSH = CreateLSH(File.ReadAllBytes("G:\\audioDB\\Tracks\\I_Dont_Wanna_Talk_To_People.mp3")),
                        Lyrics = "",
                        GenreId = 7,
                        ArtistId = 13,
                        AlbumId = null,
                        Cover=File.ReadAllBytes("G:\\audioDB\\Songs\\I_Dont_Wanna_Talk_To_People.png")
                    },
                    new DAL.Track {
                        Title = "Memory Reboot",
                        PublishDate = new DateTime(2023, 09, 01),
                        Description = "",
                        Fingerprint = CreateFingerprint(File.ReadAllBytes("G:\\audioDB\\Tracks\\Memory_Reboot.mp3")),
                        LSH = CreateLSH(File.ReadAllBytes("G:\\audioDB\\Tracks\\Memory_Reboot.mp3")),
                        Lyrics = "",
                        GenreId = 7,
                        ArtistId = 12,
                        AlbumId = null,
                        Cover=File.ReadAllBytes("G:\\audioDB\\Songs\\Memory_Reboot.png")
                    },
                    new DAL.Track {
                        Title = "Moscow Never Sleeps",
                        PublishDate = new DateTime(2023, 11, 17),
                        Description = "",
                        Fingerprint = CreateFingerprint(File.ReadAllBytes("G:\\audioDB\\Tracks\\Moscow_Never_Sleeps.mp3")),
                        LSH = CreateLSH(File.ReadAllBytes("G:\\audioDB\\Tracks\\Moscow_Never_Sleeps.mp3")),
                        Lyrics = "",
                        GenreId = 4,
                        ArtistId = 8,
                        AlbumId = 8,
                        Cover=File.ReadAllBytes("G:\\audioDB\\Songs\\Moscow_Never_Sleeps.png")
                    },
                    new DAL.Track {
                        Title = "Once Upon A Time In Detroit",
                        PublishDate = new DateTime(2016, 11, 24),
                        Description = "",
                        Fingerprint = CreateFingerprint(File.ReadAllBytes("G:\\audioDB\\Tracks\\Once_Upon_A_Time_In_Detroit.mp3")),
                        LSH = CreateLSH(File.ReadAllBytes("G:\\audioDB\\Tracks\\Once_Upon_A_Time_In_Detroit.mp3")),
                        Lyrics = "",
                        GenreId = 5,
                        ArtistId = 3,
                        AlbumId = null,
                        Cover=File.ReadAllBytes("G:\\audioDB\\Songs\\Once_Upon_A_Time_In_Detroit.png")
                    },
                    new DAL.Track {
                        Title = "Opinion",
                        PublishDate = new DateTime(2023, 11, 10),
                        Description = "",
                        Fingerprint = CreateFingerprint(File.ReadAllBytes("G:\\audioDB\\Tracks\\Opinion.mp3")),
                        LSH = CreateLSH(File.ReadAllBytes("G:\\audioDB\\Tracks\\Opinion.mp3")),
                        Lyrics = "",
                        GenreId = 4,
                        ArtistId = 7,
                        AlbumId = 5,
                        Cover=File.ReadAllBytes("G:\\audioDB\\Songs\\Opinion.png")
                    },
                    new DAL.Track {
                        Title = "ROOM",
                        PublishDate = new DateTime(2020, 12, 06),
                        Description = "",
                        Fingerprint = CreateFingerprint(File.ReadAllBytes("G:\\audioDB\\Tracks\\ROOM.mp3")),
                        LSH = CreateLSH(File.ReadAllBytes("G:\\audioDB\\Tracks\\ROOM.mp3")),
                        Lyrics = "",
                        GenreId = 7,
                        ArtistId = 6,
                        AlbumId = null,
                        Cover=File.ReadAllBytes("G:\\audioDB\\Songs\\ROOM.png")
                    },
                    new DAL.Track {
                        Title = "Tears To The Wind",
                        PublishDate = new DateTime(2022, 10, 21),
                        Description = "",
                        Fingerprint = CreateFingerprint(File.ReadAllBytes("G:\\audioDB\\Tracks\\Tears_To_The_Wind.mp3")),
                        LSH = CreateLSH(File.ReadAllBytes("G:\\audioDB\\Tracks\\Tears_To_The_Wind.mp3")),
                        Lyrics = "",
                        GenreId = 1,
                        ArtistId = 11,
                        AlbumId = 1,
                        Cover=File.ReadAllBytes("G:\\audioDB\\Songs\\Tears_To_The_Wind.png")
                    },
                    new DAL.Track {
                        Title = "The Drug In Me Is Reimagined",
                        PublishDate = new DateTime(2020, 02, 13),
                        Description = "",
                        Fingerprint = CreateFingerprint(File.ReadAllBytes("G:\\audioDB\\Tracks\\The_Drug_In_Me_Is_Reimagined.mp3")),
                        LSH = CreateLSH(File.ReadAllBytes("G:\\audioDB\\Tracks\\The_Drug_In_Me_Is_Reimagined.mp3")),
                        Lyrics = "",
                        GenreId = 1,
                        ArtistId = 5,
                        AlbumId = null,
                        Cover=File.ReadAllBytes("G:\\audioDB\\Songs\\The_Drug_In_Me_Is_Reimagined.png")
                    },
                    new DAL.Track {
                        Title = "The Shower Scene",
                        PublishDate = new DateTime(2021, 10, 12),
                        Description = "",
                        Fingerprint = CreateFingerprint(File.ReadAllBytes("G:\\audioDB\\Tracks\\The_Shower_Scene.mp3")),
                        LSH = CreateLSH(File.ReadAllBytes("G:\\audioDB\\Tracks\\The_Shower_Scene.mp3")),
                        Lyrics = "",
                        GenreId = 1,
                        ArtistId = 1,
                        AlbumId = 7,
                        Cover=File.ReadAllBytes("G:\\audioDB\\Songs\\The_Shower_Scene.png")
                    },
                    new DAL.Track {
                        Title = "Will Forget",
                        PublishDate = new DateTime(2022, 12, 16),
                        Description = "",
                        Fingerprint = CreateFingerprint(File.ReadAllBytes("G:\\audioDB\\Tracks\\Will_Forget.mp3")),
                        LSH = CreateLSH(File.ReadAllBytes("G:\\audioDB\\Tracks\\Will_Forget.mp3")),
                        Lyrics = "",
                        GenreId = 3,
                        ArtistId = 10,
                        AlbumId = 6,
                        Cover=File.ReadAllBytes("G:\\audioDB\\Songs\\Will_Forget.png")
                    },
                    new DAL.Track {
                        Title = "Wizard",
                        PublishDate = new DateTime(2024, 03, 01),
                        Description = "",
                        Fingerprint = CreateFingerprint(File.ReadAllBytes("G:\\audioDB\\Tracks\\Wizard.mp3")),
                        LSH = CreateLSH(File.ReadAllBytes("G:\\audioDB\\Tracks\\Wizard.mp3")),
                        Lyrics = "",
                        GenreId = 1,
                        ArtistId = 9,
                        AlbumId = 4,
                        Cover=File.ReadAllBytes("G:\\audioDB\\Songs\\Wizard.png")
                    },
                 };

                foreach (var track in tracks)
                    context.Track.Add(track);

                await context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        private static byte[] CreateFingerprint(byte[] data)
        {
            AudioData audioData = new AudioData(data);
            WaveFormatBuilder waveFormatBuilder = new WaveFormatBuilder();
            waveFormatBuilder.SetChannels(1);
            waveFormatBuilder.SetBitsPerSample(16);
            waveFormatBuilder.SetSampleRate(16000);
            WaveFormat waveFormat = waveFormatBuilder.GetWaveFormat();

            audioData.ChangeAudioFormat(waveFormat);

            SamplesExtractor samplesExtractor = new SamplesExtractor(audioData);

            CompactMfccExtractor mfccExtractor = new CompactMfccExtractor();

            byte[] fingerprint = mfccExtractor.ComputeFrom(waveFormat, samplesExtractor.samples);

            return fingerprint;
        }

        private static byte[] CreateLSH(byte[] data) 
        {
            var fingerprint = CreateFingerprint(data);
            int lshLength = fingerprint.Length > 14000 ? 14000 : fingerprint.Length;

            BitArray setBit = new BitArray(fingerprint.Skip((fingerprint.Length - lshLength) / 2).Take(lshLength).ToArray());

            bool[] set = setBit.Cast<bool>().Select(i => i).ToArray();

            MinHash minHash = new MinHash();

            int[] signature1 = minHash.ComputeMinHashSignature(set);

            byte[] result = new byte[signature1.Length * sizeof(int)];
            Buffer.BlockCopy(signature1, 0, result, 0, result.Length);

            return result;
        }
    }
}
