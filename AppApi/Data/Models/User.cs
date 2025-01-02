using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace AppApi.Data.Models
{

    [Index(nameof(Id), nameof(Name), nameof(Email), IsUnique = true)]
    public class User
    {
        
        public int Id { get; set; }
        public required string Name { get; set; }
       
        public required string Email { get; set; }

        public required string Password { get; set; }

      
    }
}
