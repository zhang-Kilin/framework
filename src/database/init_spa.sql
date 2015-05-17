--SPA_Users_I
IF (OBJECT_ID(N'dbo.[SPA_Users_I]',N'P') IS NOT NULL)
	DROP PROC dbo.SPA_Users_I
GO
CREATE PROC SPA_Users_I
	@UserName NVARCHAR(50) = NULL,--�û���¼��
	@Password VARCHAR(50) = NULL,--��¼����
	@HashCode VARCHAR(50) = NULL,--���������
	@NickName NVARCHAR(50) = NULL,--�ǳ�
	@Status CHAR(10) = NULL,--״̬��Ԥ���ֶΣ��Ա�ϵͳ��չ
	@LastLoginTime DATETIME = NULL,--���һ�ε�¼ʱ��
	@LastLoginIP VARCHAR(50) = NULL,--���һ�ε�¼IP
	@CREATE_TIME DATETIME = NULL,--����ʱ��
	@CREATE_USER NVARCHAR(50) = NULL,--�����û�
	@ISDELETED BIT = NULL--�Ƿ��ѱ�ɾ��
AS
BEGIN
	IF NOT EXISTS(SELECT TOP 1 1 FROM dbo.[Users] u WITH(NOLOCK) WHERE u.[UserName] = @UserName)
	BEGIN
		INSERT INTO dbo.[Users]
		(
			[UserName],
			[Password],
			[HashCode],
			[NickName],
			[Status],
			[LastLoginTime],
			[LastLoginIP],
			[CREATE_TIME],
			[CREATE_USER],
			[LAST_MODIFY_TIME],
			[LAST_MODIFY_USER],
			[ISDELETED]
		)
		VALUES
		(
			@UserName,
			@Password,
			@HashCode,
			@NickName,
			@Status,
			@LastLoginTime,
			@LastLoginIP,
			ISNULL(@CREATE_TIME,GETDATE()),
			ISNULL(@CREATE_USER,N''),
			ISNULL(@CREATE_TIME,GETDATE()),
			ISNULL(@CREATE_USER,N''),
			ISNULL(@ISDELETED,0)
		)
	END
	RETURN SCOPE_IDENTITY();
END
GO

--SPA_Users_U
IF (OBJECT_ID(N'dbo.[SPA_Users_U]',N'P') IS NOT NULL)
	DROP PROC dbo.SPA_Users_U
GO
CREATE PROC SPA_Users_U
	@UserName NVARCHAR(50) = NULL,--�û���¼��
	@Password VARCHAR(50) = NULL,--��¼����
	@HashCode VARCHAR(50) = NULL,--���������
	@NickName NVARCHAR(50) = NULL,--�ǳ�
	@Status CHAR(10) = NULL,--״̬��Ԥ���ֶΣ��Ա�ϵͳ��չ
	@LastLoginTime DATETIME = NULL ,--���һ�ε�¼ʱ��
	@LastLoginIP VARCHAR(50) = NULL,--���һ�ε�¼IP
	@LAST_MODIFY_TIME DATETIME = NULL,--���һ���޸�ʱ��
	@LAST_MODIFY_USER NVARCHAR(50) = NULL,--���һ���޸��û�
	@UserId INT --����
AS
BEGIN
	DECLARE @RowCount INT
	UPDATE TOP(1) dbo.[Users]
	SET [UserName] = ISNULL(@UserName,[UserName]),
		[Password] = ISNULL(@Password,[Password]),
		[HashCode] = ISNULL(@HashCode,[HashCode]),
		[NickName] = ISNULL(@NickName,[NickName]),
		[Status] = ISNULL(@Status,[Status]),
		[LastLoginTime] = ISNULL(@LastLoginTime,[LastLoginTime]),
		[LastLoginIP] = ISNULL(@LastLoginIP,[LastLoginIP]),
		[LAST_MODIFY_TIME] = ISNULL(@LAST_MODIFY_TIME,[LAST_MODIFY_TIME]),
		[LAST_MODIFY_USER] = ISNULL(@LAST_MODIFY_USER,[LAST_MODIFY_USER])
	WHERE [UserId]= @UserId
	
	RETURN @@ROWCOUNT;
END
GO

--[SPA_Users_D]
IF (OBJECT_ID(N'dbo.[SPA_Users_D]',N'P') IS NOT NULL)
	DROP PROC dbo.SPA_Users_D
GO
CREATE PROC dbo.SPA_Users_D
	@UserId INT
