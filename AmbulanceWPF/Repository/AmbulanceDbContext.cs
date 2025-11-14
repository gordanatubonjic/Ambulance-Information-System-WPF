using Microsoft.EntityFrameworkCore;
using AmbulanceWPF.Models;
using System.Collections.Generic;
using System.IO;
using AmbulanceWPF.Helper;

namespace AmbulanceWPF.Data
{
    public class AmbulanceDbContext : DbContext
    {
        public DbSet<Phone> Telephones { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<MedicalRecord> MedicalRecords { get; set; }
        public DbSet<DiseaseCatalog> DiseaseCatalogs { get; set; }
        public DbSet<Examination> Examinations { get; set; }
        public DbSet<Diagnosis> Diagnoses { get; set; }
        public DbSet<Intervention> Interventions { get; set; }
        public DbSet<InterventionDoctor> InterventionDoctors { get; set; }
        public DbSet<MedicationCatalog> MedicationCatalogs { get; set; }
        public DbSet<MedicationInventory> MedicationInventories { get; set; }
        public DbSet<Procurement> Procurements { get; set; }
        public DbSet<Therapy> Therapies { get; set; }
        public DbSet<Referral> Referrals { get; set; }
        public DbSet<MedicationItem> MedicationItems { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                                                                   optionsBuilder.UseSqlite("Data Source=../../../AmbulanceDatabase.db");

            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
             
                         modelBuilder.Entity<Phone>(entity =>
            {
                entity.ToTable("Phone");
                entity.HasKey(e => e.PhoneNumber);
                entity.Property(e => e.PhoneNumber)
                      .IsRequired()
                      .HasMaxLength(20);
            });

                         modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("Employee");
                entity.HasKey(e => e.JMB);
                entity.Property(e => e.JMB)
                      .IsRequired()
                      .HasMaxLength(13);
                entity.Property(e => e.Name)
                      .IsRequired()
                      .HasMaxLength(45);
                entity.Property(e => e.LastName)
                      .IsRequired()
                      .HasMaxLength(45);
                entity.Property(e => e.Username)
                      .IsRequired()
                      .HasMaxLength(45);
                entity.Property(e => e.Password)
                      .IsRequired()
                      .HasMaxLength(45);
                entity.Property(e => e.PasswordHash)
                      .IsRequired()
                      .HasMaxLength(45);
                entity.Property(e => e.Role)
                      .IsRequired()
                      .HasMaxLength(45);
                entity.Property(e => e.Theme)
                      .IsRequired()
                      .HasMaxLength(100);
                entity.HasOne(e => e.Phone)
          .WithMany(p => p.Employees)
          .HasForeignKey(e => e.PhoneNumber)
          .OnDelete(DeleteBehavior.Restrict);
            });

             
                         modelBuilder.Entity<Location>(entity =>
            {
                entity.ToTable("Location");
                entity.HasKey(e => e.PostalCode);
                entity.Property(e => e.PostalCode)
                      .IsRequired()
                      .ValueGeneratedNever();
                entity.Property(e => e.Name)
                      .IsRequired()
                      .HasMaxLength(45);
            });

                         modelBuilder.Entity<Patient>(entity =>
            {
                entity.ToTable("Patient");
                entity.HasKey(e => e.JMB);
                entity.Property(e => e.JMB)
                      .IsRequired()
                      .HasMaxLength(13);
                entity.Property(e => e.Name)
                      .IsRequired()
                      .HasMaxLength(45);
                entity.Property(e => e.LastName)
                      .IsRequired()
                      .HasMaxLength(45);

                                 entity.HasOne(p => p.ResidenceLocation)
                      .WithMany()
                      .HasForeignKey(p => p.ResidenceLocationId)
                      .OnDelete(DeleteBehavior.Restrict);

                                 entity.HasIndex(p => p.ResidenceLocationId)
                      .HasDatabaseName("IX_Patient_ResidenceLocationId");
            });

