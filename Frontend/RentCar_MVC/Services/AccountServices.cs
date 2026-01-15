using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using RentCar_MVC.DTOs;
using RentCar_MVC.Models;


namespace RentCar_MVC.Services
{

    public interface IAccountService
    {
        Task<bool> RegisterUser(RegisterViewModel model);
        
    }

    public class AccountService : IAccountService
    {
        private readonly HttpClient _httpClient;
        public AccountService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> RegisterUser(RegisterViewModel model)
        {
            var json = JsonSerializer.Serialize(model);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/Register", content);

            return response.IsSuccessStatusCode;
        }

    
    }
    
    

}