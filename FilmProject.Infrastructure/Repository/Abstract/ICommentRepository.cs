using FilmProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FilmProject.Infrastructure.Repository.Abstract
{
    public interface ICommentRepository : IEntityRepository<Comment>
    {
        Task<int> GetCountOfTotalComment();
        Task<List<Comment>> GetListWithAppUser(Expression<Func<Comment, bool>>? filter = null);
        Task<List<Comment>> GetListWithAppUserAndMovie(Expression<Func<Comment, bool>>? filter = null);//Admin comment listelmek için
    }
}
