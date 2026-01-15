using Microsoft.AspNetCore.Mvc;
using RentCar_MVC.DTOs;
using RentCar_MVC.Services;
using RentCar_MVC.Models;
using System.Diagnostics;
using System.Text.Json;


namespace RentCar_MVC.Controllers
{
    public class DisplayCarController : Controller{
        private readonly IDisplayCarService _carService;
        private readonly IRentalCarService _rentalService;
        public DisplayCarController(IDisplayCarService carService, IRentalCarService rentalService)
        {
            _carService = carService;
            _rentalService = rentalService;
        }


        public async Task<IActionResult> Search(int year, string sort, DateTime rentalDate, DateTime returnDate)
        {
            var client = new HttpClient();
            var response = await _carService.GetCarsByYear(year, sort);
            if(year == 0)
            {
                response = await _carService.GetCarsAsync(sort);
            }

            if(response == null)
            {
                response = new List<CarDisplayDto>();
            }
            ViewBag.SearchStatus = true;
            ViewBag.RentalDate = rentalDate;
            ViewBag.ReturnDate = returnDate;
            return View("~/Views/Home/Index.cshtml", response);
        }

        [HttpGet]
        public async Task<IActionResult> RentalDetails(string id, DateTime rentalDate, DateTime returnDate)
        {
            var user = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(user))
            {
                return RedirectToAction("Login", "Account");
            }
            if(id == null)
            {
                Console.WriteLine("id is null");
                return View();
            }
            else
            {
                Console.WriteLine(id);
            }
            
            
            var carDetails = await _carService.GetCarDetails(id, rentalDate, returnDate);
            if (string.IsNullOrEmpty(carDetails.CarId))
            {

                Console.WriteLine("Error carDetail is empty");
                return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });

                

            }

            return View("~/Views/Rental/RentalDetails.cshtml", carDetails);

        }


        [HttpPost]
        public async Task<ActionResult> ComfirmRental(CarDetailsDto model)
        {
            var user = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(user))
            {
                return RedirectToAction("Login", "Account");
            }

            if (!ModelState.IsValid)
            {
                Console.WriteLine("Failed1");
                return View("~/Views/Rental/RentalDetails.cshtml", model);
            }
            var rentalResponse = await _rentalService.RentalService(model);
            if(rentalResponse == null)
            {
                ModelState.AddModelError("", "Rental failed, please try again later");
                Console.WriteLine("Failed2");
                return View("~/Views/Rental/RentalDetails.cshtml",model);
            }

            TempData["RentalResponse"] = JsonSerializer.Serialize(rentalResponse);
            Console.WriteLine("successfull redirect to payment");
            return RedirectToAction("DisplayPayment", "Payment");
        } 

    }
    

}