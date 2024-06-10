namespace DAL
{
    /// <summary>
    /// Сущность позиции в плейлисте (песня в плейлисте)
    /// </summary>
    public class PlaylistPosition
    {
        public PlaylistPosition() { }

        public int Id { get; set; }
        public int PlaylistId { get; set; }
        public int TrackId { get; set; }

        public virtual Playlist Playlist { get; set; }
        public virtual Track Track { get; set; }
    }
}
