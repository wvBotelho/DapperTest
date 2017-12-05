using System;
using System.Collections.Generic;
using UsandoDapper.Models;

namespace TesteDapperRepository.Repository
{
    public interface IProdutoReposity : IGenericRepository<Produto>
    {
        IEnumerable<Produto> GetProdutoByFilter(int? FornecedorID, string Nome, DateTime? DataRegistro, DateTime? DataEsgotado);
        IEnumerable<Produto> GetProdutoByFornecedor(int FornecedorID);
    }
}
