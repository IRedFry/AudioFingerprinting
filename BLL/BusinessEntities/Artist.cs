namespace BLL
{
    public class Artist
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public DateTime StartDate { get; set; }
        public byte[]? Cover { get; set; }

        public Artist(int id, string name, string? description, DateTime startDate, byte[]? cover)
        {
            Id = id;
            Name = name;
            Description = description;
            StartDate = startDate;
            Cover = cover;
        }
    }
}
