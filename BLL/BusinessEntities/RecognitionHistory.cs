namespace BLL
{
    public class RecognitionHistory
    {
        public int Id { get; set; }
        public DateTime RecognitionDate { get; set; }
        public int UserId { get; set; }
        public int TrackId { get; set; }

        public RecognitionHistory(int id, DateTime recognitionDate, int userId, int trackId)
        {
            Id = id;
            RecognitionDate = recognitionDate;
            UserId = userId;
            TrackId = trackId;
        }
    }
}
