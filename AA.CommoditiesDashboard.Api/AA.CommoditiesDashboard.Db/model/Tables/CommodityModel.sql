CREATE TABLE [model].[CommodityModel]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	[Name] NVARCHAR(100) NOT NULL,
	[CommodityId] INT NOT NULL,
	[ModelId] INT NOT NULL,
	[CreatedDate] DATETIME2 NOT NULL DEFAULT getutcdate(),
	[CreatedBy] VARCHAR(20),
	[LastModifiedDate] DATETIME2 NOT NULL DEFAULT getutcdate(),
	[LastModifiedBy] VARCHAR(20),
	CONSTRAINT FK_CommodityModel_Commodity FOREIGN KEY (CommodityId) REFERENCES model.Commodity(Id),
	CONSTRAINT FK_CommodityModel_Model FOREIGN KEY ([ModelId]) REFERENCES model.Model(Id),
	CONSTRAINT UNQ_CommodityModel UNIQUE (CommodityId, [ModelId])
)
