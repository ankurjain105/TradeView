CREATE TABLE [model].[Commodity]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	[Name] NVARCHAR(100) NOT NULL,
	[CreatedDate] DATETIME2 NOT NULL DEFAULT getutcdate(),
	[CreatedBy] VARCHAR(20),
	[LastModifiedDate] DATETIME2 NOT NULL DEFAULT getutcdate(),
	[LastModifiedBy] VARCHAR(20),
	CONSTRAINT UNQ_Commodity UNIQUE ([Name])
)
