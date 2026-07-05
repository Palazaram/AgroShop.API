namespace AgroShop.Application.Dto.AuthDto
{
    public class RegisterUserDto
    {
        public required string LastName { get; set; }
        public required string FirstName { get; set; }
        public string? Patronymic { get; set; }
        public required string Email { get; set; }
        public required string Phone { get; set; }
        public required string Password { get; set; } 
    }
}
