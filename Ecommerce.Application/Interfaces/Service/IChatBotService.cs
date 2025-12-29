using Ecommerce.Application.Dtos.ChatBot;
using Ecommerce.Application.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.Application.Interfaces.Service
{
    public interface IChatBotService
    {
        Task<ChatBotResponseDTO> ObtenerRespuestaAsync(ChatBotRequestDTO request);
    }
}
