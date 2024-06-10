using BLL;
using DAL;
using Microsoft.Extensions.Configuration;

namespace AudioFingerprintTest
{
    public class TrackTests
    {

        private AudioFingerprintContext audioFingerprintContext;
        private UnitOfWorkSQL unitOfWorkSQL;

        [SetUp]
        public void Setup()
        {


            var testSettings = new Dictionary<string, string> {
            {"ConnectionStrings:DefaultConnection", "Server=(local);Database=AudioRecognitionDB;Trusted_Connection=True;Encrypt=False;"}
        };

            IConfiguration configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(testSettings)
            .Build();

            audioFingerprintContext = new AudioFingerprintContext(configuration);
            unitOfWorkSQL = new UnitOfWorkSQL(audioFingerprintContext);
        }

        [Test]
        public void GetFollowYouTrackTest()
        {
            var track = unitOfWorkSQL.Tracks.GetList().Where(i => i.Title == "Follow You").FirstOrDefault();

            Assert.NotNull(track);
        }

        [Test]
        public void GetMemoryRebootTrackTest()
        {
            var track = unitOfWorkSQL.Tracks.GetList().Where(i => i.Title == "Memory Reboot").FirstOrDefault();

            Assert.NotNull(track);
        }
    }
}