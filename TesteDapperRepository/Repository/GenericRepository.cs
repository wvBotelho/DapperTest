using System.Collections.Generic;

namespace TesteDapperRepository.Repository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        public virtual bool Add(TEntity entity)
        {
            throw new System.NotImplementedException();
        }

        public virtual bool Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public virtual TEntity Find(int id)
        {
            throw new System.NotImplementedException();
        }

        public virtual IEnumerable<TEntity> GetList()
        {
            throw new System.NotImplementedException();
        }

        public virtual bool Update(TEntity entity)
        {
            throw new System.NotImplementedException();
        }
    }
}
