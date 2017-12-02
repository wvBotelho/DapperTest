using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using UsandoDapper.Data;
using UsandoDapper.Models;

namespace UsandoDapper.UnitTest
{
    [TestClass]
    public class CrudTests
    {
        private DapperRepository context; 

        [TestInitialize]
        public void Initialize()
        {
            context = new DapperRepository();
        }
        
        private Produto GetNewProduto
        {
            get
            {
                return new Produto()
                {
                    Nome = "Final Fantasy X HD",
                    Preco = 108.90M,
                    QtEstoque = 4
                };
            }
        }

        private Fornecedor GetNewFornecedor
        {
            get
            {
                return new Fornecedor()
                {
                    Nome = "BigBoy Games",
                    Contato = "Big Boy",
                    Cargo = "CEO",
                    Cidade = "São Paulo",
                    Pais = "Brasil"
                };
            }
        }

        [TestMethod]
        public void Create_Produto()
        {
            Assert.IsTrue(context.CreateProduto(GetNewProduto));
        }

        [TestMethod]
        public void Create_Fornecedor()
        {
            Assert.IsTrue(context.CreateFornecedor(GetNewFornecedor));
        }

        [TestMethod]
        public void Update_Produto()
        {
            Produto produto = context.GetList().LastOrDefault();
            produto.Preco = 75.90M;
            produto.QtEstoque = 1;

            Assert.IsTrue(context.UpdateProduto(produto));
        }

        [TestMethod]
        public void Teste()
        {
            Fornecedor fornecedor = context.FindFornecedor(1);
        }
    }
}
