using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AmbulanceWPF.Migrations
{
    /     public partial class InitialCreate : Migration
    {
        /         protected override void Up(MigrationBuilder migrationBuilder)
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
                name: "Employee",
                columns: table => new
                {
                    JMB = table.Column<string>(type: "TEXT", maxLength: 13, nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 45, nullable: false),
                    LastName = table.Column<string>(type: "TEXT", maxLength: 45, nullable: false),
                    Username = table.Column<string>(type: "TEXT", maxLength: 45, nullable: false),
                    Password = table.Column<string>(type: "TEXT", maxLength: 45, nullable: false),
                    Role = table.Column<string>(type: "TEXT", maxLength: 45, nullable: false),
                    IsActive = table.Column<int>(type: "INTEGER", nullable: false),
                    Theme = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee", x => x.JMB);
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
                name: "MedicalTechnician",
                columns: table => new
                {
                    JMB = table.Column<string>(type: "TEXT", maxLength: 13, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalTechnician", x => x.JMB);
                    table.ForeignKey(
                        name: "FK_MedicalTechnician_Employee_JMB",
                        column: x => x.JMB,
                        principalTable: "Employee",
                        principalColumn: "JMB",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Patient",
                columns: table => new
                {
                    JMB = table.Column<string>(type: "TEXT", maxLength: 13, nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 45, nullable: false),
                    LastName = table.Column<string>(type: "TEXT", maxLength: 45, nullable: false),
                    ResidenceLocationId = table.Column<int>(type: "INTEGER", nullable: false)
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
                    PhoneNumber = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctor", x => x.JMB);
                    table.ForeignKey(
                        name: "FK_Doctor_Employee_JMB",
                        column: x => x.JMB,
                        principalTable: "Employee",
                        principalColumn: "JMB",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Doctor_Phone_PhoneNumber",
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
                        name: "FK_Examination_Doctor_DoctorJMB",
                        column: x => x.DoctorJMB,
                        principalTable: "Employee",
                        principalColumn: "JMB",
                        onDelete: ReferentialAction.Restrict);
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
                        name: "FK_MedicalRecord_Doctor_DoctorJMB",
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
                    DoctorJMB = table.Column<string>(type: "TEXT", maxLength: 13, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InterventionDoctor", x => new { x.InterventionId, x.DoctorJMB });
                    table.ForeignKey(
                        name: "FK_InterventionDoctor_Doctor_DoctorJMB",
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
                name: "Diagnosis",
                columns: table => new
                {
                    PatientJMB = table.Column<string>(type: "TEXT", maxLength: 13, nullable: false),
                    DiseaseCode = table.Column<int>(type: "INTEGER", nullable: false),
                    Date = table.Column<DateTime>(type: "DATE", nullable: false),
                    DoctorOpinion = table.Column<string>(type: "TEXT", nullable: false),
                    ExaminationId = table.Column<int>(type: "INTEGER", nullable: false),
                    DoctorJMB = table.Column<string>(type: "TEXT", maxLength: 13, nullable: false),
                    DoctorJMB1 = table.Column<string>(type: "TEXT", nullable: true)
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
                        name: "FK_Diagnosis_Doctor_DoctorJMB",
                        column: x => x.DoctorJMB,
                        principalTable: "Employee",
                        principalColumn: "JMB",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Diagnosis_Doctor_DoctorJMB1",
                        column: x => x.DoctorJMB1,
                        principalTable: "Employee",
                        principalColumn: "JMB");
                    table.ForeignKey(
                        name: "FK_Diagnosis_Examination_ExaminationId",
                        column: x => x.ExaminationId,
                        principalTable: "Examination",
                        principalColumn: "ExaminationId",
                        onDelete: ReferentialAction.Restrict);
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
                    ExaminationId = table.Column<int>(type: "INTEGER", nullable: false)
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
                        name: "FK_Referral_Doctor_DoctorJMB",
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
                name: "IX_Diagnosis_DoctorJMB1",
                table: "Diagnosis",
                column: "DoctorJMB1");

            migrationBuilder.CreateIndex(
                name: "IX_Diagnosis_ExaminationId",
                table: "Diagnosis",
                column: "ExaminationId");

            migrationBuilder.CreateIndex(
                name: "IX_Diagnosis_PatientJMB",
                table: "Diagnosis",
                column: "PatientJMB");

            migrationBuilder.CreateIndex(
                name: "IX_Doctor_PhoneNumber",
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
                name: "IX_Therapy_InterventionId",
                table: "Therapy",
                column: "InterventionId");

            migrationBuilder.CreateIndex(
                name: "IX_Therapy_MedicationCode",
                table: "Therapy",
                column: "MedicationCode");
        }

        /         protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Diagnosis");

            migrationBuilder.DropTable(
                name: "InterventionDoctor");

            migrationBuilder.DropTable(
                name: "MedicalRecord");

            migrationBuilder.DropTable(
                name: "MedicalTechnician");

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
                name: "Employee");

            migrationBuilder.DropTable(
                name: "Patient");

            migrationBuilder.DropTable(
                name: "Employee");

            migrationBuilder.DropTable(
                name: "Phone");

            migrationBuilder.DropTable(
                name: "Location");
        }
    }
}
