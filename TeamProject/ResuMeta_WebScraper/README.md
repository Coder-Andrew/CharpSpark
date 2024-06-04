# About
The WebScraper is designed to scrape job listings from Indeed.com, cache job listings through MongoDB, and provide advanced search options to the user.

# Prerequisites
To run the web scraper, you will need the following:
1. Python
2. Pipenv (install with `pip install pipenv`)
3. A MongoDB database
4. A MongoDB database connection string
5. (optional) A way to host the web scraper

# Installation and Running
1. Clone the repository and navigate to the project directory:
    ```bash
    cd ResuMeta_WebScraper
    ```

2. Install the required dependencies using Pipenv:
    ```bash
    pipenv install
    ```

3. Activate the Pipenv shell:
    ```bash
    pipenv shell
    ```

4. Run the application with your MongoDB connection string:
    ```bash
    python app.py <your mongodb connection string>
    ```

Replace `<your mongodb connection string>` with your actual MongoDB connection string.
