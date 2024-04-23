# create a basic flask hello world program
import time
import threading
import schedule
import argparse
from flask import Flask, request

from src.advanced_search import advanced_search
from src.cache_listings import get_and_cache_listings
from src.db_context import mongo_db_context, NUMBER_OF_LISTINGS_PER_PAGE
from src.job_listing_repository import job_listing_repository

app = Flask(__name__)

# Schedule the job to run every 24 hours
schedule.every(24).hours.do(get_and_cache_listings)

def run_scheduled_jobs():
    while True:
        schedule.run_pending()
        time.sleep(14400)

@app.route('/api/search_jobs')
def search_jobs():
    job_title = request.args.get('job_title')
    city = request.args.get('city')
    state = request.args.get('state')

    if job_title == None or city == None or state == None:
        return {'error': 'job_title, city, and state are required parameters'}
    
    jobs = advanced_search(job_title, city, state)

    return {'jobs': [job.__dict__ for job in jobs]}

@app.route('/api/cached_listings')
def get_cached_listings():
    job_title = request.args.get('job_title')
    page_number = request.args.get('page_number')

    if not page_number.isdigit():
        return {'error': 'page_number must be an integer'}
    
    page_number = int(page_number)

    print(f"job_title: {job_title}, page_number: {page_number}")

    mongoDbConnection = args.mongoDbConnectionString
    context = mongo_db_context(connectionString=mongoDbConnection, dbname='job_listings', collectionName='job_listings')
    # context = mongo_db_context(ip='localhost', port=27017, dbname='job_listings', collectionName='job_listings')
    repo = job_listing_repository(context)
    
    
    try:
        listings = list(repo.get_cached_listings(page_number, job_title))
        [i.pop('_id') for i in listings]

        return {'number_of_pages': repo.get_number_of_listings_by_search_term(job_title) // NUMBER_OF_LISTINGS_PER_PAGE + 1, 'listings': listings}
    except ValueError as e:
        return {'error': str(e)}
    except:
        return {'error': 'An error occurred'}


if __name__ == '__main__':
    parser = argparse.ArgumentParser(description='Scrape job listings')
    parser.add_argument('mongoDbConnectionString', type=str, help='The connection string to the MongoDB database')
    args = parser.parse_args()
    mongoDbConnection = args.mongoDbConnectionString

    #run scheduled jobs on a separate thread
    # get_and_cache_listings(mongoDbConnection)
    # t = threading.Thread(target=run_scheduled_jobs)
    # t.start()



    app.run(debug=True, host='0.0.0.0', port=5722)
