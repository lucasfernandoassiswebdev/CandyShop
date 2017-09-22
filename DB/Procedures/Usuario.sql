USE CandyShop
GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[CSSP_InsUsuario]') AND objectproperty(id, N'IsPROCEDURE')=1)
	DROP PROCEDURE [dbo].[CSSP_InsUsuario]
GO

CREATE PROCEDURE [dbo].[CSSP_InsUsuario]
	@NomeUsuario varchar(50),
	@SenhaUsuario varchar(12) = 'password',
	@SaldoUsuario decimal(18,2) = 0,
	@CpfUsuario varchar(14),
	@Ativo varchar(1) = 'A'
	AS

	/*
	Documenta��o
	Arquivo Fonte.....: Usuario.sql
	Objetivo..........: Inserir Usuarios 
	Autor.............: SMN - Jo�o Guilherme
 	Data..............: 06/09/2017
	Ex................: EXEC [dbo].[CSSP_InsUsuario]
	
	Editado Por.......: SMN - Rafael Morais
	Objetivo..........: Adicionar a o campo de cpf na proceure 
	Data..............: 07/09/2017
	
	*/
	
	BEGIN
		INSERT INTO [dbo].[Usuario](Cpf,NomeUsuario,SenhaUsuario,SaldoUsuario,Ativo)
			VALUES (@CpfUsuario,@NomeUsuario,@SenhaUsuario,@SaldoUsuario,@Ativo)		
			
				IF @@ERROR <> 0
					RETURN 1
		RETURN 0	
	END
GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[CSSP_DesUsuario]') AND objectproperty(id, N'IsPROCEDURE')=1)
	DROP PROCEDURE [dbo].[CSSP_DesUsuario]
GO

CREATE PROCEDURE [dbo].[CSSP_DesUsuario]
	@Cpf varchar(14)
	AS

	/*
	Documenta��o
	Arquivo Fonte.....: Usuario.sql
	Objetivo..........: Deletar cadatro de usuario
	Autor.............: SMN - Jo�o Guilherme
 	Data..............: 06/09/2017
	Ex................: EXEC [dbo].[CSSP_DelUsuario]

	*/

	BEGIN
		UPDATE [dbo].[Usuario] 
			SET Ativo = 'I'
			WHERE Cpf = @Cpf
			IF @@ERROR <> 0
				RETURN 1

		RETURN 0
	END
GO
		
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[CSSP_UpdUsuario]') AND objectproperty(id, N'IsPROCEDURE')=1)
	DROP PROCEDURE [dbo].[CSSP_UpdUsuario]
GO

CREATE PROCEDURE [dbo].[CSSP_UpdUsuario]
	@Cpf varchar(14),
	@NomeUsuario varchar(50),
	@SenhaUsuario varchar(12) = 'password',
	@SaldoUsuario decimal,
	@Ativo varchar(1)
	AS

	/*
	Documenta��o
	Arquivo Fonte.....: Usuario.sql
	Objetivo..........: Editar informa�oes do usuario
	Autor.............: SMN - Jo�o Guilherme
 	Data..............: 06/09/2017
	Ex................: EXEC [dbo].[CSSP_UpdUsuario]

	*/
	select * from usuario
	BEGIN
	
		UPDATE [dbo].[Usuario]
			SET NomeUsuario = @NomeUsuario,
				SenhaUsuario = @SenhaUsuario,
				SaldoUsuario = @SaldoUsuario,
				Ativo = @Ativo
				WHERE Cpf = @Cpf

				IF @@ERROR <> 0 
					RETURN 1

			RETURN 0
	END
GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[CSSP_SelUsuario]') AND objectproperty(id, N'IsPROCEDURE')=1)
	DROP PROCEDURE [dbo].[CSSP_SelUsuario]
GO

CREATE PROCEDURE [dbo].[CSSP_SelUsuario]
	@Cpf varchar(14)
	AS

	/*
	Documenta��o
	Arquivo Fonte.....: Usuario.sql
	Objetivo..........: Selecionar Usuarios
	Autor.............: SMN - Jo�o Guilherme
 	Data..............: 06/09/2017
	Ex................: EXEC [dbo].[CSSP_SelUsuario]

	*/

	BEGIN
		SELECT * FROM [dbo].[Usuario] WITH(NOLOCK)
			WHERE Cpf = @Cpf
	END
GO
				
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[CSSP_LisUsuario]') AND objectproperty(id, N'IsPROCEDURE')=1)
	DROP PROCEDURE [dbo].[CSSP_LisUsuario]
GO
	
CREATE PROCEDURE [dbo].[CSSP_LisUsuario]

	AS

	/*
	Documenta��o
	Arquivo Fonte.....: Usuario.sql
	Objetivo..........: Listar todos os usuarios ativos
	Autor.............: SMN - Rafael Morais
 	Data..............: 07/07/2017
	Ex................: EXEC [dbo].[CSSP_LisUsuario]

	Editado Por.......: SMN - Jo�o Guilherme
	Objetivo..........: Alterando o select 
	Data..............: 12/09/2017
	*/

	BEGIN
		SELECT	Cpf,
				SenhaUsuario,
				SaldoUsuario,
				NomeUsuario,
				Ativo
				FROM [dbo].[Usuario]
				WHERE Ativo = 'A'
	END
GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[CSSP_ListarUsuariosInativos]') AND objectproperty(id, N'IsPROCEDURE')=1)
	DROP PROCEDURE [dbo].[CSSP_ListarUsuariosInativos]
GO

CREATE PROCEDURE [dbo].[CSSP_ListarUsuariosInativos]

	AS

	/*
	Documenta��o
	Arquivo Fonte.....: Usuario.sql
	Objetivo..........: Listar todos os usuarios inativos
	Autor.............: SMN - Rafael Morais
 	Data..............: 19/09/2017
	Ex................: EXEC [dbo].[GCS_ListarUsuariosInativos]

	*/

	BEGIN
	
		SELECT	Cpf,
				SenhaUsuario,
				SaldoUsuario,
				NomeUsuario,
				Ativo
				FROM [dbo].[Usuario]
				WHERE Ativo = 'I'
	END
GO
				
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[CSSP_SelUsuariosDivida]') AND objectproperty(id, N'IsPROCEDURE')=1)
	DROP PROCEDURE [dbo].[CSSP_SelUsuariosDivida]
GO

CREATE PROCEDURE [dbo].[CSSP_SelUsuariosDivida]

	AS

	/*
	Documenta��o
	Arquivo Fonte.....: Usuario.sql
	Objetivo..........: Selecionar usuarios com saldo negativo
	Autor.............: SMN - Lucas Fernando
 	Data..............: 05/09/2017
	Ex................: EXEC [dbo].[CSSP_SelUsuariosDivida]

	*/

	BEGIN
		SELECT  Cpf,
				SenhaUsuario,
				SaldoUsuario,
				NomeUsuario,
				Ativo
			FROM [dbo].[Usuario]
			WHERE SaldoUsuario < 0 AND Ativo = 'A'
	END


IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[CSSP_LisUsuarioIgual]') AND objectproperty(id, N'IsPROCEDURE')=1)
	DROP PROCEDURE [dbo].[CSSP_LisUsuarioIgual]
GO
CREATE PROCEDURE [dbo].[CSSP_LisUsuarioIgual]
	@nome varchar(50),
	@cpf varchar(11)
	AS

	/*
	Documenta��o
	Arquivo Fonte.....: Usuario.sql
	Objetivo..........: Verificar se j� existe um usu�rio com este mesmo CPF
	Autor.............: SMN - Lucas Fernando
 	Data..............: 18/09/2017
	Ex................: EXEC [dbo].[GCS_CSSP_LisUsuarioIgual]

	*/

	BEGIN
		SELECT TOP 1 1
			FROM Usuario
			WHERE Cpf = @cpf AND NomeUsuario <> @nome
	END
GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[CSSP_LisUsuarioPorNome]') AND objectproperty(id, N'IsPROCEDURE')=1)
	DROP PROCEDURE [dbo].[CSSP_LisUsuarioPorNome]
GO

CREATE PROCEDURE [dbo].[CSSP_LisUsuarioPorNome]
	@NomeUsuario varchar (40)
	AS
	/*
	Documenta��o
	Arquivo Fonte.....: Usuario.sql
	Objetivo..........: Listar usuario de acordo com um trecho do nome
	Autor.............: SMN - Rafael Henrique
 	Data..............: 20/09/2017
	Ex................: EXEC [dbo].[CSSP_LisUsuarioPorNome]
	*/	
	BEGIN
		SELECT * 
			FROM [dbo].[Usuario] WITH(NOLOCK)
			WHERE NomeUsuario LIKE '%' + @NomeUsuario + '%'
	END
GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[CSSP_VerificaLoginSenha]') AND objectproperty(id, N'IsPROCEDURE')=1)
	DROP PROCEDURE [dbo].[CSSP_VerificaLoginSenha]
GO

CREATE PROCEDURE [dbo].[CSSP_VerificaLoginSenha]
	@Cpf varchar(11),
	@SenhaUsuario varchar(12)

	AS
	/*
	Documenta��o
	Arquivo Fonte.....: Usuario.sql
	Objetivo..........: Verificar se o login bate
	Autor.............: SMN - Lucas Fernando
 	Data..............: 20/09/2017
	Ex................: EXEC [dbo].[GCS_VerificaLoginSenha]
	*/

	BEGIN									
		SELECT TOP 1 1 
			FROM Usuario
			WHERE Cpf = @Cpf AND SenhaUsuario = @SenhaUsuario
	END


IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[GCS_ValidaCpf]') AND objectproperty(id, N'IsPROCEDURE')=1)
	DROP PROCEDURE [dbo].[GCS_ValidaCpf]
GO

CREATE PROCEDURE [dbo].[GCS_ValidaCpf]
		@Cpf varchar(11)
	AS

	/*
	Documentação
	Arquivo Fonte.....: Usuario.sql
	Objetivo..........: Validar se cpf é valido
	Autor.............: SMN - João Guilherme
 	Data..............: 22/09/2017
	Ex................: EXEC [dbo].[GCS_ValidaCpf]

	*/

	BEGIN
		SELECT TOP 1 1		
			FROM Usuario	
			WHERE Cpf = @Cpf
	END
GO
				
GO
				