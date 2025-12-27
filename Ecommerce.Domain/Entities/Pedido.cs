using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.Domain.Entities
{
    public class Pedido
    {
        public int idPedido { get; set; }
        public string idUsuario { get; set; } = string.Empty;
        public Usuario? Usuario { get; set; }
        public string nombreCliente { get; set; } = string.Empty;
        public string direccionEnvio { get; set; } = string.Empty;
        public string telefono { get; set; } = string.Empty;
        public decimal total { get; set; }
        public string estado { get; set; } = "Pendiente";
        public DateTimeOffset fechaRegistro { get; set; } = DateTimeOffset.Now;

        // Relación con DetallePedido
        public ICollection<DetallePedido> Detalles { get; set; } = new List<DetallePedido>();

        public ICollection<Pago> Pagos { get; set; } = new List<Pago>();
    }
}
