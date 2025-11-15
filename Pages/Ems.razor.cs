using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;

namespace EnquiryManagementSystem.Pages
{
    public partial class Ems
    {
        [Inject]
        private IJSRuntime JSRuntime { get; set; } = null!;

#nullable enable

        // ---------- MODELS ----------
        public class EnquiryModel
        {
            public string SourceOfInfo { get; set; } = string.Empty;
            public DateTime? EnquiryDate { get; set; } = DateTime.Now.Date;
            public DateTime? DueOn { get; set; } = null;
            public DateTime? SiteVisitDate { get; set; } = null;
            public string ProjectName { get; set; } = string.Empty;
            public string ClientName { get; set; } = string.Empty;
            public string ConsultantName { get; set; } = string.Empty;
            public string DetailsOfEnquiry { get; set; } = string.Empty;
            public string DocumentsReceived { get; set; } = string.Empty;
            public bool hardcopy { get; set; } = false;
            public bool drawing { get; set; } = false;
            public bool dvd { get; set; } = false;
            public bool spec { get; set; } = false;
            public bool eqpschedule { get; set; } = false;
            public string Remark { get; set; } = string.Empty;
            public bool AutoAck { get; set; } = true;
            public bool ceosign { get; set; } = false;
            public string Status { get; set; } = "Enquiry";
            public List<string> SelectedEnquiryTypes { get; set; } = new();
            public List<string> SelectedEnquiryFor { get; set; } = new();
            public List<string> SelectedCustomers { get; set; } = new();
            public List<string> SelectedReceivedFroms { get; set; } = new();
            public List<string> SelectedConcernedSEs { get; set; } = new();
        }

        public class ContactPersonModel
        {
            public string Category { get; set; } = "Contractor";
            public string CompanyName { get; set; } = string.Empty;
            public string ContactName { get; set; } = string.Empty;
            public string Designation { get; set; } = string.Empty;
            public string CategoryOfDesignation { get; set; } = "Technical";
            public string Address1 { get; set; } = string.Empty;
            public string Address2 { get; set; } = string.Empty;
            public string FaxNo { get; set; } = string.Empty;
            public string Phone { get; set; } = string.Empty;
            public string Mobile1 { get; set; } = string.Empty;
            public string Mobile2 { get; set; } = string.Empty;
            public string EmailId { get; set; } = string.Empty;
            public string UniqueKey => $"{ContactName}|{CompanyName}";
            public static ContactPersonModel Clone(ContactPersonModel o) => new()
            {
                Category = o.Category,
                CompanyName = o.CompanyName,
                ContactName = o.ContactName,
                Designation = o.Designation,
                CategoryOfDesignation = o.CategoryOfDesignation,
                Address1 = o.Address1,
                Address2 = o.Address2,
                FaxNo = o.FaxNo,
                Phone = o.Phone,
                Mobile1 = o.Mobile1,
                Mobile2 = o.Mobile2,
                EmailId = o.EmailId
            };
        }

        public class CustomerCompanyModel
        {
            public string Category { get; set; } = "Contractor";
            public string CompanyName { get; set; } = string.Empty;
            public string Address1 { get; set; } = string.Empty;
            public string Address2 { get; set; } = string.Empty;
            public string Rating { get; set; } = string.Empty;
            public string Type { get; set; } = string.Empty;
            public string FaxNo { get; set; } = string.Empty;
            public string Phone1 { get; set; } = string.Empty;
            public string Phone2 { get; set; } = string.Empty;
            public string MailId { get; set; } = string.Empty;
            public string Website { get; set; } = string.Empty;
            public string Status { get; set; } = "Active";
            public static CustomerCompanyModel Clone(CustomerCompanyModel o) => new()
            {
                Category = o.Category,
                CompanyName = o.CompanyName,
                Address1 = o.Address1,
                Address2 = o.Address2,
                Rating = o.Rating,
                Type = o.Type,
                FaxNo = o.FaxNo,
                Phone1 = o.Phone1,
                Phone2 = o.Phone2,
                MailId = o.MailId,
                Website = o.Website,
                Status = o.Status
            };
            public static CustomerCompanyModel FromContact(ContactPersonModel c) => new()
            {
                Category = c.Category,
                CompanyName = c.CompanyName,
                Address1 = c.Address1,
                Address2 = c.Address2,
                Phone1 = c.Phone,
                MailId = c.EmailId,
                Status = "Active"
            };
        }

        public class UserModel
        {
            public string FullName { get; set; } = string.Empty;
            public string Designation { get; set; } = string.Empty;
            public string MailId { get; set; } = string.Empty;
            public string LoginPassword { get; set; } = string.Empty;
            public string Status { get; set; } = "Active";
            public string Department { get; set; } = "MEP";
            public List<string> Roles { get; set; } = new();
            public static UserModel Clone(UserModel o) => new()
            {
                FullName = o.FullName,
                Designation = o.Designation,
                MailId = o.MailId,
                LoginPassword = o.LoginPassword,
                Status = o.Status,
                Department = o.Department,
                Roles = o.Roles.ToList()
            };
        }

        public class EnquiryForItemModel
        {
            public string ItemName { get; set; } = string.Empty;
            public string CompanyName { get; set; } = string.Empty;
            public string DepartmentName { get; set; } = string.Empty;
            public string Status { get; set; } = "Active";
            public List<string> CommonMailIds { get; set; } = new();
            public List<string> CCMailIds { get; set; } = new();
            public static EnquiryForItemModel Clone(EnquiryForItemModel o) => new()
            {
                ItemName = o.ItemName,
                CompanyName = o.CompanyName,
                DepartmentName = o.DepartmentName,
                Status = o.Status,
                CommonMailIds = o.CommonMailIds.ToList(),
                CCMailIds = o.CCMailIds.ToList()
            };
        }

