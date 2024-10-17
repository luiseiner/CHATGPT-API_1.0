using Microsoft.AspNetCore.Mvc;
using Model.Request;
using Model.Response;
using Domain;
using System.Linq;
using System;
using Infraestructure;

[ApiController]
[Route("[controller]")]
public class LanguagesController : ControllerBase
{
    private readonly ContextFactura _context;

    public LanguagesController(ContextFactura context)
    {
        _context = context;
    }

    [HttpPost]
    public IActionResult CreateLanguage([FromBody] LanguageRequest request)
    {
        var language = new Language
        {
            LanguageName = request.LanguageName,
            CreationDate = DateTime.Now,
            ModificationDate = DateTime.Now,
            IsEnabled = true
        };

        _context.Languages.Add(language);
        _context.SaveChanges();

        var response = new LanguageResponse
        {
            LanguageID = language.LanguageID,
            LanguageName = language.LanguageName,
            CreationDate = language.CreationDate,
            ModificationDate = language.ModificationDate,
            IsEnabled = language.IsEnabled
        };

        return CreatedAtAction(nameof(GetLanguage), new { id = language.LanguageID }, response);
    }

    [HttpGet("{id}")]
    public IActionResult GetLanguage(int id)
    {
        var language = _context.Languages.Find(id);
        if (language == null || !language.IsEnabled) return NotFound();

        var response = new LanguageResponse
        {
            LanguageID = language.LanguageID,
            LanguageName = language.LanguageName,
            CreationDate = language.CreationDate,
            ModificationDate = language.ModificationDate,
            IsEnabled = language.IsEnabled
        };

        return Ok(response);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateLanguage(int id, [FromBody] LanguageRequest request)
    {
        var language = _context.Languages.Find(id);
        if (language == null || !language.IsEnabled) return NotFound();

        language.LanguageName = request.LanguageName;
        language.ModificationDate = DateTime.Now;

        _context.SaveChanges();

        var response = new LanguageResponse
        {
            LanguageID = language.LanguageID,
            LanguageName = language.LanguageName,
            CreationDate = language.CreationDate,
            ModificationDate = language.ModificationDate,
            IsEnabled = language.IsEnabled
        };

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteLanguage(int id)
    {
        var language = _context.Languages.Find(id);
        if (language == null || !language.IsEnabled) return NotFound();

        language.IsEnabled = false;
        _context.SaveChanges();

        return NoContent();
    }

    [HttpGet]
    public IActionResult GetAllLanguages()
    {
        var languages = _context.Languages
            .Where(l => l.IsEnabled)
            .Select(l => new LanguageResponse
            {
                LanguageID = l.LanguageID,
                LanguageName = l.LanguageName,
                CreationDate = l.CreationDate,
                ModificationDate = l.ModificationDate,
                IsEnabled = l.IsEnabled
            }).ToList();

        return Ok(languages);
    }
}
