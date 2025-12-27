using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Api.Request
{
    public class ActualizarProductoRequest : CrearProductoRequest
    {
        [Required]
        public int idProducto { get; set; }
    }
}
