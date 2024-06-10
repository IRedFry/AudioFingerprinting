namespace BLL
{
    public class RecognitionHistoryDTO
    {
        public int Id { get; set; }
        public DateTime RecognitionDate { get; set; }
        public int UserId { get; set; }
        public int TrackId { get; set; }

        public RecognitionHistoryDTO(int id, DateTime recognitionDate, int userId, int trackId)
        {
            Id = id;
            RecognitionDate = recognitionDate;
            UserId = userId;
            TrackId = trackId;
        }
    }
}
