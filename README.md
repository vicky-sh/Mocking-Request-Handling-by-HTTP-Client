# ASP.NET Core Web API for Social Media Posts (with HttpClient Mocking examples)

## Overview

This project is an ASP.NET Core Web API designed to manage (fetch and add) social media posts by interacting with the external `https://dummyjson.com` API. It serves as a practical example of how to mock `HttpMessageHandler` when unit testing services that depend on `HttpClient`.

## Features

The following API endpoints are defined:

*   `GET /api/SocialMediaPosts/{postId}/tags`: Retrieves all tags for a specified post ID.
*   `POST /api/SocialMediaPosts`: Adds a new post.

## Technologies Used

*   .NET 9
*   ASP.NET Core
*   Swagger/OpenAPI (for API documentation and testing)
*   xUnit (for unit tests)
*   `IHttpClientFactory` (for managing HttpClient instances)
*   `Shouldly` (for assertions in tests)
*   `AutoBogus` (for generating test data)

## Setup and Running the Project

### Prerequisites

*   .NET 9 SDK

### Steps

1.  **Clone the repository**:
    ```bash
    git clone <repository-url>
    ```
2.  **Build the project**:
    ```bash
    dotnet build WebApp.sln
    ```
3.  **Run the Web API project**:
    ```bash
    dotnet run --project WebApp/WebApp.csproj
    ```
4.  **Accessing the API**:
    *   The API will typically be available at `https://localhost:<port>` and `http://localhost:<port>`. The port number is usually configured in `WebApp/Properties/launchSettings.json` (e.g., 7095 for HTTPS, 5095 for HTTP).
    *   Access the Swagger UI for interactive testing and documentation at `https://localhost:<port>/swagger`.

## Running Tests

1.  Navigate to the solution directory (the directory containing `WebApp.sln`).
2.  Run the following command:
    ```bash
    dotnet test WebApp.sln
    ```

## Key Concepts Demonstrated

*   Building a RESTful Web API using ASP.NET Core.
*   Utilizing `IHttpClientFactory` for efficient `HttpClient` management.
*   Interacting with an external third-party API (`dummyjson.com`).
*   Implementing unit tests for services that make HTTP calls.
*   Mocking `HttpMessageHandler` (via a custom test base class like `MockHttpMessageHandlerTestBase`) to isolate services from external dependencies during tests.
*   Using Swagger/OpenAPI for API documentation and interactive testing.
