from src.db_context import mongo_db_context
from src.job_listing_repository import job_listing_repository
from src.job_listing_model import job_listing

context = mongo_db_context(connectionString="mongodb://localhost:27017/", dbname="job_listings", collectionName='job_listings')
repo = job_listing_repository(context)

print(repo.get_cached_listings(1, "Accelerato"))