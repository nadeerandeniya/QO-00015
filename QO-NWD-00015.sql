/*
Navicat SQL Server Data Transfer

Source Server         : sql sev
Source Server Version : 150000
Source Host           : localhost:1433
Source Database       : ebooks
Source Schema         : dbo

Target Server Type    : SQL Server
Target Server Version : 150000
File Encoding         : 65001

Date: 2024-05-23 08:43:22
*/


-- ----------------------------
-- Table structure for [dbo].[Books]
-- ----------------------------
GO
CREATE TABLE [dbo].[Books] (
[BookId] int NOT NULL IDENTITY(1,1) PRIMARY KEY,
[Author] varchar(100) NULL ,
[Publisher] varchar(100) NULL ,
[BookTitle] varchar(100) NULL ,
[BookYear] char(4) NULL ,
[BookType] varchar(30) NULL ,
[BookPrice] varchar(30) NULL ,
[BookDescription] text NULL ,
[Status] char(30) NULL ,
[Availability] int NULL ,
[CoverImage] varchar(100) NULL 
)


GO

-- ----------------------------
-- Records of Books
-- ----------------------------
SET IDENTITY_INSERT [Books] ON
INSERT INTO [Books] ([BookId], [Author], [Publisher], [BookTitle], [BookYear], [BookType], [BookPrice], [BookDescription], [Status], [Availability], [CoverImage]) VALUES (3, 'Kimbly Nguyen', 'Sarasa Publishers', 'Happy Holidays', '2020', 'Kids', '520', 'Two siblings discover a treehouse that transports them to different magical worlds.', '1                             ', 11, 'fad3b781-a973-44ca-bda2-5f3393d9fb8c.png');
GO
INSERT INTO [Books] ([BookId], [Author], [Publisher], [BookTitle], [BookYear], [BookType], [BookPrice], [BookDescription], [Status], [Availability], [CoverImage]) VALUES (4, 'B. Dupont', 'Gunasena Publishers', 'My Friend Roise', '2010', 'Kids', '750', 'A curious kitten sets out to explore the wonders of his backyard.', '1                             ', 10, '920a5dc1-1590-4e24-8ffd-3d2a34d9d688.png');
GO
INSERT INTO [Books] ([BookId], [Author], [Publisher], [BookTitle], [BookYear], [BookType], [BookPrice], [BookDescription], [Status], [Availability], [CoverImage]) VALUES (5, 'Willson Sam', 'Sarasa Publishers', 'Safari Animals', '2012', 'Kids', '690', 'Lila discovers a hidden garden that brings her new friends and adventures.', '1                             ', 8, 'bf647d2f-644b-49b1-9d44-d3fb35de87b3.png');
GO
INSERT INTO [Books] ([BookId], [Author], [Publisher], [BookTitle], [BookYear], [BookType], [BookPrice], [BookDescription], [Status], [Availability], [CoverImage]) VALUES (6, 'Helan Antique', 'Hali publishers', 'Story of a tree', '2015', 'Kids', '800', 'A girl learns the colors of the rainbow while planting a magical garden', '1                             ', 12, '8be84026-ad9d-497a-bc47-51ad91cc5d61.png');
GO
INSERT INTO [Books] ([BookId], [Author], [Publisher], [BookTitle], [BookYear], [BookType], [BookPrice], [BookDescription], [Status], [Availability], [CoverImage]) VALUES (7, 'Oliviya Wilson', 'Sarasa Publishers', 'Friend and families', '2016', 'Kids', '900', 'Pirate Pete and his crew search for a hidden treasure on a mysterious island.', '1                             ', 12, 'b0ea9779-9d26-4608-8c20-5c0550468b81.png');
GO
INSERT INTO [Books] ([BookId], [Author], [Publisher], [BookTitle], [BookYear], [BookType], [BookPrice], [BookDescription], [Status], [Availability], [CoverImage]) VALUES (8, 'Oliviya Wilson', 'AbC Publishers', 'Little Explore', '2017', 'Kids', '800', 'A small dragon proves that courage comes in all sizes', '1                             ', 12, '49181f34-8601-484f-99d6-f363ff462f9e.png');
GO
INSERT INTO [Books] ([BookId], [Author], [Publisher], [BookTitle], [BookYear], [BookType], [BookPrice], [BookDescription], [Status], [Availability], [CoverImage]) VALUES (9, 'Chaludia wilson', 'Sarasa Publishers', 'Stay with me', '1998', 'Novel', '1900', 'In a world where trees communicate, a young girl discovers the secret to saving her endangered forest.', '1                             ', 6, 'eb19a486-5130-4a3d-94bd-63befd504157.png');
GO
INSERT INTO [Books] ([BookId], [Author], [Publisher], [BookTitle], [BookYear], [BookType], [BookPrice], [BookDescription], [Status], [Availability], [CoverImage]) VALUES (10, 'Sebesthian Bennith', 'Sarasa Publishers', 'The Mirror of destiny', '1993', 'Novel', '1800', 'A detective hunts a serial killer who leaves cryptic clues in famous paintings.', '1                             ', 7, '6dd0b1d2-1a94-4ca2-8fa0-d3df161bbc3b.png');
GO
INSERT INTO [Books] ([BookId], [Author], [Publisher], [BookTitle], [BookYear], [BookType], [BookPrice], [BookDescription], [Status], [Availability], [CoverImage]) VALUES (11, 'Shawn chawn', 'Hali publishers', 'Conquest of flames', '1996', 'Novel', '2400', 'An archaeologist unearths an ancient civilization that holds the key to humanity''s survival.', '1                             ', 10, '1a68e2b6-9617-43af-a1de-03e81b71cd2f.png');
GO
INSERT INTO [Books] ([BookId], [Author], [Publisher], [BookTitle], [BookYear], [BookType], [BookPrice], [BookDescription], [Status], [Availability], [CoverImage]) VALUES (12, 'Stella dargy', 'Sarasavi Publishers', 'Walk into the shadow', '1994', 'Novel', '2500', 'A band of misfit space pirates must thwart an intergalactic conspiracy.', '1                             ', 13, '20798fa2-c783-4cea-b040-6c76115ae81f.png');
GO
INSERT INTO [Books] ([BookId], [Author], [Publisher], [BookTitle], [BookYear], [BookType], [BookPrice], [BookDescription], [Status], [Availability], [CoverImage]) VALUES (13, 'Magarita Peres', 'Sarasi Publishers', 'Game of your mind', '1996', 'ShortStory', '3500', 'A historian time-travels to uncover the truth behind a centuries-old mystery.', '1                             ', 8, '9b0f5bf9-92e4-4627-85d3-9f6303917def.png');
GO
INSERT INTO [Books] ([BookId], [Author], [Publisher], [BookTitle], [BookYear], [BookType], [BookPrice], [BookDescription], [Status], [Availability], [CoverImage]) VALUES (14, 'Adline Palhentan', 'Hali publishers', 'Garden', '2020', 'ShortStory', '1400', 'A meteorologist races against time to stop a catastrophic weather event engineered by terrorists.', '1                             ', 13, '0bda1038-7f27-4ee6-bd03-cfffcf680d76.png');
GO
INSERT INTO [Books] ([BookId], [Author], [Publisher], [BookTitle], [BookYear], [BookType], [BookPrice], [BookDescription], [Status], [Availability], [CoverImage]) VALUES (15, 'Magarita Peres', 'Hali publishers', 'Alien', '1995', 'Novel', '2400', 'An astronaut stranded on an alien planet must find a way back home.', '1                             ', 8, 'ae1c5b32-854c-447b-9218-305b7d43e4f1.png');
GO
INSERT INTO [Books] ([BookId], [Author], [Publisher], [BookTitle], [BookYear], [BookType], [BookPrice], [BookDescription], [Status], [Availability], [CoverImage]) VALUES (16, 'Olivia Wilson', 'Sarasa Publishers', 'Aura', '1985', 'Novel', '1600', 'A young scholar deciphers an ancient manuscript that promises immortality.', '1                             ', 5, 'a20adda8-0ac0-4df7-a628-28eea3db4b0b.png');
GO

