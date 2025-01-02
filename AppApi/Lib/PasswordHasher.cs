using BC = BCrypt.Net.BCrypt;

namespace AppApi.Lib
{
    public class PasswordHasher
    {
        public string HashPassword(string password)
        {
            return BC.EnhancedHashPassword(password);
        }
        public bool VerifyPassword(string password, string hash)
        {
            return BC.EnhancedVerify(password, hash);
        }
    }
}
