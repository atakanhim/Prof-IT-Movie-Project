using FilmProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FilmProject.Application.Interfaces
{
    public interface IMovieService
    {
        Task<Movie?> GetAsync(Expression<Func<Movie, bool>> filter);

        Task<List<Movie>> GetAllAsync(); // tum filmleri doner
        Task<List<Movie>> GetLastMoviesAsync(int number); // son eklenen filmler doner
        Task<int> GetMovieCountAsync(); // toplam film sayısı
        Task<List<Movie>> GetListWithCategoryAsync(); // category ile map edip dondurdum
        void Add(Movie movie);
        
        Task<List<string>> GetAllLanguagesAsync(); // kayıtlı filmlerin dillerini listeler
        Task<List<Movie>> GetMovieByLanguageAsync(string language);

    }
}
