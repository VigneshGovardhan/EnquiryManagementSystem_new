using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EnquiryManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CustomerCompanies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Category = table.Column<string>(type: "TEXT", nullable: false),
                    CompanyName = table.Column<string>(type: "TEXT", nullable: false),
                    Address1 = table.Column<string>(type: "TEXT", nullable: false),
                    Address2 = table.Column<string>(type: "TEXT", nullable: true),
                    Rating = table.Column<string>(type: "TEXT", nullable: true),
                    Type = table.Column<string>(type: "TEXT", nullable: true),
                    FaxNo = table.Column<string>(type: "TEXT", nullable: true),
                    Phone1 = table.Column<string>(type: "TEXT", nullable: false),
                    Phone2 = table.Column<string>(type: "TEXT", nullable: true),
                    MailId = table.Column<string>(type: "TEXT", nullable: true),
                    Website = table.Column<string>(type: "TEXT", nullable: true),
                    Status = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerCompanies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Enquiries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RequestNo = table.Column<string>(type: "TEXT", nullable: false),
                    SourceOfInfo = table.Column<string>(type: "TEXT", nullable: false),
                    EnquiryDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    DueOn = table.Column<DateTime>(type: "TEXT", nullable: true),
                    SiteVisitDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ProjectName = table.Column<string>(type: "TEXT", nullable: false),
                    ClientName = table.Column<string>(type: "TEXT", nullable: false),
                    ConsultantName = table.Column<string>(type: "TEXT", nullable: true),
                    DetailsOfEnquiry = table.Column<string>(type: "TEXT", nullable: false),
                    DocumentsReceived = table.Column<string>(type: "TEXT", nullable: true),
                    hardcopy = table.Column<bool>(type: "INTEGER", nullable: false),
                    drawing = table.Column<bool>(type: "INTEGER", nullable: false),
                    dvd = table.Column<bool>(type: "INTEGER", nullable: false),
                    spec = table.Column<bool>(type: "INTEGER", nullable: false),
                    eqpschedule = table.Column<bool>(type: "INTEGER", nullable: false),
                    Remark = table.Column<string>(type: "TEXT", nullable: true),
                    AutoAck = table.Column<bool>(type: "INTEGER", nullable: false),
                    ceosign = table.Column<bool>(type: "INTEGER", nullable: false),
                    Status = table.Column<string>(type: "TEXT", nullable: false),
                    SelectedEnquiryTypes = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enquiries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EnquiryContactPerson",
                columns: table => new
                {
                    EnquiriesId = table.Column<int>(type: "INTEGER", nullable: false),
                    SelectedReceivedFromsId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnquiryContactPerson", x => new { x.EnquiriesId, x.SelectedReceivedFromsId });
                });

            migrationBuilder.CreateTable(
                name: "EnquiryCustomerCompany",
                columns: table => new
                {
                    EnquiriesId = table.Column<int>(type: "INTEGER", nullable: false),
                    SelectedCustomersId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnquiryCustomerCompany", x => new { x.EnquiriesId, x.SelectedCustomersId });
                });

            migrationBuilder.CreateTable(
                name: "EnquiryForItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ItemName = table.Column<string>(type: "TEXT", nullable: false),
                    CompanyName = table.Column<string>(type: "TEXT", nullable: true),
                    DepartmentName = table.Column<string>(type: "TEXT", nullable: true),
                    Status = table.Column<string>(type: "TEXT", nullable: false),
                    CommonMailIds = table.Column<string>(type: "TEXT", nullable: false),
                    CCMailIds = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnquiryForItems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FullName = table.Column<string>(type: "TEXT", nullable: false),
                    Designation = table.Column<string>(type: "TEXT", nullable: true),
                    MailId = table.Column<string>(type: "TEXT", nullable: false),
                    LoginPassword = table.Column<string>(type: "TEXT", nullable: false),
                    Status = table.Column<string>(type: "TEXT", nullable: false),
                    Department = table.Column<string>(type: "TEXT", nullable: false),
                    Roles = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContactPersons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ContactName = table.Column<string>(type: "TEXT", nullable: false),
                    Designation = table.Column<string>(type: "TEXT", nullable: true),
                    CategoryOfDesignation = table.Column<string>(type: "TEXT", nullable: false),
                    Address1 = table.Column<string>(type: "TEXT", nullable: false),
                    Address2 = table.Column<string>(type: "TEXT", nullable: true),
                    FaxNo = table.Column<string>(type: "TEXT", nullable: true),
                    Phone = table.Column<string>(type: "TEXT", nullable: true),
                    Mobile1 = table.Column<string>(type: "TEXT", nullable: true),
                    Mobile2 = table.Column<string>(type: "TEXT", nullable: true),
                    EmailId = table.Column<string>(type: "TEXT", nullable: true),
                    CustomerCompanyId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactPersons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContactPersons_CustomerCompanies_CustomerCompanyId",
                        column: x => x.CustomerCompanyId,
                        principalTable: "CustomerCompanies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CustomerCompanyEnquiry",
                columns: table => new
                {
                    EnquiriesId = table.Column<int>(type: "INTEGER", nullable: false),
                    SelectedCustomersId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerCompanyEnquiry", x => new { x.EnquiriesId, x.SelectedCustomersId });
                    table.ForeignKey(
                        name: "FK_CustomerCompanyEnquiry_CustomerCompanies_SelectedCustomersId",
                        column: x => x.SelectedCustomersId,
                        principalTable: "CustomerCompanies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomerCompanyEnquiry_Enquiries_EnquiriesId",
                        column: x => x.EnquiriesId,
                        principalTable: "Enquiries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EnquiryEnquiryForItem",
                columns: table => new
                {
                    EnquiriesId = table.Column<int>(type: "INTEGER", nullable: false),
                    SelectedEnquiryForId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnquiryEnquiryForItem", x => new { x.EnquiriesId, x.SelectedEnquiryForId });
                    table.ForeignKey(
                        name: "FK_EnquiryEnquiryForItem_Enquiries_EnquiriesId",
                        column: x => x.EnquiriesId,
                        principalTable: "Enquiries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EnquiryEnquiryForItem_EnquiryForItems_SelectedEnquiryForId",
                        column: x => x.SelectedEnquiryForId,
                        principalTable: "EnquiryForItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EnquiryUser",
                columns: table => new
                {
                    EnquiriesId = table.Column<int>(type: "INTEGER", nullable: false),
                    SelectedConcernedSEsId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnquiryUser", x => new { x.EnquiriesId, x.SelectedConcernedSEsId });
                    table.ForeignKey(
                        name: "FK_EnquiryUser_Enquiries_EnquiriesId",
                        column: x => x.EnquiriesId,
                        principalTable: "Enquiries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EnquiryUser_Users_SelectedConcernedSEsId",
                        column: x => x.SelectedConcernedSEsId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContactPersonEnquiry",
                columns: table => new
                {
                    EnquiriesId = table.Column<int>(type: "INTEGER", nullable: false),
                    SelectedReceivedFromsId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactPersonEnquiry", x => new { x.EnquiriesId, x.SelectedReceivedFromsId });
                    table.ForeignKey(
                        name: "FK_ContactPersonEnquiry_ContactPersons_SelectedReceivedFromsId",
                        column: x => x.SelectedReceivedFromsId,
                        principalTable: "ContactPersons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContactPersonEnquiry_Enquiries_EnquiriesId",
                        column: x => x.EnquiriesId,
                        principalTable: "Enquiries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "CustomerCompanies",
                columns: new[] { "Id", "Address1", "Address2", "Category", "CompanyName", "FaxNo", "MailId", "Phone1", "Phone2", "Rating", "Status", "Type", "Website" },
                values: new object[,]
                {
                    { 1, "123 Main St", null, "Contractor", "Customer X Ltd", null, null, "222", null, null, "Active", null, null },
                    { 2, "456 Oak Ave", null, "Contractor", "Customer Y Corp", null, null, "555", null, null, "Active", null, null },
                    { 3, "789 Pine Rd", null, "Client", "Client Z Inc", null, null, "888", null, null, "Active", null, null },
                    { 4, "101 Elm Blvd", null, "Consultant", "Consultant A", null, null, "000", null, null, "Active", null, null }
                });

            migrationBuilder.InsertData(
                table: "Enquiries",
                columns: new[] { "Id", "AutoAck", "ClientName", "ConsultantName", "DetailsOfEnquiry", "DocumentsReceived", "DueOn", "EnquiryDate", "ProjectName", "Remark", "RequestNo", "SelectedEnquiryTypes", "SiteVisitDate", "SourceOfInfo", "Status", "ceosign", "drawing", "dvd", "eqpschedule", "hardcopy", "spec" },
                values: new object[] { 1, false, "Client Z Inc", "Consultant A", "Initial seeded enquiry", "", new DateTime(2025, 11, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 11, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Project Alpha", "", "EYS/2025/11/001", "Re-Tender", null, "Phone", "Enquiry", false, true, false, false, true, false });

            migrationBuilder.InsertData(
                table: "EnquiryContactPerson",
                columns: new[] { "EnquiriesId", "SelectedReceivedFromsId" },
                values: new object[] { 1, 3 });

            migrationBuilder.InsertData(
                table: "EnquiryCustomerCompany",
                columns: new[] { "EnquiriesId", "SelectedCustomersId" },
                values: new object[] { 1, 1 });

            migrationBuilder.InsertData(
                table: "EnquiryForItems",
                columns: new[] { "Id", "CCMailIds", "CommonMailIds", "CompanyName", "DepartmentName", "ItemName", "Status" },
                values: new object[,]
                {
                    { 1, "", "elect_common@a.com", null, "Elect", "Electrical", "Active" },
                    { 2, "mech_cc1@b.com", "", null, "Mech", "Mechanical", "Active" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Department", "Designation", "FullName", "LoginPassword", "MailId", "Roles", "Status" },
                values: new object[,]
                {
                    { 1, "MEP", "Sales Engineer", "SE1 - John Doe", "123", "se1@comp.com", "Enquiry,Quotation", "Active" },
                    { 2, "MEP", "Sales Manager", "SE2 - Jane Smith", "123", "se2@comp.com", "Enquiry,Admin", "Active" }
                });

            migrationBuilder.InsertData(
                table: "ContactPersons",
                columns: new[] { "Id", "Address1", "Address2", "CategoryOfDesignation", "ContactName", "CustomerCompanyId", "Designation", "EmailId", "FaxNo", "Mobile1", "Mobile2", "Phone" },
                values: new object[,]
                {
                    { 1, "123 Main St", null, "Technical", "Velu", 1, "Manager", "pa@custx.com", null, "333", null, null },
                    { 2, "456 Oak Ave", null, "Technical", "Vijay", 2, "Director", "pb@custy.com", null, "666", null, null },
                    { 3, "123 Main St", null, "Technical", "Seema", 1, "Engineer", "sc@custx.com", null, "333", null, null },
                    { 4, "789 Pine Rd", null, "Technical", "Person C - Engineer", 3, "Engineer", "pc@clientz.com", null, "999", null, null }
                });

            migrationBuilder.InsertData(
                table: "EnquiryEnquiryForItem",
                columns: new[] { "EnquiriesId", "SelectedEnquiryForId" },
                values: new object[] { 1, 1 });

            migrationBuilder.InsertData(
                table: "EnquiryUser",
                columns: new[] { "EnquiriesId", "SelectedConcernedSEsId" },
                values: new object[] { 1, 2 });

            migrationBuilder.CreateIndex(
                name: "IX_ContactPersonEnquiry_SelectedReceivedFromsId",
                table: "ContactPersonEnquiry",
                column: "SelectedReceivedFromsId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactPersons_CustomerCompanyId",
                table: "ContactPersons",
                column: "CustomerCompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerCompanyEnquiry_SelectedCustomersId",
                table: "CustomerCompanyEnquiry",
                column: "SelectedCustomersId");

            migrationBuilder.CreateIndex(
                name: "IX_EnquiryEnquiryForItem_SelectedEnquiryForId",
                table: "EnquiryEnquiryForItem",
                column: "SelectedEnquiryForId");

            migrationBuilder.CreateIndex(
                name: "IX_EnquiryUser_SelectedConcernedSEsId",
                table: "EnquiryUser",
                column: "SelectedConcernedSEsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContactPersonEnquiry");

            migrationBuilder.DropTable(
                name: "CustomerCompanyEnquiry");

            migrationBuilder.DropTable(
                name: "EnquiryContactPerson");

            migrationBuilder.DropTable(
                name: "EnquiryCustomerCompany");

            migrationBuilder.DropTable(
                name: "EnquiryEnquiryForItem");

            migrationBuilder.DropTable(
                name: "EnquiryUser");

            migrationBuilder.DropTable(
                name: "ContactPersons");

            migrationBuilder.DropTable(
                name: "EnquiryForItems");

            migrationBuilder.DropTable(
                name: "Enquiries");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "CustomerCompanies");
        }
    }
}
