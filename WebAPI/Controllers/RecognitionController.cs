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
    public class RecognitionController : ControllerBase
    {
        private readonly IRecognitionService recognitionService;
        private readonly ITrackService trackService;
        private readonly IUnityOfWork context;
        public RecognitionController(IRecognitionService recognitionService, ITrackService trackService, IUnityOfWork context)
        {
            this.recognitionService = recognitionService;
            this.trackService = trackService;
            this.context = context;
        }

        [HttpPost]
        public async Task<IActionResult> UploadTrack([FromBody] AudioBase64ViewModel audioBase64)
        {
            int trackId = recognitionService.GetBestRecognitionResult(audioBase64.base64, audioBase64.userId);

            return Ok(new { created = "yes", trackId });
        }

        [HttpGet]
        public async Task<IActionResult> Test()
        {
            var base64 = Convert.ToBase64String(System.IO.File.ReadAllBytes("Z:\\Visual Studio 2022\\repos\\_Diplom\\examples\\sample_track5.mp3"));

            var trackId = recognitionService.GetBestRecognitionResult(base64, 1);


            return Ok(new { created = "yes", trackId});
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRecommendations(int id)
        {
            var tracksIds = recognitionService.GetRecommendations(id);
            List<TrackDTO> tracks = new List<TrackDTO>();
            foreach (var trackId in tracksIds)
                tracks.Add(new TrackDTO(trackService.GetTrack(trackId), context));

            return Ok(new { recommended = "yes", tracks });
        }
    }
}
