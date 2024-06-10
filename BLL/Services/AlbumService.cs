namespace BLL
{
    public class AlbumService : IAlbumService
    {
        private IUnityOfWork context;
        public AlbumService(IUnityOfWork context)
        {
            this.context = context;
        }
        public void AddAlbum(Album album)
        {
            context.Albums.Create(album);
            context.Save();
        }

        public void DeleteAlbum(int id)
        {
            context.Albums.Delete(id);
        }

        public Album GetAlbum(int id)
        {
            return context.Albums.GetItem(id);
        }

        public List<Album> GetAlbums()
        {
            return context.Albums.GetList();
        }

        public List<Album> GetAlbumsMatchedString(string expression)
        {
            return context.Albums.GetList().Where(i => i.Name.ToLower().Contains(expression.ToLower())).ToList();
        }

        public List<Album> GetAlbumsOfArtist(int artistId)
        {
            return context.Albums.GetList().Where(i => i.ArtistId == artistId).ToList();
        }

        public void UpdateAlbum(Album album)
        {
            context.Albums.Update(album);
            context.Save();
        }
    }
}
