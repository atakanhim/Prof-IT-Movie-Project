using FilmProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmProject.Infrastructure.Repository.Abstract
{
    public interface IMovieCategoryMapRepository : IEntityRepository<MovieCategoryMap>
    {
        Task<int[]> GetCategoriesWithMovieIdAsync(int id);
        Task DeleteArray(int movieId, int[] categories);
        Task<bool> isExistCategoryMap(int movieId, int categoryId);
    }
}
