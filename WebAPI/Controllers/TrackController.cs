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
    public class TrackController : ControllerBase
    {
        private readonly ITrackService trackService;
        private readonly IUnityOfWork context;
        public TrackController(ITrackService trackService, IUnityOfWork context)
        {
            this.trackService = trackService;
            this.context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TrackDTO>>> GetTracks()
        {
            return await Task.Run(() => trackService.GetTracks().Select(i => new TrackDTO(i, context)).ToList());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<TrackDTO>> GetTrack(int id)
        {
            var track = trackService.GetTrack(id);
            if (track == null)
                return NotFound();

            return await Task.Run(() => new TrackDTO(track, context));
        }

        [HttpGet("Artist/{artistId}")]
        public async Task<ActionResult<IEnumerable<TrackDTO>>> GetTracksOfArtist(int artistId)
        {
            return await Task.Run(() => trackService.GetTracksOfArtist(artistId).Select(i => new TrackDTO(i ,context)).ToList());
        }
        [HttpGet("Album/{albumId}")]
        public async Task<ActionResult<IEnumerable<TrackDTO>>> GetTracksOfAlbum(int albumId)
        {
            return await Task.Run(() => trackService.GetTracksOfAlbum(albumId).Select(i => new TrackDTO(i, context)).ToList());
        }

        [HttpGet("Artist/LastRelease/{artistId}")]
        public async Task<ActionResult<TrackDTO>> GetLastReleaseOfArtist(int artistId)
        {
            var lastRelease =  trackService.GetLastReleaseOfArtist(artistId);
            if (lastRelease == null)
                return NotFound();
            return await Task.Run(() => new TrackDTO(lastRelease, context));
        }
        [HttpGet("Genre")]
        public async Task<ActionResult<IEnumerable<GenreDTO>>> GetGenres()
        {
            return await Task.Run(() => trackService.GetGenres().Select(i => new GenreDTO(i)).ToList());
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<TrackDTO>>> GetTrackByMatch([FromBody] string matchString)
        {
            return await Task.Run(() => trackService.GetTracksMatchedString(matchString).Select(i => new TrackDTO(i, context)).ToList());
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> DeleteTrack(int id)
        {
            trackService.DeleteTrack(id);

            return Ok(new { message = "трек успешно удален"}); 
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateTrack([FromBody] TrackDTO track)
        {
            trackService.AddTrack(new Track(track.Id, track.Title, track.PublishDate, track.Description, track.Fingerprint, track.Lyrics, track.Cover, track.GenreId, track.ArtistId, track.AlbumId));

            return Created("", new { created = "yes", message = "трек успешно добавлен" });
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTrack([FromBody] TrackDTO track)
        {
            trackService.UpdateTrack(new Track(track.Id, track.Title, track.PublishDate, track.Description, track.Fingerprint, track.Lyrics, track.Cover, track.GenreId, track.ArtistId, track.AlbumId));

            return Created("", new { created = "yes", message = "трек успешно обновлен" });
        }

    }
}
