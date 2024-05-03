namespace server.Dto
{
    public class PasswordAddDto
    {
        public int UserId { get; set; }
        public required string EncryptedPassword { get; set; }
        public required string IV { get; set; }
        public string? Site { get; set; }
    }
}
