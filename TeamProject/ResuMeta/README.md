# ResuMeta .NET Web App Documentation

## Overview
This is the main C# .NET web app, which handles the majority of the functionality for the site. It manages resumes, communicates with APIs, and interacts with both the app and auth databases.

## Prerequisites
Before setting up the web app, ensure you have the following installed on your machine:
1. C# .NET (specifically .NET 8)
2. SQL Database
3. .NET User-Secrets Manager
4. The other prerequisites for the web scraper and Node.js web app
5. Google reCAPTCHA API key
6. Google Sign-In API key
7. SendGrid API key
8. ChatGPT API key

## Installation and User Secrets

1. **Clone the Repository**
    ```bash
    git clone <repository-url>
    ```

2. **Navigate to the ResuMeta Project Directory**
    ```bash
    cd TeamProject/ResuMeta
    ```

3. **Set Up Your Databases**
    - Up your app database:
    ```bash
    <execute the up script at `TeamProject/ResuMeta/Data/Up.sql`>
    ```
    - Seed your app database:
    ```bash
    <execute the seed script at `TeamProject/ResuMeta/Data/SeedResuMetaDb.sql`>
    ```
    - Up your auth database:
    ```bash
    <execute the up script at `TeamProject/ResuMeta/Data/UpdateIdentityAzure.sql`>
    ```

4. **Set Up User Secrets**

### Setting up .NET User Secrets

1. **Initialize .NET User Secrets**
    ```bash
    dotnet user-secrets init
    ```

2. **Add User Secrets**
    - Set SendGrid API Key:
    ```bash
    dotnet user-secrets add SendGridApiKey <your send grid api key>
    ```
    - Set Send From Email:
    ```bash
    dotnet user-secrets add SendFromEmail <an email for send grid>
    ```
    - Set Seed User Passwords (default password for seeded users):
    ```bash
    dotnet user-secrets add SeedUserPW <a password of your choice>
    ```
    - Set Web Scraper URL (for job listing scraper):
    ```bash
    dotnet user-secrets add ScraperUrl <your scraper url>
    ```
    - Set Authentication Database Connection (for user accounts):
    ```bash
    dotnet user-secrets add ConnectionStrings:ResuMetaConnection <sql connection string for auth db>
    ```
    - Set ResuMeta Application Database Connection:
    ```bash
    dotnet user-secrets add ConnectionStrings:ResuMetaConnection <sql connection string for app db>
    ```
    - Set Google Sign-In API Key:
    ```bash
    dotnet user-secrets add Authentication:Google:ClientSecret <google sign in api key>
    ```
    - Set ChatGPT API Key:
    ```bash
    dotnet user-secrets add ChatGPTAPIKey <your chatgpt api key>
    ```
    - Set Google Sign-In Client ID:
    ```bash
    dotnet user-secrets add Authentication:Google:ClientId <google sign in client id key>
    ```

---

By following these steps, you should be able to set up and run the ResuMeta .NET web app on your machine with the `dotnet run` command. If you encounter any issues or need further assistance, refer to the project's documentation or seek help from the community.
