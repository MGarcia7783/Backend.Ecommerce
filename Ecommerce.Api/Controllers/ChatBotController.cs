using Ecommerce.Application.Dtos.ChatBot;
using Ecommerce.Application.Interfaces.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class ChatBotController : ControllerBase
    {
        private readonly IChatBotService _chatBotService;

        public ChatBotController(IChatBotService chatBotService)
        {
            _chatBotService = chatBotService;
        }

        [HttpPost("obtener-respuesta")]
        public async Task<IActionResult> ObtenerRespuesta([FromBody] ChatBotRequestDTO request)
        {
            var response = await _chatBotService.ObtenerRespuestaAsync(request);
            return Ok(response);
        }
    }
}
