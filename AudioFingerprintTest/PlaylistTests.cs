using BLL;
using DAL;
using Microsoft.Extensions.Configuration;

namespace AudioFingerprintTest
{
    public class PlaylistTests
    {

        private AudioFingerprintContext audioFingerprintContext;
        private UnitOfWorkSQL unitOfWorkSQL;
        private PlaylistService playlistService;
        private string testPlaylistName;
        [SetUp]
        public void Setup()
        {
            testPlaylistName = "Test playlist";

            var testSettings = new Dictionary<string, string> {
            {"ConnectionStrings:DefaultConnection", "Server=(local);Database=AudioRecognitionDB;Trusted_Connection=True;Encrypt=False;"}
        };

            IConfiguration configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(testSettings)
            .Build();

            audioFingerprintContext = new AudioFingerprintContext(configuration);
            unitOfWorkSQL = new UnitOfWorkSQL(audioFingerprintContext);
            playlistService = new PlaylistService(unitOfWorkSQL);
        }

        [Test]
        public void CreatePlaylistTest()
        {
            BLL.Playlist playlist = new BLL.Playlist(0, testPlaylistName, DateTime.Now, 1, null);

            playlistService.CreatePlaylist(playlist);

            var createdPlaylist = unitOfWorkSQL.Playlists.GetList().Where(i => i.Name == playlist.Name).FirstOrDefault();

            Assert.IsNotNull(createdPlaylist);
        }


        [Test]
        public void DeleteCreatedPlaylistTest()
        {

            var createdPlaylist = unitOfWorkSQL.Playlists.GetList().Where(i => i.Name == testPlaylistName).FirstOrDefault();
            unitOfWorkSQL.Playlists.Delete(createdPlaylist.Id);
            unitOfWorkSQL.Save();
            var deletedPlaylist = unitOfWorkSQL.Playlists.GetList().Where(i => i.Name == testPlaylistName).FirstOrDefault();


            Assert.IsNull(deletedPlaylist);
        }

    }
}