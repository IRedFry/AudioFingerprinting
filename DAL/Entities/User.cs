using Microsoft.AspNetCore.Identity;
using System.Collections.ObjectModel;

namespace DAL
{
    /// <summary>
    /// Сущность пользователя (IdentityUser)
    /// </summary>
    public class User : IdentityUser<int>
    {
        public User() { }

        public virtual ICollection<Playlist> Playlist { get; set; } = new List<Playlist>();
        public virtual ICollection<RecognitionHistory> RecognitionHistory { get; set; } = new List<RecognitionHistory>();

    }

}
