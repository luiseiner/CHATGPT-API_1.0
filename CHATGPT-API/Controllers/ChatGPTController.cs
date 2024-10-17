using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infraestructure;
using Domain;
using Model.Request;
using api_form.Services;
using Model.Response;

[ApiController]
[Route("[controller]")]
public class ChatGptController : ControllerBase
{
    private readonly ContextFactura _context;
    private readonly ChatGPTService _chatGptService;

    public ChatGptController(ContextFactura context, ChatGPTService chatGptService)
    {
        _context = context;
        _chatGptService = chatGptService;
    }

    //[HttpPost("ProcessUserResponses")]
    //public async Task<IActionResult> ProcessUserResponses([FromBody] UserResponseRequestSimple userResponseRequest)
    //{
    //    var prompt = BuildChatGPTPrompt(userResponseRequest);
    //    var chatGPTResponse = await _chatGptService.AskChatGPT(prompt);

    //    var chatGptResponseEntry = new ChatGptResponse
    //    {
    //        UserID = userResponseRequest.UserID,
    //        CategoryName = userResponseRequest.CategoryName,
    //        Request = prompt,
    //        Response = chatGPTResponse,
    //        CreationDate = DateTime.Now,
    //        ModificationDate = DateTime.Now,
    //        IsEnabled = true
    //    };

    //    _context.ChatGptResponses.Add(chatGptResponseEntry);
    //    _context.SaveChanges();

    //    return Ok(new { response = chatGPTResponse });
    //}

    //private string BuildChatGPTPrompt(UserResponseRequestSimple userResponseRequest)
    //{
    //    StringBuilder prompt = new StringBuilder();
    //    prompt.AppendLine($"Crea un \"{userResponseRequest.CategoryName}\" con la siguiente información:");
    //    prompt.AppendLine("Te voy a pasar una serie de preguntas con sus respuestas. Quiero que las consideres para que puedas dar una respuesta más precisa.");

    //    foreach (var question in userResponseRequest.Questions)
    //    {
    //        prompt.AppendLine($"{question.QuestionText}");
    //        prompt.AppendLine($"Selected Answer: {question.SelectedAnswer}");
    //    }

    //    return prompt.ToString();
    //}

    //private string BuildChatGPTPrompt(string categoryName, List<QuestionResponseSimple> questions)
    //{
    //    StringBuilder prompt = new StringBuilder();
    //    prompt.AppendLine($"Category: {categoryName}");
    //    prompt.AppendLine("Here are the questions and selected answers:");

    //    foreach (var question in questions)
    //    {
    //        string questionText = question.QuestionText;
    //        string selectedAnswer = question.SelectedAnswer;
    //        prompt.AppendLine($"Question: {questionText}");
    //        prompt.AppendLine($"Selected Answer: {selectedAnswer}");
    //    }

    //    prompt.AppendLine("Please provide a detailed response based on the above information.");

    //    return prompt.ToString();
    //}

    [HttpPost("ProcessUserResponses")]
    public async Task<IActionResult> ProcessUserResponses([FromBody] UserResponseRequestSimple userResponseRequest)
    {
        var prompt = BuildChatGPTPrompt(userResponseRequest);
        var chatGPTResponse = await _chatGptService.AskChatGPT(prompt);

        var chatGptResponseEntry = new ChatGptResponse
        {
            UserID = userResponseRequest.UserID,
            CategoryName = userResponseRequest.CategoryName,
            Request = prompt,
            Response = chatGPTResponse,
            CreationDate = DateTime.Now,
            ModificationDate = DateTime.Now,
            IsEnabled = true
        };

        _context.ChatGptResponses.Add(chatGptResponseEntry);
        _context.SaveChanges();

        return Ok(new { response = chatGPTResponse });
    }

