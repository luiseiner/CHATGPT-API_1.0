using OpenAI_API;
using OpenAI_API.Chat;
using System;
using System.Threading.Tasks;

namespace api_form.Services
{
    public class ChatGPTService
    {
        private readonly OpenAIAPI _api;

        // Constructor que inicializa la API con la clave proporcionada
        public ChatGPTService(string apiKey)
        {
            _api = new OpenAIAPI(apiKey);
        }

        // Método para realizar la solicitud a GPT-4 usando la API de Chat
        public async Task<string> AskChatGPT(string question)
        {
            try
            {
                // Crear un nuevo chat con GPT-4
                var chat = _api.Chat.CreateConversation();

                // Especificar que estamos usando GPT-4
                chat.Model = "gpt-4";

                // Añadir el mensaje del usuario (la pregunta)
                chat.AppendUserInput(question);

                // Obtener la respuesta del chatbot
                var response = await chat.GetResponseFromChatbotAsync();

                // Retornar la respuesta como string
                return response;
            }
            catch (Exception ex)
            {
                // Manejo de errores básico
                return $"Error: {ex.Message}";
            }
        }
    }
}