using Microsoft.AspNetCore.Mvc;
using Model.Request;
using Model.Response;
using Domain;
using System.Linq;
using System;
using Infraestructure;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("[controller]")]
public class QuestionsController : ControllerBase
{
    private readonly ContextFactura _context;

    public QuestionsController(ContextFactura context)
    {
        _context = context;
    }

    [HttpPost]
    public IActionResult CreateQuestion([FromBody] QuestionRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var question = new Question
        {
            CategoryID = request.CategoryID,
            QuestionTypeID = request.QuestionTypeID,
            TextEn = request.TextEn,
            TextEs = request.TextEs,
            CreationDate = DateTime.Now,
            ModificationDate = DateTime.Now,
           
            IsEnabled = true
        };

        _context.Questions.Add(question);
        _context.SaveChanges();

        var response = new QuestionResponse
        {
            QuestionID = question.QuestionID,
            CategoryID = question.CategoryID,
            QuestionTypeID = question.QuestionTypeID,
            TextEn = question.TextEn,
            TextEs = question.TextEs,
            
            CreationDate = question.CreationDate,
            ModificationDate = question.ModificationDate,
            IsEnabled = question.IsEnabled,
            Alternatives = question.Alternatives?.Select(a => new AlternativeResponse
            {
                AlternativeID = a.AlternativeID,
                TextEn = a.TextEn,
                TextEs = a.TextEs
            }).ToList() ?? new List<AlternativeResponse>()
        };

        return CreatedAtAction(nameof(GetQuestion), new { id = question.QuestionID }, response);
    }

    [HttpGet("{id}")]
    public IActionResult GetQuestion(int id)
    {
        var question = _context.Questions
            .Include(q => q.Alternatives)
            .FirstOrDefault(q => q.QuestionID == id && q.IsEnabled);

        if (question == null) return NotFound();

        var response = new QuestionResponse
        {
            QuestionID = question.QuestionID,
            CategoryID = question.CategoryID,
            QuestionTypeID = question.QuestionTypeID,
            TextEn = question.TextEn,
            TextEs = question.TextEs,
            CreationDate = question.CreationDate,
            ModificationDate = question.ModificationDate,
            
            IsEnabled = question.IsEnabled,

            Alternatives = question.Alternatives.Select(a => new AlternativeResponse
            {
                AlternativeID = a.AlternativeID,
                TextEn = a.TextEn,
                TextEs = a.TextEs
            }).ToList()
        };

        return Ok(response);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateQuestion(int id, [FromBody] QuestionRequest request)
    {
        var question = _context.Questions.Find(id);
        if (question == null || !question.IsEnabled) return NotFound();

        question.CategoryID = request.CategoryID;
        question.QuestionTypeID = request.QuestionTypeID;
        question.TextEn = request.TextEn ?? question.TextEn;
        question.TextEs = request.TextEs ?? question.TextEs;
        question.ModificationDate = DateTime.Now;

        _context.SaveChanges();

        var response = new QuestionResponse
        {
            QuestionID = question.QuestionID,
            CategoryID = question.CategoryID,
            QuestionTypeID = question.QuestionTypeID,
            TextEn = question.TextEn,
            TextEs = question.TextEs,
            CreationDate = question.CreationDate,
            ModificationDate = question.ModificationDate,
            
            IsEnabled = question.IsEnabled
        };

        return Ok(response);
    }



    [HttpDelete("{id}")]
    public IActionResult DeleteQuestion(int id)
    {
        var question = _context.Questions.Find(id);
        if (question == null || !question.IsEnabled) return NotFound();

        question.IsEnabled = false;
        _context.SaveChanges();

        return NoContent();
    }

    [HttpGet]
    public IActionResult GetAllQuestions()
    {
        var questions = _context.Questions
            .Where(q => q.IsEnabled)
            .Select(q => new QuestionResponse
            {
                QuestionID = q.QuestionID,
                CategoryID = q.CategoryID,
                QuestionTypeID = q.QuestionTypeID,
                TextEn = q.TextEn,
                TextEs = q.TextEs,
                CreationDate = q.CreationDate,
                ModificationDate = q.ModificationDate,
                
                IsEnabled = q.IsEnabled,
                Alternatives = q.Alternatives.Select(a => new AlternativeResponse
                {
                    AlternativeID = a.AlternativeID,
                    TextEn = a.TextEn,
                    TextEs = a.TextEs
                }).ToList()
            }).ToList();

        return Ok(questions);
    }
}