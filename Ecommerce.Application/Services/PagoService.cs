using AutoMapper;
using Ecommerce.Application.Dtos.Pago;
using Ecommerce.Application.Interfaces.Repository;
using Ecommerce.Application.Interfaces.Service;
using Ecommerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.Application.Services
{
    public class PagoService : IPagoService
    {
        private readonly IPagoRepository _repository;
        private readonly IMapper _mapper;
        private readonly IPedidoService _pedidoService;

        public PagoService(IPagoRepository repository, IMapper mapper, IPedidoService pedidoService)
        {
            _repository = repository;
            _mapper = mapper;
            _pedidoService = pedidoService;
        }

        public async Task<int> ContarAsync(string? nombreCliente)
        {
            return await _repository.ContarAsync(nombreCliente);
        }

        public async Task<PagoDTO> CrearAsync(CrearPagoDTO dto)
        {
            if(dto == null) 
                throw new ArgumentNullException("Los datos del pago son requeridos.");

            if (dto.monto <= 0)
                throw new ArgumentException("El monto del pago debe ser mayor a cero");

            // 1. Crear el pago
            var pago = new Pago
            {
                monto = dto.monto,
                moneda = "USD",
                estado = "Pendiente"
            };

            pago = await _repository.CrearAsync(pago);
            if (pago == null)
                throw new InvalidOperationException("Error al crear el pago.");

            // 2. Simular aprobación inmediata del pago
            pago.estado = "Aprobado";

            // 3. Crear el pedido
            var nuevoPedido = await _pedidoService.CrearPedidoAsync(dto.pedido);

            // 4. Asociar el pedido al pago
            pago.idPedido = nuevoPedido.idPedido;
            await _repository.AcualizarAsync(pago);

            // 5. Retornar DTO
            var pagoDTO = _mapper.Map<PagoDTO>(pago);
            pagoDTO.totalPedido = nuevoPedido.total;
            pagoDTO.idUsuario = nuevoPedido.idUsuario;

            return pagoDTO;
        }

        public async Task<ICollection<PagoDTO>> ObtenerPaginadosAsync(string? nombreCliente, int pagina, int tamano)
        {
            var pagos = await _repository.ObtenerPaginadosAsync(nombreCliente, pagina, tamano);
            return _mapper.Map<ICollection<PagoDTO>>(pagos);
        }
    }
}
