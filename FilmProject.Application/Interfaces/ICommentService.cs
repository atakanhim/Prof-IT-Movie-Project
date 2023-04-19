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
        Task<Comment?> GetAsync(Expression<Func<Comment, bool>> filter);
        Task<IEnumerable<Comment>> GetAllAsync(Expression<Func<Comment, bool>>? filter = null); // tum filmleri doner
   
        Task<int> GetCountOfTotalComment();


        void Add(Comment comment);
        void Update(Comment comment);
    }
}
