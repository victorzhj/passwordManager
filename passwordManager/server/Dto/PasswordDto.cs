namespace server.Dto
{
    public class PasswordDto
    {
        public int UserId { get; set; }
        public string password { get; set; }
        public string salt { get; set; }
    }
}
