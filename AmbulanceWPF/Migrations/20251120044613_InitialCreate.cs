using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AmbulanceWPF.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DiseaseCatalog",
                columns: table => new
                {
                    DiseaseCode = table.Column<int>(type: "INTEGER", nullable: false),
                    DiseaseName = table.Column<string>(type: "TEXT", maxLength: 45, nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "DATE", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiseaseCatalog", x => x.DiseaseCode);
                });

            migrationBuilder.CreateTable(
                name: "Location",
                columns: table => new
                {
                    PostalCode = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 45, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Location", x => x.PostalCode);
                });

            migrationBuilder.CreateTable(
                name: "MedicationCatalog",
                columns: table => new
                {
                    MedicationCode = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 45, nullable: false),
                    Manufacturer = table.Column<string>(type: "TEXT", maxLength: 45, nullable: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicationCatalog", x => x.MedicationCode);
                });

            migrationBuilder.CreateTable(
                name: "Phone",
                columns: table => new
                {
                    PhoneNumber = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Phone", x => x.PhoneNumber);
                });

            migrationBuilder.CreateTable(
                name: "Procurement",
                columns: table => new
                {
                    ProcurementId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Quantity = table.Column<decimal>(type: "REAL", nullable: true),
                    ProcurementDate = table.Column<DateTime>(type: "DATE", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Procurement", x => x.ProcurementId);
                });

            migrationBuilder.CreateTable(
                name: "Patient",
                columns: table => new
                {
                    JMB = table.Column<string>(type: "TEXT", maxLength: 13, nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 45, nullable: false),
                    LastName = table.Column<string>(type: "TEXT", maxLength: 45, nullable: false),
                    ResidenceLocationId = table.Column<int>(type: "INTEGER", nullable: false),
                    Allergies = table.Column<string>(type: "TEXT", nullable: false),
                    Insurance = table.Column<bool>(type: "INTEGER", nullable: false),
                    DateOfBirth = table.Column<string>(type: "TEXT", nullable: false),
                    Gender = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patient", x => x.JMB);
                    table.ForeignKey(
                        name: "FK_Patient_Location_ResidenceLocationId",
                        column: x => x.ResidenceLocationId,
                        principalTable: "Location",
                        principalColumn: "PostalCode",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MedicationInventory",
                columns: table => new
                {
                    MedicationCode = table.Column<int>(type: "INTEGER", nullable: false),
                    Quantity = table.Column<decimal>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicationInventory", x => x.MedicationCode);
                    table.ForeignKey(
                        name: "FK_MedicationInventory_MedicationCatalog_MedicationCode",
                        column: x => x.MedicationCode,
                        principalTable: "MedicationCatalog",
                        principalColumn: "MedicationCode",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Employee",
                columns: table => new
                {
                    JMB = table.Column<string>(type: "TEXT", maxLength: 13, nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 45, nullable: false),
                    LastName = table.Column<string>(type: "TEXT", maxLength: 45, nullable: false),
                    Username = table.Column<string>(type: "TEXT", maxLength: 45, nullable: false),
                    Password = table.Column<string>(type: "TEXT", maxLength: 45, nullable: false),
                    PasswordHash = table.Column<string>(type: "TEXT", maxLength: 45, nullable: false),
                    Role = table.Column<string>(type: "TEXT", maxLength: 45, nullable: false),
                    IsActive = table.Column<int>(type: "INTEGER", nullable: false),
                    Theme = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Language = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false, defaultValue: "English"),
                    PhoneNumber = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee", x => x.JMB);
                    table.ForeignKey(
                        name: "FK_Employee_Phone_PhoneNumber",
                        column: x => x.PhoneNumber,
                        principalTable: "Phone",
                        principalColumn: "PhoneNumber",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MedicationItem",
                columns: table => new
                {
                    MedicationCode = table.Column<int>(type: "INTEGER", nullable: false),
                    ProcurementId = table.Column<int>(type: "INTEGER", nullable: false),
                    Quantity = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicationItem", x => new { x.MedicationCode, x.ProcurementId });
                    table.ForeignKey(
                        name: "FK_MedicationItem_MedicationCatalog_MedicationCode",
                        column: x => x.MedicationCode,
                        principalTable: "MedicationCatalog",
                        principalColumn: "MedicationCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MedicationItem_Procurement_ProcurementId",
                        column: x => x.ProcurementId,
                        principalTable: "Procurement",
                        principalColumn: "ProcurementId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Intervention",
                columns: table => new
                {
                    InterventionId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PatientJMB = table.Column<string>(type: "TEXT", maxLength: 13, nullable: false),
                    Date = table.Column<DateTime>(type: "DATE", nullable: false),
                    InterventionDescription = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Intervention", x => x.InterventionId);
                    table.ForeignKey(
                        name: "FK_Intervention_Patient_PatientJMB",
                        column: x => x.PatientJMB,
                        principalTable: "Patient",
                        principalColumn: "JMB",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MedicalRecord",
                columns: table => new
                {
                    PatientJMB = table.Column<string>(type: "TEXT", maxLength: 13, nullable: false),
                    ParentName = table.Column<string>(type: "TEXT", maxLength: 45, nullable: false),
                    MaritalStatus = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    Gender = table.Column<int>(type: "INTEGER", nullable: false),
                    Insurance = table.Column<int>(type: "INTEGER", nullable: false),
                    DoctorJMB = table.Column<string>(type: "TEXT", maxLength: 13, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalRecord", x => x.PatientJMB);
                    table.ForeignKey(
                        name: "FK_MedicalRecord_Employee_DoctorJMB",
                        column: x => x.DoctorJMB,
                        principalTable: "Employee",
                        principalColumn: "JMB",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MedicalRecord_Patient_PatientJMB",
                        column: x => x.PatientJMB,
                        principalTable: "Patient",
                        principalColumn: "JMB",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InterventionDoctor",
                columns: table => new
                {
                    InterventionId = table.Column<int>(type: "INTEGER", nullable: false),
                    DoctorJMB = table.Column<string>(type: "TEXT", maxLength: 13, nullable: false),
                    Role = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InterventionDoctor", x => new { x.InterventionId, x.DoctorJMB });
                    table.ForeignKey(
                        name: "FK_InterventionDoctor_Employee_DoctorJMB",
                        column: x => x.DoctorJMB,
                        principalTable: "Employee",
                        principalColumn: "JMB",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InterventionDoctor_Intervention_InterventionId",
                        column: x => x.InterventionId,
                        principalTable: "Intervention",
                        principalColumn: "InterventionId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Therapy",
                columns: table => new
                {
                    InterventionId = table.Column<int>(type: "INTEGER", nullable: false),
                    MedicationCode = table.Column<int>(type: "INTEGER", nullable: false),
                    Dosage = table.Column<decimal>(type: "REAL", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Therapy", x => new { x.InterventionId, x.MedicationCode });
                    table.ForeignKey(
                        name: "FK_Therapy_Intervention_InterventionId",
                        column: x => x.InterventionId,
                        principalTable: "Intervention",
                        principalColumn: "InterventionId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Therapy_MedicationCatalog_MedicationCode",
                        column: x => x.MedicationCode,
                        principalTable: "MedicationCatalog",
                        principalColumn: "MedicationCode",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Examination",
                columns: table => new
                {
                    ExaminationId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ExaminationDate = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    ExaminationDescription = table.Column<string>(type: "TEXT", maxLength: 45, nullable: false),
                    PatientJMB = table.Column<string>(type: "TEXT", maxLength: 13, nullable: false),
                    DoctorJMB = table.Column<string>(type: "TEXT", maxLength: 13, nullable: false),
                    PatientJMB1 = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Examination", x => x.ExaminationId);
                    table.ForeignKey(
                        name: "FK_Examination_Employee_DoctorJMB",
                        column: x => x.DoctorJMB,
                        principalTable: "Employee",
                        principalColumn: "JMB",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Examination_MedicalRecord_PatientJMB",
                        column: x => x.PatientJMB,
                        principalTable: "MedicalRecord",
                        principalColumn: "PatientJMB",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Examination_Patient_PatientJMB",
                        column: x => x.PatientJMB,
                        principalTable: "Patient",
                        principalColumn: "JMB",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Examination_Patient_PatientJMB1",
                        column: x => x.PatientJMB1,
                        principalTable: "Patient",
                        principalColumn: "JMB");
                });

            migrationBuilder.CreateTable(
                name: "Diagnosis",
                columns: table => new
                {
                    PatientJMB = table.Column<string>(type: "TEXT", maxLength: 13, nullable: false),
                    DiseaseCode = table.Column<int>(type: "INTEGER", nullable: false),
                    Date = table.Column<DateTime>(type: "DATE", nullable: false),
                    DoctorOpinion = table.Column<string>(type: "TEXT", nullable: false),
                    ExaminationId = table.Column<int>(type: "INTEGER", nullable: false),
                    DoctorJMB = table.Column<string>(type: "TEXT", maxLength: 13, nullable: false),
                    EmployeeJMB = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Diagnosis", x => new { x.PatientJMB, x.DiseaseCode, x.Date });
                    table.ForeignKey(
                        name: "FK_Diagnosis_DiseaseCatalog_DiseaseCode",
                        column: x => x.DiseaseCode,
                        principalTable: "DiseaseCatalog",
                        principalColumn: "DiseaseCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Diagnosis_Employee_DoctorJMB",
                        column: x => x.DoctorJMB,
                        principalTable: "Employee",
                        principalColumn: "JMB",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Diagnosis_Employee_EmployeeJMB",
                        column: x => x.EmployeeJMB,
                        principalTable: "Employee",
                        principalColumn: "JMB");
                    table.ForeignKey(
                        name: "FK_Diagnosis_Examination_ExaminationId",
                        column: x => x.ExaminationId,
                        principalTable: "Examination",
                        principalColumn: "ExaminationId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Diagnosis_MedicalRecord_PatientJMB",
                        column: x => x.PatientJMB,
                        principalTable: "MedicalRecord",
                        principalColumn: "PatientJMB",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Diagnosis_Patient_PatientJMB",
                        column: x => x.PatientJMB,
                        principalTable: "Patient",
                        principalColumn: "JMB",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Referral",
                columns: table => new
                {
                    ReferralId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DiseaseCode = table.Column<int>(type: "INTEGER", nullable: false),
                    Specialists = table.Column<string>(type: "TEXT", maxLength: 45, nullable: false),
                    DoctorJMB = table.Column<string>(type: "TEXT", maxLength: 13, nullable: false),
                    Date = table.Column<DateTime>(type: "DATE", nullable: false),
                    ExaminationId = table.Column<int>(type: "INTEGER", nullable: false),
                    PatientJMB = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Referral", x => x.ReferralId);
                    table.ForeignKey(
                        name: "FK_Referral_DiseaseCatalog_DiseaseCode",
                        column: x => x.DiseaseCode,
                        principalTable: "DiseaseCatalog",
                        principalColumn: "DiseaseCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Referral_Employee_DoctorJMB",
                        column: x => x.DoctorJMB,
                        principalTable: "Employee",
                        principalColumn: "JMB",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Referral_Examination_ExaminationId",
                        column: x => x.ExaminationId,
                        principalTable: "Examination",
                        principalColumn: "ExaminationId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Referral_MedicalRecord_PatientJMB",
                        column: x => x.PatientJMB,
                        principalTable: "MedicalRecord",
                        principalColumn: "PatientJMB",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "DiseaseCatalog",
                columns: new[] { "DiseaseCode", "Description", "DiseaseName", "UpdateDate" },
                values: new object[,]
                {
                    { 1001, "Persistent high arterial blood pressure.", "Hypertension", new DateTime(2025, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 1002, "Inflammation of the bronchial tubes, often viral.", "Acute bronchitis", new DateTime(2025, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 1003, "Chronic condition with high blood sugar levels.", "Diabetes Mellitus", new DateTime(2025, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 1004, "Chronic respiratory condition with airway inflammation.", "Asthma", new DateTime(2025, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 1005, "Recurrent headaches often with nausea and sensitivity to light.", "Migraine", new DateTime(2025, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Employee",
                columns: new[] { "JMB", "IsActive", "Language", "LastName", "Name", "Password", "PasswordHash", "PhoneNumber", "Role", "Theme", "Username" },
                values: new object[] { "9035172846109", 1, "English", "Juric", "Mila", "milinasifra", "amarovasifra", null, "MedicalTechnician", "Dark", "mila.j" });

            migrationBuilder.InsertData(
                table: "Location",
                columns: new[] { "PostalCode", "Name" },
                values: new object[,]
                {
                    { 71000, "Sarajevo" },
                    { 72000, "Zenica" },
                    { 75000, "Tuzla" },
                    { 78000, "Banja Luka" },
                    { 88000, "Mostar" }
                });

            migrationBuilder.InsertData(
                table: "MedicationCatalog",
                columns: new[] { "MedicationCode", "IsActive", "Manufacturer", "Name" },
                values: new object[,]
                {
                    { 2001, true, "ACME Pharma", "Lisinopril" },
                    { 2002, true, "BreatheWell", "Salbutamol" },
                    { 2003, true, "GlucoPharm", "Metformin" },
                    { 2004, true, "RespiraMed", "Albuterol" },
                    { 2005, true, "PainRelief Inc", "Ibuprofen" }
                });

            migrationBuilder.InsertData(
                table: "Phone",
                column: "PhoneNumber",
                values: new object[]
                {
                    "+38761111222",
                    "+38762123456",
                    "+38763333444",
                    "+38764444555",
                    "+38765555666"
                });

            migrationBuilder.InsertData(
                table: "Procurement",
                columns: new[] { "ProcurementId", "ProcurementDate", "Quantity" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { 2, new DateTime(2025, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { 3, new DateTime(2025, 4, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { 4, new DateTime(2025, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { 5, new DateTime(2025, 6, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), null }
                });

            migrationBuilder.InsertData(
                table: "Employee",
                columns: new[] { "JMB", "IsActive", "Language", "LastName", "Name", "Password", "PasswordHash", "PhoneNumber", "Role", "Theme", "Username" },
                values: new object[,]
                {
                    { "1234567890123", 1, "Serbian", "Doe", "John", "password", "hash", "+38763333444", "Doctor", "Gray", "john.d" },
                    { "2345678901234", 1, "English", "Smith", "Jane", "password", "hash", "+38764444555", "MedicalTechnician", "Light", "jane.s" },
                    { "3456789012345", 1, "Serbian", "Johnson", "Alice", "password", "hash", "+38765555666", "Doctor", "Dark", "alice.j" },
                    { "6482157394021", 1, "English", "Kovacevic", "Amar", "amarovasifra", "amarovasifra", "+38761111222", "Doctor", "Light", "amar.k" }
                });

            migrationBuilder.InsertData(
                table: "MedicationInventory",
                columns: new[] { "MedicationCode", "Quantity" },
                values: new object[,]
                {
                    { 2001, 25m },
                    { 2002, 40m },
                    { 2003, 50m },
                    { 2004, 30m },
                    { 2005, 60m }
                });

            migrationBuilder.InsertData(
                table: "MedicationItem",
                columns: new[] { "MedicationCode", "ProcurementId", "Quantity" },
                values: new object[,]
                {
                    { 2001, 1, 50 },
                    { 2002, 2, 100 },
                    { 2003, 3, 75 },
                    { 2004, 4, 120 },
                    { 2005, 5, 90 }
                });

            migrationBuilder.InsertData(
                table: "Patient",
                columns: new[] { "JMB", "Allergies", "DateOfBirth", "Gender", "Insurance", "LastName", "Name", "ResidenceLocationId" },
                values: new object[,]
                {
                    { "4185270936518", "", "28/02/1995", true, true, "Petrovic", "Sara", 78000 },
                    { "4567890123456", "", "15/03/1980", false, false, "Brown", "Bob", 75000 },
                    { "5678901234567", "", "10/11/1990", true, true, "Green", "Eva", 88000 },
                    { "5729618430725", "", "12/07/2001", false, true, "Horvat", "Ivan", 71000 },
                    { "6789012345678", "", "05/06/2005", false, false, "Black", "Charlie", 72000 }
                });

            migrationBuilder.InsertData(
                table: "Intervention",
                columns: new[] { "InterventionId", "Date", "InterventionDescription", "PatientJMB" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Nebulization therapy administered.", "4185270936518" },
                    { 2, new DateTime(2025, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Insulin injection training.", "4567890123456" },
                    { 3, new DateTime(2025, 7, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bronchodilator administration.", "5678901234567" }
                });

            migrationBuilder.InsertData(
                table: "MedicalRecord",
                columns: new[] { "PatientJMB", "DoctorJMB", "Gender", "Insurance", "MaritalStatus", "ParentName" },
                values: new object[,]
                {
                    { "4185270936518", "6482157394021", 1, 0, "Married", "Ivana" },
                    { "4567890123456", "1234567890123", 0, 1, "Divorced", "Slavica" },
                    { "5678901234567", "3456789012345", 1, 0, "Single", "Ljuboslav" },
                    { "5729618430725", "6482157394021", 0, 0, "Single", "Marko" },
                    { "6789012345678", "1234567890123", 0, 1, "Married", "Rajko" }
                });

            migrationBuilder.InsertData(
                table: "Examination",
                columns: new[] { "ExaminationId", "DoctorJMB", "ExaminationDate", "ExaminationDescription", "PatientJMB", "PatientJMB1" },
                values: new object[,]
                {
                    { 1, "6482157394021", new DateTime(2025, 4, 20, 10, 30, 0, 0, DateTimeKind.Unspecified), "Routine checkup", "5729618430725", null },
                    { 2, "6482157394021", new DateTime(2025, 5, 5, 9, 0, 0, 0, DateTimeKind.Unspecified), "Cough and wheezing", "4185270936518", null },
                    { 3, "1234567890123", new DateTime(2025, 6, 10, 11, 0, 0, 0, DateTimeKind.Unspecified), "Blood sugar check", "4567890123456", null },
                    { 4, "3456789012345", new DateTime(2025, 7, 15, 14, 30, 0, 0, DateTimeKind.Unspecified), "Respiratory exam", "5678901234567", null },
                    { 5, "1234567890123", new DateTime(2025, 8, 20, 16, 0, 0, 0, DateTimeKind.Unspecified), "Headache evaluation", "6789012345678", null }
                });

            migrationBuilder.InsertData(
                table: "InterventionDoctor",
                columns: new[] { "DoctorJMB", "InterventionId", "Role" },
                values: new object[,]
                {
                    { "1234567890123", 1, "Assistant" },
                    { "6482157394021", 1, "Lead Doctor" },
                    { "1234567890123", 2, "Lead Doctor" },
                    { "3456789012345", 2, "Assistant" },
                    { "3456789012345", 3, "Lead Doctor" }
                });

            migrationBuilder.InsertData(
                table: "Therapy",
                columns: new[] { "InterventionId", "MedicationCode", "Dosage" },
                values: new object[,]
                {
                    { 1, 2002, 2.5m },
                    { 1, 2005, 200m },
                    { 2, 2003, 500m },
                    { 3, 2001, 10m },
                    { 3, 2004, 100m }
                });

            migrationBuilder.InsertData(
                table: "Diagnosis",
                columns: new[] { "Date", "DiseaseCode", "PatientJMB", "DoctorJMB", "DoctorOpinion", "EmployeeJMB", "ExaminationId" },
                values: new object[,]
                {
                    { new DateTime(2025, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 1002, "4185270936518", "6482157394021", "Likely viral origin.", null, 2 },
                    { new DateTime(2025, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 1003, "4567890123456", "1234567890123", "Type 2 diabetes; diet control recommended.", null, 3 },
                    { new DateTime(2025, 7, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 1004, "5678901234567", "3456789012345", "Mild asthma; inhaler prescribed.", null, 4 },
                    { new DateTime(2025, 4, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 1001, "5729618430725", "6482157394021", "Stage 1 hypertension; monitor BP.", null, 1 },
                    { new DateTime(2025, 8, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 1005, "6789012345678", "1234567890123", "Chronic migraine; trigger avoidance advised.", null, 5 }
                });

            migrationBuilder.InsertData(
                table: "Referral",
                columns: new[] { "ReferralId", "Date", "DiseaseCode", "DoctorJMB", "ExaminationId", "PatientJMB", "Specialists" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 1002, "6482157394021", 2, "4185270936518", "Pulmonologist" },
                    { 2, new DateTime(2025, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 1003, "1234567890123", 3, "4567890123456", "Endocrinologist" },
                    { 3, new DateTime(2025, 7, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 1004, "3456789012345", 4, "5678901234567", "Pulmonologist" },
                    { 4, new DateTime(2025, 8, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 1005, "1234567890123", 5, "6789012345678", "Neurologist" },
                    { 5, new DateTime(2025, 4, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 1001, "6482157394021", 1, "5729618430725", "Cardiologist" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Diagnosis_Date",
                table: "Diagnosis",
                column: "Date");

            migrationBuilder.CreateIndex(
                name: "IX_Diagnosis_DiseaseCode",
                table: "Diagnosis",
                column: "DiseaseCode");

            migrationBuilder.CreateIndex(
                name: "IX_Diagnosis_DoctorJMB",
                table: "Diagnosis",
                column: "DoctorJMB");

            migrationBuilder.CreateIndex(
                name: "IX_Diagnosis_EmployeeJMB",
                table: "Diagnosis",
                column: "EmployeeJMB");

            migrationBuilder.CreateIndex(
                name: "IX_Diagnosis_ExaminationId",
                table: "Diagnosis",
                column: "ExaminationId");

            migrationBuilder.CreateIndex(
                name: "IX_Diagnosis_PatientJMB",
                table: "Diagnosis",
                column: "PatientJMB");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_PhoneNumber",
                table: "Employee",
                column: "PhoneNumber");

            migrationBuilder.CreateIndex(
                name: "IX_Examination_Date",
                table: "Examination",
                column: "ExaminationDate");

            migrationBuilder.CreateIndex(
                name: "IX_Examination_DoctorJMB",
                table: "Examination",
                column: "DoctorJMB");

            migrationBuilder.CreateIndex(
                name: "IX_Examination_PatientJMB",
                table: "Examination",
                column: "PatientJMB");

            migrationBuilder.CreateIndex(
                name: "IX_Examination_PatientJMB1",
                table: "Examination",
                column: "PatientJMB1");

            migrationBuilder.CreateIndex(
                name: "IX_Intervention_Date",
                table: "Intervention",
                column: "Date");

            migrationBuilder.CreateIndex(
                name: "IX_Intervention_PatientJMB",
                table: "Intervention",
                column: "PatientJMB");

            migrationBuilder.CreateIndex(
                name: "IX_InterventionDoctor_DoctorJMB",
                table: "InterventionDoctor",
                column: "DoctorJMB");

            migrationBuilder.CreateIndex(
                name: "IX_InterventionDoctor_InterventionId",
                table: "InterventionDoctor",
                column: "InterventionId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalRecord_DoctorJMB",
                table: "MedicalRecord",
                column: "DoctorJMB");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalRecord_PatientJMB",
                table: "MedicalRecord",
                column: "PatientJMB");

            migrationBuilder.CreateIndex(
                name: "IX_MedicationCatalog_IsActive",
                table: "MedicationCatalog",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_MedicationCatalog_Manufacturer",
                table: "MedicationCatalog",
                column: "Manufacturer");

            migrationBuilder.CreateIndex(
                name: "IX_MedicationCatalog_Name",
                table: "MedicationCatalog",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_MedicationItem_MedicationCode",
                table: "MedicationItem",
                column: "MedicationCode");

            migrationBuilder.CreateIndex(
                name: "IX_MedicationItem_ProcurementId",
                table: "MedicationItem",
                column: "ProcurementId");

            migrationBuilder.CreateIndex(
                name: "IX_Patient_ResidenceLocationId",
                table: "Patient",
                column: "ResidenceLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Procurement_Date",
                table: "Procurement",
                column: "ProcurementDate");

            migrationBuilder.CreateIndex(
                name: "IX_Referral_Date",
                table: "Referral",
                column: "Date");

            migrationBuilder.CreateIndex(
                name: "IX_Referral_DiseaseCode",
                table: "Referral",
                column: "DiseaseCode");

            migrationBuilder.CreateIndex(
                name: "IX_Referral_DoctorJMB",
                table: "Referral",
                column: "DoctorJMB");

            migrationBuilder.CreateIndex(
                name: "IX_Referral_ExaminationId",
                table: "Referral",
                column: "ExaminationId");

            migrationBuilder.CreateIndex(
                name: "IX_Referral_PatientJMB",
                table: "Referral",
                column: "PatientJMB");

            migrationBuilder.CreateIndex(
                name: "IX_Therapy_InterventionId",
                table: "Therapy",
                column: "InterventionId");

            migrationBuilder.CreateIndex(
                name: "IX_Therapy_MedicationCode",
                table: "Therapy",
                column: "MedicationCode");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Diagnosis");

            migrationBuilder.DropTable(
                name: "InterventionDoctor");

            migrationBuilder.DropTable(
                name: "MedicationInventory");

            migrationBuilder.DropTable(
                name: "MedicationItem");

            migrationBuilder.DropTable(
                name: "Referral");

            migrationBuilder.DropTable(
                name: "Therapy");

            migrationBuilder.DropTable(
                name: "Procurement");

            migrationBuilder.DropTable(
                name: "DiseaseCatalog");

            migrationBuilder.DropTable(
                name: "Examination");

            migrationBuilder.DropTable(
                name: "Intervention");

            migrationBuilder.DropTable(
                name: "MedicationCatalog");

            migrationBuilder.DropTable(
                name: "MedicalRecord");

            migrationBuilder.DropTable(
                name: "Employee");

            migrationBuilder.DropTable(
                name: "Patient");

            migrationBuilder.DropTable(
                name: "Phone");

            migrationBuilder.DropTable(
                name: "Location");
        }
    }
}
