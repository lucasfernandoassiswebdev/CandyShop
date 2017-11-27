

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[CSSP_InsProduto]') AND objectproperty(id, N'IsPROCEDURE')=1)
	DROP PROCEDURE [dbo].[CSSP_InsProduto]
GO

CREATE PROCEDURE [dbo].[CSSP_InsProduto]
	@NomeProduto varchar(40),
	@PrecoProduto decimal(15,2),
	@QtdeProduto int,
	@Ativo varchar(1) = 'A',
	@Categoria varchar(50),
	@sequencial int = 0 output
	AS
			
	/*
	Documentação
	Arquivo Fonte.....: Produto.sql
	Objetivo..........: Inserir Produtos
	Autor.............: SMN - João Guilherme
 	Data..............: 06/09/2017
	Ex................: EXEC [dbo].[CSSP_InsProduto] 'tortuguita', 1.00, 0, '1', 'chocolates'

	*/

	BEGIN
	
		INSERT INTO [dbo].[Produto] (NomeProduto,PrecoProduto,QtdeProduto,Ativo,Categoria)
			VALUES (@NomeProduto,@PrecoProduto,@QtdeProduto,@Ativo,@Categoria)

			SELECT @sequencial = SCOPE_IDENTITY()

				IF @@ERROR <> 0
					RETURN -1
		RETURN 0
	END
GO


IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[CSSP_DesProduto]') AND objectproperty(id, N'IsPROCEDURE')=1)
	DROP PROCEDURE [dbo].[CSSP_DesProduto]
GO

CREATE PROCEDURE [dbo].[CSSP_DesProduto]
	@IdProduto int
	AS
	
	/*
	Documentação
	Arquivo Fonte.....: Produto.sql
	Objetivo..........: Deletar Produtos 
	Autor.............: SMN - João Guilherme
 	Data..............: 01/01/2017
	Ex................: EXEC [dbo].[CSSP_DelProduto]

	*/

	BEGIN
	UPDATE [dbo].[Produto]
		SET Ativo = 'I',
			QtdeProduto = 0
		WHERE IdProduto = @IdProduto
				
		IF @@ERROR <> 0
					RETURN 1
		RETURN 0		
	END
GO


IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[CSSP_UpdProduto]') AND objectproperty(id, N'IsPROCEDURE')=1)
	DROP PROCEDURE [dbo].[CSSP_UpdProduto]
GO

CREATE PROCEDURE [dbo].[CSSP_UpdProduto]
	@IdProduto int,
	@NomeProduto varchar(40),
	@PrecoProduto decimal(15,2),
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
	Ex................: EXEC [dbo].[CSSP_UpdProduto]

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
		
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[CSSP_SelProduto]') AND objectproperty(id, N'IsPROCEDURE')=1)
	DROP PROCEDURE [dbo].[CSSP_SelProduto]
GO

CREATE PROCEDURE [dbo].[CSSP_SelProduto]
	@NomeProduto VARCHAR(50)
	AS

	/*
	Documentação
	Arquivo Fonte.....: Produto.sql
	Objetivo..........: Selecionar Produtos
	Autor.............: SMN - João Guilherme
 	Data..............: 06/09/2017
	Ex................: EXEC [dbo].[CSSP_SelProduto]

	*/

	BEGIN
		SELECT TOP 1 1
			FROM [dbo].[Produto] WITH(NOLOCK)
			WHERE NomeProduto = @NomeProduto
	END
GO


IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[CSSP_SelDadosProduto]') AND objectproperty(id, N'IsPROCEDURE')=1)
	DROP PROCEDURE [dbo].[CSSP_SelDadosProduto]
GO

CREATE PROCEDURE [dbo].[CSSP_SelDadosProduto]
	@IdProduto int
	AS

	/*
	Documentação
	Arquivo Fonte.....: Produto.sql
	Objetivo..........: Listar dados de um produto por ID
	Autor.............: SMN - Rafael Morais
 	Data..............: 19/09/2017
	Ex................: EXEC [dbo].[GCS_SelDadosProduto]

	*/

	BEGIN
	
		SELECT IdProduto,
				NomeProduto,
				PrecoProduto,
				QtdeProduto,
				Ativo,
				Categoria
			FROM Produto WITH (NOLOCK)
			WHERE IdProduto = @IdProduto

	END
