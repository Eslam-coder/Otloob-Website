using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task.Models;
using Microsoft.EntityFrameworkCore;
namespace Task.Controllers
{
    [ApiController]
    [Route("api/[controller]/[Action]")]
    public class CartController : Controller
    {
        TaskContext context;
        public CartController(TaskContext _context)
        {
            context = _context;
        }
        [HttpGet]
        public IActionResult GetProductsChosenByUserId(int id)
        {
            var products = context.UserProducts
                           .Include(UserProducts => UserProducts.Product)
                           .Where(u => u.UserID == id)
                           .Select(p =>
                                       new
                                       {
                                           p.Product.Name,
                                           p.Product.Price,
                                           p.Product.Image,
                                           p.Product.Description,
                                       });

           // return (UserProduct)products;
            return Ok(products);
        }

        [HttpPost]
        public IActionResult AddProductToCart(UserProduct userProduct)
        {
            context.UserProducts.Add(userProduct);
            context.SaveChanges();
            return Ok("Added To Cart Successfully");
        }
    }
}
