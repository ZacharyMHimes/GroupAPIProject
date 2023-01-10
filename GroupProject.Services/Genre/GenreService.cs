using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using GroupProject.Data;
using GroupProject.Data.Entities;
using GroupProject.Models.Genre;
using Microsoft.EntityFrameworkCore;

namespace GroupProject.Services.Genre
{
    public class GenreService : IGenreService
    {
        private readonly ApplicationDbContext _context;
        private ApplicationDbContext DbContext;

        public GenreService(ApplicationDbContext context)
        {
            DbContext = context;
        }

        public async Task<bool> CreateGenreAsync(GenreRegister request)
        {
            var genreEntity = new GenreEntity
            {
            GenreName = request.GenreName
            };

            DbContext.Add(genreEntity);

            var numberOfChanges = await DbContext.SaveChangesAsync();
            return numberOfChanges == 1;
        }

          public async Task<bool> DeleteGenreAsync(int Id)
        {
            var genreEntity = await _context.Genres.FindAsync(Id);

            if (genreEntity is null)
                return false;
            
            _context.Genres.Remove(genreEntity);
            var numberOfChanges = await _context.SaveChangesAsync();

            return (numberOfChanges == 1);
        }

        public async Task<GenreDetail> GetGenreByIdAsync(int Id)
        {
            var genreEntity = await DbContext.Genres.FindAsync(Id);

            return (genreEntity is null) ? null : new GenreDetail
            {
                GenreName = genreEntity.GenreName
            };
        }
    }
}