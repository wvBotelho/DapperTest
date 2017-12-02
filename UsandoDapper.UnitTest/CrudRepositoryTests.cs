using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using TesteDapperRepository.Repository;
using UsandoDapper.Models;

namespace UsandoDapper.UnitTest
{
    [TestClass]
    public class CrudRepositoryTests
    {
        private ProdutoRepository Service;

        [TestInitialize]
        public void Initialize()
        {
            Service = new ProdutoRepository();
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
                    DataRegistro = DateTime.Now
                };
            }
        }

        [TestMethod]
        public void Adicionar_Produto()
        {
            Assert.IsTrue(Service.Add(GetNewProduto));

            Produto produto = Service.GetList().LastOrDefault();

            Assert.AreEqual(GetNewProduto.Nome, produto.Nome);
        }
    }
}
