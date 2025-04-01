using MedicalSearchingPlatform.Data.Entities;
using MedicalSearchingPlatform.Data.Repositories;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MedicalSearchingPlatform.Business.Interfaces;
using MedicalSearchingPlatform.Data.IRepositories;

namespace MedicalSearchingPlatform.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public PaymentService(IPaymentRepository paymentRepository, HttpClient httpClient, IConfiguration configuration)
        {
            _paymentRepository = paymentRepository;
            _httpClient = httpClient;
            _configuration = configuration;
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

        public async Task<string> ProcessPaymentAsync(Payment payment)
        {
            var apiUrl = _configuration["PayOS:ApiUrl"] + "create-payment";
            var apiKey = _configuration["PayOS:ApiKey"];
            var secretKey = _configuration["PayOS:SecretKey"];
            var returnUrl = _configuration["PayOS:ReturnUrl"];
            var cancelUrl = _configuration["PayOS:CancelUrl"];

            var payload = new
            {
                amount = payment.Amount,
                orderId = payment.PaymentId,
                returnUrl = returnUrl,
                cancelUrl = cancelUrl,
                apiKey = apiKey,
                signature = secretKey
            };

            var content = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(apiUrl, content);

            if (!response.IsSuccessStatusCode)
            {
                return "Error: Payment request failed.";
            }

            var responseContent = await response.Content.ReadAsStringAsync();
            dynamic jsonResponse = JsonConvert.DeserializeObject(responseContent);

            payment.PaymentDate = DateTime.UtcNow;
            payment.Status = "Pending";
            await _paymentRepository.AddPaymentAsync(payment);

            return jsonResponse?.paymentUrl;
        }

        public async Task<bool> RefundPaymentAsync(string paymentId)
        {
            var payment = await _paymentRepository.GetPaymentByIdAsync(paymentId);
            if (payment == null || payment.Status == "Refunded")
            {
                return false;
            }

            var refundApiUrl = _configuration["PayOS:ApiUrl"] + "refund-payment";
            var apiKey = _configuration["PayOS:ApiKey"];
            var secretKey = _configuration["PayOS:SecretKey"];

            var payload = new
            {
                paymentId = payment.PaymentId,
                apiKey = apiKey,
                signature = secretKey
            };

            var content = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(refundApiUrl, content);

            if (!response.IsSuccessStatusCode)
            {
                return false;
            }

            payment.Status = "Refunded";
            await _paymentRepository.UpdatePaymentAsync(payment);
            return true;
        }
    }
}
