using System.Collections.Generic;

namespace TesteDapperRepository.Repository
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        TEntity Find(int id);
        IEnumerable<TEntity> GetList();
        bool Add(TEntity entity);
        bool Delete(int id);
        bool Update(TEntity entity); 
    }
}
