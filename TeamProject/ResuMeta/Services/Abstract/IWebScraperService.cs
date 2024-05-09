using System.Runtime.CompilerServices;
using System.Text.Json;
using ResuMeta.ViewModels;

namespace ResuMeta.Services.Abstract
{
    public interface IWebScraperService
    {
        Task<JobListingContainerVM> GetCachedListings(int pageNum, string jobTitle);
        Task<List<JobListingVM>> SearchJobs(string jobTitle, string city, string state);
        Task<JobDescriptionVM> GetJobDescription(JsonElement url);
    }
}