        public class EnquirySnapshot
        {
            public string RequestNo { get; set; } = string.Empty;
            public string SourceOfInfo { get; set; } = string.Empty;
            public DateTime? EnquiryDate { get; set; }
            public DateTime? DueOn { get; set; }
            public DateTime? SiteVisitDate { get; set; }
            public string ProjectName { get; set; } = string.Empty;
            public string ClientName { get; set; } = string.Empty;
            public string ConsultantName { get; set; } = string.Empty;
            public string DetailsOfEnquiry { get; set; } = string.Empty;
            public string DocumentsReceived { get; set; } = string.Empty;
            public bool hardcopy { get; set; }
            public bool drawing { get; set; }
            public bool dvd { get; set; }
            public bool spec { get; set; }
            public bool eqpschedule { get; set; }
            public string Remark { get; set; } = string.Empty;
            public bool AutoAck { get; set; }
            public bool ceosign { get; set; }
            public List<string> SelectedEnquiryTypes { get; set; } = new();
            public List<string> SelectedEnquiryFor { get; set; } = new();
            public List<string> SelectedCustomers { get; set; } = new();
            public List<string> SelectedReceivedFroms { get; set; } = new();
            public List<string> SelectedConcernedSEs { get; set; } = new();
        }

        // ---------- CONSTANTS / STATE ----------
        private const char CONTACT_DELIMITER = '|';
        private const string NO_DATA_FOUND = "No data found, please use 'New' button to add.";
        private Dictionary<string, string> inputErrors = new();

        private EnquiryModel enquiryModel = new();
        private CustomerCompanyModel newCustomerCompany = new();
        private CustomerCompanyModel? companyToEdit = null;
        private ContactPersonModel modalContactPerson = new();
        private ContactPersonModel? contactToEdit = null;
        private UserModel newUserModel = new();
        private UserModel? userToEdit = null;
        private EnquiryForItemModel newEnquiryForItem = new();
        private EnquiryForItemModel? enqItemToEdit = null;

        private string activeTab = "New";
        private string GetTabClass(string tab) => $"nav-link {(activeTab == tab ? "active" : "")}";
        private void SwitchTab(string tab) { activeTab = tab; StateHasChanged(); }
        private void OnClickTabNew() => SwitchTab("New");
        private void OnClickTabModify() => SwitchTab("Modify");
        private void OnClickTabSearch() => SwitchTab("Search");

        // in-memory "DB"
        private Dictionary<string, EnquirySnapshot> enquiriesDb = new();

        // lists
        private List<string> sourceOfInfos = new() {
            "Email","Phone","Tender Board","Customer Visit","Cold visit by us","Website","Fax","Thru top management","News Paper"
        };
        private List<string> enquirytype = new() {
            "New Tender","Re-Tender","Job in hand","Variation / Change order","Supply only","Maintenance","Retrofit",
            "Upgradation","Refurbishment","Service","Hiring","Renting","Facility Management","Demo"
        };
        private List<string> consultantTypeOptions = new() {
            "MEP","HVAC","Electrical","Plumbing","Fire Fighting","BMS","ELV","Civil","Piling","Scaffolding",
            "Cleaning","Security","Maintenance","Transport","Interior","Landscape","Carpentry","Aluminium","Real estate","Facility Management"
        };

        private List<string> availableRoles = new() { "Enquiry", "Quotation", "Sales", "Admin" };
        private List<string> projectNames = new() { "Project Alpha", "Project Beta" };
        private List<string> existingCustomers = new();
        private List<string> clientNames = new();
        private List<string> consultantNames = new();
        private List<string> concernedSEs = new();
        private List<string> enquiryfor = new();
        private List<ContactPersonModel> allReceivedFroms = new();
        private List<ContactPersonModel> filteredReceivedFroms = new();

        // simulated master
        private List<UserModel> storedUsers = new() {
            new UserModel { FullName = "SE1 - John Doe", Designation = "Sales Engineer", MailId = "se1@comp.com", Status = "Active", Roles = new() { "Enquiry", "Quotation" } },
            new UserModel { FullName = "SE2 - Jane Smith", Designation = "Sales Manager", MailId = "se2@comp.com", Status = "Active", Roles = new() { "Enquiry", "Admin" } },
        };
        private List<ContactPersonModel> storedContacts = new() {
            new ContactPersonModel { ContactName = "Velu", CompanyName = "Customer X Ltd", EmailId = "pa@custx.com", Category = "Contractor", Designation = "Manager", Address1 = "123 Main St", Mobile1 = "333" },
            new ContactPersonModel { ContactName = "Vijay", CompanyName = "Customer Y Corp", EmailId = "pb@custy.com", Category = "Contractor", Designation = "Director", Address1 = "456 Oak Ave", Mobile1 = "666" },
            new ContactPersonModel { ContactName = "Seema", CompanyName = "Customer X Ltd", EmailId = "sc@custx.com", Category = "Contractor", Designation = "Engineer", Address1 = "123 Main St", Mobile1 = "333" },
            new ContactPersonModel { ContactName = "Person C - Engineer", CompanyName = "Client Z Inc", EmailId = "pc@clientz.com", Category = "Client", Designation = "Engineer", Address1 = "789 Pine Rd", Mobile1 = "999" }
        };
        private List<CustomerCompanyModel> storedCustomers = new() {
            new CustomerCompanyModel { CompanyName = "Customer X Ltd", Category = "Contractor", Status = "Active", Address1 = "123 Main St", Phone1 = "222" },
            new CustomerCompanyModel { CompanyName = "Customer Y Corp", Category = "Contractor", Status = "Active", Address1 = "456 Oak Ave", Phone1 = "555" },
            new CustomerCompanyModel { CompanyName = "Client Z Inc", Category = "Client", Status = "Active", Address1 = "789 Pine Rd", Phone1 = "888" },
            new CustomerCompanyModel { CompanyName = "Consultant A", Category = "Consultant", Status = "Active", Address1 = "101 Elm Blvd", Phone1 = "000" }
        };
        private List<EnquiryForItemModel> storedEnqItems = new() {
            new EnquiryForItemModel { ItemName = "Electrical", DepartmentName = "Elect", CommonMailIds = new() { "elect_common@a.com" }, Status = "Active" },
            new EnquiryForItemModel { ItemName = "Mechanical", DepartmentName = "Mech", CCMailIds = new() { "mech_cc1@b.com" }, Status = "Active" }
        };

