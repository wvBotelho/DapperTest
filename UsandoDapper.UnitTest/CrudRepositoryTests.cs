using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using TesteDapperRepository.Repository;
using UsandoDapper.Data;
using UsandoDapper.Models;

namespace UsandoDapper.UnitTest
{
    [TestClass]
    public class CrudRepositoryTests
    {
        #region Helper

        private ProdutoRepository Service;
        private EntityContext Context;
        private Stopwatch stopwatch;

        [TestInitialize]
        public void Initialize()
        {
            Service = new ProdutoRepository();
            Context = new EntityContext();
            stopwatch = new Stopwatch();
        }

        private Produto GetNewProduto
        {
            get
            {
                return new Produto()
                {
                    Nome = "World of Final Fantasy",
                    Preco = 99.99M,
                    QtEstoque = 5,
                    DataRegistro = DateTime.Today
                };
            }
        }

        private List<Produto> GetListNewProdutos
        {
            get
            {
                List<Produto> produtos = new List<Produto>()
                {
                    new Produto()
                    {
                        Nome = "Muramasa - Rebirth",
                        Preco = 92.32M,
                        QtEstoque = 3,
                        DataRegistro = DateTime.Today
                    },

                    new Produto()
                    {
                        Nome = "World of Final fantasy",
                        Preco = 99.99M,
                        QtEstoque = 5,
                        DataRegistro = DateTime.Today
                    },

                    new Produto()
                    {
                        Nome = "Final Fantasy X - HD",
                        Preco = 108.52M,
                        QtEstoque = 1,
                        DataRegistro = DateTime.Today
                    },

                    new Produto()
                    {
                        Nome = "EXIST ARCHIVE: THE OTHER SIDE OF THE SKY",
                        Preco = 176.67M,
                        QtEstoque = 2,
                        DataRegistro = DateTime.Today
                    }
                };

                return produtos;
            }
        }

        #endregion

        [TestMethod]
        public void Adicionar_Produto()
        {
            Assert.IsTrue(Service.Add(GetNewProduto));

            Produto produto = Service.GetList().LastOrDefault();

            Assert.AreEqual(GetNewProduto.Nome, produto.Nome);
        }

        [TestMethod]
        public void Atualizar_Produto()
        {
            Produto produto = Service.GetList().LastOrDefault();
            produto.Nome = "Ys VII - Lacrimosa of Dana";
            produto.Preco = 189.99M;
            produto.QtEstoque = 5;
            produto.DataRegistro = DateTime.Now;

            Assert.IsTrue(Service.Update(produto));
        }

        [TestMethod]
        public void Deletar_Produto()
        {
            int produtoID = Service.GetList().FirstOrDefault().ProdutoID;

            Assert.IsTrue(Service.Delete(produtoID));

            Produto produto = Service.Find(produtoID);

            Assert.IsNull(produto);
        }

        [TestMethod]
        public void Consultar_Produto_Por_ID()
        {
            Assert.IsNotNull(Service.Find(3));
        }

        [TestMethod]
        public void Consultar_Produto_Por_Filtro()
        {
            var query = Service.GetProdutoByFilter(null, string.Empty, DateTime.Today, null);

            Assert.AreEqual(4, query.Count());
        }

        [TestMethod]
        public void Consultar_Produto_Por_Nome()
        {
            var query = Service.GetProdutoByFilter(null, "final fantasy", DateTime.Today, null);

            Assert.AreEqual(2, query.Count());
        }

        [TestMethod]
        public void Adicionar_Produtos()
        {
            foreach (var produto in GetListNewProdutos)
            {
                Assert.IsTrue(Service.Add(produto));
            }
        }

        [TestMethod]
        public void Adicionar_1000_Produtos()
        {
            for(int i = 0; i < 1000; i++)
            {
                Assert.IsTrue(Service.Add(GetNewProduto));
            }
        }

        [TestMethod]
        public void Performance()
        {
            stopwatch.Start();

            var query = Service.GetList();

            stopwatch.Stop();

            var tempo = stopwatch.Elapsed;
            var total = query.Count();
        }

        [TestMethod]
        public void Consultar_Entity_Framework()
        {
            stopwatch.Start();

            var query = Context.Produto;

            stopwatch.Stop();

            var tempo = stopwatch.Elapsed;
            var total = query.Count(); 
        }
    }
}
