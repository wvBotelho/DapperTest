using Dapper;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using TesteDapperRepository.Infraestrutura;

namespace TesteDapperRepository.Repository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        protected IConnectionFactory connectionFactory;

        public GenericRepository()
        {
            //conexão
            connectionFactory = new ConnectionFactory();

            //mapeando atributos do objeto com a tabela  
            SqlMapper.SetTypeMap(typeof(TEntity), new CustomPropertyTypeMap(typeof(TEntity),
                (type, columnName) => type.GetProperties().FirstOrDefault(prop => prop.GetCustomAttributes(false).OfType<ColumnAttribute>().Any(attr => attr.Name == columnName))));
        }

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