        // tracking
        private string? selectedSE;
        private List<string> seListBox = new();
        private string? selectedenqtype;
        private List<string> enqtypelistbox = new();
        private string? selectedenqfor;
        private List<string> enqforlistbox = new();

        // --- NEW ---
        private string? selectedCustomerInput; // For the customer dropdown
        private string? selectedReceivedContact; // key "Contact|Company"
        private List<string> listBoxCustomers = new();
        private List<string> receivedContactListBox = new();

        private List<string> selectedCustomerListboxItems = new();
        private List<string> selectedReceivedFromListboxItems = new();
        private List<string> selectedSEListboxItems = new();
        private List<string> selectedEnqTypeListboxItems = new();
        private List<string> selectedEnqForListboxItems = new();
        private string newRoleEntry = string.Empty;
        private string newCommonMailEntry = string.Empty;
        private string newCCMailEntry = string.Empty;
        private List<string> selectedRoleListboxItems = new();
        private List<string> selectedCommonMailListboxItems = new();
        private List<string> selectedCCMailListboxItems = new();

        // --- REMOVED ---
        // private string customerAutosuggest = string.Empty;
        // private string receivedAutosuggest = string.Empty;

        private bool showCustomerModal = false;
        private bool showContactModal = false;
        private bool showUserModal = false;
        private bool showEnqForModal = false;
        private string modalMode = "Add";

        private bool HasAnyContactSelection =>
            !string.IsNullOrWhiteSpace(selectedReceivedContact) || selectedReceivedFromListboxItems.Count > 0;

        // ---------- LIFECYCLE ----------
        protected override void OnInitialized()
        {
            UpdateAllListsFromStorage();
            UpdateReceivedFromList(); // Load all contacts initially
        }

        private void UpdateAllListsFromStorage()
        {
            existingCustomers = storedCustomers.Where(c => c.Category == "Contractor" && c.Status == "Active").Select(c => c.CompanyName).Distinct().ToList();
            clientNames = storedCustomers.Where(c => c.Category == "Client" && c.Status == "Active").Select(c => c.CompanyName).Distinct().ToList();
            consultantNames = storedCustomers.Where(c => c.Category == "Consultant" && c.Status == "Active").Select(c => c.CompanyName).Distinct().ToList();
            concernedSEs = storedUsers.Where(u => u.Status == "Active").Select(u => u.FullName).Distinct().ToList();
            enquiryfor = storedEnqItems.Where(i => i.Status == "Active").Select(i => i.ItemName).Distinct().ToList();
            allReceivedFroms = storedContacts.ToList();
            StateHasChanged();
        }

        // ---------- FILTER CONTACTS (NEW LOGIC) ----------
        private void UpdateReceivedFromList()
        {
            // If customers ARE selected in the listbox, filter by them.
            if (listBoxCustomers.Count > 0)
            {
                var selectedCompanies = listBoxCustomers.ToHashSet(StringComparer.OrdinalIgnoreCase);
                filteredReceivedFroms = allReceivedFroms
                    .Where(contact => contact.Category == "Contractor" && selectedCompanies.Contains(contact.CompanyName.Trim()))
                    .OrderBy(c => c.CompanyName).ThenBy(c => c.ContactName)
                    .ToList();
            }
            // If NO customers are selected, show ALL contacts.
            else
            {
                filteredReceivedFroms = allReceivedFroms
                    .Where(c => c.Category == "Contractor")
                    .OrderBy(c => c.CompanyName).ThenBy(c => c.ContactName)
                    .ToList();
            }

            // Clear selection if it's no longer in the filtered list
            if (!string.IsNullOrWhiteSpace(selectedReceivedContact) && !filteredReceivedFroms.Any(c => c.UniqueKey.Equals(selectedReceivedContact, StringComparison.OrdinalIgnoreCase)))
            {
                selectedReceivedContact = null;
            }
            StateHasChanged();
        }

        // ---------- AUTOSUGGEST HANDLERS (REMOVED/REPLACED) ----------
        // --- These are no longer used by the new dropdowns ---
        // private void OnCustomerAutosuggestBlur() { ... }
        // private void OnReceivedAutosuggestBlur() { ... }

        // ---------- FIELD BLUR HELPERS ----------
        private void OnEnquiryForBlur()
        {
            ValidateControlledInput(selectedenqfor, enquiryfor, "Enquiryfor");
            StateHasChanged();
        }
        private void OnProjectNameBlur()
        {
            ValidateControlledInput(enquiryModel.ProjectName, projectNames, "ProjectName");
            StateHasChanged();
        }
        private void OnClientNameBlur()
        {
            ValidateControlledInput(enquiryModel.ClientName, clientNames, "ClientName");
            StateHasChanged();
        }
        private void OnConsultantNameBlur()
        {
            ValidateControlledInput(enquiryModel.ConsultantName, consultantNames, "ConsultantName");
            StateHasChanged();
        }
        private void OnConcernedSEBlur()
        {
            ValidateControlledInput(selectedSE, concernedSEs, "ConcernedSE");
            StateHasChanged();
        }

