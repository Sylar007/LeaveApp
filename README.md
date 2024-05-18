Accessment for Upwork Task - Senior .NET Engineer
The goal of this exercise is to show some sample classes with comprehensive comments in the code & relevant documentation that explains the code and the functionality

Name: Mohd Diah A.Karim
In the exercise, I'm showcase the web application created with Blazor as an UI. The project solution built using a Clean Architecture approach in organizing its code into projects. With the clean architecture, the UI layer works with interfaces defined in the Application Core at compile time, and ideally shouldn't know about the implementation types defined in the Infrastructure layer. At run time, however, these implementation types are required for the app to execute, so they need to be present and wired up to the Application Core interfaces via dependency injection.

Couple of libraries and architectures :-

Clean or Onion Architecture

Command Query Responsibility Segregation (CQRS)

Implement Mediatr Pattern

Add Email Service using SendGrid

Implementing Unit Testing

Moq and Shouldy

Global Error Handling with Custom Middleware and Exceptions

Validation Using Fluent Validation

.NET Core API and Blazor UI Application

Implement JWT(JSON Web Token)  Authentication


How to run test

Open command prompt and go to Web project folder "HR.LeaveManagement.BlazorUI" for UI and "HR.LeaveManagement.Api" for API
Type dotnet run <-- ensure that .NET cli has been installed

What can be improved from this project

Handling concurrent request In .NET(C#) we also using Polly bulk head or SemaphoreSlim for handling the overwhelm request to the API service.
