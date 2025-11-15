namespace EnquiryManagementSystem.Data.Models
{
    public class ContactPerson
    {
        public int Id { get; set; } // Primary Key
        public string ContactName { get; set; } = string.Empty;
        public string? Designation { get; set; }
        public string CategoryOfDesignation { get; set; } = "Technical";
        public string Address1 { get; set; } = string.Empty;
        public string? Address2 { get; set; }
        public string? FaxNo { get; set; }
        public string? Phone { get; set; }
        public string? Mobile1 { get; set; }
        public string? Mobile2 { get; set; }
        public string? EmailId { get; set; }

        public int CustomerCompanyId { get; set; }
        public CustomerCompany CustomerCompany { get; set; } = null!;

        public ICollection<Enquiry> Enquiries { get; set; } = new List<Enquiry>();
    }
}
