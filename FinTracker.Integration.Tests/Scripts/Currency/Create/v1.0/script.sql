SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Currency]
(
    [Id] [uniqueidentifier] NOT NULL,
    [Title] [nvarchar] (64) NOT NULL,
    [Sign] [nvarchar] (6) UNIQUE NOT NULL,
    CONSTRAINT [PK_Currency] PRIMARY KEY CLUSTERED ( [Id] ASC )
    WITH
    (
        PAD_INDEX = OFF,
        STATISTICS_NORECOMPUTE = OFF,
        IGNORE_DUP_KEY = OFF,
        ALLOW_ROW_LOCKS = ON,
        ALLOW_PAGE_LOCKS = ON,
        OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF
    ) ON [PRIMARY]
) ON [PRIMARY]
GO