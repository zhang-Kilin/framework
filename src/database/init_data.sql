DECLARE @AdminId INT,
		@MenuId INT

TRUNCATE TABLE dbo.[Users]
EXEC @AdminId = dbo.SPA_Users_I @UserName=N'admin',@Password='',@HashCode='',@NickName=N'ϵͳ����Ա',@Status='Normal',@CREATE_USER='admin'

TRUNCATE TABLE dbo.[Menus]
EXEC @MenuId = dbo.SPA_Menus_I @MenuName=N'�������',@IsHide=0,@MenuType='Limb',@LinkAddress=''
EXEC @MenuId = dbo.SPA_Menus_I @MenuName=N'Ȩ�޹���',@IsHide=0,@MenuType='Limb',@ParentId=@MenuId,@LinkAddress=''
EXEC @MenuId = dbo.SPA_Menus_I @MenuName=N'Ȩ�޹���',@IsHide=0,@MenuType='Leaf',@ParentId=@MenuId,@LinkAddress=''