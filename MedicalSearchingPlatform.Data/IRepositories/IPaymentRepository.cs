using MedicalSearchingPlatform.Data.Entities;

namespace MedicalSearchingPlatform.Data.Repositories
{
    public interface IPaymentRepository
    {
        Task<IEnumerable<Payment>> GetAllPaymentsAsync();
        Task<Payment> GetPaymentByIdAsync(string paymentId);
        Task<IEnumerable<Payment>> GetPaymentsByPatientIdAsync(string patientId);
        Task AddPaymentAsync(Payment payment);
        Task UpdatePaymentAsync(Payment payment);
        Task DeletePaymentAsync(string paymentId);
    }
}
