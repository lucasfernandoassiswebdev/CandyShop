
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[CSSP_InsPagamento]') AND objectproperty(id, N'IsPROCEDURE')=1)
	DROP PROCEDURE [dbo].[CSSP_InsPagamento]
GO

CREATE PROCEDURE [dbo].[CSSP_InsPagamento]	
	@Cpf varchar(14),
	@DataPagamento datetime,  -- como a data sera setada aqui no sql, passar do vs pra ca a data como null
	@ValorPagamento decimal

	AS

	/*
	Documenta��o
	Arquivo Fonte.....: Pagamento.sql
	Objetivo..........: Inserir um pagamento
	Autor.............: SMN - Rafael Morais
 	Data..............: 06/0/2017
	Ex................: EXEC [dbo].[CSSP_InsPagamento] '12313546464', '09/14/2017',20
	*/
	
	BEGIN

		IF @DataPagamento IS NULL
		SET @DataPagamento = GETDATE()

		INSERT INTO [dbo].[Pagamento] (Cpf, DataPagamento, ValorPagamento)
			VALUES (@Cpf, @DataPagamento, @ValorPagamento)			
		
		-- Somar o pagamento feito ao saldo do usuario em quest�o
		UPDATE [dbo].[Usuario] 
			SET SaldoUsuario += @ValorPagamento			
			WHERE Cpf = @Cpf
		
			IF @@ERROR <> 0 
				RETURN 1
		RETURN 0
		
	END
GO


IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[CSSP_LisPagamento]') AND objectproperty(id, N'IsPROCEDURE')=1)
	DROP PROCEDURE [dbo].[CSSP_LisPagamento]
GO

CREATE PROCEDURE [dbo].[CSSP_LisPagamento]
	@cpf VARCHAR(11) = NULL 
	AS

	/*
	Documenta��o
	Arquivo Fonte.....: Pagamento.sql
	Objetivo..........: Listar todos os pagamentos feitos por todos usuarios ou pelo passado no cpf
	Autor.............: SMN - Rafael Morais
 	Data..............: 06/09/2017
	Ex................: EXEC [dbo].[CSSP_LisPagamento] '11111111111'

	Editado Por.......: SMN - Jo�o Guilherme
	Objetivo..........: Alterando o select  e inserindo Inner JOin
	Data..............: 13/09/2017
	*/

	BEGIN
	
		IF @CPF IS NULL
		BEGIN
			SELECT p.IdPagamento,
				p.Cpf,
				u.NomeUsuario,
				p.DataPagamento,
				p.ValorPagamento
			FROM [dbo].[Pagamento] p WITH(NOLOCK)
				INNER JOIN [dbo].[Usuario] u WITH(NOLOCK)
					ON p.Cpf = u.Cpf
		END
		ELSE
		BEGIN
			SELECT p.IdPagamento,
				p.Cpf,
				u.NomeUsuario,
				p.DataPagamento,
				p.ValorPagamento
			FROM [dbo].[Pagamento] p WITH(NOLOCK)
				INNER JOIN [dbo].[Usuario] u WITH(NOLOCK)
					ON p.Cpf = u.Cpf
			WHERE p.Cpf = @cpf
		END
		
	END
GO


IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[CSSP_LisPagamentoSemana]') AND objectproperty(id, N'IsPROCEDURE')=1)
	DROP PROCEDURE [dbo].[CSSP_LisPagamentoSemana]
GO

CREATE PROCEDURE [dbo].[CSSP_LisPagamentoSemana]
	@cpf VARCHAR(11) = NULL
	AS

	/*
	Documenta��o
	Arquivo Fonte.....: Pagamento.sql
	Objetivo..........: Listar os pagamentos da semana atual
	Autor.............: SMN - Rafael Morais
 	Data..............: 21/09/2017
	Ex................: EXEC [dbo].[CSSP_LisPagamentoSemana]

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
			SELECT p.IdPagamento,
					p.Cpf,
					u.NomeUsuario,
					p.DataPagamento,
					p.ValorPagamento
				FROM [dbo].[Pagamento] p WITH(NOLOCK)
					INNER JOIN [dbo].[Usuario] u WITH(NOLOCK)
						ON p.Cpf = u.Cpf
				WHERE p.DataPagamento > @domingo
		END
		ELSE
		BEGIN 
			SELECT p.IdPagamento,
					p.Cpf,
					u.NomeUsuario,
					p.DataPagamento,
					p.ValorPagamento
				FROM [dbo].[Pagamento] p WITH(NOLOCK)
					INNER JOIN [dbo].[Usuario] u WITH(NOLOCK)
						ON p.Cpf = u.Cpf
				WHERE p.Cpf = @cpf and p.DataPagamento > @domingo
		END
	END
GO
				

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[CSSP_UpdPagamento]') AND objectproperty(id, N'IsPROCEDURE')=1)
	DROP PROCEDURE [dbo].[CSSP_UpdPagamento]
GO

CREATE PROCEDURE [dbo].[CSSP_UpdPagamento]
	@IdPagamento int,	
	@DataPagamento datetime,
	@ValorPagamento decimal

	AS

	/*
	Documenta��o
	Arquivo Fonte.....: Pagamento.sql
	Objetivo..........: Editar um pagamento
	Autor.............: SMN - Rafael Morais
 	Data..............: 06/09/2017
	Ex................: EXEC [dbo].[CSSP_UpdPagamento]

	*/

	BEGIN
	
		UPDATE [dbo].[Pagamento] 
			SET	DataPagamento = @DataPagamento,
				ValorPagamento = @ValorPagamento		
				WHERE IdPagamento = @IdPagamento
			
			IF @@ERROR <> 0 
				RETURN 1
		RETURN 0
	END
GO
		

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[CSSP_DelPagamento]') AND objectproperty(id, N'IsPROCEDURE')=1)
	DROP PROCEDURE [dbo].[CSSP_DelPagamento]
GO

CREATE PROCEDURE [dbo].[CSSP_DelPagamento]
	@IdPagamento int

	AS

	/*
	Documenta��o
	Arquivo Fonte.....: Pagamento.sql
	Objetivo..........: Deletar um pagamento
	Autor.............: SMN - Rafael Morais
 	Data..............: 06/09/2017
	Ex................: EXEC [dbo].[CSSP_DelPagamento]

	*/

	BEGIN
	
		DELETE [dbo].[Pagamento] 
			WHERE IdPagamento = @IdPagamento

	END
GO


IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[CSSP_SelPagamento]') AND objectproperty(id, N'IsPROCEDURE')=1)
	DROP PROCEDURE [dbo].[CSSP_SelPagamento]
GO

CREATE PROCEDURE [dbo].[CSSP_SelPagamento]
	@IdPagamento int

	AS

	/*
	Documenta��o
	Arquivo Fonte.....: Pagamento.sql
	Objetivo..........: Selecionar um pagamento
	Autor.............: SMN - Rafael Morais
 	Data..............: 07/07/2017
	Ex................: EXEC [dbo].[CSSP_SelPagamento]

	*/

	BEGIN
	
		SELECT * FROM [dbo].[Pagamento]	WITH(NOLOCK)
			WHERE IdPagamento = @IdPagamento

	END
GO
													