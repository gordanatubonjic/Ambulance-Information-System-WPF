// Placeholder for IPatientService (create in Services namespace)
using System.Threading.Tasks;
using AmbulanceWPF.Data;
using AmbulanceWPF.Models;
using Microsoft.EntityFrameworkCore;

namespace AmbulanceWPF.Services
{
    public interface IPatientService
    {
        Task<MedicalRecord> GetMedicalRecordAsync(string patientJMB);
        Task SaveMedicalRecordAsync(MedicalRecord record);
        Task SaveExaminationAsync(Examination exam);
        Task SaveReferralAsync(Referral referral);
        Task SaveDiagnosisAsync(Diagnosis diagnosis);
        // Implement with EF Core or your ORM/DB context
    }

    // Example stub implementation (replace with real DB logic)
    public class PatientService : IPatientService
    {
        // Use DbContext here in real app
        private AmbulanceDbContext _context = new AmbulanceDbContext();

        public async Task<MedicalRecord> GetMedicalRecordAsync(string patientJMB)
        {
            //TODO Null Exception
            return await _context.MedicalRecords
                    .Include(mr => mr.Patient)
                    .Include(mr => mr.Employee)
                    .FirstOrDefaultAsync(mr => mr.PatientJMB == patientJMB);
        }

        public Task SaveMedicalRecordAsync(MedicalRecord record) => Task.CompletedTask;
        public Task SaveExaminationAsync(Examination exam) => Task.CompletedTask;
        public Task SaveReferralAsync(Referral referral) => Task.CompletedTask;
        public Task SaveDiagnosisAsync(Diagnosis diagnosis) => Task.CompletedTask;
    }
}