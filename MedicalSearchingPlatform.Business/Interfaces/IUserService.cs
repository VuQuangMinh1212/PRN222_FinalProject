﻿using MedicalSearchingPlatform.Data.Entities;
using System.Collections.Generic;

namespace MedicalSearchingPlatform.Business.Interfaces
{
    public interface IUserService
    {
        User GetUserById(int userId);
        User GetUserByEmail(string email);
        IEnumerable<User> GetAllUsers();
        void RegisterUser(User user);
        void UpdateUser(User user);
        void DeleteUser(int userId);
    }
}
