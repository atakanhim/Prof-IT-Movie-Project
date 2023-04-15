using FilmProject.Application.Interfaces;
using FilmProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmProject.Application.Services
{
    public class CategoryService : ICategoryService
    {
        public Task<List<Movie>> GetAllAsync()
        {
            throw new NotImplementedException();
        }
    }
}
