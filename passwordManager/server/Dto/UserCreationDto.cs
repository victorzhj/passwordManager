namespace server.Dto
{
    public class UserCreationDto
    {
        public required string Username { get; set; }
        public required string MasterPasswordHashed { get; set; }
        public required string Salt { get; set; }
        public required string derivedKeySalt { get; set; }

    }
}
