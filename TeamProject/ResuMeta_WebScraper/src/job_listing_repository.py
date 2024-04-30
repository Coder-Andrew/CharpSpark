from .job_listing_model import job_listing
from .db_context import IDbContext
from typing import List


class job_listing_repository:
    def __init__(self, context: IDbContext) -> None:
        self.context = context

    def save(self, job_listing: job_listing):
        self.context.save(job_listing)

    # This method is where the pagination and job title search should be implemented, but I wanted
    # mongodb to handle the pagination and search in case there are optimizations that mongodb does
    def get_cached_listings(self, pageNumber: int, search_term:str = None) -> List[job_listing]: 
        return self.context.get_cached_listings(pageNumber, search_term)

    def get_number_of_listings_by_search_term(self, search_term: str):
        return self.context.get_number_of_listings_by_search_term(search_term)
