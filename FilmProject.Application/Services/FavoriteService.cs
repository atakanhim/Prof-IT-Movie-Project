using AutoMapper;
using FilmProject.Application.Contracts.Movie;
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
    public class FavoriteService : IFavoriteService
    {
        private readonly IFavoriteRepository _repository;
        private readonly IMovieRepository _movieRepository;
        private IMapper _mapper;
        public FavoriteService(IFavoriteRepository repository, IMapper mapper, IMovieRepository movieRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _movieRepository = movieRepository;
        }


        public async Task<IEnumerable<MovieDto>> GetMyFavoritesAsync(string id)
        {
            var movies = await _repository.GetMyFavoritesAsync(id);
            List<MovieDto> moviesDto = _mapper.Map<List<Movie>, List<MovieDto>>(movies);
            return moviesDto;
        }


        public async Task ChangeFavoriteAsync(FavoriteDto favorite)
        {
            Favorite NewFavorite = _mapper.Map<FavoriteDto, Favorite>(favorite);
            if (!_movieRepository.isExistById(favorite.MovieId)) 
            {
                throw new InvalidOperationException("There is no movie with this id.");
            }
           
            await _repository.ChangeFavoriteAsync(NewFavorite);

        }

        public async Task<bool> IsMovieInFavoritesAsync(string userId, int MovieId)
        { 
           return await _repository.IsMovieInFavoritesAsync(userId, MovieId);
        }

        public async Task<int> NumberOfFavoritesAsync(int movieId)
        {  
            return await _repository.NumberOfFavoritesAsync(movieId);
        }
    }
}
