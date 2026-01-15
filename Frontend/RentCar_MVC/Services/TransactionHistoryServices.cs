using System.Text.Json;
using RentCar_MVC.Models;
using System.Net.Http.Headers;
namespace RentCar_MVC.Services
{
    public interface ITransactionHistoryService
    {
        Task<List<TransactionHistoryModel>> GetHistoryAsync();
    }

    public class TransactionHistoryService : ITransactionHistoryService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TransactionHistoryService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<TransactionHistoryModel>> GetHistoryAsync()
        {
            var token = _httpContextAccessor.HttpContext?.Session.GetString("AuthToken"); 
            if (!string.IsNullOrEmpty(token)) 
            { 
                Console.WriteLine("The user must logged in");
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token); 
            }

            var response = await _httpClient.GetAsync("api/TransactionHistory/History");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            if (string.IsNullOrEmpty(json))
            {
                return new List<TransactionHistoryModel>();
            }
            
            var result = JsonSerializer.Deserialize<List<TransactionHistoryModel>>(json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            
            if(result == null)
            {
                return new List<TransactionHistoryModel>();
            }
            return result;
        }
    }

}