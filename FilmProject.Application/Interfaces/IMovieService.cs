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

        Task<IEnumerable<Movie>> GetAllAsync(Expression<Func<Movie, bool>>? filter = null); // tum filmleri doner


        Task<IEnumerable<Movie>> GetLastMoviesAsync(int number); // son eklenen filmler doner
        Task<int> GetMovieCountAsync(); // toplam film sayısı
        Task<IEnumerable<Movie>> GetListWithCategoryAsync(); // category ile map edip dondurdum

        void Add(Movie movie); // film ekleme
        void Update(Movie movie); // film gncelleme

        Task<IEnumerable<string>> GetAllLanguagesAsync(); // kayıtlı filmlerin dillerini listeler
        Task<IEnumerable<Movie>> GetMovieByLanguageAsync(string language);

    }
}
