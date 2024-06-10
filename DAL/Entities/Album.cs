using System.ComponentModel.DataAnnotations.Schema;

namespace DAL
{
    /// <summary>
    /// Сущность альбома
    /// </summary>
    public class Album
    {
        public Album() { }

        public int Id { get; set; }
        public string Name { get; set; }
        public int ArtistId {  get; set; }
        public DateTime PublishDate { get; set; }
        [Column(TypeName = "varbinary(MAX)")]
        public byte[]? Cover { get; set; }

        public virtual Artist Artist { get; set; }
        public virtual ICollection<Track> Track { get; set; } = new List<Track>();

    }
}
