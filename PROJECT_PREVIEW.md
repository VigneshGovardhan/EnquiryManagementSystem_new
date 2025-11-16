# Enquiry Management System - Project Preview

## Overview

A modern, professional Enquiry Management System built with:
- **Frontend**: ASP.NET Core Blazor (Server-side rendering)
- **Database**: Supabase PostgreSQL with Row-Level Security
- **UI Framework**: Custom light-themed CSS with professional styling
- **Architecture**: Modular service-based design

---

## Project Structure

```
EnquiryManagementSystem/
├── Pages/
│   ├── Dashboard.razor           (Analytics & Overview)
│   ├── Ems.razor                 (Main Enquiry Management)
│   ├── Ems.razor.cs              (Code-behind)
│   └── Index.razor               (Home page)
├── Services/
│   ├── EnquiryService.cs         (Business logic)
│   └── SupabaseService.cs        (Database integration)
├── Shared/
│   └── MainLayout.razor          (Navigation & Layout)
├── wwwroot/
│   └── css/
│       └── site.css              (Professional light-themed styles)
├── Data/
│   ├── Models/                   (Data models)
│   └── ApplicationDbContext.cs   (Entity Framework)
└── appsettings.json              (Configuration)
```

---

## Database Schema

### Tables (10 total)

1. **customer_companies** - Customer/Client/Consultant companies
   - Company details, contact info, status

2. **contact_persons** - Individual contacts
   - Name, designation, contact details

3. **users** - System users (Sales Engineers)
   - Login credentials, department, roles

4. **enquiry_for_items** - Enquiry categories
   - Item name, department, email notifications

5. **enquiries** - Main enquiry records
   - Request number, dates, details, status

6. **enquiry_customers** - Enquiry-to-Customer junction
7. **enquiry_contacts** - Enquiry-to-Contact junction
8. **enquiry_users** - Enquiry-to-User junction
9. **enquiry_items** - Enquiry-to-Item junction
10. **enquiry_audit_log** - Audit trail for all changes

### Security
- Row-Level Security (RLS) enabled on all tables
- Public access policies for development
- Ready for authentication integration

---

## Key Features

### 1. Dashboard Page
**Route**: `/dashboard`

Features:
- Total enquiries count
- Pending quotations count
- Active companies count
- Active users count
- Recent enquiries table
- Top customers table
- Sales engineers performance table
- Key metrics and statistics

### 2. Enquiry Management System
**Route**: `/ems`

Three main tabs:

#### A. New Enquiry Tab
- Source of Enquiry selection
- Enquiry & Due dates
- Site visit date (optional)
- Enquiry type selection with list management
- Enquiry for item selection
- Customer company selection
- Contact person selection
- Project name auto-complete
- Client name auto-complete
- Consultant name auto-complete
- Sales engineer assignment
- Enquiry details text area
- Document type checkboxes (Hard copies, Drawings, CD/DVD, Specs, Equipment Schedule)
- Remarks section
- Auto-acknowledgement & CEO signature options
- Add/Cancel buttons

#### B. Modify Enquiry Tab
- Search by Request No
- Load existing enquiry
- Edit all fields
- Save changes
- Audit trail of modifications

#### C. Search Enquiry Tab
- Multi-field search (Request No, Client, Project, SE)
- Date range filtering
- Results table with pagination
- Quick open to modify results

### 3. Master Data Management
Built-in modals for:
- **Customer Companies** - Add/Edit contractors, clients, consultants
- **Contact Persons** - Manage contacts for each company
- **Users** - Create/Edit sales engineers and roles
- **Enquiry Items** - Define enquiry categories with email notifications

### 4. UI Components
- Modern form cards with shadow effects
- Responsive table designs
- Modal dialogs for data entry
- Tab navigation with smooth transitions
- Input validation with error messages
- Multi-select list boxes with add/remove buttons
- Badge displays for status indicators
- Professional buttons with hover states

---

## Design System

### Colors (Light Theme)
- **Primary**: #0066cc (Professional Blue)
- **Success**: #28a745 (Green)
- **Danger**: #dc3545 (Red)
- **Warning**: #ffc107 (Yellow)
- **Background**: #f8f9fa (Light Gray)
- **Surface**: #ffffff (White)

