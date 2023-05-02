using FilmProject.Domain.Entities;
using FilmProject.Infrastructure.Data;
using FilmProject.Infrastructure.Repository.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmProject.Infrastructure.Repository.Concrete
{
    public class CommentLikeRepository: EntityRepository<CommentLike>, ICommentLikeRepository
    {
        private readonly ApplicationDbContext _context;
        public CommentLikeRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _context = dbContext;
        }

        public async Task<bool> IsCommentLikedAsync(string userId, int CommentId)
        {
            return await _context.CommentLikes.AnyAsync(l => l.userId == userId && l.CommentId == CommentId);
        }
    }
}
