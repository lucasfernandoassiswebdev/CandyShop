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
		SELECT	c.IdCompra,
				c.UsuarioCompra ,
				u.NomeUsuario as 'nomeUsuario',
				c.DataCompra 
		 FROM [dbo].[Compra] c WITH(NOLOCK)
		 INNER JOIN Usuario u on u.Cpf = c.UsuarioCompra
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
	
		SELECT	c.IdCompra,
				c.UsuarioCompra as 'nomeUsuario',
				u.NomeUsuario,
				c.DataCompra 
		 FROM [dbo].[Compra] c WITH(NOLOCK)
		 INNER JOIN Usuario u on u.Cpf = c.UsuarioCompra
		 WHERE c.UsuarioCompra = @Cpf
	END
GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[GCS_DelCompra]') AND objectproperty(id, N'IsPROCEDURE')=1)
	DROP PROCEDURE [dbo].[GCS_DelCompra]
GO

--temporariamente inutilizada
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
		SELECT TOP 1 1 
		 FROM [dbo].[Compra] WITH(NOLOCK)
		 WHERE IdCompra = @IdCompra
	END
GO
		

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[GCS_LisCompraNomeUsuario]') AND objectproperty(id, N'IsPROCEDURE')=1)
	DROP PROCEDURE [dbo].[GCS_LisCompraNomeUsuario]
GO

CREATE PROCEDURE [dbo].[GCS_LisCompraNomeUsuario]
	@Nome varchar(50)
	AS

	/*
	Documentação
	Arquivo Fonte.....: Compra.sql
	Objetivo..........: Encontrar as compras que um usuário fez pelo seu nome
	Autor.............: SMN - Lucas Fernando
 	Data..............: 14/09/2017
	Ex................: EXEC [dbo].[GCS_LisCompraNomeUsuario]

	*/

	BEGIN
		SELECT	c.IdCompra,
				c.UsuarioCompra as 'nomeUsuario',
				u.NomeUsuario,
				c.DataCompra 
		 FROM [dbo].[Compra] c WITH(NOLOCK)
		 INNER JOIN Usuario u on u.Cpf = c.UsuarioCompra
		 WHERE u.NomeUsuario = '%' + @Nome + '%' 
	END
GO

select * from Compra
select * from CompraProduto

select cp.IdProduto, p.NomeProduto, p.PrecoProduto, c.IdCompra, cp.QtdeProduto from Produto p
inner join CompraProduto cp on cp.IdProduto = p.IdProduto
inner join Compra c on c.IdCompra = cp.IdCompra 
								
																			