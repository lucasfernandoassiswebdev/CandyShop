create database CandyShop

use CandyShop

CREATE TABLE Produto (
	IdProduto int constraint PK_Produto primary key identity (1,1) ,
	NomeProduto varchar(40) NOT NULL,
	PrecoProduto decimal(15,2) NOT NULL,
	QtdeProduto int NOT NULL,
	Ativo varchar(1) NOT NULL,
	Categoria varchar(50) NOT NULL
)

CREATE TABLE Usuario (
	Cpf varchar(11) constraint PK_Cpf primary key,
	NomeUsuario varchar(50) NOT NULL,
	SenhaUsuario varchar(30) NOT NULL,
	SaldoUsuario decimal NOT NULL,
	Ativo varchar(1) NOT NULL,
	Classificacao varchar(1) NOT NULL,		--Por padrão =>	'A' -> Admin______'U' -> Usuario
	FirstLogin varchar(1) NOT NULL			--Por padrão =>	'T'  _________	'F' 
)

CREATE TABLE Pagamento (
	IdPagamento int constraint PK_IdPagamento primary key identity (1,1) NOT NULL,
	Cpf varchar(11) constraint FK_Cpf foreign key references Usuario (Cpf) NOT NULL,
	DataPagamento datetime NOT NULL,
	ValorPagamento decimal(15,2) NOT NULL
)

CREATE TABLE Compra(
	IdCompra int constraint PK_IdCompra primary key identity (1,1),
	UsuarioCompra varchar(11) constraint FK_UsuarioCompra foreign key references Usuario (Cpf),
	DataCompra datetime NOT NULL,
	ValorCompra decimal(15,2) NOT NULL
)	

CREATE TABLE CompraProduto(
	IdProduto int constraint FK_IdCompraProduto foreign key references Produto (IdProduto),
	IdCompra int constraint FK_IdCompraProduto2 foreign key references Compra (IdCompra),
	QtdeProduto int NOT NULL, 
)
