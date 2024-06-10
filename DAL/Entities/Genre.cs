
namespace DAL
{
    /// <summary>
    /// Сущность жанра
    /// </summary>
    public class Genre
    {
        public Genre() { }

        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Track> Track { get; set; } = new List<Track>();
    }
}
