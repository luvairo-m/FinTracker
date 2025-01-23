SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Payment] 
(
    [Id] [uniqueidentifier] NOT NULL,
    [Title] [nvarchar](128) NOT NULL,
    [Description] [nvarchar](1024) NULL,
    [Amount] [money] NOT NULL,
    [Type] [tinyint] NOT NULL,
    [Date] [datetime] NOT NULL,
    [AccountId] [uniqueidentifier] NOT NULL,
    CONSTRAINT [PK_Payment] PRIMARY KEY CLUSTERED ( [Id] ASC )
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

CREATE TABLE [dbo].[PaymentCategory]
(
    [PaymentId] [uniqueidentifier] NOT NULL,
    [CategoryId] [uniqueidentifier] NOT NULL,
    CONSTRAINT [PK_PaymentCategory] PRIMARY KEY CLUSTERED ( [PaymentId] ASC, [CategoryId] ASC )
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