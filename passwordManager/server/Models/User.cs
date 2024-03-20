namespace server.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string EncryptedMasterPassword { get; set; }
        public string Salt { get; set; }
        public string derivedKeySalt { get; set; }
    }
}