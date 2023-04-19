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

        public async Task<List<Movie>> GetAllAsync()
        {
            return await _repository.GetListAsync();
        }

        public async Task<List<string>> GetAllLanguagesAsync()
        {
            return await _repository.GetAllLanguagesAsync();
        }

        public async Task<Movie?> GetAsync(Expression<Func<Movie, bool>> filter)
        {
            return await _repository.GetAsync(filter);
        }

        public async Task<List<Movie>> GetLastMoviesAsync(int number)
        {
            return await _repository.GetLastMovieAsync(number);
        }

        public async Task<List<Movie>> GetListWithCategoryAsync()//
        {
            return await _repository.GetListWithCategoryAsync();
        }

        public async Task<List<Movie>> GetMovieByLanguageAsync(string language)
        {
            return await _repository.GetListAsync(m => m.MovieLanguage == language);
        }

        public async Task<int> GetMovieCountAsync()
        {
            return await _repository.GetMovieCountAsync();
        }

     
    }
}
