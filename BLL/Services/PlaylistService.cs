
namespace BLL
{
    public class PlaylistService : IPlaylistService
    {
        private IUnityOfWork context;

        public PlaylistService(IUnityOfWork context)
        {
            this.context = context;
        }
        public int AddTrackToPlaylist(int playlistId, int trackId)
        {

            PlaylistPosition position = new PlaylistPosition(default, playlistId, trackId);
            context.PlaylistPositions.Create(position);
            context.Save();
            var newPosition = context.PlaylistPositions.GetList().Where(i => i.TrackId == trackId && i.PlaylistId == playlistId).OrderByDescending(i => i.Id).FirstOrDefault();
            return newPosition == null ? -1 : newPosition.Id;

        }

        public void CreatePlaylist(Playlist playlist)
        {
            context.Playlists.Create(playlist);
            context.Save();
        }

        public void DeletePlaylist(int id)
        {
            context.Playlists.Delete(id);
            context.Save();
        }

        public Playlist GetPlaylist(int id)
        {
            return context.Playlists.GetItem(id);
        }

        public List<Playlist> GetPlaylists()
        {
            return context.Playlists.GetList().ToList();
        }

        public List<Playlist> GetPlaylistsOfUser(int userId)
        {
            return context.Playlists.GetList().Where(i => i.UserId == userId).ToList();
        }

        public void RemoveTrackFromPlaylist(int positionId)
        {
            context.PlaylistPositions.Delete(positionId);
            context.Save();
        }
    }
}
