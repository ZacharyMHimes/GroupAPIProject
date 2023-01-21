using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GroupProject.Models.Genre;

namespace GroupProject.Services.Genre
{
    public interface IGenreService
    {
        Task<bool> CreateGenreAsync(GenreCreate request);
        Task<IEnumerable<GenreListItem>> GetAllGenresAsync();
        Task<GenreDetail?> GetGenreIdAsync(int genreId); 
        Task<bool> DeleteGenreAsync(int Id);
        Task<bool> UpdateGenreAsync(GenreUpdate request);
    }
}