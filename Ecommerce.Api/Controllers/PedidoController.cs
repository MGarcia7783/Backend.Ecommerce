using Ecommerce.Application.Dtos.Pedido;
using Ecommerce.Application.Interfaces.Service;
using Ecommerce.Application.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Administrador")]
    public class PedidoController : ControllerBase
    {
        private readonly IPedidoService _service;

        public PedidoController(IPedidoService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int numeroPagina = 1, [FromQuery] int pageSize = 10)
        {
            var pedidos = await _service.ObtenerPaginadosAsync(numeroPagina, pageSize);
            if (pedidos == null || !pedidos.Any())
                return NotFound("No hay registros disponibles.");

            var totalPedidos = await _service.ContarAsync();

            return Ok(new RespuestaPaginada<PedidoDTO>(pedidos, totalPedidos, numeroPagina, pageSize));
        }

        [HttpGet("{id:int}", Name = "GetPedido")]
        public async Task<IActionResult> GetPedido(int id)
        {
            var registro = await _service.ObtenerPorIdAsync(id);
            return Ok(registro);
        }

        [HttpGet("buscar-por-usuario")]
        [Authorize(Roles = "Administrador, Cliente")]
        public async Task<IActionResult> GetPedidoPorUsuario([FromQuery] string idUsuario, [FromQuery] int numeroPagina = 1, [FromQuery] int pageSize = 10)
        {
            var pedidos = await _service.ObtenerPorUsuarioPaginadosAsync(idUsuario, numeroPagina, pageSize);
            if (pedidos == null || !pedidos.Any())
                return NotFound();

            var totalPedidos = await _service.ContarPorUsuarioAsync(idUsuario);
            return Ok(new RespuestaPaginada<PedidoDTO>(pedidos, totalPedidos, numeroPagina, pageSize));
        }

        [HttpGet("buscar-por-fecha")]
        public async Task<IActionResult> GetPedidoPorFecha([FromQuery] DateTime fechaInicio, [FromQuery] DateTime fechaFin, [FromQuery] int numeroPagina = 1, [FromQuery] int pageSize = 10)
        {
            var pedidos = await _service.ObtenerPorFechaPaginadosAsync(fechaInicio, fechaFin, numeroPagina, pageSize);
            if (pedidos == null || !pedidos.Any())
                return NotFound();

            var totalPedidos = await _service.ContarPorFechaAsync(fechaInicio, fechaFin);
            return Ok(new RespuestaPaginada<PedidoDTO>(pedidos, totalPedidos, numeroPagina, pageSize));
        }

        [HttpPatch("actualizar-estado/{idPedido}")]
        public async Task<IActionResult> ActualizarEstadoPedido(int idPedido, [FromQuery] string nuevoEstado)
        {
            await _service.ActualizarEstadoAsync(idPedido, nuevoEstado);
            return NoContent();
        }
    }
}
