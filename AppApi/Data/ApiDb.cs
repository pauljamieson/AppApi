using AppApi.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace AppApi.Data
{
    public class ApiDb(ApiDbContext apiDbContext)
    {
        private readonly ApiDbContext _db = apiDbContext;

        public async Task<Boolean> CreateUser(User user)
        {
            var result = await _db.Users.AddAsync(user);
            await _db.SaveChangesAsync();
            return result != null;
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await _db.Users.Where(user => user.Email == email).FirstAsync<User>();
        }

    }
}
