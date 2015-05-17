--用户表
IF (OBJECT_ID(N'dbo.[Users]',N'U') IS NOT NULL)
	DROP TABLE dbo.[Users]	
CREATE TABLE [Users]
(
	[UserId] INT IDENTITY(1,1) PRIMARY KEY,	
	[UserName] NVARCHAR(50) NOT NULL,--用户登录名
	[Password] VARCHAR(50) NOT NULL,--登录密码
	[HashCode] VARCHAR(50) NOT NULL,--密码混淆码
	[NickName] NVARCHAR(50) NULL,--昵称
	[Status] CHAR(10) NOT NULL,--状态，预留字段，以备系统扩展
	[LastLoginTime] DATETIME ,--最近一次登录时间
	[LastLoginIP] VARCHAR(50),--最近一次登录IP
	[CREATE_TIME] DATETIME DEFAULT(GETDATE()),--创建时间
	[CREATE_USER] NVARCHAR(50) DEFAULT(''),--创建用户
	[LAST_MODIFY_TIME] DATETIME DEFAULT(GETDATE()),--最后一次修改时间
	[LAST_MODIFY_USER] NVARCHAR(50) DEFAULT(''),--最后一次修改用户
	[ISDELETED] BIT DEFAULT(0)--是否已被删除
)

--登录日志
IF (OBJECT_ID(N'dbo.[Login_Log]',N'U') IS NOT NULL)
	DROP TABLE dbo.[Login_Log]	
CREATE TABLE [Login_Log]
(
	[LogId] BIGINT IDENTITY(1,1) PRIMARY KEY,
	[UserName] NVARCHAR(50) NOT NULL,--登录名
	[LoginTime] DATETIME NOT NULL DEFAULT(GETDATE()),
	[LoginDay] DATE ,--登录日期
	[LoginIP] VARCHAR(50),
	[Status] CHAR(10) NOT NULL,--登录状态，取值：Success，Fault
	[CREATE_TIME] DATETIME DEFAULT(GETDATE()),--创建时间
	[CREATE_USER] NVARCHAR(50) DEFAULT(''),--创建用户
	[LAST_MODIFY_TIME] DATETIME DEFAULT(GETDATE()),--最后一次修改时间
	[LAST_MODIFY_USER] NVARCHAR(50) DEFAULT(''),--最后一次修改用户
	[ISDELETED] BIT DEFAULT(0)--是否已被删除	
)

--菜单表
IF (OBJECT_ID(N'dbo.[Menus]',N'U') IS NOT NULL)
	DROP TABLE dbo.[Menus]
CREATE TABLE [Menus]
(
	[MenuId] INT IDENTITY(1,1) PRIMARY KEY,
	[MenuName] NVARCHAR(50) NOT NULL,--菜单名
	[IsHide] BIT NOT NULL DEFAULT(0),--是否隐藏
	[MenuType] CHAR(10) NOT NULL DEFAULT('Limb'),--菜单类型，取值：Limb-枝干，Leaf-叶子节点，只有叶子节点才能设置权限
	--[HasLeaf] BIT NOT NULL DEFAULT(0), --是否包含叶子节点
	[ParentId] INT NOT NULL DEFAULT(0),--父级菜单Id
	[LinkAddress] VARCHAR(400),--菜单的连接地址
	[CREATE_TIME] DATETIME DEFAULT(GETDATE()),--创建时间
	[CREATE_USER] NVARCHAR(50) DEFAULT(''),--创建用户
	[LAST_MODIFY_TIME] DATETIME DEFAULT(GETDATE()),--最后一次修改时间
	[LAST_MODIFY_USER] NVARCHAR(50) DEFAULT(''),--最后一次修改用户
	[ISDELETED] BIT DEFAULT(0)--是否已被删除
)

--权限表
IF (OBJECT_ID(N'dbo.[Permissions]',N'U') IS NOT NULL)
	DROP TABLE dbo.[Permissions]
CREATE TABLE [Permissions]
(
	[PermissionId] INT IDENTITY(1,1) PRIMARY KEY,
	[PermissionKey] NVARCHAR(200),--权限Key
	[PermissionName] NVARCHAR(200),-- 权限名称
	[Description] NVARCHAR(800),--权限描述
	[CREATE_TIME] DATETIME DEFAULT(GETDATE()),--创建时间
	[CREATE_USER] NVARCHAR(50) DEFAULT(''),--创建用户
	[LAST_MODIFY_TIME] DATETIME DEFAULT(GETDATE()),--最后一次修改时间
	[LAST_MODIFY_USER] NVARCHAR(50) DEFAULT(''),--最后一次修改用户
	[ISDELETED] BIT DEFAULT(0)--是否已被删除	
)

--角色表
IF (OBJECT_ID(N'dbo.[Roles]',N'U') IS NOT NULL)
	DROP TABLE dbo.[Roles]
