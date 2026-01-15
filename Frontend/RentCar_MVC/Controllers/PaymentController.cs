using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using RentCar_API.DTOs;
using RentCar_MVC.Models;
using RentCar_MVC.Services;

namespace RentCar_MVC.Controllers
{
    public class PaymentController : Controller
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentServices)
        {
            _paymentService = paymentServices;
        }
        [HttpGet]
        public async Task<ActionResult> DisplayPayment()
        {
            var user = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(user))
            {
                return RedirectToAction("Login", "Account");
            }
            if(TempData["RentalResponse"] is string rentalJson)
            {
                var rentalResponse = JsonSerializer.Deserialize<RentalResponseDto>(rentalJson, new JsonSerializerOptions{PropertyNameCaseInsensitive = true});
                if(rentalResponse == null)
                {
                    return View("Error");
                }
                var result = await _paymentService.DisplayRental(rentalResponse.RentalId);
                if(result == null)
                {
                    return View("Error");
                }
                TempData.Keep("RentalResponse");
                return View("~/Views/Rental/PaymentPage.cshtml", result);
            }
            
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<ActionResult> ProcessPayment(PaymentMethodDto payment, string rentalId)
        {
            var user = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(user))
            {
                return RedirectToAction("Login", "Account");
            }

            if(TempData["RentalResponse"] is string rentalJson)
            {
                var rentalResponse = JsonSerializer.Deserialize<RentalResponseDto>(rentalJson, new JsonSerializerOptions{PropertyNameCaseInsensitive = true});
                if(rentalResponse == null)
                {
                    return View("Error");
                }
                var result = await _paymentService.ConfirmPayment(payment, rentalResponse.RentalId);
                if(result == false)
                {
                    return View("Error");
                }
                return RedirectToAction("ViewHistory", "TransactionHistory");
            }
            return RedirectToAction("Index", "Home");
        }
        
    }
}