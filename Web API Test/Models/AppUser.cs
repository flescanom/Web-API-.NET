﻿using Microsoft.AspNetCore.Identity;

namespace Web_API_Test.Models
{
    public class AppUser : IdentityUser
    {
        public List<Portfolio> Portfolios { get; set; } = new List<Portfolio>();
    }
}
