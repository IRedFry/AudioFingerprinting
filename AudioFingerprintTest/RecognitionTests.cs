using BLL;
using DAL;
using Microsoft.Extensions.Configuration;

namespace AudioFingerprintTest
{
    public class RecognitionTests
    {
        private CompactMfccExtractor compactMfccExtractor;
        private EuclidFingerprintComparer euclidFingerprintComparer;
        private AudioFingerprintContext audioFingerprintContext;
        private UnitOfWorkSQL unitOfWorkSQL;
        private RecognitionService recognitionService;
        [SetUp]
        public void Setup()
        {
            compactMfccExtractor = new CompactMfccExtractor();
            euclidFingerprintComparer = new EuclidFingerprintComparer();

            var testSettings = new Dictionary<string, string> {
            {"ConnectionStrings:DefaultConnection", "Server=(local);Database=AudioRecognitionDB;Trusted_Connection=True;Encrypt=False;"}
        };

            IConfiguration configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(testSettings)
            .Build();

            audioFingerprintContext = new AudioFingerprintContext(configuration);
            unitOfWorkSQL = new UnitOfWorkSQL(audioFingerprintContext);
            recognitionService = new RecognitionService(unitOfWorkSQL, compactMfccExtractor, euclidFingerprintComparer);
        }

        [Test]
        public void TrackRoomTest()
        {
            var base64 = Convert.ToBase64String(System.IO.File.ReadAllBytes("Z:\\Visual Studio 2022\\repos\\_Diplom\\examples\\sample_track5.mp3"));
            
            var trackId = recognitionService.GetBestRecognitionResult(base64, 1);

            Assert.That(trackId, Is.EqualTo(10));
        }

        [Test]
        public void TrackFollowYouTest()
        {
            var base64 = Convert.ToBase64String(System.IO.File.ReadAllBytes("Z:\\Visual Studio 2022\\repos\\_Diplom\\examples\\sample_track1.mp3"));

            var trackId = recognitionService.GetBestRecognitionResult(base64, 1);

            Assert.That(trackId, Is.EqualTo(2));
        }
        [Test]
        public void TrackMemoryRebootTest()
        {
            var base64 = Convert.ToBase64String(System.IO.File.ReadAllBytes("Z:\\Visual Studio 2022\\repos\\_Diplom\\examples\\sample_track2.mp3"));

            var trackId = recognitionService.GetBestRecognitionResult(base64, 1);

            Assert.That(trackId, Is.EqualTo(6));
        }
        [Test]
        public void TrackTheDrugInMeIsYouTest()
        {
            var base64 = Convert.ToBase64String(System.IO.File.ReadAllBytes("Z:\\Visual Studio 2022\\repos\\_Diplom\\examples\\sample_track6.mp3"));

            var trackId = recognitionService.GetBestRecognitionResult(base64, 1);

            Assert.That(trackId, Is.EqualTo(12));
        }
    }
}