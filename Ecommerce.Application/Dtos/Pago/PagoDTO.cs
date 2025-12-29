using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.Application.Dtos.Pago
{
    public class PagoDTO
    {
        public int idPago { get; set; }
        public decimal monto { get; set; }
        public string moneda { get; set; } = "USD";
        public string estado { get; set; } = string.Empty;
        public DateTimeOffset fechaPago { get; set; }
        //public string transaccionId { get; set; } = string.Empty;

        // Relación con el Pedido
        public int? idPedido { get; set; }
        public decimal? totalPedido { get; set; }

        // Relación con el cliente/usuario
        public string? idUsuario { get; set; }
        public string? nombreCompleto { get; set; }
    }
}
