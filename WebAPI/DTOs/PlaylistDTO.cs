namespace BLL
{
    public class PlaylistDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }
        public int TrackCount { get; set; }
        public int UserId { get; set; }
        public byte[]? Cover { get; set; }

        public PlaylistDTO(int id, string name, DateTime creationDate, int userId, int trackCount, byte[]? cover)
        {
            Id = id;
            Name = name;
            CreationDate = creationDate;
            UserId = userId;
            TrackCount = trackCount;
            Cover = cover;
        }

        public PlaylistDTO(Playlist playlist, IUnityOfWork context)
        {
            Id = playlist.Id;
            Name = playlist.Name;
            CreationDate = playlist.CreationDate;
            UserId = playlist.UserId;
            Cover = playlist.Cover;
            TrackCount = context.PlaylistPositions.GetList().Where(i => i.PlaylistId == Id).Count();
        }

        public PlaylistDTO() { }
    }
}
