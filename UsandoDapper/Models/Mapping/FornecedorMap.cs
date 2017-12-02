using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace UsandoDapper.Models.Mapping
{
    public class FornecedorMap : EntityTypeConfiguration<Fornecedor>
    {
        public FornecedorMap()
        {
            //Definindo a chave primária
            HasKey(f => f.FornecedorID);

            //Definindo o nome da tabela e os campos
            ToTable("wbyp_fornecedor_dap");

            Property(f => f.FornecedorID)
                .HasColumnName("id_fornecedor")
                .HasColumnType("int")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .IsRequired();

            Property(p => p.Nome)
                .HasColumnName("nome")
                .HasColumnType("nvarchar")
                .HasMaxLength(60)
                .IsRequired();

            Property(p => p.Contato)
                .HasColumnName("nome_contato")
                .HasColumnType("nvarchar")
                .HasMaxLength(60)
                .IsRequired();

            Property(p => p.Cargo)
                .HasColumnName("cargo")
                .HasColumnType("nvarchar")
                .HasMaxLength(60)
                .IsRequired();

            Property(p => p.Cidade)
                .HasColumnName("cidade")
                .HasColumnType("nvarchar")
                .HasMaxLength(60)
                .IsOptional();

            Property(p => p.Pais)
                .HasColumnName("pais")
                .HasColumnType("nvarchar")
                .HasMaxLength(60)
                .IsOptional();
        }
    }
}
