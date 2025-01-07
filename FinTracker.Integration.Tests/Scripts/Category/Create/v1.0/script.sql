SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Category]
(
    [Id] [uniqueidentifier] NOT NULL,
    [Title] [nvarchar] (128) UNIQUE NOT NULL,
    [Description] [nvarchar] (1024) NULL,
    CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED ( [Id] ASC )
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