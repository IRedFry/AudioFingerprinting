namespace BLL
{
    public interface IAlbumService
    {
        public Album GetAlbum(int id);
        public List<Album> GetAlbums();
        public List<Album> GetAlbumsOfArtist(int artistId);
        public void AddAlbum(Album album);
        public List<Album> GetAlbumsMatchedString(string expression);
        public void UpdateAlbum(Album album);
        public void DeleteAlbum(int id);
    }
}
