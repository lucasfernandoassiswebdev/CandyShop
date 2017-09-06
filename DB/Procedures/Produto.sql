
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[GCS_InsProduto]') AND objectproperty(id, N'IsPROCEDURE')=1)
	DROP PROCEDURE [dbo].[GCS_InsProduto]
GO

CREATE PROCEDURE [dbo].[GCS_InsProduto]
	@NomeProduto varchar(40),
	@PrecoProduto decimal,
	@QtdeProduto int
	AS

	/*
	Documentação
	Arquivo Fonte.....: Produto.sql
	Objetivo..........: Inserir Produtos
	Autor.............: SMN - João Guilherme
 	Data..............: 06/09/2017
	Ex................: EXEC [dbo].[GCS_InsProduto]

	*/

	BEGIN
	
		INSERT INTO [dbo].[Produto] (NomeProduto,PrecoProduto,QtdeProduto)
			VALUES (@NomeProduto,@PrecoProduto,@QtdeProduto)
	END
GO


IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[GCS_DelProduto]') AND objectproperty(id, N'IsPROCEDURE')=1)
	DROP PROCEDURE [dbo].[GCS_DelProduto]
GO

CREATE PROCEDURE [dbo].[GCS_DelProduto]

	AS

	/*
	Documentação
	Arquivo Fonte.....: Produto.sql
	Objetivo..........: Deletar Produtos 
	Autor.............: SMN - João Guilherme
 	Data..............: 01/01/2017
	Ex................: EXEC [dbo].[GKSSP_DelProduto]

	*/

	BEGIN
	
		


	END
GO




				

