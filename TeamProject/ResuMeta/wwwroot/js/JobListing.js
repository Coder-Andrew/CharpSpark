document.addEventListener('DOMContentLoaded', initializePage, false);

let pageNumber = 1;
let numberOfPages = 0;

function initializePage() {
    const getCachedListingsBtn = document.getElementById('get-cached-listings');
    const searchJobListingsBtn = document.getElementById('search-job-listings');
    const searchCachedListings = document.getElementById('cached-job-title');
    const pageNumberInput = document.getElementById('page-number');

    searchCachedListings.addEventListener('change', () => { getCachedJobListings(pageNumber, searchCachedListings.value) })
    //document.getElementById('page-number').addEventListener('change', getCachedJobListings);
    getCachedJobListings(pageNumber, "");
    //getCachedListingsBtn.addEventListener('click', getCachedJobListings, false);
    searchJobListingsBtn ? searchJobListingsBtn.addEventListener('click', searchJobListings, false) : "";

    pageNumberInput.addEventListener('click', (event) => {
        if (event.target.tagName === 'A') {
            pageNumber = parseInt(event.target.textContent);
            getCachedJobListings(pageNumber, searchCachedListings.value);
        }
    })
}
function hideLoader() {
    document.getElementById('page-number').classList.remove("invisible");
    document.getElementById("loader").classList.add("invisible");
}
function showLoader() {
    document.getElementById('page-number').classList.add("invisible");
    document.getElementById("loader").classList.remove("invisible");
}

async function getCachedJobListings(pageNumber, jobTitle) {
    showLoader();
    console.log("Getting cached job listings");
    //var pageNum = document.getElementById('page-number').value;
    const jobListingContainer = document.getElementById('job-container');
    jobListingContainer.innerHTML = '';
    if (jobTitle) pageNumber = 1;
    const response = await fetch(`api/scraper/cached_listings?pageNum=${pageNumber}&jobTitle=${jobTitle}`, {
        method: 'GET',
        headers: {
            'Accept': 'application/json; application/problem+json; charset=utf-8',
            'Content-Type': 'application/json; charset=utf-8'
        }
    });
    if (response.ok) {
        var jobs = await response.json();
        numberOfPages = jobs.numberOfPages;
        populatePageNumber();
        console.log(jobs.numberOfPages);
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

function populatePageNumber() {
    const paginationList = document.querySelector('.pagination');
    paginationList.innerHTML = ''; // Clear the existing page numbers

    for (let i = -1; i <= 2; i++) {
        let index = i + pageNumber;
        if (index <= 0 || index > numberOfPages) continue;
        //if (index === numberOfPages || pageNumber !== 1) break;
        const pageItem = document.createElement('li');
        pageItem.className = 'page-item';

        if (index === pageNumber) {
            pageItem.classList.add('active');
        }

        const pageLink = document.createElement('a');
        pageLink.className = 'page-link';
        pageLink.href = '#';
        pageLink.textContent = index;

        pageItem.appendChild(pageLink);
        paginationList.appendChild(pageItem);
    }
}

//function populatePageNumber() {
//    const paginationList = document.querySelector('.pagination');
//    paginationList.innerHTML = ''; // Clear existing page numbers

//    for (let i = 1; i <= numberOfPages; i++) {
//        const pageItem = document.createElement('li');
//        pageItem.className = 'page-item ' + (i === pageNumber ? 'active' : '');

//        const pageLink = document.createElement('a');
//        pageLink.className = 'page-link';
//        pageLink.href = '#';
//        pageLink.textContent = i;
//        pageLink.addEventListener('click', () => {
//            pageNumber = i;
//            getCachedJobListings(pageNumber, searchCachedListings.value);
//        });

//        pageItem.appendChild(pageLink);
//        paginationList.appendChild(pageItem);
//    }
//}


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


