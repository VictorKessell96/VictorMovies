using Microsoft.EntityFrameworkCore;
using VictorMovies.Components.Data;
using VictorMovies.Components.Domain;

namespace VictorMovies.Components.Services
{
    public class GenreService : IGenreService
    {
        private readonly DatabaseContext DB;

        public GenreService(DatabaseContext databaseContext)
        {
            DB = databaseContext;
        }


        //CRUD FUNCTIONALITY
        public async Task<IEnumerable<Genre>> GetAllGenresAsync()
        {
            return await DB.tGenre.Select(genre => genre)
                            .Where(genre => genre.IsDeleted == false)
                            .ToListAsync();
        }


        public async Task<Genre> GetGenreByIdAsync(int genreId)
        {
            var genre = await DB.tGenre.FindAsync(genreId);

            if (genre == null)
            {
                throw new KeyNotFoundException("Genre not found!");
            }

            return genre;
        }

        public async Task<IEnumerable<Genre>> GetGenreByNameAsync(string genreName)
        {
            var genre = await DB.tGenre
                                .FromSqlRaw("EXEC [prc_tGenre_GetGenreByName] @genreName = {0}", genreName)
                                .ToListAsync();

            if (genre == null)
            {
                throw new KeyNotFoundException("Genre not found!");
            }

            return genre;
        }


        public async Task CreateGenreAsync(Genre Genre)
        {
            Genre newGenre = new Genre
            {
                GenreName = Genre.GenreName,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                CreatedBy = Genre.CreatedBy,
                LastModifiedBy = Genre.CreatedBy,
                IsDeleted = Genre.IsDeleted
            };

            DB.tGenre.Add(newGenre);
            await DB.SaveChangesAsync();
        }

        public async Task UpdateGenreAsync(Genre updatedGenre)
        {
            var genre = await DB.tGenre.FindAsync(updatedGenre.GenreId);
            if (genre == null)
            {
                throw new KeyNotFoundException("Genre not found!");
            }

            genre.GenreName = updatedGenre.GenreName;
            genre.ModifiedDate = DateTime.Now;
            genre.LastModifiedBy = updatedGenre.LastModifiedBy;

            await DB.SaveChangesAsync();
        }

        public async Task DeleteGenreAsync(int genreId)
        {
            var genre = await DB.tGenre.FindAsync(genreId);
            if (genre == null)
            {
                throw new KeyNotFoundException("Genre not found!");
            }

            //This physically removes the record
            //DB.tDepartment.Remove(dep);

            //Better practice to set IsDeleted = 1
            genre.IsDeleted = true;
            genre.ModifiedDate = DateTime.Now;
            genre.LastModifiedBy = genre.LastModifiedBy;

            await DB.SaveChangesAsync();
        }
    }
}