using Ecommerce.Application.Dtos.Pago;

namespace Ecommerce.Application.Interfaces.Service
{
    public interface IPagoService
    {
        Task<PagoDTO> CrearAsync(CrearPagoDTO dto);
        Task<ICollection<PagoDTO>> ObtenerPaginadosAsync(string? nombreCliente, int pagina, int tamano);
        Task<int> ContarAsync(string? nombreCliente);
    }
}
