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
    public interface IMovieLikeService
    {
        Task<MovieLikeDto?> GetAsync(Expression<Func<MovieLike, bool>> filter);
        Task<IEnumerable<MovieLikeDto>> GetAllAsync(Expression<Func<MovieLike, bool>>? filter = null); // tum filmleri doner
        void Add(MovieLikeDto movieLike);
        void Update(MovieLikeDto movieLike);
    }
}
