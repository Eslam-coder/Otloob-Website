using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task.Models;

namespace Task.Controllers
{
    [ApiController]
    [Route("api/[controller]/[Action]")]
    public class ShoppingCartController : Controller
    {
        private readonly IRepository<UserProduct> UserProductRepository;

        public ShoppingCartController(IRepository<UserProduct>userProductRepository)
        {
            userProductRepository = UserProductRepository;
        }

        [HttpGet]
        public IActionResult GetProductsChosenByUserId(int id)
        {
           var products = UserProductRepository.Find(id);
            return Ok(products);
        }

        [HttpPost]
        public IActionResult AddProductToCart(UserProduct userProduct)
        {
            UserProductRepository.Add(userProduct);
            return Ok("Added To Cart Successfully");
        }


    }
}
