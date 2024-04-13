using System.Text.Json;
using ResuMeta.Services.Abstract;
using ResuMeta.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using ResuMeta.Services.Concrete;
using Microsoft.Extensions.Options;

namespace ResuMeta.Services.Concrete
{
 
    class JsonListing
    {
        public string listing_company { get; set; }
        public string listing_link { get; set; }
        public string listing_location { get; set; }
        public string listing_title { get; set; }
    }
    class JsonListings
    {
        public List<JsonListing> listings { get; set; }
    }

    class JsonJobs
    {
        public List<JsonListing> jobs { get; set; }
    }
    public class WebScraperService : IWebScraperService
    {
        private readonly HttpClient _httpClient;
        private readonly string _scraperUrl;
        public WebScraperService(HttpClient httpClient, IOptions<WebScraperServiceOptions> options)
        {
            _httpClient = httpClient;
            _scraperUrl = options.Value.ScraperUrl;

        }

        public async Task<List<JobListingVM>> GetCachedListings(int pageNum)
        {
            Console.WriteLine("Getting cached listings");
            var url = _scraperUrl + "api/cached_listings/" + pageNum;
            var response = await _httpClient.GetAsync(url);
            Console.WriteLine(response.StatusCode);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                try
                {
                    var jobListings = JsonSerializer.Deserialize<JsonListings>(responseString);
                    return jobListings.listings.Select(l => new JobListingVM
                    {
                        Company = l.listing_company,
                        Link = l.listing_link,
                        Location = l.listing_location,
                        JobTitle = l.listing_title
                    }).ToList();
                }
                catch (Exception e)
                {
                    throw new Exception("Error getting cached listings");
                }
            }
            else
            {
                throw new Exception("Error getting cached listings");
            }
        }

        public async Task<List<JobListingVM>> SearchJobs(string jobTitle, string city, string state)
        {
            var urlJobTitle = WebUtility.UrlEncode(jobTitle);
            var urlCity = WebUtility.UrlEncode(city);
            var urlState = WebUtility.UrlEncode(state);
            var response = await _httpClient.GetAsync(_scraperUrl + "api/search_jobs?job_title=" + urlJobTitle + "&city=" + urlCity + "&state=" + urlState);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                try
                {
                    var jobListings = JsonSerializer.Deserialize<JsonJobs>(responseString);
                    return jobListings.jobs.Select(l => new JobListingVM
                    {
                        Company = l.listing_company,
                        Link = l.listing_link,
                        Location = l.listing_location,
                        JobTitle = l.listing_title
                    }).ToList();
                }
                catch (Exception e)
                {
                    throw new Exception("Error searching jobs");
                }
            }
            else
            {
                throw new Exception("Error searching jobs");
            }
        }
    }
}