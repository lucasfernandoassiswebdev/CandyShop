
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[GCS_InsUsuario]') AND objectproperty(id, N'IsPROCEDURE')=1)
	DROP PROCEDURE [dbo].[GCS_InsUsuario]
GO

CREATE PROCEDURE [dbo].[GCS_InsUsuario]

	AS

	/*
	Documentação
	Arquivo Fonte.....: Usuario.sql
	Objetivo..........: Inserir Usuarios 
	Autor.............: SMN - João Guilherme
 	Data..............: 06/09/2017
	Ex................: EXEC [dbo].[GCS_InsUsuario]

	*/
	
	BEGIN
	
			

	END
GO
				