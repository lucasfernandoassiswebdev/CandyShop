create database CandyShop

use CandyShop


CREATE TABLE Produto (
	IdProduto int constraint PK_Produto primary key identity (1,1),
	NomeProduto varchar(40),
	PrecoProduto decimal(15,2),
	QtdeProduto int,
	Ativo varchar(1),
	Categoria varchar(50)
)

CREATE TABLE Usuario (
	Cpf varchar(11) constraint PK_Cpf primary key,
	NomeUsuario varchar(50),
	SenhaUsuario varchar(15),
	SaldoUsuario decimal,
	Ativo varchar(1),
	Classificacao varchar(1),	--Por padrão =>	'A' -> Admin______'U' -> Usuario
	FirstLogin varchar(1),		--Por padrão =>	'T'  _________	'F' 
	Email varchar(50)	
)

CREATE TABLE Pagamento (
	IdPagamento int constraint PK_IdPagamento primary key identity (1,1),
	Cpf varchar(11) constraint FK_Cpf foreign key references Usuario (Cpf),
	DataPagamento datetime,
	ValorPagamento decimal(15,2)
)

CREATE TABLE Compra(
	IdCompra int constraint PK_IdCompra primary key identity (1,1),
	UsuarioCompra varchar(11) constraint FK_UsuarioCompra foreign key references Usuario (Cpf),
	DataCompra datetime,
	ValorCompra decimal(15,2)
)	

CREATE TABLE CompraProduto(
	IdProduto int constraint FK_IdCompraProduto foreign key references Produto (IdProduto),
	IdCompra int constraint FK_IdCompraProduto2 foreign key references Compra (IdCompra),
	QtdeProduto int, 
)