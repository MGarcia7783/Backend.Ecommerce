using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.Application.Dtos.Pedido
{
    public class PedidoDTO
    {
        public int idPedido { get; set; }
        public string idUsuario { get; set; } = string.Empty;
        public string nombreCliente { get; set; } = string.Empty;
        public string direccionEnvio { get; set; } = string.Empty;
        public string telefono { get; set; } = string.Empty;
        public decimal total { get; set; }
        public string estado { get; set; } = string.Empty;
        public DateTimeOffset fechaRegistro { get; set; } = DateTimeOffset.Now;

        public List<DetallePedidoDTO> Detalles { get; set; } = new List<DetallePedidoDTO>();
    }
}
