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
    public class ProdutoRepository : GenericRepository<Produto>, IProdutoReposity
    {
        private IConnectionFactory connectionFactory;

        public ProdutoRepository()
        {
            //conexão
            connectionFactory = new ConnectionFactory();

            //mapeando atributos do objeto com a tabela  
            SqlMapper.SetTypeMap(typeof(Produto), new CustomPropertyTypeMap(typeof(Produto),
                (type, columnName) => type.GetProperties().FirstOrDefault(prop => prop.GetCustomAttributes(false).OfType<ColumnAttribute>().Any(attr => attr.Name == columnName))));
        }

        public override bool Add(Produto entity)
        {
            try
            {
                if (entity == null)
                {
                    return false;
                }

                using (var connection = new SqlConnection(connectionFactory.GetConnection.ConnectionString.ToString()))
                {
                    string commandInsert = "insert into dbo.wbyp_produto_dap (nome, preco, qtd_estoque, qtd_pedido, data_registro, data_esgotado) " +
                                           "values (@Nome, @Preco, @QtEstoque, @QtPedido, @DataRegistro, @DataEsgotado)";

                    connection.Execute(commandInsert, entity);

                    return true;
                }
               
            }
            catch (SqlException e)
            {
                throw new Exception(e.Message.ToString());
            }
        }

        public override bool Update(Produto entity)
        {
            try
            {
                if (entity == null)
                {
                    return false;
                }

                using (var connection = new SqlConnection(connectionFactory.GetConnection.ConnectionString.ToString()))
                {
                    string commandUpdate = "update dbo.wbyp_produto_dap " +
                                           "set nome = @Nome, preco = @Preco, qtd_estoque = @QtEstoque, qtd_pedido = @QtPedido, data_registro = @DataRegistro, data_esgotado = @DataEsgotado " +
                                           "where id_produto = @ProdutoID";

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
                    string commandDelete = "delete from dbo.wbyp_produto_dap " +
                                           "where id_produto = @ProdutoID";

                    connection.Execute(commandDelete, new { ProdutoID = id });

                    return true;
                }
            }
            catch (SqlException e)
            {
                throw new Exception(e.Message.ToString());
            }
        }

        public override Produto Find(int id)
        {
            try
            {
                if (id == 0)
                {
                    return null;
                }

                using (var connection = new SqlConnection(connectionFactory.GetConnection.ConnectionString.ToString()))
                {
                    string commandSelect = "select * from dbo.wbyp_produto_dap " +
                                           "where id_produto = @ProdutoID";

                    return connection.QueryFirstOrDefault(commandSelect, new { ProdutoID = id });
                }
            }
            catch (SqlException e)
            {
                throw new Exception(e.Message.ToString());
            }
        }

        public override IEnumerable<Produto> GetList()
        {
            try
            {
                using (var connection = new SqlConnection(connectionFactory.GetConnection.ConnectionString.ToString()))
                {
                    string commandSelect = "select * from dbo.wbyp_produto_dap";

                    return connection.Query<Produto>(commandSelect).ToList();
                }
            }
            catch (SqlException e)
            {
                throw new Exception(e.Message.ToString());
            }
        }

        public IEnumerable<Produto> GetProdutoByFilter(int? FornecedorID, string Nome, DateTime? DataRegistro, DateTime? DataEsgotado)
        {
            var query = GetList();

            if (FornecedorID.HasValue)
            {
                query = query.Where(p => p.FornecedorID == FornecedorID); 
            }

            if (!string.IsNullOrEmpty(Nome))
            {
                query = query.Where(p => p.Nome.Equals(Nome));
            }

            if (DataRegistro.HasValue)
            {
                query = query.Where(p => p.DataRegistro == DataRegistro);
            }

            if (DataEsgotado.HasValue)
            {
                query = query.Where(p => p.DataEsgotado == DataEsgotado);
            }

            return query.ToList();
        }
    }
}
