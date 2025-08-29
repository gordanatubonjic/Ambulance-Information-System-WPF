using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using AmbulanceWPF.Views;
using System.Collections.ObjectModel;

namespace AmbulanceWPF.ViewModels
{
    public class PatientHistoryViewModel
    {
        // Add this method to your ViewModel or wherever you initialize the data
        public ObservableCollection<CheckUp> InitializeSampleCheckUps()
        {
            return new ObservableCollection<CheckUp>
    {
        new CheckUp
        {
            CheckupDate = DateTime.Now.AddDays(-7),
            DoctorName = "Dr. Sarah Johnson - Cardiologist",
            Summary = "Routine cardiac follow-up. Blood pressure well controlled, no chest pain reported. ECG shows normal sinus rhythm.",
            CheckupType = "Follow-up",
            DetailedDescription = "Patient presents for routine 3-month cardiac follow-up. Reports good adherence to medications and dietary recommendations. Blood pressure readings at home have been consistently within target range (120-130/70-80 mmHg). No episodes of chest pain, shortness of breath, or palpitations since last visit. Physical examination reveals regular heart rate and rhythm, no murmurs or gallops detected. Lungs clear to auscultation bilaterally.",
            Diagnosis = "Hypertension, well controlled\nCoronary artery disease, stable",
            Medications = "Lisinopril 10mg daily\nAtorvastatin 20mg at bedtime\nAspirin 81mg daily\nMetoprolol 50mg twice daily",
            TestResults = new List<string>
            {
                "ECG: Normal sinus rhythm, no acute changes",
                "Blood Pressure: 128/76 mmHg",
                "Heart Rate: 68 bpm",
                "Weight: 180 lbs (stable)"
            },
            Notes = "Patient doing well on current regimen. Continue current medications. Encouraged to maintain regular exercise routine and low-sodium diet.",
            NextAppointment = "Follow-up in 3 months (April 2025)"
        },

        new CheckUp
        {
            CheckupDate = DateTime.Now.AddDays(-35),
            DoctorName = "Dr. Michael Chen - Primary Care",
            Summary = "Annual physical examination. Overall health good. Routine lab work ordered for diabetes and cholesterol monitoring.",
            CheckupType = "Annual",
            DetailedDescription = "Comprehensive annual physical examination for 45-year-old patient. Review of systems negative except for occasional mild joint stiffness in the morning. Patient reports regular exercise 3-4 times per week and generally healthy diet. Family history significant for diabetes (father) and heart disease (mother). Physical exam notable for mild obesity (BMI 28.5) but otherwise unremarkable. Skin examination reveals no suspicious lesions.",
            Diagnosis = "Overweight (BMI 28.5)\nMild osteoarthritis, knees\nHypertension, controlled",
            Medications = "Multivitamin daily\nIbuprofen 400mg as needed for joint pain\nContinue cardiac medications as prescribed by cardiologist",
            TestResults = new List<string>
            {
                "Height: 5'10\"",
                "Weight: 180 lbs",
                "BMI: 28.5",
                "Blood Pressure: 132/78 mmHg",
                "Temperature: 98.6°F",
                "Vision: 20/20 both eyes"
            },
            Notes = "Recommended weight loss of 15-20 lbs through diet and exercise. Discussed joint-friendly exercises like swimming. Scheduled mammogram and colonoscopy screening.",
            NextAppointment = "Annual exam scheduled for January 2026"
        },

        new CheckUp
        {
            CheckupDate = DateTime.Now.AddDays(-68),
            DoctorName = "Dr. Emily Rodriguez - Endocrinologist",
            Summary = "Diabetes consultation. HbA1c improved to 6.8%. Discussed insulin adjustment and continuous glucose monitoring.",
            CheckupType = "Specialist",
            DetailedDescription = "Patient referred by primary care for diabetes management consultation. Type 2 diabetes diagnosed 2 years ago, initially managed with metformin alone but requiring insulin therapy for the past 6 months. Reports good compliance with medication and home glucose monitoring. Some episodes of mild hypoglycemia in the early morning, typically resolved with glucose tablets. Feet examination shows no signs of neuropathy or ulceration.",
            Diagnosis = "Type 2 Diabetes Mellitus, improving control\nDiabetic gastroparesis, mild",
            Medications = "Metformin 1000mg twice daily\nInsulin glargine 24 units at bedtime\nInsulin aspart 8-12 units with meals\nOmeprazole 20mg daily",
            TestResults = new List<string>
            {
                "HbA1c: 6.8% (improved from 8.1%)",
                "Fasting glucose: 126 mg/dL",
                "Creatinine: 0.9 mg/dL",
                "Microalbumin: Normal",
                "Foot sensation: Intact to monofilament testing"
            },
            Notes = "Excellent improvement in glucose control. Reduced bedtime insulin to 22 units to minimize morning hypoglycemia. Patient educated on carbohydrate counting and approved for continuous glucose monitor.",
            NextAppointment = "Return in 3 months with CGM data"
        },

        new CheckUp
        {
            CheckupDate = DateTime.Now.AddDays(-95),
            DoctorName = "Dr. Amanda White - Orthopedic Surgeon",
            Summary = "Post-operative follow-up for right knee arthroscopy. Healing well, no signs of infection. Physical therapy progressing.",
            CheckupType = "Post-op",
            DetailedDescription = "6-week post-operative visit following arthroscopic meniscectomy of right knee. Patient reports significant improvement in pain and mobility since surgery. Incision sites are well-healed with no signs of infection, swelling, or unusual drainage. Range of motion has improved from 90 degrees at 2-week visit to 130 degrees currently. Patient has been compliant with physical therapy 3 times per week.",
            Diagnosis = "Right knee meniscal tear, status post arthroscopic repair\nPost-operative recovery, excellent",
            Medications = "Ibuprofen 600mg three times daily with food\nIce therapy 15-20 minutes 3-4 times daily",
            TestResults = new List<string>
            {
                "Range of Motion: 0-130 degrees (normal 0-140)",
                "Incision sites: Well-healed, no erythema",
                "Knee circumference: 38cm (pre-op 41cm)",
                "Pain scale: 2/10 at rest, 4/10 with activity"
            },
            Notes = "Excellent progress with recovery. May gradually increase activity level. Return to full sports activities cleared in 2-4 weeks pending continued PT progress. No restrictions for daily activities.",
            NextAppointment = "Return as needed, cleared for normal activities"
        },

        new CheckUp
        {
            CheckupDate = DateTime.Now.AddDays(-128),
            DoctorName = "Dr. Robert Kim - Dermatologist",
            Summary = "Skin cancer screening. Several benign moles identified. One suspicious lesion on back biopsied - results pending.",
            CheckupType = "Screening",
            DetailedDescription = "Annual full-body skin examination for patient with family history of melanoma. Comprehensive examination of all skin surfaces including scalp, between toes, and mucosal surfaces. Multiple benign-appearing nevi noted, consistent with previous examinations. One 6mm irregular, darkly pigmented lesion on upper back shows changes since last year's photos - asymmetry and color variation concerning for possible dysplasia.",
            Diagnosis = "Multiple benign nevi\nAtypical nevus, upper back - biopsy performed\nActinic keratosis, left forearm",
            Medications = "Tretinoin 0.05% cream for facial sun damage\nHydrocortisone 1% cream for post-biopsy care",
            TestResults = new List<string>
            {
                "Biopsy site: Upper back, 6mm punch biopsy",
                "Pathology: Pending (results in 5-7 days)",
                "Photography: Updated baseline photos taken",
                "UV damage assessment: Moderate photodamage consistent with age"
            },
            Notes = "Reinforced importance of daily sunscreen use and monthly self-examinations. Patient educated on ABCDE warning signs. Cryotherapy performed on actinic keratosis. Biopsy results will be called when available.",
            NextAppointment = "Follow-up in 2 weeks for biopsy results, then annual screening"
        },

        new CheckUp
        {
            CheckupDate = DateTime.Now.AddDays(-156),
            DoctorName = "Dr. Lisa Thompson - Gastroenterologist",
            Summary = "Colonoscopy screening completed. Two small polyps removed and sent for pathology. Overall colon health good.",
            CheckupType = "Procedure",
            DetailedDescription = "Screening colonoscopy performed under conscious sedation. Excellent bowel preparation allowed for complete visualization of the entire colon to the cecum. Two diminutive polyps (3mm and 5mm) identified in the sigmoid colon and removed using cold snare technique. No other abnormalities noted. Hemorrhoids noted at anal verge but not actively bleeding. Procedure tolerated well without complications.",
            Diagnosis = "Colon polyps, removed\nInternal hemorrhoids, grade I\nNormal screening colonoscopy otherwise",
            Medications = "No restrictions post-procedure\nFiber supplement recommended for hemorrhoids\nResume all home medications",
            TestResults = new List<string>
            {
                "Polyp #1: 3mm sigmoid, tubular adenoma (benign)",
                "Polyp #2: 5mm sigmoid, hyperplastic polyp (benign)",
                "Bowel preparation: Excellent",
                "Cecal intubation: Achieved",
                "Withdrawal time: 12 minutes"
            },
            Notes = "Pathology results show benign adenomatous and hyperplastic polyps with no high-grade dysplasia. Recommended increased dietary fiber and regular exercise. Patient counseled on polyp prevention strategies.",
            NextAppointment = "Repeat colonoscopy in 5 years due to adenomatous polyp"
        },

        new CheckUp
        {
            CheckupDate = DateTime.Now.AddDays(-201),
            DoctorName = "Dr. Patricia Davis - Ophthalmologist",
            Summary = "Routine eye exam. Prescription updated for reading glasses. Early signs of cataracts noted in both eyes.",
            CheckupType = "Routine",
            DetailedDescription = "Comprehensive ophthalmologic examination including visual acuity, refraction, tonometry, and dilated fundus examination. Patient reports mild difficulty with near vision and occasional glare while driving at night. Visual acuity 20/20 distance, 20/40 near without correction. Mild nuclear sclerotic cataracts noted bilaterally, more prominent in right eye. Optic discs appear healthy with cup-to-disc ratio 0.3 bilaterally.",
            Diagnosis = "Presbyopia\nNuclear sclerotic cataracts, mild, bilateral\nDry eye syndrome, mild",
            Medications = "Artificial tears 4 times daily as needed\nReading glasses +1.50 diopters prescribed",
            TestResults = new List<string>
            {
                "Visual acuity: 20/20 distance, 20/40 near",
                "Intraocular pressure: 14 mmHg OD, 16 mmHg OS",
                "Refraction: +0.25 -0.50 x 90 OD, +0.50 -0.25 x 85 OS",
                "Tear break-up time: 8 seconds (mildly decreased)"
            },
            Notes = "Cataracts are early and not significantly affecting vision at this time. Surgery not indicated currently. Patient counseled on cataract progression and symptoms that would warrant surgical evaluation. UV protection recommended.",
            NextAppointment = "Annual exam in 12 months, sooner if vision changes"
        }
    };
        }

