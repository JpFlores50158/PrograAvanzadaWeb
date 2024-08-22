USE [GTP]
GO

/****** Object:  StoredProcedure [dbo].[Login]    Script Date: 8/19/2024 4:37:19 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

create procedure [dbo].[Login](@User nvarchar(max), @Contreseña nvarchar(max))
as begin 
   select * from Usuarios where CorreoElectronico=@User and Contrasena=@Contreseña
    
end
GO

USE [GTP]
GO

/****** Object:  StoredProcedure [dbo].[Registrar]    Script Date: 8/19/2024 4:37:30 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Registrar]
    @nombre NVARCHAR(MAX),
    @Correo NVARCHAR(MAX),
    @Contrasena NVARCHAR(MAX)
AS
BEGIN
    BEGIN TRY
        -- Verifica si el correo electrónico ya existe
        IF EXISTS (SELECT 1 FROM [dbo].[Usuarios] WHERE [CorreoElectronico] = @Correo)
        BEGIN
            -- Retorna un valor que indica que el usuario ya existe
            SELECT -1 AS Resultado
            RETURN
        END

        -- Intenta insertar el usuario si no existe
        INSERT INTO [dbo].[Usuarios]
            ([Nombre], [CorreoElectronico], [Contrasena])
        VALUES
            (@nombre, @Correo, @Contrasena)

        -- Retorna un valor de éxito
        SELECT 1 AS Resultado
    END TRY
    BEGIN CATCH
        -- Retorna un valor de error
        SELECT 0 AS Resultado
    END CATCH
END
GO




USE [GTP]
GO

INSERT INTO [dbo].[Roles]
           ([Nombre]
           ,[Descripcion])
     VALUES
           ('Admin'
           ,'Administrador o Creador del proyecto')
GO


INSERT INTO [dbo].[Roles]
           ([Nombre]
           ,[Descripcion])
     VALUES
           ('CoAdmin'
           ,'Adminstrador pero no creador')
GO

INSERT INTO [dbo].[Roles]
           ([Nombre]
           ,[Descripcion])
     VALUES
           ('Usuario'
           ,'Usuario del proyecto')
GO
