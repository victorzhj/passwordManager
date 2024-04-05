using System.ComponentModel.DataAnnotations;

namespace server.Models
{
    public class Password
    {
        [Key]
        public int PasswordId { get; set; }
        public required int UserId { get; set; }
        public required string EncryptedPassword { get; set; }
        public required string Salt { get; set; }
        public string? Site { get; set; }
    }
}
