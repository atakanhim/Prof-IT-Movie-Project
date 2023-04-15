using FilmProject.Domain.Entities;
using FilmProject.Infrastructure.Data;
using FilmProject.Infrastructure.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FilmProject.Infrastructure.Repository.Concrete
{
    public class MovieRepository : EntityRepository<Movie>, IMovieRepository
    {
        private readonly ApplicationDbContext _context;
        public MovieRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _context = dbContext;
        }

        public bool ChangeOneCikar(int id)
        {
            
            throw new NotImplementedException();
        }
    }
}
