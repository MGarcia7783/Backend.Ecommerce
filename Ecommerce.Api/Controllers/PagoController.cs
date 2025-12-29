using Ecommerce.Application.Dtos.Pago;
using Ecommerce.Application.Interfaces.Service;
using Ecommerce.Application.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PagoController : ControllerBase
    {
        private readonly IPagoService _service;

        public PagoController(IPagoService service)
        {
            _service = service;
        }

        [Authorize(Roles = "Cliente, Administrador")]
        [HttpPost("generar-pago")]
        public async Task<IActionResult> GenerarPago([FromBody] CrearPagoDTO pagoDTO)
        {
            var pago = await _service.CrearAsync(pagoDTO);
            return Ok(pago);
        }

        [Authorize(Roles = "Administrador")]
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? nombreCompleto, [FromQuery] int numeroPagina = 1, [FromQuery] int pageSize = 10)
        {
            var pagos = await _service.ObtenerPaginadosAsync(nombreCompleto, numeroPagina, pageSize);
            if (pagos == null || !pagos.Any())
                return NotFound("No hay pagos registrados.");

            var totalPagos = await _service.ContarAsync(nombreCompleto);
            return Ok(new RespuestaPaginada<PagoDTO>(pagos, totalPagos, numeroPagina, pageSize));
        }
    }
}