        // ---------- LISTBOX HANDLERS ----------
        private List<string> GetSelectedItemsFromChange(ChangeEventArgs e)
            => (e.Value as string[] ?? Array.Empty<string>()).ToList();

        private void HandleCustomerListBoxSelection(ChangeEventArgs e) => selectedCustomerListboxItems = GetSelectedItemsFromChange(e);
        private void HandleReceivedFromListBoxSelection(ChangeEventArgs e) => selectedReceivedFromListboxItems = GetSelectedItemsFromChange(e);
        private void HandleSEListBoxSelection(ChangeEventArgs e) => selectedSEListboxItems = GetSelectedItemsFromChange(e);
        private void HandleEnqTypeListBoxSelection(ChangeEventArgs e) => selectedEnqTypeListboxItems = GetSelectedItemsFromChange(e);
        private void HandleEnqForListBoxSelection(ChangeEventArgs e) => selectedEnqForListboxItems = GetSelectedItemsFromChange(e);
        private void HandleRoleListBoxSelection(ChangeEventArgs e) => selectedRoleListboxItems = GetSelectedItemsFromChange(e);
        private void HandleCommonMailListBoxSelection(ChangeEventArgs e) => selectedCommonMailListboxItems = GetSelectedItemsFromChange(e);
        private void HandleCCMailListBoxSelection(ChangeEventArgs e) => selectedCCMailListboxItems = GetSelectedItemsFromChange(e);

        // --- NEW/MODIFIED LISTBOX ADD/REMOVE LOGIC ---
        private void AddCustomerToListBox()
        {
            if (!string.IsNullOrWhiteSpace(selectedCustomerInput) && !listBoxCustomers.Contains(selectedCustomerInput, StringComparer.OrdinalIgnoreCase))
            {
                listBoxCustomers.Add(selectedCustomerInput);
                inputErrors.Remove("CustomerName");
                selectedCustomerInput = null; // Clear dropdown
                UpdateReceivedFromList(); // Re-filter contacts
                StateHasChanged();
            }
        }

        private void RemoveCustomerFromListBox()
        {
            foreach (var item in selectedCustomerListboxItems.ToList())
            {
                listBoxCustomers.Remove(item);
            }
            selectedCustomerListboxItems.Clear();
            UpdateReceivedFromList(); // Re-filter contacts
            StateHasChanged();
        }


        private void AddSelectedContactToListBox()
        {
            if (string.IsNullOrWhiteSpace(selectedReceivedContact)) return;

            // selectedReceivedContact is the "Contact|Company" key
            if (!receivedContactListBox.Contains(selectedReceivedContact, StringComparer.OrdinalIgnoreCase))
            {
                receivedContactListBox.Add(selectedReceivedContact);

                // Now, also add the company
                var parts = selectedReceivedContact.Split(CONTACT_DELIMITER);
                if (parts.Length == 2)
                {
                    var companyName = parts[1];
                    if (!listBoxCustomers.Contains(companyName, StringComparer.OrdinalIgnoreCase))
                    {
                        listBoxCustomers.Add(companyName);
                        // This will auto-filter the contact list, but it's fine
                        UpdateReceivedFromList();
                    }
                }
            }

            inputErrors.Remove("ReceivedFrom");
            selectedReceivedContact = null; // Clear dropdown
            StateHasChanged();
        }

        private void RemoveSelectedContactFromListBox()
        {
            foreach (var itemKey in selectedReceivedFromListboxItems.ToList())
            {
                receivedContactListBox.Remove(itemKey);

                var parts = itemKey.Split(CONTACT_DELIMITER);
                if (parts.Length == 2)
                {
                    string companyToRemove = parts[1].Trim();
                    bool companyStillHasContacts = receivedContactListBox
                        .Any(p => p.EndsWith($"{CONTACT_DELIMITER}{companyToRemove}", StringComparison.OrdinalIgnoreCase));

                    if (!companyStillHasContacts)
                    {
                        listBoxCustomers.RemoveAll(c => c.Equals(companyToRemove, StringComparison.OrdinalIgnoreCase));
                    }
                }
            }
            selectedReceivedFromListboxItems.Clear();
            UpdateReceivedFromList();
            StateHasChanged();
        }

