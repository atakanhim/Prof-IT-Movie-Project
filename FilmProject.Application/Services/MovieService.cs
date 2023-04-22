using AutoMapper;
using AutoMapper.Execution;
using FilmProject.Application.Contracts.Movie;
using FilmProject.Application.Interfaces;
using FilmProject.Domain.Entities;
using FilmProject.Infrastructure.Repository.Abstract;
using FilmProject.Infrastructure.Repository.Concrete;
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
        private IMapper _mapper;
        public MovieService(IMovieRepository repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public void Add(MovieDto movieDto)
        {
            // Veritabanında bu isimle bir film var mı kontrolü yapıldı.
            Movie movie = _mapper.Map<MovieDto, Movie>(movieDto);

            if (_repository.isExist(movie.MovieName))
            {
                throw new InvalidOperationException("This movie is already exists.");
            }
            else
            {          
                _repository.Add(movie);              
            }
        }
        public void Update(MovieDto movieDto)
        {
            Movie movie = _mapper.Map<MovieDto, Movie>(movieDto);
            _repository.Update(movie);
        }

        public async Task<IEnumerable<MovieDto>> GetAllAsync(Expression<Func<Movie, bool>>? filter = null)
        {
            var movies = await _repository.GetListAsync();
            List<MovieDto> moviesDto = _mapper.Map<List<Movie>, List<MovieDto>>(movies);

            return moviesDto;

        }

        public async Task<IEnumerable<string>> GetAllLanguagesAsync()
        {
            return await _repository.GetAllLanguagesAsync();
        }

        public async Task<MovieDto?> GetAsync(Expression<Func<Movie, bool>> filter)
        {
            var movie = await _repository.GetAsync(filter);
            MovieDto movieDto = _mapper.Map<Movie,MovieDto>(movie);

            return movieDto;
       
        }

        public async Task<IEnumerable<MovieDto>> GetLastMoviesAsync(int number)
        {
            var movies = await _repository.GetLastMovieAsync(number);
            List<MovieDto> moviesDto = _mapper.Map<List<Movie>, List<MovieDto>>(movies);

            return moviesDto;

        }

        public async Task<IEnumerable<MovieDto>> GetListWithCategoryAsync()//
        {
            var movies = await _repository.GetListWithCategoryAsync();

            List<MovieDto> moviesDto = _mapper.Map<List<Movie>, List<MovieDto>>(movies);

            return moviesDto;
        }

        public async Task<IEnumerable<MovieDto>> GetMovieByLanguageAsync(string language)
        {
            var movies = await _repository.GetListAsync(m => m.MovieLanguage == language);

            List<MovieDto> moviesDto = _mapper.Map<List<Movie>, List<MovieDto>>(movies);

            return moviesDto;

           
        }

        public async Task<int> GetMovieCountAsync()
        {
            return await _repository.GetMovieCountAsync();
        }


    }
}
