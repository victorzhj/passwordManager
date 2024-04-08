namespace server.Dto
{
    public class PasswordAddDto
    {
        public int UserId { get; set; }
        public required string EncryptedPassword { get; set; }
        public required string Salt { get; set; }
        public string? Site { get; set; }
        public bool IsMasterPassword { get; set; } = false;
    }
}
