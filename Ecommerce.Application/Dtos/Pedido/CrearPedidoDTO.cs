using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ecommerce.Application.Dtos.Pedido
{
    public class CrearPedidoDTO
    {
        [Required(ErrorMessage = "El ID del usuario es requerido.")]
        public string idUsuario { get; set; } = string.Empty;

        [Required(ErrorMessage = "El nombre del cliente es requerido.")]
        [MaxLength(75, ErrorMessage = "El nombre del cliente no puede exceder los 75 caracteres.")]
        public string nombreCliente { get; set; } = string.Empty;

        [Required(ErrorMessage = "La dirección del envío es requerida.")]
        [MaxLength(250, ErrorMessage = "La dirección del envío no puede exceder los 250 caracteres.")]
        public string direccionEnvio { get; set; } = string.Empty;

        [Required(ErrorMessage = "El teléfono es requerido.")]
        [MaxLength(15, ErrorMessage = "el teléfono no puede exceder los 15 caracteres.")]
        public string telefono { get; set; } = string.Empty;

        [Required(ErrorMessage = "El total del pedido es requerido.")]
        public decimal total { get; set; }

        public List<CrearDetallePedidoDTO> Detalles { get; set; } = new List<CrearDetallePedidoDTO>();
    }
}
