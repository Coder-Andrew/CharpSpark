document.addEventListener('DOMContentLoaded', initializePage, false);

let pageNumber = 1;

function initializePage() {
    const getCachedListingsBtn = document.getElementById('get-cached-listings');
    const searchJobListingsBtn = document.getElementById('search-job-listings');
    const searchCachedListings = document.getElementById('cached-job-title');

    searchCachedListings.addEventListener('change', () => { getCachedJobListings(pageNumber, searchCachedListings.value) })
    //document.getElementById('page-number').addEventListener('change', getCachedJobListings);
    getCachedJobListings(pageNumber, "");
    //getCachedListingsBtn.addEventListener('click', getCachedJobListings, false);
    searchJobListingsBtn ? searchJobListingsBtn.addEventListener('click', searchJobListings, false) : "";
}
function hideLoader() {
    //document.getElementById('page-number').classList.remove("invisible");
    document.getElementById("loader").classList.add("invisible");
}
function showLoader() {
    //document.getElementById('page-number').classList.add("invisible");
    document.getElementById("loader").classList.remove("invisible");
}

async function getCachedJobListings(pageNumber, jobTitle) {
    showLoader();
    console.log("Getting cached job listings");
    //var pageNum = document.getElementById('page-number').value;
    const jobListingContainer = document.getElementById('job-container');
    jobListingContainer.innerHTML = '';

    const response = await fetch(`api/scraper/cached_listings?pageNum=${pageNumber}&jobTitle=${jobTitle}`, {
        method: 'GET',
        headers: {
            'Accept': 'application/json; application/problem+json; charset=utf-8',
            'Content-Type': 'application/json; charset=utf-8'
        }
    });
    if (response.ok) {        
        var jobs = await response.json();
        console.log(jobs.jobListings);
        if (jobs.jobListings.length === 0) {
            let noResultsNode = document.createElement('div');
            noResultsNode.className = 'no-results';
            noResultsNode.innerHTML = 'No cached job listings found';
            jobListingContainer.appendChild(noResultsNode);
        } else {
            jobs.jobListings.forEach(job => {
                const jobListingNode = generateJobListingNode(job);
                jobListingContainer.appendChild(jobListingNode);
            });
        }
    }
    else 
    {
        var noResultsNode = document.createElement('div');
        noResultsNode.className = 'no-results';
        noResultsNode.innerHTML = 'No cached job listings found';
        jobListingContainer.appendChild(noResultsNode);
    }
    hideLoader();
    return;
}

async function searchJobListings() {
    showLoader();
    console.log("Searching job listings");
    const jobListingContainer = document.getElementById('job-container');
    jobListingContainer.innerHTML = '';

    const jobTitle = document.getElementById('job-title').value;
    const city = document.getElementById('city').value;
    const state = document.getElementById('state').value;
    const response = await fetch(`api/scraper/search_jobs/job_title=${jobTitle}&city=${city}&state=${state}`, {
        method: 'GET',
        headers: {
            'Accept': 'application/json; application/problem+json; charset=utf-8',
            'Content-Type': 'application/json; charset=utf-8'
        }
    });
    if (response.ok) {
        var contentType = response.headers.get("content-type");
        if (contentType && contentType.indexOf("application/json") !== -1) {
            var jobs = await response.json();
            console.log(jobs);
            jobs.forEach(job => {
                const jobListingNode = generateJobListingNode(job);
                jobListingContainer.appendChild(jobListingNode);
            });
            hideLoader();
        }
        else
        {
            var noResultsNode = document.createElement('div');
            noResultsNode.className = 'no-results';
            noResultsNode.innerHTML = 'No job listings found';
            jobListingContainer.appendChild(noResultsNode);
            hideLoader();
            return;
        }
    }
    else 
    {
        var noResultsNode = document.createElement('div');
        noResultsNode.className = 'no-results';
        noResultsNode.innerHTML = 'No job listings found';
        jobListingContainer.appendChild(noResultsNode);
        hideLoader();
        return;
    }
    hideLoader();
}

function generateJobListingNode(job) {
    const jobListingTemplate = document.getElementById("job-listing-template");
    const templateClone = jobListingTemplate.content.cloneNode(true);
    const clone = templateClone.firstElementChild;

    const jobLink = clone.querySelector("a");
    const jobTitle = clone.querySelector(".job-listing-title");
    const jobCompany = clone.querySelector(".job-listing-company");
    const jobLocation = clone.querySelector(".job-listing-location"); 


    jobLink.href = `https://www.${job.link}`;
    jobTitle.textContent = job.jobTitle;
    jobCompany.innerHTML = `<span class="fa fa-building"></span> ${job.company}`;
    jobLocation.innerHTML = `<span class="fa fa-globe"></span> ${job.location}`;


    return clone;
}