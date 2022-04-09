CREATE TABLE [dbo].[Cliente]
(
	[numeroCliente] INT NOT NULL PRIMARY KEY IDENTITY, 
    [numeroCola] INT NOT NULL, 
    [id] NCHAR(10) NOT NULL, 
    [nombre] VARCHAR(300) NOT NULL, 
    [horaRegistro] DATETIME NOT NULL, 
    [estado] VARCHAR(3) NULL, 
    CONSTRAINT [FK_Cliente_Cola] FOREIGN KEY ([numeroCola]) REFERENCES [Cola]([numeroCola])
)
