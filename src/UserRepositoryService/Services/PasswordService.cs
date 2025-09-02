using Microsoft.AspNetCore.Identity;

namespace UserRepositoryService.Services
{
    public class PasswordService : IPasswordService
    {
        private readonly PasswordHasher<object> _hasher = new PasswordHasher<object>();

        public string HashPassword(string password)
        {
            return _hasher.HashPassword(null, password);
        }

        public bool Verify(string hashed, string providedPassword)
        {
            var r = _hasher.VerifyHashedPassword(null, hashed, providedPassword);
            return r == PasswordVerificationResult.Success || r == PasswordVerificationResult.SuccessRehashNeeded;
        }
    }
}