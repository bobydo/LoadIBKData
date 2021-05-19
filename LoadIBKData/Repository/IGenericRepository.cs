using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace LoadIBKData.Repository
{
    public interface IGenericRepository<TEntity>
    {
        void Add(TEntity entity);
        void Delete(TEntity entity);
        IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "");
        TEntity GetById(string id);
        void Update(TEntity entity);
    }
}