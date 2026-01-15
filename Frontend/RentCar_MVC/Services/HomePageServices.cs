using System.Text.Json;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http.HttpResults;
using RentCar_MVC.DTOs;
using System.Net.Http.Headers;

namespace RentCar_MVC.Services
{
    public interface IDisplayCarService
    {
        public Task<List<CarDisplayDto>> GetCarsAsync(string sort);
        public Task<List<CarDisplayDto>> GetCarsByYear(int year, string sort);
        public Task<CarDetailsDto> GetCarDetails(string id, DateTime rentalDate, DateTime returnDate);
    }


    public class HomePageService : IDisplayCarService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HomePageService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<CarDisplayDto>> GetCarsAsync(string sort)
        {
            var response = await _httpClient.GetAsync($"/api/HomePage/DisplayCar/All/{sort}");
            response.EnsureSuccessStatusCode();
            if (!response.IsSuccessStatusCode)
            {
                return new List<CarDisplayDto>();
            }

            var json = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<List<CarDisplayDto>>(json, new JsonSerializerOptions{ PropertyNameCaseInsensitive = true});

            if(result == null)
            {
                return new List<CarDisplayDto>();
            }
            return result;
        }


        public async Task<List<CarDisplayDto>> GetCarsByYear(int year, string sort)
        {
            var response = await _httpClient.GetAsync($"/api/HomePage/DisplayCar/{year}/{sort}");
            response.EnsureSuccessStatusCode();
            if (!response.IsSuccessStatusCode)
            {
                return new List<CarDisplayDto>();
            }
            
            var json = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<List<CarDisplayDto>>(json, new JsonSerializerOptions {PropertyNameCaseInsensitive = true});

            if(result == null)
            {
                return new List<CarDisplayDto>();
                
            }
            return result;
        }


        public async Task<CarDetailsDto> GetCarDetails(string id, DateTime rentalDate, DateTime returnDate)
        {
            var token = _httpContextAccessor.HttpContext?.Session.GetString("AuthToken"); 
            if (!string.IsNullOrEmpty(token)) 
            { 
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token); 
            }
            var response = await _httpClient.GetAsync($"/api/RentalDetails/CarDetails/{id}?rentalDate={rentalDate:yyy-MM-dd}&returnDate={returnDate:yyyy-MM-dd}");

            if (!response.IsSuccessStatusCode)
            {
                return new CarDetailsDto();
            }

            // Console.WriteLine($"Request URL: {response.RequestMessage.RequestUri}"); Console.WriteLine($"Status Code: {response.StatusCode}");
            var json = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Response JSON: {json}");
            var result = JsonSerializer.Deserialize<CarDetailsDto>(json, new JsonSerializerOptions{PropertyNameCaseInsensitive = true});

            if(result == null)
            {
                return new CarDetailsDto();
            }
            return result;
        }
    }

}