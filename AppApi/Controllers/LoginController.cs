using AppApi.Data;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using AppApi.Lib;
using AppApi.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace AppApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    
    public class LoginController(ILogger<LoginController> logger, ApiDbContext db, TokenProvider tokenProvider, PasswordHasher passwordHasher) : Controller
    {
        private readonly ILogger<LoginController> _logger = logger;
        private readonly ApiDbContext _db = db;
        private readonly TokenProvider _tokenProvider = tokenProvider;
        private readonly PasswordHasher _passwordHasher = passwordHasher;

        [HttpPost]
 
        public async Task<IActionResult> Post([FromForm] string email, [FromForm] string password)        
        {

            // Find user with login email address
            var user = await _db.Users.Where(user => user.Email == email).FirstAsync<User>();
            //var user = await _db.GetUserByEmail(email); 

            // Generic login failure message
            var failed = new { status = "failure", reason = "Login failed" };

            // Email is not registered
            if (user == null) return BadRequest(failed);
            
            // Check password against hash
            var goodPassword = _passwordHasher.VerifyPassword(password, user.Password.ToString());

            // Password not matching
            if (!goodPassword) return BadRequest(failed);

            // Login Success
            var token = _tokenProvider.Create(user);
            return Ok(JsonSerializer.Serialize(new { status = "success", token }));
        }
    }
}
