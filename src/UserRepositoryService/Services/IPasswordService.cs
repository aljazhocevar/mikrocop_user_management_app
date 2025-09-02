namespace UserRepositoryService.Services
{
    public interface IPasswordService
    {
        string HashPassword(string password);
        bool Verify(string hashed, string providedPassword);
    }
}
