# create a basic flask hello world program
import time
import threading
import schedule
import argparse
from flask import Flask, request

from src.advanced_search import advanced_search
from src.cache_listings import get_and_cache_listings
from src.db_context import mongo_db_context
from src.job_listing_repository import job_listing_repository

app = Flask(__name__)

# Schedule the job to run every 24 hours
schedule.every(24).hours.do(get_and_cache_listings)

def run_scheduled_jobs():
    while True:
        schedule.run_pending()
        time.sleep(1)

@app.route('/api/search_jobs')
def search_jobs():
    job_title = request.args.get('job_title')
    city = request.args.get('city')
    state = request.args.get('state')

    if job_title == None or city == None or state == None:
        return {'error': 'job_title, city, and state are required parameters'}
    
    jobs = advanced_search(job_title, city, state)

    return {'jobs': [job.__dict__ for job in jobs]}

@app.route('/api/cached_listings/<int:page>')
def get_cached_listings(page):
    print("test")
    mongoDbConnection = args.mongoDbConnectionString
    context = mongo_db_context(connectionString=mongoDbConnection, dbname='job_listings', collectionName='job_listings')
    # context = mongo_db_context(ip='localhost', port=27017, dbname='job_listings', collectionName='job_listings')
    repo = job_listing_repository(context)
    
    listings = list(repo.get_cached_listings(page))
    [i.pop('_id') for i in listings]
    
    return {'listings': listings}


if __name__ == '__main__':
    parser = argparse.ArgumentParser(description='Scrape job listings')
    parser.add_argument('mongoDbConnectionString', type=str, help='The connection string to the MongoDB database')
    args = parser.parse_args()
    mongoDbConnection = args.mongoDbConnectionString

    #run scheduled jobs on a separate thread
    get_and_cache_listings(mongoDbConnection)
    t = threading.Thread(target=run_scheduled_jobs)
    t.start()



    app.run(debug=True, host='0.0.0.0', port=5722)
