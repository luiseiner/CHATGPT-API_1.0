using Microsoft.AspNetCore.Mvc;
using Model.Request;
using Model.Response;
using Domain;
using System.Linq;
using System;
using Infraestructure;

[ApiController]
[Route("[controller]")]
public class AlternativesController : ControllerBase
{
    private readonly ContextFactura _context;

    public AlternativesController(ContextFactura context)
    {
        _context = context;
    }

    [HttpPost]
    public IActionResult CreateAlternative([FromBody] AlternativeRequest request)
    {
        var alternative = new Alternative
        {
            QuestionID = request.QuestionID,
            TextEn = request.TextEn,
            TextEs = request.TextEs,
            TranslationChatGptEn = request.TranslationChatGptEn,
            TranslationChatGptEs = request.TranslationChatGptEs,
            CreationDate = DateTime.Now,
            ModificationDate = DateTime.Now,
            IsEnabled = true
        };

        _context.Alternatives.Add(alternative);
        _context.SaveChanges();

        var response = new AlternativeResponse
        {
            AlternativeID = alternative.AlternativeID,
            QuestionID = alternative.QuestionID,
            TextEn = alternative.TextEn,
            TextEs = alternative.TextEs,
            TranslationChatGptEn = alternative.TranslationChatGptEn,
            TranslationChatGptEs = alternative.TranslationChatGptEs,
            CreationDate = alternative.CreationDate,
            ModificationDate = alternative.ModificationDate,
            IsEnabled = alternative.IsEnabled
        };

        return CreatedAtAction(nameof(GetAlternative), new { id = alternative.AlternativeID }, response);
    }

    [HttpGet("{id}")]
    public IActionResult GetAlternative(int id)
    {
        var alternative = _context.Alternatives.Find(id);
        if (alternative == null || !alternative.IsEnabled) return NotFound();

        var response = new AlternativeResponse
        {
            AlternativeID = alternative.AlternativeID,
            QuestionID = alternative.QuestionID,
            TextEn = alternative.TextEn,
            TextEs = alternative.TextEs,
            TranslationChatGptEn = alternative.TranslationChatGptEn,
            TranslationChatGptEs = alternative.TranslationChatGptEs,
            CreationDate = alternative.CreationDate,
            ModificationDate = alternative.ModificationDate,
            IsEnabled = alternative.IsEnabled
        };

        return Ok(response);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateAlternative(int id, [FromBody] AlternativeRequest request)
    {
        var alternative = _context.Alternatives.Find(id);
        if (alternative == null || !alternative.IsEnabled) return NotFound();

        alternative.QuestionID = request.QuestionID;
        alternative.TextEn = request.TextEn;
        alternative.TextEs = request.TextEs;
        alternative.TranslationChatGptEn = request.TranslationChatGptEn;
        alternative.TranslationChatGptEs = request.TranslationChatGptEs;
        alternative.ModificationDate = DateTime.Now;

        _context.SaveChanges();

        var response = new AlternativeResponse
        {
            AlternativeID = alternative.AlternativeID,
            QuestionID = alternative.QuestionID,
            TextEn = alternative.TextEn,
            TextEs = alternative.TextEs,
            TranslationChatGptEn = alternative.TranslationChatGptEn,
            TranslationChatGptEs = alternative.TranslationChatGptEs,
            CreationDate = alternative.CreationDate,
            ModificationDate = alternative.ModificationDate,
            IsEnabled = alternative.IsEnabled
        };

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteAlternative(int id)
    {
        var alternative = _context.Alternatives.Find(id);
        if (alternative == null || !alternative.IsEnabled) return NotFound();

        alternative.IsEnabled = false;
        _context.SaveChanges();

        return NoContent();
    }

    [HttpGet]
    public IActionResult GetAllAlternatives()
    {
        var alternatives = _context.Alternatives
            .Where(a => a.IsEnabled)
            .Select(a => new AlternativeResponse
            {
                AlternativeID = a.AlternativeID,
                QuestionID = a.QuestionID,
                TextEn = a.TextEn,
                TextEs = a.TextEs,
                TranslationChatGptEn = a.TranslationChatGptEn,
                TranslationChatGptEs = a.TranslationChatGptEs,
                CreationDate = a.CreationDate,
                ModificationDate = a.ModificationDate,
                IsEnabled = a.IsEnabled
            }).ToList();

        return Ok(alternatives);
    }
}
