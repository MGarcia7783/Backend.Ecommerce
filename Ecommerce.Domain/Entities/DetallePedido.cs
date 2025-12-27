using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.Domain.Entities
{
    public class DetallePedido
    {
        public int idDetallePedido { get; set; }
        public int idPedido { get; set; }
        public Pedido? Pedido { get; set; }

        public int idProducto { get; set; }
        public Producto? Producto { get; set; }

        public string nombreProducto { get; set; } = string.Empty;
        public int cantidad { get; set; }
        public decimal precioUnitario { get; set; }
        public decimal subtotal { get; set; }
    }
}