    private string BuildChatGPTPrompt(UserResponseRequestSimple userResponseRequest)
    {
        StringBuilder prompt = new StringBuilder();

        // Usar el prompt proporcionado por el cliente
        string categoryPrompt = userResponseRequest.CategoryPrompt.Replace("{respuesta}", userResponseRequest.CategoryName);
        prompt.AppendLine(categoryPrompt);

        // Unir las respuestas de las preguntas en un solo texto con puntos finales
        foreach (var question in userResponseRequest.Questions)
        {
            string selectedAnswer = question.SelectedAnswer;

            // Reemplaza {respuesta} en el TranslationChatGpt con la respuesta seleccionada
            string customizedPrompt = question.TranslationChatGpt.Replace("{respuesta}", selectedAnswer);

            prompt.AppendLine($"{customizedPrompt}.");
        }

        return prompt.ToString();
    }


    [HttpGet("GetAllResponses")]
    public IActionResult GetAllResponses()
    {
        var responses = _context.ChatGptResponses
            .Where(r => r.IsEnabled)
            .Select(r => new
            {
                r.ResponseID,
                r.UserID,
                r.CategoryName,
                r.Request,
                r.Response,
                r.CreationDate,
                r.ModificationDate
            }).ToList();

        return Ok(responses);
    }
    [HttpPost("RequestAndSaveResponse")]
    public async Task<IActionResult> RequestAndSaveResponse([FromBody] ChatGptRequestModel request)
    {

        var chatGPTResponse = await _chatGptService.AskChatGPT(request.Request);
        var chatGptResponseEntry = new ChatGptResponse
        {
            UserID = request.UserID,
            CategoryName = request.CategoryName,
            Request = request.Request,
            Response = chatGPTResponse,
            CreationDate = DateTime.Now,
            ModificationDate = DateTime.Now,
            IsEnabled = true
        };

        _context.ChatGptResponses.Add(chatGptResponseEntry);
        _context.SaveChanges();

        var responseModel = new ChatGptResponseModel
        {
            UserID = chatGptResponseEntry.UserID,
            CategoryName = chatGptResponseEntry.CategoryName,
            Request = chatGptResponseEntry.Request,
            Response = chatGptResponseEntry.Response,
            CreationDate = chatGptResponseEntry.CreationDate,
            ModificationDate = chatGptResponseEntry.ModificationDate
        };

        return Ok(responseModel);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetResponseById(int id)
    {
        var response = await _context.ChatGptResponses.FindAsync(id);
        if (response == null)
        {
            return NotFound();
        }
        return Ok(response);
    }
    [HttpGet("GetLatestResponse")]
    public IActionResult GetLatestResponse()
    {
        // Obtiene la última respuesta habilitada
        var latestResponse = _context.ChatGptResponses
            .Where(r => r.IsEnabled)
            .OrderByDescending(r => r.CreationDate) // O puedes usar r.ResponseID
            .Select(r => new
            {
                r.ResponseID,
                r.UserID,
                r.CategoryName,
                r.Request,
                r.Response,
                r.CreationDate,
                r.ModificationDate
            })
            .FirstOrDefault(); // Obtiene el primer registro después de ordenar

        if (latestResponse == null)
        {
            return NotFound(); // Retorna 404 si no se encuentra ninguna respuesta
        }

        return Ok(latestResponse); // Retorna la última respuesta encontrada
    }

    [HttpGet("GetLatestRequest")]
    public IActionResult GetLatestRequest()
    {
        // Obtiene la última solicitud habilitada
        var latestRequest = _context.ChatGptResponses
            .Where(r => r.IsEnabled)
            .OrderByDescending(r => r.CreationDate)
            .Select(r => r.Request) // Solo selecciona el campo Request
            .FirstOrDefault(); // Obtiene el primer registro después de ordenar

        if (latestRequest == null)
        {
            return NotFound(); // Retorna 404 si no se encuentra ningún request
        }

        return Ok(new { Request = latestRequest }); // Retorna el request encontrado
    }

}