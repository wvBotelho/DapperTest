using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UsandoDapper.Models
{
    [Table("wbyp_fornecedor_dap")]
    public class Fornecedor
    {
        [Key]
        [Column("id_fornecedor")]
        public int FornecedorID { get; set; }

        [Column("nome")]
        public string Nome { get; set; }

        [Column("nome_contato")]
        public string Contato { get; set; }

        [Column("cargo")]
        public string Cargo { get; set; }

        [Column("cidade")]
        public string Cidade { get; set; }

        [Column("pais")]
        public string Pais { get; set; }

        //propriedade de navegação
        public ICollection<Produto> Produtos { get; set; }
    }
}