SET IDENTITY_INSERT [Books] OFF
DBCC CHECKIDENT ([Books], RESEED, 17)
-- ----------------------------
-- Table structure for [dbo].[Customers]
-- ----------------------------
GO
CREATE TABLE [dbo].[Customers] (
[CustomerId] int NOT NULL IDENTITY(1,1) PRIMARY KEY,
[Phone] char(10) NULL ,
[Email] varchar(40) NULL ,
[Address] text NULL ,
[UserName] varchar(40) NULL ,
[Password] varchar(40) NULL ,
[FullName] varchar(60) NULL 
)


GO

-- ----------------------------
-- Records of Customers
-- ----------------------------
SET IDENTITY_INSERT [Customers] ON
INSERT INTO [Customers] ([CustomerId], [Phone], [Email], [Address], [UserName], [Password], [FullName]) VALUES (1, '075899663 ', 'kis@hm.vom', '55C , Alexca', 'sagar', 'sagar', 'Sagar Krishnamurthi');
GO
INSERT INTO [Customers] ([CustomerId], [Phone], [Email], [Address], [UserName], [Password], [FullName]) VALUES (2, '0712444778', 'sa@gmil.mm', '14/12 Jaffa Road, Mulathiw', 'Sadali', 'Sadali', 'Sadali Samarasinha');
GO
INSERT INTO [Customers] ([CustomerId], [Phone], [Email], [Address], [UserName], [Password], [FullName]) VALUES (3, '078488996 ', 'ara@gmail.vom', '45/33 Kaluthara road, mathugama', 'Harsha', 'Harsha', 'Harsha Kajenthani');
GO

