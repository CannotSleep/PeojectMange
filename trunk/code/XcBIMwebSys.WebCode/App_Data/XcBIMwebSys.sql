USE [XcBIMwebSys]
GO
/****** Object:  UserDefinedTableType [dbo].[tp_ImportAccount]    Script Date: 2018/4/28 14:46:17 ******/
CREATE TYPE [dbo].[tp_ImportAccount] AS TABLE(
	[AccountID] [nvarchar](50) NULL,
	[Name] [nvarchar](50) NULL,
	[Departments] [nvarchar](200) NULL
)
GO
/****** Object:  Table [dbo].[tbCOM_CodeTable]    Script Date: 2018/4/28 14:46:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbCOM_CodeTable](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL CONSTRAINT [DF_tbCOM_CodeTable_Name]  DEFAULT (''),
	[Desc] [nvarchar](200) NOT NULL CONSTRAINT [DF_tbCOM_CodeTable_Desc]  DEFAULT (''),
	[SelectSql] [nvarchar](max) NOT NULL CONSTRAINT [DF_tbCOM_CodeTable_SelectSql]  DEFAULT (''),
	[IsUse] [bit] NOT NULL CONSTRAINT [DF_tbCOM_CodeTable_IsUse]  DEFAULT ((0)),
 CONSTRAINT [PK_tbCOM_CodeTable] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tbCOM_VersionControl]    Script Date: 2018/4/28 14:46:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbCOM_VersionControl](
	[Key] [varchar](50) NOT NULL,
	[Version] [int] NOT NULL,
	[RefTable] [varchar](50) NOT NULL,
 CONSTRAINT [PK_tbCOM_VersionControl] PRIMARY KEY CLUSTERED 
(
	[Key] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbLOG_Account]    Script Date: 2018/4/28 14:46:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbLOG_Account](
	[ID] [uniqueidentifier] NOT NULL CONSTRAINT [DF_tbLOG_Account_ID]  DEFAULT (newid()),
	[AccountID] [nvarchar](50) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
	[DepartmentsID] [smallint] NOT NULL CONSTRAINT [DF_tbLOG_Account_DepartmentsID]  DEFAULT ((0)),
	[AddTime] [datetime] NOT NULL CONSTRAINT [DF_tbLOG_Account_AddTime]  DEFAULT (getdate()),
	[EditTime] [datetime] NOT NULL CONSTRAINT [DF_tbLOG_Account_EditTime]  DEFAULT (getdate()),
	[Img] [nvarchar](max) NOT NULL,
	[IsUse] [bit] NOT NULL CONSTRAINT [DF_tbLOG_Account_IsUse]  DEFAULT ((0)),
 CONSTRAINT [PK_tbLOG_Account] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tbLOG_AccountOfRoles]    Script Date: 2018/4/28 14:46:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbLOG_AccountOfRoles](
	[ID] [uniqueidentifier] NOT NULL,
	[AccountID] [uniqueidentifier] NOT NULL,
	[RolesID] [int] NOT NULL,
	[IsUse] [bit] NOT NULL,
 CONSTRAINT [PK_tbLOG_AccountOfRoles] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tbLOG_Departments]    Script Date: 2018/4/28 14:46:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbLOG_Departments](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ParentID] [int] NOT NULL CONSTRAINT [DF_tbLOG_Departments_ParentID]  DEFAULT ((0)),
	[Name] [nvarchar](200) NOT NULL CONSTRAINT [DF_tbLOG_Departments_Name]  DEFAULT (''),
	[IsUse] [bit] NOT NULL CONSTRAINT [DF_tbLOG_Departments_IsUse]  DEFAULT ((0)),
 CONSTRAINT [PK_tbLOG_Departments] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tbLOG_ErrorInfo]    Script Date: 2018/4/28 14:46:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbLOG_ErrorInfo](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UID] [varchar](200) NOT NULL CONSTRAINT [DF_tbLOG_ErrorInfo_UID]  DEFAULT ((0)),
	[ErrorCode] [int] NOT NULL CONSTRAINT [DF_tbLOG_ErrorInfo_ErrorCode]  DEFAULT ((0)),
	[ErrorMsg] [nvarchar](max) NULL CONSTRAINT [DF_tbLOG_ErrorInfo_ErrorMsg]  DEFAULT ((0)),
	[RunningTime] [datetime] NOT NULL CONSTRAINT [DF_tbLOG_ErrorInfo_RunningTime]  DEFAULT ((0)),
	[ProgramID] [varchar](200) NOT NULL CONSTRAINT [DF_tbLOG_ErrorInfo_ProgramID]  DEFAULT ((0)),
	[Url] [nvarchar](max) NOT NULL CONSTRAINT [DF_tbLOG_ErrorInfo_Url]  DEFAULT ((0)),
	[ExecSql] [nvarchar](max) NOT NULL CONSTRAINT [DF_tbLOG_ErrorInfo_ExecSql]  DEFAULT ((0)),
	[StackTrace] [nvarchar](max) NOT NULL CONSTRAINT [DF_tbLOG_ErrorInfo_StackTrace]  DEFAULT ((0)),
	[SolveBy] [nvarchar](max) NOT NULL CONSTRAINT [DF_tbLOG_ErrorInfo_SolveBy]  DEFAULT ((0)),
 CONSTRAINT [PK_tbLOG_ErrorInfo] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbLOG_Menus]    Script Date: 2018/4/28 14:46:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbLOG_Menus](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PartentID] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[URL] [nvarchar](max) NOT NULL,
	[Priority] [int] NOT NULL,
	[IsUse] [bit] NOT NULL,
 CONSTRAINT [PK_tbLOG_Menus] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tbLOG_Permission]    Script Date: 2018/4/28 14:46:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbLOG_Permission](
	[ID] [uniqueidentifier] NOT NULL,
	[RolesID] [int] NOT NULL,
	[MenuID] [int] NOT NULL,
 CONSTRAINT [PK_tbLOG_Permission] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tbLOG_Roles]    Script Date: 2018/4/28 14:46:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbLOG_Roles](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL CONSTRAINT [DF_tbLOG_Roles_Name]  DEFAULT (''),
	[IsUse] [bit] NOT NULL CONSTRAINT [DF_tbLOG_Roles_IsUse]  DEFAULT ((0)),
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[tbLOG_AccountOfRoles] ADD  CONSTRAINT [DF_tbLOG_AccountOfRoles_ID]  DEFAULT (newid()) FOR [ID]
GO
ALTER TABLE [dbo].[tbLOG_AccountOfRoles] ADD  CONSTRAINT [DF_tbLOG_AccountOfRoles_AccountID]  DEFAULT (newid()) FOR [AccountID]
GO
ALTER TABLE [dbo].[tbLOG_AccountOfRoles] ADD  CONSTRAINT [DF_tbLOG_AccountOfRoles_RolesID]  DEFAULT ((0)) FOR [RolesID]
GO
ALTER TABLE [dbo].[tbLOG_AccountOfRoles] ADD  CONSTRAINT [DF_tbLOG_AccountOfRoles_IsUse]  DEFAULT ((0)) FOR [IsUse]
GO
ALTER TABLE [dbo].[tbLOG_Menus] ADD  CONSTRAINT [DF_tbLOG_Menus_PartentID]  DEFAULT ((0)) FOR [PartentID]
GO
ALTER TABLE [dbo].[tbLOG_Menus] ADD  CONSTRAINT [DF_tbLOG_Menus_Name]  DEFAULT ('') FOR [Name]
GO
ALTER TABLE [dbo].[tbLOG_Menus] ADD  CONSTRAINT [DF_tbLOG_Menus_URL]  DEFAULT ('') FOR [URL]
GO
ALTER TABLE [dbo].[tbLOG_Menus] ADD  CONSTRAINT [DF_tbLOG_Menus_Priority]  DEFAULT ((0)) FOR [Priority]
GO
ALTER TABLE [dbo].[tbLOG_Menus] ADD  CONSTRAINT [DF_tbLOG_Menus_IsUse]  DEFAULT ((0)) FOR [IsUse]
GO
ALTER TABLE [dbo].[tbLOG_Permission] ADD  CONSTRAINT [DF_tbLOG_Permission_PartentID]  DEFAULT (newid()) FOR [ID]
GO
ALTER TABLE [dbo].[tbLOG_Permission] ADD  CONSTRAINT [DF_tbLOG_Permission_RolesID]  DEFAULT ((0)) FOR [RolesID]
GO
ALTER TABLE [dbo].[tbLOG_Permission] ADD  CONSTRAINT [DF_tbLOG_Permission_MenuID]  DEFAULT ((0)) FOR [MenuID]
GO
/****** Object:  StoredProcedure [dbo].[sp_BatchImportAccount]    Script Date: 2018/4/28 14:46:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--/*********************************************************************************************************
--建立者: lbw  日期﹕2018-04-27  
--调用的程序﹕
--说明﹕批量导入用户
--更新记录﹕
-- 日期			        更改人				更新说明
------------------	-------------			--------------------------------------------
-- 2018-04-27           lbw                  新增
--*********************************************************************************************************/
CREATE PROCEDURE [dbo].[sp_BatchImportAccount]
    @ErrorNo INT OUTPUT ,
    @ErrorMsg NVARCHAR(500) OUTPUT ,
    @tpImportAccount [tp_ImportAccount] READONLY
