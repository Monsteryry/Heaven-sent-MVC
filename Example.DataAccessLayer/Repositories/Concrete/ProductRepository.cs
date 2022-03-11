using DataAccessLayer.Repositories.Abstract;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories.Concrete
{
    public class ProductRepository : BaseRepository, IProductRepository
    {
        public async Task<Product> GetProductAsync(int id)
        {
            return await context.Products.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Product>> GetProductsAsync()
        {
            return await context.Products.ToListAsync();
        }

        public async Task<bool> SaveProductAsync(Product product)
        {
            if (product == null)
                return false;

            try
            {
                context.Entry(product).State = product.Id == default(int) ? EntityState.Added : EntityState.Modified;

                await context.SaveChangesAsync();
            }

            catch(Exception)
            {
                return false;
            }

            return true;
        }
        public async Task<bool> DeleteProductAsync(Product product)
        {
            if (product == null)
                return false;

            context.Products.Remove(product);

            try
            {
                await context.SaveChangesAsync();
            }

            catch (Exception)
            {
                return false;
            }

            return true;
        }
    }
}
