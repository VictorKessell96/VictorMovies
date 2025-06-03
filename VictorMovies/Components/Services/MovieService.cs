using Microsoft.EntityFrameworkCore;
using VictorMovies.Components.Data;
using VictorMovies.Components.Domain;

namespace VictorMovies.Components.Services
{
    public class MovieService : IMovieService
    {
        private readonly DatabaseContext DB;

        public MovieService(DatabaseContext databaseContext)
        {
            DB = databaseContext;
        }


        //CRUD FUNCTIONALITY
        public async Task<IEnumerable<Movie>> GetAllMoviesAsync()
        {
            return await DB.tMovie.Select(movie => movie)
                .Include(movie => movie.Genre)
                .Where(movie => movie.IsDeleted == false)
                .ToListAsync();
        }
        public async Task<IEnumerable<Movie>> GetMoviesBy(string movieName = "", int yearReleased = 0, string ageRestricted = "", int rating = 0)
        {
            var movie = await DB.tMovie
                                 .FromSqlRaw("EXEC [prc_tMovies_Get]" +
                                             "@MovieName = {0}, @YearReleased = {1}, " +
                                             "@AgeRestriction = {2}, @Rating = {3}", movieName, yearReleased, ageRestricted, rating)
                                 .ToListAsync();

            if (movie == null)
            {
                throw new KeyNotFoundException("Movie(s) not found!");
            }

            return movie;
        }
        public async Task<Movie> GetMovieByIdAsync(int movieId)
        {
            var movie = await DB.tMovie.FindAsync(movieId);

            if (movie == null)
            {
                throw new KeyNotFoundException("Movie not found!");
            }

            return movie;
        }

        public async Task CreateMovieAsync(Movie movie)
        {
            Movie newMovie = new Movie
            {
                MovieName = movie.MovieName,
                YearReleased = movie.YearReleased,
                AgeRestriction = movie.AgeRestriction,
                Rating = movie.Rating,
                GenreId = movie.GenreId,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                CreatedBy = movie.CreatedBy,
                LastModifiedBy = movie.CreatedBy,
                IsDeleted = movie.IsDeleted
            };

            DB.tMovie.Add(newMovie);
            await DB.SaveChangesAsync();
        }

        public async Task UpdateMovieAsync(Movie updatedMovie)
        {
            var movie = await DB.tMovie.FindAsync(updatedMovie.MovieId);
            if (movie == null)
            {
                throw new KeyNotFoundException("Movie not found!");
            }

            movie.MovieName = updatedMovie.MovieName;
            movie.YearReleased = updatedMovie.YearReleased;
            movie.AgeRestriction = updatedMovie.AgeRestriction;
            movie.Rating = updatedMovie.Rating;
            movie.GenreId = updatedMovie.GenreId;
            movie.ModifiedDate = DateTime.Now;
            movie.LastModifiedBy = updatedMovie.LastModifiedBy;

            await DB.SaveChangesAsync();
        }

        public async Task DeleteMovieAsync(int movieId)
        {
            var movie = await DB.tMovie.FindAsync(movieId);
            if (movie == null)
            {
                throw new KeyNotFoundException("Movie not found!");
            }

            //This physically removes the record
            //DB.tDepartment.Remove(dep);

            //Better practice to set IsDeleted = 1
            movie.IsDeleted = true;
            movie.ModifiedDate = DateTime.Now;
            movie.LastModifiedBy = movie.LastModifiedBy;

            await DB.SaveChangesAsync();
        }
    }
}