        private void AddToListBoxenqtype()
        {
            if (!string.IsNullOrWhiteSpace(selectedenqtype) && !enqtypelistbox.Contains(selectedenqtype.Trim(), StringComparer.OrdinalIgnoreCase))
            {
                enqtypelistbox.Add(selectedenqtype.Trim());
                inputErrors.Remove("Enquirytype");
                selectedenqtype = null;
            }
            StateHasChanged();
        }
        private void RemoveFromListBoxenqtype()
        {
            foreach (var item in selectedEnqTypeListboxItems.ToList())
            {
                enqtypelistbox.Remove(item);
            }
            selectedEnqTypeListboxItems.Clear();
            StateHasChanged();
        }
        private void AddToListBoxenqfor()
        {
            if (ValidateControlledInput(selectedenqfor, enquiryfor, "Enquiryfor"))
            {
                if (!string.IsNullOrWhiteSpace(selectedenqfor) && !enqforlistbox.Contains(selectedenqfor.Trim(), StringComparer.OrdinalIgnoreCase))
                {
                    enqforlistbox.Add(selectedenqfor.Trim());
                    inputErrors.Remove("Enquiryfor");
                    selectedenqfor = null;
                }
            }
            StateHasChanged();
        }
        private void RemoveFromListBoxenqfor()
        {
            foreach (var item in selectedEnqForListboxItems.ToList())
            {
                enqforlistbox.Remove(item);
            }
            selectedEnqForListboxItems.Clear();
            StateHasChanged();
        }
        private void AddSEToListBoxToselectedSE()
        {
            if (ValidateControlledInput(selectedSE, concernedSEs, "ConcernedSE"))
            {
                if (!string.IsNullOrWhiteSpace(selectedSE) && !seListBox.Contains(selectedSE.Trim(), StringComparer.OrdinalIgnoreCase))
                {
                    seListBox.Add(selectedSE.Trim());
                    inputErrors.Remove("ConcernedSE");
                    selectedSE = null;
                }
            }
            StateHasChanged();
        }
        private void RemoveSEFromListBoxselectedSE()
        {
            foreach (var item in selectedSEListboxItems.ToList())
            {
                seListBox.Remove(item);
            }
            selectedSEListboxItems.Clear();
            StateHasChanged();
        }
        private void AddCommonMailToListBox()
        {
            if (!string.IsNullOrWhiteSpace(newCommonMailEntry) && !newEnquiryForItem.CommonMailIds.Contains(newCommonMailEntry.Trim()))
            {
                newEnquiryForItem.CommonMailIds.Add(newCommonMailEntry.Trim());
                newCommonMailEntry = string.Empty;
            }
            StateHasChanged();
        }
        private void RemoveCommonMailFromListBox()
        {
            foreach (var item in selectedCommonMailListboxItems.ToList())
            {
                newEnquiryForItem.CommonMailIds.Remove(item);
            }
            selectedCommonMailListboxItems.Clear();
            StateHasChanged();
        }
        private void AddCCMailToListBox()
        {
            if (!string.IsNullOrWhiteSpace(newCCMailEntry) && !newEnquiryForItem.CCMailIds.Contains(newCCMailEntry.Trim()))
            {
                newEnquiryForItem.CCMailIds.Add(newCCMailEntry.Trim());
                newCCMailEntry = string.Empty;
            }
            StateHasChanged();
        }
        private void RemoveCCMailFromListBox()
        {
            foreach (var item in selectedCCMailListboxItems.ToList())
            {
                newEnquiryForItem.CCMailIds.Remove(item);
            }
            selectedCCMailListboxItems.Clear();
            StateHasChanged();
        }
        private void AddRoleToListBox()
        {
            if (!string.IsNullOrWhiteSpace(newRoleEntry) && !newUserModel.Roles.Contains(newRoleEntry.Trim()))
            {
                newUserModel.Roles.Add(newRoleEntry.Trim());
                newRoleEntry = string.Empty;
            }
            StateHasChanged();
        }
        private void RemoveRoleFromListBox()
        {
            foreach (var item in selectedRoleListboxItems.ToList())
            {
                newUserModel.Roles.Remove(item);
            }
            selectedRoleListboxItems.Clear();
            StateHasChanged();
        }

        // ---------- MODAL TOGGLES / EDITS ----------
        private void ToggleNewCustomerInput()
        {
            modalMode = "Add";
            newCustomerCompany = new CustomerCompanyModel();
            showCustomerModal = true;
            StateHasChanged();
        }
        private void CloseCustomerModal()
        {
            showCustomerModal = false; StateHasChanged();
        }
        private void ToggleNewReceivedFromInput()
        {
            modalMode = "Add";
            modalContactPerson = new ContactPersonModel();
            if (listBoxCustomers.Count == 1)
            {
                modalContactPerson.CompanyName = listBoxCustomers.First();
                modalContactPerson.Category = "Contractor";
            }
            contactToEdit = null;
            showContactModal = true;
            StateHasChanged();
        }
        private void CloseContactModal()
        {
            showContactModal = false; StateHasChanged();
        }
        private void OnCancelContact()
        {
            showContactModal = false; contactToEdit = null; modalContactPerson = new ContactPersonModel(); StateHasChanged();
        }

        private void StartEditContact()
        {
            string? key = selectedReceivedFromListboxItems.FirstOrDefault() ?? selectedReceivedContact;
            if (string.IsNullOrWhiteSpace(key)) return;

            var parts = key.Split(CONTACT_DELIMITER);
            if (parts.Length == 2)
            {
                string contactName = parts[0].Trim();
                string companyName = parts[1].Trim();

                contactToEdit = storedContacts.FirstOrDefault(c =>
                    c.ContactName.Equals(contactName, StringComparison.OrdinalIgnoreCase) &&
                    c.CompanyName.Equals(companyName, StringComparison.OrdinalIgnoreCase));

                if (contactToEdit != null)
                {
                    modalContactPerson = ContactPersonModel.Clone(contactToEdit);
                    modalMode = "Edit";
                    showContactModal = true;
                }
                else
                {
                    JSRuntime.InvokeVoidAsync("alert", "Error: Contact details not found for editing in master list.");
                }
            }
            else
            {
                JSRuntime.InvokeVoidAsync("alert", "Error: Invalid contact selection format.");
            }
            StateHasChanged();
        }

        private void ToggleNewUserModal()
        {
            modalMode = "Add";
            newUserModel = new UserModel();
            newRoleEntry = string.Empty;
            showUserModal = true;
            StateHasChanged();
        }
        private void CloseUserModal()
        {
            showUserModal = false; StateHasChanged();
        }
        private void ToggleNewEnquiryForModal()
        {
            modalMode = "Add";
            newEnquiryForItem = new EnquiryForItemModel();
            newCommonMailEntry = string.Empty;
            newCCMailEntry = string.Empty;
            showEnqForModal = true;
            StateHasChanged();
        }
        private void CloseEnqForModal()
        {
            showEnqForModal = false; StateHasChanged();
        }

