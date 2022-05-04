CREATE TABLE [trade].[CommodityModelValuation]
(
	[Id] BIGINT NOT NULL PRIMARY KEY IDENTITY(1,1),
	[CommodityModelId] INT NOT NULL,
	[Date] DATE NOT NULL,
	[Price] DECIMAL(10,2) NOT NULL,
	[Pnl] DECIMAL(10,2) NOT NULL,
	[CreatedDate] DATETIME2 NOT NULL DEFAULT getutcdate(),
	[CreatedBy] VARCHAR(20),
	[LastModifiedDate] DATETIME2 NOT NULL DEFAULT getutcdate(),
	[LastModifiedBy] VARCHAR(20),
	CONSTRAINT FK_CommodityModelValuation_CommodityModel FOREIGN KEY (CommodityModelId) REFERENCES model.CommodityModel(Id),
	CONSTRAINT UNQ_CommodityModelValuation UNIQUE (CommodityModelId, [Date])
)
