document.addEventListener('DOMContentLoaded', initializePage, false);

function initializePage() {
    const getCachedListingsBtn = document.getElementById('get-cached-listings');
    const searchJobListingsBtn = document.getElementById('search-job-listings');
    getCachedListingsBtn.addEventListener('click', getCachedJobListings, false);
    searchJobListingsBtn.addEventListener('click', searchJobListings, false);
}

async function getCachedJobListings() {
    console.log("Getting cached job listings");
    const jobListingContainer = document.getElementById('job-container');
    jobListingContainer.innerHTML = '';

    const response = await fetch(`api/scraper/cached_listings/${1}`, {
        method: 'PUT',
        headers: {
            'Accept': 'application/json; application/problem+json; charset=utf-8',
            'Content-Type': 'application/json; charset=utf-8'
        },
        body: JSON.stringify({
            pageNum: 1
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

    const jobTitle = "Software Engineer";
    const city = "Salem";
    const state = "OR";

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
    jobListingNode.innerHTML = `
        <div class="job-listing-title">
            <a href="${job.Link}" target="_blank">${job.JobTitle}</a>
        </div>
        <div class="job-listing-company">
            ${job.Company}
        </div>
        <div class="job-listing-location">
            ${job.Location}
        </div>
    `;
    return jobListingNode;
}