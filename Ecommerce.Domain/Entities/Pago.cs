using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.Domain.Entities
{
    public class Pago
    {
        public int idPago { get; set; }
        public decimal monto { get; set; }
        public string moneda { get; set; } = "USD";
        public string estado { get; set; } = "Pendiente";

        public int? idPedido { get; set; }
        public Pedido? Pedido { get; set; }
        public DateTimeOffset fechaPago { get; set; } = DateTimeOffset.Now;
    }
}
