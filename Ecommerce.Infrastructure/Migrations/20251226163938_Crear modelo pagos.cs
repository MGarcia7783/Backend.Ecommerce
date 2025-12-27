using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Crearmodelopagos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetallePedido_Pedidos_idPedido",
                table: "DetallePedido");

            migrationBuilder.DropForeignKey(
                name: "FK_DetallePedido_Productos_idProducto",
                table: "DetallePedido");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DetallePedido",
                table: "DetallePedido");

            migrationBuilder.RenameTable(
                name: "DetallePedido",
                newName: "DetallesPedidos");

            migrationBuilder.RenameIndex(
                name: "IX_DetallePedido_idProducto",
                table: "DetallesPedidos",
                newName: "IX_DetallesPedidos_idProducto");

            migrationBuilder.RenameIndex(
                name: "IX_DetallePedido_idPedido",
                table: "DetallesPedidos",
                newName: "IX_DetallesPedidos_idPedido");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DetallesPedidos",
                table: "DetallesPedidos",
                column: "idDetallePedido");

            migrationBuilder.CreateTable(
                name: "Pagos",
                columns: table => new
                {
                    idPago = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    monto = table.Column<decimal>(type: "decimal(11,2)", nullable: false),
                    moneda = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    estado = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    idPedido = table.Column<int>(type: "int", nullable: true),
                    fechaPago = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pagos", x => x.idPago);
                    table.CheckConstraint("CK_Pago_Estado", "\"estado\" IN ('Aprobado', 'Rechazado', 'Cancelado', 'Pendiente')");
                    table.ForeignKey(
                        name: "FK_Pagos_Pedidos_idPedido",
                        column: x => x.idPedido,
                        principalTable: "Pedidos",
                        principalColumn: "idPedido");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pagos_idPedido",
                table: "Pagos",
                column: "idPedido");

            migrationBuilder.AddForeignKey(
                name: "FK_DetallesPedidos_Pedidos_idPedido",
                table: "DetallesPedidos",
                column: "idPedido",
                principalTable: "Pedidos",
                principalColumn: "idPedido",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DetallesPedidos_Productos_idProducto",
                table: "DetallesPedidos",
                column: "idProducto",
                principalTable: "Productos",
                principalColumn: "idProducto",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetallesPedidos_Pedidos_idPedido",
                table: "DetallesPedidos");

            migrationBuilder.DropForeignKey(
                name: "FK_DetallesPedidos_Productos_idProducto",
                table: "DetallesPedidos");

            migrationBuilder.DropTable(
                name: "Pagos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DetallesPedidos",
                table: "DetallesPedidos");

            migrationBuilder.RenameTable(
                name: "DetallesPedidos",
                newName: "DetallePedido");

            migrationBuilder.RenameIndex(
                name: "IX_DetallesPedidos_idProducto",
                table: "DetallePedido",
                newName: "IX_DetallePedido_idProducto");

            migrationBuilder.RenameIndex(
                name: "IX_DetallesPedidos_idPedido",
                table: "DetallePedido",
                newName: "IX_DetallePedido_idPedido");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DetallePedido",
                table: "DetallePedido",
                column: "idDetallePedido");

            migrationBuilder.AddForeignKey(
                name: "FK_DetallePedido_Pedidos_idPedido",
                table: "DetallePedido",
                column: "idPedido",
                principalTable: "Pedidos",
                principalColumn: "idPedido",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DetallePedido_Productos_idProducto",
                table: "DetallePedido",
                column: "idProducto",
                principalTable: "Productos",
                principalColumn: "idProducto",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
