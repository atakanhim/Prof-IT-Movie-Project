using FilmProject.Domain.Entities;
using FilmProject.Infrastructure.Data;
using FilmProject.Infrastructure.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmProject.Infrastructure.Repository.Concrete
{
    public class FavoriteRepository : IFavoriteRepository
    {
        private readonly ApplicationDbContext _context;
        public FavoriteRepository(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task AddFavoriteMovie(Favorite favorite)
        {
            _context.Add(favorite);
            await _context.SaveChangesAsync();
        }
    }
}
