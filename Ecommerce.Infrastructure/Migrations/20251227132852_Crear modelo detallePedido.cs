using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CrearmodelodetallePedido : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DetallePedidos",
                columns: table => new
                {
                    idDetallePedido = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idPedido = table.Column<int>(type: "int", nullable: false),
                    idProducto = table.Column<int>(type: "int", nullable: false),
                    nombreProducto = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    cantidad = table.Column<int>(type: "int", nullable: false),
                    precioUnitario = table.Column<decimal>(type: "decimal(11,2)", nullable: false),
                    subtotal = table.Column<decimal>(type: "decimal(11,2)", nullable: false, computedColumnSql: "\"cantidad\" * \"precioUnitario\"", stored: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetallePedidos", x => x.idDetallePedido);
                    table.ForeignKey(
                        name: "FK_DetallePedidos_Pedidos_idPedido",
                        column: x => x.idPedido,
                        principalTable: "Pedidos",
                        principalColumn: "idPedido",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DetallePedidos_Productos_idProducto",
                        column: x => x.idProducto,
                        principalTable: "Productos",
                        principalColumn: "idProducto",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DetallePedidos_idPedido",
                table: "DetallePedidos",
                column: "idPedido");

            migrationBuilder.CreateIndex(
                name: "IX_DetallePedidos_idProducto",
                table: "DetallePedidos",
                column: "idProducto");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DetallePedidos");
        }
    }
}
