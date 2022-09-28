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
    public class ProductCategoryRepository : IProductCategoryRepository
    {
        private AppDbContext db;

        public ProductCategoryRepository(AppDbContext context)
        {
            db = context;
        }

        public async Task AddAsync(ProductCategory entity)
        {
            await db.ProductCategories.AddAsync(entity);
        }

        public void Delete(ProductCategory entity)
        {
            db.ProductCategories.Remove(entity);
        }

        public async Task DeleteByIdAsync(int id)
        {
            ProductCategory p_category = await db.ProductCategories.FindAsync(id);
            if (p_category != null)
            {
                db.ProductCategories.Remove(p_category);
            }
        }

        public async Task<IEnumerable<ProductCategory>> GetAllAsync()
        {
            return await db.ProductCategories.ToListAsync();
        }

        public async Task<ProductCategory> GetByIdAsync(int id)
        {
            return await db
                         .ProductCategories
                         .FirstOrDefaultAsync(x => x.Id == id);
        }

        public void Update(ProductCategory entity)
        {
            db.Entry(entity).State = EntityState.Modified;
        }
    }
}
