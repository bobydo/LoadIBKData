using LoadIBKData.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace LoadIBKData.Repository
{
    public interface IHistoricPriceUnitOfWork
    {
        void Add(Price item);
        void Delete(Price item);
        IEnumerable<Price> Get(Expression<Func<Price, bool>> filter = null, Func<IQueryable<Price>, IOrderedQueryable<Price>> orderBy = null, string includeProperties = "");
        Price GetById(string id);
        IEnumerable<Price> GetList(string Symbol);
        void Update(Price item);
    }
}