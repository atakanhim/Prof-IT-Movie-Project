using AutoMapper;
using FilmProject.Application.Contracts.Movie;
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
    public class MovieLikeService : IMovieLikeService
    {
        private IMovieLikeRepository _repository;
        private IMapper _mapper;
        public MovieLikeService(IMovieLikeRepository repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }
        public void Add(MovieLikeDto movieLike)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<MovieLikeDto>> GetAllAsync(Expression<Func<MovieLike, bool>>? filter = null)
        {
            List<MovieLike> movieLikes = await _repository.GetListAsync(filter);
            List<MovieLikeDto> moviesDto = _mapper.Map<List<MovieLike>, List<MovieLikeDto>>(movieLikes);

            return moviesDto;
      
        }

        public async Task<MovieLikeDto?> GetAsync(Expression<Func<MovieLike, bool>> filter)
        {
           MovieLike movieLike = await _repository.GetAsync(filter);
            MovieLikeDto movieDto = _mapper.Map<MovieLike, MovieLikeDto>(movieLike);

            return movieDto;
        }

        public void Update(MovieLikeDto movieLike)
        {
                 MovieLike movie = _mapper.Map<MovieLikeDto, MovieLike>(movieLike);

                _repository.Update(movie);
        }
    }
}
