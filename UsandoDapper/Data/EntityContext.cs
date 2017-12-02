using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using UsandoDapper.Models;
using UsandoDapper.Models.Mapping;

namespace UsandoDapper.Data
{
    public class EntityContext : DbContext
    {
        public EntityContext() : base("name=DapperContext")
        {
        }

        public DbSet<Produto> Produto { get; set; }

        public DbSet<Fornecedor> Fornecedor { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            modelBuilder.Configurations.Add(new ProdutoMap());
            modelBuilder.Configurations.Add(new FornecedorMap());
        }
    }
}
