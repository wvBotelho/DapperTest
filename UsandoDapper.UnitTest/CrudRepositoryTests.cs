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
        private EntityContext EntityService;
        private FornecedorRepository FornecedorService;
        private Stopwatch stopwatch;

        [TestInitialize]
        public void Initialize()
        {
            Service = new ProdutoRepository();
            EntityService = new EntityContext();
            FornecedorService = new FornecedorRepository();
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

        private Fornecedor GetNewFornecedor
        {
            get
            {
                return new Fornecedor()
                {
                    Nome = "Big Boy Games",
                    Contato = "Big Boy",
                    Cargo = "CEO",
                    Cidade = "São Paulo",
                    Pais = "Brasil"                    
                };
            }
        }

        private List<Fornecedor> GetListNewFornecedores
        {
            get
            {
                List<Fornecedor> Fornecedores = new List<Fornecedor>()
                {
                    new Fornecedor()
                    {
                        Nome = "Games 4",
                        Contato = "Ricardo Gonçalves",
                        Cargo = "Vendedor",
                        Cidade = "São Paulo",
                        Pais = "Brasil"
                    },

                    new Fornecedor()
                    {
                        Nome = "Submarino",
                        Contato = "Um cara rico",
                        Cargo = "CEO",
                        Cidade = "Rio de Janeiro",
                        Pais = "Brasil"
                    },

                    new Fornecedor()
                    {
                        Nome = "Americanas.com",
                        Contato = "Um americano",
                        Cargo = "Vice-Presidente",
                        Cidade = "New York City",
                        Pais = "United States of America"
                    },

                    new Fornecedor()
                    {
                        Nome = "Aliexpress",
                        Contato = "Um chinês",
                        Cargo = "Dono da porra toda",
                        Cidade = "Xangai",
                        Pais = "China"
                    }
                };

                return Fornecedores;
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

        private Produto GetLastProduto
        {
            get
            {
                //Produto produto = Service.GetList().LastOrDefault();
                Produto produto = Service.GetLastProduto();

                if (produto == null)
                {
                    Assert.Fail("Produto não encontrado");
                }

                return produto;
            }
        }

        private Fornecedor GetLastFornecedor
        {
            get
            {
                //Fornecedor fornecedor = FornecedorService.GetList().LastOrDefault();
                Fornecedor fornecedor = FornecedorService.GetLastFornecedor();

                if (fornecedor == null)
                {
                    Assert.Fail("Fornecedor não encontrado");
                }

                return fornecedor;
            }
        }

        #endregion

        #region Produto

        [TestMethod]
        public void Adicionar_Produto()
        {
            Assert.IsTrue(Service.Add(GetNewProduto));

            Assert.AreEqual(GetNewProduto.Nome, GetLastProduto.Nome);
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
        public void Atualizar_Produto()
        {
            Produto produto = GetLastProduto;
            produto.Nome = "Ys VII - Lacrimosa of Dana";
            produto.Preco = 189.99M;
            produto.QtEstoque = 5;
            produto.DataRegistro = DateTime.Now;

            Assert.IsTrue(Service.Update(produto));
        }

        [TestMethod]
        public void Deletar_Produto()
        {
            int id = GetLastProduto.ProdutoID;

            Assert.IsTrue(Service.Delete(id));

            Assert.IsNull(Service.Find(id));
        }

        [TestMethod]
        public void Consultar_Produto_Por_ID()
        {
            Assert.IsNotNull(Service.Find(GetLastProduto.ProdutoID));
        }

        [TestMethod]
        public void Consultar_Produto_Por_Filtro()
        {
            var query = Service.GetProdutoByFilter(null, string.Empty, DateTime.Today, null);

            int valorEsperado = 4;

            Assert.AreEqual(valorEsperado, query.Count());
        }

        [TestMethod]
        public void Consultar_Produto_Por_Nome()
        {
            var query = Service.GetProdutoByFilter(null, "final fantasy", null, null);

            int valorEsperado = 3;

            Assert.AreEqual(valorEsperado, query.Count());
        }

        [TestMethod]
        public void Consultar_Produto_Por_FornecedorID()
        {
            var query = Service.GetProdutoByFornecedor(GetLastFornecedor.FornecedorID);

            int valorEsperado = 0;

            Assert.AreEqual(valorEsperado, query.Count());
        }

        #endregion

        #region Fornecedor

        [TestMethod]
        public void Adicionar_Fornecedor()
        {
            Assert.IsTrue(FornecedorService.Add(GetNewFornecedor));
        }

        [TestMethod]
        public void Adicionar_Fornecedores()
        {
            foreach(Fornecedor fornecedor in GetListNewFornecedores)
            {
                Assert.IsTrue(FornecedorService.Add(fornecedor));
            }
        }

        [TestMethod]
        public void Atualizar_Fornecedor()
        {
            Fornecedor fornecedor = GetLastFornecedor;
            fornecedor.Contato = "Jack Chan";
            fornecedor.Cidade = "Pequin";
            fornecedor.Pais = "Hong Kong";

            Assert.IsTrue(FornecedorService.Update(fornecedor));
        }

        [TestMethod]
        public void Deletar_Fornecedor()
        {
            int id = GetLastFornecedor.FornecedorID;

            Assert.IsTrue(FornecedorService.Delete(id));

            Assert.IsNull(FornecedorService.Find(id));
        }

        [TestMethod]
        public void Consultar_Fornecedor_Por_Cidade()
        {
            var query = FornecedorService.GetFornecedorByCidade("São Paulo");

            int valorEsperado = 2;

            Assert.AreEqual(valorEsperado, query.Count());
        }

        [TestMethod]
        public void Consultar_Fornecedor_Por_Pais()
        {
            var query = FornecedorService.GetFornecedorByPais("Brasil");

            int valorEsperado = 3;

            Assert.AreEqual(valorEsperado, query.Count());
        }

        [TestMethod]
        public void Adicionar_Fornecedor_A_Produto()
        {
            foreach (Fornecedor fornecedor in FornecedorService.GetList())
            {
                foreach(Produto produto in Service.GetList())
                {
                    produto.FornecedorID = fornecedor.FornecedorID;
                    Assert.IsTrue(Service.Update(produto));
                }
            }
        } 

        #endregion

        [TestMethod]
        public void Adicionar_Fornecedor_Produto()
        {
            var query = Service.GetList().Where(p => p.Nome.Contains("Final Fantasy"));

            foreach(var produto in query)
            {
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

            var query = EntityService.Produto;

            stopwatch.Stop();

            var tempo = stopwatch.Elapsed;
            var total = query.Count(); 
        }
    }
}
