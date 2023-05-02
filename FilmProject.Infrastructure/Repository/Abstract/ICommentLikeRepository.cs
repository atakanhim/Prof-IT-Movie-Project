using FilmProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmProject.Infrastructure.Repository.Abstract
{
    public interface ICommentLikeRepository : IEntityRepository<CommentLike>
    {
        Task<CommentLike> IsCommentLikedAsync(string userId, int CommentId);
        Task<int> NumberOfCommentLikeAsync(int CommentId);
        Task ChangeCommentLikeStatueAsync(CommentLike commentLike);
    }
}
