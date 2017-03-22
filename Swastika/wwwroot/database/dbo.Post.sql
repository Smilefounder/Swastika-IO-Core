CREATE TABLE [dbo].[Post]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Name] NVARCHAR(500) NULL, 
    [Title] NVARCHAR(500) NULL, 
    [Description] NVARCHAR(MAX) NULL, 
    [CreateDateTime] DATETIME NULL
)
