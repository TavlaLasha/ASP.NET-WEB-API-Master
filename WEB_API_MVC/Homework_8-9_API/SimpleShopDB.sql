USE [master]
GO
/****** Object:  Database [SimpleShopDB]    Script Date: 12/29/2023 02:25:38 ******/
CREATE DATABASE [SimpleShopDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'SimpleShopDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\SimpleShopDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'SimpleShopDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\SimpleShopDB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
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
ALTER DATABASE [SimpleShopDB] SET  DISABLE_BROKER 
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
/****** Object:  UserDefinedFunction [dbo].[CalculateOrderTotal]    Script Date: 12/29/2023 02:25:39 ******/
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
/****** Object:  Table [dbo].[Orders]    Script Date: 12/29/2023 02:25:39 ******/
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
/****** Object:  UserDefinedFunction [dbo].[GetTop5RecentOrders]    Script Date: 12/29/2023 02:25:39 ******/
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
/****** Object:  UserDefinedFunction [dbo].[GetOrdersByDates]    Script Date: 12/29/2023 02:25:39 ******/
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
/****** Object:  Table [dbo].[__MigrationHistory]    Script Date: 12/29/2023 02:25:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__MigrationHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ContextKey] [nvarchar](300) NOT NULL,
	[Model] [varbinary](max) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK_dbo.__MigrationHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC,
	[ContextKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 12/29/2023 02:25:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoles](
	[Id] [nvarchar](128) NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 12/29/2023 02:25:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 12/29/2023 02:25:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](128) NOT NULL,
	[ProviderKey] [nvarchar](128) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC,
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 12/29/2023 02:25:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [nvarchar](128) NOT NULL,
	[RoleId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 12/29/2023 02:25:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](128) NOT NULL,
	[Email] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEndDateUtc] [datetime] NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
	[UserName] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customers]    Script Date: 12/29/2023 02:25:39 ******/
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
/****** Object:  Table [dbo].[ProductCategories]    Script Date: 12/29/2023 02:25:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductCategories](
	[ProductCategoryID] [int] IDENTITY(1,1) NOT NULL,
	[ProductCategoryParentID] [int] NULL,
	[ProductCategoryName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_ProductCategories] PRIMARY KEY CLUSTERED 
(
	[ProductCategoryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Products]    Script Date: 12/29/2023 02:25:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ProductCategoryID] [int] NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Price] [money] NULL,
	[ExpireDate] [datetime] NULL,
	[DateAdded] [datetime] NULL,
 CONSTRAINT [PK__Products__3214EC27D69F17E5] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[__MigrationHistory] ([MigrationId], [ContextKey], [Model], [ProductVersion]) VALUES (N'202312282027227_InitialCreate', N'Homework_8_9_API.Models.ApplicationDbContext', 0x1F8B0800000000000400DD5CDB6EDC36107D2FD07F10F4D416CECA9726488CDD14EEDA6E8DC617649DA06F0657E2AE8548942A51AE8D225FD6877E527FA14389BAF1A2CBAEBCBB2E02042B727866381C92C3E1D0FFFEFDCFF8A747DF331E7014BB01999807A37DD3C0C40E1C972C27664217AFDE9A3FBDFFF69BF199E33F1A9F73BA2346072D493C31EF290D8F2D2BB6EFB18FE291EFDA5110070B3AB203DF424E601DEEEFBFB30E0E2C0C10266019C6F86342A8EBE3F4033EA701B1714813E45D060EF6625E0E35B314D5B8423E8E4364E389F96BE0E33F83E8CBDDDBBB7777273717A3AC89699C782E027166D85B98062224A08882B0C79F623CA3514096B3100A9077FB1462A05B202FC6BC13C72579D7FEEC1FB2FE5865C31CCA4E621AF83D010F8EB8822CB1F94A6A360B05820ACF40D5F489F53A55E3C4BC70705AF431F0400122C3E3A91731E2897959B03889C32B4C4779C35106791EA16C284655C43DA373BBBDC2A00E47FBECDF9E314D3C9A447842704223E4ED1937C9DC73EDDFF0D36DF00593C9D1C17C71F4F6F51BE41CBDF9111FBDAEF614FA0A74B50228BA89821047201B5E14FD370DABDECE121B16CD2A6D32AD802DC1DC308D4BF4F8019325BD875973F8D634CEDD47ECE425DCB83E1117A61234A251029F5789E7A1B9878B7AAB9127FBBF81EBE1EB378370BD420FEE321D7A813F4C9C08E6D547ECA5B5F1BD1B66D3AB36DE779CEC3C0A7CF65DB7AFACF66E162491CD3A1368496E51B4C4B42EDDD82A8DB7934933A8E1CD3A47DD7DD36692CAE6AD24651D5A6526E42C363D1B72799F976F678B3B094318BCD4B498469A0C4EB3638D04883DA3247C9512964674D0D5880874EEFFBC269EF9C8F50658143B7001A764E1463E2E7AF973002688486F996F501CC3B03ABFA2F8BE4174F83980E8336C271198EA8C223F7C766E37F701C157893F67336073BC061B9ADB3F837364D3203A23ACD5DA781F02FB4B90D033E29C228A3F513B07649FB7AEDF1D6010714E6C1BC7F139183376A601F8DC39E005A14787BDE1D82AB56DA764EA21D7577B25C27A7A9793969E899A42F24E34642A0FA549D40FC1D225DD44CD49F5A26614ADA272B2BEA232B06E92724ABDA02941AB9C19D5603E5F3A42C33B7D29ECEE7B7DEB6DDEBAB5A0A2C619AC90F8174C7004CB98738328C5112947A0CBBAB10D67211D3EC6F4D9F7A694D367E42543B35A6936A48BC0F0B32185DDFDD9908A09C50FAEC3BC920E47A19C18E03BD1AB4F59ED734E906CD3D3A1D6CD4D33DFCC1AA09B2E27711CD86E3A0B1441301EC2A8CB0F3E9CD11ECFC87A23C644A06360E82EDBF2A004FA668A46754D4EB18729364EEC2C483845B18D1C598DD021A78760F98EAA10AC8C8DD485FB41E209968E23D608B143500C33D525549E162EB1DD1079AD5A125A76DCC258DF0B1E62CD290E31610C5B35D185B93A14C20428F80883D2A6A1B155B1B86643D478ADBA316F7361CB719722141BB1C916DF596397DC7F7B16C36CD6D8068CB359255D04D086F5B661A0FCACD2D500C483CBAE19A87062D2182877A93662A0758D6DC140EB2A7971069A1D51BB8EBF705EDD35F3AC1F9437BFAD37AA6B0BB659D3C78E9966E67B421B0A2D70249BE7E99C55E247AA389C819CFC7C167357573411063EC3B41EB229FD5DA51F6A35838846D404581A5A0B28BF109480A409D543B83C96D7281DF7227AC0E671B74658BEF60BB0151B90B1AB17A31542FDF5A9689C9D4E1F45CF0A6B908CBCD361A182A3300871F1AA77BC835274715959315D7CE13EDE70A5637C301A14D4E2B96A94947766702DE5A6D9AE259543D6C7255B4B4B82FBA4D152DE99C1B5C46DB45D490AA7A0875BB0968AEA5BF840932D8F7414BB4D5137B6B2A4295E30B634D955E34B14862E5956B2AD788931CB52ADA6AF66FDD38FFC0CC3B263451652216DC18906115A62A1165883A4E76E14D35344D11CB138CFD4F12532E5DEAA59FE7396D5ED531EC47C1FC8A9D9EFAC85EE1ABFB6E1CA1E09073A876EFACCAD4963E90A23503737580A1CF250A408DF4F032FF189DECBD2B7CE2EF1AAEDB31219616C09F24B5E94A432C9D7ADEBBFD3E8C83363C8912A3C99D5474B0FA1D379EE8756B5AEF34DF52879A8AA8AA20B5F6D6DF4742E4DFF11135DC6FE03D68AF03C338CE7A9540178514F8C4AAA830456A9EB8E5ACF46A962D66BBA230A29275548A1AA8794D5C4929A90D58A95F0341A555374E720A79254D1E5DAEEC88AA4922AB4A27A056C85CC625D775445DE49155851DD1DBB4C421157D21DDEC3B40799F536B1ECC0BBDE2EA6C1789E6571984DB072AF5F05AA14F7C4E237F712182FDF4993D29EFAD633A92CD8B19E496930F46B50ED5ABCBE0435DEE5EB316B77DDB565BEE9AE5F8FD7CF709FD53CA4939F4852702F4E80C2496FCC4F5DED8F6DA4635846621AB91A618B7F8A29F6478C6034FBC39B7A2E660B7A4E708988BBC031CDF23BCCC3FD8343E1A9CEEE3C9BB1E2D8F114A756DDDB99FA986D20558B3CA0C8BE47919C38B1C6D39212548A495F10073F4ECCBFD256C7697883FD4A8BF78C8BF81371FF48A0E2364AB0F1554E041D26D5BEC3E38E42D0AF2FE2D54477955FFC7E9735DD33AE23984EC7C6BEA0E85586BFFE96A2973459D335A459F985C5CB9D6DB5470B4A5461B6ACFE4661EED241DE27E4527EE7A3C7EFFB8AA67C83B016A2E29DC1507883A850F78E60152CED1B02073E69FA86A05F67D56F0A56114DFB9EC025FDC1C4D704DD97A1BCE516F721C5A969134B52AAE7D66CECB55233B7BD374949DB6B4D743931BB07DCA0C9D7EBB9282F2CA979B0AD5391B33C18F636EDFED91395772537B974DAB79B92BCC92CE486FBA5FF55F2F10EA4CB29D27FB69F62BC695BD38581773C4FB35F22F18E191BDFE6B79F2EBC6963D3058877DCD87A2505EF98AD6D6BFFDCB2A575DE42B79EE22B672B69AE735451E4B614DE2CE40EC7FF790046907994D9CB4B75CE5853BE6B0BC39244CF549FAC263296268EC457A26866DBAFAF7CC36FEC2CA76966AB49F16CE2CDD7FF46DE9CA699B72671721BC9C7CAD445554278CB3AD6944DF592928D6B3D69C96D6FF3591BEFE65F526EF1204AA9CD1ECDEDF2CB49251E4425434E9D1EA9C3F24531EC9D95BFDD08FB77EC2E4B08F6971C09B66BBB664173411641BE790B12E5244284E61253E4C0967A125177816C0AD52C009D3E1D4F837AEC1A648E9D0B729DD030A1D065ECCFBD5AC08B39014DFCD3FCE8BACCE3EB907DC5437401C47459E0FE9AFC9CB89E53C87DAE880969209877C1C3BD6C2C290BFB2E9F0AA4AB807404E2EA2B9CA25BEC871E80C5D764861EF02AB281F97DC04B643F9511401D48FB40D4D53E3E75D132427ECC31CAF6F00936ECF88FEFFF03D322F671C2540000, N'6.4.4')
GO
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'7dcea264-b80a-471f-8455-63d7faec480d', N'lashatavlalashvili@yahoo.com', 0, N'AEJtk3NpiOIb6irc1bIc1H4XyCXcQ6qM79ggTVHBIkdED8kNqHMTt/GI2znL72e4LA==', N'08a2a434-9c7f-4c00-be7b-218434423e7e', NULL, 0, 0, NULL, 0, 0, N'lashatavlalashvili@yahoo.com')
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
SET IDENTITY_INSERT [dbo].[ProductCategories] ON 

INSERT [dbo].[ProductCategories] ([ProductCategoryID], [ProductCategoryParentID], [ProductCategoryName]) VALUES (1, NULL, N'Beverages')
INSERT [dbo].[ProductCategories] ([ProductCategoryID], [ProductCategoryParentID], [ProductCategoryName]) VALUES (2, NULL, N'Food')
INSERT [dbo].[ProductCategories] ([ProductCategoryID], [ProductCategoryParentID], [ProductCategoryName]) VALUES (3, 1, N'Alcohol')
INSERT [dbo].[ProductCategories] ([ProductCategoryID], [ProductCategoryParentID], [ProductCategoryName]) VALUES (4, 1, N'Fizzy Drinks')
INSERT [dbo].[ProductCategories] ([ProductCategoryID], [ProductCategoryParentID], [ProductCategoryName]) VALUES (5, 2, N'Cereal')
INSERT [dbo].[ProductCategories] ([ProductCategoryID], [ProductCategoryParentID], [ProductCategoryName]) VALUES (6, 2, N'Meats')
INSERT [dbo].[ProductCategories] ([ProductCategoryID], [ProductCategoryParentID], [ProductCategoryName]) VALUES (7, 2, N'Vegetables')
INSERT [dbo].[ProductCategories] ([ProductCategoryID], [ProductCategoryParentID], [ProductCategoryName]) VALUES (8, 2, N'Semi-Finished Food')
INSERT [dbo].[ProductCategories] ([ProductCategoryID], [ProductCategoryParentID], [ProductCategoryName]) VALUES (9, 2, N'Sweets')
SET IDENTITY_INSERT [dbo].[ProductCategories] OFF
GO
SET IDENTITY_INSERT [dbo].[Products] ON 

INSERT [dbo].[Products] ([ID], [ProductCategoryID], [Name], [Price], [ExpireDate], [DateAdded]) VALUES (1, 4, N'Coca Cola', 2.0000, CAST(N'2023-01-01T00:00:00.000' AS DateTime), CAST(N'2022-12-28T22:19:28.987' AS DateTime))
INSERT [dbo].[Products] ([ID], [ProductCategoryID], [Name], [Price], [ExpireDate], [DateAdded]) VALUES (2, 4, N'Sprite', 2.3000, CAST(N'2023-02-01T00:00:00.000' AS DateTime), CAST(N'2022-12-28T22:19:46.170' AS DateTime))
INSERT [dbo].[Products] ([ID], [ProductCategoryID], [Name], [Price], [ExpireDate], [DateAdded]) VALUES (3, 9, N'Alpen Gold', 3.5400, CAST(N'2022-12-22T00:00:00.000' AS DateTime), CAST(N'2022-12-28T22:20:18.660' AS DateTime))
INSERT [dbo].[Products] ([ID], [ProductCategoryID], [Name], [Price], [ExpireDate], [DateAdded]) VALUES (4, 3, N'test', NULL, CAST(N'2023-11-20T00:00:00.000' AS DateTime), NULL)
SET IDENTITY_INSERT [dbo].[Products] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [RoleNameIndex]    Script Date: 12/29/2023 02:25:39 ******/
CREATE UNIQUE NONCLUSTERED INDEX [RoleNameIndex] ON [dbo].[AspNetRoles]
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_UserId]    Script Date: 12/29/2023 02:25:39 ******/
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserClaims]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_UserId]    Script Date: 12/29/2023 02:25:39 ******/
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserLogins]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_RoleId]    Script Date: 12/29/2023 02:25:39 ******/
CREATE NONCLUSTERED INDEX [IX_RoleId] ON [dbo].[AspNetUserRoles]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_UserId]    Script Date: 12/29/2023 02:25:39 ******/
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserRoles]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UserNameIndex]    Script Date: 12/29/2023 02:25:39 ******/
CREATE UNIQUE NONCLUSTERED INDEX [UserNameIndex] ON [dbo].[AspNetUsers]
(
	[UserName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Orders] ADD  DEFAULT (getdate()) FOR [OrderDate]
GO
ALTER TABLE [dbo].[Products] ADD  CONSTRAINT [DF__Products__DateAd__24927208]  DEFAULT (getdate()) FOR [DateAdded]
GO
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD FOREIGN KEY([CustomerID])
REFERENCES [dbo].[Customers] ([ID])
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK__Orders__ProductI__2A4B4B5E] FOREIGN KEY([ProductID])
REFERENCES [dbo].[Products] ([ID])
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [FK__Orders__ProductI__2A4B4B5E]
GO
ALTER TABLE [dbo].[Products]  WITH CHECK ADD  CONSTRAINT [FK_Products_ProductCategories] FOREIGN KEY([ProductCategoryID])
REFERENCES [dbo].[ProductCategories] ([ProductCategoryID])
GO
ALTER TABLE [dbo].[Products] CHECK CONSTRAINT [FK_Products_ProductCategories]
GO
/****** Object:  StoredProcedure [dbo].[OrderIU]    Script Date: 12/29/2023 02:25:39 ******/
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