AS
    SELECT  @ErrorNo = 1 ,
            @ErrorMsg = '';


	--将有上级部门的名称加在前面
    SELECT  D1.ID ,
            CASE WHEN D2.Name IS NOT NULL THEN D2.Name + D1.Name
                 ELSE D1.Name
            END AS Name
    INTO    #Temp_Departments
    FROM    dbo.tbLOG_Departments D1
            LEFT JOIN dbo.tbLOG_Departments D2 ON D1.ParentID = D2.ID
                                                  AND D2.IsUse = 1
    WHERE   D1.IsUse = 1;

    CREATE TABLE #Temp_Account
        (
          [AccountID] [NVARCHAR](50) NULL ,
          [Name] [NVARCHAR](50) NULL ,
          [Departments] [NVARCHAR](200) NULL
        );

    INSERT  INTO #Temp_Account
            ( AccountID ,
              Name ,
              Departments
	        )
            SELECT  AccountID ,
                    Name ,
                    Departments
            FROM    @tpImportAccount
            WHERE   AccountID IS NOT NULL
                    AND AccountID <> '';
    
    BEGIN TRY 
        INSERT  INTO dbo.tbLOG_Account
                ( ID ,
                  AccountID ,
                  Name ,
                  Password ,
                  DepartmentsID ,
                  AddTime ,
                  EditTime ,
                  Img ,
                  IsUse
	            )
                SELECT  NEWID() ,
                        TA.AccountID ,
                        TA.Name ,
                        '0O67SArJ3xOarE6Z1xQ0dw==' ,
                        ISNULL(TD.ID, 0) ,
                        GETDATE() ,
                        GETDATE() ,
                        '' ,
                        1
                FROM    #Temp_Account TA
                        LEFT JOIN #Temp_Departments TD ON TD.Name LIKE TA.Departments
						LEFT JOIN dbo.tbLOG_Account A ON A.AccountID = TA.AccountID
				WHERE   A.ID IS NULL; 

    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION myTran;
        SELECT  ERROR_MESSAGE();
        SELECT  @ErrorNo = 0;  --ERROR_NUMBER()
        SELECT  @ErrorMsg = ERROR_MESSAGE();
    END CATCH; 


GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'表名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbCOM_CodeTable', @level2type=N'COLUMN',@level2name=N'Name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'表描述' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbCOM_CodeTable', @level2type=N'COLUMN',@level2name=N'Desc'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'查询语句' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbCOM_CodeTable', @level2type=N'COLUMN',@level2name=N'SelectSql'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否启用' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbCOM_CodeTable', @level2type=N'COLUMN',@level2name=N'IsUse'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbCOM_VersionControl', @level2type=N'COLUMN',@level2name=N'Key'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'全局唯一标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbLOG_Account', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbLOG_Account', @level2type=N'COLUMN',@level2name=N'AccountID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbLOG_Account', @level2type=N'COLUMN',@level2name=N'Name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户密码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbLOG_Account', @level2type=N'COLUMN',@level2name=N'Password'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'部门ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbLOG_Account', @level2type=N'COLUMN',@level2name=N'DepartmentsID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbLOG_Account', @level2type=N'COLUMN',@level2name=N'AddTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编辑时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbLOG_Account', @level2type=N'COLUMN',@level2name=N'EditTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'头像' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbLOG_Account', @level2type=N'COLUMN',@level2name=N'Img'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否启用' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbLOG_Account', @level2type=N'COLUMN',@level2name=N'IsUse'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户唯一标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbLOG_AccountOfRoles', @level2type=N'COLUMN',@level2name=N'AccountID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'角色ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbLOG_AccountOfRoles', @level2type=N'COLUMN',@level2name=N'RolesID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否启用' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbLOG_AccountOfRoles', @level2type=N'COLUMN',@level2name=N'IsUse'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'部门ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbLOG_Departments', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'父级ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbLOG_Departments', @level2type=N'COLUMN',@level2name=N'ParentID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'部门名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbLOG_Departments', @level2type=N'COLUMN',@level2name=N'Name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否启用' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbLOG_Departments', @level2type=N'COLUMN',@level2name=N'IsUse'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbLOG_ErrorInfo', @level2type=N'COLUMN',@level2name=N'UID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'错误代码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbLOG_ErrorInfo', @level2type=N'COLUMN',@level2name=N'ErrorCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'错误信息' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbLOG_ErrorInfo', @level2type=N'COLUMN',@level2name=N'ErrorMsg'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'发生时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbLOG_ErrorInfo', @level2type=N'COLUMN',@level2name=N'RunningTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'程序ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbLOG_ErrorInfo', @level2type=N'COLUMN',@level2name=N'ProgramID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'地址' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbLOG_ErrorInfo', @level2type=N'COLUMN',@level2name=N'Url'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'数据库执行语句' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbLOG_ErrorInfo', @level2type=N'COLUMN',@level2name=N'ExecSql'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'堆栈' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbLOG_ErrorInfo', @level2type=N'COLUMN',@level2name=N'StackTrace'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'解决人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbLOG_ErrorInfo', @level2type=N'COLUMN',@level2name=N'SolveBy'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'父菜单ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbLOG_Menus', @level2type=N'COLUMN',@level2name=N'PartentID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'部门名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbLOG_Menus', @level2type=N'COLUMN',@level2name=N'Name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'菜单地址' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbLOG_Menus', @level2type=N'COLUMN',@level2name=N'URL'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'优先级' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbLOG_Menus', @level2type=N'COLUMN',@level2name=N'Priority'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否启用' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbLOG_Menus', @level2type=N'COLUMN',@level2name=N'IsUse'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'全局唯一ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbLOG_Permission', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'角色ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbLOG_Permission', @level2type=N'COLUMN',@level2name=N'RolesID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'菜单ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbLOG_Permission', @level2type=N'COLUMN',@level2name=N'MenuID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'角色ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbLOG_Roles', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'角色名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbLOG_Roles', @level2type=N'COLUMN',@level2name=N'Name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否启用' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbLOG_Roles', @level2type=N'COLUMN',@level2name=N'IsUse'
GO
