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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pagos");
        }
    }
}
