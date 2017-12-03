using System.Collections.Generic;
using UsandoDapper.Models;

namespace TesteDapperRepository.Repository
{
    public interface IFornecedorRepository : IGenericRepository<Fornecedor>
    {
        IEnumerable<Fornecedor> GetFornecedorByCidade(string Cidade);

        IEnumerable<Fornecedor> GetFornecedorByPais(string Pais);
    }
}
