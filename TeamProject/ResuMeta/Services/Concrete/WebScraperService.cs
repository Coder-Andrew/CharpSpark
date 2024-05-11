using System.Text.Json;
using ResuMeta.Services.Abstract;
using ResuMeta.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using ResuMeta.Services.Concrete;
using Microsoft.Extensions.Options;
using EllipticCurve.Utils;

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
        public int number_of_pages { get; set; }
    }

    class JsonJobs
    {
        public List<JsonListing> jobs { get; set; }
    }

    class JsonJobDescription
    {
        public string job_description { get; set; }
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

        public async Task<JobListingContainerVM> GetCachedListings(int pageNum, string jobTitle)
        {
            Console.WriteLine("Getting cached listings");
            var url = _scraperUrl + "api/cached_listings?page_number=" + pageNum + "&job_title=" + System.Net.WebUtility.UrlEncode(jobTitle);
            var response = await _httpClient.GetAsync(url);
            Console.WriteLine(response.StatusCode);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                try
                {
                    var jobListings = JsonSerializer.Deserialize<JsonListings>(responseString);
                    JobListingContainerVM listingsContainer = new JobListingContainerVM();
                    listingsContainer.JobListings = jobListings.listings.Select(l => new JobListingVM
                    {
                        Company = l.listing_company,
                        Link = l.listing_link,
                        Location = l.listing_location,
                        JobTitle = l.listing_title
                    }).ToList();

                    listingsContainer.NumberOfPages = jobListings.number_of_pages;

                    return listingsContainer;
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
        public async Task<JobDescriptionVM> GetJobDescription(JsonElement url)
        {
            string endPoint = "api/job_description";
            string messageAndEndpoint = _scraperUrl + endPoint;
            var json = JsonSerializer.Serialize(url);
            var stringContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            var response = _httpClient.PostAsync(messageAndEndpoint, stringContent);

            if (response.Result.StatusCode == HttpStatusCode.OK)
            {
                var responseString = await response.Result.Content.ReadAsStringAsync();
                try
                {
                    var jobDescription = JsonSerializer.Deserialize<JsonJobDescription>(responseString);
                    JobDescriptionVM description = new JobDescriptionVM
                    {
                        JobDescription = jobDescription.job_description
                    };
                    return description;
                }
                catch (Exception e)
                {
                    throw new Exception("Error getting job description");
                }
            }
            else
            {
                throw new Exception("Error getting job description");
            }
        }
    }
}