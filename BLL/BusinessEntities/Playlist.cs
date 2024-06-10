namespace BLL
{
    public class Playlist
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }
        public int UserId { get; set; }
        public byte[]? Cover { get; set; }

        public Playlist(int id, string name, DateTime creationDate, int userId, byte[]? cover)
        {
            Id = id;
            Name = name;
            CreationDate = creationDate;
            UserId = userId;
            Cover = cover;
        }
    }
}
