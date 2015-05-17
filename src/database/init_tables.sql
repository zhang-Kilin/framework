--�û���
IF (OBJECT_ID(N'dbo.[Users]',N'U') IS NOT NULL)
	DROP TABLE dbo.[Users]	
CREATE TABLE [Users]
(
	[UserId] INT IDENTITY(1,1) PRIMARY KEY,	
	[UserName] NVARCHAR(50) NOT NULL,--�û���¼��
	[Password] VARCHAR(50) NOT NULL,--��¼����
	[HashCode] VARCHAR(50) NOT NULL,--���������
	[NickName] NVARCHAR(50) NULL,--�ǳ�
	[Status] CHAR(10) NOT NULL,--״̬��Ԥ���ֶΣ��Ա�ϵͳ��չ
	[LastLoginTime] DATETIME ,--���һ�ε�¼ʱ��
	[LastLoginIP] VARCHAR(50),--���һ�ε�¼IP
	[CREATE_TIME] DATETIME DEFAULT(GETDATE()),--����ʱ��
	[CREATE_USER] NVARCHAR(50) DEFAULT(''),--�����û�
	[LAST_MODIFY_TIME] DATETIME DEFAULT(GETDATE()),--���һ���޸�ʱ��
	[LAST_MODIFY_USER] NVARCHAR(50) DEFAULT(''),--���һ���޸��û�
	[ISDELETED] BIT DEFAULT(0)--�Ƿ��ѱ�ɾ��
)

--��¼��־
IF (OBJECT_ID(N'dbo.[Login_Log]',N'U') IS NOT NULL)
	DROP TABLE dbo.[Login_Log]	
CREATE TABLE [Login_Log]
(
	[LogId] BIGINT IDENTITY(1,1) PRIMARY KEY,
	[UserName] NVARCHAR(50) NOT NULL,--��¼��
	[LoginTime] DATETIME NOT NULL DEFAULT(GETDATE()),
	[LoginDay] DATE ,--��¼����
	[LoginIP] VARCHAR(50),
	[Status] CHAR(10) NOT NULL,--��¼״̬��ȡֵ��Success��Fault
	[CREATE_TIME] DATETIME DEFAULT(GETDATE()),--����ʱ��
	[CREATE_USER] NVARCHAR(50) DEFAULT(''),--�����û�
	[LAST_MODIFY_TIME] DATETIME DEFAULT(GETDATE()),--���һ���޸�ʱ��
	[LAST_MODIFY_USER] NVARCHAR(50) DEFAULT(''),--���һ���޸��û�
	[ISDELETED] BIT DEFAULT(0)--�Ƿ��ѱ�ɾ��	
)

--�˵���
IF (OBJECT_ID(N'dbo.[Menus]',N'U') IS NOT NULL)
	DROP TABLE dbo.[Menus]
CREATE TABLE [Menus]
(
	[MenuId] INT IDENTITY(1,1) PRIMARY KEY,
	[MenuName] NVARCHAR(50) NOT NULL,--�˵���
	[IsHide] BIT NOT NULL DEFAULT(0),--�Ƿ�����
	[MenuType] CHAR(10) NOT NULL DEFAULT('Limb'),--�˵����ͣ�ȡֵ��Limb-֦�ɣ�Leaf-Ҷ�ӽڵ㣬ֻ��Ҷ�ӽڵ��������Ȩ��
	--[HasLeaf] BIT NOT NULL DEFAULT(0), --�Ƿ����Ҷ�ӽڵ�
	[ParentId] INT NOT NULL DEFAULT(0),--�����˵�Id
	[LinkAddress] VARCHAR(400),--�˵������ӵ�ַ
	[CREATE_TIME] DATETIME DEFAULT(GETDATE()),--����ʱ��
	[CREATE_USER] NVARCHAR(50) DEFAULT(''),--�����û�
	[LAST_MODIFY_TIME] DATETIME DEFAULT(GETDATE()),--���һ���޸�ʱ��
	[LAST_MODIFY_USER] NVARCHAR(50) DEFAULT(''),--���һ���޸��û�
	[ISDELETED] BIT DEFAULT(0)--�Ƿ��ѱ�ɾ��
)

--Ȩ�ޱ�
IF (OBJECT_ID(N'dbo.[Permissions]',N'U') IS NOT NULL)
	DROP TABLE dbo.[Permissions]
CREATE TABLE [Permissions]
(
	[PermissionId] INT IDENTITY(1,1) PRIMARY KEY,
	[PermissionKey] NVARCHAR(200),--Ȩ��Key
	[PermissionName] NVARCHAR(200),-- Ȩ������
	[Description] NVARCHAR(800),--Ȩ������
	[CREATE_TIME] DATETIME DEFAULT(GETDATE()),--����ʱ��
	[CREATE_USER] NVARCHAR(50) DEFAULT(''),--�����û�
	[LAST_MODIFY_TIME] DATETIME DEFAULT(GETDATE()),--���һ���޸�ʱ��
	[LAST_MODIFY_USER] NVARCHAR(50) DEFAULT(''),--���һ���޸��û�
	[ISDELETED] BIT DEFAULT(0)--�Ƿ��ѱ�ɾ��	
)

--��ɫ��
IF (OBJECT_ID(N'dbo.[Roles]',N'U') IS NOT NULL)
	DROP TABLE dbo.[Roles]
