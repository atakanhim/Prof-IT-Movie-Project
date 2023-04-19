using FilmProject.Application.Interfaces;
using FilmProject.Domain.Entities;
using FilmProject.Infrastructure.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FilmProject.Application.Services
{
    public class MovieService : IMovieService
    {
        private IMovieRepository _repository;

        public MovieService(IMovieRepository repository)
        {
            _repository = repository;
        }

        public void Add(Movie movie)
        {
            _repository.Add(movie);
        }
        public void Update(Movie movie)
        {
            _repository.Update(movie);
        }

        public async Task<IEnumerable<Movie>> GetAllAsync(Expression<Func<Movie, bool>>? filter = null)
        {
            return await _repository.GetListAsync();
        }
     
        public async Task<IEnumerable<string>> GetAllLanguagesAsync()
        {
            return await _repository.GetAllLanguagesAsync();
        }

        public async Task<Movie?> GetAsync(Expression<Func<Movie, bool>> filter)
        {
            return await _repository.GetAsync(filter);
        }

        public async Task<IEnumerable<Movie>> GetLastMoviesAsync(int number)
        {
            return await _repository.GetLastMovieAsync(number);
        }

        public async Task<IEnumerable<Movie>> GetListWithCategoryAsync()//
        {
            var result = await _repository.GetListWithCategoryAsync();

            //_mapper.Map<Source, Dest>(result);

            return result;
        }

        public async Task<IEnumerable<Movie>> GetMovieByLanguageAsync(string language)
        {
            return await _repository.GetListAsync(m => m.MovieLanguage == language);
        }

        public async Task<int> GetMovieCountAsync()
        {
            return await _repository.GetMovieCountAsync();
        }
  
       
    }
}
