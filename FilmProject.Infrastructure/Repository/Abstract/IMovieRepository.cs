using FilmProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FilmProject.Infrastructure.Repository.Abstract
{
    public interface IMovieRepository : IEntityRepository<Movie>
    {
        bool ChangeOneCikar(int id);
        Task<int> GetMovieCountAsync();
        Task<List<Movie>> GetListWithCategoryAsync(string category);
        Task<List<string>> GetAllLanguagesAsync();

        bool isExist(string MovieName);

    }
}
