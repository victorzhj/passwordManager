namespace server.Dto.PasswordDtos
{
    public class PasswordDetailsDto
    {
        public int PasswordId { get; set; }
        public int UserId { get; set; }
        public required string EncryptedPassword { get; set; }
        public required string Salt { get; set; }
        public string? Site { get; set; }
    }
}
