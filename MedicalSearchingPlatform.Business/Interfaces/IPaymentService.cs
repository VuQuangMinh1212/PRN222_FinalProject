﻿using MedicalSearchingPlatform.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MedicalSearchingPlatform.Services
{
    public interface IPaymentService
    {
        Task<IEnumerable<Payment>> GetAllPaymentsAsync();
        Task<Payment> GetPaymentByIdAsync(string paymentId);
        Task<IEnumerable<Payment>> GetPaymentsByPatientIdAsync(string patientId);
        Task<bool> ProcessPaymentAsync(Payment payment);
        Task<bool> RefundPaymentAsync(string paymentId);
    }
}