        private void StartEditCompany()
        {
            // --- MODIFIED: Now checks dropdown OR text field ---
            string companyName = selectedCustomerInput ?? enquiryModel.ClientName ?? enquiryModel.ConsultantName ?? string.Empty;

            companyToEdit = storedCustomers.FirstOrDefault(c => c.CompanyName.Equals(companyName, StringComparison.OrdinalIgnoreCase));
            if (companyToEdit != null)
            {
                newCustomerCompany = CustomerCompanyModel.Clone(companyToEdit);
                modalMode = "Edit";
                showCustomerModal = true;
            }
            StateHasChanged();
        }
        private void StartEditUser()
        {
            userToEdit = storedUsers.FirstOrDefault(u => u.FullName.Equals(selectedSE, StringComparison.OrdinalIgnoreCase));
            if (userToEdit != null)
            {
                newUserModel = UserModel.Clone(userToEdit);
                modalMode = "Edit";
                newRoleEntry = string.Empty;
                showUserModal = true;
            }
            StateHasChanged();
        }
        private void StartEditEnquiryFor()
        {
            enqItemToEdit = storedEnqItems.FirstOrDefault(i => i.ItemName.Equals(selectedenqfor, StringComparison.OrdinalIgnoreCase));
            if (enqItemToEdit != null)
            {
                newEnquiryForItem = EnquiryForItemModel.Clone(enqItemToEdit);
                modalMode = "Edit";
                newCommonMailEntry = string.Empty;
                newCCMailEntry = string.Empty;
                showEnqForModal = true;
            }
            StateHasChanged();
        }

        // ---------- MODAL SUBMISSIONS ----------
        private void HandleNewCompanySubmit()
        {
            if (modalMode == "Add")
            {
                storedCustomers.Add(newCustomerCompany);
            }
            else if (modalMode == "Edit" && companyToEdit != null)
            {
                var index = storedCustomers.IndexOf(companyToEdit);
                if (index != -1) storedCustomers[index] = newCustomerCompany;
            }

            UpdateAllListsFromStorage();

            if (newCustomerCompany.Status == "Active")
            {
                if (newCustomerCompany.Category == "Client") enquiryModel.ClientName = newCustomerCompany.CompanyName;
                else if (newCustomerCompany.Category == "Consultant") enquiryModel.ConsultantName = newCustomerCompany.CompanyName;
                else if (newCustomerCompany.Category == "Contractor") selectedCustomerInput = newCustomerCompany.CompanyName; // Pre-select in dropdown
            }

            showCustomerModal = false;
            newCustomerCompany = new CustomerCompanyModel();
            companyToEdit = null;
            StateHasChanged();
        }

        private void HandleNewContactSubmit()
        {
            if (modalContactPerson.Category == "Contractor" && !storedCustomers.Any(c => c.CompanyName.Equals(modalContactPerson.CompanyName, StringComparison.OrdinalIgnoreCase)))
            {
                storedCustomers.Add(CustomerCompanyModel.FromContact(modalContactPerson));
            }

            if (modalMode == "Add")
            {
                storedContacts.Add(modalContactPerson);
            }
            else if (modalMode == "Edit" && contactToEdit != null)
            {
                var index = storedContacts.IndexOf(contactToEdit);
                if (index != -1) { storedContacts[index] = modalContactPerson; }
                contactToEdit = modalContactPerson;
            }

            UpdateAllListsFromStorage();

            if (modalContactPerson.Category == "Contractor")
            {
                string companyName = modalContactPerson.CompanyName;
                string newContactKey = $"{modalContactPerson.ContactName}{CONTACT_DELIMITER}{companyName}";
                string oldContactKey = modalMode == "Edit" && contactToEdit != null ?
                                       $"{contactToEdit.ContactName}{CONTACT_DELIMITER}{contactToEdit.CompanyName}" : string.Empty;

                if (!listBoxCustomers.Any(c => c.Equals(companyName, StringComparison.OrdinalIgnoreCase)))
                {
                    listBoxCustomers.Add(companyName);
                }

                if (modalMode == "Edit" && !string.IsNullOrWhiteSpace(oldContactKey))
                {
                    receivedContactListBox.RemoveAll(p => p.Equals(oldContactKey, StringComparison.OrdinalIgnoreCase));
                }
                if (!receivedContactListBox.Contains(newContactKey))
                {
                    receivedContactListBox.Add(newContactKey);
                }
            }

            showContactModal = false;
            modalContactPerson = new ContactPersonModel();
            contactToEdit = null;
            UpdateReceivedFromList();
            StateHasChanged();
        }

        private void HandleNewUserSubmit()
        {
            if (modalMode == "Add") storedUsers.Add(newUserModel);
            else if (modalMode == "Edit" && userToEdit != null)
            {
                var index = storedUsers.IndexOf(userToEdit);
                if (index != -1) storedUsers[index] = newUserModel;
            }

            UpdateAllListsFromStorage();
            if (newUserModel.Status == "Active") selectedSE = newUserModel.FullName;

            showUserModal = false;
            newUserModel = new UserModel();
            userToEdit = null;
            StateHasChanged();
        }

        private void HandleNewEnquiryForItemSubmit()
        {
            if (modalMode == "Add") storedEnqItems.Add(newEnquiryForItem);
            else if (modalMode == "Edit" && enqItemToEdit != null)
            {
                var index = storedEnqItems.IndexOf(enqItemToEdit);
                if (index != -1) storedEnqItems[index] = newEnquiryForItem;
            }

            UpdateAllListsFromStorage();
            if (newEnquiryForItem.Status == "Active") selectedenqfor = newEnquiryForItem.ItemName;

            showEnqForModal = false;
            newEnquiryForItem = new EnquiryForItemModel();
            enqItemToEdit = null;
            StateHasChanged();
        }

        // ---------- VALIDATION / SUBMIT ----------
        private bool ValidateControlledInput(string? inputValue, List<string> masterList, string errorKey)
        {
            inputErrors.Remove(errorKey);
            if (string.IsNullOrWhiteSpace(inputValue)) return true;
            if (!masterList.Any(n => n.Equals(inputValue.Trim(), StringComparison.OrdinalIgnoreCase)))
            {
                inputErrors[errorKey] = NO_DATA_FOUND; return false;
            }
            return true;
        }

