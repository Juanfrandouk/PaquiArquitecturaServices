namespace Paquigroup.Ecommerce.Application.DTO
{
    public class UserDto
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        public string Token { get; set; } = string.Empty;


    }


}
