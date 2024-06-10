
namespace BLL
{
    public class Track
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime PublishDate { get; set; }
        public string? Description { get; set; }
        public byte[] Fingerprint { get; set; }
        public byte[] LSH { get; set; }
        public string? Lyrics { get;set; }
        public byte[]? Cover { get; set; }
        public int GenreId { get; set; }
        public int ArtistId { get; set; }
        public int? AlbumId { get; set; }

        public Track(int id, string title, DateTime publishDate, string? description, byte[] fingerprint, string? lyrics, byte[]? cover, int genreId, int artistId, int? albumId, byte[] lsh = null)
        {
            Id = id;
            Title = title;
            PublishDate = publishDate;
            Description = description;
            Fingerprint = fingerprint;
            Lyrics = lyrics;
            Cover = cover;
            GenreId = genreId;
            ArtistId = artistId;
            AlbumId = albumId;
            LSH = lsh;
        }

        public Track() { }
    }
}
