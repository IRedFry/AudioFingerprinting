using BLL;

namespace DAL
{
    public class AlbumRepositorySQL : IRepository<BLL.Album>
    {
        private AudioFingerprintContext context;

        public AlbumRepositorySQL(AudioFingerprintContext context)
        {
            this.context = context;
        }

        public void Create(BLL.Album item)
        {
            context.Album.Add(new Album { Id = item.Id, ArtistId = item.ArtistId, Name = item.Name, PublishDate = item.PublishDate, Cover = item.Cover});
        }

        public void Delete(int id)
        {
            Album entity = context.Album.Find(id);
            if (entity != null)
                context.Album.Remove(entity);
            context.SaveChanges();
        }

        public BLL.Album GetItem(int id)
        {
            Album item = context.Album.Find(id);
            if (item == null)
                return null;

            return new BLL.Album(item.Id, item.Name, item.PublishDate, item.ArtistId, item.Cover);
        }

        public List<BLL.Album> GetList()
        {
            return context.Album.Select(i => new BLL.Album(i.Id, i.Name, i.PublishDate, i.ArtistId, i.Cover)).ToList();
        }

        public void Update(BLL.Album item)
        {
            Album realItem = context.Album.Find(item.Id);
            realItem.ArtistId = item.ArtistId;
            realItem.Name = item.Name;
            realItem.Cover = item.Cover;
            context.Entry(realItem).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }
    }
}
