using MedicalSearchingPlatform.Data.Entities;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace MedicalSearchingPlatform.Business.Interfaces
{
    public interface IPaymentService
    {
        Task<IEnumerable<Payment>> GetAllPaymentsAsync();
        Task<Payment> GetPaymentByIdAsync(string paymentId);
        Task<IEnumerable<Payment>> GetPaymentsByPatientIdAsync(string patientId);
        Task<string> ProcessPaymentAsync(Payment payment);
        Task<bool> RefundPaymentAsync(string paymentId);
    }
}