### Typography
- Font: Segoe UI, Tahoma, Geneva, Verdana, sans-serif
- Sizes: Responsive scaling
- Line-height: 1.6 for readability
- Font-weights: 400, 500, 600, 700

### Spacing
- 8px base unit system
- Consistent padding/margins
- Mobile-first responsive design

### Effects
- Soft shadows for depth
- Smooth transitions (0.2s ease)
- Subtle hover states
- Rounded corners (4-8px)
- Professional gradients

---

## API Endpoints

### Supabase REST API
Base URL: `{SUPABASE_URL}/rest/v1/`

**Supported Operations**:
- GET `/customer_companies` - Retrieve all companies
- POST `/customer_companies` - Create new company
- PATCH `/customer_companies?id=eq.{id}` - Update company
- DELETE `/customer_companies?id=eq.{id}` - Delete company
- GET `/enquiries` - Retrieve all enquiries
- POST `/enquiries` - Create new enquiry
- And more for all tables...

### Authentication
- Headers: `apikey`, `Authorization: Bearer {token}`
- Content-Type: `application/json`

---

## Configuration

### appsettings.json
```json
{
  "Supabase": {
    "Url": "https://your-project.supabase.co",
    "AnonKey": "your-anon-key"
  }
}
```

### .env File
```
SUPABASE_URL=https://your-project.supabase.co
SUPABASE_ANON_KEY=your-anon-key
```

---

## Technology Stack

### Frontend
- ASP.NET Core 8.0
- Blazor (Server-side)
- Bootstrap-inspired utilities
- Custom CSS (no external UI library)

### Backend
- C# .NET Core
- Entity Framework Core
- Supabase PostgreSQL
- REST API integration

### Database
- PostgreSQL (Supabase managed)
- UUID primary keys
- Array data types for relationships
- JSONB for audit logs

---

## Getting Started

### Prerequisites
- .NET 8.0 SDK
- Supabase Project (connected)
- Web browser

### Setup Steps

1. **Configure Supabase**
   ```bash
   # Update appsettings.json with your Supabase credentials
   ```

2. **Run the Application**
   ```bash
   dotnet run
   ```

3. **Access the Application**
   - Navigate to `https://localhost:5001`
   - Start from Dashboard or home page

### Creating Your First Enquiry

1. Go to Enquiry Management (`/ems`)
2. Click "New Enquiry" tab
3. Fill in required fields:
   - Source of Enquiry
   - Enquiry Date & Due Date
   - Enquiry Type
   - Customer Company
   - Client Name
   - Project Name
   - Sales Engineer
   - Details
4. Click "Add" to create

---

## Features Summary

### Current Implementation
✓ Professional light-themed UI
✓ Enquiry creation & management
✓ Customer company management
✓ Contact person management
✓ User/Sales Engineer management
✓ Enquiry item categorization
✓ Search & filter functionality
✓ Dashboard with statistics
✓ Row-Level Security setup
✓ Audit trail infrastructure

### Ready for Extension
- Email notifications (configure SMTP)
- PDF/Excel export (add reporting library)
- Advanced analytics (add charts library)
- User authentication (add Auth0/Supabase Auth)
- Role-based access control (implement policies)
- File uploads (configure Supabase Storage)

---

## Best Practices Implemented

1. **Separation of Concerns**
   - Services for business logic
   - Models for data representation
   - UI components for presentation

2. **Database Security**
   - RLS policies on all tables
   - Foreign key constraints
   - Data integrity checks

3. **User Experience**
   - Responsive design
   - Consistent styling
   - Clear navigation
   - Validation feedback

4. **Code Organization**
   - Modular structure
   - Clear naming conventions
   - Reusable components

5. **Performance**
   - Database indexes
   - Efficient queries
   - Client-side validation
   - Lazy loading ready

---

## Support & Documentation

- **Supabase Docs**: https://supabase.com/docs
- **Blazor Docs**: https://learn.microsoft.com/en-us/aspnet/core/blazor
- **Entity Framework**: https://learn.microsoft.com/en-us/ef/core

---

## License & Credits

Built with modern web technologies and best practices.
Ready for production deployment.

Last Updated: November 16, 2025
