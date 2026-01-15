using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using RentCar_MVC.DTOs;
using RentCar_MVC.Models;
using RentCar_MVC.Services;

namespace RentCar_MVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly Services.IAccountService _accountService;
        private readonly HttpClient _httpClient;
        private readonly HttpClient _httpClientLogin;
        public AccountController(IAccountService accountService, HttpClient httpClient, IHttpClientFactory httpClientLogin)
        {
            _accountService = accountService;
            _httpClient = httpClient;
            _httpClientLogin = httpClientLogin.CreateClient("RentCarApi");
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                Console.WriteLine("failed1");
                return View(model);
            }

            var result = await _accountService.RegisterUser(model);

            if (result)
            {
                Console.WriteLine("Successfull");
                return RedirectToAction("Login");
            }
            Console.WriteLine("failed2");
            ModelState.AddModelError("","Registration failed. Please Try Again");
            return View(model);

        }


        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var json = JsonSerializer.Serialize(model);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("http://localhost:5271/api/Login", content);

            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                var loginResponse = JsonSerializer.Deserialize<LoginResponseDto>(responseBody,
                                    new JsonSerializerOptions{PropertyNameCaseInsensitive = true});

                if(loginResponse == null)
                {
                    Console.WriteLine("Can't proccess login response");
                    ModelState.AddModelError("", "Can't process login response");
                    return View(model);
                }

                HttpContext.Session.SetString("AuthToken", loginResponse.AccessToken); 
                HttpContext.Session.SetString("UserEmail", loginResponse.UserEmail); 
                HttpContext.Session.SetString("UserId", loginResponse.UserId);
                HttpContext.Session.SetString("UserName", loginResponse.UserName);

                if(string.IsNullOrEmpty(HttpContext.Session.GetString("UserName")))
                {
                    Console.WriteLine("Fail");
                }
                else
                {
                    Console.WriteLine("Perfect");
                }

                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Login Failed, Check your email/password again");
            return View(model);


        }

        [HttpPost] public IActionResult Logout() 
        { 
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home"); 
        }
    }
}