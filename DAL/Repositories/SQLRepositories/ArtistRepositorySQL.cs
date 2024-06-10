using BLL;

namespace DAL
{
    public class ArtistRepositorySQL : IRepository<BLL.Artist>
    {
        private AudioFingerprintContext context;

        public ArtistRepositorySQL(AudioFingerprintContext context)
        {
            this.context = context;
        }

        public void Create(BLL.Artist item)
        {
            context.Artist.Add(new Artist { Id = item.Id, Description = item.Description, Name = item.Name, StartDate = item.StartDate, Cover = item.Cover});
        }

        public void Delete(int id)
        {
            Artist entity = context.Artist.Find(id);
            if (entity != null)
                context.Artist.Remove(entity);
            context.SaveChanges();
        }

        public BLL.Artist GetItem(int id)
        {
            Artist item = context.Artist.Find(id);
            return new BLL.Artist(item.Id, item.Name, item.Description, item.StartDate, item.Cover);
        }

        public List<BLL.Artist> GetList()
        {
            return context.Artist.Select(i => new BLL.Artist(i.Id, i.Name, i.Description, i.StartDate, i.Cover)).ToList();
        }

        public void Update(BLL.Artist item)
        {
            Artist realItem = context.Artist.Find(item.Id);
            realItem.Description = item.Description;
            realItem.Cover = item.Cover;
            realItem.StartDate = item.StartDate;
            realItem.Name = item.Name;

            context.Entry(realItem).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }
    }
}
