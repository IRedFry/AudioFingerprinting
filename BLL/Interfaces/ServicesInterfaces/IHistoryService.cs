namespace BLL
{
    public interface IHistoryService
    {
        public List<Tuple<Track, DateTime>> GetRecognitionHistoryOfUser(int userId);
        public void AddTrackToHistory(int trackId, int userId);
    }
}
