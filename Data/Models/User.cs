using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace EnquiryManagementSystem.Data.Models
{
    public class User
    {
        public int Id { get; set; } // Primary Key
        public string FullName { get; set; } = string.Empty;
        public string? Designation { get; set; }
        public string MailId { get; set; } = string.Empty;
        public string LoginPassword { get; set; } = "password123";
        public string Status { get; set; } = "Active";
        public string Department { get; set; } = "MEP";
        public string Roles { get; set; } = string.Empty;

        [NotMapped]
        public List<string> RolesList
        {
            get { return string.IsNullOrWhiteSpace(Roles) ? new List<string>() : Roles.Split(',').ToList(); }
            set { Roles = string.Join(",", value); }
        }

        public ICollection<Enquiry> Enquiries { get; set; } = new List<Enquiry>();
    }
}
