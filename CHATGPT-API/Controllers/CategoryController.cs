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
public class CategoriesController : ControllerBase
{
    private readonly ContextFactura _context;

    public CategoriesController(ContextFactura context)
    {
        _context = context;
    }

    // POST: /Category
    [HttpPost]
    public IActionResult CreateCategory([FromBody] CategoryRequestPut request)
    {
        var category = new Category
        {
            NameEn = request.NameEn,
            NameEs = request.NameEs,
            Prompt = request.Prompt, 
            CreationDate = DateTime.Now,
            ModificationDate = DateTime.Now,
            IsEnabled = true
        };

        _context.Categories.Add(category);
        _context.SaveChanges();

        var response = new CategoryResponse
        {
            CategoryID = category.CategoryID,
            NameEn = category.NameEn,
            NameEs = category.NameEs,
            Prompt = category.Prompt, 
            CreationDate = category.CreationDate,
            ModificationDate = category.ModificationDate,
            IsEnabled = category.IsEnabled
        };

        return CreatedAtAction(nameof(GetCategory), new { id = category.CategoryID }, response);
    }


    [HttpGet("{id}")]
    public IActionResult GetCategory(int id)
    {
        var category = _context.Categories
            .Include(c => c.Questions)
                .ThenInclude(q => q.Alternatives)
            .FirstOrDefault(c => c.CategoryID == id && c.IsEnabled);

        if (category == null) return NotFound();

        var response = new CategoryResponse
        {
            CategoryID = category.CategoryID,
            NameEn = category.NameEn,
            NameEs = category.NameEs,
            Prompt = category.Prompt, // Incluye el Prompt en la respuesta
            CreationDate = category.CreationDate,
            ModificationDate = category.ModificationDate,
            IsEnabled = category.IsEnabled,
            Questions = category.Questions.Select(q => new QuestionResponse
            {
                QuestionID = q.QuestionID,
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
            }).ToList()
        };

        return Ok(response);
    }



    [HttpPut("{id}")]
    public IActionResult UpdateCategory(int id, [FromBody] CategoryRequestPut request)
    {
        var category = _context.Categories.Find(id);
        if (category == null || !category.IsEnabled) return NotFound();

        category.NameEn = request.NameEn;
        category.NameEs = request.NameEs;
        category.Prompt = request.Prompt; // Actualiza el campo Prompt
        category.ModificationDate = DateTime.Now;

        _context.SaveChanges();

        var response = new CategoryResponse
        {
            CategoryID = category.CategoryID,
            NameEn = category.NameEn,
            NameEs = category.NameEs,
            Prompt = category.Prompt, // Incluye el Prompt en la respuesta
            CreationDate = category.CreationDate,
            ModificationDate = category.ModificationDate,
            IsEnabled = category.IsEnabled
        };

        return Ok(response);
    }



    [HttpDelete("{id}")]
    public IActionResult DeleteCategory(int id)
    {
        var category = _context.Categories.Find(id);
        if (category == null || !category.IsEnabled) return NotFound();

        category.IsEnabled = false;
        _context.SaveChanges();

        return NoContent();
    }




    [HttpGet]
    public IActionResult GetAllCategories()
    {
        var categories = _context.Categories
            .Where(c => c.IsEnabled)
            .Select(c => new CategoryResponse
            {
                CategoryID = c.CategoryID,
                NameEn = c.NameEn,
                NameEs = c.NameEs,
                Prompt = c.Prompt, // Incluye el Prompt en la respuesta
                CreationDate = c.CreationDate,
                ModificationDate = c.ModificationDate,
                IsEnabled = c.IsEnabled
            }).ToList();

        return Ok(categories);
    }




    [HttpGet("{categoryId}/Questions")]
    public IActionResult GetQuestionsByCategory(int categoryId)
    {
        var category = _context.Categories
            .Where(c => c.CategoryID == categoryId && c.IsEnabled)
            .Select(c => new CategorySimpleResponse
            {
                CategoryID = c.CategoryID,
                NameEn = c.NameEn,
                NameEs = c.NameEs,
                Prompt = c.Prompt, 
                Questions = _context.Questions
                    .Where(q => q.CategoryID == categoryId && q.IsEnabled)
                    .Select(q => new QuestionResponseSimplified
                    {
                        QuestionID = q.QuestionID,
                        QuestionTypeID = q.QuestionTypeID,
                        TextEn = q.TextEn,
                        TextEs = q.TextEs,
                        Alternatives = _context.Alternatives
                            .Where(a => a.QuestionID == q.QuestionID && a.IsEnabled)
                            .Select(a => new AlternativeResponseSimplified
                            {
                                AlternativeID = a.AlternativeID,
                                QuestionID = a.QuestionID,
                                TextEn = a.TextEn,
                                TextEs = a.TextEs,
                                TranslationChatGptEn = a.TranslationChatGptEn,
                                TranslationChatGptEs = a.TranslationChatGptEs
                            }).ToList()
                    }).ToList()
            }).FirstOrDefault();

        if (category == null)
        {
            return NotFound();
        }

        return Ok(category);
    }


}