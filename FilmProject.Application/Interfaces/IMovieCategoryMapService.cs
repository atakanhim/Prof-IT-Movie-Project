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
        Task<bool?> AnyMoviesInThisCategory(Expression<Func<MovieCategoryMap, bool>> filter);

        public Task AddMovieToCategory(int MovieId, ICollection<int> IdOfCategories);
    }
}
