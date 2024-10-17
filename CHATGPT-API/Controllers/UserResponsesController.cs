using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using Domain;
using Infraestructure;
using Model.Request;
using Model.Response;

[ApiController]
[Route("[controller]")]
public class UserResponseController : ControllerBase
{
    private readonly ContextFactura _context;

    public UserResponseController(ContextFactura context)
    {
        _context = context;
    }

    [HttpPost]
    public IActionResult CreateUserResponse([FromBody] UserResponseRequestPost request)
    {
        var userResponse = new UserResponse
        {
            UserID = request.UserID,
            QuestionID = request.QuestionID,
            ResponseEn = request.ResponseEn,
            ResponseEs = request.ResponseEs,
            CreationDate = DateTime.Now,
            ModificationDate = DateTime.Now,
            IsEnabled = true
        };

        _context.UserResponses.Add(userResponse);
        _context.SaveChanges();

        var response = new UserResponseResponsePost
        {
            UserResponseID = userResponse.UserResponseID,
            QuestionID = userResponse.QuestionID,
            UserID = userResponse.UserID,
            ResponseEn = userResponse.ResponseEn,
            ResponseEs = userResponse.ResponseEs
        };

        return CreatedAtAction(nameof(GetUserResponse), new { id = userResponse.UserResponseID }, response);
    }

    [HttpGet("{id}")]
    public IActionResult GetUserResponse(int id)
    {
        var userResponse = _context.UserResponses.Find(id);
        if (userResponse == null || !userResponse.IsEnabled) return NotFound();

        var response = new UserResponseResponsePost
        {
            UserResponseID = userResponse.UserResponseID,
            QuestionID = userResponse.QuestionID,
            UserID = userResponse.UserID,
            ResponseEn = userResponse.ResponseEn,
            ResponseEs = userResponse.ResponseEs
        };

        return Ok(response);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateUserResponse(int id, [FromBody] UserResponseRequestPost request)
    {
        var userResponse = _context.UserResponses.Find(id);
        if (userResponse == null || !userResponse.IsEnabled) return NotFound();

        userResponse.QuestionID = request.QuestionID;
        userResponse.UserID = request.UserID;
        userResponse.ResponseEn = request.ResponseEn;
        userResponse.ResponseEs = request.ResponseEs;
        userResponse.ModificationDate = DateTime.Now;

        _context.SaveChanges();

        var response = new UserResponseResponsePost
        {
            UserResponseID = userResponse.UserResponseID,
            QuestionID = userResponse.QuestionID,
            UserID = userResponse.UserID,
            ResponseEn = userResponse.ResponseEn,
            ResponseEs = userResponse.ResponseEs
        };

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteUserResponse(int id)
    {
        var userResponse = _context.UserResponses.Find(id);
        if (userResponse == null || !userResponse.IsEnabled) return NotFound();

        userResponse.IsEnabled = false;
        _context.SaveChanges();

        return NoContent();
    }
}
