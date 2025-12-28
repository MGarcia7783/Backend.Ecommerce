using Ecommerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.Application.Interfaces.Repository
{
    public interface IPedidoRepository
    {
        Task<Pedido> CrearAsync(Pedido pedido, List<Producto> productosEditados);
        Task<bool> GuardarCambiosAsync();

        Task<Producto?> ObtenerProductoPorIdAsync(int idProducto);
        Task ActualizarStockAsync(List<Producto> productos);

        Task<ICollection<Pedido>> ObtenerPaginadosAsync(int pagina, int tamano);
        Task<int> ContarAsync();

        Task<ICollection<Pedido>> ObtenerPorUsuarioPaginadosAsync(string idUsuario, int pagina, int tamano);
        Task<int> ContarPorUsuarioAsync(string idUsuario);

        Task<ICollection<Pedido>> ObtenerPorFechaPaginadosAsync(DateTime fechaInicio, DateTime fechaFin, int pagina, int tamano);
        Task<int> ContarPorFechaAsync(DateTime fechaInicio, DateTime fechaFin);

        Task<Pedido?> ObtenerPorIdAsync(int idPedido);
    }
}
