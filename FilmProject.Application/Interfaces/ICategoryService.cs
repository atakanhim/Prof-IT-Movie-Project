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
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDto>> GetAllAsync(Expression<Func<Category, bool>>? filter = null); // tum filmleri doner

        Task<bool> AddCategory(CategoryDto Category);
        Task UpdateCategoryAsync(CategoryDto category);

        Task DeleteCategoryAsync(int id);

        Task<int> CountAsync();
    }
}
