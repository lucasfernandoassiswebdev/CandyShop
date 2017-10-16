USE CandyShop
GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[CSSP_InsCompra]') AND objectproperty(id, N'IsPROCEDURE')=1)
	DROP PROCEDURE [dbo].[CSSP_InsCompra]
GO

CREATE PROCEDURE [dbo].[CSSP_InsCompra]
	@UsuarioCompra VARCHAR(14),
	@sequencial int = 0 output

	AS

	/*
	Documentação
	Arquivo Fonte.....: Compra.sql
	Objetivo..........: Inserir uma compra
	Autor.............: SMN - Rafael Morais
 	Data..............: 06/09/2017
	Ex................: EXEC [dbo].[CSSP_InsCompra] '43838601840'	
	*/					EXEC [dbo].[CSSP_LisCompra]

	BEGIN
		INSERT INTO [dbo].[Compra] (UsuarioCompra, DataCompra, ValorCompra)
			VALUES(@UsuarioCompra, GETDATE(), 0)	

		SET @sequencial = SCOPE_IDENTITY()
		
		if @@ERROR <> 0 
			RETURN -1

		RETURN 0	
	END
GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[CSSP_UpdCompra]') AND objectproperty(id, N'IsPROCEDURE')=1)
	DROP PROCEDURE [dbo].[CSSP_UpdCompra]
GO

CREATE PROCEDURE [dbo].[CSSP_UpdCompra]
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
	Ex................: EXEC [dbo].[CSSP_UpdCompra]

	*/

	BEGIN	
		UPDATE [dbo].[Compra] 
			SET UsuarioCompra = @UsuarioCompra,			
				DataCompra = @DataCompra
			WHERE IdCompra = @IdCompra			
			
	
			if @@ERROR <> 0 
				RETURN 1
		RETURN 0

	END
GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[CSSP_LisCompra]') AND objectproperty(id, N'IsPROCEDURE')=1)
	DROP PROCEDURE [dbo].[CSSP_LisCompra]
GO

CREATE PROCEDURE [dbo].[CSSP_LisCompra]	
	@mes INT = 0
	AS

	/*
	Documentação
	Arquivo Fonte.....: Compra.sql
	Objetivo..........: Listar todas as compras feitas 
	Autor.............: SMN - Rafael Morais
 	Data..............: 06/09/2017
	Ex................: EXEC [dbo].[CSSP_LisCompra]
	
	Editado Por.......: SMN - João Guilherme
	Objetivo..........: Alterando o select 
	Data..............: 14/09/2017

	*/
	BEGIN
		IF @mes = 0
		BEGIN 
			SELECT @mes = MONTH(GETDATE())
		END				
		BEGIN
			SELECT	c.IdCompra,
					c.UsuarioCompra ,
					u.NomeUsuario as 'NomeUsuario',
					c.DataCompra,
					c.ValorCompra
			 FROM [dbo].[Compra] c WITH(NOLOCK)
				INNER JOIN Usuario u 
					ON u.Cpf = c.UsuarioCompra
			WHERE MONTH(c.DataCompra) = @mes
			ORDER BY c.DataCompra DESC
		END		
	END
GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[CSSP_LisCompraSemana]') AND objectproperty(id, N'IsPROCEDURE')=1)
	DROP PROCEDURE [dbo].[CSSP_LisCompraSemana]
GO

