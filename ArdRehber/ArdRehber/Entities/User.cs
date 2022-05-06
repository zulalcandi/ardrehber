﻿namespace ArdRehber.Entities
{
    public class User
    {
       
            public int Id { get; set; }
            public string Name { get; set; }
            public string Surname { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
            public string RefreshToken { get; set; }=String.Empty;
            public DateTime? RefreshTokenEndDate { get; set; }
            public string UserType { get; set; }

    }
}
