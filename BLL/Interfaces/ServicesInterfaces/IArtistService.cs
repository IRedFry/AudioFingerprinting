namespace BLL
{
    public interface IArtistService
    {
        public Artist GetArtist(int id);
        public List<Artist> GetArtists();
        public void AddArtist(Artist artist);
        public List<Artist> GetArtistsMatchedString(string expression);
        public void UpdateArtist(Artist artist);
        public void DeleteArtist(int id);
    }
}
