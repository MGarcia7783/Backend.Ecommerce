
namespace Ecommerce.Domain.Entities
{
    public class Categoria
    {
        public int idCategoria { get; set; }
        public string nombreCategoria { get; set; } = string.Empty;
        public string descripcionCategoria { get; set; } = string.Empty;
        public string estado { get; set; } = "Activo";
        public DateTimeOffset fechaRegistro { get; set; } = DateTimeOffset.Now;

        public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
    }
}
