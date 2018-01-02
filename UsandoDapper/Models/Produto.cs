using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UsandoDapper.Models
{
    public class Produto : IValidatableObject
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

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (QtEstoque == 0)
            {
                yield return new ValidationResult("Quantidade de estoque não pode ser zero", new[] { "QtEstoque" });
            }

            if (QtEstoque < 0)
            {
                yield return new ValidationResult("Quantidade de estoque não pode ser negativa", new[] { "QtEstoque" });
            }

            if (string.IsNullOrEmpty(Nome))
            {
                yield return new ValidationResult("Nome obrigatório", new[] { "Nome" });
            }

            if (DataEsgotado < DataEsgotado)
            {
                yield return new ValidationResult("Data de esgotado não pode ser maior que data registro", new[] { "DataEsgotado" });
            }

            if (DataRegistro < DateTime.Today)
            {
                yield return new ValidationResult("Data de Registro não pode ser inferior ao dia atual", new[] { "DataEsgotado" });
            }
        }
    }
}
