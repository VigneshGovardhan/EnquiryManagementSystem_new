using System.Collections.Generic;

namespace EnquiryManagementSystem.Data.Models
{
    public class CustomerCompany
    {
        public int Id { get; set; } // Primary Key
        public string Category { get; set; } = "Contractor";
        public string CompanyName { get; set; } = string.Empty;
        public string Address1 { get; set; } = string.Empty;
        public string? Address2 { get; set; }
        public string? Rating { get; set; }
        public string? Type { get; set; }
        public string? FaxNo { get; set; }
        public string Phone1 { get; set; } = string.Empty;
        public string? Phone2 { get; set; }
        public string? MailId { get; set; }
        public string? Website { get; set; }
        public string Status { get; set; } = "Active";

        public ICollection<ContactPerson> ContactPersons { get; set; } = new List<ContactPerson>();
        public ICollection<Enquiry> Enquiries { get; set; } = new List<Enquiry>();
    }
}