AS
BEGIN
	UPDATE TOP(1) dbo.Users
	SET ISDELETED = 1
	WHERE UserId = @UserId
	RETURN @@ROWCOUNT
END
GO


--[SPA_Login_Log_I]
IF (OBJECT_ID(N'dbo.[SPA_Login_Log_I]',N'P') IS NOT NULL)
	DROP PROC dbo.SPA_Login_Log_I
GO
CREATE PROC SPA_Login_Log_I
	@UserName NVARCHAR(50) = NULL,--��¼��
	@LoginTime DATETIME = NULL,
	@LoginDay DATE  = NULL,--��¼����
	@LoginIP VARCHAR(50) = NULL,
	@Status CHAR(10) = NULL,--��¼״̬��ȡֵ��Success��Fault
	@CREATE_TIME DATETIME = NULL,--����ʱ��
	@CREATE_USER NVARCHAR(50) = NULL ,--�����û�
	@ISDELETED BIT = NULL --�Ƿ��ѱ�ɾ��	
AS
BEGIN
	INSERT INTO dbo.Login_Log
	(
		UserName,
		LoginTime,
		LoginDay,
		LoginIP,
		[Status],
		CREATE_TIME,
		CREATE_USER,
		LAST_MODIFY_TIME,
		LAST_MODIFY_USER,
		ISDELETED
	)
	VALUES
	(
		@UserName,
		@LoginTime,
		@LoginDay,
		@LoginIP,
		@Status,
		ISNULL(@CREATE_TIME,GETDATE()),
		ISNULL(@CREATE_USER,N''),
		ISNULL(@CREATE_TIME,GETDATE()),
		ISNULL(@CREATE_USER,N''),
		ISNULL(@ISDELETED,0)
	)
	RETURN SCOPE_IDENTITY();
END
GO


--[SPA_Login_Log_U]
IF (OBJECT_ID(N'dbo.[SPA_Login_Log_U]',N'P') IS NOT NULL)
	DROP PROC dbo.SPA_Login_Log_U
GO
CREATE PROC SPA_Login_Log_U
	@UserName NVARCHAR(50) = NULL,--��¼��
	@LoginTime DATETIME = NULL,
	@LoginDay DATE = NULL ,--��¼����
	@LoginIP VARCHAR(50) = NULL,
	@Status CHAR(10) = NULL,--��¼״̬��ȡֵ��Success��Fault
	@LAST_MODIFY_TIME DATETIME = NULL,--���һ���޸�ʱ��
	@LAST_MODIFY_USER NVARCHAR(50) = NULL ,--���һ���޸��û�
	
	@LogId BIGINT --����
AS
BEGIN
	UPDATE TOP(1) dbo.Login_Log
	SET @UserName = ISNULL(@UserName,[UserName]),
		@LoginTime = ISNULL(@LoginTime,[LoginTime]),
		@LoginDay = ISNULL(@LoginDay,[LoginDay]),
		@LoginIP = ISNULL(@LoginIP,[LoginIP]),
		@Status = ISNULL(@Status,[Status]),
		@LAST_MODIFY_TIME = ISNULL(@LAST_MODIFY_TIME,[LAST_MODIFY_TIME]),
		@LAST_MODIFY_USER = ISNULL(@LAST_MODIFY_USER,[LAST_MODIFY_USER])
	WHERE LogId = @LogId	
	RETURN @@ROWCOUNT
END
GO

--[SPA_Login_Log_D]
IF (OBJECT_ID(N'dbo.[SPA_Login_Log_D]',N'P') IS NOT NULL)
	DROP PROC dbo.SPA_Login_Log_D
GO
CREATE PROC dbo.SPA_Login_Log_D
	@LogId BIGINT
AS
BEGIN
	UPDATE TOP(1) dbo.Login_Log
	SET ISDELETED = 1
	WHERE LogId = @LogId
	RETURN @@ROWCOUNT
END
GO


--[SPA_Menus_I]
IF (OBJECT_ID(N'dbo.[SPA_Menus_I]',N'P') IS NOT NULL)
	DROP PROC dbo.SPA_Menus_I
GO
CREATE PROC SPA_Menus_I
	@MenuName NVARCHAR(50) = NULL,--�˵���
	@IsHide BIT = NULL,--�Ƿ�����
	@MenuType CHAR(10) = NULL,--�˵����ͣ�ȡֵ��Limb-֦�ɣ�Leaf-Ҷ�ӽڵ㣬ֻ��Ҷ�ӽڵ��������Ȩ��
	@ParentId INT = NULL,--�����˵�Id
	@LinkAddress VARCHAR(400) = NULL,--�˵������ӵ�ַ
	@CREATE_TIME DATETIME = NULL,--����ʱ��
	@CREATE_USER NVARCHAR(50) = NULL,--�����û�
	@ISDELETED BIT = NULL--�Ƿ��ѱ�ɾ��
