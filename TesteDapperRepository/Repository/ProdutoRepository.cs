using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using UsandoDapper.Models;

namespace TesteDapperRepository.Repository
{
    public class ProdutoRepository : GenericRepository<Produto>, IProdutoReposity
    {
        public override bool Add(Produto entity)
        {
            try
            {
                if (entity == null)
                {
                    return false;
                }

                using (IDbConnection connection = connectionFactory.GetConnection)
                {
                    string commandInsert = "insert into dbo.wbyp_produto_dap (nome, preco, qtd_estoque, qtd_pedido, data_registro, data_esgotado) " +
                                           "values (@Nome, @Preco, @QtEstoque, @QtPedido, @DataRegistro, @DataEsgotado)";

                    connection.Execute(commandInsert, entity, null, 5000);

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

                using (IDbConnection connection = connectionFactory.GetConnection)
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

                using (IDbConnection connection = connectionFactory.GetConnection)
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

                using (IDbConnection connection = connectionFactory.GetConnection)
                {
                    string commandSelect = "select * from dbo.wbyp_produto_dap " +
                                           "where id_produto = @ProdutoID";

                    return connection.QueryFirstOrDefault<Produto>(commandSelect, new { ProdutoID = id });
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
                using (IDbConnection connection = connectionFactory.GetConnection)
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
            //TODO fazer a consulta por store procedure

            var query = GetList();

            if (FornecedorID.HasValue)
            {
                query = query.Where(p => p.FornecedorID == FornecedorID); 
            }

            if (!string.IsNullOrEmpty(Nome))
            {
                query = query.Where(p => p.Nome.ToLower().Contains(Nome.ToLower()));
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

        public IEnumerable<Produto> GetProdutoByFornecedor(int FornecedorID)
        {
            try
            {
                if (FornecedorID == 0)
                {
                    return null;
                }

                using (IDbConnection connection = connectionFactory.GetConnection)
                {
                    string commandSelect = "select * from dbo.wbyp_produto_dap " +
                                           "where id_fornecedor = @FornecedorID";

                    return connection.Query<Produto>(commandSelect, new { FornecedorID });
                }
            }
            catch (SqlException e)
            {
                throw new Exception(e.Message.ToString());
            }
        }

        public Produto GetLastProduto()
        {
            try
            {
                using (IDbConnection connection = connectionFactory.GetConnection)
                {
                    string commandSelect = "select * from dbo.wbyp_produto_dap " +
                                           "where id_produto = (select max(id_produto) from dbo.wbyp_produto_dap)";

                    return connection.QueryFirstOrDefault<Produto>(commandSelect);
                }
            }
            catch (SqlException e)
            {
                throw new Exception(e.Message.ToString());
            }
        }
    }
}
