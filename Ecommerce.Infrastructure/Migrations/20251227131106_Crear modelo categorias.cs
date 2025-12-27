using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Crearmodelocategorias : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categorias",
                columns: table => new
                {
                    idCategoria = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombreCategoria = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    descripcionCategoria = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    estado = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    fechaRegistro = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorias", x => x.idCategoria);
                    table.CheckConstraint("CK_Categoria_Estado", "\"estado\" IN ('Activo', 'Inactivo')");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categorias_nombreCategoria",
                table: "Categorias",
                column: "nombreCategoria",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Categorias");
        }
    }
}
