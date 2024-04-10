document.addEventListener('DOMContentLoaded', initializePage, false);

function initializePage() {
    const getCachedListingsBtn = document.getElementById('get-cached-listings');
    const searchJobListingsBtn = document.getElementById('search-job-listings');
    getCachedListingsBtn.addEventListener('click', getCachedJobListings, false);
    searchJobListingsBtn.addEventListener('click', searchJobListings, false);
}

async function getCachedJobListings() {
    console.log("Getting cached job listings");
    var pageNum = document.getElementById('page-number').value;
    const jobListingContainer = document.getElementById('job-container');
    jobListingContainer.innerHTML = '';

    const response = await fetch(`api/scraper/cached_listings/${pageNum}`, {
        method: 'PUT',
        headers: {
            'Accept': 'application/json; application/problem+json; charset=utf-8',
            'Content-Type': 'application/json; charset=utf-8'
        },
        body: JSON.stringify({
            pageNum: pageNum
        })
    });
    if (response.ok) {
        var jobs = await response.json();
        console.log(jobs);
        jobs.forEach(job => {
            const jobListingNode = generateJobListingNode(job);
            jobListingContainer.appendChild(jobListingNode);
        });
    }
    else 
    {
        console.log("Error fetching cached job listings");
        return;
    }
}

async function searchJobListings() {
    console.log("Searching job listings");
    const jobListingContainer = document.getElementById('job-container');
    jobListingContainer.innerHTML = '';

    const jobTitle = document.getElementById('job-title').value;
    const city = document.getElementById('city').value;
    const state = document.getElementById('state').value;

    const response = await fetch(`api/scraper/search_jobs`, {
        method: 'PUT',
        headers: {
            'Accept': 'application/json; application/problem+json; charset=utf-8',
            'Content-Type': 'application/json; charset=utf-8'
        },
        body: JSON.stringify({
            title: jobTitle,
            city: city,
            state: state
        })
    });
    if (response.ok) {
        var jobs = await response.json();
        console.log(jobs);
        jobs.forEach(job => {
            const jobListingNode = generateJobListingNode(job);
            jobListingContainer.appendChild(jobListingNode);
        });
    }
    else 
    {
        console.log("Error fetching job listings");
        return;
    }
}

function generateJobListingNode(job) {
    const jobListingNode = document.createElement('div');
    jobListingNode.className = 'job-listing';
    var link = "https://www." + job.link;
    jobListingNode.innerHTML = `
        <div class="job-listing-title">
            <a href="${link}" target="_blank">${job.jobTitle}</a>
        </div>
        <div class="job-listing-company">
            ${job.company}
        </div>
        <div class="job-listing-location">
            ${job.location}
        </div>
    `;
    return jobListingNode;
}