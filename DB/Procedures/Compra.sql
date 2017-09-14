USE CandyShop
GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[GCS_InsCompra]') AND objectproperty(id, N'IsPROCEDURE')=1)
	DROP PROCEDURE [dbo].[GCS_InsCompra]
GO

CREATE PROCEDURE [dbo].[GCS_InsCompra]
	@UsuarioCompra VARCHAR(14),
	@DataCompra DATE

	AS

	/*
	Documentação
	Arquivo Fonte.....: Compra.sql
	Objetivo..........: Inserir uma compra
	Autor.............: SMN - Rafael Morais
 	Data..............: 06/09/2017
	Ex................: EXEC [dbo].[GCS_InsCompra] 

	*/

	BEGIN
		INSERT INTO [dbo].[Compra] (UsuarioCompra, DataCompra)
			VALUES(@UsuarioCompra, @DataCompra)	
			
			if @@ERROR <> 0 
				RETURN 1
		RETURN 0	
	END
GO


IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[GCS_UpdCompra]') AND objectproperty(id, N'IsPROCEDURE')=1)
	DROP PROCEDURE [dbo].[GCS_UpdCompra]
GO

CREATE PROCEDURE [dbo].[GCS_UpdCompra]
	@UsuarioCompra VARCHAR(14),
	@IdCompra INT,
	@DataCompra DATE

	AS

	/*
	Documentação
	Arquivo Fonte.....: Compra.sql
	Objetivo..........: Editar uma compra
	Autor.............: SMN - Rafael Morais
 	Data..............: 06/09/2017
	Ex................: EXEC [dbo].[GCS_UpdCompra]

	*/

	BEGIN	
		UPDATE [dbo].[Compra] SET 
			UsuarioCompra = @UsuarioCompra,			
			DataCompra = @DataCompra
			WHERE
				IdCompra = @IdCompra
			
			if @@ERROR <> 0 
				RETURN 1
		RETURN 0
	END
GO
				

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[GCS_LisCompra]') AND objectproperty(id, N'IsPROCEDURE')=1)
	DROP PROCEDURE [dbo].[GCS_LisCompra]
GO

CREATE PROCEDURE [dbo].[GCS_LisCompra]

	AS

	/*
	Documentação
	Arquivo Fonte.....: Compra.sql
	Objetivo..........: Listar todas as compras feitas 
	Autor.............: SMN - Rafael Morais
 	Data..............: 06/09/2017
	Ex................: EXEC [dbo].[GCS_LisCompra]

	Editado Por.......: SMN - João Guilherme
	Objetivo..........: Alterando o select 
	Data..............: 14/09/2017

	*/

	BEGIN
	
		SELECT IdCompra,
				UsuarioCompra,
				DataCompra
		 FROM [dbo].[Compra] WITH(NOLOCK)

	END
GO


IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[GCS_LisCpfCompra]') AND objectproperty(id, N'IsPROCEDURE')=1)
	DROP PROCEDURE [dbo].[GCS_LisCpfCompra]
GO

CREATE PROCEDURE [dbo].[GCS_LisCpfCompra]
	@Cpf varchar(14)

	AS

	/*
	Documentação
	Arquivo Fonte.....: Compra.sql
	Objetivo..........: Listar as compras feitas por um usuario
	Autor.............: SMN - Rafael Morais
 	Data..............: 07/07/2017
	Ex................: EXEC [dbo].[GCS_LisCpfCompra]

	*/

	BEGIN
	
		SELECT * FROM [dbo].[Compra] WITH(NOLOCK)	
			WHERE UsuarioCompra = @cpf

	END
GO
				


IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[GCS_DelCompra]') AND objectproperty(id, N'IsPROCEDURE')=1)
	DROP PROCEDURE [dbo].[GCS_DelCompra]
GO

CREATE PROCEDURE [dbo].[GCS_DelCompra]
	@IdCompra INT

	AS

	/*
	Documentação
	Arquivo Fonte.....: Compra.sql
	Objetivo..........: Deletar uma compra
	Autor.............: SMN - Rafael Morais
 	Data..............: 06/09/2017
	Ex................: EXEC [dbo].[GCS_DelCompra]

	*/

	BEGIN
	
		DELETE FROM [dbo].[Compra] WHERE
			IdCompra = @IdCompra
	END
GO


IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[GCS_SelCompra]') AND objectproperty(id, N'IsPROCEDURE')=1)
	DROP PROCEDURE [dbo].[GCS_SelCompra]
GO

CREATE PROCEDURE [dbo].[GCS_SelCompra]
	@IdCompra int
	
	AS

	/*
	Documentação
	Arquivo Fonte.....: Compra.sql
	Objetivo..........: Selecionar uma compra
	Autor.............: SMN - Rafael Morais
 	Data..............: 07/07/2017
	Ex................: EXEC [dbo].[GCS_SelCompra]

	*/

	BEGIN
	
		SELECT * FROM [dbo].[Compra] WITH(NOLOCK)
			WHERE IdCompra = @IdCompra

	END
GO
				
																			