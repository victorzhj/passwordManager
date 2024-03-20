namespace server.Dto
{
    public class PasswordDto
    {
        public int passwordId {  get; set; }
        public int UserId { get; set; }
        public string password { get; set; }
        public string salt { get; set; }
    }
}
