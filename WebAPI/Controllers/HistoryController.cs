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
    public class HistoryController : ControllerBase
    {
        private readonly IHistoryService historyService;
        private readonly IUnityOfWork context;
        public HistoryController(IHistoryService historyService, IUnityOfWork context)
        {
            this.historyService = historyService;
            this.context = context;
        }
        [HttpGet("user/{id}")]
        public async Task<ActionResult<IEnumerable<TrackDTO>>> GetTracks(int id)
        {
            return await Task.Run(() => historyService.GetRecognitionHistoryOfUser(id).Select(i => {
                var t = new TrackDTO(i.Item1, context);
                t.RecognitionDate = i.Item2;
                return t;
            }).ToList());
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateTrack([FromBody] RecognitionHistoryDTO history)
        {
            historyService.AddTrackToHistory(history.TrackId, history.UserId);

            return Created("", new { created = "yes", message = "трек успешно добавлен в историю" });
        }
    }
}
