﻿using MedicalSearchingPlatform.Data.DataContext;
using MedicalSearchingPlatform.Data.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;

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
                var passwordHasher = new PasswordHasher<User>();
                var users = new List<User>
                {
                    new User { Id = "U1",UserName = "admin@example.com", NormalizedUserName ="ADMIN@EXAMPLE.COM", FullName = "Admin User", Email = "admin@example.com", Role = "Admin", IsActive = true, PasswordHash = "admin123"},
                    new User { Id = "U2",UserName = "staff1@example.com",NormalizedUserName ="STAFF1@EXAMPLE.COM",FullName = "Staff One", Email = "staff1@example.com", Role = "Staff", IsActive = true, PasswordHash = "staff123"},
                    new User { Id = "U3",UserName = "staff2@example.com",NormalizedUserName ="STAFF2@EXAMPLE.COM",FullName = "Staff Two", Email = "staff2@example.com", Role = "Staff", IsActive = true, PasswordHash = "staff123"},
                    new User { Id = "U4",UserName = "alice.smith@example.com",NormalizedUserName ="ALICE.SMITH@EXAMPLE.COM",FullName = "Dr. Alice Smith", Email = "alice.smith@example.com", Role = "Doctor", IsActive = true, PasswordHash = "doctor123"},
                    new User { Id = "U5",UserName = "bob.johnson@example.com",NormalizedUserName ="BOB.JOHNSON@EXAMPLE.COM",FullName = "Dr. Bob Johnson", Email = "bob.johnson@example.com", Role = "Doctor", IsActive = true, PasswordHash = "doctor123"},
                    new User { Id = "U6",UserName = "patient1@example.com",NormalizedUserName ="PATIENT1@EXAMPLE.COM",FullName = "Patient One", Email = "patient1@example.com", Role = "Patient", IsActive = true, PasswordHash = "patient123"},
                    new User { Id = "U7",UserName = "patient2@example.com",NormalizedUserName ="PATIENT2@EXAMPLE.COM",FullName = "Patient Two", Email = "patient2@example.com", Role = "Patient", IsActive = true, PasswordHash ="patient123"}
                };

                foreach (var user in users)
                {
                    user.PasswordHash = passwordHasher.HashPassword(user, user.PasswordHash);
                }

                context.Users.AddRange(users);
                context.SaveChanges();
            }

            // Check if Medical Facilities exist
            if (!context.MedicalFacilities.Any())
            {
                var facilities = new List<MedicalFacility>
                {
                    new MedicalFacility { FacilityId = "F1", FacilityName = "City Hospital", Address = "123 Main St", PhoneNumber = "123-456-7890",
                        CreatedAt = DateTime.UtcNow.AddDays(-10), Infor = "A top-tier hospital with modern equipment.",
                        ImageUrl = "/img/departments/departments-1.jpg" },

                    new MedicalFacility { FacilityId = "F2", FacilityName = "Downtown Clinic", Address = "456 Market St", PhoneNumber = "987-654-3210",
                        CreatedAt = DateTime.UtcNow.AddDays(-8), Infor = "Conveniently located clinic for quick medical check-ups.",
                        ImageUrl = "/img/departments/departments-2.jpg" }
                };

                context.MedicalFacilities.AddRange(facilities);
                context.SaveChanges();
            }

            // Check if Doctors exist
            if (!context.Doctors.Any())
            {
                var doctors = new List<Doctor>
                {
                    new Doctor { DoctorId = "D1", UserId = "U4", FacilityId = "F1", Specialization = "Cardiology", ExperienceYears = 10,
                        Qualifications = "MD, PhD", Experience = "10 years in cardiac care.", CreatedAt = DateTime.UtcNow.AddDays(-5),
                        ImageUrl = "/img/doctors/doctors-1.jpg" },

                    new Doctor { DoctorId = "D2", UserId = "U5", FacilityId = "F2", Specialization = "Pediatrics", ExperienceYears = 8,
                        Qualifications = "MD", Experience = "Specialist in children's health.", CreatedAt = DateTime.UtcNow.AddDays(-3),
                        ImageUrl = "/img/doctors/doctors-2.jpg" }
                };

                context.Doctors.AddRange(doctors);
                context.SaveChanges();
            }

            // Check if Patients exist
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

            // Check if Medical Services exist
            if (!context.MedicalServices.Any())
            {
                var services = new List<MedicalService>
                {
                    new MedicalService { ServiceId = "S1", ServiceName = "General Checkup", Description = "Routine health examination.", Status = "Active" },
                    new MedicalService { ServiceId = "S2", ServiceName = "Pediatrics", Description = "Healthcare services for children.", Status = "Active" },
                    new MedicalService { ServiceId = "S3", ServiceName = "Cardiology", Description = "Heart health and diagnostics.", Status = "Active" },
                    new MedicalService { ServiceId = "S4", ServiceName = "Dermatology", Description = "Skin care and treatment services.", Status = "Active" },
                    new MedicalService { ServiceId = "S5", ServiceName = "Physical Therapy", Description = "Rehabilitation for injuries and mobility.", Status = "Active" }
                };

                context.MedicalServices.AddRange(services);
                context.SaveChanges();
            }
        }
    }
}