        // In your ViewModel constructor or initialization method:
        public PatientHistoryViewModel()
        {
            CheckUps = InitializeSampleCheckUps();
        }

        // Property in your ViewModel:
        private ObservableCollection<CheckUp> _checkUps;
        public ObservableCollection<CheckUp> CheckUps
        {
            get { return _checkUps; }
            set
            {
                _checkUps = value;
                //OnPropertyChanged();
            }
        }

        private ICommand _showDetailedReportCommand;
        public ICommand ShowDetailedReportCommand
        {
            get
            {
                if (_showDetailedReportCommand == null)
                {
                    _showDetailedReportCommand = new RelayCommand<CheckUp>(ShowDetailedReport);
                }
                return _showDetailedReportCommand;
            }
        }

        private void ShowDetailedReport(CheckUp checkup)
        {
            if (checkup == null) return;

            // Create and show the detailed report window
            var detailWindow = new CheckupDetailWindow
            {
                DataContext = checkup,
                Owner = Application.Current.MainWindow,
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            };

            detailWindow.Show(); // Use Show() for non-modal or ShowDialog() for modal
        }

        // Sample CheckUp model (adjust properties according to your needs)
        public class CheckUp : INotifyPropertyChanged
        {
            public DateTime CheckupDate { get; set; }
            public string DoctorName { get; set; }
            public string Summary { get; set; }
            public string CheckupType { get; set; } // e.g., "Routine", "Follow-up", "Emergency"
            public string DetailedDescription { get; set; }
            public string Diagnosis { get; set; }
            public string Medications { get; set; }
            public string NextAppointment { get; set; }
            public List<string> TestResults { get; set; }
            public string Notes { get; set; }

            public event PropertyChangedEventHandler PropertyChanged;
            protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
