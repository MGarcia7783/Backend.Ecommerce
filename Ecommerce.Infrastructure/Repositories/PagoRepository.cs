using Ecommerce.Application.Interfaces.Repository;
using Ecommerce.Domain.Entities;
using Ecommerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.Infrastructure.Repositories
{
    public class PagoRepository : IPagoRepository
    {
        private readonly EcommerceDbContext _context;

        public PagoRepository(EcommerceDbContext context)
        {
            _context = context;
        }

        public Task AcualizarAsync(Pago pago)
        {
            _context.Pagos.Update(pago);
            return _context.SaveChangesAsync();
        }

        public async Task<int> ContarAsync(string? nombreCliente)
        {
            var consulta = _context.Pagos
                .Include(x => x.Pedido)!.ThenInclude(pe => pe!.Usuario)
                .AsQueryable();

            return await consulta.CountAsync();
        }

        public async Task<Pago> CrearAsync(Pago pago)
        {
            await _context.Pagos.AddAsync(pago);
            await _context.SaveChangesAsync();
            return pago;
        }

        public async Task<ICollection<Pago>> ObtenerPaginadosAsync(string? nombreCliente, int pagina, int tamano)
        {
            if (pagina <= 0) pagina = 1;
            if (tamano <= 0) tamano = 10;

            var consulta = _context.Pagos
                .Include(p => p.Pedido)!.ThenInclude(pe => pe!.Usuario)
                .AsQueryable();

            if (!string.IsNullOrEmpty(nombreCliente))
                consulta = consulta.Where(p => p.Pedido!.Usuario!.nombreCompeto.Contains(nombreCliente));

            return await consulta
                .OrderByDescending(p => p.fechaPago)
                .Skip((pagina -1) * tamano)
                .Take(tamano)
                .ToListAsync();
        }

        public async Task<Pago?> ObtenerPorIdAsync(int idPago)
        {
            return await _context.Pagos.FindAsync(idPago);
        }
    }
}
