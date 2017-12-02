using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace UsandoDapper.Models
{
    public class Produto
    {
        [Column("id_produto")]
        public int ProdutoID { get; set; }
        
        [Column("id_fornecedor")]
        public int FornecedorID { get; set; }
        
        [Column("nome")]
        public string Nome { get; set; }
        
        [Column("preco")]
        public decimal Preco { get; set; }
        
        [Column("qtd_estoque")]
        public short QtEstoque { get; set; }

        [Column("qtd_pedido")]
        public short QtPedido { get; set; }

        [Column("data_registro")]
        public DateTime DataRegistro { get; set; }

        [Column("data_esgotado")]
        public DateTime? DataEsgotado { get; set; }

        //Propriedade de navegação
        public Fornecedor Fornecedor { get; set; }
    }
}
