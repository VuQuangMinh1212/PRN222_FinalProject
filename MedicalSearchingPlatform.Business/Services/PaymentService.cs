using MedicalSearchingPlatform.Data.Entities;
using MedicalSearchingPlatform.Data.Repositories;

namespace MedicalSearchingPlatform.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;

        public PaymentService(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public async Task<IEnumerable<Payment>> GetAllPaymentsAsync()
        {
            return await _paymentRepository.GetAllPaymentsAsync();
        }

        public async Task<Payment> GetPaymentByIdAsync(string paymentId)
        {
            return await _paymentRepository.GetPaymentByIdAsync(paymentId);
        }

        public async Task<IEnumerable<Payment>> GetPaymentsByPatientIdAsync(string patientId)
        {
            return await _paymentRepository.GetPaymentsByPatientIdAsync(patientId);
        }

        public async Task<bool> ProcessPaymentAsync(Payment payment)
        {
            try
            {
                payment.PaymentDate = DateTime.UtcNow;
                payment.Status = "Completed";
                await _paymentRepository.AddPaymentAsync(payment);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> RefundPaymentAsync(string paymentId)
        {
            var payment = await _paymentRepository.GetPaymentByIdAsync(paymentId);
            if (payment == null || payment.Status == "Refunded")
            {
                return false;
            }

            payment.Status = "Refunded";
            await _paymentRepository.UpdatePaymentAsync(payment);
            return true;
        }
    }
}
