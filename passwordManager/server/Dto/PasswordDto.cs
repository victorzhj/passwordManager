namespace server.Dto
{
    public class PasswordDto
    {
        public int passwordId {  get; set; }
        public int UserId { get; set; }
        public required string password { get; set; }
        public required string salt { get; set; }
        public required string site { get; set; }
    }
}
