using MedicalSearchingPlatform.Data.Entities;

namespace MedicalSearchingPlatform.Services
{
    public interface IPaymentService
    {
        Task<string> CreatePaymentLink(decimal totalPrice);
        Task<decimal> CalculateTotalPriceAsync(List<string> serviceIds);
    }
}
