using DemoBS23.DAL.DatabaseContext;
using DemoBS23.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoBS23.DAL.Repositories
{
    public class ProductRepo : IProductRepo
    {
        private readonly ProductDbContext _context;

        public ProductRepo(ProductDbContext context)
        {
            _context = context;
        }

       
        public async Task<Product> AddOne(Product newProduct)
        {
            try
            {
                await _context.Products.AddAsync(newProduct);

                await _context.SaveChangesAsync();

                return newProduct;                
            }
            catch (Exception)
            {
                throw;
            }
            
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            IList<Product> productsFromDb = null;

            try
            {
                productsFromDb = _context.Products.Select(x=>x).ToList();    
                return productsFromDb;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Product> GetById(int id)
        {
            Product productFromDb = null;
            try
            {
                productFromDb = _context.Products.FirstOrDefault(x => x.Id == id);
                return productFromDb;
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        public async Task<Product> Update(Product newProduct)
        {
            _context.Update(newProduct);
        }
    }
}
