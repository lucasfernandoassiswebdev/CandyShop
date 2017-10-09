IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[CSSP_InsPagamento]') AND objectproperty(id, N'IsPROCEDURE')=1)
	DROP PROCEDURE [dbo].[CSSP_InsPagamento]
GO

CREATE PROCEDURE [dbo].[CSSP_InsPagamento]	
	@Cpf varchar(14),  
	@ValorPagamento decimal (15,2)
	AS

	/*
	Documentação
	Arquivo Fonte.....: Pagamento.sql
	Objetivo..........: Inserir um pagamento
	Autor.............: SMN - Rafael Morais
 	Data..............: 06/0/2017
	Ex................: EXEC [dbo].[CSSP_InsPagamento] '12313546464',30
	*/
	
	BEGIN

		INSERT INTO [dbo].[Pagamento] (Cpf, DataPagamento, ValorPagamento)
			VALUES (@Cpf, GETDATE(), @ValorPagamento)			
		
		-- Somar o pagamento feito ao saldo do usuario em questão
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
	@cpf VARCHAR(11) = NULL,
	@mes INT = 0
	AS

	/*
	Documentação
	Arquivo Fonte.....: Pagamento.sql
	Objetivo..........: Listar todos os pagamentos feitos no mes atual ou infomado, por todos usuarios ou pelo passado no cpf
	Autor.............: SMN - Rafael Morais
 	Data..............: 06/09/2017
	Ex................: EXEC [dbo].[CSSP_LisPagamento] '43838601840'      LISTA OS DO CPF NO MES ATUAL
														'11111111111', 5	LISTA OS DO CPF NO MES INFORMADO
														VAZIO              LISTA NO MES ATUAL
														NULL, 5				LISTA TODOS NO MES INFORMADO - NAO ESQUECER DE PASSAR O NULL
	Editado Por.......: SMN - João Guilherme
	Objetivo..........: Alterando o select  e inserindo Inner JOin
	Data..............: 13/09/2017
	*/	
	BEGIN
		
		IF @mes = 0
		BEGIN 
			SELECT @mes = MONTH(GETDATE())
		END

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
			WHERE MONTH(p.DataPagamento) = @mes
			ORDER BY p.DataPagamento DESC
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
			WHERE (p.Cpf = @cpf) and (MONTH(p.DataPagamento) = @mes)
			ORDER BY p.DataPagamento DESC
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
	Documentação
	Arquivo Fonte.....: Pagamento.sql
	Objetivo..........: Listar os pagamentos da semana atual
	Autor.............: SMN - Rafael Morais
 	Data..............: 21/09/2017
	Ex................: EXEC [dbo].[CSSP_LisPagamentoSemana] 'cpf'		lista os pagamentos feitos na semana pelo cpf
															 vazio		lista todos os pagamentos feitos na semana por todos

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
				ORDER BY p.DataPagamento DESC
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
				ORDER BY p.DataPagamento DESC
		END
	END
GO
				

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[CSSP_ListarPagamentoDia]') AND objectproperty(id, N'IsPROCEDURE')=1)
	DROP PROCEDURE [dbo].[CSSP_ListarPagamentoDia]
GO

CREATE PROCEDURE [dbo].[CSSP_ListarPagamentoDia]
	@data date = null
	AS

	/*
	Documentação
	Arquivo Fonte.....: Pagamento.sql
	Objetivo..........: Listar todos os pagamentos feitos em uma data informada ou dia atual
	Autor.............: SMN - Rafael Morais
 	Data..............: 22/09/2017
	Ex................: EXEC [dbo].[CSSP_ListarPagamentoDia] '09/14/2017'	
	*/
	
	BEGIN
	
		IF @data is NULL
		BEGIN 
			SELECT @data = GETDATE()		
		END	
		
		SELECT p.IdPagamento,
			p.Cpf,
			u.NomeUsuario,
			p.DataPagamento,
			p.ValorPagamento
		FROM [dbo].[Pagamento] p WITH(NOLOCK)
			INNER JOIN [dbo].[Usuario] u WITH(NOLOCK)
				ON p.Cpf = u.Cpf
		WHERE cast(p.DataPagamento as date) = CAST(@data as date)
		ORDER BY p.DataPagamento DESC
		

	END
GO
				


IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[CSSP_UpdPagamento]') AND objectproperty(id, N'IsPROCEDURE')=1)
	DROP PROCEDURE [dbo].[CSSP_UpdPagamento]
GO

CREATE PROCEDURE [dbo].[CSSP_UpdPagamento]
	@IdPagamento int,		
	@ValorPagamento decimal(15,2)	

	AS

	/*
	Documentação
	Arquivo Fonte.....: Pagamento.sql
	Objetivo..........: Editar um pagamento
	Autor.............: SMN - Rafael Morais
 	Data..............: 06/09/2017
	Ex................: EXEC [dbo].[CSSP_UpdPagamento] 9, 5

	*/	

	BEGIN
		DECLARE @REVOGAR NUMERIC(15,2)
		SET @REVOGAR = (SELECT valorPagamento 
							FROM [dbo].[Pagamento]
							WHERE IdPagamento =	@IdPagamento)
		
		DECLARE @cpf VARCHAR(11)
		SET @CPF = (SELECT CPF 
						FROM [dbo].[Pagamento]
						WHERE IdPagamento =	@IdPagamento)

		UPDATE [dbo].[Usuario] 
			SET	SaldoUsuario -= @Revogar		
			WHERE Cpf = @cpf

		UPDATE [dbo].[Pagamento] 
			SET	ValorPagamento = @ValorPagamento		
			WHERE IdPagamento = @IdPagamento
			
		UPDATE [dbo].[Usuario] 
			SET SaldoUsuario += @ValorPagamento			
			WHERE Cpf = @Cpf

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
	Documentação
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
	Documentação
	Arquivo Fonte.....: Pagamento.sql
	Objetivo..........: Selecionar um pagamento
	Autor.............: SMN - Rafael Morais
 	Data..............: 07/07/2017
	Ex................: EXEC [dbo].[CSSP_SelPagamento] @IdPagamento = 1

	*/

	BEGIN
	
		SELECT	p.IdPagamento,
				u.Cpf,
				u.NomeUsuario,
				p.DataPagamento,
				p.ValorPagamento
			FROM [dbo].[Pagamento] p	WITH(NOLOCK)
				INNER JOIN [dbo].[Usuario] u 
					ON p.Cpf = u.Cpf
			WHERE IdPagamento = @IdPagamento

	END
GO


