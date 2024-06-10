using BLL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace WebAPI.Controllers
{
    /// <summary>
    /// Контроллер треков
    /// </summary>

    [Route("api/[controller]")]
    [EnableCors]
    [ApiController]
    public class PlaylistController : ControllerBase
    {
        private readonly IPlaylistService playlistService;
        private readonly ITrackService trackService;
        private readonly IUnityOfWork context;
        public PlaylistController(IPlaylistService playlistService, ITrackService trackService, IUnityOfWork context)
        {
            this.playlistService = playlistService;
            this.trackService = trackService;
            this.context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlaylistDTO>>> GetPlaylists()
        {
            return await Task.Run(() => playlistService.GetPlaylists().Select(i => new PlaylistDTO(i ,context)).ToList());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<PlaylistDTO>> GetPlaylist(int id)
        {
            var playlist = playlistService.GetPlaylist(id);
            if (playlist == null)
                return NotFound();

            return await Task.Run(() => new PlaylistDTO(playlist, context));
        }
        [HttpGet("{id}/track")]
        public async Task<ActionResult<IEnumerable<TrackDTO>>> GetPlaylistsTracks(int id)
        {
            return await Task.Run(() => trackService.GetTracksOfPlaylist(id).Select(i =>
            {
                var t = new TrackDTO(i.Item1, context);
                t.PositionId = i.Item2;
                return t;
            }).ToList());
        }

        [HttpGet("User/{userId}")]
        public async Task<ActionResult<IEnumerable<PlaylistDTO>>> GetPlaylistsOfUser(int userId)
        {
            return await Task.Run(() => playlistService.GetPlaylistsOfUser(userId).Select(i => new PlaylistDTO(i, context)).ToList());
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> DeletePlaylist(int id)
        {
            playlistService.DeletePlaylist(id);

            return Ok(new { message = "Плейлист успешно удален"}); 
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreatePlaylist([FromBody] PlaylistDTO playlist)
        {
            playlist.CreationDate = DateTime.Now;
            playlistService.CreatePlaylist(new Playlist(playlist.Id, playlist.Name, DateTime.Now, playlist.UserId, playlist.Cover));

            return Created("", new { created = "yes", message = "плейлист успешно добавлен" });
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddTrackToPlaylist([FromBody] AddTrackToPlaylistVewModel addTrackToPlaylist)
        {
            int positionId = playlistService.AddTrackToPlaylist(addTrackToPlaylist.playlistId, addTrackToPlaylist.trackId);

            return Ok(new {message = "Трек успешно добавлен в плейлист", positionId });
        }
        [HttpPost("Position/{id}")]
        public async Task<IActionResult> RemoveTrackFromPlaylist(int id) 
        {
            playlistService.RemoveTrackFromPlaylist(id);
            return Ok(new { message = "Трек успешно удален из плейлиста"});
        }

    }
}
