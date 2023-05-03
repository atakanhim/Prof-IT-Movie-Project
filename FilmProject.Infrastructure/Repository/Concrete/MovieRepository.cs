using FilmProject.Domain.Entities;
using FilmProject.Infrastructure.Data;
using FilmProject.Infrastructure.Repository.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FilmProject.Infrastructure.Repository.Concrete
{
    public class MovieRepository : EntityRepository<Movie>, IMovieRepository
    {
        private readonly ApplicationDbContext _context;
        public MovieRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _context = dbContext;
        }

        public async Task<int> GetMovieCountAsync()
        {
            return await _context.Movies.CountAsync();
        }

        public bool ChangeOneCikar(int id)
        {

            throw new NotImplementedException();
        }



        public async Task<List<Movie>> GetListWithCategoryAsync(string category)
        {
            if (category != "" && category != null)
            {
                var model = await _context.MovieCategoryMaps
                    .Include(a => a.Category)
                    .Include(b => b.Movie)
                    .Include(b => b.Movie.MovieLikes) 
                    .Include(b => b.Movie.Comments).ThenInclude(yu => yu.CommentLikes)
                    .Include(b => b.Movie.MovieCategories).ThenInclude(yu => yu.Category)
                    .Where(c => c.Category.CategoryName == category).Select(m => m.Movie).ToListAsync();

                return model;
            }

            return await _context.Movies.Include(u => u.MovieLikes).Include(y => y.Comments).ThenInclude(yu => yu.CommentLikes).Include(x => x.MovieCategories).ThenInclude(xc => xc.Category).ToListAsync();
        }

        public async Task<List<string>> GetAllLanguagesAsync()
        {
            return await _context.Movies.Select(m => m.MovieLanguage).Distinct().ToListAsync();
        }

        public bool isExist(string MovieName)
        {
            return _context.Movies.Any(c => c.MovieName == MovieName);
        }

        public bool isExistById(int MovieId)
        {
            return _context.Movies.Any(c => c.Id == MovieId);
        }

        public async Task<Movie> GetWithCategoryAsync(Expression<Func<Movie, bool>> filter)
        {
#pragma warning disable CS8603 // Possible null reference return.
            return await _context.Movies.Include(u => u.MovieLikes)
                .Include(y => y.WhoFavorited)
                .Include(a => a.Comments).ThenInclude(ax => ax.AppUser)
                .Include(v => v.Comments).ThenInclude(vx => vx.CommentLikes)
                .Include(x => x.MovieCategories).ThenInclude(xc => xc.Category).
                Where(filter).FirstOrDefaultAsync();
#pragma warning restore CS8603 // Possible null reference return.
        }
    }
}
