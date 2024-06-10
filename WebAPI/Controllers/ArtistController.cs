using BLL;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    /// <summary>
    /// Контроллер исполнителей
    /// </summary>

    [Route("api/[controller]")]
    [EnableCors]
    [ApiController]
    public class ArtistController : ControllerBase
    {
        private readonly IArtistService artistService;

        public ArtistController(IArtistService artistService)
        {
            this.artistService = artistService;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ArtistDTO>>> GetArtists()
        {
            return await Task.Run(() => artistService.GetArtists().Select(i => new ArtistDTO(i)).ToList());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ArtistDTO>> GetArtist(int id)
        {
            var artist = artistService.GetArtist(id);
            if (artist == null)
                return NotFound();

            return await Task.Run(() => new ArtistDTO(artist));
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<ArtistDTO>>> GetTrackByMatch([FromBody] string matchString)
        {
            return await Task.Run(() => artistService.GetArtistsMatchedString(matchString).Select(i => new ArtistDTO(i)).ToList());
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateArtist([FromBody] ArtistDTO artist)
        {
            artistService.AddArtist(new Artist(artist.Id, artist.Name, artist.Description, artist.StartDate, artist.Cover));
            return Created("", new { created = "yes", message = "исполнитель успешно добавлен" });
        }

        [HttpPut]
        public async Task<IActionResult> UpdateArtist([FromBody] ArtistDTO artist)
        {
            artistService.UpdateArtist(new Artist(artist.Id, artist.Name, artist.Description, artist.StartDate, artist.Cover));

            return Created("", new { created = "yes", message = "артист успешно обновлен" });
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> DeleteArtist(int id)
        {
            artistService.DeleteArtist(id);

            return Ok(new { message = "артист успешно удален" });
        }


    }
}