AS
BEGIN
	INSERT INTO dbo.Menus
	(
		MenuName,
		IsHide,
		MenuType,
		ParentId,
		LinkAddress,
		CREATE_TIME,
		CREATE_USER,
		LAST_MODIFY_TIME,
		LAST_MODIFY_USER,
		ISDELETED
	)
	VALUES
	(
		@MenuName,
		@IsHide,
		@MenuType,
		@ParentId,
		@LinkAddress,
		ISNULL(@CREATE_TIME,GETDATE()),
		ISNULL(@CREATE_USER,N''),
		ISNULL(@CREATE_TIME,GETDATE()),
		ISNULL(@CREATE_USER,N''),
		ISNULL(@ISDELETED,0)
	)
	RETURN SCOPE_IDENTITY();
END
GO

--[SPA_Menus_U]
IF (OBJECT_ID(N'dbo.[SPA_Menus_U]',N'P') IS NOT NULL)
	DROP PROC dbo.SPA_Menus_U
GO
CREATE PROC SPA_Menus_U
	@MenuName NVARCHAR(50) = NULL,--�˵���
	@IsHide BIT = NULL,--�Ƿ�����
	@MenuType CHAR(10) = NULL,--�˵����ͣ�ȡֵ��Limb-֦�ɣ�Leaf-Ҷ�ӽڵ㣬ֻ��Ҷ�ӽڵ��������Ȩ��
	@ParentId INT = NULL,--�����˵�Id
	@LinkAddress VARCHAR(400) = NULL,--�˵������ӵ�ַ
	@LAST_MODIFY_TIME DATETIME = NULL,--���һ���޸�ʱ��
	@LAST_MODIFY_USER NVARCHAR(50) = NULL,--���һ���޸��û�
	
	@MenuId INT	
AS
BEGIN
	UPDATE TOP(1) dbo.Menus
	SET MenuName = ISNULL(@MenuName,[MenuName]),
		IsHide = ISNULL(@IsHide,[IsHide]),
		MenuType = ISNULL(@MenuType,[MenuType]),
		ParentId = ISNULL(@ParentId,[ParentId]),
		LinkAddress = ISNULL(@LinkAddress,[LinkAddress]),
		LAST_MODIFY_TIME = ISNULL(@LAST_MODIFY_TIME,[LAST_MODIFY_TIME]),
		LAST_MODIFY_USER = ISNULL(@LAST_MODIFY_USER,[LAST_MODIFY_USER])
	WHERE MenuId = @MenuId
	RETURN @@ROWCOUNT
END
GO

--[SPA_Menus_D]
IF (OBJECT_ID(N'dbo.[SPA_Menus_D]',N'P') IS NOT NULL)
	DROP PROC dbo.SPA_Menus_D
GO
CREATE PROC dbo.SPA_Menus_D
	@MenuId INT
AS
BEGIN
	UPDATE TOP(1) dbo.Menus
	SET ISDELETED = 1
	WHERE MenuId = @MenuId
	RETURN @@ROWCOUNT
END
GO


--[SPA_Permissions_I]
IF (OBJECT_ID(N'dbo.[SPA_Permissions_I]',N'P') IS NOT NULL)
	DROP PROC dbo.SPA_Permissions_I
GO
CREATE PROC SPA_Permissions_I
	@PermissionKey NVARCHAR(200) = NULL,--Ȩ��Key
	@PermissionName NVARCHAR(200) = NULL,-- Ȩ������
	@Description NVARCHAR(800) = NULL,--Ȩ������
	@CREATE_TIME DATETIME = NULL,--����ʱ��
	@CREATE_USER NVARCHAR(50) = NULL,--�����û�
	@ISDELETED BIT = NULL --�Ƿ��ѱ�ɾ��	
