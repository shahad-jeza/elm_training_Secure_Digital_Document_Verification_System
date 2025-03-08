

Document Verification System

A secure and modern system for uploading, managing, and verifying digital documents. Built with Angular for the frontend and ASP.NET Core for the backend.

Features
	•	Dashboard: View a list of uploaded documents with their status (Pending, Verified, Rejected).
	•	Document Upload: Upload documents with a title and file, and track their verification status.
	•	Document Verification: Verify documents using a unique verification code.
	•	User-Friendly Interface: Modern and responsive UI built with Bootstrap.

Technologies Used

Frontend
	•	Angular: Frontend framework for building the user interface.
	•	Bootstrap: For modern and responsive styling.
	•	RxJS: For handling asynchronous operations.
	•	Angular Forms: For form validation and handling.

Backend
	•	ASP.NET Core: Backend framework for building RESTful APIs.
	•	Entity Framework Core: For database operations and migrations.
	•	SQLite: Lightweight database for storing documents and verification logs.

Getting Started

Prerequisites

Make sure you have the following installed:
	•	Node.js and npm (for Angular)
	•	.NET SDK (for ASP.NET Core)
	•	SQLite (or any other database supported by EF Core)



Project Structure

Backend
	•	Controllers: Handle API endpoints (DocumentsController, VerificationController).
	•	Models: Define database entities (Document, VerificationLog, User).
	•	Data: Database context and migrations (AppDbContext).

Frontend
	•	Components: Angular components for the UI (DashboardComponent, DocumentUploadComponent, VerificationComponent).
	•	Services: Angular services for API communication (ApiService).
	•	Routing: Angular routing for navigation (app.routes.ts).

