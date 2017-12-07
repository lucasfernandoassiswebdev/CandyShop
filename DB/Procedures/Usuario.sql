
GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[CSSP_InsUsuario]') AND objectproperty(id, N'IsPROCEDURE')=1)
	DROP PROCEDURE [dbo].[CSSP_InsUsuario]
GO

CREATE PROCEDURE [dbo].[CSSP_InsUsuario]
	@NomeUsuario varchar(50),
	@SaldoUsuario decimal(18,2) = 0,
	@CpfUsuario varchar(14),
	@Ativo varchar(1) = 'A',
	@Classificacao varchar(1),
	@Email varchar(50)
	AS
	
	/*
	Documenta��o
	Arquivo Fonte.....: Usuario.sql
	Objetivo..........: Inserir Usuarios 
	Autor.............: SMN - Jo�o Guilherme
 	Data..............: 06/09/2017
	Ex................: EXEC [dbo].[CSSP_InsUsuario]
	
	Editado Por.......: SMN - Rafael Morais
	Objetivo..........: Adicionar a o campo de cpf na procedure 
	Data..............: 07/09/2017

	Editado Por.......: SMN - Gustavo Dantas
	Objetivo..........: Adicionar a o campo de email na procedure
	Data..............: 07/09/2017
	
	*/
	
	BEGIN
		INSERT INTO [dbo].[Usuario](Cpf,NomeUsuario,SenhaUsuario,SaldoUsuario,Ativo, Classificacao, FirstLogin, Email)
			VALUES (@CpfUsuario,@NomeUsuario,@CpfUsuario,@SaldoUsuario,@Ativo, @Classificacao, 'T',@Email)		
			
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
		DECLARE @NUMADM INT = 0
		IF (SELECT Classificacao 
				FROM [dbo].[Usuario]
				WHERE Cpf = @Cpf) like 'A'
		BEGIN
			SET @NUMADM = (SELECT COUNT(Classificacao) as Num 
				FROM [dbo].[Usuario] 
				WHERE Classificacao = 'A' AND Ativo = 'A')
		END

		IF @NUMADM <> 1
		BEGIN
			UPDATE [dbo].[Usuario] 
				SET Ativo = 'I'
				WHERE Cpf = @Cpf
				IF @@ERROR <> 0
					RETURN 1
			RETURN 0
		END
		ELSE 
		BEGIN
			RETURN 1
		END
	END
GO
		
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[CSSP_UpdUsuario]') AND objectproperty(id, N'IsPROCEDURE')=1)
	DROP PROCEDURE [dbo].[CSSP_UpdUsuario]
GO

CREATE PROCEDURE [dbo].[CSSP_UpdUsuario]
		@Cpf varchar(14),
	@NomeUsuario varchar(50),
	@SenhaUsuario varchar(12),
	@SaldoUsuario decimal,
	@Ativo varchar(1),
	@Classificacao varchar(1),
	@Email varchar(50)
	AS

	/*
	Documenta��o
	Arquivo Fonte.....: Usuario.sql
	Objetivo..........: Editar informa�oes do usuario
	Autor.............: SMN - Jo�o Guilherme
 	Data..............: 06/09/2017
	Ex................: EXEC [dbo].[CSSP_UpdUsuario]
	
	Objetivo..........: Editar informa�oes do usuario encriptando senha 
	Autor.............: SMN - Gustavo Dantas
 	Data..............: 27/11/2017

	Editado Por.......: SMN - Gustavo Dantas
	Objetivo..........: Adicionar a o campo de email na procedure
	Data..............: 07/09/2017
		*/
	BEGIN
	
		UPDATE [dbo].[Usuario]
			SET NomeUsuario = @NomeUsuario,
				SenhaUsuario =  CONVERT(NVARCHAR(12), HashBytes('MD5', @SenhaUsuario), 2),
				SaldoUsuario = @SaldoUsuario,
				Ativo = @Ativo,
				Classificacao = @Classificacao,
				Email = @Email
				WHERE Cpf = @Cpf

				IF @@ERROR <> 0 
					RETURN 1

			RETURN 0
	END