AS
BEGIN
	INSERT INTO dbo.[Permissions]
	(
		[PermissionKey],--Ȩ��Key
		[PermissionName],-- Ȩ������
		[Description],--Ȩ������
		[CREATE_TIME],--����ʱ��
		[CREATE_USER],--�����û�
		[LAST_MODIFY_TIME],--���һ���޸�ʱ��
		[LAST_MODIFY_USER],--���һ���޸��û�
		[ISDELETED]--�Ƿ��ѱ�ɾ��	
	)
	VALUES
	(
		@PermissionKey,--Ȩ��Key
		@PermissionName,-- Ȩ������
		@Description,--Ȩ������
		ISNULL(@CREATE_TIME,GETDATE()),--����ʱ��
		ISNULL(@CREATE_USER,N''),--�����û�
		ISNULL(@CREATE_TIME,GETDATE()),
		ISNULL(@CREATE_USER,N''),
		ISNULL(@ISDELETED,0)--�Ƿ��ѱ�ɾ��	
	)
	RETURN SCOPE_IDENTITY();
END
GO

--[SPA_Permissions_U]
IF (OBJECT_ID(N'dbo.[SPA_Permissions_U]',N'P') IS NOT NULL)
	DROP PROC dbo.SPA_Permissions_U
GO
CREATE PROC SPA_Permissions_U
	@PermissionKey NVARCHAR(200) = NULL,--Ȩ��Key
	@PermissionName NVARCHAR(200) = NULL,-- Ȩ������
	@Description NVARCHAR(800) = NULL,--Ȩ������
	@LAST_MODIFY_TIME DATETIME = NULL,--���һ���޸�ʱ��
	@LAST_MODIFY_USER NVARCHAR(50) = NULL,--���һ���޸��û�
	
	@PermissionId INT --����
AS
BEGIN
	UPDATE TOP(1) dbo.[Permissions]
	SET [PermissionKey]=ISNULL(@PermissionKey,[PermissionKey]),--Ȩ��Key
		[PermissionName]=ISNULL(@PermissionName,[PermissionName]),-- Ȩ������
		[Description]=ISNULL(@Description,[Description]),--Ȩ������
		[LAST_MODIFY_TIME]=ISNULL(@LAST_MODIFY_TIME,GETDATE()),--���һ���޸�ʱ��
		[LAST_MODIFY_USER]=ISNULL(@LAST_MODIFY_USER,[LAST_MODIFY_USER])	
	WHERE PermissionId = @PermissionId
	
	RETURN @@ROWCOUNT
END
GO

--[SPA_Permissions_D]
IF (OBJECT_ID(N'dbo.[SPA_Permissions_D]',N'P') IS NOT NULL)
	DROP PROC dbo.SPA_Permissions_D
GO
CREATE PROC dbo.SPA_Permissions_D
	@PermissionId INT
AS
BEGIN
	UPDATE TOP(1) dbo.[Permissions]
	SET ISDELETED = 1
	WHERE PermissionId = @PermissionId
	RETURN @@ROWCOUNT
END
GO


--[SPA_Roles_I]
IF (OBJECT_ID(N'dbo.[SPA_Roles_I]',N'P') IS NOT NULL)
	DROP PROC dbo.SPA_Roles_I
GO
CREATE PROC SPA_Roles_I
	@RoleName NVARCHAR(50) = NULL,--��ɫ����
	@Description NVARCHAR(800) = NULL,--��ɫ����
	@CREATE_TIME DATETIME = NULL,--����ʱ��
	@CREATE_USER NVARCHAR(50) = NULL,--�����û�
	@ISDELETED BIT = NULL --�Ƿ��ѱ�ɾ��
AS
BEGIN
	INSERT INTO dbo.Roles
	(
		[RoleName],
		[Description],
		[CREATE_TIME],--����ʱ��
		[CREATE_USER],--�����û�
		[LAST_MODIFY_TIME],--���һ���޸�ʱ��
		[LAST_MODIFY_USER],--���һ���޸��û�
		[ISDELETED]--�Ƿ��ѱ�ɾ��	
	)
	VALUES
	(
		@RoleName,
		@Description,
		ISNULL(@CREATE_TIME,GETDATE()),--����ʱ��
		ISNULL(@CREATE_USER,N''),--�����û�
		ISNULL(@CREATE_TIME,GETDATE()),
		ISNULL(@CREATE_USER,N''),
		ISNULL(@ISDELETED,0)--�Ƿ��ѱ�ɾ��	
	)
	RETURN SCOPE_IDENTITY();
END
GO

--[SPA_Roles_U]
IF (OBJECT_ID(N'dbo.[SPA_Roles_U]',N'P') IS NOT NULL)
	DROP PROC dbo.SPA_Roles_U
GO
CREATE PROC dbo.SPA_Roles_U
	@RoleName NVARCHAR(50) = NULL,--��ɫ����
	@Description NVARCHAR(800) = NULL,--��ɫ����
	@LAST_MODIFY_TIME DATETIME = NULL,--���һ���޸�ʱ��
	@LAST_MODIFY_USER NVARCHAR(50) = NULL,--���һ���޸��û�	
	
	@RoleId INT --����
