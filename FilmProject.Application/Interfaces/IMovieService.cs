using FilmProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmProject.Application.Interfaces
{
    public interface IMovieService
    {
        Task<List<Movie>> GetAllAsync();
        Task<List<Movie>> GetLastMoviesAsync(int number);
        Task<int> GetMovieCountAsync();
    }
}
