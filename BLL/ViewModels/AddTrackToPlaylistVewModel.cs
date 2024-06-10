using System.ComponentModel.DataAnnotations;

namespace BLL
{
    public class AddTrackToPlaylistVewModel
    {
        [Required]
        public int playlistId { get; set; }
        [Required]
        public int trackId { get; set; }
    }
}
