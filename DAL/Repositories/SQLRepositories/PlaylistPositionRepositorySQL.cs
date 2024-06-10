using BLL;

namespace DAL
{
    public class PlaylistPositionRepositorySQL : IRepository<BLL.PlaylistPosition>
    {
        private AudioFingerprintContext context;

        public PlaylistPositionRepositorySQL(AudioFingerprintContext context)
        {
            this.context = context;
        }

        public void Create(BLL.PlaylistPosition item)
        {
            context.PlaylistPosition.Add(new PlaylistPosition { Id = item.Id, PlaylistId = item.PlaylistId, TrackId = item.TrackId});
        }

        public void Delete(int id)
        {
            PlaylistPosition entity = context.PlaylistPosition.Find(id);
            if (entity != null)
                context.PlaylistPosition.Remove(entity);
        }

        public BLL.PlaylistPosition GetItem(int id)
        {
            PlaylistPosition item = context.PlaylistPosition.Find(id);
            return new BLL.PlaylistPosition(item.Id, item.PlaylistId, item.TrackId);
        }

        public List<BLL.PlaylistPosition> GetList()
        {
            return context.PlaylistPosition.Select(i => new BLL.PlaylistPosition(i.Id, i.PlaylistId, i.TrackId)).ToList();
        }

        public void Update(BLL.PlaylistPosition item)
        {
            PlaylistPosition realItem = context.PlaylistPosition.Find(item.Id);
            context.Entry(realItem).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }
    }
}
