namespace UserRepositoryService.DTOs
{
    public class CreateUserRequest
    {
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Language { get; set; }
        public string Culture { get; set; }
        public string Password { get; set; }
    }
}