CREATE TABLE [Roles]
(
	[RoleId] INT IDENTITY(1,1) PRIMARY KEY,
	[RoleName] NVARCHAR(50) NOT NULL,--��ɫ����
	[Description] NVARCHAR(800),--��ɫ����
	[CREATE_TIME] DATETIME DEFAULT(GETDATE()),--����ʱ��
	[CREATE_USER] NVARCHAR(50) DEFAULT(''),--�����û�
	[LAST_MODIFY_TIME] DATETIME DEFAULT(GETDATE()),--���һ���޸�ʱ��
	[LAST_MODIFY_USER] NVARCHAR(50) DEFAULT(''),--���һ���޸��û�
	[ISDELETED] BIT DEFAULT(0)--�Ƿ��ѱ�ɾ��		
)

--��ɫ��
IF (OBJECT_ID(N'dbo.[RoleGroup]',N'U') IS NOT NULL)
	DROP TABLE dbo.[RoleGroup]
CREATE TABLE [RoleGroup]
(
	[RoleGroupId] INT IDENTITY(1,1) PRIMARY KEY,
	[RoleGroupName] NVARCHAR(50) NOT NULL,--��ɫ����
	[Description] NVARCHAR(800),--��ɫ����
	[CREATE_TIME] DATETIME DEFAULT(GETDATE()),--����ʱ��
	[CREATE_USER] NVARCHAR(50) DEFAULT(''),--�����û�
	[LAST_MODIFY_TIME] DATETIME DEFAULT(GETDATE()),--���һ���޸�ʱ��
	[LAST_MODIFY_USER] NVARCHAR(50) DEFAULT(''),--���һ���޸��û�
	[ISDELETED] BIT DEFAULT(0)--�Ƿ��ѱ�ɾ��		
)

--��ɫ��ӳ���
IF (OBJECT_ID(N'dbo.[RoleGroupMap]',N'U') IS NOT NULL)
	DROP TABLE dbo.[RoleGroupMap]
CREATE TABLE [RoleGroupMap]
(
	[RoleGroupMapId] INT IDENTITY(1,1) PRIMARY KEY,
	[RoleGroupId] INT NOT NULL, --��ɫ��
	[RoleId] INT NOT NULL, --��ɫ
	[CREATE_TIME] DATETIME DEFAULT(GETDATE()),--����ʱ��
	[CREATE_USER] NVARCHAR(50) DEFAULT(''),--�����û�
	[LAST_MODIFY_TIME] DATETIME DEFAULT(GETDATE()),--���һ���޸�ʱ��
	[LAST_MODIFY_USER] NVARCHAR(50) DEFAULT(''),--���һ���޸��û�
	[ISDELETED] BIT DEFAULT(0)--�Ƿ��ѱ�ɾ��			
)

--��ɫ��Ȩ��ӳ���
IF (OBJECT_ID(N'dbo.[RolePermissionMap]',N'U') IS NOT NULL)
	DROP TABLE dbo.[RolePermissionMap]
CREATE TABLE [RolePermissionMap]
(
	[RolePermissionMapId] INT IDENTITY(1,1) PRIMARY KEY,
	[RoleId] INT NOT NULL,--��ɫ
	[PermissionId] INT NOT NULL,--Ȩ��
	[CREATE_TIME] DATETIME DEFAULT(GETDATE()),--����ʱ��
	[CREATE_USER] NVARCHAR(50) DEFAULT(''),--�����û�
	[LAST_MODIFY_TIME] DATETIME DEFAULT(GETDATE()),--���һ���޸�ʱ��
	[LAST_MODIFY_USER] NVARCHAR(50) DEFAULT(''),--���һ���޸��û�
	[ISDELETED] BIT DEFAULT(0)--�Ƿ��ѱ�ɾ��		
)

--�û���ɫӳ���
IF (OBJECT_ID(N'dbo.[UserRoleMap]',N'U') IS NOT NULL)
	DROP TABLE dbo.[UserRoleMap]
CREATE TABLE [UserRoleMap]
(
	[UserRoleMapId] INT IDENTITY(1,1) PRIMARY KEY,
	[UserId] INT NOT NULL, -- �û�
	[RoleId] INT NOT NULL, --��ɫ
	[CREATE_TIME] DATETIME DEFAULT(GETDATE()),--����ʱ��
	[CREATE_USER] NVARCHAR(50) DEFAULT(''),--�����û�
	[LAST_MODIFY_TIME] DATETIME DEFAULT(GETDATE()),--���һ���޸�ʱ��
	[LAST_MODIFY_USER] NVARCHAR(50) DEFAULT(''),--���һ���޸��û�
	[ISDELETED] BIT DEFAULT(0)--�Ƿ��ѱ�ɾ��			
)

--�˵���Ȩ��ӳ���
IF (OBJECT_ID(N'dbo.[MenuPermissionMap]',N'U') IS NOT NULL)
	DROP TABLE dbo.[MenuPermissionMap]
CREATE TABLE [MenuPermissionMap]
(
	[MenuPermissionMapId] INT IDENTITY(1,1) PRIMARY KEY,
	[MenuId] INT NOT NULL, --�˵�
	[PermissionId] INT NOT NULL,--Ȩ��
	[CREATE_TIME] DATETIME DEFAULT(GETDATE()),--����ʱ��
	[CREATE_USER] NVARCHAR(50) DEFAULT(''),--�����û�
	[LAST_MODIFY_TIME] DATETIME DEFAULT(GETDATE()),--���һ���޸�ʱ��
	[LAST_MODIFY_USER] NVARCHAR(50) DEFAULT(''),--���һ���޸��û�
	[ISDELETED] BIT DEFAULT(0)--�Ƿ��ѱ�ɾ��	
)