using Microsoft.EntityFrameworkCore;
using EnquiryManagementSystem.Data.Models;
using System;
using System.Collections.Generic;

namespace EnquiryManagementSystem.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Enquiry> Enquiries { get; set; } = null!;
        public DbSet<CustomerCompany> CustomerCompanies { get; set; } = null!;
        public DbSet<ContactPerson> ContactPersons { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<EnquiryForItem> EnquiryForItems { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ========================= RELATIONSHIPS =========================

            modelBuilder.Entity<ContactPerson>()
                .HasOne(cp => cp.CustomerCompany)
                .WithMany(cc => cc.ContactPersons)
                .HasForeignKey(cp => cp.CustomerCompanyId);

            // ========================= SEED PRINCIPAL TABLES =========================

            modelBuilder.Entity<CustomerCompany>().HasData(
                new CustomerCompany { Id = 1, CompanyName = "Customer X Ltd", Category = "Contractor", Status = "Active", Address1 = "123 Main St", Phone1 = "222" },
                new CustomerCompany { Id = 2, CompanyName = "Customer Y Corp", Category = "Contractor", Status = "Active", Address1 = "456 Oak Ave", Phone1 = "555" },
                new CustomerCompany { Id = 3, CompanyName = "Client Z Inc", Category = "Client", Status = "Active", Address1 = "789 Pine Rd", Phone1 = "888" },
                new CustomerCompany { Id = 4, CompanyName = "Consultant A", Category = "Consultant", Status = "Active", Address1 = "101 Elm Blvd", Phone1 = "000" }
            );

            modelBuilder.Entity<ContactPerson>().HasData(
                new { Id = 1, ContactName = "Velu", CustomerCompanyId = 1, EmailId = "pa@custx.com", Designation = "Manager", Address1 = "123 Main St", Mobile1 = "333", CategoryOfDesignation = "Technical", Phone = (string?)null, Mobile2 = (string?)null, FaxNo = (string?)null, Address2 = (string?)null },
                new { Id = 2, ContactName = "Vijay", CustomerCompanyId = 2, EmailId = "pb@custy.com", Designation = "Director", Address1 = "456 Oak Ave", Mobile1 = "666", CategoryOfDesignation = "Technical", Phone = (string?)null, Mobile2 = (string?)null, FaxNo = (string?)null, Address2 = (string?)null },
                new { Id = 3, ContactName = "Seema", CustomerCompanyId = 1, EmailId = "sc@custx.com", Designation = "Engineer", Address1 = "123 Main St", Mobile1 = "333", CategoryOfDesignation = "Technical", Phone = (string?)null, Mobile2 = (string?)null, FaxNo = (string?)null, Address2 = (string?)null },
                new { Id = 4, ContactName = "Person C - Engineer", CustomerCompanyId = 3, EmailId = "pc@clientz.com", Designation = "Engineer", Address1 = "789 Pine Rd", Mobile1 = "999", CategoryOfDesignation = "Technical", Phone = (string?)null, Mobile2 = (string?)null, FaxNo = (string?)null, Address2 = (string?)null }
            );

            modelBuilder.Entity<User>().HasData(
                new { Id = 1, FullName = "SE1 - John Doe", Designation = "Sales Engineer", MailId = "se1@comp.com", Status = "Active", Roles = "Enquiry,Quotation", LoginPassword = "123", Department = "MEP" },
                new { Id = 2, FullName = "SE2 - Jane Smith", Designation = "Sales Manager", MailId = "se2@comp.com", Status = "Active", Roles = "Enquiry,Admin", LoginPassword = "123", Department = "MEP" }
            );

            modelBuilder.Entity<EnquiryForItem>().HasData(
                new { Id = 1, ItemName = "Electrical", DepartmentName = "Elect", CommonMailIds = "elect_common@a.com", Status = "Active", CCMailIds = "" },
                new { Id = 2, ItemName = "Mechanical", DepartmentName = "Mech", Status = "Active", CommonMailIds = "", CCMailIds = "mech_cc1@b.com" }
            );

            modelBuilder.Entity<Enquiry>().HasData(
                new Enquiry
                {
                    Id = 1,
                    RequestNo = "EYS/2025/11/001",
                    SourceOfInfo = "Phone",
                    EnquiryDate = new DateTime(2025, 11, 12),
                    DueOn = new DateTime(2025, 11, 19),
                    SelectedEnquiryTypes = "Re-Tender",
                    ProjectName = "Project Alpha",
                    ClientName = "Client Z Inc",
                    ConsultantName = "Consultant A",
                    DetailsOfEnquiry = "Initial seeded enquiry",
                    hardcopy = true,
                    drawing = true,
                    dvd = false,
                    spec = false,
                    eqpschedule = false,
                    Status = "Enquiry",
                    DocumentsReceived = "",
                    Remark = ""
                }
            );

            // ========================= MANY-TO-MANY SHADOW TABLES =========================

            modelBuilder.Entity("EnquiryCustomerCompany", eb =>
            {
                eb.Property<int>("EnquiriesId");
                eb.Property<int>("SelectedCustomersId");
                eb.HasKey("EnquiriesId", "SelectedCustomersId");
                eb.HasData(new { EnquiriesId = 1, SelectedCustomersId = 1 });
            });

            modelBuilder.Entity("EnquiryContactPerson", eb =>
            {
                eb.Property<int>("EnquiriesId");
                eb.Property<int>("SelectedReceivedFromsId");
                eb.HasKey("EnquiriesId", "SelectedReceivedFromsId");
                eb.HasData(new { EnquiriesId = 1, SelectedReceivedFromsId = 3 });
            });

            modelBuilder.Entity("EnquiryUser", eb =>
            {
                eb.Property<int>("EnquiriesId");
                eb.Property<int>("SelectedConcernedSEsId");
                eb.HasKey("EnquiriesId", "SelectedConcernedSEsId");
                eb.HasData(new { EnquiriesId = 1, SelectedConcernedSEsId = 2 });
            });

            modelBuilder.Entity("EnquiryEnquiryForItem", eb =>
            {
                eb.Property<int>("EnquiriesId");
                eb.Property<int>("SelectedEnquiryForId");
                eb.HasKey("EnquiriesId", "SelectedEnquiryForId");
                eb.HasData(new { EnquiriesId = 1, SelectedEnquiryForId = 1 });
            });
        }
    }
}
