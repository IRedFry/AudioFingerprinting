namespace BLL
{
    public class ArtistService : IArtistService
    {
        private IUnityOfWork context;
        public ArtistService(IUnityOfWork context)
        {
            this.context = context;
        }
        public void AddArtist(Artist artist)
        {
            context.Artists.Create(artist);
            context.Save();
        }

        public void DeleteArtist(int id)
        {
            context.Artists.Delete(id);
        }

        public Artist GetArtist(int id)
        {
            return context.Artists.GetItem(id);
        }

        public List<Artist> GetArtists()
        {
            return context.Artists.GetList().ToList();
        }

        public List<Artist> GetArtistsMatchedString(string expression)
        {
            return context.Artists.GetList().Where(i => i.Name.ToLower().Contains(expression.ToLower())).ToList();
        }

        public void UpdateArtist(Artist artist)
        {
            context.Artists.Update(artist);
            context.Save();
        }
    }
}
