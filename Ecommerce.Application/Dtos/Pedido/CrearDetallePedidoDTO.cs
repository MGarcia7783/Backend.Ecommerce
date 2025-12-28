using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ecommerce.Application.Dtos.Pedido
{
    public class CrearDetallePedidoDTO
    {
        [Required(ErrorMessage = "El ID del producto es requerido.")]
        public int idProducto { get; set; }

        [Required(ErrorMessage = "El nombre del producto es requerido.")]
        [MaxLength(40, ErrorMessage = "El nombre del producto no puede exceder los 40 caracteres.")]
        public string nombreProducto { get; set; } = string.Empty;

        [Required(ErrorMessage = "La cantidad es requerida.")]
        [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser un número entero mayor a cero.")]
        public int cantidad { get; set; }

        [Required(ErrorMessage = "El precio uitario es requerido.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El precio del producto debe mayor a cero.")]
        public decimal precioUnitario { get; set; }
    }
}
