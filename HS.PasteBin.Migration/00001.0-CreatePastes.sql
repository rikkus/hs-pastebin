CREATE TABLE [dbo].[Pastes]
(
	[id]		[int]			IDENTITY(1,1) NOT NULL,
	[Text]		[ntext]			NOT NULL,
	[Html]		[ntext]			NOT NULL,
	[Preview]	[ntext]			NOT NULL,
	[CreatedAt]	[datetime]		NOT NULL,
	[Language]	[nvarchar](50)	NOT NULL,
	
	CONSTRAINT [PK_Pastes] PRIMARY KEY CLUSTERED ([id] ASC)
)
