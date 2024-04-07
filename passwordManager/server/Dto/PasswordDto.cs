namespace server.Dto
{
    public class PasswordDto
    {
        public int PasswordId {  get; set; }
        public int UserId { get; set; }
        public required string EncryptedPassword { get; set; }
        public required string Salt { get; set; }
        public required string Site { get; set; }
    }
}
