using System.Runtime.CompilerServices;
using ResuMeta.ViewModels;

namespace ResuMeta.Services.Abstract
{
    public interface IWebScraperService
    {
        Task<JobListingContainerVM> GetCachedListings(int pageNum, string jobTitle);
        Task<List<JobListingVM>> SearchJobs(string jobTitle, string city, string state);
        Task<string> GetJobDescription(string url);
    }
}