IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Carros] (
    [Id] int NOT NULL IDENTITY,
    [VinCode] nvarchar(max) NULL,
    [Marca] VARCHAR(100) NULL DEFAULT 'Padrão',
    [Modelo] nvarchar(max) NULL,
    [Ano] int NOT NULL,
    [Preco] decimal(18,2) NOT NULL,
    [Cor] nvarchar(max) NULL,
    [DataProducao] datetime2 NOT NULL DEFAULT (getdate()),
    [Status] int NOT NULL,
    [Registrado] datetime2 NOT NULL,
    CONSTRAINT [PK_Carros] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Pedido] (
    [Id] int NOT NULL IDENTITY,
    [IdCar] int NOT NULL,
    [idCustomer] int NOT NULL,
    [TotalCost] decimal(18,2) NOT NULL,
    CONSTRAINT [PK_Pedido] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Pedido_Carros_IdCar] FOREIGN KEY ([IdCar]) REFERENCES [Carros] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Clientes] (
    [Id] int NOT NULL IDENTITY,
    [Nome] nvarchar(max) NULL,
    [Documento] nvarchar(max) NULL,
    [pedidoId] int NULL,
    [DataNascimento] datetime2 NOT NULL,
    CONSTRAINT [PK_Clientes] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Clientes_Pedido_pedidoId] FOREIGN KEY ([pedidoId]) REFERENCES [Pedido] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [ItensPedidosExtras] (
    [Id] int NOT NULL IDENTITY,
    [Descricao] nvarchar(max) NULL,
    [Valor] decimal(18,2) NOT NULL,
    [IdPedido] int NOT NULL,
    CONSTRAINT [PK_ItensPedidosExtras] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ItensPedidosExtras_Pedido_IdPedido] FOREIGN KEY ([IdPedido]) REFERENCES [Pedido] ([Id]) ON DELETE NO ACTION
);
GO

CREATE INDEX [IX_Clientes_pedidoId] ON [Clientes] ([pedidoId]);
GO

CREATE INDEX [IX_ItensPedidosExtras_IdPedido] ON [ItensPedidosExtras] ([IdPedido]);
GO

CREATE UNIQUE INDEX [IX_Pedido_IdCar] ON [Pedido] ([IdCar]);
GO

CREATE INDEX [IX_Pedido_idCustomer] ON [Pedido] ([idCustomer]);
GO

ALTER TABLE [Pedido] ADD CONSTRAINT [FK_Pedido_Clientes_idCustomer] FOREIGN KEY ([idCustomer]) REFERENCES [Clientes] ([Id]) ON DELETE NO ACTION;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210403003918_InitialMigration', N'5.0.4');
GO

COMMIT;
GO

