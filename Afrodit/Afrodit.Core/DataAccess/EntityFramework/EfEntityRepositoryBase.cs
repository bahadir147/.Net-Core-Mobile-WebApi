using Afrodit.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Afrodit.Core.Repositories.EntityFramework
{
    public class EfEntityRepositoryBase<TEntity, TContext> : IEntityRepository<TEntity>
        where TEntity : class, IEntity, new()
        where TContext : DbContext, new()
    {
        public async Task<bool> Add(TEntity entity)
        {
            using (var context = new TContext())
            {
                var addedEntity = context.Entry(entity);
                addedEntity.State = EntityState.Added;
                int returnValue = await context.SaveChangesAsync();
                if (returnValue > 0) return true;
                else return false;
            }
        }
        public async Task<bool> Delete(TEntity entity)
        {
            using (var context = new TContext())
            {
                var deletedEntity = context.Entry(entity);
                deletedEntity.State = EntityState.Deleted;
                int returnValue = await context.SaveChangesAsync();
                if (returnValue > 0) return true;
                else return false;
            }
        }
        public async Task<TEntity> Get(Expression<Func<TEntity, bool>> filter)
        {
            using (var context = new TContext())
            {
                return await context.Set<TEntity>().SingleOrDefaultAsync(filter);
            }
        }
        public async Task<List<TEntity>> GetList(Expression<Func<TEntity, bool>> filter = null)
        {
            using (var context = new TContext())
            {
                if (filter == null)
                    return await context.Set<TEntity>().ToListAsync();
                else return await context.Set<TEntity>().Where(filter).ToListAsync();
            }
        }
        public async Task<bool> Update(TEntity entity)
        {
            using (var context = new TContext())
            {
                var updatedEntity = context.Entry(entity);
                updatedEntity.State = EntityState.Modified;
                int returnValue = await context.SaveChangesAsync();
                if (returnValue > 0) return true;
                else return false;
            }
        }
    }
}
