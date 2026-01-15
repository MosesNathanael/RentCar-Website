using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using RentCar_API.Data;
using RentCar_API.DTOs;
using RentCar_API.Models;
using RentCar_API.PasswordHash;

namespace RentCar_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegisterController : ControllerBase
    {
        private readonly RentCarDbContext _context;

        private readonly  PasswordHasher _hash;

        public RegisterController(RentCarDbContext context, PasswordHasher hash)
        {
            _context = context;
            _hash = hash;
        }

        
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto )
        {
            if(await _context.Customers.AnyAsync(c => c.Email == dto.Email))
            {
                return BadRequest(new {message = "Account already exists"});
            }

            string hashedPassword = _hash.HashPassword(dto.Password);
            

            var customer = new MsCustomer
            {
                Email = dto.Email,
                Password = hashedPassword,
                Name = dto.Name,
                Phone_number = dto.Phone_number,
                Address = dto.Address,
                Driver_license_number = dto.Driver_license_number
            };


            _context.Customers.Add(customer);

            await _context.SaveChangesAsync();

            return Ok("Customer added");
        }
        
    }
}