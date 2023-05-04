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
    public interface IMovieService
    {
        Task<bool> ChangeIsHighlighted(int id);
        Task<MovieDto?> GetAsync(Expression<Func<Movie, bool>> filter);

        Task<MovieDto?> GetWithCategoryAsync(Expression<Func<Movie, bool>> filter);

        Task<IEnumerable<MovieDto>> GetAllAsync(Expression<Func<Movie, bool>>? filter = null); // tum filmleri doner


        Task<int> GetMovieCountAsync(); // toplam film sayısı
        Task<IEnumerable<MovieDto>> GetListWithCategoryAsync(string category = ""); // category ile map edip dondurdum

        int Add(MovieDto movie); // film ekleme
        void Update(MovieDto movie); // film gncelleme

        Task<IEnumerable<string>> GetAllLanguagesAsync(); // kayıtlı filmlerin dillerini listeler
        Task<IEnumerable<MovieDto>> GetMovieByLanguageAsync(string language);
        
    }
}