AS
BEGIN
	UPDATE TOP(1) dbo.Roles
	SET [RoleName]= ISNULL(@RoleName,[RoleName]),
		[Description] = ISNULL(@Description,[Description]),		
		[LAST_MODIFY_TIME]=ISNULL(@LAST_MODIFY_TIME,GETDATE()),--���һ���޸�ʱ��
		[LAST_MODIFY_USER]=ISNULL(@LAST_MODIFY_USER,[LAST_MODIFY_USER])	
	WHERE RoleId = @RoleId
END
GO

--[SPA_Roles_D]
IF (OBJECT_ID(N'dbo.[SPA_Roles_D]',N'P') IS NOT NULL)
	DROP PROC dbo.SPA_Roles_D
GO
CREATE PROC dbo.SPA_Roles_D
	@RoleId INT
AS
BEGIN
	UPDATE TOP(1) dbo.Roles
	SET ISDELETED = 1
	WHERE RoleId = @RoleId
	RETURN @@ROWCOUNT
END
GO


--[SPA_RoleGroup_I]
IF (OBJECT_ID(N'dbo.[SPA_RoleGroup_I]',N'P') IS NOT NULL)
	DROP PROC dbo.SPA_RoleGroup_I
GO
CREATE PROC SPA_RoleGroup_I
	@RoleGroupName NVARCHAR(50) = NULL,--��ɫ����
	@Description NVARCHAR(800) = NULL,--��ɫ����
	@CREATE_TIME DATETIME = NULL,--����ʱ��
	@CREATE_USER NVARCHAR(50) = NULL,--�����û�
	@ISDELETED BIT = NULL --�Ƿ��ѱ�ɾ��
AS
BEGIN
	INSERT INTO dbo.RoleGroup
	(
		[RoleGroupName],
		[Description],
		[CREATE_TIME],--����ʱ��
		[CREATE_USER],--�����û�
		[LAST_MODIFY_TIME],--���һ���޸�ʱ��
		[LAST_MODIFY_USER],--���һ���޸��û�
		[ISDELETED]--�Ƿ��ѱ�ɾ��	
	)
	VALUES
	(
		@RoleGroupName,
		@Description,
		ISNULL(@CREATE_TIME,GETDATE()),--����ʱ��
		ISNULL(@CREATE_USER,N''),--�����û�
		ISNULL(@CREATE_TIME,GETDATE()),
		ISNULL(@CREATE_USER,N''),
		ISNULL(@ISDELETED,0)--�Ƿ��ѱ�ɾ��	
	)
	RETURN SCOPE_IDENTITY();
END
GO

--[SPA_RoleGroup_U]
IF (OBJECT_ID(N'dbo.[SPA_RoleGroup_U]',N'P') IS NOT NULL)
	DROP PROC dbo.SPA_RoleGroup_U
GO
CREATE PROC dbo.SPA_RoleGroup_U
	@RoleGroupName NVARCHAR(50) = NULL,--��ɫ����
	@Description NVARCHAR(800) = NULL,--��ɫ����
	@LAST_MODIFY_TIME DATETIME = NULL,--���һ���޸�ʱ��
	@LAST_MODIFY_USER NVARCHAR(50) = NULL,--���һ���޸��û�
	
	@RoleGroupId INT --����
AS
BEGIN
	UPDATE TOP(1) dbo.RoleGroup
	SET [RoleGroupName]= ISNULL(@RoleGroupName,[RoleGroupName]),
		[Description] = ISNULL(@Description,[Description]),		
		[LAST_MODIFY_TIME]=ISNULL(@LAST_MODIFY_TIME,GETDATE()),--���һ���޸�ʱ��
		[LAST_MODIFY_USER]=ISNULL(@LAST_MODIFY_USER,[LAST_MODIFY_USER])
	WHERE RoleGroupId = @RoleGroupId
END
GO

--[SPA_RoleGroup_D]
IF (OBJECT_ID(N'dbo.[SPA_RoleGroup_D]',N'P') IS NOT NULL)
	DROP PROC dbo.SPA_RoleGroup_D
GO
CREATE PROC dbo.SPA_RoleGroup_D
	@RoleGroupId INT
AS
BEGIN
	UPDATE TOP(1) dbo.RoleGroup
	SET ISDELETED = 1
	WHERE RoleGroupId = @RoleGroupId
	RETURN @@ROWCOUNT
