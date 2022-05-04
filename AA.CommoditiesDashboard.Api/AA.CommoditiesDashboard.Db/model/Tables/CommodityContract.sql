CREATE TABLE [model].[CommodityContract]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	[Name] NVARCHAR(100) NOT NULL,
	[CommodityId] INT NOT NULL,
	[CreatedDate] DATETIME2 NOT NULL DEFAULT getutcdate(),
	[CreatedBy] VARCHAR(20),
	[LastModifiedDate] DATETIME2 NOT NULL DEFAULT getutcdate(),
	[LastModifiedBy] VARCHAR(20),
	CONSTRAINT FK_CommodityContract_Commodity FOREIGN KEY (CommodityId) REFERENCES model.Commodity(Id),
	CONSTRAINT UNQ_CommodityContract UNIQUE (CommodityId, [Name])
)
