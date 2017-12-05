using System;
using System.Linq;
using TesteDapperRepository.Repository;
using UsandoDapper.Models;

namespace TesteDapperRepository.Service
{
    public class ProdutoService : ProdutoRepository
    {
        public override bool Add(Produto produto)
        {
            var produtosCadastrados = GetList().Where(p => p.Nome.Equals(produto.Nome));

            if (produtosCadastrados.Any())
            {
                throw new ApplicationException("Produto já cadastrado");
            }

            if (produto.QtEstoque <= 0)
            {
                throw new ApplicationException("Não pode cadastrar produto com quantidade zero ou inferior");
            }

            return base.Add(produto);
        }

        public override bool Update(Produto produto)
        {
            var produtosCadastrados = GetList().Where(p => p.Nome.Equals(produto.Nome) && p.ProdutoID != produto.ProdutoID);

            if (produtosCadastrados.Any())
            {
                throw new ApplicationException("Produto já cadastrado");
            }

            if (produto.QtEstoque <= 0)
            {
                throw new ApplicationException("Não pode cadastrar produto com quantidade zero ou inferior");
            }

            return base.Update(produto);
        }

        public override bool Delete(int id)
        {
            Produto produto = Find(id);

            if (produto.QtEstoque > 0)
            {
                throw new ApplicationException("Produto ainda disponível no estoque");
            }

            return base.Delete(id);
        }
    }
}