END
GO


--[SPA_RoleGroupMap_I]
IF (OBJECT_ID(N'dbo.[SPA_RoleGroupMap_I]',N'P') IS NOT NULL)
	DROP PROC dbo.SPA_RoleGroupMap_I
GO
CREATE PROC SPA_RoleGroupMap_I
	@RoleGroupId INT = NULL,--��ɫ��
	@RoleId INT = NULL,--��ɫ
	@CREATE_TIME DATETIME = NULL,--����ʱ��
	@CREATE_USER NVARCHAR(50) = NULL,--�����û�
	@ISDELETED BIT = NULL --�Ƿ��ѱ�ɾ��
AS
BEGIN
	INSERT INTO dbo.RoleGroupMap
	(
		[RoleGroupId],
		[RoleId],
		[CREATE_TIME],--����ʱ��
		[CREATE_USER],--�����û�
		[LAST_MODIFY_TIME],--���һ���޸�ʱ��
		[LAST_MODIFY_USER],--���һ���޸��û�
		[ISDELETED]--�Ƿ��ѱ�ɾ��	
	)
	VALUES
	(
		@RoleGroupId,
		@RoleId,
		ISNULL(@CREATE_TIME,GETDATE()),--����ʱ��
		ISNULL(@CREATE_USER,N''),--�����û�
		ISNULL(@CREATE_TIME,GETDATE()),
		ISNULL(@CREATE_USER,N''),
		ISNULL(@ISDELETED,0)--�Ƿ��ѱ�ɾ��	
	)
	RETURN SCOPE_IDENTITY();
END
GO

--[SPA_RoleGroupMap_U]
IF (OBJECT_ID(N'dbo.[SPA_RoleGroupMap_U]',N'P') IS NOT NULL)
	DROP PROC dbo.SPA_RoleGroupMap_U
GO
CREATE PROC dbo.SPA_RoleGroupMap_U
	@RoleGroupId INT = NULL,--��ɫ��
	@RoleId INT = NULL,--��ɫ
	@LAST_MODIFY_TIME DATETIME = NULL,--���һ���޸�ʱ��
	@LAST_MODIFY_USER NVARCHAR(50) = NULL,--���һ���޸��û�
	
	@RoleGroupMapId INT --����
AS
BEGIN
	UPDATE TOP(1) dbo.RoleGroupMap
	SET [RoleGroupId]= ISNULL(@RoleGroupId,[RoleGroupId]),
		[RoleId] = ISNULL(@RoleId,[RoleId]),		
		[LAST_MODIFY_TIME]=ISNULL(@LAST_MODIFY_TIME,GETDATE()),--���һ���޸�ʱ��
		[LAST_MODIFY_USER]=ISNULL(@LAST_MODIFY_USER,[LAST_MODIFY_USER])
	WHERE RoleGroupMapId = @RoleGroupMapId
END
GO

--[SPA_RoleGroupMap_D]
IF (OBJECT_ID(N'dbo.[SPA_RoleGroupMap_D]',N'P') IS NOT NULL)
	DROP PROC dbo.SPA_RoleGroupMap_D
GO
CREATE PROC dbo.SPA_RoleGroupMap_D
	@RoleGroupMapId INT
AS
BEGIN
	UPDATE TOP(1) dbo.RoleGroupMap
	SET ISDELETED = 1
	WHERE RoleGroupMapId = @RoleGroupMapId
	RETURN @@ROWCOUNT
END
GO


--[SPA_RolePermissionMap_I]
IF (OBJECT_ID(N'dbo.[SPA_RolePermissionMap_I]',N'P') IS NOT NULL)
	DROP PROC dbo.SPA_RolePermissionMap_I
GO
CREATE PROC SPA_RolePermissionMap_I
	@RoleId INT = NULL,--��ɫ
	@PermissionId INT = NULL,--Ȩ��
	@CREATE_TIME DATETIME = NULL,--����ʱ��
	@CREATE_USER NVARCHAR(50) = NULL,--�����û�
	@ISDELETED BIT = NULL --�Ƿ��ѱ�ɾ��
