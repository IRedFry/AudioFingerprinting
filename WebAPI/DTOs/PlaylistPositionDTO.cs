namespace BLL
{
    public class PlaylistPositionDTO
    {
        public int Id { get; set; }
        public int PlaylistId { get; set; }
        public int TrackId { get; set; }

        public PlaylistPositionDTO(int id, int playlistId, int trackId)
        {
            Id = id;
            PlaylistId = playlistId;
            TrackId = trackId;
        }
    }
}
