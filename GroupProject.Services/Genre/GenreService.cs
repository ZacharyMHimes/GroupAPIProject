using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using GroupProject.Data;
using GroupProject.Models.Genre;
using Microsoft.EntityFrameworkCore;

namespace GroupProject.Services.Genre
{
    public class GenreService : IGenreService
    {
        private readonly ApplicationDbContext _context;
        private readonly int _genreId;
        private ApplicationDbContext DbContext;

        public GenreService(ApplicationDbContext context)
        {
            DbContext = context;
        }

        public async Task<bool> CreateGenreAsync(GenreRegister request)
        {
            var genreEntity = new GenreEntity
            {
                Id = _genreId,
                GenreName = request.GenreName
            };

            DbContext.Add(genreEntity);

            var numberOfChanges = await DbContext.SaveChangesAsync();
            return numberOfChanges == 1;
        }

        public async Task<bool> UpdateGenreAsync(GenreUpdate request)
        {
            var genreEntity = await _context.Genres.FindAsync(request.Id);
            if(genreEntity?.Id != _genreId)
                return false;
            genreEntity.GenreName = request.GenreName;

            var numberOfChanges = await _context.SaveChangesAsync();
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
    }
}