SET IDENTITY_INSERT [Customers] OFF
DBCC CHECKIDENT ([Customers], RESEED, 4)
-- ----------------------------
-- Table structure for [dbo].[Feedback]
-- ----------------------------
GO
CREATE TABLE [dbo].[Feedback] (
[FeedbackId] int NOT NULL IDENTITY(1,1) PRIMARY KEY,
[BookId] int NULL ,
[CustomerId] int NULL ,
[Feedbacks] text NULL 
)


GO

-- ----------------------------
-- Records of Feedback
-- ----------------------------
SET IDENTITY_INSERT [Feedback] ON
INSERT INTO [Feedback] ([FeedbackId], [BookId], [CustomerId], [Feedbacks]) VALUES (1, 16, 1, 'this is most beautiful story i ever read');
GO
INSERT INTO [Feedback] ([FeedbackId], [BookId], [CustomerId], [Feedbacks]) VALUES (2, 12, 1, 'Fantastic story');
GO
INSERT INTO [Feedback] ([FeedbackId], [BookId], [CustomerId], [Feedbacks]) VALUES (3, 3, 1, 'This is a grate book');
GO
INSERT INTO [Feedback] ([FeedbackId], [BookId], [CustomerId], [Feedbacks]) VALUES (4, 3, 2, 'My kids much love this book');
GO

SET IDENTITY_INSERT [Feedback] OFF
DBCC CHECKIDENT ([Feedback], RESEED, 5)
-- ----------------------------
-- Table structure for [dbo].[Sales]
-- ----------------------------
GO
CREATE TABLE [dbo].[Sales] (
[SalesId] int NOT NULL IDENTITY(1,1) PRIMARY KEY,
[SalesDate] varchar(60) NULL ,
[CusId] int NULL ,
[PaymentMethod] varchar(25) NULL ,
[TotalPrice] varchar(25) NULL ,
[Status] varchar(25) NULL 
)


GO


-- ----------------------------
-- Table structure for [dbo].[SalesItems]
-- ----------------------------
GO
CREATE TABLE [dbo].[SalesItems] (
[SalesItemId] int NOT NULL IDENTITY(1,1) PRIMARY KEY,
[SalesId] int NULL ,
[BookId] int NULL ,
[Units] int NULL ,
[UnitPrice] bigint NULL ,
[TotalPrice] bigint NULL 
)




-- ----------------------------
-- Table structure for [dbo].[Users]
-- ----------------------------
GO
CREATE TABLE [dbo].[Users] (
[UserId] int NOT NULL IDENTITY(1,1) PRIMARY KEY,
[UserName] varchar(30) NULL ,
[Password] varchar(30) NULL ,
[Userrole] varchar(40) NULL 
)


GO

-- ----------------------------
-- Records of Users
-- ----------------------------
SET IDENTITY_INSERT [Users] ON
INSERT INTO [Users] ([UserId], [UserName], [Password], [Userrole]) VALUES (1, 'admin', 'admin@123', 'Admin');
GO
SET IDENTITY_INSERT [Users] OFF
DBCC CHECKIDENT ([Users], RESEED, 2)