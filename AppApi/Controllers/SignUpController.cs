using Microsoft.AspNetCore.Mvc;
using AppApi.Data.Models;
using AppApi.Data;


namespace AppApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SignUpController : Controller
    {
        private readonly ILogger<SignUpController> _logger;
        private readonly ApiDbContext _context;

        public SignUpController(ApiDbContext context, ILogger<SignUpController> logger)
        {
            _context = context;
            _logger = logger;
        }

       
        [HttpPost]
        public IActionResult Post([FromForm]string name, [FromForm] string email, [FromForm] string password)
        {
            // create a new user
            var user = new User
            {
                Name = name,
                Email = email,
                Password = password
            };
            
            _context.Users.Add(user);

            _context.SaveChanges();

            return Ok();
        }
    }
}
