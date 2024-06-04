# ResuMeta Node API Documentation

## Overview
The ResuMeta Node microservice is designed to handle the import and export of resumes in PDF format. It transcribes resumes into a readable format for the site and exports resumes back to PDF format.

## Prerequisites
Before setting up the microservice, ensure you have the following installed on your machine:
- Node.js
- npm (Node Package Manager)

## How to Set Up

1. **Clone the Repository**
    ```bash
    git clone <repository-url>
    ```

2. **Navigate to the Project Directory**
    ```bash
    cd ResuMeta_NodeAPI/NodePdf
    ```

3. **Install Dependencies**
    - The `package.json` file includes all the packages used in this project.
    - Run the following command to install the required packages:
    ```bash
    npm install
    ```

4. **Verify Installation**
    - You should now see a `node_modules` folder in the project directory. This folder contains all the installed packages.

5. **Start the Server**
    - Run the following command to start the server. This will execute `index.js`, which listens for requests:
    ```bash
    node .
    ```

## Usage
After setting up the server, you can start using the microservice to import and export resumes. Ensure the `NodeUrl` variable is correctly set in your .NET server configuration to interact with this Node.js microservice.

## Additional Information
- The microservice is designed to handle PDF files for resumes.
- Make sure to test the endpoints to ensure they are working as expected.

---

By following these steps, you should be able to set up and run the ResuMeta Node microservice on your machine.
