using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FilmProject.Infrastructure.Repository.Abstract
{
    public interface IEntityRepository<T> where T : class
    {
        Task<List<T>> GetListAsync(Expression<Func<T, bool>>? filter = null); // delege
        Task<T?> GetAsync(Expression<Func<T, bool>> filter);

        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);

    }
}
