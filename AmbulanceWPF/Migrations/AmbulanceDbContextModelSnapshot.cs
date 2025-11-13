 using System;
using AmbulanceWPF.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AmbulanceWPF.Migrations
{
    [DbContext(typeof(AmbulanceDbContext))]
    partial class AmbulanceDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.10");

            modelBuilder.Entity("AmbulanceWPF.Models.Diagnosis", b =>
                {
                    b.Property<string>("PatientJMB")
                        .HasMaxLength(13)
                        .HasColumnType("TEXT")
                        .HasColumnOrder(1);

                    b.Property<int>("DiseaseCode")
                        .HasColumnType("INTEGER")
                        .HasColumnOrder(2);

                    b.Property<DateTime>("Date")
                        .HasColumnType("DATE")
                        .HasColumnOrder(3);

                    b.Property<string>("DoctorJMB")
                        .IsRequired()
                        .HasMaxLength(13)
                        .HasColumnType("TEXT");

                    b.Property<string>("DoctorJMB1")
                        .HasColumnType("TEXT");

                    b.Property<string>("DoctorOpinion")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("ExaminationId")
                        .HasColumnType("INTEGER");

                    b.HasKey("PatientJMB", "DiseaseCode", "Date");

                    b.HasIndex("Date")
                        .HasDatabaseName("IX_Diagnosis_Date");

                    b.HasIndex("DiseaseCode")
                        .HasDatabaseName("IX_Diagnosis_DiseaseCode");

                    b.HasIndex("DoctorJMB");

                    b.HasIndex("DoctorJMB1");

                    b.HasIndex("ExaminationId")
                        .HasDatabaseName("IX_Diagnosis_ExaminationId");

                    b.HasIndex("PatientJMB")
                        .HasDatabaseName("IX_Diagnosis_PatientJMB");

                    b.ToTable("Diagnosis", (string)null);
                });

            modelBuilder.Entity("AmbulanceWPF.Models.DiseaseCatalog", b =>
                {
                    b.Property<int>("DiseaseCode")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("DiseaseName")
                        .IsRequired()
                        .HasMaxLength(45)
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("DATE");

                    b.HasKey("DiseaseCode");

                    b.ToTable("DiseaseCatalog", (string)null);
                });

            modelBuilder.Entity("AmbulanceWPF.Models.Employee", b =>
                {
                    b.Property<string>("JMB")
                        .HasMaxLength(13)
                        .HasColumnType("TEXT");

                    b.Property<int>("IsActive")
                        .HasColumnType("INTEGER");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(45)
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(45)
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(45)
                        .HasColumnType("TEXT");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasMaxLength(45)
                        .HasColumnType("TEXT");

                    b.Property<string>("Theme")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(45)
                        .HasColumnType("TEXT");

                    b.HasKey("JMB");

                    b.ToTable("Employee", (string)null);

                    b.UseTptMappingStrategy();
                });

            modelBuilder.Entity("AmbulanceWPF.Models.Examination", b =>
                {
                    b.Property<int>("ExaminationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("DoctorJMB")
                        .IsRequired()
                        .HasMaxLength(13)
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("ExaminationDate")
                        .HasColumnType("DATETIME");

                    b.Property<string>("ExaminationDescription")
                        .IsRequired()
                        .HasMaxLength(45)
                        .HasColumnType("TEXT");

                    b.Property<string>("PatientJMB")
                        .IsRequired()
                        .HasMaxLength(13)
                        .HasColumnType("TEXT");

                    b.Property<string>("PatientJMB1")
                        .HasColumnType("TEXT");

                    b.HasKey("ExaminationId");

                    b.HasIndex("DoctorJMB")
                        .HasDatabaseName("IX_Examination_DoctorJMB");

                    b.HasIndex("ExaminationDate")
                        .HasDatabaseName("IX_Examination_Date");

                    b.HasIndex("PatientJMB")
                        .HasDatabaseName("IX_Examination_PatientJMB");

                    b.HasIndex("PatientJMB1");

                    b.ToTable("Examination", (string)null);
                });

            modelBuilder.Entity("AmbulanceWPF.Models.Intervention", b =>
                {
                    b.Property<int>("InterventionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Date")
                        .HasColumnType("DATE");

                    b.Property<string>("InterventionDescription")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("PatientJMB")
                        .IsRequired()
                        .HasMaxLength(13)
                        .HasColumnType("TEXT");

                    b.HasKey("InterventionId");

                    b.HasIndex("Date")
                        .HasDatabaseName("IX_Intervention_Date");

                    b.HasIndex("PatientJMB")
                        .HasDatabaseName("IX_Intervention_PatientJMB");

                    b.ToTable("Intervention", (string)null);
                });

            modelBuilder.Entity("AmbulanceWPF.Models.InterventionDoctor", b =>
                {
                    b.Property<int>("InterventionId")
                        .HasColumnType("INTEGER")
                        .HasColumnOrder(1);

                    b.Property<string>("DoctorJMB")
                        .HasMaxLength(13)
                        .HasColumnType("TEXT")
                        .HasColumnOrder(2);

                    b.HasKey("InterventionId", "DoctorJMB");

                    b.HasIndex("DoctorJMB")
                        .HasDatabaseName("IX_InterventionDoctor_DoctorJMB");

                    b.HasIndex("InterventionId")
                        .HasDatabaseName("IX_InterventionDoctor_InterventionId");

                    b.ToTable("InterventionDoctor", (string)null);
                });

            modelBuilder.Entity("AmbulanceWPF.Models.Location", b =>
                {
                    b.Property<int>("PostalCode")
                        .HasColumnType("INTEGER")
                        .HasColumnName("PostalCode");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(45)
                        .HasColumnType("TEXT");

                    b.HasKey("PostalCode");

                    b.ToTable("Location", (string)null);
                });

            modelBuilder.Entity("AmbulanceWPF.Models.MedicalRecord", b =>
                {
                    b.Property<string>("PatientJMB")
                        .HasMaxLength(13)
                        .HasColumnType("TEXT");

                    b.Property<string>("DoctorJMB")
                        .IsRequired()
                        .HasMaxLength(13)
                        .HasColumnType("TEXT");

                    b.Property<int>("Gender")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Insurance")
                        .HasColumnType("INTEGER");

                    b.Property<string>("MaritalStatus")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("TEXT");

                    b.Property<string>("ParentName")
                        .IsRequired()
                        .HasMaxLength(45)
                        .HasColumnType("TEXT");

                    b.HasKey("PatientJMB");

                    b.HasIndex("DoctorJMB")
                        .HasDatabaseName("IX_MedicalRecord_DoctorJMB");

                    b.HasIndex("PatientJMB")
                        .HasDatabaseName("IX_MedicalRecord_PatientJMB");

                    b.ToTable("MedicalRecord", (string)null);
                });

            modelBuilder.Entity("AmbulanceWPF.Models.MedicationCatalog", b =>
                {
                    b.Property<int>("MedicationCode")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasDefaultValue(true);

                    b.Property<string>("Manufacturer")
                        .IsRequired()
                        .HasMaxLength(45)
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(45)
                        .HasColumnType("TEXT");

                    b.HasKey("MedicationCode");

                    b.HasIndex("IsActive")
                        .HasDatabaseName("IX_MedicationCatalog_IsActive");

                    b.HasIndex("Manufacturer")
                        .HasDatabaseName("IX_MedicationCatalog_Manufacturer");

                    b.HasIndex("Name")
                        .HasDatabaseName("IX_MedicationCatalog_Name");

                    b.ToTable("MedicationCatalog", (string)null);
                });

            modelBuilder.Entity("AmbulanceWPF.Models.MedicationInventory", b =>
                {
                    b.Property<int>("MedicationCode")
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("Quantity")
                        .HasColumnType("REAL");

                    b.HasKey("MedicationCode");

                    b.ToTable("MedicationInventory", (string)null);
                });

            modelBuilder.Entity("AmbulanceWPF.Models.MedicationItem", b =>
                {
                    b.Property<int>("MedicationCode")
                        .HasColumnType("INTEGER")
                        .HasColumnOrder(1);

                    b.Property<int>("ProcurementId")
                        .HasColumnType("INTEGER")
                        .HasColumnOrder(2);

                    b.Property<int>("Quantity")
                        .HasColumnType("INTEGER");

                    b.HasKey("MedicationCode", "ProcurementId");

                    b.HasIndex("MedicationCode")
                        .HasDatabaseName("IX_MedicationItem_MedicationCode");

                    b.HasIndex("ProcurementId")
                        .HasDatabaseName("IX_MedicationItem_ProcurementId");

                    b.ToTable("MedicationItem", (string)null);
                });

            modelBuilder.Entity("AmbulanceWPF.Models.Patient", b =>
                {
                    b.Property<string>("JMB")
                        .HasMaxLength(13)
                        .HasColumnType("TEXT")
                        .HasColumnName("JMB");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(45)
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(45)
                        .HasColumnType("TEXT");

                    b.Property<int>("ResidenceLocationId")
                        .HasColumnType("INTEGER");

                    b.HasKey("JMB");

                    b.HasIndex("ResidenceLocationId")
                        .HasDatabaseName("IX_Patient_ResidenceLocationId");

                    b.ToTable("Patient", (string)null);
                });

            modelBuilder.Entity("AmbulanceWPF.Models.Phone", b =>
                {
                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(20)
                        .HasColumnType("TEXT");

                    b.HasKey("PhoneNumber");

                    b.ToTable("Phone", (string)null);
                });

            modelBuilder.Entity("AmbulanceWPF.Models.Procurement", b =>
                {
                    b.Property<int>("ProcurementId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("ProcurementDate")
                        .HasColumnType("DATE");

                    b.Property<decimal?>("Quantity")
                        .HasColumnType("REAL");

                    b.HasKey("ProcurementId");

                    b.HasIndex("ProcurementDate")
                        .HasDatabaseName("IX_Procurement_Date");

                    b.ToTable("Procurement", (string)null);
                });

            modelBuilder.Entity("AmbulanceWPF.Models.Referral", b =>
                {
                    b.Property<int>("ReferralId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Date")
                        .HasColumnType("DATE");

                    b.Property<int>("DiseaseCode")
                        .HasColumnType("INTEGER");

                    b.Property<string>("DoctorJMB")
                        .IsRequired()
                        .HasMaxLength(13)
                        .HasColumnType("TEXT");

                    b.Property<int>("ExaminationId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Specialists")
                        .IsRequired()
                        .HasMaxLength(45)
                        .HasColumnType("TEXT");

                    b.HasKey("ReferralId");

                    b.HasIndex("Date")
                        .HasDatabaseName("IX_Referral_Date");

                    b.HasIndex("DiseaseCode")
                        .HasDatabaseName("IX_Referral_DiseaseCode");

                    b.HasIndex("DoctorJMB")
                        .HasDatabaseName("IX_Referral_DoctorJMB");

                    b.HasIndex("ExaminationId")
                        .HasDatabaseName("IX_Referral_ExaminationId");

                    b.ToTable("Referral", (string)null);
                });

            modelBuilder.Entity("AmbulanceWPF.Models.Therapy", b =>
                {
                    b.Property<int>("InterventionId")
                        .HasColumnType("INTEGER")
                        .HasColumnOrder(1);

                    b.Property<int>("MedicationCode")
                        .HasColumnType("INTEGER")
                        .HasColumnOrder(2);

                    b.Property<decimal?>("Dosage")
                        .HasColumnType("REAL");

                    b.HasKey("InterventionId", "MedicationCode");

                    b.HasIndex("InterventionId")
                        .HasDatabaseName("IX_Therapy_InterventionId");

                    b.HasIndex("MedicationCode")
                        .HasDatabaseName("IX_Therapy_MedicationCode");

                    b.ToTable("Therapy", (string)null);
                });

            modelBuilder.Entity("AmbulanceWPF.Models.Employee", b =>
                {
                    b.HasBaseType("AmbulanceWPF.Models.Employee");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("TEXT");

                    b.HasIndex("PhoneNumber");

                    b.ToTable("Employee", (string)null);
                });

            modelBuilder.Entity("AmbulanceWPF.Models.MedicalTechnician", b =>
                {
                    b.HasBaseType("AmbulanceWPF.Models.Employee");

                    b.ToTable("MedicalTechnician", (string)null);
                });

            modelBuilder.Entity("AmbulanceWPF.Models.Diagnosis", b =>
                {
                    b.HasOne("AmbulanceWPF.Models.DiseaseCatalog", "Disease")
                        .WithMany("Diagnoses")
                        .HasForeignKey("DiseaseCode")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("AmbulanceWPF.Models.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("DoctorJMB")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("AmbulanceWPF.Models.Employee", null)
                        .WithMany("Diagnosis")
                        .HasForeignKey("DoctorJMB1");

                    b.HasOne("AmbulanceWPF.Models.Examination", "Examination")
                        .WithMany("Diagnoses")
                        .HasForeignKey("ExaminationId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("AmbulanceWPF.Models.Patient", "Patient")
                        .WithMany()
                        .HasForeignKey("PatientJMB")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Disease");

                    b.Navigation("Employee");

                    b.Navigation("Examination");

                    b.Navigation("Patient");
                });

            modelBuilder.Entity("AmbulanceWPF.Models.Examination", b =>
                {
                    b.HasOne("AmbulanceWPF.Models.Employee", "Employee")
                        .WithMany("Examinations")
                        .HasForeignKey("DoctorJMB")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("AmbulanceWPF.Models.Patient", "Patient")
                        .WithMany()
                        .HasForeignKey("PatientJMB")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("AmbulanceWPF.Models.Patient", null)
                        .WithMany("Examinations")
                        .HasForeignKey("PatientJMB1");

                    b.Navigation("Employee");

                    b.Navigation("Patient");
                });

            modelBuilder.Entity("AmbulanceWPF.Models.Intervention", b =>
                {
                    b.HasOne("AmbulanceWPF.Models.Patient", "Patient")
                        .WithMany()
                        .HasForeignKey("PatientJMB")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Patient");
                });

            modelBuilder.Entity("AmbulanceWPF.Models.InterventionDoctor", b =>
                {
                    b.HasOne("AmbulanceWPF.Models.Employee", "Employee")
                        .WithMany("InterventionDoctors")
                        .HasForeignKey("DoctorJMB")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("AmbulanceWPF.Models.Intervention", "Intervention")
                        .WithMany("InterventionDoctors")
                        .HasForeignKey("InterventionId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Employee");

                    b.Navigation("Intervention");
                });

            modelBuilder.Entity("AmbulanceWPF.Models.MedicalRecord", b =>
                {
                    b.HasOne("AmbulanceWPF.Models.Employee", "Employee")
                        .WithMany("MedicalRecords")
                        .HasForeignKey("DoctorJMB")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("AmbulanceWPF.Models.Patient", "Patient")
                        .WithOne()
                        .HasForeignKey("AmbulanceWPF.Models.MedicalRecord", "PatientJMB")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Employee");

                    b.Navigation("Patient");
                });

            modelBuilder.Entity("AmbulanceWPF.Models.MedicationInventory", b =>
                {
                    b.HasOne("AmbulanceWPF.Models.MedicationCatalog", "Medication")
                        .WithOne("MedicationInventory")
                        .HasForeignKey("AmbulanceWPF.Models.MedicationInventory", "MedicationCode")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Medication");
                });

            modelBuilder.Entity("AmbulanceWPF.Models.MedicationItem", b =>
                {
                    b.HasOne("AmbulanceWPF.Models.MedicationCatalog", "Medication")
                        .WithMany("MedicationItems")
                        .HasForeignKey("MedicationCode")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("AmbulanceWPF.Models.Procurement", "Procurement")
                        .WithMany("MedicationItems")
                        .HasForeignKey("ProcurementId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Medication");

                    b.Navigation("Procurement");
                });

            modelBuilder.Entity("AmbulanceWPF.Models.Patient", b =>
                {
                    b.HasOne("AmbulanceWPF.Models.Location", "ResidenceLocation")
                        .WithMany()
                        .HasForeignKey("ResidenceLocationId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("ResidenceLocation");
                });

            modelBuilder.Entity("AmbulanceWPF.Models.Referral", b =>
                {
                    b.HasOne("AmbulanceWPF.Models.DiseaseCatalog", "Disease")
                        .WithMany()
                        .HasForeignKey("DiseaseCode")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("AmbulanceWPF.Models.Employee", "Employee")
                        .WithMany("Referrals")
                        .HasForeignKey("DoctorJMB")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("AmbulanceWPF.Models.Examination", "Examination")
                        .WithMany("Referrals")
                        .HasForeignKey("ExaminationId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Disease");

                    b.Navigation("Employee");

                    b.Navigation("Examination");
                });

            modelBuilder.Entity("AmbulanceWPF.Models.Therapy", b =>
                {
                    b.HasOne("AmbulanceWPF.Models.Intervention", "Intervention")
                        .WithMany("Therapies")
                        .HasForeignKey("InterventionId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("AmbulanceWPF.Models.MedicationCatalog", "Medication")
                        .WithMany("Therapies")
                        .HasForeignKey("MedicationCode")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Intervention");

                    b.Navigation("Medication");
                });

            modelBuilder.Entity("AmbulanceWPF.Models.Employee", b =>
                {
                    b.HasOne("AmbulanceWPF.Models.Employee", "Employee")
                        .WithOne("Employee")
                        .HasForeignKey("AmbulanceWPF.Models.Employee", "JMB")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("AmbulanceWPF.Models.Phone", "Phone")
                        .WithMany("Doctors")
                        .HasForeignKey("PhoneNumber")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Employee");

                    b.Navigation("Phone");
                });

            modelBuilder.Entity("AmbulanceWPF.Models.MedicalTechnician", b =>
                {
                    b.HasOne("AmbulanceWPF.Models.Employee", "Employee")
                        .WithOne("MedicalTechnician")
                        .HasForeignKey("AmbulanceWPF.Models.MedicalTechnician", "JMB")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("AmbulanceWPF.Models.DiseaseCatalog", b =>
                {
                    b.Navigation("Diagnoses");
                });

            modelBuilder.Entity("AmbulanceWPF.Models.Employee", b =>
                {
                    b.Navigation("Employee")
                        .IsRequired();

                    b.Navigation("MedicalTechnician")
                        .IsRequired();
                });

            modelBuilder.Entity("AmbulanceWPF.Models.Examination", b =>
                {
                    b.Navigation("Diagnoses");

                    b.Navigation("Referrals");
                });

            modelBuilder.Entity("AmbulanceWPF.Models.Intervention", b =>
                {
                    b.Navigation("InterventionDoctors");

                    b.Navigation("Therapies");
                });

            modelBuilder.Entity("AmbulanceWPF.Models.MedicationCatalog", b =>
                {
                    b.Navigation("MedicationInventory")
                        .IsRequired();

                    b.Navigation("MedicationItems");

                    b.Navigation("Therapies");
                });

            modelBuilder.Entity("AmbulanceWPF.Models.Patient", b =>
                {
                    b.Navigation("Examinations");
                });

            modelBuilder.Entity("AmbulanceWPF.Models.Phone", b =>
                {
                    b.Navigation("Doctors");
                });

            modelBuilder.Entity("AmbulanceWPF.Models.Procurement", b =>
                {
                    b.Navigation("MedicationItems");
                });

            modelBuilder.Entity("AmbulanceWPF.Models.Employee", b =>
                {
                    b.Navigation("Diagnosis");

                    b.Navigation("Examinations");

                    b.Navigation("InterventionDoctors");

                    b.Navigation("MedicalRecords");

                    b.Navigation("Referrals");
                });
#pragma warning restore 612, 618
        }
    }
}
