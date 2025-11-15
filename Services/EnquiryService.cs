using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EnquiryManagementSystem.Data;
using EnquiryManagementSystem.Data.Models;
// This 'using' is important as it finds the 'EnquiryModel' you defined
using static EnquiryManagementSystem.Pages.Ems;

namespace EnquiryManagementSystem.Services
{
    public class EnquiryService
    {
        private readonly ApplicationDbContext _context;

        public EnquiryService(ApplicationDbContext context)
        {
            _context = context;
        }

        // --- LOAD LISTS ---
        public async Task<List<CustomerCompany>> GetActiveCustomersAsync() => await _context.CustomerCompanies.Where(c => c.Status == "Active" && c.Category == "Contractor").ToListAsync();
        public async Task<List<CustomerCompany>> GetActiveClientsAsync() => await _context.CustomerCompanies.Where(c => c.Status == "Active" && c.Category == "Client").ToListAsync();
        public async Task<List<CustomerCompany>> GetActiveConsultantsAsync() => await _context.CustomerCompanies.Where(c => c.Status == "Active" && c.Category == "Consultant").ToListAsync();
        public async Task<List<User>> GetActiveUsersAsync() => await _context.Users.Where(u => u.Status == "Active").ToListAsync();
        public async Task<List<EnquiryForItem>> GetActiveEnquiryForItemsAsync() => await _context.EnquiryForItems.Where(i => i.Status == "Active").ToListAsync();

        public async Task<List<ContactPerson>> GetActiveContactsAsync()
        {
            return await _context.ContactPersons
                .Include(c => c.CustomerCompany)
                .ToListAsync();
        }

        // --- MAIN ENQUIRY CRUD ---

        public async Task<string> CreateEnquiryAsync(EnquiryModel model, List<string> customers, List<string> contacts, List<string> ses, List<string> items, List<string> types)
        {
            // 1. Generate Request No
            var year = DateTime.Now.Year;
            var month = DateTime.Now.Month;
            var count = await _context.Enquiries.CountAsync(e => e.EnquiryDate.HasValue && e.EnquiryDate.Value.Year == year && e.EnquiryDate.Value.Month == month) + 1;
            var reqNo = $"EYS/{year}/{month:D2}/{count:D3}";

            // 2. Map ALL fields from Model to Entity
            var entity = new Enquiry
            {
                RequestNo = reqNo,
                SourceOfInfo = model.SourceOfInfo,
                EnquiryDate = model.EnquiryDate,
                DueOn = model.DueOn,
                SiteVisitDate = model.SiteVisitDate,
                ProjectName = model.ProjectName,
                ClientName = model.ClientName,
                ConsultantName = model.ConsultantName,
                DetailsOfEnquiry = model.DetailsOfEnquiry,
                DocumentsReceived = model.DocumentsReceived,

                // === FIX: Use lowercase properties ===
                hardcopy = model.hardcopy,
                drawing = model.drawing,
                dvd = model.dvd,
                spec = model.spec,
                eqpschedule = model.eqpschedule,

                Remark = model.Remark,
                AutoAck = model.AutoAck,

                // === FIX: Use lowercase property ===
                ceosign = model.ceosign,

                // === FIX: Use lowercase property ===
                Status = model.Status,

                SelectedEnquiryTypes = string.Join(",", types),
            };

            _context.Enquiries.Add(entity);
            await _context.SaveChangesAsync();

            // 3. Handle Relationships (Many-to-Many)
            await UpdateEnquiryRelations(entity, customers, contacts, ses, items);

            return reqNo;
        }

