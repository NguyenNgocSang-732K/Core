USE [master]
GO
/****** Object:  Database [Project]    Script Date: 13/05/2020 11:18:41 SA ******/
CREATE DATABASE [Project]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Project', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\Project.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Project_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\Project_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [Project] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Project].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Project] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Project] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Project] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Project] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Project] SET ARITHABORT OFF 
GO
ALTER DATABASE [Project] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Project] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Project] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Project] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Project] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Project] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Project] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Project] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Project] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Project] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Project] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Project] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Project] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Project] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Project] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Project] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Project] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Project] SET RECOVERY FULL 
GO
ALTER DATABASE [Project] SET  MULTI_USER 
GO
ALTER DATABASE [Project] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Project] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Project] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Project] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Project] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'Project', N'ON'
GO
ALTER DATABASE [Project] SET QUERY_STORE = OFF
GO
USE [Project]
GO
/****** Object:  Table [dbo].[category]    Script Date: 13/05/2020 11:18:41 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[category](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](50) NOT NULL,
	[status] [bit] NOT NULL,
 CONSTRAINT [PK_category] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[invoice]    Script Date: 13/05/2020 11:18:41 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[invoice](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[code] [nvarchar](50) NOT NULL,
	[cusid] [int] NOT NULL,
	[empid] [int] NOT NULL,
	[daycreate] [datetime] NOT NULL,
	[total] [decimal](18, 0) NOT NULL,
	[status] [bit] NOT NULL,
 CONSTRAINT [PK_invoice] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[invoice_detail]    Script Date: 13/05/2020 11:18:41 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[invoice_detail](
	[invoiceid] [int] NOT NULL,
	[proid] [int] NOT NULL,
	[price] [decimal](18, 0) NOT NULL,
	[quantity] [int] NOT NULL,
 CONSTRAINT [PK_invoice_detail] PRIMARY KEY CLUSTERED 
(
	[invoiceid] ASC,
	[proid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[product]    Script Date: 13/05/2020 11:18:41 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[product](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](200) NOT NULL,
	[code] [nvarchar](100) NOT NULL,
	[price] [decimal](18, 0) NOT NULL,
	[quantity] [int] NOT NULL,
	[subcateid] [int] NOT NULL,
	[unitid] [int] NOT NULL,
	[description] [nvarchar](max) NULL,
	[daycreate] [datetime] NOT NULL,
	[dayedited] [datetime] NOT NULL,
	[color] [nvarchar](200) NULL,
	[editer_id] [int] NULL,
	[active] [bit] NOT NULL,
	[status] [bit] NOT NULL,
 CONSTRAINT [PK_product] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[product_photo]    Script Date: 13/05/2020 11:18:41 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[product_photo](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[photo] [nvarchar](100) NOT NULL,
	[proid] [int] NOT NULL,
	[main] [bit] NOT NULL,
 CONSTRAINT [PK_product_photo] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[sub_category]    Script Date: 13/05/2020 11:18:41 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[sub_category](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](50) NOT NULL,
	[cateid] [int] NULL,
	[status] [bit] NOT NULL,
 CONSTRAINT [PK_sub_category] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[unit]    Script Date: 13/05/2020 11:18:41 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[unit](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](50) NOT NULL,
	[nameconvert] [nvarchar](50) NOT NULL,
	[quantity] [int] NOT NULL,
	[status] [bit] NOT NULL,
 CONSTRAINT [PK_unit] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[user]    Script Date: 13/05/2020 11:18:41 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[user](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[email] [nvarchar](50) NOT NULL,
	[password] [nvarchar](max) NOT NULL,
	[name] [nvarchar](50) NOT NULL,
	[birthday] [date] NULL,
	[address] [nvarchar](200) NULL,
	[phone] [nchar](11) NULL,
	[photo] [nvarchar](50) NULL,
	[gender] [tinyint] NULL,
	[day_create] [datetime] NOT NULL,
	[day_edited] [datetime] NOT NULL,
	[editer_id] [int] NULL,
	[status] [bit] NOT NULL,
	[role] [tinyint] NOT NULL,
	[active] [bit] NOT NULL,
	[description] [nvarchar](max) NULL,
	[forgotpw] [nvarchar](50) NULL,
 CONSTRAINT [PK_user_1] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[category] ON 

INSERT [dbo].[category] ([id], [name], [status]) VALUES (1, N'Điện tử', 1)
INSERT [dbo].[category] ([id], [name], [status]) VALUES (2, N'May mặc', 1)
INSERT [dbo].[category] ([id], [name], [status]) VALUES (3, N'Giày dép', 1)
INSERT [dbo].[category] ([id], [name], [status]) VALUES (4, N'Thực phẩm', 1)
SET IDENTITY_INSERT [dbo].[category] OFF
GO
SET IDENTITY_INSERT [dbo].[invoice] ON 

INSERT [dbo].[invoice] ([id], [code], [cusid], [empid], [daycreate], [total], [status]) VALUES (1, N'HD01', 4, 2, CAST(N'2020-05-11T00:52:50.320' AS DateTime), CAST(0 AS Decimal(18, 0)), 1)
INSERT [dbo].[invoice] ([id], [code], [cusid], [empid], [daycreate], [total], [status]) VALUES (2, N'HD02', 4, 2, CAST(N'2020-05-11T00:52:59.510' AS DateTime), CAST(0 AS Decimal(18, 0)), 1)
INSERT [dbo].[invoice] ([id], [code], [cusid], [empid], [daycreate], [total], [status]) VALUES (3, N'HD03', 5, 2, CAST(N'2020-05-11T00:53:08.017' AS DateTime), CAST(0 AS Decimal(18, 0)), 1)
INSERT [dbo].[invoice] ([id], [code], [cusid], [empid], [daycreate], [total], [status]) VALUES (4, N'HD04', 6, 2, CAST(N'2020-05-11T00:53:17.670' AS DateTime), CAST(0 AS Decimal(18, 0)), 1)
INSERT [dbo].[invoice] ([id], [code], [cusid], [empid], [daycreate], [total], [status]) VALUES (6, N'HD05', 7, 3, CAST(N'2020-05-11T00:54:01.860' AS DateTime), CAST(0 AS Decimal(18, 0)), 1)
INSERT [dbo].[invoice] ([id], [code], [cusid], [empid], [daycreate], [total], [status]) VALUES (7, N'HD06', 8, 3, CAST(N'2020-05-11T00:54:09.207' AS DateTime), CAST(0 AS Decimal(18, 0)), 1)
SET IDENTITY_INSERT [dbo].[invoice] OFF
GO
INSERT [dbo].[invoice_detail] ([invoiceid], [proid], [price], [quantity]) VALUES (1, 4, CAST(1300 AS Decimal(18, 0)), 4)
INSERT [dbo].[invoice_detail] ([invoiceid], [proid], [price], [quantity]) VALUES (1, 5, CAST(1300 AS Decimal(18, 0)), 4)
INSERT [dbo].[invoice_detail] ([invoiceid], [proid], [price], [quantity]) VALUES (1, 7, CAST(2100 AS Decimal(18, 0)), 5)
INSERT [dbo].[invoice_detail] ([invoiceid], [proid], [price], [quantity]) VALUES (2, 1, CAST(4300 AS Decimal(18, 0)), 4)
INSERT [dbo].[invoice_detail] ([invoiceid], [proid], [price], [quantity]) VALUES (2, 4, CAST(1000 AS Decimal(18, 0)), 1)
INSERT [dbo].[invoice_detail] ([invoiceid], [proid], [price], [quantity]) VALUES (3, 7, CAST(1300 AS Decimal(18, 0)), 2)
INSERT [dbo].[invoice_detail] ([invoiceid], [proid], [price], [quantity]) VALUES (3, 8, CAST(1000 AS Decimal(18, 0)), 3)
INSERT [dbo].[invoice_detail] ([invoiceid], [proid], [price], [quantity]) VALUES (4, 1, CAST(1300 AS Decimal(18, 0)), 3)
INSERT [dbo].[invoice_detail] ([invoiceid], [proid], [price], [quantity]) VALUES (4, 9, CAST(1200 AS Decimal(18, 0)), 3)
INSERT [dbo].[invoice_detail] ([invoiceid], [proid], [price], [quantity]) VALUES (4, 10, CAST(1200 AS Decimal(18, 0)), 4)
INSERT [dbo].[invoice_detail] ([invoiceid], [proid], [price], [quantity]) VALUES (6, 9, CAST(1500 AS Decimal(18, 0)), 1)
INSERT [dbo].[invoice_detail] ([invoiceid], [proid], [price], [quantity]) VALUES (6, 10, CAST(1400 AS Decimal(18, 0)), 3)
INSERT [dbo].[invoice_detail] ([invoiceid], [proid], [price], [quantity]) VALUES (7, 1, CAST(15000 AS Decimal(18, 0)), 2)
INSERT [dbo].[invoice_detail] ([invoiceid], [proid], [price], [quantity]) VALUES (7, 2, CAST(1000 AS Decimal(18, 0)), 3)
INSERT [dbo].[invoice_detail] ([invoiceid], [proid], [price], [quantity]) VALUES (7, 3, CAST(1400 AS Decimal(18, 0)), 4)
GO
SET IDENTITY_INSERT [dbo].[product] ON 

INSERT [dbo].[product] ([id], [name], [code], [price], [quantity], [subcateid], [unitid], [description], [daycreate], [dayedited], [color], [editer_id], [active], [status]) VALUES (1, N'Laptop ASUS ROG Strix G', N'G531GT-AL017T', CAST(27990 AS Decimal(18, 0)), 27, 1, 1, NULL, CAST(N'2020-05-10T23:56:05.217' AS DateTime), CAST(N'2020-05-10T23:56:05.217' AS DateTime), NULL, NULL, 1, 1)
INSERT [dbo].[product] ([id], [name], [code], [price], [quantity], [subcateid], [unitid], [description], [daycreate], [dayedited], [color], [editer_id], [active], [status]) VALUES (2, N'Laptop Acer Nitro 5', N'AN515-54-71HS', CAST(24790 AS Decimal(18, 0)), 43, 1, 1, NULL, CAST(N'2020-05-10T23:57:41.673' AS DateTime), CAST(N'2020-05-10T23:57:41.673' AS DateTime), NULL, NULL, 1, 1)
INSERT [dbo].[product] ([id], [name], [code], [price], [quantity], [subcateid], [unitid], [description], [daycreate], [dayedited], [color], [editer_id], [active], [status]) VALUES (3, N'Laptop Dell G7', N'7588-N7588A', CAST(30790 AS Decimal(18, 0)), 23, 1, 1, NULL, CAST(N'2020-05-10T23:58:28.000' AS DateTime), CAST(N'2020-05-10T23:58:28.000' AS DateTime), NULL, NULL, 1, 1)
INSERT [dbo].[product] ([id], [name], [code], [price], [quantity], [subcateid], [unitid], [description], [daycreate], [dayedited], [color], [editer_id], [active], [status]) VALUES (4, N'Điện thoại Apple iPhone 11 Pro Max 64GB', N'MWHH2VN/A', CAST(29999 AS Decimal(18, 0)), 13, 2, 1, NULL, CAST(N'2020-05-11T00:05:15.190' AS DateTime), CAST(N'2020-05-11T00:05:15.190' AS DateTime), NULL, NULL, 1, 1)
INSERT [dbo].[product] ([id], [name], [code], [price], [quantity], [subcateid], [unitid], [description], [daycreate], [dayedited], [color], [editer_id], [active], [status]) VALUES (5, N'Điện Thoại Samsung Galaxy Note 10 Lite, 128GB', N'SM-N770FZSUXXV', CAST(13990000 AS Decimal(18, 0)), 34, 2, 1, NULL, CAST(N'2020-05-11T00:06:34.387' AS DateTime), CAST(N'2020-05-11T00:06:34.387' AS DateTime), NULL, NULL, 1, 1)
INSERT [dbo].[product] ([id], [name], [code], [price], [quantity], [subcateid], [unitid], [description], [daycreate], [dayedited], [color], [editer_id], [active], [status]) VALUES (7, N'Áo Brazil sân nhà 2020-2021', N'BRAZIL2021', CAST(200000 AS Decimal(18, 0)), 11, 3, 2, NULL, CAST(N'2020-05-11T00:41:52.763' AS DateTime), CAST(N'2020-05-11T00:41:52.763' AS DateTime), NULL, NULL, 1, 1)
INSERT [dbo].[product] ([id], [name], [code], [price], [quantity], [subcateid], [unitid], [description], [daycreate], [dayedited], [color], [editer_id], [active], [status]) VALUES (8, N'Áo Barcelona sân nhà 2020-2021', N'BAR2021', CAST(200000 AS Decimal(18, 0)), 13, 3, 2, NULL, CAST(N'2020-05-11T00:43:23.647' AS DateTime), CAST(N'2020-05-11T00:43:23.647' AS DateTime), NULL, NULL, 1, 1)
INSERT [dbo].[product] ([id], [name], [code], [price], [quantity], [subcateid], [unitid], [description], [daycreate], [dayedited], [color], [editer_id], [active], [status]) VALUES (9, N'Nước ngọt Redbull', N'RB885', CAST(15000 AS Decimal(18, 0)), 6, 5, 3, NULL, CAST(N'2020-05-11T00:45:41.403' AS DateTime), CAST(N'2020-05-11T00:45:41.403' AS DateTime), NULL, NULL, 1, 1)
INSERT [dbo].[product] ([id], [name], [code], [price], [quantity], [subcateid], [unitid], [description], [daycreate], [dayedited], [color], [editer_id], [active], [status]) VALUES (10, N'Bia Sư Tử Trắng King 330ml', N'HCM-BI0023-1
', CAST(10000 AS Decimal(18, 0)), 80, 5, 4, NULL, CAST(N'2020-05-11T00:47:21.313' AS DateTime), CAST(N'2020-05-11T00:47:21.313' AS DateTime), NULL, NULL, 1, 1)
INSERT [dbo].[product] ([id], [name], [code], [price], [quantity], [subcateid], [unitid], [description], [daycreate], [dayedited], [color], [editer_id], [active], [status]) VALUES (11, N'
Snack hương bò thơm cay', N'SBC73', CAST(11000 AS Decimal(18, 0)), 8, 6, 5, NULL, CAST(N'2020-05-11T00:50:51.507' AS DateTime), CAST(N'2020-05-11T00:50:51.507' AS DateTime), NULL, NULL, 1, 1)
SET IDENTITY_INSERT [dbo].[product] OFF
GO
SET IDENTITY_INSERT [dbo].[product_photo] ON 

INSERT [dbo].[product_photo] ([id], [photo], [proid], [main]) VALUES (1, N'1.jpg', 1, 1)
INSERT [dbo].[product_photo] ([id], [photo], [proid], [main]) VALUES (2, N'2.jpg', 2, 1)
INSERT [dbo].[product_photo] ([id], [photo], [proid], [main]) VALUES (3, N'3.jpg', 3, 1)
INSERT [dbo].[product_photo] ([id], [photo], [proid], [main]) VALUES (4, N'4.jpg', 4, 1)
INSERT [dbo].[product_photo] ([id], [photo], [proid], [main]) VALUES (5, N'5.jpg', 5, 1)
INSERT [dbo].[product_photo] ([id], [photo], [proid], [main]) VALUES (7, N'7.jpg', 7, 1)
INSERT [dbo].[product_photo] ([id], [photo], [proid], [main]) VALUES (8, N'8.jpg', 8, 1)
INSERT [dbo].[product_photo] ([id], [photo], [proid], [main]) VALUES (9, N'9.jpg', 9, 1)
INSERT [dbo].[product_photo] ([id], [photo], [proid], [main]) VALUES (10, N'10.jpg', 10, 1)
INSERT [dbo].[product_photo] ([id], [photo], [proid], [main]) VALUES (11, N'11.jpg', 11, 1)
INSERT [dbo].[product_photo] ([id], [photo], [proid], [main]) VALUES (12, N'21.jpg', 1, 1)
INSERT [dbo].[product_photo] ([id], [photo], [proid], [main]) VALUES (13, N'31.jpg', 1, 1)
INSERT [dbo].[product_photo] ([id], [photo], [proid], [main]) VALUES (14, N'32.jpg', 1, 1)
SET IDENTITY_INSERT [dbo].[product_photo] OFF
GO
SET IDENTITY_INSERT [dbo].[sub_category] ON 

INSERT [dbo].[sub_category] ([id], [name], [cateid], [status]) VALUES (1, N'Laptop', 1, 1)
INSERT [dbo].[sub_category] ([id], [name], [cateid], [status]) VALUES (2, N'Điện thoại', 1, 1)
INSERT [dbo].[sub_category] ([id], [name], [cateid], [status]) VALUES (3, N'Đồ thể thao', 2, 1)
INSERT [dbo].[sub_category] ([id], [name], [cateid], [status]) VALUES (4, N'Giày thể thao', 3, 1)
INSERT [dbo].[sub_category] ([id], [name], [cateid], [status]) VALUES (5, N'Nước uống', 4, 1)
INSERT [dbo].[sub_category] ([id], [name], [cateid], [status]) VALUES (6, N'Đồ ăn nhanh', 4, 1)
SET IDENTITY_INSERT [dbo].[sub_category] OFF
GO
SET IDENTITY_INSERT [dbo].[unit] ON 

INSERT [dbo].[unit] ([id], [name], [nameconvert], [quantity], [status]) VALUES (1, N'cái', N'cái', 1, 1)
INSERT [dbo].[unit] ([id], [name], [nameconvert], [quantity], [status]) VALUES (2, N'bộ', N'bộ', 1, 1)
INSERT [dbo].[unit] ([id], [name], [nameconvert], [quantity], [status]) VALUES (3, N'lon', N'lốc', 6, 1)
INSERT [dbo].[unit] ([id], [name], [nameconvert], [quantity], [status]) VALUES (4, N'lon', N'thùng', 24, 1)
INSERT [dbo].[unit] ([id], [name], [nameconvert], [quantity], [status]) VALUES (5, N'bịch', N'bịch', 1, 1)
SET IDENTITY_INSERT [dbo].[unit] OFF
GO
SET IDENTITY_INSERT [dbo].[user] ON 

INSERT [dbo].[user] ([id], [email], [password], [name], [birthday], [address], [phone], [photo], [gender], [day_create], [day_edited], [editer_id], [status], [role], [active], [description], [forgotpw]) VALUES (1, N'sang.nn281@aptechlearning.edu.vn', N'202cb962ac59075b964b07152d234b70', N'Nguyễn Ngọc Sáng', CAST(N'2000-03-07' AS Date), N'Tây Ninh', N'0868946944 ', N'637244626944009907duii.jpg', 1, CAST(N'2020-04-16T00:00:00.000' AS DateTime), CAST(N'2020-04-16T00:00:00.000' AS DateTime), NULL, 1, 0, 1, N'Description', N'788d5696')
INSERT [dbo].[user] ([id], [email], [password], [name], [birthday], [address], [phone], [photo], [gender], [day_create], [day_edited], [editer_id], [status], [role], [active], [description], [forgotpw]) VALUES (2, N'nhu.nth12473@aptechlearning.edu.vn', N'202cb962ac59075b964b07152d234b70', N'Nguyễn Thị Huỳnh Như', CAST(N'2000-04-12' AS Date), N'Tây Ninh', N'0345345345 ', N'HuynhNhu2.jpg', 0, CAST(N'2020-04-16T00:00:00.000' AS DateTime), CAST(N'2020-04-16T00:00:00.000' AS DateTime), NULL, 1, 1, 1, N'Description', NULL)
INSERT [dbo].[user] ([id], [email], [password], [name], [birthday], [address], [phone], [photo], [gender], [day_create], [day_edited], [editer_id], [status], [role], [active], [description], [forgotpw]) VALUES (3, N'ace.73@aptechlearning.edu.vn', N'202cb962ac59075b964b07152d234b70', N'D Ace', CAST(N'2000-01-01' AS Date), N'Japane', N'0987654321 ', N'HuynhNhu2.jpg', 1, CAST(N'2020-04-16T00:00:00.000' AS DateTime), CAST(N'2020-04-16T00:00:00.000' AS DateTime), NULL, 1, 1, 1, N'Description', NULL)
INSERT [dbo].[user] ([id], [email], [password], [name], [birthday], [address], [phone], [photo], [gender], [day_create], [day_edited], [editer_id], [status], [role], [active], [description], [forgotpw]) VALUES (4, N'test 1', N'202cb962ac59075b964b07152d234b70', N'Test 1', CAST(N'2000-01-01' AS Date), N'Test', N'0987654321 ', N'HuynhNhu2.jpg', 1, CAST(N'2020-04-16T00:00:00.000' AS DateTime), CAST(N'2020-04-16T00:00:00.000' AS DateTime), NULL, 1, 2, 1, N'Description', NULL)
INSERT [dbo].[user] ([id], [email], [password], [name], [birthday], [address], [phone], [photo], [gender], [day_create], [day_edited], [editer_id], [status], [role], [active], [description], [forgotpw]) VALUES (5, N'test 2', N'202cb962ac59075b964b07152d234b70', N'Test 2', CAST(N'2000-01-01' AS Date), N'Test', N'0987654321 ', N'HuynhNhu2.jpg', 1, CAST(N'2020-04-16T00:00:00.000' AS DateTime), CAST(N'2020-04-16T00:00:00.000' AS DateTime), NULL, 1, 2, 1, N'Description', NULL)
INSERT [dbo].[user] ([id], [email], [password], [name], [birthday], [address], [phone], [photo], [gender], [day_create], [day_edited], [editer_id], [status], [role], [active], [description], [forgotpw]) VALUES (6, N'test 3', N'202cb962ac59075b964b07152d234b70', N'Test 3', CAST(N'2000-01-01' AS Date), N'Test', N'0987654321 ', N'HuynhNhu2.jpg', 1, CAST(N'2020-04-16T00:00:00.000' AS DateTime), CAST(N'2020-04-16T00:00:00.000' AS DateTime), NULL, 1, 2, 1, N'Description', NULL)
INSERT [dbo].[user] ([id], [email], [password], [name], [birthday], [address], [phone], [photo], [gender], [day_create], [day_edited], [editer_id], [status], [role], [active], [description], [forgotpw]) VALUES (7, N'test 4', N'202cb962ac59075b964b07152d234b70', N'Test 4', CAST(N'2000-01-01' AS Date), N'Test', N'0987654321 ', N'HuynhNhu2.jpg', 1, CAST(N'2020-04-16T00:00:00.000' AS DateTime), CAST(N'2020-04-16T00:00:00.000' AS DateTime), NULL, 1, 2, 1, N'Description', NULL)
INSERT [dbo].[user] ([id], [email], [password], [name], [birthday], [address], [phone], [photo], [gender], [day_create], [day_edited], [editer_id], [status], [role], [active], [description], [forgotpw]) VALUES (8, N'test 5', N'202cb962ac59075b964b07152d234b70', N'Test 5', CAST(N'2000-01-01' AS Date), N'Test', N'0987654321 ', N'HuynhNhu2.jpg', 1, CAST(N'2020-04-16T00:00:00.000' AS DateTime), CAST(N'2020-04-16T00:00:00.000' AS DateTime), NULL, 1, 2, 1, N'Description', NULL)
INSERT [dbo].[user] ([id], [email], [password], [name], [birthday], [address], [phone], [photo], [gender], [day_create], [day_edited], [editer_id], [status], [role], [active], [description], [forgotpw]) VALUES (9, N'test 6', N'202cb962ac59075b964b07152d234b70', N'Test 6', CAST(N'2000-01-01' AS Date), N'Test', N'0987654321 ', N'HuynhNhu2.jpg', 1, CAST(N'2020-04-16T00:00:00.000' AS DateTime), CAST(N'2020-04-16T00:00:00.000' AS DateTime), NULL, 1, 2, 1, N'Description', NULL)
INSERT [dbo].[user] ([id], [email], [password], [name], [birthday], [address], [phone], [photo], [gender], [day_create], [day_edited], [editer_id], [status], [role], [active], [description], [forgotpw]) VALUES (10, N'test 7', N'202cb962ac59075b964b07152d234b70', N'Test 7', CAST(N'2000-01-01' AS Date), N'Test', N'0987654321 ', N'HuynhNhu2.jpg', 1, CAST(N'2020-04-16T00:00:00.000' AS DateTime), CAST(N'2020-04-16T00:00:00.000' AS DateTime), NULL, 1, 2, 1, N'Description', NULL)
INSERT [dbo].[user] ([id], [email], [password], [name], [birthday], [address], [phone], [photo], [gender], [day_create], [day_edited], [editer_id], [status], [role], [active], [description], [forgotpw]) VALUES (11, N'test 8', N'202cb962ac59075b964b07152d234b70', N'Test 8', CAST(N'2000-01-01' AS Date), N'Test', N'0987654321 ', N'HuynhNhu2.jpg', 1, CAST(N'2020-04-16T00:00:00.000' AS DateTime), CAST(N'2020-04-16T00:00:00.000' AS DateTime), NULL, 1, 2, 1, N'Description', NULL)
INSERT [dbo].[user] ([id], [email], [password], [name], [birthday], [address], [phone], [photo], [gender], [day_create], [day_edited], [editer_id], [status], [role], [active], [description], [forgotpw]) VALUES (12, N'test 9', N'202cb962ac59075b964b07152d234b70', N'Test 9', CAST(N'2000-01-01' AS Date), N'Test', N'0987654321 ', N'HuynhNhu2.jpg', 1, CAST(N'2020-04-16T00:00:00.000' AS DateTime), CAST(N'2020-04-16T00:00:00.000' AS DateTime), NULL, 0, 2, 1, N'Description', NULL)
INSERT [dbo].[user] ([id], [email], [password], [name], [birthday], [address], [phone], [photo], [gender], [day_create], [day_edited], [editer_id], [status], [role], [active], [description], [forgotpw]) VALUES (13, N'test 10', N'202cb962ac59075b964b07152d234b70', N'Test 10', CAST(N'2000-01-01' AS Date), N'Test', N'0987654321 ', N'HuynhNhu2.jpg', 1, CAST(N'2020-04-16T00:00:00.000' AS DateTime), CAST(N'2020-04-16T00:00:00.000' AS DateTime), NULL, 0, 2, 1, N'Description', NULL)
INSERT [dbo].[user] ([id], [email], [password], [name], [birthday], [address], [phone], [photo], [gender], [day_create], [day_edited], [editer_id], [status], [role], [active], [description], [forgotpw]) VALUES (14, N'test 12', N'202cb962ac59075b964b07152d234b70', N'Test 11', CAST(N'2000-01-01' AS Date), N'Test', N'0987654321 ', N'HuynhNhu2.jpg', 1, CAST(N'2020-04-16T00:00:00.000' AS DateTime), CAST(N'2020-04-16T00:00:00.000' AS DateTime), NULL, 0, 2, 1, N'Description', NULL)
INSERT [dbo].[user] ([id], [email], [password], [name], [birthday], [address], [phone], [photo], [gender], [day_create], [day_edited], [editer_id], [status], [role], [active], [description], [forgotpw]) VALUES (15, N'test 13', N'202cb962ac59075b964b07152d234b70', N'Test 12', CAST(N'2000-01-01' AS Date), N'Test', N'0987654321 ', N'HuynhNhu2.jpg', 1, CAST(N'2020-04-16T00:00:00.000' AS DateTime), CAST(N'2020-04-16T00:00:00.000' AS DateTime), NULL, 0, 2, 1, N'Description', NULL)
INSERT [dbo].[user] ([id], [email], [password], [name], [birthday], [address], [phone], [photo], [gender], [day_create], [day_edited], [editer_id], [status], [role], [active], [description], [forgotpw]) VALUES (16, N'test 14', N'202cb962ac59075b964b07152d234b70', N'Test 13', CAST(N'2000-01-01' AS Date), N'Test', N'0987654321 ', N'HuynhNhu2.jpg', 1, CAST(N'2020-04-16T00:00:00.000' AS DateTime), CAST(N'2020-04-16T00:00:00.000' AS DateTime), NULL, 0, 2, 1, N'Description', NULL)
INSERT [dbo].[user] ([id], [email], [password], [name], [birthday], [address], [phone], [photo], [gender], [day_create], [day_edited], [editer_id], [status], [role], [active], [description], [forgotpw]) VALUES (17, N'test 15', N'202cb962ac59075b964b07152d234b70', N'Test 14', CAST(N'2000-01-01' AS Date), N'Test', N'0987654321 ', N'HuynhNhu2.jpg', 1, CAST(N'2020-04-16T00:00:00.000' AS DateTime), CAST(N'2020-04-16T00:00:00.000' AS DateTime), NULL, 0, 2, 1, N'Description', NULL)
INSERT [dbo].[user] ([id], [email], [password], [name], [birthday], [address], [phone], [photo], [gender], [day_create], [day_edited], [editer_id], [status], [role], [active], [description], [forgotpw]) VALUES (18, N'test 16', N'202cb962ac59075b964b07152d234b70', N'Test 15', CAST(N'2000-01-01' AS Date), N'Test', N'0987654321 ', N'HuynhNhu2.jpg', 1, CAST(N'2020-04-16T00:00:00.000' AS DateTime), CAST(N'2020-04-16T00:00:00.000' AS DateTime), NULL, 0, 2, 0, N'Description', NULL)
INSERT [dbo].[user] ([id], [email], [password], [name], [birthday], [address], [phone], [photo], [gender], [day_create], [day_edited], [editer_id], [status], [role], [active], [description], [forgotpw]) VALUES (19, N'test 17', N'202cb962ac59075b964b07152d234b70', N'Test 16', CAST(N'2000-01-01' AS Date), N'Test', N'0987654321 ', N'HuynhNhu2.jpg', 1, CAST(N'2020-04-16T00:00:00.000' AS DateTime), CAST(N'2020-04-16T00:00:00.000' AS DateTime), NULL, 0, 2, 1, N'Description', NULL)
INSERT [dbo].[user] ([id], [email], [password], [name], [birthday], [address], [phone], [photo], [gender], [day_create], [day_edited], [editer_id], [status], [role], [active], [description], [forgotpw]) VALUES (20, N'test 18', N'202cb962ac59075b964b07152d234b70', N'Test 17', CAST(N'2000-01-01' AS Date), N'Test', N'0987654321 ', N'HuynhNhu2.jpg', 1, CAST(N'2020-04-16T00:00:00.000' AS DateTime), CAST(N'2020-04-16T00:00:00.000' AS DateTime), NULL, 0, 2, 1, N'Description', NULL)
INSERT [dbo].[user] ([id], [email], [password], [name], [birthday], [address], [phone], [photo], [gender], [day_create], [day_edited], [editer_id], [status], [role], [active], [description], [forgotpw]) VALUES (21, N'test 19', N'202cb962ac59075b964b07152d234b70', N'Test 18', CAST(N'2000-01-01' AS Date), N'Test', N'0987654321 ', N'HuynhNhu2.jpg', 1, CAST(N'2020-04-16T00:00:00.000' AS DateTime), CAST(N'2020-04-16T00:00:00.000' AS DateTime), NULL, 0, 2, 1, N'Description', NULL)
SET IDENTITY_INSERT [dbo].[user] OFF
GO
ALTER TABLE [dbo].[category] ADD  CONSTRAINT [DF_category_status]  DEFAULT ((0)) FOR [status]
GO
ALTER TABLE [dbo].[invoice] ADD  CONSTRAINT [DF_invoice_daycreate]  DEFAULT (getdate()) FOR [daycreate]
GO
ALTER TABLE [dbo].[invoice] ADD  CONSTRAINT [DF_invoice_total]  DEFAULT ((0)) FOR [total]
GO
ALTER TABLE [dbo].[invoice] ADD  CONSTRAINT [DF_invoice_status]  DEFAULT ((0)) FOR [status]
GO
ALTER TABLE [dbo].[invoice_detail] ADD  CONSTRAINT [DF_invoice_detail_price]  DEFAULT ((0)) FOR [price]
GO
ALTER TABLE [dbo].[invoice_detail] ADD  CONSTRAINT [DF_invoice_detail_quantity]  DEFAULT ((0)) FOR [quantity]
GO
ALTER TABLE [dbo].[product] ADD  CONSTRAINT [DF_product_price]  DEFAULT ((0)) FOR [price]
GO
ALTER TABLE [dbo].[product] ADD  CONSTRAINT [DF_product_quantity]  DEFAULT ((0)) FOR [quantity]
GO
ALTER TABLE [dbo].[product] ADD  CONSTRAINT [DF_product_daycreate]  DEFAULT (getdate()) FOR [daycreate]
GO
ALTER TABLE [dbo].[product] ADD  CONSTRAINT [DF_product_dayedited]  DEFAULT (getdate()) FOR [dayedited]
GO
ALTER TABLE [dbo].[product] ADD  CONSTRAINT [DF_product_active]  DEFAULT ((0)) FOR [active]
GO
ALTER TABLE [dbo].[product] ADD  CONSTRAINT [DF_product_status]  DEFAULT ((0)) FOR [status]
GO
ALTER TABLE [dbo].[product_photo] ADD  CONSTRAINT [DF_product_photo_photo]  DEFAULT (N'product_default.jpg') FOR [photo]
GO
ALTER TABLE [dbo].[product_photo] ADD  CONSTRAINT [DF_product_photo_main]  DEFAULT ((0)) FOR [main]
GO
ALTER TABLE [dbo].[sub_category] ADD  CONSTRAINT [DF_sub_category_status]  DEFAULT ((0)) FOR [status]
GO
ALTER TABLE [dbo].[unit] ADD  CONSTRAINT [DF_unit_quantity]  DEFAULT ((0)) FOR [quantity]
GO
ALTER TABLE [dbo].[unit] ADD  CONSTRAINT [DF_unit_status]  DEFAULT ((0)) FOR [status]
GO
ALTER TABLE [dbo].[user] ADD  CONSTRAINT [DF_user_day_create]  DEFAULT (getdate()) FOR [day_create]
GO
ALTER TABLE [dbo].[user] ADD  CONSTRAINT [DF_user_status]  DEFAULT ((0)) FOR [status]
GO
ALTER TABLE [dbo].[user] ADD  CONSTRAINT [DF_user_active]  DEFAULT ((0)) FOR [active]
GO
ALTER TABLE [dbo].[invoice]  WITH CHECK ADD  CONSTRAINT [FK_invoice_user] FOREIGN KEY([empid])
REFERENCES [dbo].[user] ([id])
GO
ALTER TABLE [dbo].[invoice] CHECK CONSTRAINT [FK_invoice_user]
GO
ALTER TABLE [dbo].[invoice]  WITH CHECK ADD  CONSTRAINT [FK_invoice_user1] FOREIGN KEY([cusid])
REFERENCES [dbo].[user] ([id])
GO
ALTER TABLE [dbo].[invoice] CHECK CONSTRAINT [FK_invoice_user1]
GO
ALTER TABLE [dbo].[invoice_detail]  WITH CHECK ADD  CONSTRAINT [FK_invoice_detail_invoice] FOREIGN KEY([invoiceid])
REFERENCES [dbo].[invoice] ([id])
GO
ALTER TABLE [dbo].[invoice_detail] CHECK CONSTRAINT [FK_invoice_detail_invoice]
GO
ALTER TABLE [dbo].[invoice_detail]  WITH CHECK ADD  CONSTRAINT [FK_invoice_detail_product] FOREIGN KEY([proid])
REFERENCES [dbo].[product] ([id])
GO
ALTER TABLE [dbo].[invoice_detail] CHECK CONSTRAINT [FK_invoice_detail_product]
GO
ALTER TABLE [dbo].[product]  WITH CHECK ADD  CONSTRAINT [FK_product_unit] FOREIGN KEY([unitid])
REFERENCES [dbo].[unit] ([id])
GO
ALTER TABLE [dbo].[product] CHECK CONSTRAINT [FK_product_unit]
GO
ALTER TABLE [dbo].[product_photo]  WITH CHECK ADD  CONSTRAINT [FK_product_photo_product] FOREIGN KEY([proid])
REFERENCES [dbo].[product] ([id])
GO
ALTER TABLE [dbo].[product_photo] CHECK CONSTRAINT [FK_product_photo_product]
GO
ALTER TABLE [dbo].[sub_category]  WITH CHECK ADD  CONSTRAINT [FK_sub_category_category] FOREIGN KEY([cateid])
REFERENCES [dbo].[category] ([id])
GO
ALTER TABLE [dbo].[sub_category] CHECK CONSTRAINT [FK_sub_category_category]
GO
USE [master]
GO
ALTER DATABASE [Project] SET  READ_WRITE 
GO
