using Dapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlClient;
using System.Linq;
using TesteDapperRepository.Infraestrutura;
using UsandoDapper.Models;

namespace TesteDapperRepository.Repository
{
    public class FornecedorRepository : GenericRepository<Fornecedor>, IFornecedorRepository
    {
        private IConnectionFactory connectionFactory;

        public FornecedorRepository()
        {
            //conexão
            connectionFactory = new ConnectionFactory();

            //mapeando atributos do objeto com a tabela  
            SqlMapper.SetTypeMap(typeof(Fornecedor), new CustomPropertyTypeMap(typeof(Fornecedor),
                (type, columnName) => type.GetProperties().FirstOrDefault(prop => prop.GetCustomAttributes(false).OfType<ColumnAttribute>().Any(attr => attr.Name == columnName))));
        }

        public override bool Add(Fornecedor entity)
        {
            try
            {
                if (entity == null)
                {
                    return false;
                }

                using (var connection = new SqlConnection(connectionFactory.GetConnection.ConnectionString.ToString()))
                {
                    string commandInsert = "insert into dbo.wbyp_fornecedor_dap (nome, nome_contato, cargo, cidade, pais) " +
                                           "values (@Nome, @NomeContato, @Cargo, @Cidade, @Pais)";

                    connection.Execute(commandInsert, entity, null, 5000);

                    return true;
                }
            }
            catch (SqlException e)
            {
                throw new Exception(e.Message.ToString());
            }
        }

        public override bool Update(Fornecedor entity)
        {
            try
            {
                if (entity == null)
                {
                    return false;
                }

                using (var connection = new SqlConnection(connectionFactory.GetConnection.ConnectionString.ToString()))
                {
                    string commandUpdate = "update dbo.wbyp_fornecedor_dap " +
                                           "set nome = @Nome, nome_contato = @NomeContato, cargo = @Cargo"; 

                    connection.Execute(commandUpdate, entity);

                    return true;
                }

            }
            catch (SqlException e)
            {
                throw new Exception(e.Message.ToString());
            }
        }

        public override bool Delete(int id)
        {
            try
            {
                if (id == 0)
                {
                    return false;
                }

                using (var connection = new SqlConnection(connectionFactory.GetConnection.ConnectionString.ToString()))
                {
                    string commandDelete = "delete from dbo.wbyp_fornecedor_dap " +
                                           "where id_fornecedor = @FornecedorID";

                    connection.Execute(commandDelete, new { FornecedorID = id });

                    return true;
                }
            }
            catch (SqlException e)
            {
                throw new Exception(e.Message.ToString());
            }
        }

        public override Fornecedor Find(int id)
        {
            try
            {
                if (id == 0)
                {
                    return null;
                }

                using (var connection = new SqlConnection(connectionFactory.GetConnection.ConnectionString.ToString()))
                {
                    string commandSelect = "select * from dbo.wbyp_fornecedor_dap " +
                                           "where id_fornecedor = @FornecedorID";

                    return connection.QueryFirstOrDefault<Fornecedor>(commandSelect, new { FornecedorID = id });
                }
            }
            catch (SqlException e)
            {
                throw new Exception(e.Message.ToString());
            }
        }

        public override IEnumerable<Fornecedor> GetList()
        {
            try
            {
                using (var connection = new SqlConnection(connectionFactory.GetConnection.ConnectionString.ToString()))
                {
                    string commandSelect = "select * from dbo.wbyp_fornecedor_dap";

                    return connection.Query<Fornecedor>(commandSelect).ToList();
                }
            }
            catch (SqlException e)
            {
                throw new Exception(e.Message.ToString());
            }
        }

        public IEnumerable<Fornecedor> GetFornecedorByCidade(string Cidade)
        {
            try
            {
                using (var connection = new SqlConnection(connectionFactory.GetConnection.ConnectionString.ToString()))
                {
                    string commandSelect = "select * from dbo.wbyp_fornecedor_dap " +
                                           "where cidade = @Cidade";

                    return connection.Query<Fornecedor>(commandSelect, new { Cidade = Cidade }).ToList();
                }
            }
            catch (SqlException e)
            {
                throw new Exception(e.Message.ToString());
            }
        }

        public IEnumerable<Fornecedor> GetFornecedorByPais(string Pais)
        {
            try
            {
                using (var connection = new SqlConnection(connectionFactory.GetConnection.ConnectionString.ToString()))
                {
                    string commandSelect = "select * from dbo.wbyp_fornecedor_dap " +
                                           "where pais = @Pais";

                    return connection.Query<Fornecedor>(commandSelect, new { Pais = Pais }).ToList();
                }
            }
            catch (SqlException e)
            {
                throw new Exception(e.Message.ToString());
            }
        }
    }
}
