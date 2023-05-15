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
    public class MovieCategoryMapRepository : EntityRepository<MovieCategoryMap>, IMovieCategoryMapRepository
    {
        private readonly ApplicationDbContext _context;
        public MovieCategoryMapRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _context = dbContext;
        }

        public async Task DeleteArray(int movieId, int[] categories)
        {
            try
            {


                List<MovieCategoryMap> WillDeleteCategories =  await _context.MovieCategoryMaps
               .Where(x => x.MovieId == movieId && !categories.Contains(x.CategoryId))
               .ToListAsync();
     
                if (WillDeleteCategories.Count > 0)
                {
                    _context.MovieCategoryMaps.RemoveRange(WillDeleteCategories);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }


        }
        public async Task<int[]> GetCategoriesWithMovieIdAsync(int id)
        {
             return  await _context.MovieCategoryMaps
                                .Where(mcm => mcm.MovieId == id)
                                .Select(mcm => mcm.CategoryId)
                                .ToArrayAsync();
        }

        public async Task<bool> isExistCategoryMap(int movieId, int categoryId)
        {
            var model = await _context.MovieCategoryMaps.Where(mcm => mcm.MovieId == movieId && mcm.CategoryId == categoryId).FirstOrDefaultAsync();
            if (model == null)
                    return false;
            return true;
        }
    }
}
