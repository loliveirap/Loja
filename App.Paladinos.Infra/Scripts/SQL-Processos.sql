
CREATE DATABASE PaladinosDB
GO

USE PaladinosDB
GO
CREATE TABLE [dbo].[Cliente]
(
	[Cli-Id] INT IDENTITY,
	[Cli-GuId] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [Cli-Nome] VARCHAR(40) NULL, 
    [Cli-Sobrenome] VARCHAR(40) NULL, 
	[Cli-RazaoSocial] VARCHAR(45) NULL, 
    [Cli-Cnpj] VARCHAR(45) NULL, 
    [Cli-Email] VARCHAR(100) NULL, 
    [Cli-Telefone] VARCHAR(15) NULL
)
GO

CREATE TABLE [dbo].[Endereco]
(
	[End-Id] INT IDENTITY,
	[End-GuId] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [Cli-GuId] UNIQUEIDENTIFIER NOT NULL, 
	[End-Rua] VARCHAR(60) NOT NULL,
    [End-Numero] VARCHAR(10) NOT NULL, 
    [End-Complemento] VARCHAR(40) NOT NULL, 
    [End-Municipio] VARCHAR(60) NOT NULL,
    [End-Estado] CHAR(2) NOT NULL, 
    [End-Pais] CHAR(2) NOT NULL, 
    [End-Cep] CHAR(8) NOT NULL, 
    [End-Tipo] INT NOT NULL DEFAULT 1,
	FOREIGN	KEY ([Cli-GuId]) REFERENCES [Cliente]([Cli-GuId])
)
go

CREATE TABLE [dbo].[Produto]
(
    [Pro-Id] INT IDENTITY,
	[Pro-GuId] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
	[Pro-Sku]  VARCHAR(50) NOT NULL,
    [Pro-Titulo] VARCHAR(255) NOT NULL, 
    [Pro-Descricao] TEXT NOT NULL, 
    [Pro-Image] VARCHAR(1024) NOT NULL, 
    [Pro-Preco] MONEY NOT NULL, 
    [Pro-Estoque] DECIMAL(10, 2) NULL
)

GO

CREATE TABLE [dbo].[CondPagto]
(
    [Cpg-Id] INT IDENTITY,
	[Cpg-GuId] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
	[Cpg-Descricao] VARCHAR(50) NOT NULL,
	[Cpg-Dias] INT
)
GO

CREATE TABLE [dbo].[Pedido]
(
    [Ped-Id] INT IDENTITY,
	[Ped-GuId] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
	[Ped-Numero] VARCHAR(10) NOT NULL,
    [Cli-GuId] UNIQUEIDENTIFIER NOT NULL, 
	[Cpg-GuId] UNIQUEIDENTIFIER NOT NULL,
    [Ped-DataCriacao] DATETIME NOT NULL DEFAULT Getdate(), 
    [Ped-Status] INT NOT NULL DEFAULT 1,
	FOREIGN KEY([Cli-GuId]) REFERENCES [Cliente]([Cli-GuId]),
	FOREIGN KEY([Cpg-GuId]) REFERENCES [CondPagto]([Cpg-GuId])
)

GO

CREATE TABLE [dbo].[ItemPedido]
(
	[Itm-Id] INT IDENTITY,
	[Itm-GuId] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [Ped-GuId] UNIQUEIDENTIFIER NOT NULL, 
	[Pro-GuId] UNIQUEIDENTIFIER NOT NULL, 
    [Itm-Quantidade] DECIMAL(10, 2) NOT NULL, 
    [Itm-Preco] MONEY NOT NULL, 
    FOREIGN KEY([Ped-GuId]) REFERENCES [Pedido]([Ped-GuId]),
	FOREIGN KEY([Pro-GuId]) REFERENCES [Produto]([Pro-GuId]),
)

GO


CREATE TABLE [dbo].[Entrega]
(
	[Etr-Id] INT IDENTITY,
	[Etr-GuId] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
	[Ped-GuId] UNIQUEIDENTIFIER NOT NULL,
    [Etr-DataCriacao] DATETIME NOT NULL DEFAULT Getdate(), 
	[Etr-DataEntregaEstimada] DATETIME NOT NULL,
    [Etr-Status] INT NOT NULL DEFAULT 1,
	FOREIGN KEY([Ped-GuId]) REFERENCES [Pedido]([Ped-GuId])
)

GO



Select * from Cliente
Select * from Endereco
Select * from Produto
Select * from Pedido
Select * from ItemPedido
Select * from CondPagto