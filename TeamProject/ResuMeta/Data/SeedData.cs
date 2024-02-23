using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResuMeta.Data
{
    public class SeedUserInfo
    {
        public string? Email { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PhoneNumber { get; set; }
        public bool EmailConfirmed { get; set; } = true;
    }
    public class SeedData
    {
        public static readonly SeedUserInfo[] UserSeedData = new SeedUserInfo[]
        {
            new SeedUserInfo { Email = "reynoldsa@mail.com", FirstName = "Adrian", LastName = "Reynolds", PhoneNumber = "555-628-1234", EmailConfirmed = true},
            new SeedUserInfo { Email = "mitchelle@mail.com", FirstName = "Emily", LastName = "Mitchell", PhoneNumber = "555-224-5576", EmailConfirmed = true},
            new SeedUserInfo { Email = "patelj@mail.com", FirstName = "Jasmine", LastName = "Patel", PhoneNumber = "555-794-4847", EmailConfirmed = true}
        };
    }
}