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
        Task<IEnumerable<Comment>> GetAllAsync(Expression<Func<Movie, bool>>? filter = null); // tum filmleri doner

        Task<int> GetCountOfTotalComment();
    }
}
