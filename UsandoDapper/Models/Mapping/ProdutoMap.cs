using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace UsandoDapper.Models.Mapping
{
    public class ProdutoMap : EntityTypeConfiguration<Produto>
    {
        public ProdutoMap()
        {
            //Definindo a chave primaria
            HasKey(p => p.ProdutoID);

            //Definindo o nome da tabela e os campos
            ToTable("wbyp_produto_dap");

            Property(p => p.ProdutoID)
                .HasColumnName("id_produto")
                .HasColumnType("int")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .IsRequired();

            Property(p => p.FornecedorID)
                .HasColumnName("id_fornecedor")
                .HasColumnType("int")
                .IsOptional();

            Property(p => p.Nome)
                .HasColumnName("nome")
                .HasColumnType("nvarchar")
                .HasMaxLength(60)
                .IsRequired();

            Property(p => p.Preco)
                .HasColumnName("preco")
                .HasColumnType("decimal")
                .IsRequired();

            Property(p => p.QtEstoque)
                .HasColumnName("qtd_estoque")
                .HasColumnType("smallint")
                .IsOptional();

            Property(p => p.QtPedido)
                .HasColumnName("qtd_pedido")
                .HasColumnType("smallint")
                .IsOptional();

            Property(p => p.DataRegistro)
                .HasColumnName("data_registro")
                .HasColumnType("datetime")
                .IsRequired();

            Property(p => p.DataEsgotado)
                .HasColumnName("data_esgotado")
                .HasColumnType("datetime")
                .IsOptional();

            //Relacionamento
            HasRequired(p => p.Fornecedor)
                .WithMany(p => p.Produtos)
                .HasForeignKey(p => p.FornecedorID)
                .WillCascadeOnDelete(false);
        }
    }
}
