using CustomerAPI.Data;
using CustomerAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly CustomerDbContext _context;

        public CustomerController(CustomerDbContext context)
        {
            _context = context;
        }
        // Add a New Customer
        [HttpPost]
        public async Task<IActionResult> AddCustomer([FromBody] Customer newCustomer)
        {
            _context.Customers.Add(newCustomer);
            await _context.SaveChangesAsync();
            return Ok(newCustomer);
        }

        // Update an Existing Customer's City
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomerCity(int id, [FromBody] string newCity)
        {
            var customer = _context.Customers.FirstOrDefault(c => c.Id == id);
            if (customer == null) return NotFound();

            customer.City = newCity;
            await _context.SaveChangesAsync();
            return Ok(customer);
        }

        // Get All Customers Ordered by Name
        [HttpGet("ordered-by-name")]
        public IActionResult GetCustomersOrderedByName()
        {
            var customers = _context.Customers.OrderBy(c => c.Name).ToList();
            return Ok(customers);
        }
        // Get All Customers Older than 25
        [HttpGet("older-than-25")]
        public IActionResult GetCustomersOlderThan25()
        {
            var customers = _context.Customers.Where(c => c.Age > 25).ToList();
            return Ok(customers);
        }
        
        // Select Only the Customer Names
        [HttpGet("names")]
        public IActionResult GetCustomerNames()
        {
            var customerNames = _context.Customers.Select(c => c.Name).ToList();
            return Ok(customerNames);
        }
    }
}
