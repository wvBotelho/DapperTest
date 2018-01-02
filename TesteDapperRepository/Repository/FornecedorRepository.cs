using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using UsandoDapper.Models;

namespace TesteDapperRepository.Repository
{
    public class FornecedorRepository : GenericRepository<Fornecedor>, IFornecedorRepository
    {
        public override bool Add(Fornecedor entity)
        {
            try
            {
                if (entity == null)
                {
                    return false;
                }

                using (IDbConnection connection = connectionFactory.GetConnection)
                {
                    string commandInsert = "insert into dbo.wbyp_fornecedor_dap (nome, nome_contato, cargo, cidade, pais) " +
                                           "values (@Nome, @Contato, @Cargo, @Cidade, @Pais)";

                    connection.Execute(commandInsert, entity, null, 1000);

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

                using (IDbConnection connection = connectionFactory.GetConnection)
                {
                    string commandUpdate = "update dbo.wbyp_fornecedor_dap " +
                                           "set nome = @Nome, nome_contato = @Contato, cargo = @Cargo, cidade = @Cidade, Pais = @Pais " +
                                           "where id_fornecedor = @FornecedorID"; 

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

                using (IDbConnection connection = connectionFactory.GetConnection)
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

                using (IDbConnection connection = connectionFactory.GetConnection)
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
                using (IDbConnection connection = connectionFactory.GetConnection)
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
                using (IDbConnection connection = connectionFactory.GetConnection)
                {
                    string commandSelect = "select * from dbo.wbyp_fornecedor_dap " +
                                           "where cidade = @Cidade";

                    return connection.Query<Fornecedor>(commandSelect, new { Cidade }).ToList();
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
                using (IDbConnection connection = connectionFactory.GetConnection)
                {
                    string commandSelect = "select * from dbo.wbyp_fornecedor_dap " +
                                           "where pais = @Pais";

                    return connection.Query<Fornecedor>(commandSelect, new { Pais }).ToList();
                }
            }
            catch (SqlException e)
            {
                throw new Exception(e.Message.ToString());
            }
        }
    }
}
