using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace ResuMeta.Data
{
    public class ApplicationUser : IdentityUser
    {
        public string? SecurityQuestion1 { get; set; }
        public string? SecurityAnswer1 { get; set; }
        public string? SecurityQuestion2 { get; set; }
        public string? SecurityAnswer2 { get; set; }
        public string? SecurityQuestion3 { get; set; }
        public string? SecurityAnswer3 { get; set; }
    }
}