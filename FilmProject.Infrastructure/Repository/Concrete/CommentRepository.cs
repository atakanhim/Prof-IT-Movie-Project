using FilmProject.Domain.Entities;
using FilmProject.Infrastructure.Data;
using FilmProject.Infrastructure.Repository.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FilmProject.Infrastructure.Repository.Concrete
{
    public class CommentRepository : EntityRepository<Comment>, ICommentRepository
    {
        private readonly ApplicationDbContext _context;
        public CommentRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _context = dbContext;
        }

        public async Task<int> GetCountOfTotalComment()
        {
            return await _context.Comments.CountAsync();
        }
        public async Task<List<Comment>>  GetListWithAppUser(Expression<Func<Comment, bool>>? filter = null)
        {
            return await _context.Comments.Include(y => y.AppUser).Where(filter).ToListAsync();
        }
    }
}