AS
BEGIN
	INSERT INTO dbo.RolePermissionMap
	(
		[RoleId],
		[PermissionId],
		[CREATE_TIME],--����ʱ��
		[CREATE_USER],--�����û�
		[LAST_MODIFY_TIME],--���һ���޸�ʱ��
		[LAST_MODIFY_USER],--���һ���޸��û�
		[ISDELETED]--�Ƿ��ѱ�ɾ��	
	)
	VALUES
	(
		@RoleId,
		@PermissionId,
		ISNULL(@CREATE_TIME,GETDATE()),--����ʱ��
		ISNULL(@CREATE_USER,N''),--�����û�
		ISNULL(@CREATE_TIME,GETDATE()),
		ISNULL(@CREATE_USER,N''),
		ISNULL(@ISDELETED,0)--�Ƿ��ѱ�ɾ��	
	)
	RETURN SCOPE_IDENTITY();
END
GO

--[SPA_RolePermissionMap_U]
IF (OBJECT_ID(N'dbo.[SPA_RolePermissionMap_U]',N'P') IS NOT NULL)
	DROP PROC dbo.SPA_RolePermissionMap_U
GO
CREATE PROC dbo.SPA_RolePermissionMap_U
	@RoleId INT = NULL,--��ɫ
	@PermissionId INT = NULL,--Ȩ��
	@LAST_MODIFY_TIME DATETIME = NULL,--���һ���޸�ʱ��
	@LAST_MODIFY_USER NVARCHAR(50) = NULL,--���һ���޸��û�
	
	@RolePermissionMapId INT --����
AS
BEGIN
	UPDATE TOP(1) dbo.RolePermissionMap
	SET [RoleId] = ISNULL(@RoleId,[RoleId]),		
		[PermissionId]= ISNULL(@PermissionId,[PermissionId]),
		[LAST_MODIFY_TIME]=ISNULL(@LAST_MODIFY_TIME,GETDATE()),--���һ���޸�ʱ��
		[LAST_MODIFY_USER]=ISNULL(@LAST_MODIFY_USER,[LAST_MODIFY_USER])
	WHERE RolePermissionMapId = @RolePermissionMapId
END
GO

--[SPA_RolePermissionMap_D]
IF (OBJECT_ID(N'dbo.[SPA_RolePermissionMap_D]',N'P') IS NOT NULL)
	DROP PROC dbo.SPA_RolePermissionMap_D
GO
CREATE PROC dbo.SPA_RolePermissionMap_D
	@RolePermissionMapId INT
AS
BEGIN
	UPDATE TOP(1) dbo.RolePermissionMap
	SET ISDELETED = 1
	WHERE RolePermissionMapId = @RolePermissionMapId
	RETURN @@ROWCOUNT
END
GO


--[SPA_UserRoleMap_I]
IF (OBJECT_ID(N'dbo.[SPA_UserRoleMap_I]',N'P') IS NOT NULL)
	DROP PROC dbo.SPA_UserRoleMap_I
GO
CREATE PROC SPA_UserRoleMap_I
	@RoleId INT = NULL,--��ɫ
	@UserId INT = NULL,--�û�
	@CREATE_TIME DATETIME = NULL,--����ʱ��
	@CREATE_USER NVARCHAR(50) = NULL,--�����û�
	@ISDELETED BIT = NULL --�Ƿ��ѱ�ɾ��
AS
BEGIN
	INSERT INTO dbo.UserRoleMap
	(
		[RoleId],
		[UserId],
		[CREATE_TIME],--����ʱ��
		[CREATE_USER],--�����û�
		[LAST_MODIFY_TIME],--���һ���޸�ʱ��
		[LAST_MODIFY_USER],--���һ���޸��û�
		[ISDELETED]--�Ƿ��ѱ�ɾ��	
	)
	VALUES
	(
		@RoleId,
		@UserId,
		ISNULL(@CREATE_TIME,GETDATE()),--����ʱ��
		ISNULL(@CREATE_USER,N''),--�����û�
		ISNULL(@CREATE_TIME,GETDATE()),
		ISNULL(@CREATE_USER,N''),
		ISNULL(@ISDELETED,0)--�Ƿ��ѱ�ɾ��	
	)
	RETURN SCOPE_IDENTITY();
END
GO

--[SPA_UserRoleMap_U]
IF (OBJECT_ID(N'dbo.[SPA_UserRoleMap_U]',N'P') IS NOT NULL)
	DROP PROC dbo.SPA_UserRoleMap_U
GO
CREATE PROC dbo.SPA_UserRoleMap_U
	@RoleId INT = NULL,--��ɫ
	@UserId INT = NULL,--�û�
	@LAST_MODIFY_TIME DATETIME = NULL,--���һ���޸�ʱ��
	@LAST_MODIFY_USER NVARCHAR(50) = NULL,--���һ���޸��û�
	
	@UserRoleMapId INT --����
