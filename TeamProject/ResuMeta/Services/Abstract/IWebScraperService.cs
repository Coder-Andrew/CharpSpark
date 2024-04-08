using System.Runtime.CompilerServices;
using ResuMeta.ViewModels;

namespace ResuMeta.Services.Abstract
{
    public interface IWebScraperService
    {
        Task<List<JobListingVM>> GetCachedListings(int pageNum);
        Task<List<JobListingVM>> SearchJobs(string jobTitle, string city, string state);
    }
}