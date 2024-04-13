from src.job_listing_model import job_listing
from src.db_context import mongo_db_context
from src.job_listing_repository import job_listing_repository


if __name__ == '__main__':
    # Create a new job listing
    job_listing = job_listing(
        listing_link='https://www.google2.com',
        listing_title='Software Engineer',
        listing_company='Googles',
        listing_location='Mountain View, OR'
    )

    # Create a new MongoDbContext
    context = mongo_db_context(ip='localhost', port=27017, dbname='job_listings', collectionName='job_listings')

    # Create a new JobListingRepository
    repo = job_listing_repository(context)

    # Save the job listing
    repo.save(job_listing)

    # Get the cached listings
    listings = repo.get_cached_listings(1)

    # Print the listings
    for listing in listings:
        print(listing)