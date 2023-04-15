using FilmProject.Infrastructure.Data;
using FilmProject.Infrastructure.Repository.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FilmProject.Infrastructure.Repository.Concrete
{
    public class EntityRepository<T> : IEntityRepository<T> where T : class,new()

    {
        private readonly ApplicationDbContext context;
        public EntityRepository(ApplicationDbContext dbContext)
        {
            context = dbContext;
        }
        public void Add(T entity)
        {
                context.Add(entity);
                context.SaveChanges();
           
        }

        public void Delete(T entity)
        {

            context.Remove(entity);
            context.SaveChanges();
        }

        public async  Task<T?> GetAsync(Expression<Func<T, bool>> filter)
        {
            var x = await context.Set<T>()
                     .AsNoTracking()
                     .Where(filter)
                     .FirstOrDefaultAsync();

            if (x == null)
                return null;
            return x;
        }

        public async Task<List<T>> GetListAsync(Expression<Func<T, bool>>? filter = null)
        {
            return filter == null ? await context.Set<T>().ToListAsync() :
                                    await context.Set<T>().Where(filter).ToListAsync();
        }

        public void Update(T entity)
        {
            context.Update(entity);
            context.SaveChanges();
        }
    }
}
