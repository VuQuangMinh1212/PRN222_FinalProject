using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MedicalSearchingPlatform.Data.Entities;
using MedicalSearchingPlatform.Data.DataContext;
using MedicalSearchingPlatform.Data.IRepositories;

namespace MedicalSearchingPlatform.Data.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly ApplicationDbContext _context;

        public PaymentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Payment>> GetAllPaymentsAsync()
        {
            return await _context.Payments.ToListAsync();
        }

        public async Task<Payment> GetPaymentByIdAsync(string paymentId)
        {
            return await _context.Payments.FindAsync(paymentId);
        }

        public async Task<IEnumerable<Payment>> GetPaymentsByPatientIdAsync(string patientId)
        {
            return await _context.Payments
                .Where(p => p.PatientId == patientId)
                .ToListAsync();
        }

        public async Task AddPaymentAsync(Payment payment)
        {
            await _context.Payments.AddAsync(payment);
            await _context.SaveChangesAsync();
        }

        public async Task UpdatePaymentAsync(Payment payment)
        {
            _context.Payments.Update(payment);
            await _context.SaveChangesAsync();
        }
    }
}
