namespace server.Dto.UserDtos
{
    public class UserLoginDto
    {
        public required string Username { get; set; }
        public string? MasterPasswordHashed { get; set; }
    }
}
