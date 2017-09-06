USE CandyShop


IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[GCS_InsCompraProduto]') AND objectproperty(id, N'IsPROCEDURE')=1)
	DROP PROCEDURE [dbo].[GCS_InsCompraProduto]
GO

CREATE PROCEDURE [dbo].[GCS_InsCompraProduto]
	@IdProduto int,
	@IdCompra int,
	@QtdeProduto int

	AS

	/*
	Documentação
	Arquivo Fonte.....: CompraProduto.sql
	Objetivo..........: Inserir um produto e a quantidade dele numa venda.
	Autor.............: SMN - Rafael Morais
 	Data..............: 06/09/2017
	Ex................: EXEC [dbo].[GCS_InsCompraProduto]

	*/

	BEGIN	
		INSERT INTO CompraProduto (IdProduto, IdCompra, QtdeProduto)
			VALUES (@IdProduto, @IdCompra, @QtdeProduto)
			
			if @@ERROR <> 0 
				RETURN 1
		RETURN 0
	END
GO


IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[GCS_UpdCompraProduto]') AND objectproperty(id, N'IsPROCEDURE')=1)
	DROP PROCEDURE [dbo].[GCS_UpdCompraProduto]
GO

CREATE PROCEDURE [dbo].[GCS_UpdCompraProduto]
	@IdCompra int,
	@IdProduto int, 
	@QtdeProduto int

	AS

	/*
	Documentação
	Arquivo Fonte.....: ArquivoFonte.sql
	Objetivo..........: Objetivo
	Autor.............: SMN - Rafael Morais
 	Data..............: 01/01/2017
	Ex................: EXEC [dbo].[GCS_UpdCompraProduto]

	*/

	BEGIN
		
		UPDATE [dbo].[CompraProduto] SET				
			IdProduto = @IdProduto,
			QtdeProduto = @QtdeProduto
 			
			WHERE IdCompra = @IdCompra

			if @@ERROR <> 0 
				RETURN 1
		RETURN 0

	END
GO


IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[GCS_DelCompraProduto]') AND objectproperty(id, N'IsPROCEDURE')=1)
	DROP PROCEDURE [dbo].[GCS_DelCompraProduto]
GO

CREATE PROCEDURE [dbo].[GCS_DelCompraProduto]
	@IdCompra int,
	@IdProduto int

	AS

	/*
	Documentação
	Arquivo Fonte.....: CompraProduto.sql
	Objetivo..........: Excluir um item da compra
	Autor.............: SMN - Rafael Morais
 	Data..............: 06/09/2017
	Ex................: EXEC [dbo].[GCS_DelCompraProduto]

	*/

	BEGIN
		DELETE CompraProduto WHERE
			IdCompra = @IdCompra and IdProduto = @IdProduto		
	END
GO
								

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[GCS_LisCompraProduto]') AND objectproperty(id, N'IsPROCEDURE')=1)
	DROP PROCEDURE [dbo].[GCS_LisCompraProduto]
GO

CREATE PROCEDURE [dbo].[GCS_LisCompraProduto]
	@IdCompra int

	AS

	/*
	Documentação
	Arquivo Fonte.....: Lista.sql
	Objetivo..........: Listar produtos de uma compra específica
	Autor.............: SMN - Rafael Morais
 	Data..............: 01/01/2017
	Ex................: EXEC [dbo].[GCS_LisCompraProduto]

	*/

	BEGIN
		SELECT * FROM CompraProduto WITH(NOLOCK)
			WHERE IdCompra = @Idcompra		

	END
GO
								