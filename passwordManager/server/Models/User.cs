using System.ComponentModel.DataAnnotations;

namespace server.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public required string Username { get; set; }
        public required string EncryptedMasterPassword { get; set; }
        public required string Salt { get; set; }
        public required string DerivedKeySalt { get; set; }
    }
}