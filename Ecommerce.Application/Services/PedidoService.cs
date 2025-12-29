using AutoMapper;
using Ecommerce.Application.Dtos.Categoria;
using Ecommerce.Application.Dtos.Pedido;
using Ecommerce.Application.Interfaces.Repository;
using Ecommerce.Application.Interfaces.Service;
using Ecommerce.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.Application.Services
{
    public class PedidoService : IPedidoService
    {
        private readonly IPedidoRepository _repository;
        private readonly UserManager<Usuario> _userManager;
        private readonly IMapper _mapper;

        public PedidoService(IPedidoRepository repository, UserManager<Usuario> userManager, IMapper mapper)
        {
            _repository = repository;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task ActualizarEstadoAsync(int idpedido, string nuevoEstado)
        {
            var permitidos = new[] { "Pediente", "Entregado", "Cancelado" };

            if (!permitidos.Contains(nuevoEstado))
                throw new ArgumentException("El estado proporcionado no es válido.");

            var pedido = await _repository.ObtenerPorIdAsync(idpedido);
            if (pedido == null)
                throw new ArgumentException("El pedido no existe.");

            var estadoEstual = pedido.estado;

            // Reglas de negocio para cambios de estado
            if (estadoEstual == nuevoEstado)
                throw new InvalidOperationException($"El pedido ya se encuentra en estado: '{nuevoEstado}'.");

            if (estadoEstual == "Entregado")
                throw new InvalidOperationException("El pedido ya fue entregado y no se puede modificar.");

            if (estadoEstual == "Cancelado")
                throw new InvalidOperationException("el pedido ya ha sido cancelado y no puede modificarse.");

            if (nuevoEstado == "Cancelado")
            {
                var productosActualizar = new List<Producto>();

                foreach(var detalle in pedido.Detalles)
                {
                    var producto = await _repository.ObtenerProductoPorIdAsync(detalle.idProducto);
                    if(producto != null)
                    {
                        producto.stock += detalle.cantidad;
                        productosActualizar.Add(producto);
                    }
                }

                await _repository.ActualizarStockAsync(productosActualizar);
            }

            pedido.estado = nuevoEstado;
            await _repository.GuardarCambiosAsync();
        }

        public async Task<int> ContarAsync()
        {
            return await _repository.ContarAsync(); 
        }

        public async Task<int> ContarPorFechaAsync(DateTime fechaInicio, DateTime fechaFin)
        {
            return await _repository.ContarPorFechaAsync(fechaInicio, fechaFin);
        }

        public async Task<int> ContarPorUsuarioAsync(string idUsuario)
        {
            return await _repository.ContarPorUsuarioAsync(idUsuario);
        }

        public async Task<PedidoDTO> CrearPedidoAsync(CrearPedidoDTO dto)
        {
            if(dto == null)
                throw new ArgumentNullException("El pedido no puede ser nulo.");

            if (dto.total <= 0)
                throw new InvalidOperationException("El total del pedido debe ser mayor a cero.");

            if (dto.Detalles == null || !dto.Detalles.Any())
                throw new ArgumentException("El pedido debe contener al menos un detalle.");

            var usuario = await _userManager.FindByIdAsync(dto.idUsuario);
            if (usuario == null)
                throw new ArgumentException("El usuario asignado no existe.");

            // Validar si hay productos repetidos
            if (dto.Detalles.GroupBy(d => d.idProducto).Any(g => g.Count() > 1))
                throw new ArgumentException("No se permiten productos repetidos en el pedido.");

            // Validar productos y el stock
            var productosEditados = new List<Producto>();

            foreach (var item in dto.Detalles)
            {
                var producto = await _repository.ObtenerProductoPorIdAsync(item.idProducto);

                if(producto != null)
                {
                    if (item.cantidad <= 0)
                        throw new ArgumentException("La cantidad debe ser mayor a cero.");

                    item.precioUnitario = producto.precio;

                    if (producto.stock < item.cantidad)
                        throw new InvalidOperationException($"Stock insuficiente para el producto: '{producto.nombreProducto}'. " +
                                                            $"Disponible: '{producto.stock}', solicitado: '{item.cantidad}'.");

                    // Reducir el stock y agregar a la lista de productos editados
                    producto.stock -= item.cantidad;
                    productosEditados.Add(producto);
                }
            }

            // Mapear DTO a entidad
            var pedido = _mapper.Map<Pedido>(dto);

            // Guardar en la base de datos
            var nuevoPedido = await _repository.CrearAsync(pedido, productosEditados);

            // Mapear para la respuesta
            return _mapper.Map<PedidoDTO>(nuevoPedido);
        }

        public async Task<ICollection<PedidoDTO>> ObtenerPaginadosAsync(int pagina, int tamano)
        {
            var pedidos = await _repository.ObtenerPaginadosAsync(pagina, tamano);
            return _mapper.Map<ICollection<PedidoDTO>>(pedidos);
        }

        public async Task<ICollection<PedidoDTO>> ObtenerPorFechaPaginadosAsync(DateTime fechaInicio, DateTime fechaFin, int pagina, int tamano)
        {
            if (fechaInicio > fechaFin)
                throw new ArgumentException("La fecha de inicio no puede ser mayor que la fecha de fin.");

            var pedidos = await _repository.ObtenerPorFechaPaginadosAsync(fechaInicio, fechaFin, pagina, tamano);
            if (pedidos == null)
                throw new KeyNotFoundException($"No se encontró ningún pedido en el rango de fecjas del: '{fechaInicio}' hasta el: '{fechaFin}'.");

            return _mapper.Map<ICollection<PedidoDTO>>(pedidos);
        }

        public async Task<PedidoDTO?> ObtenerPorIdAsync(int idPedido)
        {
            if (idPedido <= 0)
                throw new ArgumentException("el ID del pedido no es válido.");

            var pedido = await _repository.ObtenerPorIdAsync(idPedido);
            if (pedido == null)
                throw new KeyNotFoundException($"No se encontró el pedido con ID: '{idPedido}'.");

            return _mapper.Map<PedidoDTO>(pedido);
        }

        public async Task<ICollection<PedidoDTO>> ObtenerPorUsuarioPaginadosAsync(string idUsuario, int pagina, int tamano)
        {
            if (idUsuario == null)
                throw new ArgumentNullException("El ID del usuario no puede ser nulo.");

            var pedidos = await _repository.ObtenerPorUsuarioPaginadosAsync(idUsuario, pagina, tamano);
            if (pedidos == null)
                throw new KeyNotFoundException($"No se encontró ningún pedido para el usuario con ID: '{idUsuario}'");

            return _mapper.Map<ICollection<PedidoDTO>>(pedidos);
        }
    }
}
