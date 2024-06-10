using BLL;

namespace DAL
{
    public class PlaylistRepositorySQL : IRepository<BLL.Playlist>
    {
        private AudioFingerprintContext context;

        public PlaylistRepositorySQL(AudioFingerprintContext context)
        {
            this.context = context;
        }

        public void Create(BLL.Playlist item)
        {
            context.Playlist.Add(new Playlist { Id = item.Id, CreationDate = item.CreationDate, Name = item.Name, UserId = item.UserId, Cover=item.Cover});
        }

        public void Delete(int id)
        {
            Playlist entity = context.Playlist.Find(id);
            if (entity != null)
                context.Playlist.Remove(entity);
        }

        public BLL.Playlist GetItem(int id)
        {
            Playlist item = context.Playlist.Find(id);

            return new BLL.Playlist(item.Id, item.Name, item.CreationDate, item.UserId, item.Cover);
        }

        public List<BLL.Playlist> GetList()
        {
            return context.Playlist.Select(i => new BLL.Playlist(i.Id, i.Name, i.CreationDate, i.UserId, i.Cover)).ToList();
        }

        public void Update(BLL.Playlist item)
        {
            Playlist realItem = context.Playlist.Find(item.Id);
            context.Entry(realItem).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }
    }
}
