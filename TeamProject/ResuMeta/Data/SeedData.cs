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
        // public string? SecurityQuestion1 { get; set; }
        // public string? SecurityAnswer1 { get; set; }
        // public string? SecurityQuestion2 { get; set; }
        // public string? SecurityAnswer2 { get; set; }
        // public string? SecurityQuestion3 { get; set; }
        // public string? SecurityAnswer3 { get; set; }
    }
    public class SeedData
    {
        public static readonly SeedUserInfo[] UserSeedData = new SeedUserInfo[]
        {
            new SeedUserInfo { Email = "reynoldsa@mail.com", FirstName = "Adrian", LastName = "Reynolds", PhoneNumber = "555-628-1234", EmailConfirmed = true},
            new SeedUserInfo { Email = "mitchelle@mail.com", FirstName = "Emily", LastName = "Mitchell", PhoneNumber = "555-224-5576", EmailConfirmed = true},
            new SeedUserInfo { Email = "patelj@mail.com", FirstName = "Jasmine", LastName = "Patel", PhoneNumber = "555-794-4847", EmailConfirmed = true},
            // new SeedUserInfo { Email = "reynoldsa@mail.com", FirstName = "Adrian", LastName = "Reynolds", PhoneNumber = "555-628-1234", EmailConfirmed = true, SecurityQuestion1 = "What is your mother's maiden name?",
            // SecurityAnswer1 = "Smith", SecurityQuestion2 = "What was the make and model of your first car?", SecurityAnswer2 = "Honda Accord", SecurityQuestion3 = "What is the name of your favorite pet?", SecurityAnswer3 = "Buddy"},
            // new SeedUserInfo { Email = "mitchelle@mail.com", FirstName = "Emily", LastName = "Mitchell", PhoneNumber = "555-224-5576", EmailConfirmed = true, SecurityQuestion1 = "What is your mother's maiden name?",
            // SecurityAnswer1 = "Johnson", SecurityQuestion2 = "What is your favorite book?", SecurityAnswer2 = "The Hunger Games", SecurityQuestion3 = "What is the name of your favorite teacher?", SecurityAnswer3 = "Mrs Smith"},
            // new SeedUserInfo { Email = "patelj@mail.com", FirstName = "Jasmine", LastName = "Patel", PhoneNumber = "555-794-4847", EmailConfirmed = true, SecurityQuestion1 = "What was your favorite food as a child?",
            // SecurityAnswer1 = "Pizza", SecurityQuestion2 = "What is your favorite book?", SecurityAnswer2 = "Harry Potter", SecurityQuestion3 = "What is the name of your favorite pet?", SecurityAnswer3 = "Whiskers"},
        };
    }
}