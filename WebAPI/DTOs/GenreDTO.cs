namespace BLL
{
    public class GenreDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public GenreDTO(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public GenreDTO(Genre genre)
        {
            Id = genre.Id;
            Name = genre.Name;
        }
    }
}