        public async Task UpdateEnquiryAsync(Enquiry existingEntity, EnquiryModel model, List<string> customers, List<string> contacts, List<string> ses, List<string> items, List<string> types)
        {
            // 1. Update scalar fields
            existingEntity.SourceOfInfo = model.SourceOfInfo;
            existingEntity.EnquiryDate = model.EnquiryDate;
            existingEntity.DueOn = model.DueOn;
            existingEntity.SiteVisitDate = model.SiteVisitDate;
            existingEntity.ProjectName = model.ProjectName;
            existingEntity.ClientName = model.ClientName;
            existingEntity.ConsultantName = model.ConsultantName;
            existingEntity.DetailsOfEnquiry = model.DetailsOfEnquiry;
            existingEntity.DocumentsReceived = model.DocumentsReceived;

            // === FIX: Use lowercase properties ===
            existingEntity.hardcopy = model.hardcopy;
            existingEntity.drawing = model.drawing;
            existingEntity.dvd = model.dvd;
            existingEntity.spec = model.spec;
            existingEntity.eqpschedule = model.eqpschedule;

            existingEntity.Remark = model.Remark;
            existingEntity.AutoAck = model.AutoAck;

            // === FIX: Use lowercase property ===
            existingEntity.ceosign = model.ceosign;

            // === FIX: Use lowercase property ===
            existingEntity.Status = model.Status;

            existingEntity.SelectedEnquiryTypes = string.Join(",", types);

            // 2. Update Relationships
            await UpdateEnquiryRelations(existingEntity, customers, contacts, ses, items);

            await _context.SaveChangesAsync();
        }

        private async Task UpdateEnquiryRelations(Enquiry enquiry, List<string> customerNames, List<string> contactKeys, List<string> seNames, List<string> itemNames)
        {
            // Clear existing relations
            enquiry.SelectedCustomers.Clear();
            enquiry.SelectedReceivedFroms.Clear();
            enquiry.SelectedConcernedSEs.Clear();
            enquiry.SelectedEnquiryFor.Clear();

            // Add Customers
            foreach (var name in customerNames)
            {
                var c = await _context.CustomerCompanies.FirstOrDefaultAsync(x => x.CompanyName == name);
                if (c != null) enquiry.SelectedCustomers.Add(c);
            }

            // Add Contacts (Key format: Id|Name|Company)
            foreach (var key in contactKeys)
            {
                var parts = key.Split('|');
                if (parts.Length > 0 && int.TryParse(parts[0], out int id))
                {
                    var c = await _context.ContactPersons.FindAsync(id);
                    if (c != null) enquiry.SelectedReceivedFroms.Add(c);
                }
            }

            // Add SEs
            foreach (var name in seNames)
            {
                var u = await _context.Users.FirstOrDefaultAsync(x => x.FullName == name);
                if (u != null) enquiry.SelectedConcernedSEs.Add(u);
            }

            // Add Enquiry For Items
            foreach (var name in itemNames)
            {
                var i = await _context.EnquiryForItems.FirstOrDefaultAsync(x => x.ItemName == name);
                if (i != null) enquiry.SelectedEnquiryFor.Add(i);
            }

            await _context.SaveChangesAsync();
        }

        public async Task<Enquiry?> GetEnquiryByRequestNoAsync(string reqNo)
        {
            return await _context.Enquiries
                .Include(e => e.SelectedCustomers)
                .Include(e => e.SelectedReceivedFroms)
                    .ThenInclude(cp => cp.CustomerCompany)
                .Include(e => e.SelectedConcernedSEs)
                .Include(e => e.SelectedEnquiryFor)
                .FirstOrDefaultAsync(e => e.RequestNo == reqNo);
        }

        public async Task<List<Enquiry>> SearchEnquiriesAsync(string text, string category, string catValue, DateTime? from, DateTime? to)
        {
            var query = _context.Enquiries
                .Include(e => e.SelectedCustomers)
                .Include(e => e.SelectedConcernedSEs)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(text))
            {
                text = text.ToLower();
                query = query.Where(e =>
                    e.RequestNo.ToLower().Contains(text) ||
                    e.ProjectName.ToLower().Contains(text) ||
                    e.ClientName.ToLower().Contains(text) ||
                    e.SelectedCustomers.Any(c => c.CompanyName.ToLower().Contains(text)) ||
                    e.SelectedConcernedSEs.Any(u => u.FullName.ToLower().Contains(text))
                );
            }

