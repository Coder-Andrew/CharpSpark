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
            new SeedUserInfo { Email = "john.doe@example.com", FirstName = "John", LastName = "Doe", PhoneNumber = "123-456-7890", EmailConfirmed = true },
            new SeedUserInfo { Email = "jane.smith@example.com", FirstName = "Jane", LastName = "Smith", PhoneNumber = "234-567-8901", EmailConfirmed = true },
            new SeedUserInfo { Email = "jim.brown@example.com", FirstName = "Jim", LastName = "Brown", PhoneNumber = "345-678-9012", EmailConfirmed = true },
            new SeedUserInfo { Email = "jack.white@example.com", FirstName = "Jack", LastName = "White", PhoneNumber = "456-789-0123", EmailConfirmed = true },
            new SeedUserInfo { Email = "jill.green@example.com", FirstName = "Jill", LastName = "Green", PhoneNumber = "567-890-1234", EmailConfirmed = true },
            new SeedUserInfo { Email = "joe.black@example.com", FirstName = "Joe", LastName = "Black", PhoneNumber = "678-901-2345", EmailConfirmed = true },
            new SeedUserInfo { Email = "jenny.blue@example.com", FirstName = "Jenny", LastName = "Blue", PhoneNumber = "789-012-3456", EmailConfirmed = true },
            new SeedUserInfo { Email = "jason.gray@example.com", FirstName = "Jason", LastName = "Gray", PhoneNumber = "890-123-4567", EmailConfirmed = true },
            new SeedUserInfo { Email = "julia.yellow@example.com", FirstName = "Julia", LastName = "Yellow", PhoneNumber = "901-234-5678", EmailConfirmed = true },
            new SeedUserInfo { Email = "jerry.purple@example.com", FirstName = "Jerry", LastName = "Purple", PhoneNumber = "012-345-6789", EmailConfirmed = true },
            new SeedUserInfo { Email = "jordan.orange@example.com", FirstName = "Jordan", LastName = "Orange", PhoneNumber = "123-450-9876", EmailConfirmed = true },
            new SeedUserInfo { Email = "jamie.pink@example.com", FirstName = "Jamie", LastName = "Pink", PhoneNumber = "234-560-9875", EmailConfirmed = true },
            new SeedUserInfo { Email = "jude.red@example.com", FirstName = "Jude", LastName = "Red", PhoneNumber = "345-670-9874", EmailConfirmed = true },
            new SeedUserInfo { Email = "jess.silver@example.com", FirstName = "Jess", LastName = "Silver", PhoneNumber = "456-780-9873", EmailConfirmed = true },
            new SeedUserInfo { Email = "joan.gold@example.com", FirstName = "Joan", LastName = "Gold", PhoneNumber = "567-890-9872", EmailConfirmed = true },

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