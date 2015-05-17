DECLARE @AdminId INT,
		@MenuId INT

TRUNCATE TABLE dbo.[Users]
EXEC @AdminId = dbo.SPA_Users_I @UserName=N'admin',@Password='',@HashCode='',@NickName=N'系统管理员',@Status='Normal',@CREATE_USER='admin'

TRUNCATE TABLE dbo.[Menus]
EXEC @MenuId = dbo.SPA_Menus_I @MenuName=N'控制面板',@IsHide=0,@MenuType='Limb',@LinkAddress=''
EXEC @MenuId = dbo.SPA_Menus_I @MenuName=N'权限管理',@IsHide=0,@MenuType='Limb',@ParentId=@MenuId,@LinkAddress=''
EXEC @MenuId = dbo.SPA_Menus_I @MenuName=N'权限管理',@IsHide=0,@MenuType='Leaf',@ParentId=@MenuId,@LinkAddress=''