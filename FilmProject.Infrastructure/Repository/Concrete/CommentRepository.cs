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
    public class CommentRepository : EntityRepository<Comment>, ICommentRepository
    {
        public CommentRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
