from .job_listing_model import job_listing
from .db_context import IDbContext
from typing import List


class job_listing_repository:
    def __init__(self, context: IDbContext) -> None:
        self.context = context

    def save(self, job_listing: job_listing):
        self.context.save(job_listing)

    def get_cached_listings(self, pageNumber: int) -> List[job_listing]: 
        return self.context.get_cached_listings(pageNumber)