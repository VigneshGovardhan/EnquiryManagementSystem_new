# Quick Start Guide - Enquiry Management System

## Prerequisites

- **.NET 8.0 SDK** or later
- **Supabase Account** (already connected)
- **Web Browser** (Chrome, Firefox, Safari, Edge)
- **Text Editor** or Visual Studio

---

## 1. Configuration

### Step 1: Update Supabase Credentials

Edit `appsettings.json`:

```json
{
  "Supabase": {
    "Url": "https://your-project-id.supabase.co",
    "AnonKey": "your-anon-key-here"
  }
}
```

Get your credentials from:
1. Go to Supabase Dashboard
2. Settings → API
3. Copy Project URL and anon/public key

### Step 2: Update .env File (Optional)

```
SUPABASE_URL=https://your-project-id.supabase.co
SUPABASE_ANON_KEY=your-anon-key-here
```

---

## 2. Running the Application

### Option A: Command Line

```bash
# Navigate to project directory
cd /tmp/cc-agent/60238150/project

# Restore packages
dotnet restore

# Run the application
dotnet run

# Access the application
# Open browser: https://localhost:5001
```

### Option B: Visual Studio

1. Open `EnquiryManagementSystem.sln`
2. Right-click project → Set as Startup Project
3. Press F5 to run
4. Application opens in default browser

### Option C: Visual Studio Code

```bash
# Install C# extension if not already installed
# Open project folder
# Open terminal (Ctrl + `)
# Run: dotnet run
```

---

## 3. First Time Setup

### Create Initial Data

1. **Navigate to Enquiry Management** (`/ems`)
2. **Add Customer Company**:
   - Click "New" button in Customer section
   - Fill in company details
   - Save
3. **Add Contact Person**:
   - Click "New" button in Contact section
   - Fill in contact details
   - Save
4. **Add Sales Engineer**:
   - Click "New" button in SE section
   - Fill in user details (password: auto-generated)
   - Save

### Create Your First Enquiry

1. **Go to New Enquiry Tab**
2. **Fill Required Fields**:
   - Source of Enquiry: Email
   - Enquiry Date: Today
   - Due Date: 30 days from today
   - Enquiry Type: Select one
   - Customer: Select from list
   - Client Name: Enter name
   - Project Name: Enter project
   - Sales Engineer: Assign SE
   - Details: Enter enquiry details
3. **Click "Add" Button**
4. **System generates** Request No automatically

---

## 4. Key Navigation

### Home Page `/`
- Landing page with feature overview
- Quick links to Dashboard and Enquiry Management

### Dashboard `/dashboard`
- Overview of all business metrics
- Recent enquiries list
- Top customers list
- Sales engineers performance
- Statistics and trends

### Enquiry Management `/ems`

#### New Enquiry Tab
- Create new enquiries
- Full form with validation
- Auto-suggestions for common fields
- Multi-select for customers and contacts

#### Modify Enquiry Tab
- Search existing enquiries by Request No
- Load and edit enquiry details
- Save changes with audit trail

#### Search Enquiry Tab
- Advanced search functionality
- Filter by text, dates
- View results in table format
- Quick open to modify

---

## 5. Common Tasks

### Creating a New Enquiry

1. Go to `/ems` → New Enquiry tab
2. Select source and dates
3. Choose enquiry type
4. Select customer and contacts
5. Enter project and client details
6. Assign sales engineer
7. Add details and select documents
8. Click "Add"

### Searching Enquiries

1. Go to `/ems` → Search tab
2. Enter search text (Request No, Client, Project)
3. Optionally set date range
4. Click "Search"
5. Click "Open" to modify an enquiry

### Managing Master Data

Access via modals in Enquiry Management:

- **Customers**: Add/Edit contractors, clients, consultants
- **Contacts**: Manage people associated with companies
- **Users**: Create sales engineers and assign roles
- **Items**: Define enquiry categories

---

## 6. Database Verification

### Check Data in Supabase

1. Go to Supabase Dashboard
2. Select your project
3. Click "Table Editor"
4. View tables:
   - `customer_companies` - Your companies
   - `contact_persons` - Contact details
   - `users` - Sales engineers
   - `enquiries` - Your enquiries
   - `enquiry_audit_log` - Change history

### Run Sample Queries

```sql
-- Count total enquiries
SELECT COUNT(*) FROM enquiries;

-- Get recent enquiries
SELECT request_no, client_name, enquiry_date
FROM enquiries
ORDER BY enquiry_date DESC LIMIT 10;

-- Get audit log for specific enquiry
SELECT * FROM enquiry_audit_log
WHERE enquiry_id = 'your-enquiry-id'
ORDER BY created_at DESC;
```

---

## 7. Troubleshooting

### Application Won't Start

**Error**: Connection to database failed
- **Solution**: Check Supabase URL and API key in appsettings.json

**Error**: Port 5001 already in use
- **Solution**: Run on different port:
  ```bash
  dotnet run --urls "https://localhost:5002"
  ```

### Data Not Saving

**Error**: 401 Unauthorized
- **Solution**: Verify your Supabase anon key is correct

**Error**: Network timeout
- **Solution**: Check internet connection and Supabase server status

### Forms Not Validating

**Issue**: Required fields not showing errors
- **Solution**: Ensure JavaScript is enabled in browser

---

## 8. Development Tips

### Adding New Fields

1. Add column to Supabase table
2. Update C# model class
3. Add to form UI
4. Update validation

### Customizing Styles

Edit `wwwroot/css/site.css`:
- Colors: Modify `:root` variables
- Spacing: Adjust margin/padding utilities
- Fonts: Change font-family

### Extending Functionality

1. Add new services in `Services/` folder
2. Add new pages in `Pages/` folder
3. Create reusable components in `Shared/` folder
4. Keep components focused and testable

---

## 9. Performance Optimization

### Caching Strategy
- Enable output caching for static pages
- Cache dropdown lists in memory

### Database Optimization
- Use indexes for frequently searched fields
- Implement pagination for large result sets

### Browser Optimization
- Enable browser caching
- Minify CSS/JS for production
- Use CDN for static assets

---

## 10. Deployment

### Prepare for Deployment

1. **Build Release Version**:
   ```bash
   dotnet publish -c Release
   ```

2. **Update Configuration**:
   - Set production Supabase URL
   - Configure proper API keys
   - Enable HTTPS only

3. **Deploy to Server**:
   - Azure App Service
   - AWS EC2
   - DigitalOcean
   - Self-hosted server

### Production Checklist

- [ ] Supabase credentials updated for production
- [ ] HTTPS enabled
- [ ] Database backups configured
- [ ] Monitoring and logging enabled
- [ ] Error handling implemented
- [ ] Performance tested under load
- [ ] Security audit completed
- [ ] Documentation updated

---

## 11. Support Resources

- **Supabase Documentation**: https://supabase.com/docs
- **Blazor Documentation**: https://learn.microsoft.com/en-us/aspnet/core/blazor
- **Entity Framework Core**: https://learn.microsoft.com/en-us/ef/core
- **PostgreSQL Docs**: https://www.postgresql.org/docs

---

## Next Steps

1. ✓ Confirm Supabase connection
2. ✓ Add test data
3. ✓ Create sample enquiries
4. ✓ Test search functionality
5. ✓ Review dashboard metrics
6. ⬜ Implement email notifications
7. ⬜ Add PDF export feature
8. ⬜ Set up user authentication
9. ⬜ Deploy to production

---

**Happy Enquiry Management! 🚀**
