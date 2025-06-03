using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using VictorMovies.Components.Domain;

namespace VictorMovies.Components.Data
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Genre> tGenre { get; set; }
        public DbSet<Movie> tMovie { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }
    }
}
