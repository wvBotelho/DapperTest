using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using TesteDapperRepository.Service;
using UsandoDapper.Models;

namespace UsandoDapper.UnitTest
{
    [TestClass]
    public class ProdutoServiceTest
    {
        #region Helper

        private ProdutoService Service;

        [TestInitialize]
        public void Initialize()
        {
            Service = new ProdutoService();
        }

        private Produto GetNewProduto
        {
            get
            {
                return new Produto()
                {
                    Nome = "Berserk and the Band of Hawks",
                    Preco = 119.90M,
                    QtEstoque = 10,
                    DataRegistro = DateTime.Today,                    
                };
            }
        }

        private Produto GetLastProduto
        {
            get
            {
                Produto produto = Service.GetList().LastOrDefault();

                if (produto == null)
                {
                    Assert.Fail("Produto não encontrado");
                }

                return produto;
            }
        }

        #endregion

        [TestMethod]
        public void Adicionar_Produto()
        {
            Assert.IsTrue(Service.Add(GetNewProduto));
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException), "Produto já cadastrado")]
        public void Nao_Adicionar_Mesmo_Produto()
        {
            Service.Add(GetNewProduto);
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException), "Não pode cadastrar produto com quantidade zero ou inferior")]
        public void Nao_Adicionar_Produto_Qtd_Vazia()
        {
            Produto produto = GetNewProduto;
            produto.QtEstoque = 0;

            Service.Add(produto);
        }

        [TestMethod]
        public void Atualizar_Produto()
        {
            Produto produto = GetLastProduto;
            produto.Preco = 89.90M;

            Assert.IsTrue(Service.Update(produto));
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException), "Produto já cadastrado")]
        public void Nao_Atualizar_Mesmo_Produto()
        {
            Produto produto = GetLastProduto;
            produto.ProdutoID = GetLastProduto.ProdutoID + 1;

            Assert.IsTrue(Service.Update(produto));
        }
    }
}
