
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL
{
    /// <summary>
    /// Сущность плейлиста
    /// </summary>
    public class Playlist
    {
        public Playlist() { }
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }
        public int UserId { get; set; }
        [Column(TypeName = "varbinary(MAX)")]
        public byte[]? Cover { get; set; }

        public virtual User User { get; set; }

        public virtual ICollection<PlaylistPosition> PlaylistPosition { get; set; } = new List<PlaylistPosition>();
    }
}
