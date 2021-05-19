using LoadIBKData.Data;
using LoadIBKData.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LoadIBKData.Repository
{
    public class HistoricPriceUnitOfWork : IHistoricPriceUnitOfWork
    {
        private readonly APIDbContext _context;
        private GenericRepository<Price> PriceRepository;
        public HistoricPriceUnitOfWork(APIDbContext getContext)
        {
            _context = getContext;
        }

        public virtual IEnumerable<Price> Get(
            Expression<Func<Price, bool>> filter = null,
            Func<IQueryable<Price>, IOrderedQueryable<Price>> orderBy = null,
            string includeProperties = "")
        {
            try
            {
                if (this.PriceRepository == null)
                {
                    this.PriceRepository = new GenericRepository<Price>(_context);
                }
                return this.PriceRepository.Get(filter, orderBy, includeProperties);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual IEnumerable<Price> GetList(string Symbol)
        {
            try
            {
                if (this.PriceRepository == null)
                {
                    this.PriceRepository = new GenericRepository<Price>(_context);
                }
                var wholeList = this.PriceRepository.Get();
                var priceList = wholeList.Where(el => Symbol == el.Symbol);
                return priceList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Price GetById(string id)
        {
            try
            {
                if (this.PriceRepository == null)
                {
                    this.PriceRepository = new GenericRepository<Price>(_context);
                }
                return this.PriceRepository.GetById(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Add(Price item)
        {
            try
            {
                if (this.PriceRepository == null)
                {
                    this.PriceRepository = new GenericRepository<Price>(_context);
                }
                this.PriceRepository.Add(item);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Update(Price item)
        {
            try
            {
                if (this.PriceRepository == null)
                {
                    this.PriceRepository = new GenericRepository<Price>(_context);
                }
                this.PriceRepository.Update(item);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Delete(Price item)
        {
            try
            {
                if (this.PriceRepository == null)
                {
                    this.PriceRepository = new GenericRepository<Price>(_context);
                }
                this.PriceRepository.Delete(item);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
