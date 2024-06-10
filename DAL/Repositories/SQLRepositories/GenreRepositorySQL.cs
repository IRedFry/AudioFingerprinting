using BLL;

namespace DAL
{ 
    public class GenreRepositorySQL : IRepository<BLL.Genre>
    {
        private AudioFingerprintContext context;

        public GenreRepositorySQL(AudioFingerprintContext context)
        {
            this.context = context;
        }

        public void Create(BLL.Genre item)
        {
            context.Genre.Add(new Genre { Id = item.Id, Name = item.Name});
        }

        public void Delete(int id)
        {
            Genre entity = context.Genre.Find(id);
            if (entity != null)
                context.Genre.Remove(entity);
        }

        public BLL.Genre GetItem(int id)
        {
            Genre item = context.Genre.Find(id);
            return new BLL.Genre(item.Id, item.Name);
        }

        public List<BLL.Genre> GetList()
        {
            return context.Genre.Select(i => new BLL.Genre(i.Id, i.Name)).ToList();
        }

        public void Update(BLL.Genre item)
        {
            Genre realItem = context.Genre.Find(item.Id);
            context.Entry(realItem).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }
    }
}
