namespace BLL
{
    public class TrackDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime PublishDate { get; set; }
        public string? Description { get; set; }
        public byte[] Fingerprint { get; set; }
        public string? Lyrics { get; set; }
        public string ArtistName { get; set; }
        public string? AlbumName { get; set; }
        public DateTime? AlbumDate { get; set; }
        public string GenreName { get; set; }
        public int GenreId { get; set; }
        public int ArtistId { get; set; }
        public int? AlbumId { get; set; }
        public byte[]? Cover { get; set; }
        public int? PositionId { get; set; }
        public DateTime? RecognitionDate { get; set; }
        public TrackDTO(int id, string title, DateTime publishDate, string? description, byte[] fingerprint, string? lyrics, int genreId, int artistId, int? albumId, string artistName, string? albumName, DateTime? albumDate, string genreName, byte[]? cover, int? positionId, DateTime? recognitionDate = null)
        {
            Id = id;
            Title = title;
            PublishDate = publishDate;
            Description = description;
            Fingerprint = fingerprint;
            Lyrics = lyrics;
            GenreId = genreId;
            ArtistId = artistId;
            AlbumId = albumId;
            ArtistName = artistName;
            AlbumName = albumName;
            AlbumDate = albumDate;
            GenreName = genreName;
            Cover = cover;
            PositionId = positionId;
            RecognitionDate = recognitionDate;
        }

        public TrackDTO(Track track, IUnityOfWork context)
        {
            Id = track.Id;
            Title = track.Title;
            PublishDate = track.PublishDate;
            Description = track.Description;
            Fingerprint = track.Fingerprint;
            Lyrics = track.Lyrics;
            GenreId = track.GenreId;
            ArtistId = track.ArtistId;
            AlbumId = track.AlbumId;
            Cover = track.Cover;

            var album = track.AlbumId == null ? null : context.Albums.GetItem((int)track.AlbumId);

            ArtistName = context.Artists.GetItem(track.ArtistId).Name;
            AlbumName = album == null ? null : album.Name;
            AlbumDate = album == null ? null : album.PublishDate;
            GenreName = context.Genres.GetItem(track.GenreId).Name;
        }

        public TrackDTO() { }
    }
}
