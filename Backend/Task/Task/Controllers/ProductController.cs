using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task.Models;

namespace Task.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly IRepository<Product> productRepository;

        public IHostingEnvironment Hosting { get; }
        public ProductController(IRepository<Product> ProductRepository, IHostingEnvironment hosting)
        {
            productRepository = ProductRepository;
            Hosting = hosting;
        }
        [HttpGet]
        public IActionResult GetProducts()
        {
            IList<Product> productList =productRepository.Get().ToList();
            return Ok(productList);
        }

        [HttpGet]
        public IActionResult GetProductById(int id)
        {
            Product productList = productRepository.Find(id);
            return Ok(productList);
        }

        [HttpPost]
        public IActionResult createProduct(Product newProduct)
         {
            productRepository.Add(newProduct);
            return Ok();
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            productRepository.Delete(id);
            return Ok("Deleted Successfully");
        }

        [HttpPut]
        public IActionResult Edit(int id,Product newProduct)
        {
            productRepository.Update(id, newProduct);
            return Ok("Updated Successfully");
        }
    }
}
