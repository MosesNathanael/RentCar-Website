using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using RentCar_API.DTOs;

namespace RentCar_MVC{

    public interface IRentalCarService
    {
        public Task<RentalResponseDto?> RentalService(CarDetailsDto dto);
    
    }
    public class RentalCarServices : IRentalCarService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public RentalCarServices(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<RentalResponseDto?> RentalService(CarDetailsDto dto)
        {
            var token = _httpContextAccessor.HttpContext?.Session.GetString("AuthToken"); 
            if (!string.IsNullOrEmpty(token)) 
            { 
                Console.WriteLine("The user must logged in");
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token); 
            }
            var json = JsonSerializer.Serialize(dto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/RentalDetails/CreateRental", content);
            var responseBody = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Status: {response.StatusCode} Body: {responseBody}");
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var rentalResponse = JsonSerializer.Deserialize<RentalResponseDto>(responseBody, new JsonSerializerOptions {PropertyNameCaseInsensitive = true});
            return rentalResponse;
        }
    }
}