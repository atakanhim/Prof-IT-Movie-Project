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
            if(category != "" && category != null)
            {
                 List<Movie> movies = new List<Movie>();
                var model = await _context.Categories.Include(x => x.MovieCategories).ThenInclude(xa=>xa.Movie).Where(a=>a.CategoryName == category).ToListAsync();   
                foreach(var movie in model[0].MovieCategories)
                {
                    Movie moviedata = await _context.Movies.Include(y => y.Comments).Include(x => x.MovieCategories).ThenInclude(xc => xc.Category).Where(i=>i.Id == movie.MovieId).FirstOrDefaultAsync();

                    movies.Add(moviedata);
                }

                return movies;
            }

            return await _context.Movies.Include(y => y.Comments).Include(x => x.MovieCategories).ThenInclude(xc => xc.Category).ToListAsync();
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
    }
}
