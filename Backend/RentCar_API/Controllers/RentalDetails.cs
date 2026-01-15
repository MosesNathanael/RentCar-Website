using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RentCar_API.Data;
using RentCar_API.DTOs;
using RentCar_API.Models;

namespace RentCar_API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class RentalDetails : ControllerBase
    {
        private readonly RentCarDbContext _context;

        public RentalDetails(RentCarDbContext context)
        {
            _context = context;
        }

        [HttpGet("CarDetails/{id}")]
        public async Task<ActionResult> Details(string id, [FromQuery] DateTime rentalDate, [FromQuery] DateTime returnDate)
        {
            if(rentalDate >= returnDate)
            {
                return BadRequest("Return date not valid");
            }
            var car = await _context.Cars.FindAsync(id); 
            if(car == null)
            {
                return NotFound();
            }
            
            var image = await _context.CarImages
                            .Where(c => c.Car_id == car.Car_id)
                            .Select(c => c.Image_link)
                            .ToListAsync();
            if(image == null)
            {
                return NotFound("Images not found");
            }

            var duration = (returnDate - rentalDate).Days;


            var car_details = new  RentalDetailsDto
            {
                CarImages = image,
                CarId = car.Car_id,
                CarName = car.Name,
                Transmission = car.Transmission,
                RentalDate = rentalDate,
                ReturnDate = returnDate,
                Price = car.Price_per_day,
                Total_Price = car.Price_per_day * duration
            };


            return Ok(car_details);
        }


        [HttpPost("CreateRental")]
        public async Task<ActionResult> CreateRental(RentalDetailsDto dto)
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            var user = await _context.Customers.FirstOrDefaultAsync(c => c.Email == email);
            if(user == null)
            {
                return NotFound("User not found");
            }

            var car = await _context.Cars.FirstOrDefaultAsync(c => c.Car_id == dto.CarId);
            if(car == null)
            {
                return NotFound("Car not found");
            }
            
            var rental = new TrRental
            {
                Rental_date = dto.RentalDate,
                Return_date = dto.ReturnDate,
                Total_price = dto.Total_Price,
                Payment_status = false,
                Customer_id = user.Customer_id,
                Car_id = dto.CarId,
                Car = car,
                Customer = user
            };


            _context.Rentals.Add(rental);
            await _context.SaveChangesAsync();


            var responseDto = new RentalResponseDto {
                RentalId = rental.Rental_id, 
                CarId = rental.Car_id,
                RentalDate = rental.Rental_date, 
                ReturnDate = rental.Return_date, 
                TotalPrice = rental.Total_price, 
                PaymentStatus = rental.Payment_status 
            };

            return CreatedAtAction(nameof(Details), new { id = dto.CarId, rentalDate = dto.RentalDate, returnDate = dto.ReturnDate }, responseDto);

        }
    }
}