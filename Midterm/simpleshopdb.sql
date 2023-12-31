USE [master]
GO
/****** Object:  Database [SimpleShopDB]    Script Date: 11/17/2023 19:35:09 ******/
CREATE DATABASE [SimpleShopDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'SimpleShopDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\SimpleShopDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'SimpleShopDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\SimpleShopDB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [SimpleShopDB] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [SimpleShopDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [SimpleShopDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [SimpleShopDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [SimpleShopDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [SimpleShopDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [SimpleShopDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [SimpleShopDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [SimpleShopDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [SimpleShopDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [SimpleShopDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [SimpleShopDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [SimpleShopDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [SimpleShopDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [SimpleShopDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [SimpleShopDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [SimpleShopDB] SET  ENABLE_BROKER 
GO
ALTER DATABASE [SimpleShopDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [SimpleShopDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [SimpleShopDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [SimpleShopDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [SimpleShopDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [SimpleShopDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [SimpleShopDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [SimpleShopDB] SET RECOVERY FULL 
GO
ALTER DATABASE [SimpleShopDB] SET  MULTI_USER 
GO
ALTER DATABASE [SimpleShopDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [SimpleShopDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [SimpleShopDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [SimpleShopDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [SimpleShopDB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [SimpleShopDB] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'SimpleShopDB', N'ON'
GO
ALTER DATABASE [SimpleShopDB] SET QUERY_STORE = OFF
GO
USE [SimpleShopDB]
GO
/****** Object:  UserDefinedFunction [dbo].[CalculateOrderTotal]    Script Date: 11/17/2023 19:35:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[CalculateOrderTotal]
(
	@ProductID int,
	@Quantity int
)
RETURNS money
AS
BEGIN
	DECLARE @ProductPrice money
	SET @ProductPrice = (SELECT TOP(1) Price FROM [Products] 
						WHERE ID = @ProductID)
	IF(@ProductPrice IS NOT NULL)
	BEGIN
		RETURN
		(
			@ProductPrice * @Quantity
		)
	END

	RETURN (NULL)
END
GO
/****** Object:  Table [dbo].[Orders]    Script Date: 11/17/2023 19:35:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Orders](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Quantity] [int] NULL,
	[Total] [money] NULL,
	[CustomerID] [int] NULL,
	[ProductID] [int] NULL,
	[OrderDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  UserDefinedFunction [dbo].[GetTop5RecentOrders]    Script Date: 11/17/2023 19:35:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[GetTop5RecentOrders]
(
)
RETURNS TABLE 
AS
RETURN 
(	
	SELECT TOP(5) * FROM Orders
	ORDER BY OrderDate DESC
)
GO
/****** Object:  UserDefinedFunction [dbo].[GetOrdersByDates]    Script Date: 11/17/2023 19:35:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[GetOrdersByDates]
(
	@StartDate datetime,
	@EndDate datetime
)
RETURNS TABLE 
AS
RETURN 
(	
	SELECT * FROM Orders
	WHERE OrderDate BETWEEN @StartDate AND @EndDate
)
GO
/****** Object:  Table [dbo].[Customers]    Script Date: 11/17/2023 19:35:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customers](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[City] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Products]    Script Date: 11/17/2023 19:35:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Price] [money] NULL,
	[ExpireDate] [datetime] NULL,
	[DateAdded] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Customers] ON 

INSERT [dbo].[Customers] ([ID], [Name], [City]) VALUES (1, N'Lasha', N'Tbilisi')
INSERT [dbo].[Customers] ([ID], [Name], [City]) VALUES (2, N'Misha', N'Tbilisi')
SET IDENTITY_INSERT [dbo].[Customers] OFF
GO
SET IDENTITY_INSERT [dbo].[Orders] ON 

INSERT [dbo].[Orders] ([ID], [Quantity], [Total], [CustomerID], [ProductID], [OrderDate]) VALUES (1, 2, 4.0000, 1, 1, CAST(N'2022-12-28T22:21:08.587' AS DateTime))
INSERT [dbo].[Orders] ([ID], [Quantity], [Total], [CustomerID], [ProductID], [OrderDate]) VALUES (2, 10, 20.0000, 1, 1, CAST(N'2022-12-28T22:21:29.920' AS DateTime))
INSERT [dbo].[Orders] ([ID], [Quantity], [Total], [CustomerID], [ProductID], [OrderDate]) VALUES (3, 5, 10.0000, 2, 2, CAST(N'2022-12-28T22:21:56.053' AS DateTime))
INSERT [dbo].[Orders] ([ID], [Quantity], [Total], [CustomerID], [ProductID], [OrderDate]) VALUES (4, 2, 4.0000, 2, 2, CAST(N'2022-12-28T22:22:11.050' AS DateTime))
SET IDENTITY_INSERT [dbo].[Orders] OFF
GO
SET IDENTITY_INSERT [dbo].[Products] ON 

INSERT [dbo].[Products] ([ID], [Name], [Price], [ExpireDate], [DateAdded]) VALUES (1, N'Coca Cola', 2.0000, CAST(N'2023-01-01T00:00:00.000' AS DateTime), CAST(N'2022-12-28T22:19:28.987' AS DateTime))
INSERT [dbo].[Products] ([ID], [Name], [Price], [ExpireDate], [DateAdded]) VALUES (2, N'Sprite', 2.3000, CAST(N'2023-02-01T00:00:00.000' AS DateTime), CAST(N'2022-12-28T22:19:46.170' AS DateTime))
INSERT [dbo].[Products] ([ID], [Name], [Price], [ExpireDate], [DateAdded]) VALUES (3, N'Alpen Gold', 3.5400, CAST(N'2022-12-22T00:00:00.000' AS DateTime), CAST(N'2022-12-28T22:20:18.660' AS DateTime))
INSERT [dbo].[Products] ([ID], [Name], [Price], [ExpireDate], [DateAdded]) VALUES (4, N'test', NULL, CAST(N'2023-11-20T00:00:00.000' AS DateTime), NULL)
SET IDENTITY_INSERT [dbo].[Products] OFF
GO
ALTER TABLE [dbo].[Orders] ADD  DEFAULT (getdate()) FOR [OrderDate]
GO
ALTER TABLE [dbo].[Products] ADD  DEFAULT (getdate()) FOR [DateAdded]
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD FOREIGN KEY([CustomerID])
REFERENCES [dbo].[Customers] ([ID])
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD FOREIGN KEY([ProductID])
REFERENCES [dbo].[Products] ([ID])
GO
/****** Object:  StoredProcedure [dbo].[OrderIU]    Script Date: 11/17/2023 19:35:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[OrderIU]
	@OrderID int,
	@CustomerID int,
	@ProductID int,
	@Quantity int
AS
BEGIN

	DECLARE @IsUpdate bit = 0
	IF(@OrderID IS NOT NULL AND (SELECT 1 FROM [Products] WHERE ID = @ProductID) = 1)
	BEGIN
		SET @IsUpdate = 1
	END

	DECLARE @ProductPrice money
	SET @ProductPrice = (SELECT TOP(1) Price FROM [Products] 
						WHERE ID = @ProductID)

	IF(@IsUpdate = 0)
	BEGIN
		INSERT INTO Orders 
		(
			[Quantity],
			[Total],
			[CustomerID],
			[ProductID]
		)
		VALUES 
		(
			@Quantity,
			@ProductPrice * @Quantity,
			@CustomerID,
			@ProductID
		)
	END
	ELSE IF(@IsUpdate = 1)
	BEGIN
		UPDATE Orders SET
		[Quantity] = ISNULL(@Quantity, [Quantity]),
		[Total] = IIF(@Quantity IS NULL OR @ProductPrice IS NULL, [Total], @Quantity * @ProductPrice),
		[CustomerID] = ISNULL(@CustomerID, [CustomerID]),
		[ProductID] = ISNULL(@ProductID, [ProductID])
		WHERE ID = @OrderID
	END

END
GO
USE [master]
GO
ALTER DATABASE [SimpleShopDB] SET  READ_WRITE 
GO
