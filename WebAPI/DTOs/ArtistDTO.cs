using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace BLL
{
    public class ArtistDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public DateTime StartDate { get; set; }
        public byte[]? Cover { get; set; }

        public ArtistDTO(int id, string name, string? description, DateTime startDate, byte[]? cover)
        {
            Id = id;
            Name = name;
            Description = description;
            StartDate = startDate;
            Cover = cover;
        }

        public ArtistDTO (Artist artist)
        {
            Id = artist.Id;
            Name = artist.Name;
            Description = artist.Description;
            StartDate = artist.StartDate;
            Cover = artist.Cover;
        }

        public ArtistDTO() { }
    }
}
