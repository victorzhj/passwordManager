namespace server.Dto.UserDtos
{
    public class TokenDto
    {
        public required string DerivedKeySalt { get; set; }
        public string TokenType { get; set; } = "Bearer";
        public required string AccessToken { get; set; }
        public required int ExpiresIn { get; set;}
        //public string RefreshToken { get; set; }
        
    }
}
