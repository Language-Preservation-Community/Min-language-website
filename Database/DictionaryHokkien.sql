CREATE TABLE [dbo].[user] (
    [userID]   NVARCHAR (450) NOT NULL,
    [email]    NVARCHAR (MAX) NOT NULL,
    [psswrd]   NVARCHAR (MAX) NOT NULL,
    [username] NVARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_user] PRIMARY KEY CLUSTERED ([userID] ASC)
);

