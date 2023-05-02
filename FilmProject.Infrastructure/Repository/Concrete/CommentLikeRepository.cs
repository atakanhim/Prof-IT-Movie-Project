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

        public async Task ChangeCommentLikeStatueAsync(CommentLike commentLike)
        {
            var result = await this.IsCommentLikedAsync(commentLike.userId, commentLike.CommentId);
           if (result != null){
                _context.Remove(result);
            }
            else
            {
                _context.Add(commentLike);
            }

           await _context.SaveChangesAsync();
        }

        public async Task<CommentLike> IsCommentLikedAsync(string userId, int CommentId)
        {
            var model =  await _context.CommentLikes.Where(l => l.userId == userId && l.CommentId == CommentId).FirstOrDefaultAsync();
            return model;

        }

        public async Task<int> NumberOfCommentLikeAsync(int CommentId)
        {
            return await _context.CommentLikes.CountAsync(l => l.CommentId == CommentId);
        }
    }
}