GO


IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[CSSP_UpdSenha]') AND objectproperty(id, N'IsPROCEDURE')=1)
	DROP PROCEDURE [dbo].[CSSP_UpdSenha]
GO

CREATE PROCEDURE [dbo].[CSSP_UpdSenha]
	@senha varchar(15),
	@cpf varchar(11)
	AS
	/*
	Documentação
	Arquivo Fonte.....: Usuario.sql
	Objetivo..........: Trocar somente a senha do usuario
	Autor.............: SMN - Rafael Morais
 	Data..............: 04/10/2017
	Ex................: EXEC [dbo].[CSSP_UpdSenha]

	Objetivo..........: colocando senha com decodificação md5 
	Autor.............: SMN - Gustavo Dantas
 	Data..............: 27/11/2017
	*/	
	BEGIN	
		UPDATE [dbo].[Usuario] 
			SET SenhaUsuario = CONVERT(NVARCHAR(12), HashBytes('MD5', @senha), 2),
				FirstLogin = 'F'
			WHERE Cpf = @cpf

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

	Editado Por.......: SMN - Gustavo Dantas
	Objetivo..........: Adicionar a o campo de email na procedure
	Data..............: 07/09/2017
	*/

	BEGIN
		SELECT	Cpf,
				SenhaUsuario,
				SaldoUsuario,
				NomeUsuario,
				Ativo,
				Classificacao,
				Email
				FROM [dbo].[Usuario]
				WHERE Ativo = 'A'
				ORDER BY NomeUsuario
	END
GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[GCS_LisUsuariosInativos]') AND objectproperty(id, N'IsPROCEDURE')=1)
	DROP PROCEDURE [dbo].[GCS_LisUsuariosInativos]
GO

CREATE PROCEDURE [dbo].[GCS_LisUsuariosInativos]

	AS

	/*
	Documentação
	Arquivo Fonte.....: Usuario.sql
	Objetivo..........: Listar todos usuarios inativos
	Autor.............: SMN - João Guilherme
 	Data..............: 27/10/2017
	Ex................: EXEC [dbo].[GCS_LisUsuariosInativos]

	Editado Por.......: SMN - Gustavo Dantas
	Objetivo..........: Adicionar a o campo de email na procedure
	Data..............: 07/09/2017
	*/

	BEGIN
		SELECT	Cpf,
				SenhaUsuario,
				SaldoUsuario,
				NomeUsuario,
				Ativo,
				Classificacao,
				Email
				FROM [dbo].[Usuario]
				WHERE Ativo = 'I'
	END
GO
				

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[CSSP_LisUsuariosAtivoseInativos]') AND objectproperty(id, N'IsPROCEDURE')=1)
	DROP PROCEDURE [dbo].[CSSP_LisUsuariosAtivoseInativos]
GO

CREATE PROCEDURE [dbo].[CSSP_LisUsuariosAtivoseInativos]

	AS

	/*
	Documenta��o
	Arquivo Fonte.....: Usuario.sql
	Objetivo..........: Listar todos os usuarios e ordenar por ativos
	Autor.............: SMN - João Guilherme
 	Data..............: 27/10/2017
	Ex................: EXEC [dbo].[CSSP_LisUsuariosAtivoseInativos]

	Editado Por.......: SMN - Gustavo Dantas
	Objetivo..........: Adicionar a o campo de email na procedure
	Data..............: 07/09/2017
	*/

	BEGIN
	
		SELECT	Cpf,
				SenhaUsuario,
				SaldoUsuario,
				NomeUsuario,
				Ativo,
				Classificacao
				Email
				FROM [dbo].[Usuario]
				ORDER BY Ativo
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

	Editado Por.......: SMN - Gustavo Dantas
	Objetivo..........: Adicionar a o campo de email na procedure
	Data..............: 07/09/2017
	*/

	BEGIN
		SELECT  Cpf,
				SenhaUsuario,
				SaldoUsuario,
				NomeUsuario,
				Ativo,
				Classificacao,
				Email
			FROM [dbo].[Usuario]
			WHERE SaldoUsuario < 0 AND Ativo = 'A'
			ORDER BY NomeUsuario

	END