AS
BEGIN
	UPDATE TOP(1) dbo.UserRoleMap
	SET [RoleId] = ISNULL(@RoleId,[RoleId]),		
		[UserId]= ISNULL(@UserId,[UserId]),
		[LAST_MODIFY_TIME]=ISNULL(@LAST_MODIFY_TIME,GETDATE()),--���һ���޸�ʱ��
		[LAST_MODIFY_USER]=ISNULL(@LAST_MODIFY_USER,[LAST_MODIFY_USER])
	WHERE UserRoleMapId = @UserRoleMapId
END
GO

--[SPA_UserRoleMap_D]
IF (OBJECT_ID(N'dbo.[SPA_UserRoleMap_D]',N'P') IS NOT NULL)
	DROP PROC dbo.SPA_UserRoleMap_D
GO
CREATE PROC dbo.SPA_UserRoleMap_D
	@UserRoleMapId INT
AS
BEGIN
	UPDATE TOP(1) dbo.UserRoleMap
	SET ISDELETED = 1
	WHERE UserRoleMapId = @UserRoleMapId
	RETURN @@ROWCOUNT
END
GO


--[SPA_MenuPermissionMap_I]
IF (OBJECT_ID(N'dbo.[SPA_MenuPermissionMap_I]',N'P') IS NOT NULL)
	DROP PROC dbo.SPA_MenuPermissionMap_I
GO
CREATE PROC SPA_MenuPermissionMap_I
	@MenuId INT = NULL,--�˵�
	@PermissionId INT = NULL,--Ȩ��
	@CREATE_TIME DATETIME = NULL,--����ʱ��
	@CREATE_USER NVARCHAR(50) = NULL,--�����û�
	@ISDELETED BIT = NULL --�Ƿ��ѱ�ɾ��
AS
BEGIN
	INSERT INTO dbo.MenuPermissionMap
	(
		[MenuId],
		[PermissionId],
		[CREATE_TIME],--����ʱ��
		[CREATE_USER],--�����û�
		[LAST_MODIFY_TIME],--���һ���޸�ʱ��
		[LAST_MODIFY_USER],--���һ���޸��û�
		[ISDELETED]--�Ƿ��ѱ�ɾ��	
	)
	VALUES
	(
		@MenuId,
		@PermissionId,
		ISNULL(@CREATE_TIME,GETDATE()),--����ʱ��
		ISNULL(@CREATE_USER,N''),--�����û�
		ISNULL(@CREATE_TIME,GETDATE()),
		ISNULL(@CREATE_USER,N''),
		ISNULL(@ISDELETED,0)--�Ƿ��ѱ�ɾ��	
	)
	RETURN SCOPE_IDENTITY();
END
GO

--[SPA_MenuPermissionMap_U]
IF (OBJECT_ID(N'dbo.[SPA_MenuPermissionMap_U]',N'P') IS NOT NULL)
	DROP PROC dbo.SPA_MenuPermissionMap_U
GO
CREATE PROC dbo.SPA_MenuPermissionMap_U
	@MenuId INT = NULL,--�˵�
	@PermissionId INT = NULL,--Ȩ��
	@LAST_MODIFY_TIME DATETIME = NULL,--���һ���޸�ʱ��
	@LAST_MODIFY_USER NVARCHAR(50) = NULL,--���һ���޸��û�
	
	@MenuPermissionMapId INT --����
AS
BEGIN
	UPDATE TOP(1) dbo.MenuPermissionMap
	SET [MenuId] = ISNULL(@MenuId,[MenuId]),		
		[PermissionId]= ISNULL(@PermissionId,[PermissionId]),
		[LAST_MODIFY_TIME]=ISNULL(@LAST_MODIFY_TIME,GETDATE()),--���һ���޸�ʱ��
		[LAST_MODIFY_USER]=ISNULL(@LAST_MODIFY_USER,[LAST_MODIFY_USER])
	WHERE MenuPermissionMapId = @MenuPermissionMapId
END
GO

--[SPA_MenuPermissionMap_D]
IF (OBJECT_ID(N'dbo.[SPA_MenuPermissionMap_D]',N'P') IS NOT NULL)
	DROP PROC dbo.SPA_MenuPermissionMap_D
GO
CREATE PROC dbo.SPA_MenuPermissionMap_D
	@MenuPermissionMapId INT
AS
BEGIN
	UPDATE TOP(1) dbo.MenuPermissionMap
	SET ISDELETED = 1
	WHERE MenuPermissionMapId = @MenuPermissionMapId
	RETURN @@ROWCOUNT
END
GO
