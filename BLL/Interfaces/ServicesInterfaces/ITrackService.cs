namespace BLL
{
    public interface ITrackService
    {
        public Track GetTrack(int id);
        public Track GetLastReleaseOfArtist(int artistId);
        public List<Track> GetTracks();
        public List<Genre> GetGenres();
        public List<Track> GetTracksOfArtist(int artistId);
        public List<Track> GetTracksOfAlbum(int albumId);
        public void AddTrack(Track track);
        public List<Track> GetTracksMatchedString(string expression);
        public void UpdateTrack(Track track);
        public void DeleteTrack(int id);
        public List<Tuple<Track, int>> GetTracksOfPlaylist(int playlistId);
    }
}