CREATE TABLE [Roles]
(
	[RoleId] INT IDENTITY(1,1) PRIMARY KEY,
	[RoleName] NVARCHAR(50) NOT NULL,--角色名称
	[Description] NVARCHAR(800),--角色描述
	[CREATE_TIME] DATETIME DEFAULT(GETDATE()),--创建时间
	[CREATE_USER] NVARCHAR(50) DEFAULT(''),--创建用户
	[LAST_MODIFY_TIME] DATETIME DEFAULT(GETDATE()),--最后一次修改时间
	[LAST_MODIFY_USER] NVARCHAR(50) DEFAULT(''),--最后一次修改用户
	[ISDELETED] BIT DEFAULT(0)--是否已被删除		
)

--角色组
IF (OBJECT_ID(N'dbo.[RoleGroup]',N'U') IS NOT NULL)
	DROP TABLE dbo.[RoleGroup]
CREATE TABLE [RoleGroup]
(
	[RoleGroupId] INT IDENTITY(1,1) PRIMARY KEY,
	[RoleGroupName] NVARCHAR(50) NOT NULL,--角色名称
	[Description] NVARCHAR(800),--角色描述
	[CREATE_TIME] DATETIME DEFAULT(GETDATE()),--创建时间
	[CREATE_USER] NVARCHAR(50) DEFAULT(''),--创建用户
	[LAST_MODIFY_TIME] DATETIME DEFAULT(GETDATE()),--最后一次修改时间
	[LAST_MODIFY_USER] NVARCHAR(50) DEFAULT(''),--最后一次修改用户
	[ISDELETED] BIT DEFAULT(0)--是否已被删除		
)

--角色组映射表
IF (OBJECT_ID(N'dbo.[RoleGroupMap]',N'U') IS NOT NULL)
	DROP TABLE dbo.[RoleGroupMap]
CREATE TABLE [RoleGroupMap]
(
	[RoleGroupMapId] INT IDENTITY(1,1) PRIMARY KEY,
	[RoleGroupId] INT NOT NULL, --角色组
	[RoleId] INT NOT NULL, --角色
	[CREATE_TIME] DATETIME DEFAULT(GETDATE()),--创建时间
	[CREATE_USER] NVARCHAR(50) DEFAULT(''),--创建用户
	[LAST_MODIFY_TIME] DATETIME DEFAULT(GETDATE()),--最后一次修改时间
	[LAST_MODIFY_USER] NVARCHAR(50) DEFAULT(''),--最后一次修改用户
	[ISDELETED] BIT DEFAULT(0)--是否已被删除			
)

--角色、权限映射表
IF (OBJECT_ID(N'dbo.[RolePermissionMap]',N'U') IS NOT NULL)
	DROP TABLE dbo.[RolePermissionMap]
CREATE TABLE [RolePermissionMap]
(
	[RolePermissionMapId] INT IDENTITY(1,1) PRIMARY KEY,
	[RoleId] INT NOT NULL,--角色
	[PermissionId] INT NOT NULL,--权限
	[CREATE_TIME] DATETIME DEFAULT(GETDATE()),--创建时间
	[CREATE_USER] NVARCHAR(50) DEFAULT(''),--创建用户
	[LAST_MODIFY_TIME] DATETIME DEFAULT(GETDATE()),--最后一次修改时间
	[LAST_MODIFY_USER] NVARCHAR(50) DEFAULT(''),--最后一次修改用户
	[ISDELETED] BIT DEFAULT(0)--是否已被删除		
)

--用户角色映射表
IF (OBJECT_ID(N'dbo.[UserRoleMap]',N'U') IS NOT NULL)
	DROP TABLE dbo.[UserRoleMap]
CREATE TABLE [UserRoleMap]
(
	[UserRoleMapId] INT IDENTITY(1,1) PRIMARY KEY,
	[UserId] INT NOT NULL, -- 用户
	[RoleId] INT NOT NULL, --角色
	[CREATE_TIME] DATETIME DEFAULT(GETDATE()),--创建时间
	[CREATE_USER] NVARCHAR(50) DEFAULT(''),--创建用户
	[LAST_MODIFY_TIME] DATETIME DEFAULT(GETDATE()),--最后一次修改时间
	[LAST_MODIFY_USER] NVARCHAR(50) DEFAULT(''),--最后一次修改用户
	[ISDELETED] BIT DEFAULT(0)--是否已被删除			
)

--菜单、权限映射表
IF (OBJECT_ID(N'dbo.[MenuPermissionMap]',N'U') IS NOT NULL)
	DROP TABLE dbo.[MenuPermissionMap]
CREATE TABLE [MenuPermissionMap]
(
	[MenuPermissionMapId] INT IDENTITY(1,1) PRIMARY KEY,
	[MenuId] INT NOT NULL, --菜单
	[PermissionId] INT NOT NULL,--权限
	[CREATE_TIME] DATETIME DEFAULT(GETDATE()),--创建时间
	[CREATE_USER] NVARCHAR(50) DEFAULT(''),--创建用户
	[LAST_MODIFY_TIME] DATETIME DEFAULT(GETDATE()),--最后一次修改时间
	[LAST_MODIFY_USER] NVARCHAR(50) DEFAULT(''),--最后一次修改用户
	[ISDELETED] BIT DEFAULT(0)--是否已被删除	
)