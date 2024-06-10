namespace DAL
{
    /// <summary>
    /// Сущность истории распознавания
    /// </summary>
    public class RecognitionHistory
    {
        public RecognitionHistory() { }

        public int Id { get; set; }
        public DateTime RecognitionDate { get; set; }
        public int UserId { get; set; }
        public int TrackId { get; set; }

        public virtual User User { get; set; }
        public virtual Track Track { get; set; }

    }
}
