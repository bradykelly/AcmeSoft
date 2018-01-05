/*    ==Scripting Parameters==

    Source Server Version : SQL Server 2016 (13.0.4001)
    Source Database Engine Edition : Microsoft SQL Server Enterprise Edition
    Source Database Engine Type : Standalone SQL Server

    Target Server Version : SQL Server 2017
    Target Database Engine Edition : Microsoft SQL Server Standard Edition
    Target Database Engine Type : Standalone SQL Server
*/

USE [AcmeSoft]
GO
SET IDENTITY_INSERT [dbo].[Person] ON 
GO
INSERT [dbo].[Person] ([PersonId], [BirthDate], [FirstName], [LastName], [IdNumber]) VALUES (9, CAST(N'1969-12-13' AS Date), N'Brady', N'Kelly', N'6912135011088')
GO
INSERT [dbo].[Person] ([PersonId], [BirthDate], [FirstName], [LastName], [IdNumber]) VALUES (11, CAST(N'1969-12-13' AS Date), N'Charlie', N'Brown', N'6912135011089')
GO
SET IDENTITY_INSERT [dbo].[Person] OFF
GO
