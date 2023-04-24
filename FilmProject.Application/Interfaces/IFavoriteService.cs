
using FilmProject.Application.Contracts.Movie;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmProject.Application.Interfaces
{
    public interface IFavoriteService
    {
        Task ChangeFavoriteAsync(FavoriteDto favorite);
        Task<IEnumerable<MovieDto>> GetMyFavoritesAsync(string id);
        Task<bool> IsMovieInFavoritesAsync(string userId, int MovieId);
        Task<int> NumberOfFavoritesAsync(int movieId);
    }
}
