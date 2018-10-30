
CREATE TABLE [dbo].[Estabelecimento] (
    [estabelecimentoID] INT           IDENTITY (1, 1) NOT NULL,
    [CategoriaID]       INT           NOT NULL,
    [dataCad]           DATETIME      NULL,
    [razao]             VARCHAR (150) NOT NULL,
    [fantasia]          VARCHAR (150) NULL,
    [CNPJ]              VARCHAR (18)  NOT NULL,
    [email]             VARCHAR (150) NULL,
    [endereco]          VARCHAR (350) NULL,
    [cidade]            VARCHAR (50)  NULL,
    [estado]            VARCHAR (10)  NULL,
    [telefone]          VARCHAR (50)  NULL,
    [agencia]           VARCHAR (50)  NULL,
    [conta]             VARCHAR (50)  NULL,
    [status]            BIT           NOT NULL
);

CREATE TABLE [dbo].[Categoria] (
    [categoriaID] INT           IDENTITY (1, 1) NOT NULL,
    [nome]        VARCHAR (150) NULL
);

INSERT INTO [dbo].[Categoria]  VALUES ('SuperMercado');
INSERT INTO [dbo].[Categoria]  VALUES ('Restaurante');
INSERT INTO [dbo].[Categoria]  VALUES ('Borracharia');
INSERT INTO [dbo].[Categoria]  VALUES ('Posto');
INSERT INTO [dbo].[Categoria]  VALUES ('Oficina');
