
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[GCS_InsPagamento]') AND objectproperty(id, N'IsPROCEDURE')=1)
	DROP PROCEDURE [dbo].[GCS_InsPagamento]
GO

CREATE PROCEDURE [dbo].[GCS_InsPagamento]	
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
	Ex................: EXEC [dbo].[GCS_InsPagamento]

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



IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[GCS_LisPagamento]') AND objectproperty(id, N'IsPROCEDURE')=1)
	DROP PROCEDURE [dbo].[GCS_LisPagamento]
GO

CREATE PROCEDURE [dbo].[GCS_LisPagamento]

	AS

	/*
	Documentação
	Arquivo Fonte.....: Pagamento.sql
	Objetivo..........: Listar todos os pagamentos feitos por todos usuarios
	Autor.............: SMN - Rafael Morais
 	Data..............: 06/09/2017
	Ex................: EXEC [dbo].[GCS_LisPagamento]

	Editado Por.......: SMN - João Guilherme
	Objetivo..........: Alterando o select 
	Data..............: 13/09/2017
	*/

	BEGIN
	
		SELECT IdPagamento,
				Cpf,
				DataPagamento,
				ValorPagamento
				FROM [dbo].[Pagamento] WITH(NOLOCK)
	END
GO


IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[GCS_UpdPagamento]') AND objectproperty(id, N'IsPROCEDURE')=1)
	DROP PROCEDURE [dbo].[GCS_UpdPagamento]
GO

CREATE PROCEDURE [dbo].[GCS_UpdPagamento]
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
	Ex................: EXEC [dbo].[GCS_UpdPagamento]

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


IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[GCS_LisCpfPagamento]') AND objectproperty(id, N'IsPROCEDURE')=1)
	DROP PROCEDURE [dbo].[GCS_LisCpfPagamento]
GO

CREATE PROCEDURE [dbo].[GCS_LisCpfPagamento]
	@Cpf VARCHAR(14)
	AS

	/*
	Documentação
	Arquivo Fonte.....: Pagamento.sql
	Objetivo..........: Listar os pagamentos de um usuario
	Autor.............: SMN - Rafael Morais
 	Data..............: 06/09/2017
	Ex................: EXEC [dbo].[GCS_LisCpfPagamento]

	*/

	BEGIN
	
		SELECT * FROM [dbo].[Pagamento] WITH(NOLOCK)
			WHERE Cpf = @Cpf

	END
GO			

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[GCS_DelPagamento]') AND objectproperty(id, N'IsPROCEDURE')=1)
	DROP PROCEDURE [dbo].[GCS_DelPagamento]
GO

CREATE PROCEDURE [dbo].[GCS_DelPagamento]
	@IdPagamento int

	AS

	/*
	Documentação
	Arquivo Fonte.....: Pagamento.sql
	Objetivo..........: Deletar um pagamento
	Autor.............: SMN - Rafael Morais
 	Data..............: 06/09/2017
	Ex................: EXEC [dbo].[GCS_DelPagamento]

	*/

	BEGIN
	
		DELETE [dbo].[Pagamento] 
			WHERE IdPagamento = @IdPagamento

	END
GO


IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[GCS_SelPagamento]') AND objectproperty(id, N'IsPROCEDURE')=1)
	DROP PROCEDURE [dbo].[GCS_SelPagamento]
GO

CREATE PROCEDURE [dbo].[GCS_SelPagamento]
	@IdPagamento int

	AS

	/*
	Documentação
	Arquivo Fonte.....: Pagamento.sql
	Objetivo..........: Selecionar um pagamento
	Autor.............: SMN - Rafael Morais
 	Data..............: 07/07/2017
	Ex................: EXEC [dbo].[GCS_SelPagamento]

	*/

	BEGIN
	
		SELECT * FROM [dbo].[Pagamento]	WITH(NOLOCK)
			WHERE IdPagamento = @IdPagamento

	END
GO
													