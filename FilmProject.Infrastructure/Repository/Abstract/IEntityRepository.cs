using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FilmProject.Infrastructure.Repository.Abstract
{
    public interface IEntityRepository<T> where T : class
    {
        // get islemleri
        Task<List<T>> GetListAsync(Expression<Func<T, bool>>? filter = null);
        Task<IEnumerable<T>> GetIEnumerableListAsync(Expression<Func<T, bool>>? filter = null);
        Task<T?> GetAsync(Expression<Func<T, bool>> filter);


        // post islemleri
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);

    }
}