        private async void HandleMainFormSubmit()
        {
            HandleMainFormValidationOnly(out bool isValid);
            if (!isValid) return;

            string requestNo = GenerateRequestNumber();
            var snap = BuildSnapshotFromForm(requestNo);
            enquiriesDb[requestNo] = snap;

            await JSRuntime.InvokeVoidAsync("alert", $"Enquiry submitted successfully! Request No: {requestNo}");
            ResetFormState();
        }

        private EnquirySnapshot BuildSnapshotFromForm(string requestNo) => new()
        {
            RequestNo = requestNo,
            SourceOfInfo = enquiryModel.SourceOfInfo,
            EnquiryDate = enquiryModel.EnquiryDate,
            DueOn = enquiryModel.DueOn,
            SiteVisitDate = enquiryModel.SiteVisitDate,
            ProjectName = enquiryModel.ProjectName,
            ClientName = enquiryModel.ClientName,
            ConsultantName = enquiryModel.ConsultantName,
            DetailsOfEnquiry = enquiryModel.DetailsOfEnquiry,
            DocumentsReceived = enquiryModel.DocumentsReceived,
            hardcopy = enquiryModel.hardcopy,
            drawing = enquiryModel.drawing,
            dvd = enquiryModel.dvd,
            spec = enquiryModel.spec,
            eqpschedule = enquiryModel.eqpschedule,
            Remark = enquiryModel.Remark,
            AutoAck = enquiryModel.AutoAck,
            ceosign = enquiryModel.ceosign,
            SelectedEnquiryTypes = enqtypelistbox.ToList(),
            SelectedEnquiryFor = enqforlistbox.ToList(),
            SelectedCustomers = listBoxCustomers.ToList(),
            SelectedReceivedFroms = receivedContactListBox.ToList(),
            SelectedConcernedSEs = seListBox.ToList()
        };

        private void PopulateFormFromSnapshot(EnquirySnapshot s)
        {
            enquiryModel.SourceOfInfo = s.SourceOfInfo;
            enquiryModel.EnquiryDate = s.EnquiryDate;
            enquiryModel.DueOn = s.DueOn;
            enquiryModel.SiteVisitDate = s.SiteVisitDate;
            enquiryModel.ProjectName = s.ProjectName;
            enquiryModel.ClientName = s.ClientName;
            enquiryModel.ConsultantName = s.ConsultantName;
            enquiryModel.DetailsOfEnquiry = s.DetailsOfEnquiry;
            enquiryModel.DocumentsReceived = s.DocumentsReceived;
            enquiryModel.hardcopy = s.hardcopy;
            enquiryModel.drawing = s.drawing;
            enquiryModel.dvd = s.dvd;
            enquiryModel.spec = s.spec;
            enquiryModel.eqpschedule = s.eqpschedule;
            enquiryModel.Remark = s.Remark;
            enquiryModel.AutoAck = s.AutoAck;
            enquiryModel.ceosign = s.ceosign;

            enqtypelistbox = s.SelectedEnquiryTypes.ToList();
            enqforlistbox = s.SelectedEnquiryFor.ToList();
            listBoxCustomers = s.SelectedCustomers.ToList();
            receivedContactListBox = s.SelectedReceivedFroms.ToList();
            seListBox = s.SelectedConcernedSEs.ToList();

            UpdateReceivedFromList();
            StateHasChanged();
        }

        private void HandleMainFormValidationOnly(out bool isValid)
        {
            inputErrors.Clear();
            isValid = true;

            isValid &= ValidateControlledInput(enquiryModel.ClientName, clientNames, "ClientName");
            isValid &= ValidateControlledInput(enquiryModel.ConsultantName, consultantNames, "ConsultantName");
            isValid &= ValidateControlledInput(enquiryModel.ProjectName, projectNames, "ProjectName");

            isValid &= ValidateRequiredField(enquiryModel.SourceOfInfo, "SourceOfInfo", "Source is required.");
            isValid &= ValidateRequiredDateField(enquiryModel.EnquiryDate, "EnquiryDate", "Enquiry Date is required.");
            isValid &= ValidateRequiredDateField(enquiryModel.DueOn, "DueOn", "Due Date is required.");
            if (enquiryModel.EnquiryDate.HasValue && enquiryModel.DueOn.HasValue)
                isValid &= ValidateDueDate(enquiryModel.DueOn, enquiryModel.EnquiryDate, "DueOn");

            isValid &= ValidateRequiredField(enquiryModel.DetailsOfEnquiry, "DetailsOfEnquiry", "Enquiry details are required.");
            isValid &= ValidateRequiredListbox(enqtypelistbox, "Enquirytype", "At least one Enquiry Type is required.");
            isValid &= ValidateRequiredListbox(enqforlistbox, "Enquiryfor", "At least one Enquiry For item is required.");
            isValid &= ValidateRequiredListbox(seListBox, "ConcernedSE", "At least one 'Concerned SE' is required.");

            isValid &= ValidateRequiredListbox(listBoxCustomers, "CustomerName", "At least one Customer Company Name is required.");
            isValid &= ValidateRequiredListbox(receivedContactListBox, "ReceivedFrom", "At least one 'Received From' contact is required.");

            isValid &= ValidateRequiredField(enquiryModel.ProjectName, "ProjectName", "Project Name is required.");
            isValid &= ValidateRequiredField(enquiryModel.ClientName, "ClientName", "Client Name is required.");

            if (!isValid)
            {
                StateHasChanged();
                JSRuntime.InvokeVoidAsync("alert", "Validation failed. Please correct the fields with red messages.");
            }
        }

