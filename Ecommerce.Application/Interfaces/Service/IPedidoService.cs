using Ecommerce.Application.Dtos.Pedido;
using Ecommerce.Domain.Entities;

namespace Ecommerce.Application.Interfaces.Service
{
    public interface IPedidoService
    {
        Task<ICollection<PedidoDTO>> ObtenerPaginadosAsync(int pagina, int tamano);
        Task<int> ContarAsync();

        Task<ICollection<PedidoDTO>> ObtenerPorUsuarioPaginadosAsync(string idUsuario, int pagina, int tamano);
        Task<int> ContarPorUsuarioAsync(string idUsuario);

        Task<ICollection<PedidoDTO>> ObtenerPorFechaPaginadosAsync(DateTime fechaInicio, DateTime fechaFin, int pagina, int tamano);
        Task<int> ContarPorFechaAsync(DateTime fechaInicio, DateTime fechaFin);

        Task<PedidoDTO?> ObtenerPorIdAsync(int idPedido);

        Task<PedidoDTO> CrearPedidoAsync(CrearPedidoDTO dto);
        Task ActualizarEstadoAsync(int idpedido, string nuevoEstado);
    }
}
