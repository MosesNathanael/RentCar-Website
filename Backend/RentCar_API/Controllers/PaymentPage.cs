using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentCar_API.Data;
using RentCar_API.DTOs;
using RentCar_API.Models;

namespace RentCar_API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly RentCarDbContext _context;

        public PaymentController(RentCarDbContext context)
        {
            _context = context;
        }

        [HttpGet("{rentalId}/Payment")]
        public async Task<ActionResult> displayPayment(string rentalId)
        {
            var rental = await _context.Rentals.FirstOrDefaultAsync(r => r.Rental_id == rentalId);

            if(rental == null)
            {
                return NotFound("Rental not found");
            }

            return Ok(rental);
            
        }


        [HttpPost("{rentalId}/Payment")]

        public async Task<ActionResult> processPayment([FromBody] PaymentMethod dto,string rentalId)
        {
            var rental = await _context.Rentals.FirstOrDefaultAsync(r => r.Rental_id == rentalId);
            if(rental == null) return NotFound("Payment failed");

            var payment = new LtPayment
            {
                Payment_date = DateTime.UtcNow,
                Amount = rental.Total_price,
                Payment_method = dto.paymentMethod,
                Rental_id = rentalId,
                Rental = rental
            };

            _context.Payments.Add(payment);

            rental.Payment_status = true;

            await _context.SaveChangesAsync();
            
            return Ok("Payment Successfull");
        }
    }
}