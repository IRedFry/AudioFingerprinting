using BLL;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    /// <summary>
    /// Контроллер треков
    /// </summary>

    [Route("api/[controller]")]
    [EnableCors]
    [ApiController]
    public class AlbumController : ControllerBase
    {
        private readonly IAlbumService albumService;
        private readonly IUnityOfWork context;
        public AlbumController(IAlbumService albumService, IUnityOfWork context)
        {
            this.albumService = albumService;
            this.context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AlbumDTO>>> GetAlbums()
        {
            return await Task.Run(() => albumService.GetAlbums().Select(i => new AlbumDTO(i, context)).ToList());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<AlbumDTO>> GetAlbum(int id)
        {
            var album = albumService.GetAlbum(id);
            if (album == null)
                return NotFound();

            return await Task.Run(() => new AlbumDTO(album, context));
        }

        [HttpGet("Artist/{artistId}")]
        public async Task<ActionResult<IEnumerable<AlbumDTO>>> GetTrackOfArtist(int artistId)
        {
            return await Task.Run(() => albumService.GetAlbumsOfArtist(artistId).Select(i => new AlbumDTO(i, context)).ToList());
        }


        [HttpPost]
        public async Task<ActionResult<IEnumerable<AlbumDTO>>> GetAlbumsByMatch([FromBody] string matchString)
        {
            return await Task.Run(() => albumService.GetAlbumsMatchedString(matchString).Select(i => new AlbumDTO(i, context)).ToList());
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateAlbum([FromBody] AlbumDTO album)
        {
            albumService.AddAlbum(new Album(album.Id, album.Name, album.PublishDate, album.ArtistId, album.Cover));
            return Created("", new { created = "yes", message = "альбом успешно добавлен" });
        }
        [HttpPut]
        public async Task<IActionResult> UpdateAlbum([FromBody] AlbumDTO album)
        {
            albumService.UpdateAlbum(new Album(album.Id, album.Name, album.PublishDate, album.ArtistId, album.Cover));

            return Created("", new { created = "yes", message = "альбом успешно обновлен" });
        }
        [HttpPost("{id}")]
        public async Task<IActionResult> DeleteAlbum(int id)
        {
            albumService.DeleteAlbum(id);

            return Ok(new { message = "альбом успешно удален" });
        }

    }
}
