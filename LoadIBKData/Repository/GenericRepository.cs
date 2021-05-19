using LoadIBKData.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LoadIBKData.Repository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        internal APIDbContext context;
        internal DbSet<TEntity> dbSet;
        public GenericRepository(APIDbContext context)
        {
            this.context = context;
            this.dbSet = this.context.Set<TEntity>();
        }

        public virtual IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<TEntity> query = dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }
            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        public virtual TEntity GetById(string id)
        {
            return this.dbSet.Find(id);
        }

        public virtual void Add(TEntity entity)
        {
            this.dbSet.Add(entity);
            this.context.SaveChanges();
        }

        public virtual void Update(TEntity entity)
        {
            this.dbSet.Attach(entity);
            this.context.Entry(entity).State = EntityState.Modified;
            this.context.SaveChanges();
        }

        public virtual void Delete(TEntity entity)
        {
            this.dbSet.Remove(entity);
            this.context.SaveChanges();
        }
    }
}