                         modelBuilder.Entity<MedicalRecord>(entity =>
            {
                entity.ToTable("MedicalRecord");
                entity.HasKey(e => e.PatientJMB);
                entity.Property(e => e.PatientJMB)
                      .IsRequired()
                      .HasMaxLength(13);
                entity.Property(e => e.ParentName)
                      .HasMaxLength(45);                  entity.Property(e => e.MaritalStatus)
                      .IsRequired()
                      .HasMaxLength(10);
                entity.Property(e => e.Gender)
                      .IsRequired()
                      .HasConversion<int>();
                entity.Property(e => e.Insurance)
                      .IsRequired()
                      .HasConversion<int>();
                entity.Property(e => e.DoctorJMB)
                      .IsRequired()
                      .HasMaxLength(13);

                                 entity.HasOne(mr => mr.Patient)
                      .WithOne()
                      .HasForeignKey<MedicalRecord>(mr => mr.PatientJMB)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(mr => mr.Employee)
                      .WithMany(d => d.MedicalRecords)
                      .HasForeignKey(mr => mr.DoctorJMB)
                      .OnDelete(DeleteBehavior.Restrict);

                                 entity.HasIndex(mr => mr.PatientJMB)
                      .HasDatabaseName("IX_MedicalRecord_PatientJMB");
                entity.HasIndex(mr => mr.DoctorJMB)
                      .HasDatabaseName("IX_MedicalRecord_DoctorJMB");
            });

                         modelBuilder.Entity<DiseaseCatalog>(entity =>
            {
                entity.ToTable("DiseaseCatalog");
                entity.HasKey(e => e.DiseaseCode);
                entity.Property(e => e.DiseaseCode)
                      .IsRequired()
                      .ValueGeneratedNever();
                entity.Property(e => e.DiseaseName)
                      .IsRequired()
                      .HasMaxLength(45);
                entity.Property(e => e.Description)
                      .IsRequired()
                      .HasColumnType("TEXT");
                entity.Property(e => e.UpdateDate)
                      .IsRequired()
                      .HasColumnType("DATE");
            });

                         modelBuilder.Entity<Examination>(entity =>
            {
                entity.ToTable("Examination");
                entity.HasKey(e => e.ExaminationId);
                entity.Property(e => e.ExaminationId)
                      .ValueGeneratedOnAdd();
                entity.Property(e => e.ExaminationDate)
                      .IsRequired()
                      .HasColumnType("DATETIME");
                entity.Property(e => e.ExaminationDescription)
                      .HasMaxLength(45);
                entity.Property(e => e.PatientJMB)
                      .IsRequired()
                      .HasMaxLength(13);
                entity.Property(e => e.DoctorJMB)
                      .IsRequired()
                      .HasMaxLength(13);

                                 entity.HasOne(e => e.Patient)
                      .WithMany()
                      .HasForeignKey(e => e.PatientJMB)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Employee)
                      .WithMany(d => d.Examinations)
                      .HasForeignKey(e => e.DoctorJMB)
                      .OnDelete(DeleteBehavior.Restrict);

                                                  entity.HasIndex(e => e.PatientJMB)
                      .HasDatabaseName("IX_Examination_PatientJMB");
                entity.HasIndex(e => e.DoctorJMB)
                      .HasDatabaseName("IX_Examination_DoctorJMB");
                entity.HasIndex(e => e.ExaminationDate)
                      .HasDatabaseName("IX_Examination_Date");
            });