GO

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

	Editado Por.......: SMN - Gustavo Dantas
	Objetivo..........: Adicionar a o campo de email na procedure
	Data..............: 07/09/2017
	*/	
	BEGIN
		SELECT Cpf,
				SenhaUsuario,
				SaldoUsuario,
				NomeUsuario,
				Ativo,
				Classificacao,
				Email
			FROM [dbo].[Usuario] WITH(NOLOCK)
			WHERE NomeUsuario LIKE '%' + @NomeUsuario + '%' AND Ativo = 'A'
	END
GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[CSSP_VerificaLoginSenha]') AND objectproperty(id, N'IsPROCEDURE')=1)
	DROP PROCEDURE [dbo].[CSSP_VerificaLoginSenha]
GO

CREATE PROCEDURE [dbo].[CSSP_VerificaLoginSenha]
	@Cpf varchar(11),
	@SenhaUsuario varchar(15)

	AS
	/*
	Documenta��o
	Arquivo Fonte.....: Usuario.sql
	Objetivo..........: Verificar se o login bate
	Autor.............: SMN - Lucas Fernando
 	Data..............: 20/09/2017
	Ex................: EXEC [dbo].[CSSP_VerificaLoginSenha] '44541561824', 'AUVJYT'
	
	
	Objetivo..........: Verficar decodificação md5 
	Autor.............: SMN - Gustavo Dantas
 	Data..............: 27/11/2017
	*/

	BEGIN		
					
		SELECT TOP 1 1 
			FROM Usuario
			WHERE Cpf = @Cpf 
			  AND Ativo = 'A'   
			  AND SenhaUsuario = CASE WHEN FirstLogin = 'T' THEN @SenhaUsuario
			  ELSE CONVERT(NVARCHAR(12), HashBytes('MD5', @SenhaUsuario), 2) 
			  END  

	END
GO


IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[CSSP_VerificaSaldoLoja]') AND objectproperty(id, N'IsPROCEDURE')=1)
	DROP PROCEDURE [dbo].[CSSP_VerificaSaldoLoja]
GO

CREATE PROCEDURE [dbo].[CSSP_VerificaSaldoLoja]

	AS

	/*
	Documentação
	Arquivo Fonte.....: ArquivoFonte.sql
	Objetivo..........: Objetivo
	Autor.............: SMN - Lucas Fernando
 	Data..............: 01/01/2017
	Ex................: EXEC [dbo].[GCS_NomeProcedure]

	*/

	BEGIN
		SELECT SUM(SaldoUsuario) as 'saldo'
			FROM Usuario
			WHERE Ativo = 'A'
	END
GO


IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].CSSP_UpdEmail]') AND objectproperty(id, N'IsPROCEDURE')=1)
	DROP PROCEDURE [dbo].[CSSP_UpdEmail]
GO

CREATE PROCEDURE [dbo].[CSSP_UpdEmail]
	@email varchar(50),
	@cpf varchar(11)

	AS

	/*
	Documentação
	Arquivo Fonte.....: Usuario.sql
	Objetivo..........: Atualizar o email dos usuários
	Autor.............: SMN - Gustavo Dantas
 	Data..............: 04/12/2017
	Ex................: EXEC [dbo].[CSSP_UpdEmail] 'email@teste.com', '10963853171'

	*/
	select * from Usuario
	BEGIN
		UPDATE Usuario 
			SET Email = @email
			WHERE Cpf = @cpf 
	END
GO
				