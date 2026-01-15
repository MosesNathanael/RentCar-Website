using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RentCar_MVC.Models;
using RentCar_MVC.Services;

namespace RentCar_MVC.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    private readonly IDisplayCarService _carService;

    public HomeController(ILogger<HomeController> logger, IDisplayCarService carService)
    {
        _logger = logger;
        _carService = carService;
    }

    public async Task<IActionResult> Index(string sort = "Asc") 
    { 
        var cars = await _carService.GetCarsAsync(sort); 
        return View(cars); 
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
    
}
