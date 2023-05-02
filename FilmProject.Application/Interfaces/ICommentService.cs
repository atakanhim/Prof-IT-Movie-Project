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
    public interface ICommentService
    {
        Task<CommentDto?> GetAsync(Expression<Func<Comment, bool>> filter);
        Task<IEnumerable<CommentDto>> GetAllAsync(Expression<Func<Comment, bool>>? filter = null); // tum filmleri doner
   
        Task<int> GetCountOfTotalComment();
        Task<IEnumerable<CommentDto>> GetListWithAppUser(Expression<Func<Comment, bool>>? filter = null);
        Task<IEnumerable<CommentDto>> GetListWithAppUserAndMovie(Expression<Func<Comment, bool>>? filter = null);//Admin comment listleme

        void Add(CommentDto comment);
        void Update(CommentDto comment);
    }
}
