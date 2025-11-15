using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace EnquiryManagementSystem.Data.Models
{
    public class EnquiryForItem
    {
        public int Id { get; set; } // Primary Key
        public string ItemName { get; set; } = string.Empty;
        public string? CompanyName { get; set; }
        public string? DepartmentName { get; set; }
        public string Status { get; set; } = "Active";

        public string CommonMailIds { get; set; } = string.Empty; 
        public string CCMailIds { get; set; } = string.Empty;

        [NotMapped]
        public List<string> CommonMailIdsList
        {
            get { return string.IsNullOrWhiteSpace(CommonMailIds) ? new List<string>() : CommonMailIds.Split(',').ToList(); }
            set { CommonMailIds = string.Join(",", value); }
        }

        [NotMapped]
        public List<string> CCMailIdsList
        {
            get { return string.IsNullOrWhiteSpace(CCMailIds) ? new List<string>() : CCMailIds.Split(',').ToList(); }
            set { CCMailIds = string.Join(",", value); }
        }

        public ICollection<Enquiry> Enquiries { get; set; } = new List<Enquiry>();
    }
}
