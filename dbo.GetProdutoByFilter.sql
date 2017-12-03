CREATE PROCEDURE GetProdutoByFilter
	@FornecedorID integer,
	@Nome nvarchar(60),
	@DataRegistro Datetime,
	@DataEsgotado Datetime
AS
DECLARE @sql_statement nvarchar(4000)
DECLARE @sql_where nvarchar(4000)

BEGIN
IF (@FornecedorID IS NOT NULL)
BEGIN 
select * from dbo.wbyp_produto_dap where id_fornecedor = @FornecedorID;
END

IF (@Nome IS NOT NULL)
BEGIN
select * from dbo.wbyp_produto_dap where nome = @Nome;
END

IF (@DataRegistro IS NOT NULL)
BEGIN
select * from dbo.wbyp_produto_dap 
where data_registro = @DataRegistro;
END

IF (@DataEsgotado IS NOT NULL)
BEGIN
select * from dbo.wbyp_produto_dap 
where data_esgotado = @DataEsgotado;
END

END