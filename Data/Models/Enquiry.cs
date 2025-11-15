using System;
using System.Collections.Generic;

namespace EnquiryManagementSystem.Data.Models
{
    public class Enquiry
    {
        public int Id { get; set; } // Primary Key
        public string RequestNo { get; set; } = string.Empty;
        public string SourceOfInfo { get; set; } = string.Empty;
        public DateTime? EnquiryDate { get; set; }
        public DateTime? DueOn { get; set; }
        public DateTime? SiteVisitDate { get; set; }
        public string ProjectName { get; set; } = string.Empty;
        public string ClientName { get; set; } = string.Empty; 
        public string? ConsultantName { get; set; }
        public string DetailsOfEnquiry { get; set; } = string.Empty;
        public string? DocumentsReceived { get; set; }
        public bool hardcopy { get; set; }
        public bool drawing { get; set; }
        public bool dvd { get; set; }
        public bool spec { get; set; }
        public bool eqpschedule { get; set; }
        public string? Remark { get; set; }
        public bool AutoAck { get; set; }
        public bool ceosign { get; set; }
        public string Status { get; set; } = "Enquiry";

        public string SelectedEnquiryTypes { get; set; } = string.Empty; 

        public ICollection<CustomerCompany> SelectedCustomers { get; set; } = new List<CustomerCompany>();
        public ICollection<ContactPerson> SelectedReceivedFroms { get; set; } = new List<ContactPerson>();
        public ICollection<User> SelectedConcernedSEs { get; set; } = new List<User>();
        public ICollection<EnquiryForItem> SelectedEnquiryFor { get; set; } = new List<EnquiryForItem>();
    }
}
