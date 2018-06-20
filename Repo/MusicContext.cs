using Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repo
{
    public class MusicContext : DbContext
    {
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Track> Tracks { get; set; }
        public DbSet<Album> Albums { get; set; }
        public DbSet<ArtistSimilarArtist> SimilarArtists { get; set; }

        public MusicContext(DbContextOptions<MusicContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ArtistSimilarArtist>()
                .HasOne(p => p.Artist)
                .WithMany(p => p.SimilarArtists);
        }
    }
}
