using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CorregirnombreidProductoendetallePedido : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetallePedido_Productos_idProduto",
                table: "DetallePedido");

            migrationBuilder.RenameColumn(
                name: "idProduto",
                table: "DetallePedido",
                newName: "idProducto");

            migrationBuilder.RenameIndex(
                name: "IX_DetallePedido_idProduto",
                table: "DetallePedido",
                newName: "IX_DetallePedido_idProducto");

            migrationBuilder.AddForeignKey(
                name: "FK_DetallePedido_Productos_idProducto",
                table: "DetallePedido",
                column: "idProducto",
                principalTable: "Productos",
                principalColumn: "idProducto",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetallePedido_Productos_idProducto",
                table: "DetallePedido");

            migrationBuilder.RenameColumn(
                name: "idProducto",
                table: "DetallePedido",
                newName: "idProduto");

            migrationBuilder.RenameIndex(
                name: "IX_DetallePedido_idProducto",
                table: "DetallePedido",
                newName: "IX_DetallePedido_idProduto");

            migrationBuilder.AddForeignKey(
                name: "FK_DetallePedido_Productos_idProduto",
                table: "DetallePedido",
                column: "idProduto",
                principalTable: "Productos",
                principalColumn: "idProducto",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
