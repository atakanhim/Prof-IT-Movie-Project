using FilmProject.Application.Contracts.Movie;
using FilmProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FilmProject.Application.Interfaces
{
    public interface IMovieCategoryMapService
    {
        Task<int[]> GetCategoriesWithMovieIdAsync(int id);
        Task UpdateMovieToCategoryAsync(int MovieId, ICollection<int> IdOfCategories);
        Task<bool?> AnyMoviesInThisCategory(Expression<Func<MovieCategoryMap, bool>> filter);

        public Task AddMovieToCategory(int MovieId, ICollection<int> IdOfCategories);
    }
}
