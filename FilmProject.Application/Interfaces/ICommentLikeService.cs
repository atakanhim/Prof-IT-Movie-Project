using FilmProject.Application.Contracts.Movie;
using FilmProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FilmProject.Application.Interfaces
{
    public interface ICommentLikeService
    {
        Task<bool> IsCommentLike(string userId, int CommentId);
        Task<int> NumberofCommentLike(int CommentId);
        Task ChangeCommentLikeStatue(CommentLikeDto commentLike);
        Task<CommentLikeDto> GetAsync(Expression<Func<CommentLike, bool>> filter);
    }
}
