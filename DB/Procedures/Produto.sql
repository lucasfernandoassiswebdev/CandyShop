IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[GCS_InsProduto]') AND objectproperty(id, N'IsPROCEDURE')=1)
	DROP PROCEDURE [dbo].[GCS_InsProduto]
GO

CREATE PROCEDURE [dbo].[GCS_InsProduto]
	@NomeProduto varchar(40),
	@PrecoProduto decimal,
	@QtdeProduto int,
	@Ativo varchar(1) = 'A',
	@Categoria varchar(50)
	AS

	/*
	Documentação
	Arquivo Fonte.....: Produto.sql
	Objetivo..........: Inserir Produtos
	Autor.............: SMN - João Guilherme
 	Data..............: 06/09/2017
	Ex................: EXEC [dbo].[GCS_InsProduto] 'tortuguita', 1.00, 0, '1', 'chocolates'

	*/

	BEGIN
	
		INSERT INTO [dbo].[Produto] (NomeProduto,PrecoProduto,QtdeProduto,Ativo,Categoria)
			VALUES (@NomeProduto,@PrecoProduto,@QtdeProduto,@Ativo,@Categoria)

				IF @@ERROR <> 0
					RETURN 1
		RETURN 0
	END
GO


IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[GCS_DelProduto]') AND objectproperty(id, N'IsPROCEDURE')=1)
	DROP PROCEDURE [dbo].[GCS_DelProduto]
GO

CREATE PROCEDURE [dbo].[GCS_DelProduto]
	@IdProduto int
	AS

	/*
	Documentação
	Arquivo Fonte.....: Produto.sql
	Objetivo..........: Deletar Produtos 
	Autor.............: SMN - João Guilherme
 	Data..............: 01/01/2017
	Ex................: EXEC [dbo].[GKSSP_DelProduto]

	*/

	BEGIN
	DELETE [dbo].[Produto]
		WHERE IdProduto = @IdProduto
		IF @@ERROR <> 0
					RETURN 1
		RETURN 0		
	END
GO


IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[GCS_UpdProduto]') AND objectproperty(id, N'IsPROCEDURE')=1)
	DROP PROCEDURE [dbo].[GCS_UpdProduto]
GO

CREATE PROCEDURE [dbo].[GCS_UpdProduto]
	@IdProduto int,
	@NomeProduto varchar(40),
	@PrecoProduto decimal,
	@QtdeProduto int,
	@Ativo varchar(1),
	@Categoria varchar(50)
	AS

	/*
	Documentação
	Arquivo Fonte.....: Produto.sql
	Objetivo..........: Editar Poodutos
	Autor.............: SMN - João Guilherme
 	Data..............: 06/09/2017
	Ex................: EXEC [dbo].[GCS_UpdProduto]

	*/

	BEGIN
		UPDATE [dbo].[Produto]
		SET	NomeProduto = @NomeProduto,
			PrecoProduto = @PrecoProduto,
			QtdeProduto = @QtdeProduto,
			Ativo = @Ativo,
			Categoria = @Categoria
		WHERE IdProduto = @IdProduto

		IF @@ERROR <> 0
					RETURN 1
		RETURN 0
	END
GO
		
		
		
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[GCS_SelProduto]') AND objectproperty(id, N'IsPROCEDURE')=1)
	DROP PROCEDURE [dbo].[GCS_SelProduto]
GO

CREATE PROCEDURE [dbo].[GCS_SelProduto]
	@NomeProduto VARCHAR(50)
	AS

	/*
	Documentação
	Arquivo Fonte.....: Produto.sql
	Objetivo..........: Selecionar Produtos
	Autor.............: SMN - João Guilherme
 	Data..............: 06/09/2017
	Ex................: EXEC [dbo].[GCS_SelProduto]

	*/

	BEGIN
		SELECT TOP 1 1
		 FROM [dbo].[Produto] WITH(NOLOCK)
			WHERE NomeProduto like '%' + @NomeProduto + '%'
	END
GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[GCS_LisProduto]') AND objectproperty(id, N'IsPROCEDURE')=1)
	DROP PROCEDURE [dbo].[GCS_LisProduto]
GO

CREATE PROCEDURE [dbo].[GCS_LisProduto]

	AS

	/*
	Documentação
	Arquivo Fonte.....: Produto.sql
	Objetivo..........: Listar todos os produtos
	Autor.............: SMN - Rafael Morais
 	Data..............: 07/07/2017
	Ex................: EXEC [dbo].[GCS_LisProduto]
						select * from Produto
	Editado Por.......: SMN - João Guilherme
	Objetivo..........: Alterando o select 
	Data..............: 12/09/2017
	*/

	BEGIN
	
		SELECT IdProduto,
				NomeProduto,
				PrecoProduto,
				QtdeProduto,
				Ativo,
				Categoria
			 FROM Produto WITH(NOLOCK)
	END
GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[GCS_LisProdutoInativo]') AND objectproperty(id, N'IsPROCEDURE')=1)
	DROP PROCEDURE [dbo].[GCS_LisProdutoInativo]
GO

CREATE PROCEDURE [dbo].[GCS_LisProdutoInativo]

	AS

	/*
	Documentação
	Arquivo Fonte.....: Produto.sql
	Objetivo..........: Listar todos os produtos que estão inativos
	Autor.............: SMN - Lucas Fernando
 	Data..............: 14/09/2017
	Ex................: EXEC [dbo].[GCS_LisProduto]

	*/

	BEGIN
		SELECT IdProduto,
				NomeProduto,
				PrecoProduto,
				QtdeProduto,
				Ativo,
				Categoria
			 FROM Produto WITH(NOLOCK)
			 WHERE Ativo = 'N'
	END
GO


IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[GCS_LisProdutoValorCres]') AND objectproperty(id, N'IsPROCEDURE')=1)
	DROP PROCEDURE [dbo].GCS_LisProdutoValorCres
GO

CREATE PROCEDURE [dbo].GCS_LisProdutoValorCres

	AS

	/*
	Documentação
	Arquivo Fonte.....: Produto.sql
	Objetivo..........: Listar os produtos pela ordem de preço crescente	
	Autor.............: SMN - Lucas Fernando
 	Data..............: 14/09/2017
	Ex................: EXEC [dbo].[GCS_LisProdutoValor]

	*/

	BEGIN
		SELECT * 
			FROM Produto
			ORDER BY PrecoProduto
	END
GO
				

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[GCS_LisProdutoValorDesc]') AND objectproperty(id, N'IsPROCEDURE')=1)
	DROP PROCEDURE [dbo].[GCS_LisProdutoValorDesc]
GO

CREATE PROCEDURE [dbo].[GCS_LisProdutoValorDesc]

	AS

	/*
	Documentação
	Arquivo Fonte.....: Produto.sql
	Objetivo..........: Listar os produtos em ordem decrescente de valor
	Autor.............: SMN - Lucas Fernando
 	Data..............: 14/09/2017
	Ex................: EXEC [dbo].[GCS_LisProdutoValorDesc]

	*/

	BEGIN
	SELECT * 
		FROM Produto
		ORDER BY PrecoProduto desc
END
GO
				

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[GCS_LisProdutoAbaixoValor]') AND objectproperty(id, N'IsPROCEDURE')=1)
	DROP PROCEDURE [dbo].[GCS_LisProdutoAbaixoValor]
GO

CREATE PROCEDURE [dbo].[GCS_LisProdutoAbaixoValor]
	@Valor decimal(18,2)
	AS

	/*
	Documentação
	Arquivo Fonte.....: Produt.sql
	Objetivo..........: Listar os produtos abaixo do valor que será passado âtravés de um parâmetro
	Autor.............: SMN - Lucas Fernando
 	Data..............: 14/09/2017
	Ex................: EXEC [dbo].[GCS_LisProdutoAbaixoValor]

	*/

	BEGIN
	SELECT * 
		FROM Produto
		WHERE PrecoProduto <= @Valor	
	END
GO
				
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[GCS_LisProdutoAcimaValor]') AND objectproperty(id, N'IsPROCEDURE')=1)
	DROP PROCEDURE [dbo].[GCS_LisProdutoAcimaValor]
GO

CREATE PROCEDURE [dbo].[GCS_LisProdutoAcimaValor]
	@valor decimal(18,2)
	AS

	/*
	Documentação
	Arquivo Fonte.....: Produto.sql
	Objetivo..........: Listar os produtos que estão acima de um certo valor
	Autor.............: SMN - Lucas Fernando
 	Data..............: 14/09/2017
	Ex................: EXEC [dbo].[GCS_LisProdutoAcimaValor]

	*/

	BEGIN
	SELECT * 
		FROM Produto
		WHERE PrecoProduto >= @valor
	END
GO


IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[GCS_LisProdutoCategoria]') AND objectproperty(id, N'IsPROCEDURE')=1)
	DROP PROCEDURE [dbo].[GCS_LisProdutoCategoria]
GO

CREATE PROCEDURE [dbo].[GCS_LisProdutoCategoria]
	@Categoria varchar(50)
	AS

	/*
	Documentação
	Arquivo Fonte.....: Produto.sql
	Objetivo..........: Listar os produtos de acordo com a sua categoria
	Autor.............: SMN - Lucas Fernando
 	Data..............: 14/09/2017
	Ex................: EXEC [dbo].[GCS_LisProdutoCategoria]

	*/

	BEGIN
		SELECT * 
			FROM Produto
			WHERE Categoria like '%' + @Categoria + '@%'
	END
GO
				


				

				


				

