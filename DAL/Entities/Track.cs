
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL
{
    /// <summary>
    /// Сущность трека (песни)
    /// </summary>
    public class Track
    {
        public Track() { }

        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime PublishDate { get; set; }
        public string? Description { get; set; }
        [Column(TypeName = "varbinary(MAX)")]
        public byte[] Fingerprint { get; set; }
        [Column(TypeName = "varbinary(MAX)")]
        public byte[] LSH { get; set; }
        public string? Lyrics { get;set; }
        [Column(TypeName = "varbinary(MAX)")]
        public byte[]? Cover { get; set; }

        public int GenreId { get; set; }
        public int ArtistId { get; set; }
        public int? AlbumId { get; set; }

        public virtual Genre Genre { get; set; }
        public virtual Artist Artist { get; set; }
        public virtual Album? Album { get; set; }

        public virtual ICollection<PlaylistPosition> PlaylistPosition { get; set; } = new List<PlaylistPosition>();
        public virtual ICollection<RecognitionHistory> RecognitionHistory { get; set; } = new List<RecognitionHistory>();

    }
}
