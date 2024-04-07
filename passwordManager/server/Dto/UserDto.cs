namespace server.Dto
{
    public class UserDto
    {
        public int UserId { get; set; }
        public required string Username { get; set; }
        public string? MasterPasswordHashed { get; set; }
    }
}
