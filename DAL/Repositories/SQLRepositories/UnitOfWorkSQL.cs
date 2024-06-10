using BLL;

namespace DAL
{
    public class UnitOfWorkSQL : IUnityOfWork
    {
        private AudioFingerprintContext context;
        private AlbumRepositorySQL albumRepositorySQL;
        private ArtistRepositorySQL artistRepositorySQL;
        private GenreRepositorySQL genreRepositorySQL;
        private PlaylistRepositorySQL playlistRepositorySQL;
        private PlaylistPositionRepositorySQL playlistPositionRepositorySQL;
        private RecognitionHistoryRepositorySQL recognitionHistoryRepositorySQL;
        private TrackRepositorySQL trackRepositorySQL;

        public UnitOfWorkSQL(AudioFingerprintContext context)
        {
            this.context = context;
        }

        public IRepository<BLL.Album> Albums => albumRepositorySQL ?? (albumRepositorySQL = new AlbumRepositorySQL(context));

        public IRepository<BLL.Artist> Artists
        {
            get
            {
                if (artistRepositorySQL == null)
                    artistRepositorySQL = new ArtistRepositorySQL(context);
                return artistRepositorySQL;
            }
        }

        public IRepository<BLL.Genre> Genres
        {
            get
            {
                if (genreRepositorySQL == null)
                    genreRepositorySQL = new GenreRepositorySQL(context);
                return genreRepositorySQL;
            }
        }
        
        public IRepository<BLL.Playlist> Playlists
        {
            get
            {
                if (playlistRepositorySQL == null)
                    playlistRepositorySQL = new PlaylistRepositorySQL(context);
                return playlistRepositorySQL;
            }
        }
        
        public IRepository<BLL.PlaylistPosition> PlaylistPositions
        {
            get
            {
                if (playlistPositionRepositorySQL == null)
                    playlistPositionRepositorySQL = new PlaylistPositionRepositorySQL(context);
                return playlistPositionRepositorySQL;
            }
        }
        
        public IRepository<BLL.RecognitionHistory> RecognitionHistory
        {
            get
            {
                if (recognitionHistoryRepositorySQL == null)
                    recognitionHistoryRepositorySQL = new RecognitionHistoryRepositorySQL(context);
                return recognitionHistoryRepositorySQL;
            }
        }
        
        public IRepository<BLL.Track> Tracks
        {
            get
            {
                if (trackRepositorySQL == null)
                    trackRepositorySQL = new TrackRepositorySQL(context);
                return trackRepositorySQL;
            }
        }
       


        public int Save()
        {
            return context.SaveChanges();
        }
    }
}
