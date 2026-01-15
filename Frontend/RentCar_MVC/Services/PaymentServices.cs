using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using RentCar_API.DTOs;
using RentCar_MVC.Models;


namespace RentCar_MVC.Services
{
    public interface IPaymentService
    {
        public Task<RentalViewModel> DisplayRental(string id);
        public Task<bool> ConfirmPayment(PaymentMethodDto dto, string id);
    }

    public class PaymentServices : IPaymentService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PaymentServices(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<RentalViewModel> DisplayRental(string id)
        {
            var token = _httpContextAccessor.HttpContext?.Session.GetString("AuthToken"); 
            if (!string.IsNullOrEmpty(token)) 
            { 
                Console.WriteLine("The user must logged in");
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token); 
            }

            var response = await _httpClient.GetAsync($"api/Payment/{id}/Payment");
            if(!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Failed to fetch rental: {response.StatusCode}");
            }

            var json = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<RentalViewModel>(json, new JsonSerializerOptions{PropertyNameCaseInsensitive = true});
            if(result == null)
            {
                throw new HttpRequestException("Rental response was empty or invalid.");
            }
            return result;
        }

        public async Task<bool> ConfirmPayment(PaymentMethodDto dto, string id)
        {
            var token = _httpContextAccessor.HttpContext?.Session.GetString("AuthToken"); 
            if (!string.IsNullOrEmpty(token)) 
            { 
                Console.WriteLine("The user must logged in");
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token); 
            }
            
            var json = JsonSerializer.Serialize(dto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var result = await _httpClient.PostAsync($"api/Payment/{id}/Payment", content);
            return result.IsSuccessStatusCode;
        }
    }
}