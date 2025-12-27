using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Api.Request
{
    public class CrearProductoRequest
    {
        [Required(ErrorMessage = "El nombre del producto es requerido.")]
        [MaxLength(40, ErrorMessage = "El nombre del producto no debe exceder los 40 caracteres.")]
        public string nombreProducto { get; set; } = string.Empty;

        [Required(ErrorMessage = "La descripción del producto es requerida.")]
        [MaxLength(500, ErrorMessage = "La descripción del producto no debe exceder los 500 caracteres.")]
        public string descripcionProducto { get; set; } = string.Empty;

        [Range(0.01, double.MaxValue, ErrorMessage = "El precio del producto debe ser mayor a cero.")]
        public decimal precio { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "El stock del producto debe ser mayor a cero.")]
        public int stock { get; set; }

        [Required(ErrorMessage = "La sección del producto es requerida.")]
        [MaxLength(6, ErrorMessage = "La sección del producto no debe exceder los 6 caracteres.")]
        public string seccion { get; set; } = string.Empty;

        [Required(ErrorMessage = "La categoría del producto es requerida.")]
        public int idCategoria { get; set; }


        [Required(ErrorMessage = "Debe enviar al menos una imagen.")]
        public List<IFormFile>? imagenes { get; set; }
    }
}
