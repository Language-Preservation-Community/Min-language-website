CREATE TABLE [dbo].[VocabsSuggest]
(
    [EnglishTranslation] NVARCHAR (MAX),
    [RegionUsed]         NVARCHAR (MAX),
    [ExampleSentences]   NVARCHAR (MAX),
    [WordClass]          NVARCHAR (MAX),
    [Category]           NVARCHAR (MAX),
    [Key]                INT IDENTITY (1, 1) PRIMARY KEY,
	[Vocabskey]			 INT FOREIGN KEY REFERENCES [dbo].[Vocabs] ([key]),
    [UserId]             NVARCHAR (450) NOT NULL
)