GO
				
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[CSSP_LisProduto]') AND objectproperty(id, N'IsPROCEDURE')=1)
	DROP PROCEDURE [dbo].[CSSP_LisProduto]
GO

CREATE PROCEDURE [dbo].[CSSP_LisProduto]

	AS	
	/* 
	Documentação
	Arquivo Fonte.....: Produto.sql
	Objetivo..........: Listar todos os produtos
	Autor.............: SMN - Rafael Morais
 	Data..............: 07/07/2017
	Ex................: EXEC [dbo].[CSSP_LisProduto]
						select * from Produto
	Editado Por.......: SMN - João Guilherme
	Objetivo..........: Alterando o select 
	Data..............: 12/09/2017
	
	Editado Por.......: SMN - Gustavo Dantas
	Objetivo..........: Alterando o select 
	Data..............: 12/09/2017
	*/

	BEGIN
	
		SELECT	IdProduto,
				NomeProduto,
				PrecoProduto,
				QtdeProduto,
				Ativo,
				Categoria
			 FROM Produto WITH(NOLOCK)
			 WHERE Ativo = 'A'
			 ORDER BY NomeProduto
	END
GO


IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[CSSP_LisProdutoAeI]') AND objectproperty(id, N'IsPROCEDURE')=1)
	DROP PROCEDURE [dbo].[CSSP_LisProdutoAeI]
GO

CREATE PROCEDURE [dbo].[CSSP_LisProdutoAeI]

	AS

	/*
	Documentação
	Arquivo Fonte.....: Produto.sql
	Objetivo..........: Listar produtos ativos e inativos ordenado 
	Autor.............: SMN - João Guilherme
 	Data..............: 26/10/2017
	Ex................: EXEC [dbo].[CSSP_LisProdutoAeI]

	*/

	BEGIN
	
		SELECT  IdProduto,
				NomeProduto,
				PrecoProduto,
				QtdeProduto,
				Ativo,
				Categoria
		FROM Produto WITH(NOLOCK)
		ORDER BY Ativo

	END
GO
				

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[CSSP_LisProdutoInativo]') AND objectproperty(id, N'IsPROCEDURE')=1)
	DROP PROCEDURE [dbo].[CSSP_LisProdutoInativo]
GO

CREATE PROCEDURE [dbo].[CSSP_LisProdutoInativo]

	AS

	/*
	Documentação
	Arquivo Fonte.....: Produto.sql
	Objetivo..........: Listar todos os produtos que estão inativos
	Autor.............: SMN - Lucas Fernando
 	Data..............: 14/09/2017
	Ex................: EXEC [dbo].[CSSP_LisProdutoInativo]

	*/

	BEGIN
		SELECT	IdProduto,
				NomeProduto,
				PrecoProduto,
				QtdeProduto,
				Ativo,
				Categoria
			 FROM Produto WITH(NOLOCK)
			 WHERE Ativo = 'I'
			 ORDER BY NomeProduto
	END
GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[CSSP_LisProdutoValorCres]') AND objectproperty(id, N'IsPROCEDURE')=1)
	DROP PROCEDURE [dbo].CSSP_LisProdutoValorCres
GO

CREATE PROCEDURE [dbo].CSSP_LisProdutoValorCres

	AS

	/*
	Documentação
	Arquivo Fonte.....: Produto.sql
	Objetivo..........: Listar os produtos pela ordem de preço crescente	
	Autor.............: SMN - Lucas Fernando
 	Data..............: 14/09/2017
	Ex................: EXEC [dbo].[CSSP_LisProdutoValor]

	*/

	BEGIN
		SELECT * 
			FROM Produto
			WHERE Ativo = 'A'
			ORDER BY PrecoProduto
			 
	END
GO
				

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[CSSP_LisProdutoValorDesc]') AND objectproperty(id, N'IsPROCEDURE')=1)
	DROP PROCEDURE [dbo].[CSSP_LisProdutoValorDesc]
GO

