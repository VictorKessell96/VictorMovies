using System.Threading.Tasks;
using VictorMovies.Components.Domain;

namespace VictorMovies.Components.Services
{
    public interface IGenreService
    {
        Task<IEnumerable<Genre>> GetAllGenresAsync();
        Task<Genre> GetGenreByIdAsync(int genreId);
        Task<IEnumerable<Genre>> GetGenreByNameAsync(string genreName);

        Task CreateGenreAsync(Genre genre);
        Task UpdateGenreAsync(Genre genre);
        Task DeleteGenreAsync(int genreId);
    }


}

