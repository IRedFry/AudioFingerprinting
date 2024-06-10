namespace BLL
{
    public interface IRecognitionService
    {
        public int GetBestRecognitionResult(string base64, int userId);
        public int[] GetRecommendations(int trackId);
    }
}
