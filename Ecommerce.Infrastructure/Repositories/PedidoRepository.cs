using Ecommerce.Application.Interfaces.Repository;
using Ecommerce.Domain.Entities;
using Ecommerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.Infrastructure.Repositories
{
    public class PedidoRepository : IPedidoRepository
    {
        private readonly EcommerceDbContext _context;

        public PedidoRepository(EcommerceDbContext context)
        {
            _context = context;
        }

        public async Task ActualizarStockAsync(List<Producto> productos)
        {
            foreach(var p in productos)
            {
                _context.Productos.Update(p);
            }
        }

        public async Task<int> ContarAsync()
        {
            return await _context.Pedidos.CountAsync();
        }

        public async Task<int> ContarPorFechaAsync(DateTime fechaInicio, DateTime fechaFin)
        {
            return await _context.Pedidos
                .Where(p => p.fechaRegistro.Date >= fechaInicio.Date && p.fechaRegistro <= fechaFin.Date)
                .CountAsync();
        }

        public async Task<int> ContarPorUsuarioAsync(string idUsuario)
        {
            return await _context.Pedidos
                .Where(p => p.idUsuario == idUsuario)
                .CountAsync();
        }

        public async Task<Pedido> CrearAsync(Pedido pedido, List<Producto> productosEditados)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                // Crear el pedido
                _context.Pedidos.Add(pedido);
                await _context.SaveChangesAsync();

                // Actualuzar el stock de los productos
                await ActualizarStockAsync(productosEditados);

                // Guardar los cambios dentro de la transacción
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return pedido;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new InvalidOperationException("Error al crear el pedido: " + ex.Message);
            }
        }

        public async Task<bool> GuardarCambiosAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<ICollection<Pedido>> ObtenerPaginadosAsync(int pagina, int tamano)
        {
            if (pagina <= 0) pagina = 1;
            if (tamano <= 0) tamano = 10;

            return await _context.Pedidos
                .Include(x => x.Detalles)
                .OrderByDescending(x => x.fechaRegistro)
                .Skip((pagina - 1) * tamano)
                .Take(tamano)
                .ToListAsync();
        }

        public async Task<ICollection<Pedido>> ObtenerPorFechaPaginadosAsync(DateTime fechaInicio, DateTime fechaFin, int pagina, int tamano)
        {
            if (pagina <= 0) pagina = 1;
            if (tamano <= 0) tamano = 10;

            return await _context.Pedidos
                .Where(x => x.fechaRegistro.Date >= fechaInicio.Date && x.fechaRegistro.Date <= fechaFin.Date)
                .Include(x => x.Detalles)
                .OrderByDescending(x => x.fechaRegistro)
                .Skip((pagina - 1) * tamano)
                .Take(tamano)
                .ToListAsync();
        }

        public async Task<Pedido?> ObtenerPorIdAsync(int idPedido)
        {
            return await _context.Pedidos
                .Include(x => x.Detalles)
                .FirstOrDefaultAsync(x => x.idPedido == idPedido);
        }

        public async Task<ICollection<Pedido>> ObtenerPorUsuarioPaginadosAsync(string idUsuario, int pagina, int tamano)
        {
            if (pagina <= 0) pagina = 1;
            if (tamano <= 0) tamano = 10;

            return await _context.Pedidos
                .Where(x => x.idUsuario == idUsuario)
                .Include(x => x.Detalles)
                .OrderByDescending(x => x.fechaRegistro)
                .Skip((pagina - 1) * tamano)
                .Take(tamano)
                .ToListAsync();
        }

        public async Task<Producto?> ObtenerProductoPorIdAsync(int idProducto)
        {
            return await _context.Productos
                .FindAsync(idProducto);
        }
    }
}
