using BLL;

namespace DAL
{
    public class RecognitionHistoryRepositorySQL : IRepository<BLL.RecognitionHistory>
    {
        private AudioFingerprintContext context;

        public RecognitionHistoryRepositorySQL(AudioFingerprintContext context)
        {
            this.context = context;
        }

        public void Create(BLL.RecognitionHistory item)
        {
            context.RecognitionHistory.Add(new RecognitionHistory { UserId = item.UserId, RecognitionDate = item.RecognitionDate, TrackId = item.TrackId});
        }

        public void Delete(int id)
        {
            RecognitionHistory entity = context.RecognitionHistory.Find(id);
            if (entity != null)
                context.RecognitionHistory.Remove(entity);
        }

        public BLL.RecognitionHistory GetItem(int id)
        {
            RecognitionHistory item = context.RecognitionHistory.Find(id);
            return new BLL.RecognitionHistory(item.Id, item.RecognitionDate, item.UserId, item.TrackId);
        }

        public List<BLL.RecognitionHistory> GetList()
        {
            return context.RecognitionHistory.Select(i => new BLL.RecognitionHistory(i.Id, i.RecognitionDate, i.UserId, i.TrackId)).ToList();
        }

        public void Update(BLL.RecognitionHistory item)
        {
            RecognitionHistory realItem = context.RecognitionHistory.Find(item.Id);
            context.Entry(realItem).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }
    }
}
