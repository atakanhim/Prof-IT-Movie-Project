using FilmProject.Domain.Entities;
using FilmProject.Infrastructure.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FilmProject.Infrastructure.Repository.Concrete
{
    public class MovieRepository : IMovieRepository
    {
        public void Add(Movie entity)
        {
            throw new NotImplementedException();
        }

        public bool ChangeOneCikar(int id)
        {
            throw new NotImplementedException(); // one cikar senecegi degisitrielecek true false
        }

        public void Delete(Movie entity)
        {
            throw new NotImplementedException();
        }

        public Task<Movie?> GetAsync(Expression<Func<Movie, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public Task<List<Movie>> GetListAsync(Expression<Func<Movie, bool>>? filter = null)
        {
            throw new NotImplementedException();
        }

        public void Update(Movie entity)
        {
            throw new NotImplementedException();
        }
    }
}
