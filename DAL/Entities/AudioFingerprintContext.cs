using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    /// <summary>
    /// Контекст базы данных приложения
    /// </summary>
    public class AudioFingerprintContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        protected readonly IConfiguration configuration;

        public AudioFingerprintContext(IConfiguration configuration)
        {
            this.configuration = configuration; 
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Album>(entity => {
                entity.HasOne(a => a.Artist)
                .WithMany(p => p.Album)
                .HasForeignKey(a => a.ArtistId);
            });

            builder.Entity<Track>(entity => {
                entity.HasOne(t => t.Artist)
                .WithMany(p => p.Track)
                .HasForeignKey(t => t.ArtistId)
                .OnDelete(DeleteBehavior.NoAction);              
            });

            builder.Entity<Track>(entity => {
                entity.HasOne(t => t.Album)
                .WithMany(p => p.Track)
                .HasForeignKey(t => t.AlbumId);
            });


            builder.Entity<Track>(entity => {
                entity.HasOne(t => t.Genre)
                .WithMany(g => g.Track)
                .HasForeignKey(t => t.GenreId);
            });


            builder.Entity<RecognitionHistory>(entity => {
                entity.HasOne(r => r.Track)
                .WithMany(t => t.RecognitionHistory)
                .HasForeignKey(r => r.TrackId);
            });

            builder.Entity<RecognitionHistory>(entity =>
            {
                entity.HasOne(r => r.User)
                .WithMany(u => u.RecognitionHistory)
                .HasForeignKey(r => r.UserId);
            });

            builder.Entity<PlaylistPosition>(entity => {
                entity.HasOne(p => p.Track)
                .WithMany(t => t.PlaylistPosition)
                .HasForeignKey(p => p.TrackId);
            });

            builder.Entity<PlaylistPosition>(entity => {
                entity.HasOne(p => p.Playlist)
                .WithMany(pl => pl.PlaylistPosition)
                .HasForeignKey(p => p.PlaylistId);
            });

            builder.Entity<Playlist>(entity => {
                entity.HasOne(p => p.User)
                .WithMany(u => u.Playlist)
                .HasForeignKey(p => p.UserId);
            });
        }

        public virtual DbSet<Album> Album { get; set; }
        public virtual DbSet<Artist> Artist { get; set; }
        public virtual DbSet<Genre> Genre { get; set; }
        public virtual DbSet<Playlist> Playlist { get; set; }
        public virtual DbSet<PlaylistPosition> PlaylistPosition { get; set; }
        public virtual DbSet<RecognitionHistory> RecognitionHistory { get; set; }
        public virtual DbSet<Track> Track { get; set; }
        public virtual DbSet<User> User { get; set; }
    }
}
