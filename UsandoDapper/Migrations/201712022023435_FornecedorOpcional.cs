namespace UsandoDapper.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FornecedorOpcional : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.wbyp_produto_dap", new[] { "id_fornecedor" });
            AlterColumn("dbo.wbyp_produto_dap", "id_fornecedor", c => c.Int());
            CreateIndex("dbo.wbyp_produto_dap", "id_fornecedor");
        }
        
        public override void Down()
        {
            DropIndex("dbo.wbyp_produto_dap", new[] { "id_fornecedor" });
            AlterColumn("dbo.wbyp_produto_dap", "id_fornecedor", c => c.Int(nullable: false));
            CreateIndex("dbo.wbyp_produto_dap", "id_fornecedor");
        }
    }
}
