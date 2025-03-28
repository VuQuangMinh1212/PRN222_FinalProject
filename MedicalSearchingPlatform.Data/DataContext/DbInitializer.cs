﻿using MedicalSearchingPlatform.Data.DataContext;
using MedicalSearchingPlatform.Data.Entities;
using Microsoft.AspNetCore.Identity;

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
                    // Admin
                    new User { Id = "U1", UserName = "admin@example.com", NormalizedUserName = "ADMIN@EXAMPLE.COM", FullName = "Admin User", Email = "admin@example.com", Role = "Admin", IsActive = true, PasswordHash = "admin123" },

                    // Staff
                    new User { Id = "U2", UserName = "staff1@example.com", NormalizedUserName = "STAFF1@EXAMPLE.COM", FullName = "Staff One", Email = "staff1@example.com", Role = "Staff", IsActive = true, PasswordHash = "staff123" },
                    new User { Id = "U3", UserName = "staff2@example.com", NormalizedUserName = "STAFF2@EXAMPLE.COM", FullName = "Staff Two", Email = "staff2@example.com", Role = "Staff", IsActive = true, PasswordHash = "staff123" },

                    // Doctors
                    new User { Id = "U4", UserName = "alice.smith@example.com", NormalizedUserName = "ALICE.SMITH@EXAMPLE.COM", FullName = "Dr. Alice Smith", Email = "alice.smith@example.com", Role = "Doctor", IsActive = true, PasswordHash = "doctor123" },
                    new User { Id = "U5", UserName = "bob.johnson@example.com", NormalizedUserName = "BOB.JOHNSON@EXAMPLE.COM", FullName = "Dr. Bob Johnson", Email = "bob.johnson@example.com", Role = "Doctor", IsActive = true, PasswordHash = "doctor123" },
                    new User { Id = "U6", UserName = "sarah.lee@example.com", NormalizedUserName = "SARAH.LEE@EXAMPLE.COM", FullName = "Dr. Sarah Lee", Email = "sarah.lee@example.com", Role = "Doctor", IsActive = true, PasswordHash = "doctor123" },
                    
                    // Patients
                    new User { Id = "U7", UserName = "peter.parker@email.com", NormalizedUserName = "PETER.PARKER@EMAIL.COM", FullName = "Peter Parker", Email = "peter.parker@email.com", Role = "Patient", IsActive = true, PasswordHash = "patient123" },
                    new User { Id = "U8", UserName = "mary.johnson@email.com", NormalizedUserName = "MARY.JOHNSON@EMAIL.COM", FullName = "Mary Johnson", Email = "mary.johnson@email.com", Role = "Patient", IsActive = true, PasswordHash = "patient123" },
                    new User { Id = "U9", UserName = "david.wilson@email.com", NormalizedUserName = "DAVID.WILSON@EMAIL.COM", FullName = "David Wilson", Email = "david.wilson@email.com", Role = "Patient", IsActive = true, PasswordHash = "patient123" },
                    new User { Id = "U10", UserName = "olivia.martin@email.com", NormalizedUserName = "OLIVIA.MARTIN@EMAIL.COM", FullName = "Olivia Martin", Email = "olivia.martin@email.com", Role = "Patient", IsActive = true, PasswordHash = "patient123" },
                    new User { Id = "U11", UserName = "james.anderson@email.com", NormalizedUserName = "JAMES.ANDERSON@EMAIL.COM", FullName = "James Anderson", Email = "james.anderson@email.com", Role = "Patient", IsActive = true, PasswordHash = "patient123" },
                    new User { Id = "U12", UserName = "sophia.thomas@email.com", NormalizedUserName = "SOPHIA.THOMAS@EMAIL.COM", FullName = "Sophia Thomas", Email = "sophia.thomas@email.com", Role = "Patient", IsActive = true, PasswordHash = "patient123" },
                    new User { Id = "U13", UserName = "william.moore@email.com", NormalizedUserName = "WILLIAM.MOORE@EMAIL.COM", FullName = "William Moore", Email = "william.moore@email.com", Role = "Patient", IsActive = true, PasswordHash = "patient123" },
                    new User { Id = "U14", UserName = "charlotte.taylor@email.com", NormalizedUserName = "CHARLOTTE.TAYLOR@EMAIL.COM", FullName = "Charlotte Taylor", Email = "charlotte.taylor@email.com", Role = "Patient", IsActive = true, PasswordHash = "patient123" },
                    new User { Id = "U15", UserName = "benjamin.clark@email.com", NormalizedUserName = "BENJAMIN.CLARK@EMAIL.COM", FullName = "Benjamin Clark", Email = "benjamin.clark@email.com", Role = "Patient", IsActive = true, PasswordHash = "patient123" },
                    new User { Id = "U16", UserName = "amelia.rodriguez@email.com", NormalizedUserName = "AMELIA.RODRIGUEZ@EMAIL.COM", FullName = "Amelia Rodriguez", Email = "amelia.rodriguez@email.com", Role = "Patient", IsActive = true, PasswordHash = "patient123" },
                    new User { Id = "U17", UserName = "ethan.lewis@email.com", NormalizedUserName = "ETHAN.LEWIS@EMAIL.COM", FullName = "Ethan Lewis", Email = "ethan.lewis@email.com", Role = "Patient", IsActive = true, PasswordHash = "patient123" },
                    new User { Id = "U18", UserName = "harper.walker@email.com", NormalizedUserName = "HARPER.WALKER@EMAIL.COM", FullName = "Harper Walker", Email = "harper.walker@email.com", Role = "Patient", IsActive = true, PasswordHash = "patient123" },
                    new User { Id = "U19", UserName = "daniel.hall@email.com", NormalizedUserName = "DANIEL.HALL@EMAIL.COM", FullName = "Daniel Hall", Email = "daniel.hall@email.com", Role = "Patient", IsActive = true, PasswordHash = "patient123" },
                    new User { Id = "U20", UserName = "ella.young@email.com", NormalizedUserName = "ELLA.YOUNG@EMAIL.COM", FullName = "Ella Young", Email = "ella.young@email.com", Role = "Patient", IsActive = true, PasswordHash = "patient123" },
                    new User { Id = "U21", UserName = "jacob.king@email.com", NormalizedUserName = "JACOB.KING@EMAIL.COM", FullName = "Jacob King", Email = "jacob.king@email.com", Role = "Patient", IsActive = true, PasswordHash = "patient123" },
                    new User { Id = "U22", UserName = "mia.scott@email.com", NormalizedUserName = "MIA.SCOTT@EMAIL.COM", FullName = "Mia Scott", Email = "mia.scott@email.com", Role = "Patient", IsActive = true, PasswordHash = "patient123" },
                    new User { Id = "U23", UserName = "alexander.adams@email.com", NormalizedUserName = "ALEXANDER.ADAMS@EMAIL.COM", FullName = "Alexander Adams", Email = "alexander.adams@email.com", Role = "Patient", IsActive = true, PasswordHash = "patient123" },
                    new User { Id = "U24", UserName = "isabella.wright@email.com", NormalizedUserName = "ISABELLA.WRIGHT@EMAIL.COM", FullName = "Isabella Wright", Email = "isabella.wright@email.com", Role = "Patient", IsActive = true, PasswordHash = "patient123" }
                };


                foreach (var user in users)
                {
                    user.NormalizedEmail = user.Email.ToUpper();
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
                        ImageUrl = "/img/departments-2.jpg" },

                    new MedicalFacility { FacilityId = "F2", FacilityName = "Downtown Clinic", Address = "456 Market St", PhoneNumber = "987-654-3210",
                        CreatedAt = DateTime.UtcNow.AddDays(-8), Infor = "Conveniently located clinic for quick medical check-ups.",
                        ImageUrl = "/img/departments-3.jpg" }
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

            if (!context.ArticleCategories.Any())
            {
                var articleCategories = new List<ArticleCategory>
                {
                    new ArticleCategory { CategoryId = "AC1", Name = "Health", Description = "Articles related to general health and well-being." },
                    new ArticleCategory { CategoryId = "AC2", Name = "Medical", Description = "In-depth medical research and case studies." },
                    new ArticleCategory { CategoryId = "AC3", Name = "Nutrition", Description = "Guides and tips on maintaining a healthy diet." },
                    new ArticleCategory { CategoryId = "AC4", Name = "Mental Health", Description = "Awareness and discussions on mental health topics." },
                    new ArticleCategory { CategoryId = "AC5", Name = "Fitness", Description = "Articles about exercise, workouts, and staying fit." },
                    new ArticleCategory { CategoryId = "AC6", Name = "Immunization", Description = "Information about vaccines and disease prevention." },
                    new ArticleCategory { CategoryId = "AC7", Name = "Wellness", Description = "Holistic approaches to achieving wellness and balance." }
                };

                context.ArticleCategories.AddRange(articleCategories);
                context.SaveChanges();
            }

            if (!context.Articles.Any())
            {
                var articles = new List<Article>
                {
                    new Article { ArticleId = "A1", Title = "The Importance of Regular Exercise", Content = "Regular exercise helps maintain a healthy body and mind...", AuthorId = "U4", ArticleCategoryId = "AC5", PublishedDate = DateTime.UtcNow, Status = "Published" },
                    new Article { ArticleId = "A2", Title = "Understanding Mental Health", Content = "Mental health is just as important as physical health...", AuthorId = "U9", ArticleCategoryId = "AC4", PublishedDate = DateTime.UtcNow, Status = "Published" },
                    new Article { ArticleId = "A3", Title = "Benefits of a Balanced Diet", Content = "A balanced diet provides essential nutrients for the body...", AuthorId = "U5", ArticleCategoryId = "AC3", PublishedDate = DateTime.UtcNow, Status = "Published" },
                    new Article { ArticleId = "A4", Title = "How Vaccines Work", Content = "Vaccines help the immune system recognize and fight diseases...", AuthorId = "U4", ArticleCategoryId = "AC6", PublishedDate = DateTime.UtcNow, Status = "Draft" },
                    new Article { ArticleId = "A5", Title = "The Role of Sleep in Health", Content = "Getting enough sleep is vital for overall health and well-being...", AuthorId = "U9", ArticleCategoryId = "AC1", PublishedDate = DateTime.UtcNow, Status = "Published" },
                    new Article { ArticleId = "A6", Title = "Staying Hydrated: Why Water Matters", Content = "Water is essential for various bodily functions...", AuthorId = "U5", ArticleCategoryId = "AC7", PublishedDate = DateTime.UtcNow, Status = "Published" },
                    new Article { ArticleId = "A7", Title = "Common Nutritional Deficiencies", Content = "Many people suffer from nutrient deficiencies without realizing it...", AuthorId = "U4", ArticleCategoryId = "AC3", PublishedDate = DateTime.UtcNow, Status = "Draft" },
                    new Article { ArticleId = "A8", Title = "Managing Stress Effectively", Content = "Chronic stress can lead to various health problems...", AuthorId = "U9", ArticleCategoryId = "AC4", PublishedDate = DateTime.UtcNow, Status = "Published" },
                    new Article { ArticleId = "A9", Title = "Strength Training for Beginners", Content = "Strength training improves muscle mass and overall fitness...", AuthorId = "U5", ArticleCategoryId = "AC5", PublishedDate = DateTime.UtcNow, Status = "Draft" },
                    new Article { ArticleId = "A10", Title = "The Science Behind Mindfulness", Content = "Mindfulness has been proven to reduce stress and enhance focus...", AuthorId = "U4", ArticleCategoryId = "AC7", PublishedDate = DateTime.UtcNow, Status = "Published" }
                };
                context.Articles.AddRange(articles);
                context.SaveChanges();
            }


            if (!context.ArticleLikes.Any())
            {
                var articlesLike = new List<ArticleLike>
                {
                    // A1 được thích bởi nhiều bệnh nhân
                    new ArticleLike { ArticleId = "A1", UserId = "U6", LikeAt = DateTime.UtcNow },
                    new ArticleLike { ArticleId = "A1", UserId = "U7", LikeAt = DateTime.UtcNow },
                    new ArticleLike { ArticleId = "A1", UserId = "U8", LikeAt = DateTime.UtcNow },
                    new ArticleLike { ArticleId = "A1", UserId = "U9", LikeAt = DateTime.UtcNow },
                    new ArticleLike { ArticleId = "A1", UserId = "U10", LikeAt = DateTime.UtcNow },
                    new ArticleLike { ArticleId = "A1", UserId = "U11", LikeAt = DateTime.UtcNow },

                    // A2 được thích bởi nhiều bệnh nhân
                    new ArticleLike { ArticleId = "A2", UserId = "U6", LikeAt = DateTime.UtcNow },
                    new ArticleLike { ArticleId = "A2", UserId = "U7", LikeAt = DateTime.UtcNow },
                    new ArticleLike { ArticleId = "A2", UserId = "U8", LikeAt = DateTime.UtcNow },
                    new ArticleLike { ArticleId = "A2", UserId = "U9", LikeAt = DateTime.UtcNow },

                    // A3
                    new ArticleLike { ArticleId = "A3", UserId = "U6", LikeAt = DateTime.UtcNow },
                    new ArticleLike { ArticleId = "A3", UserId = "U7", LikeAt = DateTime.UtcNow },
                    new ArticleLike { ArticleId = "A3", UserId = "U11", LikeAt = DateTime.UtcNow },

                    // A4
                    new ArticleLike { ArticleId = "A4", UserId = "U8", LikeAt = DateTime.UtcNow },
                    new ArticleLike { ArticleId = "A4", UserId = "U9", LikeAt = DateTime.UtcNow },

                    // A5
                    new ArticleLike { ArticleId = "A5", UserId = "U10", LikeAt = DateTime.UtcNow },
                    new ArticleLike { ArticleId = "A5", UserId = "U11", LikeAt = DateTime.UtcNow },

                    // A6, A7, A8, A9, A10 mỗi bài có ít nhất 3 lượt thích
                    new ArticleLike { ArticleId = "A6", UserId = "U6", LikeAt = DateTime.UtcNow },
                    new ArticleLike { ArticleId = "A6", UserId = "U7", LikeAt = DateTime.UtcNow },
                    new ArticleLike { ArticleId = "A6", UserId = "U8", LikeAt = DateTime.UtcNow },

                    new ArticleLike { ArticleId = "A7", UserId = "U9", LikeAt = DateTime.UtcNow },
                    new ArticleLike { ArticleId = "A7", UserId = "U10", LikeAt = DateTime.UtcNow },
                    new ArticleLike { ArticleId = "A7", UserId = "U11", LikeAt = DateTime.UtcNow },

                    new ArticleLike { ArticleId = "A8", UserId = "U6", LikeAt = DateTime.UtcNow },
                    new ArticleLike { ArticleId = "A8", UserId = "U7", LikeAt = DateTime.UtcNow },
                    new ArticleLike { ArticleId = "A8", UserId = "U8", LikeAt = DateTime.UtcNow },

                    new ArticleLike { ArticleId = "A9", UserId = "U9", LikeAt = DateTime.UtcNow },
                    new ArticleLike { ArticleId = "A9", UserId = "U10", LikeAt = DateTime.UtcNow },
                    new ArticleLike { ArticleId = "A9", UserId = "U11", LikeAt = DateTime.UtcNow },

                    new ArticleLike { ArticleId = "A10", UserId = "U6", LikeAt = DateTime.UtcNow },
                    new ArticleLike { ArticleId = "A10", UserId = "U7", LikeAt = DateTime.UtcNow },
                    new ArticleLike { ArticleId = "A10", UserId = "U8", LikeAt = DateTime.UtcNow }
                };
                context.ArticleLikes.AddRange(articlesLike);
                context.SaveChanges();
            }
        }
    }
}
