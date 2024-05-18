Accessment for Upwork Task - Senior .NET Engineer
The goal of this exercise is to show some sample classes with comprehensive comments in the code & relevant documentation that explains the code and the functionality.
The main comprehensive comments is in HR.LeaveManagement.Application folder under Features/LeaveRequest.


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

**About this application**
In a modern corporate environment, the process of managing leave requests is a critical aspect of human resources operations. When an employee decides to take a leave of absence, they initiate the process by submitting a leave request through the company's digital leave management system. This request includes essential details such as the type of leave, start and end dates, and the reason for the leave. Upon submission, the system promptly records the request, ensuring that all data is accurately captured and stored within the employee's personnel file.

The system then triggers an automated workflow, sending a notification to the employee's manager. This notification is designed to alert the manager of the pending leave request, prompting them to review the details. The manager, equipped with insights into team schedules and project deadlines, then deliberates on the request. They have the authority to approve or reject the leave based on various factors, including the employee's leave balance, the impact on team productivity, and the validity of the leave reason.

Once the manager makes a decision, they execute the approval or rejection through the system. This action automatically updates the employee's leave balance, deducting the approved days from their total entitlement or leaving it unchanged in the case of a rejection. The final step in this streamlined process is the system's notification to the employee, informing them of the manager's decision. This notification is crucial as it provides clarity and allows the employee to plan accordingly. Whether the leave is approved or not, the employee receives timely communication, ensuring transparency and maintaining trust in the system's efficacy.

This seamless integration of technology in leave management not only simplifies administrative tasks but also enhances the employee experience by providing a clear, efficient, and user-friendly process for handling leave requests. It exemplifies how digital transformation in the workplace can lead to improved operational efficiency and employee satisfaction.

Here's a high-level overview of the diagrams for a leave management system:

**Use Case Diagram:**
The use case diagram identify the different types of users (actors) and the various interactions they have with the system. Actors includes `Employee`, `Manager`, and `HR Personnel`. Use cases for an employee  include `Apply for Leave`, `View Leave Balance`, `View eave Allocation` and `Cancel Leave Request`. Managers have use cases like `Approve Leave Request`, `Reject Leave Request`, and `View Team Leave Schedule`. HR Personnel have `Allocate Leave`, `Update Leave Policy`, and `Generate Leave Reports`.

**Sequence Diagram:**
The sequence diagram show how objects interact in a particular scenario of a use case. For the `Apply for Leave` use case, the sequence involve an `Employee` sending a leave request, which the system records before forwarding a notification to the `Manager`. The `Manager` then responds, and the system updates the leave balance and notifies the `Employee` of the approval or rejection.

**Class Diagram:**
Classes includes `LeaveRequest` with attributes like `requestDate`, `startDate`, `endDate`, `leaveType`, and methods like `submitRequest()`, `cancelRequest()`. The `LeaveAllocation` class might have attributes like `totalLeaves`, `leavesTaken`, `leavesRemaining`, and methods like `allocateLeaves()`, `updateBalance()`. The `LeaveType` class could have attributes like `typeName`, `maxAllowed`, and a method like `updatePolicy()`.

```markdown
# Use Case Diagram
## Actors
- Employee
- Manager
- HR Personnel

## Use Cases for Employee
- Apply for Leave
- View Leave Balance
- Cancel Leave Request

## Use Cases for Manager
- Approve Leave Request
- Reject Leave Request
- View Team Leave Schedule

## Use Cases for HR Personnel
- Allocate Leave
- Update Leave Policy
- Generate Leave Reports

# Sequence Diagram for 'Apply for Leave'
1. Employee sends leave request.
2. System records request.
3. System sends notification to Manager.
4. Manager approves/rejects request.
5. System updates leave balance.
6. System notifies Employee of decision.

# Class Diagram
## Class: LeaveRequest
### Attributes
- requestDate
- startDate
- endDate
- leaveType
### Methods
- submitRequest()
- cancelRequest()

## Class: LeaveAllocation
### Attributes
- totalLeaves
- leavesTaken
- leavesRemaining
### Methods
- allocateLeaves()
- updateBalance()

## Class: LeaveType
### Attributes
- typeName
- maxAllowed
### Methods
- updatePolicy()
```
How to run test

Open command prompt and go to Web project folder "HR.LeaveManagement.BlazorUI" for UI and "HR.LeaveManagement.Api" for API
Type dotnet run <-- ensure that .NET cli has been installed

What can be improved from this project

Handling concurrent request In .NET(C#) we also using Polly bulk head or SemaphoreSlim for handling the overwhelm request to the API service.
