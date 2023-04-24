using FilmProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmProject.Infrastructure.Repository.Abstract
{
    public interface IFavoriteRepository
    {
        Task ChangeFavoriteAsync(Favorite favorite);
        Task<List<Movie>> GetMyFavoritesAsync(string id);
        Task<bool> IsMovieInFavoritesAsync(string userId, int MovieId);
        Task<int> NumberOfFavoritesAsync(int movieId);
        
    }
}
