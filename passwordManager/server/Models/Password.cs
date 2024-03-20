namespace server.Models
{
    public class Password
    {
        public int passwordId;
        public int userId;
        public string encryptedPassword;
        public string salt;
        public string site;
    }
}
