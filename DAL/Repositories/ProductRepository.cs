using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Data;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private AppDbContext db;

        public ProductRepository(AppDbContext context)
        {
            db = context;
        }

        public async Task AddAsync(Product entity)
        {
            await db.Products.AddAsync(entity);
        }

        public void Delete(Product entity)
        {
            db.Products.Remove(entity);
        }

        public async Task DeleteByIdAsync(int id)
        {
            Product product = await db.Products.FindAsync(id);
            if (product != null)
            {
                db.Products.Remove(product);
            }
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await db.Products.ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetAllWithDetailsAsync()
        {
            return await db
                         .Products
                         .Include(x => x.Category)
                         .Include(x => x.ReceiptDetails)
                         .ToListAsync();
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            return await db
                         .Products
                         .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Product> GetByIdWithDetailsAsync(int id)
        {
            return await db
                        .Products
                        .AsNoTracking()
                        .Include(x => x.Category)
                        .Include(x => x.ReceiptDetails)
                        .FirstOrDefaultAsync(x => x.Id == id);
        }

        public void Update(Product entity)
        {
            db.Entry(entity).State = EntityState.Modified;
        }
    }
}
