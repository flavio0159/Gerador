CREATE TABLE [dbo].[Table]
(
	[ID] INT NOT NULL PRIMARY KEY, 
    [Time] NVARCHAR(50) NOT NULL, 
	[Gols] INT NOT NULL,
    [Pontos] INT NULL, 
	[Saldo de Gols] INT NULL,
    [Jogos] INT NULL, 
    [Vitórias] INT NULL,
	[Derrotas] INT NULL, 
    [Empates] INT NULL
)
