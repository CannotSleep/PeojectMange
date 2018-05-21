USE [XcBIMwebSys]
GO
/****** Object:  StoredProcedure [dbo].[MaterialInfo_ADD]    Script Date: 2018/5/17 13:13:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
------------------------------------
--用途：增加一条记录 
--项目名称：
--说明：
--时间：2018/5/11 12:15:15
------------------------------------
CREATE PROCEDURE [dbo].[MaterialInfo_ADD]
@tb_id uniqueidentifier,
@tb_name_before varchar(500),
@tb_name_now varchar(500),
@tb_date varchar(500),
@tb_size decimal(18,0),
@tb_username varchar(500),
@tb_userid int,
@tb_type int,
@tb_address varchar(500),
@tb_projectid int,
@tb_project varchar(500),
@tb_fileType varchar(500),
@tb_fileExplain varchar(500)

 AS 
	INSERT INTO [MaterialInfo](
	[tb_id],[tb_name_before],[tb_name_now],[tb_date],[tb_size],[tb_username],[tb_userid],[tb_type],[tb_address],[tb_projectid],[tb_project],[tb_fileType],[tb_fileExplain]
	)VALUES(
	@tb_id,@tb_name_before,@tb_name_now,@tb_date,@tb_size,@tb_username,@tb_userid,@tb_type,@tb_address,@tb_projectid,@tb_project,@tb_fileType,@tb_fileExplain
	)


GO
/****** Object:  StoredProcedure [dbo].[MaterialInfo_Delete]    Script Date: 2018/5/17 13:13:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
------------------------------------
--用途：删除一条记录 
--项目名称：
--说明：
--时间：2018/5/11 12:15:15
------------------------------------
CREATE PROCEDURE [dbo].[MaterialInfo_Delete]
@tb_name_before varchar(500)
 AS 
	DELETE [MaterialInfo]
	 WHERE  tb_name_before=@tb_name_before 


GO
/****** Object:  StoredProcedure [dbo].[MaterialInfo_Exists]    Script Date: 2018/5/17 13:13:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
------------------------------------
--用途：是否已经存在 
--项目名称：
--说明：
--时间：2018/5/11 12:15:15
------------------------------------
CREATE PROCEDURE [dbo].[MaterialInfo_Exists]
@tb_id uniqueidentifier,
@tb_name_before varchar(500)
AS
	DECLARE @TempID int
	SELECT @TempID = count(1) FROM [MaterialInfo] WHERE tb_id=@tb_id and tb_name_before=@tb_name_before 
	IF @TempID = 0
		RETURN 0
	ELSE
		RETURN 1


GO
/****** Object:  StoredProcedure [dbo].[MaterialInfo_GetList]    Script Date: 2018/5/17 13:13:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
------------------------------------
--用途：查询记录信息 
--项目名称：
--说明：
--时间：2018/5/11 12:15:15
------------------------------------
CREATE PROCEDURE [dbo].[MaterialInfo_GetList]
 AS 
	SELECT 
	tb_id,tb_name_before,tb_name_now,tb_date,tb_size,tb_username,tb_userid,tb_type,tb_address,tb_projectid,tb_project,tb_fileType,tb_fileExplain
	 FROM [MaterialInfo]


GO
/****** Object:  StoredProcedure [dbo].[MaterialInfo_GetModel]    Script Date: 2018/5/17 13:13:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
------------------------------------
--用途：得到实体对象的详细信息 
--项目名称：
--说明：
--时间：2018/5/11 12:15:15
------------------------------------
CREATE PROCEDURE [dbo].[MaterialInfo_GetModel]
@tb_id uniqueidentifier,
@tb_name_before varchar(500)
 AS 
	SELECT 
	tb_id,tb_name_before,tb_name_now,tb_date,tb_size,tb_username,tb_userid,tb_type,tb_address,tb_projectid,tb_project,tb_fileType,tb_fileExplain
	 FROM [MaterialInfo]
	 WHERE tb_id=@tb_id and tb_name_before=@tb_name_before 


GO
/****** Object:  StoredProcedure [dbo].[MaterialInfo_Update]    Script Date: 2018/5/17 13:13:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
------------------------------------
--用途：修改一条记录 
--项目名称：
--说明：
--时间：2018/5/11 12:15:15
------------------------------------
CREATE PROCEDURE [dbo].[MaterialInfo_Update]
@tb_id uniqueidentifier,
@tb_name_before varchar(500),
@tb_name_now varchar(500),
@tb_date varchar(500),
@tb_size decimal(18,0),
@tb_username varchar(500),
@tb_userid int,
@tb_type int,
@tb_address varchar(500),
@tb_projectid int,
@tb_project varchar(500),
@tb_fileType varchar(500),
@tb_fileExplain varchar(500)
 AS 
	UPDATE [MaterialInfo] SET 
	[tb_name_now] = @tb_name_now,[tb_date] = @tb_date,[tb_size] = @tb_size,[tb_username] = @tb_username,[tb_userid] = @tb_userid,[tb_type] = @tb_type,[tb_address] = @tb_address,[tb_projectid] = @tb_projectid,[tb_project] = @tb_project,[tb_fileType] = @tb_fileType,[tb_fileExplain] = @tb_fileExplain
	WHERE tb_id=@tb_id and tb_name_before=@tb_name_before 


GO
/****** Object:  Table [dbo].[MaterialInfo]    Script Date: 2018/5/17 13:13:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[MaterialInfo](
	[tb_id] [uniqueidentifier] NOT NULL,
	[tb_name_before] [varchar](500) NULL,
	[tb_name_now] [varchar](500) NULL,
	[tb_date] [varchar](500) NULL,
	[tb_size] [decimal](18, 0) NULL,
	[tb_username] [varchar](500) NULL,
	[tb_userid] [int] NULL,
	[tb_type] [int] NULL,
	[tb_address] [varchar](500) NULL,
	[tb_projectid] [int] NULL,
	[tb_project] [varchar](500) NULL,
	[tb_fileType] [varchar](500) NULL,
	[tb_fileExplain] [varchar](500) NULL,
 CONSTRAINT [PK_MaterialInfo] PRIMARY KEY CLUSTERED 
(
	[tb_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
