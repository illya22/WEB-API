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
    public class ReceiptRepository : IReceiptRepository
    {
        private AppDbContext db;

        public ReceiptRepository(AppDbContext context)
        {
            db = context;
        }

        public async Task AddAsync(Receipt entity)
        {
            await db.Receipts.AddAsync(entity);
        }

        public void Delete(Receipt entity)
        {
            db.Receipts.Remove(entity);
        }

        public async Task DeleteByIdAsync(int id)
        {
            Receipt receipt = await db.Receipts.FindAsync(id);
            if (receipt != null)
            {
                db.Receipts.Remove(receipt);
            }
        }

        public async Task<IEnumerable<Receipt>> GetAllAsync()
        {
            return await db.Receipts.ToListAsync();
        }

        public async Task<IEnumerable<Receipt>> GetAllWithDetailsAsync()
        {
            return await db
                         .Receipts
                          .Include(x => x.Customer)
                          .Include(x => x.ReceiptDetails)
                          .ThenInclude(x => x.Product)
                          .ThenInclude(x => x.Category)
                          .ToListAsync();
        }

        public async Task<Receipt> GetByIdAsync(int id)
        {
            return await db
                        .Receipts
                        .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Receipt> GetByIdWithDetailsAsync(int id)
        {
            return await db
                         .Receipts
                          .Include(x => x.Customer)
                          .Include(x => x.ReceiptDetails)
                          .ThenInclude(x => x.Product)
                          .ThenInclude(x => x.Category)
                          .FirstOrDefaultAsync(x => x.Id == id);
        }

        public void Update(Receipt entity)
        {
            db.Entry(entity).State = EntityState.Modified;
        }
    }
}
