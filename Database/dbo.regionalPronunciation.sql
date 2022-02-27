CREATE TABLE [dbo].[regionalPronunciation] (
    [name]          NVARCHAR (450) NOT NULL,
    [pronunciation] NVARCHAR (MAX) NOT NULL,
    [VocabsId]      INT            NOT NULL,
    CONSTRAINT [PK_regionalPronunciation] PRIMARY KEY CLUSTERED ([name] ASC),
    CONSTRAINT [FK_regionalPronunciation_Vocabs_VocabsId] FOREIGN KEY ([VocabsId]) REFERENCES [dbo].[Vocabs] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_regionalPronunciation_VocabsId]
    ON [dbo].[regionalPronunciation]([VocabsId] ASC);

