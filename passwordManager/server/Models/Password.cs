namespace server.Models
{
    public class Password
    {
        int userId;
        string? encryptedPassword;
        string? salt;
        string? site;
    }
}
