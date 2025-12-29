using Ecommerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.Application.Interfaces.Repository
{
    public interface IPagoRepository
    {
        Task<ICollection<Pago>> ObtenerPaginadosAsync(string? nombreCliente, int pagina, int tamano);
        Task<int> ContarAsync(string? nombreCliente);
        Task<Pago?> ObtenerPorIdAsync(int idPago);
        Task<Pago> CrearAsync(Pago pago);
        Task AcualizarAsync(Pago pago);
    }
}
