CREATE TABLE [trade].[CommodityModelPosition]
(
	[Id] BIGINT NOT NULL PRIMARY KEY IDENTITY(1,1),
	[IsCurrent] BIT NOT NULL,
	[FromDate] DATE NOT NULL,
	[ToDate] DATE NULL,
	[CommodityModelId] INT NOT NULL,
	[NetPosition] DECIMAL(10,3) NOT NULL,
	[CreatedDate] DATETIME2 NOT NULL DEFAULT getutcdate(),
	[CreatedBy] VARCHAR(20),
	[LastModifiedDate] DATETIME2 NOT NULL DEFAULT getutcdate(),
	[LastModifiedBy] VARCHAR(20),
	CONSTRAINT FK_CommodityModelPosition_CommodityModel FOREIGN KEY (CommodityModelId) REFERENCES model.CommodityModel(Id),
)
