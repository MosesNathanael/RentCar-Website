using Microsoft.AspNetCore.Mvc;
using RentCar_API.Models;
using RentCar_API.Data;
using RentCar_API.DTOs;

namespace RentCar_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HomePageController : ControllerBase
    {

        private readonly RentCarDbContext _context;

        public HomePageController(RentCarDbContext context)
        {
            _context = context;
        }

        [HttpGet("DisplayCar/All/{sort}")]
        public ActionResult<IEnumerable<CarDisplayDto>> GetAllCars(string sort = "asc")
        {

            if(sort == "asc")
            {
                var cars = _context.Cars
                .OrderBy(c => c.Price_per_day)
                .Where(c => c.Status == true)
                .Select(c => new
                {
                    c.Car_id,
                    c.Name,
                    c.Price_per_day,
                    Images = c.CarImages
                        .Where(img => img.FrontImage == true)
                        .Select(img => img.Image_link)
                        .FirstOrDefault()
                })
                .ToList();
                return Ok(cars);
            }
            else
            {
                var cars = _context.Cars
                .OrderByDescending(c => c.Price_per_day)
                .Where(c => c.Status == true)
                .Select(c => new
                {
                    c.Car_id,
                    c.Name,
                    c.Price_per_day,
                    Images = c.CarImages
                        .Where(img => img.FrontImage == true)
                        .Select(img => img.Image_link)
                        .FirstOrDefault()
                })
                .ToList();
                return Ok(cars);
            }
            
        }

        [HttpGet("DisplayCar/{year}/{sort}")]
        public ActionResult<IEnumerable<CarDisplayDto>> GetByYear(decimal year, string sort = "asc")
        {

            if(sort == "asc")
            {
                var carsAsc = _context.Cars
                .OrderBy(c => c.Price_per_day)
                .Where(c => c.Year == year)
                .Where(c => c.Status == true)
                .Select(c => new
                {
                    c.Car_id,
                    c.Name,
                    c.Price_per_day,
                    Images = c.CarImages
                        .Where(img => img.FrontImage == true)
                        .Select(img => img.Image_link)
                        .FirstOrDefault()
                })
                .ToList();

                return Ok(carsAsc);
            }
            var carsDesc = _context.Cars
                .OrderByDescending(c => c.Price_per_day)
                .Where(c => c.Year == year)
                .Where(c => c.Status == true)
                .Select(c => new
                {
                    c.Car_id,
                    c.Name,
                    c.Price_per_day,
                    Images = c.CarImages
                        .Where(img => img.FrontImage == true)
                        .Select(img => img.Image_link)
                        .FirstOrDefault()
                })
                .ToList();

            return Ok(carsDesc);
        }

        
    }

}