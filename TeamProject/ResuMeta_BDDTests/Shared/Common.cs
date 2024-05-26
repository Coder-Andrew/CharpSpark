using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResuMeta_BDDTests.Shared
{
    // Sitewide definitions and useful methods
    public class Common
    {
        public const string BaseUrl = "http://localhost:5160";     // copied from launchSettings.json


        // File to store browser cookies in
        public const string CookieFile = "../../../../StandupsCookies.txt";

        // Page names that everyone should use
        // A handy way to look these up
        public static Dictionary<string, string> Paths = new()
        {
            { "Home" , "/" },
            { "Register", "/Identity/Account/Register"},
            { "Login", "/Identity/Account/Login" },
            { "CreateResume", "/Resume/CreateResume" },
            { "ViewResume", "/Resume/ViewResume/" },
            { "YourResume", "/Resume/YourResume/" },
            { "YourDashboard", "/Resume/YourDashboard" },
            { "YourCoverLetter", "/CoverLetter/YourCoverLetter/" },
            { "CreateCoverLetter", "/CoverLetter/CreateCoverLetter" },
            { "ViewCoverLetter", "/CoverLetter/ViewCoverLetter/" },
            { "ApplicationTracker", "/ApplicationTracker"},
            { "JobListings", "/JobListing"},
            { "ImproveResume", "/Resume/ImproveResume/" },
            { "ImproveCoverLetter", "/CoverLetter/ImproveCoverLetter/" },
            { "CreateProfile", "/Profile/" },
            { "YourProfile", "/Profile/YourProfile/" },
            { "UserProfile", "/Profile/UserProfile/" }
        };

        public static string PathFor(string pathName, string id = "") => Paths[pathName] + id;
        public static string UrlFor(string pathName, string id = "") => BaseUrl + Paths[pathName] + id;

        public static void ResetPaths()
        {
            Paths = new()
            {
                { "Home" , "/" },
                { "Register", "/Identity/Account/Register"},
                { "Login", "/Identity/Account/Login" },
                { "CreateResume", "/Resume/CreateResume" },
                { "ViewResume", "/Resume/ViewResume/" },
                { "YourResume", "/Resume/YourResume/" },
                { "YourDashboard", "/Resume/YourDashboard" },
                { "YourCoverLetter", "/CoverLetter/YourCoverLetter/" },
                { "CreateCoverLetter", "/CoverLetter/CreateCoverLetter" },
                { "ViewCoverLetter", "/CoverLetter/ViewCoverLetter/" },
                { "ApplicationTracker", "/ApplicationTracker"},
                { "JobListings", "/JobListing"},
                { "ImproveResume", "/Resume/ImproveResume/"},
                { "ImproveCoverLetter", "/CoverLetter/ImproveCoverLetter/" },
                { "CreateProfile", "/Profile/" },
                { "YourProfile", "/Profile/YourProfile/" },
                { "UserProfile", "/Profile/UserProfile/" }
            };
        }
    }
}