        private bool ValidateRequiredField(string? value, string key, string message)
        {
            if (string.IsNullOrWhiteSpace(value) && !inputErrors.ContainsKey(key))
            {
                inputErrors[key] = message; return false;
            }
            return true;
        }
        private bool ValidateRequiredListbox(List<string> list, string key, string message)
        {
            if (list.Count == 0) { inputErrors[key] = message; return false; }
            return true;
        }
        private bool ValidateRequiredDateField(DateTime? date, string key, string message)
        {
            if (!date.HasValue || date.Value == DateTime.MinValue) { inputErrors[key] = message; return false; }
            return true;
        }
        private bool ValidateDueDate(DateTime? dueDate, DateTime? enquiryDate, string key)
        {
            if (!dueDate.HasValue || !enquiryDate.HasValue) return true;
            if (dueDate.Value.Date < enquiryDate.Value.Date)
            {
                inputErrors[key] = "Due Date cannot be before the Enquiry Date."; return false;
            }
            return true;
        }

        private string GenerateRequestNumber()
        {
            string year = DateTime.Now.Year.ToString();
            string month = DateTime.Now.Month.ToString("00");
            int currentCount = enquiriesDb.Count + 1;
            return $"EYS/{year}/{month}/{currentCount:000}";
        }

        private void ResetFormState()
        {
            enquiryModel = new EnquiryModel();
            enquiryModel.EnquiryDate = DateTime.Now.Date;

            // --- NEW ---
            selectedCustomerInput = null;
            // --- REMOVED ---
            // customerAutosuggest = string.Empty;
            // receivedAutosuggest = string.Empty;

            selectedReceivedContact = null;
            selectedSE = null;
            selectedenqtype = null;
            selectedenqfor = null;

            listBoxCustomers.Clear();
            receivedContactListBox.Clear();
            seListBox.Clear();
            enqtypelistbox.Clear();
            enqforlistbox.Clear();
            filteredReceivedFroms.Clear();

            selectedCustomerListboxItems.Clear();
            selectedReceivedFromListboxItems.Clear();
            selectedSEListboxItems.Clear();
            selectedEnqTypeListboxItems.Clear();
            selectedEnqForListboxItems.Clear();
            inputErrors.Clear();

            UpdateReceivedFromList();
            StateHasChanged();
        }

        // ---------- MODIFY FLOW ----------
        private string modifyRequestNo = string.Empty;
        private EnquirySnapshot? modifyLoadedSnapshot = null;

        private async Task LoadForModify()
        {
            inputErrors.Clear();
            modifyLoadedSnapshot = null;

            var key = (modifyRequestNo ?? string.Empty).Trim();
            if (string.IsNullOrWhiteSpace(key))
            {
                await JSRuntime.InvokeVoidAsync("alert", "Please enter a Request No.");
                return;
            }

            if (!enquiriesDb.TryGetValue(key, out var snap))
            {
                await JSRuntime.InvokeVoidAsync("alert", $"Request No '{key}' not found.");
                return;
            }

            modifyLoadedSnapshot = snap;
            PopulateFormFromSnapshot(snap);
        }

        private void ClearModify()
        {
            modifyRequestNo = string.Empty;
            modifyLoadedSnapshot = null;
            ResetFormState();
        }

        private async void HandleModifyFormSubmit()
        {
            if (modifyLoadedSnapshot is null || string.IsNullOrWhiteSpace(modifyRequestNo))
            {
                await JSRuntime.InvokeVoidAsync("alert", "Nothing loaded to modify.");
                return;
            }

            HandleMainFormValidationOnly(out bool ok);
            if (!ok) return;

            var snap = BuildSnapshotFromForm(modifyRequestNo);
            enquiriesDb[modifyRequestNo] = snap;
            modifyLoadedSnapshot = snap;

            await JSRuntime.InvokeVoidAsync("alert", $"Changes saved for {modifyRequestNo}");
        }

        // ---------- SEARCH ----------
        private string searchText = string.Empty;
        private DateTime? searchFromDate = null;
        private DateTime? searchToDate = null;
        private List<EnquirySnapshot> searchResults = new();

        private void RunSearch()
        {
            IEnumerable<EnquirySnapshot> q = enquiriesDb.Values.OrderByDescending(v => v.EnquiryDate ?? DateTime.MinValue);

            if (searchFromDate.HasValue)
                q = q.Where(v => (v.EnquiryDate ?? DateTime.MinValue).Date >= searchFromDate.Value.Date);
            if (searchToDate.HasValue)
                q = q.Where(v => (v.EnquiryDate ?? DateTime.MinValue).Date <= searchToDate.Value.Date);

            var s = (searchText ?? string.Empty).Trim();
            if (!string.IsNullOrWhiteSpace(s))
            {
                s = s.ToLowerInvariant();
                q = q.Where(v =>
                        (v.RequestNo?.ToLowerInvariant().Contains(s) ?? false) ||
                        (v.ClientName?.ToLowerInvariant().Contains(s) ?? false) ||
                        (v.ProjectName?.ToLowerInvariant().Contains(s) ?? false) ||
                        (v.SourceOfInfo?.ToLowerInvariant().Contains(s) ?? false) ||
                        v.SelectedConcernedSEs.Any(x => x.ToLowerInvariant().Contains(s)));
            }

            searchResults = q.Take(500).ToList();
            StateHasChanged();
        }

        private void ClearSearch()
        {
            searchText = string.Empty;
            searchFromDate = null;
            searchToDate = null;
            searchResults.Clear();
        }

        private void OpenInModify(string reqNo)
        {
            modifyRequestNo = reqNo;
            SwitchTab("Modify");
            _ = LoadForModify();
        }
#nullable disable
    }
}