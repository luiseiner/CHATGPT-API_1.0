using Microsoft.AspNetCore.Mvc;
using Model.Request;
using Model.Response;
using Domain;
using System.Linq;
using System;
using Infraestructure;

[ApiController]
[Route("[controller]")]
public class QuestionTypesController : ControllerBase
{
    private readonly ContextFactura _context;

    public QuestionTypesController(ContextFactura context)
    {
        _context = context;
    }

    [HttpPost]
    public IActionResult CreateQuestionType([FromBody] QuestionTypeRequest request)
    {
        var questionType = new QuestionType
        {
            TypeEn = request.TypeEn,
            TypeEs = request.TypeEs,
            CreationDate = DateTime.Now,
            ModificationDate = DateTime.Now,
            IsEnabled = true
        };

        _context.QuestionTypes.Add(questionType);
        _context.SaveChanges();

        var response = new QuestionTypeResponse
        {
            QuestionTypeID = questionType.QuestionTypeID,
            TypeEn = questionType.TypeEn,
            TypeEs = questionType.TypeEs,
            CreationDate = questionType.CreationDate,
            ModificationDate = questionType.ModificationDate,
            IsEnabled = questionType.IsEnabled
        };

        return CreatedAtAction(nameof(GetQuestionType), new { id = questionType.QuestionTypeID }, response);
    }

    [HttpGet("{id}")]
    public IActionResult GetQuestionType(int id)
    {
        var questionType = _context.QuestionTypes.Find(id);
        if (questionType == null || !questionType.IsEnabled) return NotFound();

        var response = new QuestionTypeResponse
        {
            QuestionTypeID = questionType.QuestionTypeID,
            TypeEn = questionType.TypeEn,
            TypeEs = questionType.TypeEs,
            CreationDate = questionType.CreationDate,
            ModificationDate = questionType.ModificationDate,
            IsEnabled = questionType.IsEnabled
        };

        return Ok(response);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateQuestionType(int id, [FromBody] QuestionTypeRequest request)
    {
        var questionType = _context.QuestionTypes.Find(id);
        if (questionType == null || !questionType.IsEnabled) return NotFound();

        questionType.TypeEn = request.TypeEn;
        questionType.TypeEs = request.TypeEs;
        questionType.ModificationDate = DateTime.Now;

        _context.SaveChanges();

        var response = new QuestionTypeResponse
        {
            QuestionTypeID = questionType.QuestionTypeID,
            TypeEn = questionType.TypeEn,
            TypeEs = questionType.TypeEs,
            CreationDate = questionType.CreationDate,
            ModificationDate = questionType.ModificationDate,
            IsEnabled = questionType.IsEnabled
        };

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteQuestionType(int id)
    {
        var questionType = _context.QuestionTypes.Find(id);
        if (questionType == null || !questionType.IsEnabled) return NotFound();

        questionType.IsEnabled = false;
        _context.SaveChanges();

        return NoContent();
    }

    [HttpGet]
    public IActionResult GetAllQuestionTypes()
    {
        var questionTypes = _context.QuestionTypes
            .Where(qt => qt.IsEnabled)
            .Select(qt => new QuestionTypeResponse
            {
                QuestionTypeID = qt.QuestionTypeID,
                TypeEn = qt.TypeEn,
                TypeEs = qt.TypeEs,
                CreationDate = qt.CreationDate,
                ModificationDate = qt.ModificationDate,
                IsEnabled = qt.IsEnabled
            }).ToList();

        return Ok(questionTypes);
    }
}
