﻿CREATE TABLE [DishDepot].[Recipes]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
	[UserId] UNIQUEIDENTIFIER NOT NULL,
	[Name] NVARCHAR(128) NOT NULL,
	[Description] NVARCHAR(256) NULL,
	[PrepTime] INT NOT NULL,
	[CookTime] INT NOT NULL,
	[Servings] INT NOT NULL,
	[CreatedDateTime] DATETIME NOT NULL DEFAULT GETDATE(),	
	[ETag] ROWVERSION NOT NULL,
	CONSTRAINT FK_Recipes_User FOREIGN KEY (UserId) REFERENCES DishDepot.Users(Id)
);

GO

