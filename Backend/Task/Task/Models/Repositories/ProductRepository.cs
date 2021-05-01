using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Task.Models.Repositories
{
    public class ProductRepository : IRepository<Product>
    {
        TaskContext context;
        public ProductRepository(TaskContext _context)
        {
            context = _context;
        }
        public void Add(Product newProduct)
        {
            context.Products.Add(newProduct);
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            Product productInDb = context.Products.FirstOrDefault(p => p.ProductID == id);
            if (productInDb != null)
            {
                context.Products.Remove(productInDb);
                context.SaveChanges();
            }
        }

        public Product Find(int id)
        {
            Product productInDb = context.Products.FirstOrDefault(p => p.ProductID == id);
            return productInDb;
        }

        public IList<Product> Get()
        {
            IList<Product> productList =context.Products.ToList();
            return productList;
        }

        public Product Login(Product entity)
        {
            throw new NotImplementedException();
        }

        public IList<Product> Search(string term)
        {
            List<Product> ResultSearch = context.Products
                .Where
                (
                  p => p.Name.Contains(term) ||
                       p.Image.Contains(term)
                ).ToList();
            return ResultSearch;
        }

        public void Update(int id, Product newProduct)
        {
            Product productInDb = context.Products.FirstOrDefault(p => p.ProductID == id);
            if (productInDb != null)
            {
                productInDb.Name = newProduct.Name;
                productInDb.Image = newProduct.Image;
                productInDb.Price = newProduct.Price;
                context.SaveChanges();
            }
        }
    }
}
