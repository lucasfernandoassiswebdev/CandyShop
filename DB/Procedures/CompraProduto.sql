USE CandyShop

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[CSSP_InsCompraProduto]') AND objectproperty(id, N'IsPROCEDURE')=1)
	DROP PROCEDURE [dbo].[CSSP_InsCompraProduto]
GO

CREATE PROCEDURE [dbo].[CSSP_InsCompraProduto]
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
	Ex................: EXEC [dbo].[CSSP_InsCompraProduto]

	*/

	BEGIN	
		INSERT INTO CompraProduto (IdProduto, IdCompra, QtdeProduto)
			VALUES (@IdProduto, @IdCompra, @QtdeProduto)

			DECLARE @PrecoProduto decimal
			DECLARE @Cpf varchar(11)

			UPDATE [dbo].[Compra]
				SET ValorCompra += (@QtdeProduto * @PrecoProduto)
					WHERE IdCompra = @IdCompra
					
			UPDATE [dbo].[Usuario]
				SET SaldoUsuario -= (@QtdeProduto * @PrecoProduto)
					WHERE Cpf = @Cpf

			if @@ERROR <> 0 
				RETURN 1
		RETURN 0
	END
GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[CSSP_UpdCompraProduto]') AND objectproperty(id, N'IsPROCEDURE')=1)
	DROP PROCEDURE [dbo].[CSSP_UpdCompraProduto]
GO

CREATE PROCEDURE [dbo].[CSSP_UpdCompraProduto]
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
	Ex................: EXEC [dbo].[CSSP_UpdCompraProduto]

	*/

	BEGIN
		
		UPDATE [dbo].[CompraProduto] 
			SET IdProduto = @IdProduto,
				QtdeProduto = @QtdeProduto
 			
			WHERE IdCompra = @IdCompra

			if @@ERROR <> 0 
				RETURN 1
		RETURN 0

	END
GO

--temporariamente inutilizada
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[CSSP_DelCompraProduto]') AND objectproperty(id, N'IsPROCEDURE')=1)
	DROP PROCEDURE [dbo].[CSSP_DelCompraProduto]
GO

CREATE PROCEDURE [dbo].[CSSP_DelCompraProduto]
	@IdCompra int,
	@IdProduto int

	AS

	/*
	Documentação
	Arquivo Fonte.....: CompraProduto.sql
	Objetivo..........: Excluir um item da compra
	Autor.............: SMN - Rafael Morais
 	Data..............: 06/09/2017
	Ex................: EXEC [dbo].[CSSP_DelCompraProduto]

	*/

	BEGIN
		DELETE CompraProduto WHERE
			IdCompra = @IdCompra and IdProduto = @IdProduto		
	END
GO
								

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[CSSP_LisCompraProduto]') AND objectproperty(id, N'IsPROCEDURE')=1)
	DROP PROCEDURE [dbo].[CSSP_LisCompraProduto]
GO

CREATE PROCEDURE [dbo].[CSSP_LisCompraProduto]
	
	AS

	/*
	Documentação
	Arquivo Fonte.....: Lista.sql
	Objetivo..........: Listar produtos de uma compra específica
	Autor.............: SMN - Rafael Morais
 	Data..............: 01/01/2017
	Ex................: EXEC [dbo].[CSSP_LisCompraProduto]

	Editado Por.......: SMN - João Guilherme
	Objetivo..........: Alterando o select 
	Data..............: 12/09/2017
	*/

	BEGIN
		SELECT	cp.IdCompra,
				cp.QtdeProduto,
				p.IdProduto,
				p.NomeProduto,
				p.PrecoProduto,
				p.Ativo
		 FROM CompraProduto cp WITH(NOLOCK)
		 INNER JOIN Produto p on p.IdProduto = cp.IdProduto
	END
GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[CSSP_LisCompraProdutoIdVenda]') AND objectproperty(id, N'IsPROCEDURE')=1)
	DROP PROCEDURE [dbo].CSSP_LisCompraProdutoIdVenda
GO

CREATE PROCEDURE [dbo].CSSP_LisCompraProdutoIdVenda
	@IdCompra int

	AS

	/*
	Documentação
	Arquivo Fonte.....: Lista.sql
	Objetivo..........: Listar produtos de uma compra específica
	Autor.............: SMN - Rafael Morais
 	Data..............: 01/01/2017
	Ex................: EXEC [dbo].[CSSP_LisCompraProduto]

	Editado Por.......: SMN - João Guilherme
	Objetivo..........: Alterando o select 
	Data..............: 12/09/2017
	*/

	BEGIN
		SELECT	cp.IdCompra,
				cp.QtdeProduto,
				p.IdProduto,
				p.NomeProduto,
				p.PrecoProduto,
				p.Ativo
		 FROM CompraProduto cp WITH(NOLOCK)
		 INNER JOIN Produto p on p.IdProduto = cp.IdProduto
		 WHERE cp.IdCompra = @IdCompra
	END
GO

