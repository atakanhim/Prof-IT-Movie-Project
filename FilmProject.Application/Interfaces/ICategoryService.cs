using FilmProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmProject.Application.Interfaces
{
    public interface ICategoryService
    {
        Task<List<Movie>> GetAllAsync();
    }
}