CREATE PROCEDURE [dbo].[CSSP_LisProdutoValorDesc]

	AS

	/*
	Documentação
	Arquivo Fonte.....: Produto.sql
	Objetivo..........: Listar os produtos em ordem decrescente de valor
	Autor.............: SMN - Lucas Fernando
 	Data..............: 14/09/2017
	Ex................: EXEC [dbo].[CSSP_LisProdutoValorDesc]

	*/

	BEGIN
	SELECT * 
		FROM Produto
		WHERE Ativo = 'A'
		ORDER BY PrecoProduto desc
END
GO
				

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[CSSP_LisProdutoAbaixoValor]') AND objectproperty(id, N'IsPROCEDURE')=1)
	DROP PROCEDURE [dbo].[CSSP_LisProdutoAbaixoValor]
GO

CREATE PROCEDURE [dbo].[CSSP_LisProdutoAbaixoValor]
	@Valor decimal(15,2)
	AS

	/*
	Documentação
	Arquivo Fonte.....: Produt.sql
	Objetivo..........: Listar os produtos abaixo do valor que será passado âtravés de um parâmetro
	Autor.............: SMN - Lucas Fernando
 	Data..............: 14/09/2017
	Ex................: EXEC [dbo].[CSSP_LisProdutoAbaixoValor]

	*/

	BEGIN
	SELECT * 
		FROM Produto
		WHERE PrecoProduto <= @Valor AND Ativo = 'A'
	END
GO
				
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[CSSP_LisProdutoAcimaValor]') AND objectproperty(id, N'IsPROCEDURE')=1)
	DROP PROCEDURE [dbo].[CSSP_LisProdutoAcimaValor]
GO

CREATE PROCEDURE [dbo].[CSSP_LisProdutoAcimaValor]
	@valor decimal(15,2)
	AS

	/*
	Documentação
	Arquivo Fonte.....: Produto.sql
	Objetivo..........: Listar os produtos que estão acima de um certo valor
	Autor.............: SMN - Lucas Fernando
 	Data..............: 14/09/2017
	Ex................: EXEC [dbo].[CSSP_LisProdutoAcimaValor]

	*/
	
	BEGIN
	SELECT * 
		FROM Produto
		WHERE PrecoProduto >= @valor AND Ativo = 'A'
	END
GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[CSSP_LisProdutoCategoria]') AND objectproperty(id, N'IsPROCEDURE')=1)
	DROP PROCEDURE [dbo].[CSSP_LisProdutoCategoria]
GO

CREATE PROCEDURE [dbo].[CSSP_LisProdutoCategoria]
	@Categoria varchar(50)
	AS

	/*
	Documentação
	Arquivo Fonte.....: Produto.sql
	Objetivo..........: Listar os produtos de acordo com a sua categoria
	Autor.............: SMN - Lucas Fernando
 	Data..............: 14/09/2017
	Ex................: EXEC [dbo].[CSSP_LisProdutoCategoria] 'Doces'

	*/	
	BEGIN
		SELECT * 
			FROM Produto
			WHERE Categoria like @Categoria AND Ativo = 'A'
			ORDER BY NomeProduto
	END
GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[CSSP_LisProdPorNome]') AND objectproperty(id, N'IsPROCEDURE')=1)
	DROP PROCEDURE [dbo].[CSSP_LisProdPorNome]
GO

CREATE PROCEDURE [dbo].[CSSP_LisProdPorNome]
	@NomeProduto varchar (40)
	AS

	/*
	Documentação
	Arquivo Fonte.....: Produto.sql
	Objetivo..........: Realmente eu nao sei
	Autor.............: SMN - João Guilherme
 	Data..............: 18/09/2017
	Ex................: EXEC [dbo].[CSSP_LisProdPorNome]

	*/

	BEGIN
	
		SELECT * 
			FROM [dbo].[Produto] WITH(NOLOCK)
			WHERE NomeProduto LIKE '%' + @NomeProduto + '%' AND Ativo = 'A'
	END
GO


IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[CSSP_SelLastProduto]') AND objectproperty(id, N'IsPROCEDURE')=1)
	DROP PROCEDURE [dbo].[CSSP_SelLastProduto]
GO

