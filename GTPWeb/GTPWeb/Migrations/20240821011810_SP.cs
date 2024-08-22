using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GTPWeb.Migrations
{
    /// <inheritdoc />
    public partial class SP : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Crear procedimiento almacenado Login
            migrationBuilder.Sql(@"
            CREATE PROCEDURE [dbo].[Login]
                @User NVARCHAR(MAX),
                @Contrasena NVARCHAR(MAX)
            AS
            BEGIN
                SELECT * FROM [Usuarios]
                WHERE [CorreoElectronico] = @User AND [Contrasena] = @Contrasena;
            END;
        ");

            // Crear procedimiento almacenado Registrar
            migrationBuilder.Sql(@"
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
                        SELECT -1 AS Resultado;
                        RETURN;
                    END

                    -- Intenta insertar el usuario si no existe
                    INSERT INTO [dbo].[Usuarios] ([Nombre], [CorreoElectronico], [Contrasena])
                    VALUES (@nombre, @Correo, @Contrasena);

                    -- Retorna un valor de éxito
                    SELECT 1 AS Resultado;
                END TRY
                BEGIN CATCH
                    -- Retorna un valor de error
                    SELECT 0 AS Resultado;
                END CATCH
            END;
        ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Eliminar procedimientos almacenados en la reversión de la migración
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS [dbo].[Login];");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS [dbo].[Registrar];");
        }
    }
}
