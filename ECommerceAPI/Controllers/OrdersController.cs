using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace ECommerceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly MyDbContext _dbContext;

        public OrdersController(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] OrderRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var customer = new Customer
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Phone = request.Phone,
            };

            var order = new Order
            {
                Customer = customer,
                TotalAmount = request.TotalAmount,
                ShippingAddress = request.ShippingAddress,
                OrderDate = DateTime.UtcNow,
            };

            foreach (var productRequest in request.Products)
            {
                var product = _dbContext.Products.FirstOrDefault(p => p.Id == productRequest.Id);
                if (product == null)
                {
                    return BadRequest($"Product with ID {productRequest.Id} not found.");
                }

                var orderLine = new OrderLine
                {
                    Product = product,
                    Quantity = productRequest.Quantity,
                    UnitPrice = product.Price,
                };

                order.OrderLines.Add(orderLine);
            }

            await _dbContext.Orders.AddAsync(order);
            await _dbContext.SaveChangesAsync();

            return Ok(order);
        }
    }
}

