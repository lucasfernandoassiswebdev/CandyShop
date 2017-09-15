
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[GCS_InsUsuario]') AND objectproperty(id, N'IsPROCEDURE')=1)
	DROP PROCEDURE [dbo].[GCS_InsUsuario]
GO

CREATE PROCEDURE [dbo].[GCS_InsUsuario]
	@NomeUsuario varchar(50),
	@SenhaUsuario varchar(12) = 'password',
	@SaldoUsuario decimal = 0,
	@CpfUsuario varchar(14) 
	AS

	/*
	Documenta��o
	Arquivo Fonte.....: Usuario.sql
	Objetivo..........: Inserir Usuarios 
	Autor.............: SMN - Jo�o Guilherme
 	Data..............: 06/09/2017
	Ex................: EXEC [dbo].[GCS_InsUsuario]
	
	Editado Por.......: SMN - Rafael Morais
	Objetivo..........: Adicionar a o campo de cpf na proceure 
	Data..............: 07/09/2017
	
	*/
	
	BEGIN
		INSERT INTO [dbo].[Usuario](Cpf,NomeUsuario,SenhaUsuario,SaldoUsuario)
			VALUES (@CpfUsuario,@NomeUsuario,@SenhaUsuario,@SaldoUsuario)		
			
				IF @@ERROR <> 0
					RETURN 1
		RETURN 0	
	END
GO


IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[GCS_DelUsuario]') AND objectproperty(id, N'IsPROCEDURE')=1)
	DROP PROCEDURE [dbo].[GCS_DelUsuario]
GO

CREATE PROCEDURE [dbo].[GCS_DelUsuario]
	@Cpf varchar(14)
	AS

	/*
	Documenta��o
	Arquivo Fonte.....: Usuario.sql
	Objetivo..........: Deletar cadatro de usuario
	Autor.............: SMN - Jo�o Guilherme
 	Data..............: 06/09/2017
	Ex................: EXEC [dbo].[GCS_DelUsuario]

	*/

	BEGIN
		DELETE [dbo].[Usuario] 
			WHERE Cpf = @Cpf
			IF @@ERROR <> 0
				RETURN 1

		RETURN 0
	END
GO
		
		
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[GCS_UpdUsuario]') AND objectproperty(id, N'IsPROCEDURE')=1)
	DROP PROCEDURE [dbo].[GCS_UpdUsuario]
GO

CREATE PROCEDURE [dbo].[GCS_UpdUsuario]
	@Cpf varchar(14),
	@NomeUsuario varchar(50),
	@SenhaUsuario varchar(12),
	@SaldoUsuario decimal
	AS

	/*
	Documenta��o
	Arquivo Fonte.....: Usuario.sql
	Objetivo..........: Editar informa�oes do usuario
	Autor.............: SMN - Jo�o Guilherme
 	Data..............: 06/09/2017
	Ex................: EXEC [dbo].[GCS_UpdUsuario]

	*/

	BEGIN
	
		UPDATE [dbo].[Usuario]
			SET NomeUsuario = @NomeUsuario,
				SenhaUsuario = @SenhaUsuario,
				SaldoUsuario = @SaldoUsuario
				WHERE Cpf = @Cpf

				IF @@ERROR <> 0 
					RETURN 1

			RETURN 0
	END
GO


IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[GCS_SelUsuario]') AND objectproperty(id, N'IsPROCEDURE')=1)
	DROP PROCEDURE [dbo].[GCS_SelUsuario]
GO

CREATE PROCEDURE [dbo].[GCS_SelUsuario]
	@Cpf varchar(14)
	AS

	/*
	Documenta��o
	Arquivo Fonte.....: Usuario.sql
	Objetivo..........: Selecionar Usuarios
	Autor.............: SMN - Jo�o Guilherme
 	Data..............: 06/09/2017
	Ex................: EXEC [dbo].[GCS_SelUsuario]

	*/

	BEGIN
		SELECT * FROM [dbo].[Usuario] WITH(NOLOCK)
			WHERE Cpf = @Cpf
	END
GO
				

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[GCS_LisUsuario]') AND objectproperty(id, N'IsPROCEDURE')=1)
	DROP PROCEDURE [dbo].[GCS_LisUsuario]
GO
	
CREATE PROCEDURE [dbo].[GCS_LisUsuario]

	AS

	/*
	Documenta��o
	Arquivo Fonte.....: Usuario.sql
	Objetivo..........: Listar todos os usuario
	Autor.............: SMN - Rafael Morais
 	Data..............: 07/07/2017
	Ex................: EXEC [dbo].[GCS_LisUsuario]

	Editado Por.......: SMN - Jo�o Guilherme
	Objetivo..........: Alterando o select 
	Data..............: 12/09/2017
	*/

	BEGIN
		SELECT Cpf,
				SenhaUsuario,
				SaldoUsuario,
				NomeUsuario
				 FROM [dbo].[Usuario]
	END
GO


IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[GCS_SelUsuarioSaldo]') AND objectproperty(id, N'IsPROCEDURE')=1)
	DROP PROCEDURE [dbo].[GCS_SelUsuarioSaldo]
GO

CREATE PROCEDURE [dbo].[GCS_SelUsuarioSaldo]

	AS

	/*
	Documenta��o
	Arquivo Fonte.....: Usuario.sql
	Objetivo..........: Selecionar usuarios com saldo negativo
	Autor.............: SMN - Jo�o Guilherme
 	Data..............: 14/09/2017
	Ex................: EXEC [dbo].[GCS_SelUsuarioSaldo]

	*/

	BEGIN
		SELECT Cpf,
				NomeUsuario,
				SaldoUsuario	
				FROM [dbo].[Usuario]
		WHERE SaldoUsuario < 0
		
	END
GO
				