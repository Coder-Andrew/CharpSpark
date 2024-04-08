# create a basic flask hello world program
import time
import threading
import schedule
from flask import Flask, request

from src.cache_listings import get_and_cache_listings
from src.db_context import mongo_db_context
from src.job_listing_repository import job_listing_repository

app = Flask(__name__)

# Schedule the job to run every 24 hours
#schedule.every(24).hours.do(get_and_cache_listings)

def run_scheduled_jobs():
    while True:
        schedule.run_pending()
        time.sleep(1)

@app.route('/<message>')
def hello_world(message):
    return f'Hello, World! {message}'

@app.route('/api/cached_listings/<int:page>')
def get_cached_listings(page):
    print("test")
    context = mongo_db_context(ip='localhost', port=27017, dbname='job_listings', collectionName='job_listings')
    repo = job_listing_repository(context)
    
    listings = list(repo.get_cached_listings(page))
    [i.pop('_id') for i in listings]
    
    return {'listings': listings}


if __name__ == '__main__':
    #run scheduled jobs on a separate thread
    #get_and_cache_listings()
    # t = threading.Thread(target=run_scheduled_jobs)
    # t.start()



    app.run(debug=True)


# Left off trying to figure out why the scraper is running twice 