using Microsoft.AspNetCore.Mvc;
using Model.Request;
using Model.Response;
using Domain;
using System.Linq;
using System;
using Infraestructure;

[ApiController]
[Route("[controller]")]
public class ProntsController : ControllerBase
{
    private readonly ContextFactura _context;

    public ProntsController(ContextFactura context)
    {
        _context = context;
    }

    [HttpPost]
    public IActionResult CreatePront([FromBody] ProntRequest request)
    {
        var pront = new Pront
        {
            UserID = request.UserID,
            CategoryID = request.CategoryID,
            DatePront = request.DatePront,
            CreationDate = DateTime.Now,
            ModificationDate = DateTime.Now,
            IsEnabled = true
        };

        _context.Pronts.Add(pront);
        _context.SaveChanges();

        var response = new ProntResponse
        {
            ProntID = pront.ProntID,
            UserID = pront.UserID,
            CategoryID = pront.CategoryID,
            DatePront = pront.DatePront,
            CreationDate = pront.CreationDate,
            ModificationDate = pront.ModificationDate,
            IsEnabled = pront.IsEnabled
        };

        return CreatedAtAction(nameof(GetPront), new { id = pront.ProntID }, response);
    }

    [HttpGet("{id}")]
    public IActionResult GetPront(int id)
    {
        var pront = _context.Pronts.Find(id);
        if (pront == null || !pront.IsEnabled) return NotFound();

        var response = new ProntResponse
        {
            ProntID = pront.ProntID,
            UserID = pront.UserID,
            CategoryID = pront.CategoryID,
            DatePront = pront.DatePront,
            CreationDate = pront.CreationDate,
            ModificationDate = pront.ModificationDate,
            IsEnabled = pront.IsEnabled
        };

        return Ok(response);
    }

    [HttpPut("{id}")]
    public IActionResult UpdatePront(int id, [FromBody] ProntRequest request)
    {
        var pront = _context.Pronts.Find(id);
        if (pront == null || !pront.IsEnabled) return NotFound();

        pront.UserID = request.UserID;
        pront.CategoryID = request.CategoryID;
        pront.DatePront = request.DatePront;
        pront.ModificationDate = DateTime.Now;

        _context.SaveChanges();

        var response = new ProntResponse
        {
            ProntID = pront.ProntID,
            UserID = pront.UserID,
            CategoryID = pront.CategoryID,
            DatePront = pront.DatePront,
            CreationDate = pront.CreationDate,
            ModificationDate = pront.ModificationDate,
            IsEnabled = pront.IsEnabled
        };

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public IActionResult DeletePront(int id)
    {
        var pront = _context.Pronts.Find(id);
        if (pront == null || !pront.IsEnabled) return NotFound();

        pront.IsEnabled = false;
        _context.SaveChanges();

        return NoContent();
    }

    [HttpGet]
    public IActionResult GetAllPronts()
    {
        var pronts = _context.Pronts
            .Where(p => p.IsEnabled)
            .Select(p => new ProntResponse
            {
                ProntID = p.ProntID,
                UserID = p.UserID,
                CategoryID = p.CategoryID,
                DatePront = p.DatePront,
                CreationDate = p.CreationDate,
                ModificationDate = p.ModificationDate,
                IsEnabled = p.IsEnabled
            }).ToList();

        return Ok(pronts);
    }
}
