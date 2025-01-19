SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Account]
(
    [Id] [uniqueidentifier] NOT NULL,
    [Balance] [money] NOT NULL,
    [Title] [nvarchar] (128) UNIQUE NOT NULL,
    [Description] [nvarchar] (1024) NULL,
    [CurrencyId] [uniqueidentifier] NOT NULL,
    CONSTRAINT [PK_Account] PRIMARY KEY CLUSTERED ( [Id] ASC )
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