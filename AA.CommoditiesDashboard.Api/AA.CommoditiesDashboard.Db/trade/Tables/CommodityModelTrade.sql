CREATE TABLE [trade].[CommodityModelTrade]
(
	[Id] BIGINT NOT NULL PRIMARY KEY IDENTITY(1,1),
	[CommodityModelId] INT NOT NULL,
	[CommodityContractId] INT NOT NULL,
	[TradeReference] VARCHAR(20),
	[TradeDate] DATE NOT NULL,
	[TradeAction] TINYINT NOT NULL,
	[TradedQuantity] DECIMAL(10,3) NOT NULL,
	[CreatedDate] DATETIME2 NOT NULL DEFAULT getutcdate(),
	[CreatedBy] VARCHAR(20),
	[LastModifiedDate] DATETIME2 NOT NULL DEFAULT getutcdate(),
	[LastModifiedBy] VARCHAR(20),
	CONSTRAINT FK_CommodityModelTrade_CommodityContract FOREIGN KEY (CommodityContractId) REFERENCES model.CommodityContract(Id),
	CONSTRAINT FK_CommodityModelTrade_CommodityModel FOREIGN KEY (CommodityModelId) REFERENCES model.CommodityModel(Id),
	CONSTRAINT CHK_CommodityModelTrade_TradeAction CHECK ([TradeAction] in (1, 2))
)
