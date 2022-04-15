CREATE TABLE [dbo].[RegionalSuggest] (
    [RegionalKey]   INT REFERENCES [dbo].[regionalPronunciation] ([key]),
    [Name]          NVARCHAR (MAX),
    [Pronunciation] NVARCHAR (MAX),
    [Hanji]         NVARCHAR (MAX),
    [Key]           INT IDENTITY (1, 1) PRIMARY KEY,
    [VocabsSuggestKey] INT NOT NULL FOREIGN KEY REFERENCES [dbo].[VocabsSuggest] ([Key])
);
