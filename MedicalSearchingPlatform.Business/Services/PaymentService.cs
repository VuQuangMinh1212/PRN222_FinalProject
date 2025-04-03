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
        private readonly string _clientId = "3aad716e-ebb0-4299-88b6-256ab7964869";
        private readonly string _apiKey = "1fd27fed-6cbc-4fd5-abf3-fd37363b54d1";
        private readonly string _checksumKey = "dabaf5da137520f2f61e26b104b964dd7feec23eec6461080b13652424943524";
        private readonly IMedicalServiceService _medicalServiceService;

        private readonly PayOS _payOS;
        private readonly string _domain = "http://localhost:7290";

        public PaymentService(IMedicalServiceService medicalServiceService)
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
                items: [new("Dịch vụ y tế", 1, 2000)],
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
