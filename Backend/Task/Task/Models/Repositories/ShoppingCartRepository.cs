using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
namespace Task.Models.Repositories
{
    public class ShoppingCartRepository : IRepository<UserProduct>
    {
        TaskContext context;
        public ShoppingCartRepository(TaskContext _context)
        {
            context = _context;
        }
        public void Add(UserProduct userProduct)
        {
            context.UserProducts.Add(userProduct);
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            UserProduct userProductInDb = context.UserProducts.Find(id);
            if (userProductInDb != null)
            {
                context.UserProducts.Remove(userProductInDb);
                context.SaveChanges();
            }
        }

        public UserProduct Find(int id)
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

            return (UserProduct)products;
            //IList<int> ProductId = context.UserProducts.Where(u => u.UserID == id)
            //                       .Select(p=>p.ProductID).ToList();
            //foreach (var ID in ProductId)
            //{
            //    Product ProductInDb = context.Products.FirstOrDefault(p => p.ProductID == ID);
            //    return ProductInDb;
            //}
            //return ProductInDb;
            //throw new NotImplementedException();
        }

        public IList<UserProduct> Get()
        {
            IList<UserProduct> UserProduct = context.UserProducts.ToList();
            return UserProduct;
        }

        public UserProduct Login(UserProduct entity)
        {
            throw new NotImplementedException();
        }

        public IList<UserProduct> Search(string term)
        {
            throw new NotImplementedException();
        }

        public void Update(int id, UserProduct entity)
        {
            throw new NotImplementedException();
        }

    }
}
