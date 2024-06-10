using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace BLL
{
    public class AlbumDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime PublishDate { get; set; }
        public string ArtistName { get; set; }
        public int ArtistId { get; set; }
        public byte[]? Cover { get; set; }

        public AlbumDTO(int id, string name, DateTime publishDate, int artistId, string artistName, byte[]? cover) 
        {
            Id = id;
            Name = name;
            PublishDate = publishDate;
            ArtistId = artistId;
            ArtistName = artistName;
            Cover = cover;
        }

        public AlbumDTO (Album album, IUnityOfWork context)
        {
            Id = album.Id;
            Name = album.Name;
            PublishDate = album.PublishDate;
            ArtistId = album.ArtistId;
            ArtistName = context.Artists.GetItem(album.ArtistId).Name;
            Cover = album.Cover;
        }

        public AlbumDTO() { }
    }
}
