using MedicalSearchingPlatform.Data.Entities;
using MedicalSearchingPlatform.Data.Repositories;
using Net.payOS.Types;
using Net.payOS;
using MedicalSearchingPlatform.Business.Services;
using MedicalSearchingPlatform.Business.Interfaces;

namespace MedicalSearchingPlatform.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly string _clientId = "YOUR_CLIENT_ID";
        private readonly string _apiKey = "YOUR_API_KEY";
        private readonly string _checksumKey = "YOUR_CHECKSUM_KEY";
        private readonly MedicalServiceService _medicalServiceService;

        private readonly PayOS _payOS;
        private readonly string _domain = "http://localhost:7290";

        public PaymentService(MedicalServiceService medicalServiceService)
        {
            _medicalServiceService = medicalServiceService;
            _payOS = new PayOS(_clientId, _apiKey, _checksumKey);
        }

        public async Task<string> CreatePaymentLink(decimal totalPrice)
        {
            var paymentLinkRequest = new PaymentData(
                orderCode: int.Parse(DateTimeOffset.Now.ToString("ffffff")),
                amount: (int)totalPrice,
                description: "Thanh toán đơn hàng",
                items: [new("Mì tôm hảo hảo ly", 1, 2000)],
                returnUrl: $"{_domain}/Success",
                cancelUrl: $"{_domain}/Cancel"
            );

            var response = await _payOS.createPaymentLink(paymentLinkRequest);
            return response.checkoutUrl;
        }

        public async Task<decimal> CalculateTotalPriceAsync(List<string> serviceIds)
        {
            decimal totalPrice = 0;

            foreach (var serviceId in serviceIds)
            {
                var service = await _medicalServiceService.GetServiceByIdAsync(serviceId); 
                if (service != null)
                {
                    totalPrice += service.Price;
                }
            }

            return totalPrice;
        }

    }
}
