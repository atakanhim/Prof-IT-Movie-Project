using FilmProject.Application.Interfaces;
using FilmProject.Domain.Entities;
using FilmProject.Infrastructure.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
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


        public async Task<List<Movie>> GetAllAsync()
        {
            return await _repository.GetListAsync();
        }

        public async Task<List<Movie>> GetLastMoviesAsync(int number)
        {
            return await _repository.GetLastMovieAsync(number);
        }

        public async Task<int> GetMovieCountAsync()
        {
            return await _repository.GetMovieCountAsync();
        }
    }
}
