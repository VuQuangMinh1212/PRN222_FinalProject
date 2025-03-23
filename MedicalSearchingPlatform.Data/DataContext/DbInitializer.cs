using MedicalSearchingPlatform.Data.DataContext;
using MedicalSearchingPlatform.Data.Entities;

namespace MedicalSearchingPlatform.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            // Ensure the database is created
            context.Database.EnsureCreated();

            // Check if Users table is empty
            if (!context.Users.Any())
            {
                var users = new List<User>
                {
                    new User { UserId = "U1", FullName = "Admin User", Email = "admin@example.com", Role = "Admin", IsActive = true, Password = "admin123" },
                    new User { UserId = "U2", FullName = "Staff One", Email = "staff1@example.com", Role = "Staff", IsActive = true, Password = "staff123" },
                    new User { UserId = "U3", FullName = "Staff Two", Email = "staff2@example.com", Role = "Staff", IsActive = true, Password = "staff123" },
                    new User { UserId = "U4", FullName = "Dr. Alice Smith", Email = "alice.smith@example.com", Role = "Doctor", IsActive = true, Password = "doctor123" },
                    new User { UserId = "U5", FullName = "Dr. Bob Johnson", Email = "bob.johnson@example.com", Role = "Doctor", IsActive = true, Password = "doctor123" },
                    new User { UserId = "U6", FullName = "Patient One", Email = "patient1@example.com", Role = "Patient", IsActive = true, Password = "patient123" },
                    new User { UserId = "U7", FullName = "Patient Two", Email = "patient2@example.com", Role = "Patient", IsActive = true, Password = "patient123" }
                };

                context.Users.AddRange(users);
                context.SaveChanges();
            }

            if (!context.MedicalFacilities.Any())
            {
                var facilities = new List<MedicalFacility>
                {
                    new MedicalFacility { FacilityId = "F1", FacilityName = "City Hospital", Address = "123 Main St", PhoneNumber = "123-456-7890" },
                    new MedicalFacility { FacilityId = "F2", FacilityName = "Downtown Clinic", Address = "456 Market St", PhoneNumber = "987-654-3210" }
                };

                context.MedicalFacilities.AddRange(facilities);
                context.SaveChanges();
            }

            if (!context.Doctors.Any())
            {
                var doctors = new List<Doctor>
                {
                    new Doctor { DoctorId = "D1", UserId = "U4", FacilityId = "F1", Specialization = "Cardiology", ExperienceYears = 10, Qualifications = "MD, PhD", Experience = "10 years in cardiac care." },
                    new Doctor { DoctorId = "D2", UserId = "U5", FacilityId = "F2", Specialization = "Pediatrics", ExperienceYears = 8, Qualifications = "MD", Experience = "Specialist in children's health." }
                };

                context.Doctors.AddRange(doctors);
                context.SaveChanges();
            }

            if (!context.Patients.Any())
            {
                var patients = new List<Patient>
                {
                    new Patient { PatientId = "P1", UserId = "U6", MedicalHistory = "No major issues", ConditionsToNote = "Allergic to penicillin" },
                    new Patient { PatientId = "P2", UserId = "U7", MedicalHistory = "History of high blood pressure", ConditionsToNote = "Takes medication daily" }
                };

                context.Patients.AddRange(patients);
                context.SaveChanges();
            }
        }
    }
}
