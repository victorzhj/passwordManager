namespace server.Dto
{
    public class UserLoginDto
    {
        public int? UserId { get; set; }
        public required string Username { get; set; }
        public string? MasterPasswordSalt { get; set; }
    }
}
