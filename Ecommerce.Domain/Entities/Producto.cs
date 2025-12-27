using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.Domain.Entities
{
    public class Producto
    {
        public int idProducto { get; set; }
        public string nombreProducto { get; set; } = string.Empty;
        public string descripcionProducto { get; set; } = string.Empty;
        public decimal precio { get; set; }
        public int stock { get; set; }
        public string seccion { get; set; } = string.Empty;
        public string imagen1 {  get; set; } = string.Empty;
        public string imagen2 {  get; set; } = string.Empty;
        public string imagen3 {  get; set; } = string.Empty;
        public string estado { get; set; } = "Activo";
        public DateTimeOffset fechaRegistro { get; set; } = DateTimeOffset.Now;

        public int idCategoria { get; set; }
        public Categoria? Categoria { get; set; }   // Propiedad de navegacion
        public ICollection<DetallePedido> Detalles { get; set; } = new List<DetallePedido>();
    }
}
