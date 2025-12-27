using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.Application.Dtos.Producto
{
    public class ProductoDTO : CrearProductoDTO
    {
        public int idProducto { get; set; }
        public string nombreCategoria { get; set; } = string.Empty;

        // Respuesta calculada
        public string UrlImagenPrincipal => imagen1;
    }
}
