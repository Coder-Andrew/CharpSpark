from src.advanced_search import advanced_search
from src.cache_listings import get_cachable_listings, cache_listing

# This is for dev purposes to see if the two functions work
# This will be removed once the functions are integrated into the Flask web app
if __name__ == "__main__":
    print("Welcome to ResuMeta!")
    print("Would you like to search for a job or cache remote job listings?")
    print("1. Search for a job")
    print("2. Cache remote job listings")
    user_choice = input("Enter the number of your choice: ")

    if user_choice == "1":
        job_title = input("Enter the job title: ")
        city = input("Enter the city: ")
        state = input("Enter the state: ")
        advanced_search(job_title, city, state)
    elif user_choice == "2":
        jobs = get_cachable_listings()
        for job in jobs:
            cache_listing(job)
    else:
        print("Invalid choice. Please run the program again.")