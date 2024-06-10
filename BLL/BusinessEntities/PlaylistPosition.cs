namespace BLL
{
    public class PlaylistPosition
    {
        public int Id { get; set; }
        public int PlaylistId { get; set; }
        public int TrackId { get; set; }

        public PlaylistPosition(int id, int playlistId, int trackId)
        {
            Id = id;
            PlaylistId = playlistId;
            TrackId = trackId;
        }
    }
}
