using Dapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using UsandoDapper.Models;

namespace UsandoDapper.Data
{
    public class DapperRepository
    {
        private string ConnectionString { get; }
        private string insertCommandProduto = "insert into dbo.wbyp_produto_dap (nome, preco, qtd_estoque)" +
                                              "values (@Nome, @Preco, @QtEstoque)";
        private string insertComandFornecedor = "insert into dbo.wbyp_fornecedor_dap (nome, nome_contato, cargo)" +
                                                "values (@Nome, @Contato, @Cargo)";
        private string updateCommandProduto = "update dbo.wbyp_produto_dap " +
                                              "set nome = @Nome, preco = @Preco, qtd_estoque = @QtEstoque, qtd_pedido = @QtPedido " +
                                              "where id_produto = @ProdutoID";
        private string updateCommandFornecedor = "update dbo.wbyp_fornecedor_dap " +
                                                 "set nome = @Nome, nome_contato = @Contato, cargo = @Cargo, cidade = @Cidade, pais = @Pais " +
                                                 "where id_fornecedor = @FornecedorID";
        private string deleteCommandProduto = "delete from dbo.wbyp_produto_dap where id_produto = @ProdutoID";
        private string deleteCommandFornecedor = "delete from dbo.wbyp_fornecedor_dap where id_fornecedor = @FornecedorID";
        private string selectCommandProduto = "select * from dbo.wbyp_produto_dap where id_produto = @ProdutoID";
        private string selectCommandProdutos = "select * from dbo.wbyp_produto_dap";
        private string selectCommandFornecedor = "select  from dbo.wbyp_fornecedor_dap where id_fornecedor = @FornecedorID";


        public DapperRepository()
        {
            //mapeando atributos do objeto com a tabela  
            SqlMapper.SetTypeMap(typeof(Produto), new CustomPropertyTypeMap(typeof(Produto),
                (type, columnName) => type.GetProperties().FirstOrDefault(prop => prop.GetCustomAttributes(false).OfType<ColumnAttribute>().Any(attr => attr.Name == columnName))));

            SqlMapper.SetTypeMap(typeof(Fornecedor), new CustomPropertyTypeMap(typeof(Fornecedor),
                (type, columnName) => type.GetProperties().FirstOrDefault(prop => prop.GetCustomAttributes(false).OfType<ColumnAttribute>().Any(attr => attr.Name == columnName))));

            ConnectionString = ConfigurationManager.ConnectionStrings["DapperContext"].ToString();
        }

        public bool CreateProduto(Produto produto)
        {
            try
            {
                if (produto == null)
                {
                    return false;
                }

                using (var connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    connection.Execute(insertCommandProduto, produto);

                    return true;
                }
            }
            catch (SqlException e)
            {
                throw new Exception("Erro: " + e.Message.ToString());
            }
        }

        public bool CreateFornecedor(Fornecedor fornecedor)
        {
            try
            {
                if (fornecedor == null)
                {
                    return false;
                }

                using (var connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    connection.Execute(insertComandFornecedor, fornecedor);

                    return true;
                }
            }
            catch (SqlException e)
            {
                throw new Exception("Erro: " + e.Message.ToString());
            }
        }

        public bool UpdateProduto(Produto produto)
        {
            try
            {
                if (produto == null)
                {
                    return false;
                }

                using (var connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    connection.Execute(updateCommandProduto, produto);

                    return true;
                }
            }
            catch (SqlException e)
            {
                throw new Exception("Erro: " + e.Message.ToString());
            }
        }

        public bool UpdateFornecedor(Fornecedor fornecedor)
        {
            try
            {
                if (fornecedor == null)
                {
                    return false;
                }

                using (var connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    connection.Execute(updateCommandFornecedor, fornecedor);

                    return true;
                }
            }
            catch (SqlException e)
            {
                throw new Exception("Erro: " + e.Message.ToString());
            }
        }

        public bool DeleteProduto(Produto produto)
        {
            try
            {
                if (produto == null)
                {
                    return false;
                }

                using (var connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    connection.Execute(deleteCommandProduto, produto);

                    return true;
                }
            }
            catch (SqlException e)
            {
                throw new Exception("Erro: " + e.Message.ToString());
            }
        }

        public bool DeleteFornecedor(Fornecedor fornecedor)
        {
            try
            {
                if (fornecedor == null)
                {
                    return false;
                }

                using (var connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    connection.Execute(deleteCommandFornecedor, fornecedor);

                    return true;
                }
            }
            catch (SqlException e)
            {
                throw new Exception("Erro: " + e.Message.ToString());
            }
        }

        public Produto FindProduto (int ProdutoID)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                var produto = connection.QueryFirstOrDefault<Produto>(selectCommandProduto, new { ProdutoID = ProdutoID });

                if (produto == null)
                {
                    return null;
                }

                return produto;
            }
        }

        public Fornecedor FindFornecedor(int FornecedorID)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                var forneccedor = connection.QueryFirstOrDefault<Fornecedor>(selectCommandFornecedor, new { FornecedorID = FornecedorID });

                if (forneccedor == null)
                {
                    return null;
                }

                return forneccedor;
            }
        }

        public IEnumerable<Produto> GetList()
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                return connection.Query<Produto>(selectCommandProdutos);
            }
        }
    }
}
