using ResuMeta.Services.Abstract;
using ResuMeta.ViewModels;
using System.Text.Json;

namespace ResuMeta.Services.Concrete
{
    public class FakeWebScraper : IWebScraperService
    {
        public Task<JobListingContainerVM> GetCachedListings(int pageNum, string jobTitle)
        {
            var fakeResult = new JobListingContainerVM
            {
                JobListings = new List<JobListingVM>
                {
                    new JobListingVM
                    {
                        Company = "Not a real company - Reach out for actual info.",
                        JobTitle = "Fake job title",
                        Link = "Not Real",
                        Location = "Oregon"
                    }
                },
                NumberOfPages = 1
            };

            return Task.FromResult(fakeResult);
        }

        public Task<JobDescriptionVM> GetJobDescription(JsonElement url)
        {
            var fakeResult = new JobDescriptionVM
            {
                JobDescription = "Not a valid job description, please reach out if you want to see the actual web scraper."
            };

            return Task.FromResult(fakeResult);
        }

        public Task<List<JobListingVM>> SearchJobs(string jobTitle, string city, string state)
        {
            var fakeResult = new List<JobListingVM>
            {
                new JobListingVM
                {
                    JobTitle = "Not a real company - Reach out for actual info.",
                    Company = "Not a real company",
                    Link = "Not Real",
                    Location = "Oregon"
                }
            };

            return Task.FromResult(fakeResult);
        }
    }
}
