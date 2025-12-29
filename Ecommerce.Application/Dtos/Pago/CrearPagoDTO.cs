using Ecommerce.Application.Dtos.Pedido;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ecommerce.Application.Dtos.Pago
{
    public class CrearPagoDTO
    {
        [Required]
        public decimal monto { get; set; }

        [Required]
        public string metodoPago { get; set; } = "Paypal";

        // Pedido se creará automáticamente
        [Required]
        public CrearPedidoDTO pedido { get; set; } = new CrearPedidoDTO();
    }
}
