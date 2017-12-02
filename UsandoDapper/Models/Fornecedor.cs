using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UsandoDapper.Models
{
    [Table("wbyp_fornecedor_dap")]
    public class Fornecedor
    {
        [Key]
        public int FornecedorID { get; set; }

        public string Nome { get; set; }

        public string Contato { get; set; }

        public string Cargo { get; set; }

        public string Cidade { get; set; }

        public string Pais { get; set; }

        //propriedade de navegação
        public ICollection<Produto> Produtos { get; set; }
    }
}
