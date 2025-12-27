using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Crearmodeloproductos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Productos",
                columns: table => new
                {
                    idProducto = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombreProducto = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    descripcionProducto = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    precio = table.Column<decimal>(type: "decimal(11,2)", nullable: false),
                    stock = table.Column<int>(type: "int", nullable: false),
                    seccion = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false),
                    imagen1 = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    imagen2 = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    imagen3 = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    estado = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    fechaRegistro = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    idCategoria = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Productos", x => x.idProducto);
                    table.CheckConstraint("CK_Producto_Estado", "\"estado\" IN ('Activo', 'Inactivo')");
                    table.CheckConstraint("CK_Producto_Seccion", "\"seccion\" IN('Hombre', 'Mujer', 'Unisex')");
                    table.ForeignKey(
                        name: "FK_Productos_Categorias_idCategoria",
                        column: x => x.idCategoria,
                        principalTable: "Categorias",
                        principalColumn: "idCategoria",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Productos_idCategoria",
                table: "Productos",
                column: "idCategoria");

            migrationBuilder.CreateIndex(
                name: "IX_Productos_nombreProducto",
                table: "Productos",
                column: "nombreProducto",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Productos");
        }
    }
}
