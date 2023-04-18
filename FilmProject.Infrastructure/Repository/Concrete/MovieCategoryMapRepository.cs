using FilmProject.Domain.Entities;
using FilmProject.Infrastructure.Data;
using FilmProject.Infrastructure.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmProject.Infrastructure.Repository.Concrete
{
    public class MovieCategoryMapRepository : EntityRepository<MovieCategoryMap>, IMovieCategoryMapRepository
    {
        public MovieCategoryMapRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
