# EnquiryManagementSystem (Blazor Server)

This project is a minimal Blazor Server app wired to an EF Core SQLite database.

## Steps to run in Visual Studio

1. Open the solution folder `EnquiryManagementSystem` in Visual Studio.
2. Restore NuGet packages.
3. Open Package Manager Console (Tools > NuGet Package Manager > Package Manager Console).
4. Run:
   Add-Migration InitialCreate
   Update-Database
   This will create `ems.db` in the project folder and apply seed data.
5. Run the project (F5). Navigate to `/ems` to view the Enquiry page.

If you prefer CLI:
dotnet ef migrations add InitialCreate
dotnet ef database update