CREATE PROCEDURE [dbo].[CSSP_LisCompraSemana]
	@cpf VARCHAR(11) = NULL
	AS

	/*
	Documentação
	Arquivo Fonte.....: Compra.sql
	Objetivo..........: Listar todas as compras da semana, ou as compra da semana de um usuario
	Autor.............: SMN - Rafael Morais
 	Data..............: 26/09/2017
	Ex................: EXEC [dbo].[CSSP_LisCompraSemana]

	*/

	BEGIN
		DECLARE @domingo AS DATETIME = GETDATE();
		WHILE ((SELECT DATENAME(weekday, @domingo)) <> 'sunday')
		BEGIN
			IF ((SELECT DATENAME(weekday, @domingo)) <> 'sunday')
			BEGIN				
				SELECT @domingo = DATEADD(DAY, -1, @domingo)
			END
		END
		IF @cpf IS NULL
		BEGIN
			SELECT	c.IdCompra,
					c.UsuarioCompra ,
					u.NomeUsuario as 'NomeUsuario',
					c.DataCompra,
					c.ValorCompra 
			 FROM [dbo].[Compra] c WITH(NOLOCK)
				INNER JOIN Usuario u 
					ON u.Cpf = c.UsuarioCompra
			WHERE c.DataCompra > @domingo
			ORDER BY c.DataCompra DESC
		END
		ELSE
		BEGIN
			SELECT	c.IdCompra,
					c.UsuarioCompra ,
					u.NomeUsuario as 'NomeUsuario',
					c.DataCompra,
					c.ValorCompra 
			 FROM [dbo].[Compra] c WITH(NOLOCK)
				INNER JOIN Usuario u 
					ON u.Cpf = c.UsuarioCompra
			WHERE c.DataCompra > @domingo and c.UsuarioCompra = @cpf
			ORDER BY c.DataCompra DESC
		END
	END
GO
				
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[CSSP_LisCompraDia]') AND objectproperty(id, N'IsPROCEDURE')=1)
	DROP PROCEDURE [dbo].[CSSP_LisCompraDia]
GO

CREATE PROCEDURE [dbo].[CSSP_LisCompraDia]
	@data date = null
	AS

	/*
	Documentação
	Arquivo Fonte.....: Compra.sql
	Objetivo..........: Listar todas as compras feitas em uma data informada, se nao informar a data, retorna do dia atual
	Autor.............: SMN - Rafael Morais
 	Data..............: 26/09/2017
	Ex................: EXEC [dbo].[CSSP_LisCompraDia]

	*/

	BEGIN
		IF @data is NULL
		BEGIN 
			SELECT @data = GETDATE()		
		END	
		SELECT	c.IdCompra,
					c.UsuarioCompra ,
					u.NomeUsuario as 'NomeUsuario',
					c.DataCompra,
					c.ValorCompra 
			 FROM [dbo].[Compra] c WITH(NOLOCK)
				INNER JOIN Usuario u 
					ON u.Cpf = c.UsuarioCompra
		WHERE CAST(c.DataCompra AS DATE) = CAST(@data AS DATE)
		ORDER BY c.DataCompra DESC
	END
GO
				
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[CSSP_LisCpfCompra]') AND objectproperty(id, N'IsPROCEDURE')=1)
	DROP PROCEDURE [dbo].[CSSP_LisCpfCompra]
GO

CREATE PROCEDURE [dbo].[CSSP_LisCpfCompra]
	@Cpf varchar(14)
	
	AS
	
	/*
	Documentação
	Arquivo Fonte.....: Compra.sql
	Objetivo..........: Listar as compras feitas por um usuario
	Autor.............: SMN - Rafael Morais
 	Data..............: 07/07/2017
	Ex................: EXEC [dbo].[CSSP_LisCpfCompra] '43838601840'

	*/

	BEGIN
	
		SELECT	c.IdCompra,
				c.DataCompra,
				c.ValorCompra,
				c.UsuarioCompra,
				u.NomeUsuario as 'NomeUsuario'				
		 FROM [dbo].[Compra] c WITH(NOLOCK)
			INNER JOIN Usuario u on u.Cpf = c.UsuarioCompra
		 WHERE c.UsuarioCompra = @Cpf
		 ORDER BY c.DataCompra DESC
	END
GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[CSSP_DelCompra]') AND objectproperty(id, N'IsPROCEDURE')=1)
	DROP PROCEDURE [dbo].[CSSP_DelCompra]
GO

--temporariamente inutilizada
CREATE PROCEDURE [dbo].[CSSP_DelCompra]
	@IdCompra INT

	AS

	/*
	Documentação
	Arquivo Fonte.....: Compra.sql
	Objetivo..........: Deletar uma compra
	Autor.............: SMN - Rafael Morais
 	Data..............: 06/09/2017
	Ex................: EXEC [dbo].[CSSP_DelCompra]

	*/

	BEGIN
	
		DELETE FROM [dbo].[Compra] WHERE
			IdCompra = @IdCompra
	END
GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[CSSP_SelCompra]') AND objectproperty(id, N'IsPROCEDURE')=1)
	DROP PROCEDURE [dbo].[CSSP_SelCompra]
GO

CREATE PROCEDURE [dbo].[CSSP_SelCompra]
	@IdCompra int
	
	AS

	/*
	Documentação
	Arquivo Fonte.....: Compra.sql
	Objetivo..........: Selecionar uma compra
	Autor.............: SMN - Rafael Morais
 	Data..............: 07/07/2017
	Ex................: EXEC [dbo].[CSSP_SelCompra]

	*/

	BEGIN
		SELECT TOP 1 1 
		 FROM [dbo].[Compra] WITH(NOLOCK)
		 WHERE IdCompra = @IdCompra
	END
GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[CSSP_SelDadosCompra]') AND objectproperty(id, N'IsPROCEDURE')=1)
	DROP PROCEDURE [dbo].[CSSP_SelDadosCompra]
GO

CREATE PROCEDURE [dbo].[CSSP_SelDadosCompra]
	@IdCompra int	
	AS

	/*
	Documentação
	Arquivo Fonte.....: Compra.sql
	Objetivo..........: Selecionar dados de uma compra específica
	Autor.............: SMN - Rafael Morais
 	Data..............: 28/09/2017
	Ex................: EXEC [dbo].[CSSP_SelDadosCompra] 21
	*/
	BEGIN	
		SELECT  c.IdCompra,
				c.UsuarioCompra,
				c.DataCompra,
				c.ValorCompra,
				u.NomeUsuario,
				u.Classificacao 
			FROM [dbo].[Compra] c WITH(NOLOCK)
				INNER JOIN [dbo].[Usuario] u 
					ON c.UsuarioCompra = u.Cpf
			WHERE IdCompra = @IdCompra
	END
GO
				
		
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[CSSP_LisCompraNomeUsuario]') AND objectproperty(id, N'IsPROCEDURE')=1)
	DROP PROCEDURE [dbo].[CSSP_LisCompraNomeUsuario]
GO

CREATE PROCEDURE [dbo].[CSSP_LisCompraNomeUsuario]
	@Nome varchar(50)
	AS

	/*
	Documentação
	Arquivo Fonte.....: Compra.sql
	Objetivo..........: Encontrar as compras que um usuário fez pelo seu nome
	Autor.............: SMN - Lucas Fernando
 	Data..............: 14/09/2017
	Ex................: EXEC [dbo].[CSSP_LisCompraNomeUsuario]

	*/

	BEGIN
		SELECT	c.IdCompra,
				c.UsuarioCompra as 'NomeUsuario',
				u.NomeUsuario,
				c.DataCompra 
		 FROM [dbo].[Compra] c WITH(NOLOCK)
		 INNER JOIN Usuario u on u.Cpf = c.UsuarioCompra
		 WHERE u.NomeUsuario = '%' + @Nome + '%' 
	END


IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[CSSP_SelLastCompra]') AND objectproperty(id, N'IsPROCEDURE')=1)
	DROP PROCEDURE [dbo].[CSSP_SelLastCompra]
GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[CSSP_SelLastCompra]') AND objectproperty(id, N'IsPROCEDURE')=1)
	DROP PROCEDURE [dbo].[CSSP_SelLastCompra]
GO
CREATE PROCEDURE [dbo].[CSSP_SelLastCompra]

	AS

	/*
	Documentação
	Arquivo Fonte.....: Compra.sql
	Objetivo..........: Retornar a última compra inserida
	Autor.............: SMN - Lucas Fernando
 	Data..............: 26/09/2017
	Ex................: EXEC [dbo].[GCS_SelLastCompra]

	*/

	BEGIN
		SELECT  IDENT_CURRENT('Produto') as 'Item' 
	END
GO



																			