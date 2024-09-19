CREATE TABLE [DishDepot].[RecipeIngredients]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
	[RecipeId] UNIQUEIDENTIFIER NOT NULL,
	[Name] NVARCHAR(64) NOT NULL,
	[MeasurementType] INT NOT NULL,
	[MeasurementValue] DECIMAL NOT NULL,
	CONSTRAINT FK_RecipeIngredients_Recipe FOREIGN KEY (RecipeId) REFERENCES DishDepot.Recipes(Id)
);

GO

