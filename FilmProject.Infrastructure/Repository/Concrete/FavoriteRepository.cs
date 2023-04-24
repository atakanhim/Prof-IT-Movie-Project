using FilmProject.Domain.Entities;
using FilmProject.Infrastructure.Data;
using FilmProject.Infrastructure.Repository.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmProject.Infrastructure.Repository.Concrete
{
    public class FavoriteRepository : EntityRepository<Favorite>, IFavoriteRepository
    {
        private readonly ApplicationDbContext _context;
        public FavoriteRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _context = dbContext;
        }

        public async Task ChangeFavoriteAsync(Favorite favorite)
        {
            if (await this.IsMovieInFavoritesAsync(favorite.UserId, favorite.MovieId))
            {
                _context.Remove(favorite);
            }
            else 
            {
                _context.Add(favorite);
            }
            
            await _context.SaveChangesAsync();
        }

        public async Task<List<Movie>> GetMyFavoritesAsync(string id)
        {
            return await _context.Favorite
                    .Include(f => f.Movie)
                    .ThenInclude(m => m.MovieCategories)
                    .ThenInclude(mc => mc.Category)
                    .Where(f => f.UserId == id)
                    .Select(f => f.Movie)
                    .ToListAsync();

        }

        public async Task<bool> IsMovieInFavoritesAsync(string userId, int movieId)
        {
            return await _context.Favorite.AnyAsync(f => f.UserId == userId && f.MovieId == movieId);
        }

        public async Task<int> NumberOfFavoritesAsync(int movieId)
        {
            return await _context.Favorite.CountAsync(f => f.MovieId == movieId);
        }

        

        
    }
}
