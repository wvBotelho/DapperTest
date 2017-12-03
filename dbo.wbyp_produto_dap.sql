CREATE TABLE [dbo].[wbyp_produto_dap] (
    [id_produto]    INT             IDENTITY (1, 1) NOT NULL,
    [nome]          NVARCHAR (60)   NOT NULL,
    [preco]         DECIMAL (18, 2) NOT NULL,
    [qtd_estoque]   SMALLINT        NULL,
    [qtd_pedido]    SMALLINT        NULL,
    CONSTRAINT [PK_dbo.wbyp_produto_dap] PRIMARY KEY CLUSTERED ([id_produto] ASC)
);

GO

