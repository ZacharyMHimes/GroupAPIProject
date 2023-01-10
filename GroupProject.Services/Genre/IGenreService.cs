using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GroupProject.Models.Genre;

namespace GroupProject.Services.Genre
{
    public interface IGenreService
    {
        Task<bool> CreateGenreAsync(GenreRegister request);
        Task<GenreDetail> GetGenreByIdAsync(int Id);
        Task<bool> DeleteGenreAsync(int Id);
    }
}