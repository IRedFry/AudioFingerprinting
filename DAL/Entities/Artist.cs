
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL
{
    /// <summary>
    /// Сущность исполнителя
    /// </summary>
    public class Artist
    {
        public Artist() { }

        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public DateTime StartDate { get; set; }
        [Column(TypeName = "varbinary(MAX)")]
        public byte[]? Cover { get; set; }
        public virtual ICollection<Track> Track { get; set; } = new List<Track>();
        public virtual ICollection<Album> Album { get; set; } = new List<Album>();
    }
}
