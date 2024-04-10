﻿namespace server.Dto.UserDtos
{
    public class UserCreationDto
    {
        public required string Username { get; set; }
        public required string MasterPasswordHashed { get; set; }
        public required string Salt { get; set; }
        public required string DerivedKeySalt { get; set; }

    }
}
