USE [Sales]
GO
/****** Object:  Table [dbo].[Sales]    Script Date: 15-Mar-22 12:10:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Sales](
	[DealNumber] [int] NOT NULL,
	[CustomerName] [varchar](100) NOT NULL,
	[DealerShipName] [varchar](100) NOT NULL,
	[Vehicle] [varchar](100) NULL,
	[Price] [float] NULL,
	[Date] [date] NULL,
 CONSTRAINT [PK_Sales] PRIMARY KEY CLUSTERED 
(
	[DealNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
