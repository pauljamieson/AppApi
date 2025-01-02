using Microsoft.AspNetCore.Mvc;
using AppApi.Data.Models;
using AppApi.Data;
using System.Text.Json;
using AppApi.Lib;
using Microsoft.AspNetCore.Authorization;


namespace AppApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(Policy = "Users")]
    public class SignUpController(ILogger<SignUpController> logger, ApiDbContext db, PasswordHasher passwordHasher) : Controller
    {
        private readonly ILogger<SignUpController> _logger = logger;
        private readonly ApiDbContext _db = db;
        private readonly PasswordHasher _passwordHasher = passwordHasher;

        [HttpPost]
        public async Task<IActionResult> Post([FromForm] string name, [FromForm] string email, [FromForm] string password)
        {
            try
            {
                // Password must be at least 8 characters long
                if (password.Length < 8)
                {
                    return BadRequest(JsonSerializer.Serialize(new { status = "failure", message = "Password must be at least 8 characters long" }));
                }

                // Hash the password
                var passwordHash = _passwordHasher.HashPassword(password);
                // create db new user
                var user = new User
                {
                    Name = name,
                    Email = email,
                    Password = passwordHash
                };

                // save the user            
                var result = await _db.Users.AddAsync(user);

                //var result = await _db.CreateUser(user);

                if (result == null)
                {
                    return BadRequest(JsonSerializer.Serialize(new { status = "failure", message = "User already exists" }));
                }
                await _db.SaveChangesAsync();

                return Ok(JsonSerializer.Serialize(new { status = "success" }));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
