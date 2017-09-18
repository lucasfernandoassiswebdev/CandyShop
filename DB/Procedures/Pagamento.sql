
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[CSSP_InsPagamento]') AND objectproperty(id, N'IsPROCEDURE')=1)
	DROP PROCEDURE [dbo].[CSSP_InsPagamento]
GO

CREATE PROCEDURE [dbo].[CSSP_InsPagamento]	
	@Cpf varchar(14),
	@DataPagamento datetime,
	@ValorPagamento decimal

	AS

	/*
	Documentação
	Arquivo Fonte.....: Pagamento.sql
	Objetivo..........: Inserir um pagamento
	Autor.............: SMN - Rafael Morais
 	Data..............: 06/0/2017
	Ex................: EXEC [dbo].[CSSP_InsPagamento]

	*/

	BEGIN
		INSERT INTO [dbo].[Pagamento] (Cpf, DataPagamento, ValorPagamento)
			VALUES (@Cpf, @DataPagamento, @ValorPagamento)			
		
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

	AS

	/*
	Documentação
	Arquivo Fonte.....: Pagamento.sql
	Objetivo..........: Listar todos os pagamentos feitos por todos usuarios
	Autor.............: SMN - Rafael Morais
 	Data..............: 06/09/2017
	Ex................: EXEC [dbo].[CSSP_LisPagamento]

	Editado Por.......: SMN - João Guilherme
	Objetivo..........: Alterando o select  e inserindo Inner JOin
	Data..............: 13/09/2017
	*/

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
	Documentação
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


IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[CSSP_LisCpfPagamento]') AND objectproperty(id, N'IsPROCEDURE')=1)
	DROP PROCEDURE [dbo].[CSSP_LisCpfPagamento]
GO

CREATE PROCEDURE [dbo].[CSSP_LisCpfPagamento]
	@Cpf VARCHAR(14)
	AS

	/*
	Documentação
	Arquivo Fonte.....: Pagamento.sql
	Objetivo..........: Listar os pagamentos de um usuario
	Autor.............: SMN - Rafael Morais
 	Data..............: 06/09/2017
	Ex................: EXEC [dbo].[CSSP_LisCpfPagamento]

	*/

	BEGIN
	
		SELECT * FROM [dbo].[Pagamento] WITH(NOLOCK)
			WHERE Cpf = @Cpf

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
	Ex................: EXEC [dbo].[CSSP_SelPagamento]

	*/

	BEGIN
	
		SELECT * FROM [dbo].[Pagamento]	WITH(NOLOCK)
			WHERE IdPagamento = @IdPagamento

	END
GO
													