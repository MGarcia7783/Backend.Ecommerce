using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.Application.Dtos.Pedido
{
    public class DetallePedidoDTO
    {
        public int idDetallePedido { get; set; }
        public int idPedido { get; set; }
        public int idProducto { get; set; }
        public string nombreProducto { get; set; } = string.Empty;
        public int cantidad { get; set; }
        public decimal precioUnitario { get; set; }
    }
}
