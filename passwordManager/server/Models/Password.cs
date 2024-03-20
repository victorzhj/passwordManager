namespace server.Models
{
    public class Password
    {
        public int passwordId;
        public required int userId;
        public required string encryptedPassword;
        public required string salt;
        public string? site;
    }
}
