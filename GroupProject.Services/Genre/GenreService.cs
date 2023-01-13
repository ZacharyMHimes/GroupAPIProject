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
        private readonly ApplicationDbContext _dbContext;

        public GenreService(ApplicationDbContext context)
        {
            _dbContext = context;
        }

        public async Task<bool> CreateGenreAsync(GenreCreate request)
        {
            var genreEntity = new GenreEntity
            {
                GenreName = request.GenreName
            };

            _dbContext.Add(genreEntity);

            var numberOfChanges = await _dbContext.SaveChangesAsync();
            return numberOfChanges == 1;
        }

        public async Task<IEnumerable<GenreListItem>> GetAllGenresAsync()
        {
            var genres = await _dbContext.Genres
                .Select(entity => new GenreListItem
                    {
                        Id = entity.Id,
                        GenreName = entity.GenreName
                    })
                    .ToListAsync();

            return genres;
        }

        public async Task<GenreDetail?> GetGenreIdAsync(int genreId)
        {
                var genreEntity = await _dbContext.Genres.FirstOrDefaultAsync(e =>
                e.Id == genreId);
                return genreEntity is null ? null : new GenreDetail
                    {
                        Id = genreEntity.Id,
                        GenreName = genreEntity.GenreName
                    };
        }

        public async Task<bool> UpdateGenreAsync(GenreUpdate request)
        {
            var genreEntity = await _dbContext.Genres.FindAsync(request.Id);
            
            genreEntity.GenreName = request.GenreName;

            var numberOfChanges = await _dbContext.SaveChangesAsync();
            return numberOfChanges == 1;
        }

            public async Task<bool> DeleteGenreAsync(int Id)
        {
            var genreEntity = await _dbContext.Genres.FindAsync(Id);
            
            _dbContext.Genres.Remove(genreEntity);
            var numberOfChanges = await _dbContext.SaveChangesAsync();
            return (numberOfChanges == 1);
        }
    }
}