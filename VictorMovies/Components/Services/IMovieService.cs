using VictorMovies.Components.Domain;

namespace VictorMovies.Components.Services
{
    public interface IMovieService
    {
        Task<IEnumerable<Movie>> GetAllMoviesAsync();
        Task<Movie> GetMovieByIdAsync(int movieId);

        Task<IEnumerable<Movie>> GetMoviesBy(string movieName = "", int yearReleased = 1900, string ageRestricted = "", int rating = 0);


        Task CreateMovieAsync(Movie movie);
        Task UpdateMovieAsync(Movie movie);
        Task DeleteMovieAsync(int movieId);
    }
}