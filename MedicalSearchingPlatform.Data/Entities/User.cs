﻿using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace MedicalSearchingPlatform.Data.Entities
{
    public class User : IdentityUser
    {
        [Required, MaxLength(100)]
        public string FullName { get; set; } = string.Empty;

        [Required, MaxLength(10)]
        public string Role { get; set; } = "Patient";

        public bool IsActive { get; set; } = true;
        public User()
        {

        }
        public User(string fullName, string email, string role)
        {
            FullName = fullName;
            Role = role;
            Email = email;
            UserName = email;
            NormalizedEmail = email.ToUpper();
            NormalizedUserName = email.ToUpper();
        }
    }
}
