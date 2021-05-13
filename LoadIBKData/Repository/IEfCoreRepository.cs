using LoadIBKData.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LoadIBKData.Repository
{
    public interface IEfCoreRepository<TEntity> where TEntity : class, IEntity
    {
        Task<TEntity> Add(TEntity entity);
        Task<TEntity> Delete(int id);
        Task<TEntity> Get(int id);
        Task<List<TEntity>> GetAll();
        Task<TEntity> Update(TEntity entity);
    }
}