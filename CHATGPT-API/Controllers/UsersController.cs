using Microsoft.AspNetCore.Mvc;
using Model.Request;
using Model.Response;
using Domain;
using System.Linq;
using System;
using Infraestructure;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims;

namespace Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly ContextFactura _context;
        private readonly string jwtSecret = "YoussrJsvsvWsvdsvdsTSdfdsvssecsfrvsvsdfevdvstKeysoytupapi"; // Cambia esto a una clave segura

        public UsersController(ContextFactura context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult CreateUser([FromBody] UserRequest request)
        {
            // Verificar si el nombre de usuario ya existe
            if (_context.Users.Any(u => u.Username == request.Username))
            {
                var suggestions = GenerateUserNameSuggestions(request.Username);
                return Conflict(new { message = "User name already exists", suggestions });
            }

            // Verificar si el correo ya existe
            if (_context.Users.Any(u => u.Email == request.Email))
            {
                return Conflict(new { message = "Email already exists" });
            }

            // Verificar si el LanguageID proporcionado existe en la tabla Languages
            if (!_context.Languages.Any(l => l.LanguageID == request.LanguageID))
            {
                return BadRequest(new { message = "Invalid LanguageID" });
            }

            var user = new User
            {
                LanguageID = request.LanguageID,
                Username = request.Username,
                Email = request.Email,
                Password = request.Password,
                CreationDate = DateTime.Now,
                ModificationDate = DateTime.Now,
                IsEnabled = true
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            var response = new UserResponse_user
            {
                UserID = user.UserID,
                LanguageID = user.LanguageID,
                Username = user.Username,
                Email = user.Email,
                CreationDate = user.CreationDate,
                ModificationDate = user.ModificationDate,
                IsEnabled = user.IsEnabled
            };

            return CreatedAtAction(nameof(GetUser), new { id = user.UserID }, response);
        }


        //[HttpPost("Register")]
        //public async Task<IActionResult> Register([FromBody] UserRequest userRequest)
        //{
        //    // Verificar si el nombre de usuario ya existe
        //    if (_context.Users.Any(u => u.Username == userRequest.Username))
        //    {
        //        var suggestions = GenerateUserNameSuggestions(userRequest.Username);
        //        return Conflict(new { message = "User name already exists", suggestions });
        //    }

        //    // Verificar si el correo ya existe
        //    if (_context.Users.Any(u => u.Email == userRequest.Email))
        //    {
        //        return Conflict(new { message = "Email already exists" });
        //    }

        //    var user = new User
        //    {
        //        LanguageID = userRequest.LanguageID,
        //        Username = userRequest.Username,
        //        Email = userRequest.Email,
        //        Password = userRequest.Password, // Asegúrate de encriptar la contraseña en una implementación real
        //        Role = userRequest.Role
        //    };

        //    _context.Users.Add(user);
        //    await _context.SaveChangesAsync();

        //    return Ok(new { message = "User registered successfully" });
        //}

        //private List<string> GenerateUserNameSuggestions(string userName)
        //{
        //    List<string> suggestions = new List<string>();
        //    int suggestionCount = 0;

        //    while (suggestions.Count < 5)
        //    {
        //        string suggestedName = $"{userName}{suggestionCount}";
        //        if (!_context.Users.Any(u => u.Username == suggestedName))
        //        {
        //            suggestions.Add(suggestedName);
        //        }
        //        suggestionCount++;
        //    }

        //    return suggestions;
        //}

        [HttpPost("Login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            var user = _context.Users.SingleOrDefault(u => u.Username == request.Username && u.Password == request.Password);
            if (user == null)
            {
                return Unauthorized();
            }

            var token = GenerateJwtToken(user);
            return Ok(new { token });
        }

        [HttpGet("{id}")]
        public IActionResult GetUser(int id)
        {
            var user = _context.Users.Find(id);
            if (user == null || !user.IsEnabled) return NotFound();

            var response = new UserResponse_user
            {
                UserID = user.UserID,
                LanguageID = user.LanguageID,
                Username = user.Username,
                Email = user.Email,
                CreationDate = user.CreationDate,
                ModificationDate = user.ModificationDate,
                IsEnabled = user.IsEnabled
            };

            return Ok(response);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, [FromBody] UserRequest request)
        {
            var user = _context.Users.Find(id);
            if (user == null || !user.IsEnabled) return NotFound();

            user.LanguageID = request.LanguageID;
            user.Username = request.Username;
            user.Email = request.Email;
            user.Password = request.Password;
            user.ModificationDate = DateTime.Now;

            _context.SaveChanges();

            var response = new UserResponse_user
            {
                UserID = user.UserID,
                LanguageID = user.LanguageID,
                Username = user.Username,
                Email = user.Email,
                CreationDate = user.CreationDate,
                ModificationDate = user.ModificationDate,
                IsEnabled = user.IsEnabled
            };

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            var user = _context.Users.Find(id);
            if (user == null || !user.IsEnabled) return NotFound();

            user.IsEnabled = false;
            _context.SaveChanges();

            return NoContent();
        }

        [HttpGet]
        public IActionResult GetAllUsers()
        {
            var users = _context.Users
                .Where(u => u.IsEnabled)
                .Select(u => new UserResponse_user
                {
                    UserID = u.UserID,
                    LanguageID = u.LanguageID,
                    Username = u.Username,
                    Email = u.Email,
                    CreationDate = u.CreationDate,
                    ModificationDate = u.ModificationDate,
                    IsEnabled = u.IsEnabled
                }).ToList();

            return Ok(users);
        }

        private string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(jwtSecret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.UserID.ToString()),
                    new Claim(ClaimTypes.Email, user.Email),
                }),
                //Expires = DateTime.UtcNow.AddDays(7), // Token expira en 7 días
                Expires = DateTime.UtcNow.AddMinutes(5), // Token expira en 5 minutos 
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        private List<string> GenerateUserNameSuggestions(string existingUserName)
        {
            var suggestions = new List<string>();
            var random = new Random();

            while (suggestions.Count < 5)
            {
                string suggestion = existingUserName + random.Next(1000, 9999);
                if (!_context.Users.Any(u => u.Username == suggestion) && !suggestions.Contains(suggestion))
                {
                    suggestions.Add(suggestion);
                }
            }

            return suggestions;
        }

    }
}
