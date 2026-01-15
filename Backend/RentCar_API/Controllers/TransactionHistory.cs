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
    public class TransactionHistoryController : ControllerBase
    {
        private readonly RentCarDbContext _context;
        public TransactionHistoryController(RentCarDbContext context)
        {
            _context = context;
        }


        [HttpGet("History")]
        public async Task<ActionResult> history()
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            var user = await _context.Customers.FirstOrDefaultAsync(c => c.Email == email);
            if(user == null)
            {
                return NotFound("User not found");
            }
            var trans_histo = await _context.Rentals
                                .OrderBy(t => t.Rental_date)
                                .Where(t => t.Customer_id == user.Customer_id)
                                .Select(t => new TransactionHistoryDto
                                {
                                    RentalDate = t.Rental_date,
                                    ReturnDate = t.Return_date,
                                    CarName = t.Car.Name,
                                    Price = t.Car.Price_per_day,
                                    Total_Days = (t.Return_date - t.Rental_date).Days,
                                    Total_Price = t.Total_price,
                                    status = t.Payment_status,
                                }
                                    
                                )
                                .ToListAsync();

            if(!trans_histo.Any())
            {
                return NoContent();
            }

            return Ok(trans_histo);
        }
        
    }
}