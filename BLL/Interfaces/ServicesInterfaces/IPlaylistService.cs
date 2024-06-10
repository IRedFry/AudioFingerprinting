namespace BLL
{
    public interface IPlaylistService
    {
        public List<Playlist> GetPlaylists();
        public Playlist GetPlaylist(int id);
        public List<Playlist> GetPlaylistsOfUser(int userId);
        public void CreatePlaylist(Playlist playlist);
        public void DeletePlaylist(int id);
        public int AddTrackToPlaylist(int playlistId, int trackId);
        public void RemoveTrackFromPlaylist (int positionId);
    }
}
