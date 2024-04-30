import pytest

from src.db_context import mongo_db_context
from src.job_listing_repository import job_listing_repository
from src.job_listing_model import job_listing

# from CharpSparkWebScraper import get_cached_listings

# ^ fake import, as the function does not exist yet ^
# defined stub function down below for a better visualisation of the test cases
# def get_cached_listings(int: page_num):
#     # returns a list of the cached listings
#     # param: page_num: the 'page number' of the cached listings
#     # i.e. page_num = 1 returns the first x to 10 listings (x being from 1 to 9 depending on the number of listings cached)
#     # page_num = 2 returns the second 10 listings... etc.
#     # returns: a list of the cached listings
#     # raises: ValueError if page_num is not an integer
#     # raises: ValueError if page_num is less than 1
#     # raises: ValueError if page_num is greater than the number of pages
#     pass

# -------------------------------------------------------------------------
# Mock/Fake the Mongo DB cache of job listings here (or in a separate file)
# -------------------------------------------------------------------------
#@pytest.fixture
#def setup_test_db():
#setup the test db and populate with test data and return the db context
context = mongo_db_context(ip='localhost', port=27017, dbname='job_listings', collectionName='job_listings_test')
repo = job_listing_repository(context)

#clear the db
context.collection.delete_many({})

#populate the db with test data
job_listings = [job_listing(f"https://www.indeed.com/job{i}", f"Job Title {i}", f"Company {i}", f"Location {i}") for i in range(1, 26)]

#save the example job listings
for job in job_listings:
    repo.save(job)


def test_get_cached_listings_for_page_1_should_return_10_job_listings_when_there_are_25_listings():
    # Arrange
    page_num = 1

    # Act
    listings = repo.get_cached_listings(page_num)

    # Assert that the amount of cached listings returned are 10
    assert len(listings) == 10


def test_get_cached_listings_for_page_3_should_return_5_job_listings_when_there_are_25_listings():
    # Arrange
    page_num = 3

    # Act
    listings = repo.get_cached_listings(page_num)
    
    # Assert that the amount of cached listings returned are 5
    assert len(listings) == 5


def test_get_cached_listings_for_page_1_should_return_correct_job_listings():
    # Arrange
    page_num = 1
    # Fake/Moq the expected and cached listings, left blank for now
    expected_listings = [i.__dict__ for i in job_listings[:10]]

    # Act
    listings = context.get_cached_listings(page_num)

    # Assert that the cached listings returned are the expected listings
    for listing in listings:
        assert listing in expected_listings


def test_get_cached_listings_for_page_0_should_raise_value_error():
    # Arrange
    page_num = 0
    # Act / Assert
    with pytest.raises(ValueError):
        repo.get_cached_listings(0)


def test_get_cached_listings_for_page_4_should_raise_value_error_when_there_are_25_listings():
    # Arrange
    page_num = 4
    # Act / Assert
    with pytest.raises(ValueError):
        repo.get_cached_listings(page_num)

def test_get_cached_listings_should_raise_value_error_when_given_noninteger_page_num():
    # Arrange
    page_num = "Hello World!"
    # Act / Assert
    with pytest.raises(TypeError):
        repo.get_cached_listings(page_num)