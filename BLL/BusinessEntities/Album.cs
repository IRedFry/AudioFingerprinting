namespace BLL
{
    public class Album
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime PublishDate { get; set; }
        public int ArtistId { get; set; }
        public byte[]? Cover { get; set; }

        public Album(int id, string name, DateTime publishDate, int artistId, byte[]? cover)
        {
            Id = id;
            Name = name;
            PublishDate = publishDate;
            ArtistId = artistId;
            Cover = cover;
        }
    }
}