            if (category != "All" && !string.IsNullOrWhiteSpace(catValue))
            {
                if (category == "SourceOfInfo") query = query.Where(e => e.SourceOfInfo == catValue);
                else if (category == "Status") query = query.Where(e => e.Status == catValue);
                else if (category == "EnquiryType") query = query.Where(e => e.SelectedEnquiryTypes.Contains(catValue));
                else if (category == "EnquiryFor") query = query.Where(e => e.SelectedEnquiryFor.Any(i => i.ItemName == catValue));
            }

            if (from.HasValue) query = query.Where(e => e.EnquiryDate >= from.Value);
            if (to.HasValue) query = query.Where(e => e.EnquiryDate <= to.Value);

            return await query.OrderByDescending(e => e.RequestNo).ToListAsync();
        }

        // --- POPUP SAVE METHODS (Ensuring ALL fields are saved) ---

        public async Task<CustomerCompany> AddOrUpdateCustomerAsync(CustomerCompany company)
        {
            var existing = await _context.CustomerCompanies.FindAsync(company.Id);
            if (existing == null)
            {
                _context.CustomerCompanies.Add(company);
            }
            else
            {
                // Map ALL fields
                existing.Category = company.Category;
                existing.CompanyName = company.CompanyName;
                existing.Address1 = company.Address1;
                existing.Address2 = company.Address2;
                existing.Rating = company.Rating;
                existing.Type = company.Type;
                existing.FaxNo = company.FaxNo;
                existing.Phone1 = company.Phone1;
                existing.Phone2 = company.Phone2;
                existing.MailId = company.MailId;
                existing.Website = company.Website;
                existing.Status = company.Status;
            }
            await _context.SaveChangesAsync();
            return company;
        }

        public async Task<ContactPerson> AddOrUpdateContactAsync(ContactPerson contact)
        {
            var existing = await _context.ContactPersons.FindAsync(contact.Id);
            if (existing == null)
            {
                _context.ContactPersons.Add(contact);
            }
            else
            {
                // Map ALL fields
                existing.CustomerCompanyId = contact.CustomerCompanyId;
                existing.ContactName = contact.ContactName;
                existing.Designation = contact.Designation;
                existing.CategoryOfDesignation = contact.CategoryOfDesignation;
                existing.Address1 = contact.Address1;
                existing.Address2 = contact.Address2;
                existing.FaxNo = contact.FaxNo;
                existing.Phone = contact.Phone;
                existing.Mobile1 = contact.Mobile1;
                existing.Mobile2 = contact.Mobile2;
                existing.EmailId = contact.EmailId;
            }
            await _context.SaveChangesAsync();
            return contact;
        }

        public async Task<User> AddOrUpdateUserAsync(User user)
        {
            var existing = await _context.Users.FindAsync(user.Id);
            if (existing == null)
            {
                _context.Users.Add(user);
            }
            else
            {
                // Map ALL fields
                existing.FullName = user.FullName;
                existing.Designation = user.Designation;
                existing.MailId = user.MailId;
                existing.LoginPassword = user.LoginPassword;
                existing.Status = user.Status;
                existing.Department = user.Department;
                existing.Roles = user.Roles; // This is the List<string>
            }
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<EnquiryForItem> AddOrUpdateEnquiryForItemAsync(EnquiryForItem item)
        {
            var existing = await _context.EnquiryForItems.FindAsync(item.Id);
            if (existing == null)
            {
                _context.EnquiryForItems.Add(item);
            }
            else
            {
                // Map ALL fields
                existing.ItemName = item.ItemName;
                existing.CompanyName = item.CompanyName;
                existing.DepartmentName = item.DepartmentName;
                existing.Status = item.Status;
                existing.CommonMailIds = item.CommonMailIds; // List<string>
                existing.CCMailIds = item.CCMailIds; // List<string>
            }
            await _context.SaveChangesAsync();
            return item;
        }
    }
}