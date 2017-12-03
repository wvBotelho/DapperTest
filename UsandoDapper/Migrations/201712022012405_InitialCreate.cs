namespace UsandoDapper.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.wbyp_fornecedor_dap",
                c => new
                    {
                        id_fornecedor = c.Int(nullable: false, identity: true),
                        nome = c.String(nullable: false, maxLength: 60),
                        nome_contato = c.String(nullable: false, maxLength: 60),
                        cargo = c.String(nullable: false, maxLength: 60),
                        cidade = c.String(maxLength: 60),
                        pais = c.String(maxLength: 60),
                    })
                .PrimaryKey(t => t.id_fornecedor);
            
            CreateTable(
                "dbo.wbyp_produto_dap",
                c => new
                    {
                        id_produto = c.Int(nullable: false, identity: true),
                        id_fornecedor = c.Int(nullable: false),
                        nome = c.String(nullable: false, maxLength: 60),
                        preco = c.Decimal(nullable: false, precision: 18, scale: 2),
                        qtd_estoque = c.Short(),
                        qtd_pedido = c.Short(),
                        data_registro = c.DateTime(nullable: false),
                        data_esgotado = c.DateTime(),
                    })
                .PrimaryKey(t => t.id_produto)
                .ForeignKey("dbo.wbyp_fornecedor_dap", t => t.id_fornecedor)
                .Index(t => t.id_fornecedor);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.wbyp_produto_dap", "id_fornecedor", "dbo.wbyp_fornecedor_dap");
            DropIndex("dbo.wbyp_produto_dap", new[] { "id_fornecedor" });
            DropTable("dbo.wbyp_produto_dap");
            DropTable("dbo.wbyp_fornecedor_dap");
        }
    }
}
