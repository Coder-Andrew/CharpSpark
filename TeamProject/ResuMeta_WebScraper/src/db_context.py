from pymongo import MongoClient
from abc import ABC, abstractmethod
from .job_listing_model import job_listing
from typing import List

'''
This script is the interface for a DbContext. If you desire to add another db source, 
you can create a new class that inherits from IDbContext and implement the methods.

This could easily be split into multiple files/directories, but for the sake of simplicity,
I left it here.

Definitely violated the intention of the interface by implementing extra methods used elsewhere...
'''

# This global should probably be moved outside of the context and into the flask route
# so that users can decide how many listings they want to see per page
NUMBER_OF_LISTINGS_PER_PAGE = 10

class IDbContext(ABC):
    @abstractmethod
    def save(self, job_listing: job_listing):
        pass

    @abstractmethod
    def get_cached_listings(self, pageNumber: int) -> List[job_listing]:
        pass


class mongo_db_context(IDbContext):
    def __init__(self, connectionString: str, dbname: str, collectionName: str):
        self.collection = MongoClient(connectionString)[dbname][collectionName]
        # self.collection = MongoClient(ip, port, serverSelectionTimeoutMS=5000)[dbname][collectionName]

    def save(self, job_listing: job_listing) -> None:
        # If the listing already exists, (the scraper already scraped this listing), we will update it
        if self.collection.find_one({'listing_link': job_listing.listing_link}):
            self.collection.update_one({'listing_link': job_listing.listing_link}, {'$set': job_listing.__dict__})
            return
        
        self.collection.insert_one(job_listing.__dict__)

    def get_cached_listings(self, pageNumber: int, search_term: str = None) -> List[job_listing]:
        # This should be moved into a class and expose a .jobs method that returns the listings instead of being done in dbcontext, but for the sake of simplicity, I left it here
        # Return the number of cached listing based on the number of listings per page (pagination)
        if not isinstance(pageNumber, int):
            return TypeError("Page number must be an integer")

        if pageNumber <= 0:
            raise ValueError("Page number must be greater than 0")
        
        if self.get_number_of_listings() - ((pageNumber - 1) * NUMBER_OF_LISTINGS_PER_PAGE) < 0:
            raise ValueError("Page number is greater than the number of pages")
        
        if not search_term:
            return list(self.collection.find().skip((pageNumber - 1) * NUMBER_OF_LISTINGS_PER_PAGE).limit(NUMBER_OF_LISTINGS_PER_PAGE))
        
        return list(self.collection.find({"listing_title": {"$regex": search_term, "$options":'i'}}).skip((pageNumber - 1) * NUMBER_OF_LISTINGS_PER_PAGE).limit(NUMBER_OF_LISTINGS_PER_PAGE))
    
    def get_number_of_listings(self):
        return self.collection.count_documents({})
    
    def get_number_of_listings_by_search_term(self, search_term: str):
        return self.collection.count_documents({"listing_title": {"$regex": search_term, "$options":'i'}})