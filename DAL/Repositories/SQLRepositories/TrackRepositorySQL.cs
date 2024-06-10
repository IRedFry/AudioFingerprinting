using BLL;

namespace DAL
{
    public class TrackRepositorySQL : IRepository<BLL.Track>
    {
        private AudioFingerprintContext context;

        public TrackRepositorySQL(AudioFingerprintContext context)
        {
            this.context = context;
        }

        public void Create(BLL.Track item)
        {
            context.Track.Add(new Track { Id = item.Id, AlbumId = item.AlbumId, Title = item.Title, PublishDate = item.PublishDate, Lyrics = item.Lyrics, GenreId = item.GenreId, Fingerprint = item.Fingerprint, Description = item.Description, ArtistId = item.ArtistId, Cover = item.Cover, LSH = item.LSH});
        }

        public void Delete(int id)
        {
            Track entity = context.Track.Find(id);
            if (entity != null)
                context.Track.Remove(entity);
            context.SaveChanges();
        }

        public BLL.Track GetItem(int id)
        {
            Track item = context.Track.Find(id);
            if (item == null)
                return null;

            return new BLL.Track(item.Id, item.Title, item.PublishDate, item.Description, item.Fingerprint, item.Lyrics, item.Cover, item.GenreId, item.ArtistId, item.AlbumId, item.LSH);
        }

        public List<BLL.Track> GetList()
        {
            return context.Track.Select(i => new BLL.Track(i.Id, i.Title, i.PublishDate, i.Description, i.Fingerprint, i.Lyrics, i.Cover, i.GenreId, i.ArtistId, i.AlbumId, i.LSH)).ToList();
        }

        public void Update(BLL.Track item)
        {
            Track realItem = context.Track.Find(item.Id);
            realItem.Title = item.Title;
            realItem.Cover = item.Cover;
            realItem.Description = item.Description;
            realItem.Lyrics = item.Lyrics;
            realItem.ArtistId = item.ArtistId;
            realItem.AlbumId = item.AlbumId == -1 ? null : item.AlbumId;
            realItem.Fingerprint = item.Fingerprint;
            realItem.PublishDate = item.PublishDate;
            realItem.GenreId = item.GenreId;
            realItem.LSH = item.LSH;

            context.Entry(realItem).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }
    }
}
