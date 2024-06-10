using System.Threading.Tasks.Dataflow;

namespace BLL
{
    public class HistoryService : IHistoryService
    {
        private IUnityOfWork context;
        public HistoryService(IUnityOfWork context)
        {
            this.context = context;
        }

        public void AddTrackToHistory(int trackId, int userId)
        {
            RecognitionHistory history = new RecognitionHistory(default, DateTime.Now, userId, trackId);
            context.RecognitionHistory.Create(history);
            context.Save();
        }

        public List<Tuple<Track, DateTime>> GetRecognitionHistoryOfUser(int userId)
        {
            var history = context.RecognitionHistory.GetList().Where(i => i.UserId == userId).OrderByDescending(i => i.RecognitionDate).ToList();                
            var tracks = new List<Tuple<Track, DateTime>>();

            foreach (var position in history)
            {
                Track track = context.Tracks.GetItem(position.TrackId);

                tracks.Add(new Tuple<Track, DateTime>(track, position.RecognitionDate));
            }

            return tracks;
            
        }
    }
}