                         modelBuilder.Entity<Diagnosis>(entity =>
            {
                entity.ToTable("Diagnosis");
                entity.HasKey(d => new { d.PatientJMB, d.DiseaseCode, d.Date });
                entity.Property(d => d.PatientJMB)
                      .IsRequired()
                      .HasMaxLength(13);
                entity.Property(d => d.DiseaseCode)
                      .IsRequired();
                entity.Property(d => d.Date)
                      .IsRequired()
                      .HasColumnType("DATE");
                entity.Property(d => d.DoctorOpinion)
                      .HasColumnType("TEXT");
                entity.Property(d => d.ExaminationId)
                      .IsRequired();
                entity.Property(d => d.DoctorJMB)
                      .IsRequired()
                      .HasMaxLength(13);

                                 entity.HasOne(d => d.Patient)
                      .WithMany()
                      .HasForeignKey(d => d.PatientJMB)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.Disease)
                      .WithMany(dc => dc.Diagnoses)
                      .HasForeignKey(d => d.DiseaseCode)
                      .OnDelete(DeleteBehavior.Restrict);

                                                  entity.HasOne(d => d.Examination)
                      .WithMany(e => e.Diagnoses)
                      .HasForeignKey(d => d.ExaminationId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.Employee)
                      .WithMany()
                      .HasForeignKey(d => d.DoctorJMB)
                      .OnDelete(DeleteBehavior.Restrict);

                                 entity.HasIndex(d => d.DiseaseCode)
                      .HasDatabaseName("IX_Diagnosis_DiseaseCode");
                entity.HasIndex(d => d.PatientJMB)
                      .HasDatabaseName("IX_Diagnosis_PatientJMB");
                                 entity.HasIndex(d => d.ExaminationId)
                      .HasDatabaseName("IX_Diagnosis_ExaminationId");
                entity.HasIndex(d => d.Date)
                      .HasDatabaseName("IX_Diagnosis_Date");
            });

                         modelBuilder.Entity<Intervention>(entity =>
            {
                entity.ToTable("Intervention");
                entity.HasKey(e => e.InterventionId);
                entity.Property(e => e.InterventionId)
                      .ValueGeneratedOnAdd();
                entity.Property(e => e.PatientJMB)
                      .IsRequired()
                      .HasMaxLength(13);
                entity.Property(e => e.Date)
                      .IsRequired()
                      .HasColumnType("DATE");
                entity.Property(e => e.InterventionDescription)
                      .IsRequired()
                      .HasColumnType("TEXT");

                                 entity.HasOne(i => i.Patient)
                      .WithMany()
                      .HasForeignKey(i => i.PatientJMB)
                      .OnDelete(DeleteBehavior.Restrict);

                                 entity.HasIndex(i => i.PatientJMB)
                      .HasDatabaseName("IX_Intervention_PatientJMB");
                entity.HasIndex(i => i.Date)
                      .HasDatabaseName("IX_Intervention_Date");
            });

                         modelBuilder.Entity<InterventionDoctor>(entity =>
            {
                entity.ToTable("InterventionDoctor");
                entity.HasKey(id => new { id.InterventionId, id.DoctorJMB });
                entity.Property(id => id.InterventionId)
                      .IsRequired();
                entity.Property(id => id.DoctorJMB)
                      .IsRequired()
                      .HasMaxLength(13);

                                 entity.HasOne(id => id.Intervention)
                      .WithMany(i => i.InterventionDoctors)
                      .HasForeignKey(id => id.InterventionId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(id => id.Employee)
                      .WithMany(d => d.InterventionDoctors)
                      .HasForeignKey(id => id.DoctorJMB)
                      .OnDelete(DeleteBehavior.Restrict);

                                 entity.HasIndex(id => id.InterventionId)
                      .HasDatabaseName("IX_InterventionDoctor_InterventionId");
                entity.HasIndex(id => id.DoctorJMB)
                      .HasDatabaseName("IX_InterventionDoctor_DoctorJMB");
            });

                         modelBuilder.Entity<MedicationCatalog>(entity =>
            {
                entity.ToTable("MedicationCatalog");
                entity.HasKey(e => e.MedicationCode);
                entity.Property(e => e.MedicationCode)
                      .IsRequired()
                      .ValueGeneratedNever();
                entity.Property(e => e.Name)
                      .IsRequired()
                      .HasMaxLength(45);
                entity.Property(e => e.Manufacturer)
                      .IsRequired()
                      .HasMaxLength(45);
                entity.Property(e => e.IsActive)
                      .IsRequired()
                      .HasDefaultValue(true);  
                                 entity.HasIndex(m => m.Name)
                      .HasDatabaseName("IX_MedicationCatalog_Name");
                entity.HasIndex(m => m.Manufacturer)
                      .HasDatabaseName("IX_MedicationCatalog_Manufacturer");
                entity.HasIndex(m => m.IsActive)
                      .HasDatabaseName("IX_MedicationCatalog_IsActive");
            });

                         modelBuilder.Entity<MedicationInventory>(entity =>
            {
                entity.ToTable("MedicationInventory");
                entity.HasKey(e => e.MedicationCode);
                entity.Property(e => e.MedicationCode)
                      .IsRequired();
                entity.Property(e => e.Quantity)
                      .IsRequired()
                      .HasColumnType("REAL");

                                 entity.HasOne(mi => mi.Medication)
                      .WithOne(m => m.MedicationInventory)
                      .HasForeignKey<MedicationInventory>(mi => mi.MedicationCode)
                      .OnDelete(DeleteBehavior.Restrict);
            });

             
                         modelBuilder.Entity<Procurement>(entity =>
            {
                entity.ToTable("Procurement");
                entity.HasKey(e => e.ProcurementId);
                entity.Property(e => e.ProcurementId)
                      .ValueGeneratedOnAdd();
                                 entity.Property(e => e.ProcurementDate)
                      .IsRequired()
                      .HasColumnType("DATE");

                                 entity.HasIndex(p => p.ProcurementDate)
                      .HasDatabaseName("IX_Procurement_Date");
            });

                         modelBuilder.Entity<Therapy>(entity =>
            {
                entity.ToTable("Therapy");
                entity.HasKey(t => new { t.InterventionId, t.MedicationCode });
                entity.Property(t => t.InterventionId)
                      .IsRequired();
                entity.Property(t => t.MedicationCode)
                      .IsRequired();
                entity.Property(t => t.Dosage)
                      .HasColumnType("REAL");

                                 entity.HasOne(t => t.Intervention)
                      .WithMany(i => i.Therapies)
                      .HasForeignKey(t => t.InterventionId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(t => t.Medication)
                      .WithMany(m => m.Therapies)
                      .HasForeignKey(t => t.MedicationCode)
                      .OnDelete(DeleteBehavior.Restrict);

                                 entity.HasIndex(t => t.InterventionId)
                      .HasDatabaseName("IX_Therapy_InterventionId");
                entity.HasIndex(t => t.MedicationCode)
                      .HasDatabaseName("IX_Therapy_MedicationCode");
            });

                         modelBuilder.Entity<Referral>(entity =>
            {
                entity.ToTable("Referral");
                entity.HasKey(e => e.ReferralId);
                entity.Property(e => e.ReferralId)
                      .ValueGeneratedOnAdd();
                entity.Property(e => e.DiseaseCode)
                      .IsRequired();
                entity.Property(e => e.Specialists)
                      .IsRequired()
                      .HasMaxLength(45);
                entity.Property(e => e.DoctorJMB)
                      .IsRequired()
                      .HasMaxLength(13);
                entity.Property(e => e.Date)
                      .IsRequired()
                      .HasColumnType("DATE");
                entity.Property(e => e.ExaminationId)
                      .IsRequired();

                                 entity.HasOne(r => r.Disease)
                      .WithMany()
                      .HasForeignKey(r => r.DiseaseCode)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(r => r.Employee)
                      .WithMany(d => d.Referrals)
                      .HasForeignKey(r => r.DoctorJMB)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(r => r.Examination)
                      .WithMany(e => e.Referrals)
                      .HasForeignKey(r => r.ExaminationId)
                      .OnDelete(DeleteBehavior.Restrict);

                                 entity.HasIndex(r => r.DiseaseCode)
                      .HasDatabaseName("IX_Referral_DiseaseCode");
                entity.HasIndex(r => r.DoctorJMB)
                      .HasDatabaseName("IX_Referral_DoctorJMB");
                entity.HasIndex(r => r.ExaminationId)
                      .HasDatabaseName("IX_Referral_ExaminationId");
                entity.HasIndex(r => r.Date)
                      .HasDatabaseName("IX_Referral_Date");
            });

                         modelBuilder.Entity<MedicationItem>(entity =>
            {
                entity.ToTable("MedicationItem");
                entity.HasKey(mi => new { mi.MedicationCode, mi.ProcurementId });
                entity.Property(mi => mi.MedicationCode)
                      .IsRequired();
                entity.Property(mi => mi.ProcurementId)
                      .IsRequired();
                entity.Property(mi => mi.Quantity)
                      .IsRequired();

                                 entity.HasOne(mi => mi.Medication)
                      .WithMany(m => m.MedicationItems)
                      .HasForeignKey(mi => mi.MedicationCode)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(mi => mi.Procurement)
                      .WithMany(p => p.MedicationItems)
                      .HasForeignKey(mi => mi.ProcurementId)
                      .OnDelete(DeleteBehavior.Restrict);

                                 entity.HasIndex(mi => mi.MedicationCode)
                      .HasDatabaseName("IX_MedicationItem_MedicationCode");
                entity.HasIndex(mi => mi.ProcurementId)
                      .HasDatabaseName("IX_MedicationItem_ProcurementId");


            });
            //
                         modelBuilder.Entity<Phone>().HasData(
                new Phone { PhoneNumber = "+38761111222" },
                new Phone { PhoneNumber = "+38762123456" }
            );

            modelBuilder.Entity<Location>().HasData(
                new Location { PostalCode = 71000, Name = "Sarajevo" },
                new Location { PostalCode = 78000, Name = "Banja Luka" }
            );

                         modelBuilder.Entity<Employee>().HasData(
     new Employee
     {
         JMB = "6482157394021",
         Name = "Amar",
         LastName = "Kovacevic",
         Username = "amar.k",
         Password = "amarovasifra",
         PasswordHash = "amarovasifra",
         Role = "Doctor",
         IsActive = 1,
         Theme = "Light",
         PhoneNumber = "+38761111222"       },
     new Employee
     {
         JMB = "9035172846109",
         Name = "Mila",
         LastName = "Juric",
         Username = "mila.j",
         Password = "milinasifra",
         PasswordHash = "amarovasifra",
         Role = "MedicalTechnician",
         IsActive = 1,
         Theme = "Dark"
               }
 );
             
            modelBuilder.Entity<Patient>().HasData(
                new Patient
                {
                    JMB = "5729618430725",
                    Name = "Ivan",
                    LastName = "Horvat",
                    ResidenceLocationId = 71000
                },
                new Patient
                {
                    JMB = "4185270936518",
                    Name = "Sara",
                    LastName = "Petrovic",
                    ResidenceLocationId = 78000
                }
            );

            modelBuilder.Entity<MedicalRecord>().HasData(
                new MedicalRecord
                {
                    PatientJMB = "5729618430725",
                    ParentName = "Marko",
                    MaritalStatus = "Single",
                    Gender = false,                          Insurance = false,                       DoctorJMB = "6482157394021"
                },
                new MedicalRecord
                {
                    PatientJMB = "4185270936518",
                    ParentName = "Ivana",
                    MaritalStatus = "Married",
                    Gender = true,
                    Insurance = false,
                    DoctorJMB = "6482157394021"
                }
            );

            modelBuilder.Entity<DiseaseCatalog>().HasData(
                new DiseaseCatalog
                {
                    DiseaseCode = 1001,
                    DiseaseName = "Hypertension",
                    Description = "Persistent high arterial blood pressure.",
                    UpdateDate = new DateTime(2025, 1, 15)
                },
                new DiseaseCatalog
                {
                    DiseaseCode = 1002,
                    DiseaseName = "Acute bronchitis",
                    Description = "Inflammation of the bronchial tubes, often viral.",
                    UpdateDate = new DateTime(2025, 1, 15)
                }
            );

            modelBuilder.Entity<MedicationCatalog>().HasData(
                new MedicationCatalog
                {
                    MedicationCode = 2001,
                    Name = "Lisinopril",
                    Manufacturer = "ACME Pharma",
                    IsActive = true
                },
                new MedicationCatalog
                {
                    MedicationCode = 2002,
                    Name = "Salbutamol",
                    Manufacturer = "BreatheWell",
                    IsActive = true
                }
            );

            modelBuilder.Entity<MedicationInventory>().HasData(
                new MedicationInventory { MedicationCode = 2001, Quantity = (decimal)25.0 },
                new MedicationInventory { MedicationCode = 2002, Quantity = (decimal)40.0 }
            );

            modelBuilder.Entity<Procurement>().HasData(
                new Procurement
                {
                    ProcurementId = 1,
                    ProcurementDate = new DateTime(2025, 2, 1)
                },
                new Procurement
                {
                    ProcurementId = 2,
                    ProcurementDate = new DateTime(2025, 3, 10)
                }
            );

            modelBuilder.Entity<MedicationItem>().HasData(
                new MedicationItem
                {
                    MedicationCode = 2001,
                    ProcurementId = 1,
                    Quantity = 50
                },
                new MedicationItem
                {
                    MedicationCode = 2002,
                    ProcurementId = 2,
                    Quantity = 100
                }
            );

            modelBuilder.Entity<Examination>().HasData(
                new Examination
                {
                    ExaminationId = 1,
                    ExaminationDate = new DateTime(2025, 4, 20, 10, 30, 0),
                    ExaminationDescription = "Routine checkup",
                    PatientJMB = "5729618430725",
                    DoctorJMB = "6482157394021"
                },
                new Examination
                {
                    ExaminationId = 2,
                    ExaminationDate = new DateTime(2025, 5, 5, 9, 0, 0),
                    ExaminationDescription = "Cough and wheezing",
                    PatientJMB = "4185270936518",
                    DoctorJMB = "6482157394021"
                }
            );

            modelBuilder.Entity<Diagnosis>().HasData(
                new Diagnosis
                {
                    PatientJMB = "5729618430725",
                    DiseaseCode = 1001,
                    Date = new DateTime(2025, 4, 20),
                    DoctorOpinion = "Stage 1 hypertension; monitor BP.",
                    ExaminationId = 1,
                    DoctorJMB = "6482157394021"
                },
                new Diagnosis
                {
                    PatientJMB = "4185270936518",
                    DiseaseCode = 1002,
                    Date = new DateTime(2025, 5, 5),
                    DoctorOpinion = "Likely viral origin.",
                    ExaminationId = 2,
                    DoctorJMB = "6482157394021"
                }
            );

            modelBuilder.Entity<Intervention>().HasData(
                new Intervention
                {
                    InterventionId = 1,
                    PatientJMB = "4185270936518",
                    Date = new DateTime(2025, 5, 5),
                    InterventionDescription = "Nebulization therapy administered."
                }
            );

            modelBuilder.Entity<InterventionDoctor>().HasData(
                new InterventionDoctor
                {
                    InterventionId = 1,
                    DoctorJMB = "6482157394021"
                }
            );

            modelBuilder.Entity<Therapy>().HasData(
                new Therapy
                {
                    InterventionId = 1,
                    MedicationCode = 2002,
                    Dosage = (decimal)2.5
                }
            );

            modelBuilder.Entity<Referral>().HasData(
                new Referral
                {
                    ReferralId = 1,
                    DiseaseCode = 1002,
                    Specialists = "Pulmonologist",
                    DoctorJMB = "6482157394021",
                    Date = new DateTime(2025, 5, 5),
                    ExaminationId = 2
                }
            );
             
            //

            base.OnModelCreating(modelBuilder);
        }
    }
}