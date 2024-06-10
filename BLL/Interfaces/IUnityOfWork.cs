namespace BLL
{
    public interface IUnityOfWork
    {
        IRepository<Album> Albums { get; }
        IRepository<Artist> Artists { get; }
        IRepository<Genre> Genres { get; }
        IRepository<Playlist> Playlists { get; }
        IRepository<PlaylistPosition> PlaylistPositions { get; }
        IRepository<RecognitionHistory> RecognitionHistory { get; }
        IRepository<Track> Tracks { get; }
        int Save();
    }
}
