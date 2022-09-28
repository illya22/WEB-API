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
    public class ReceiptDetailRepository : IReceiptDetailRepository
    {
        private AppDbContext db;

        public ReceiptDetailRepository(AppDbContext context)
        {
            db = context;
        }

        public async Task AddAsync(ReceiptDetail entity)
        {
            await db.ReceiptsDetails.AddAsync(entity);
        }

        public void Delete(ReceiptDetail entity)
        {
            db.ReceiptsDetails.Remove(entity);
        }

        public async Task DeleteByIdAsync(int id)
        {
            ReceiptDetail detail = await db.ReceiptsDetails.FindAsync(id);
            if (detail != null)
            {
                db.ReceiptsDetails.Remove(detail);
            }
        }

        public async Task<IEnumerable<ReceiptDetail>> GetAllAsync()
        {
            return await db.ReceiptsDetails.ToListAsync();
        }

        public async Task<IEnumerable<ReceiptDetail>> GetAllWithDetailsAsync()
        {
            return await db
                         .ReceiptsDetails
                         .Include(x => x.Product)
                         .ThenInclude(x => x.Category)
                         .Include(x => x.Receipt)
                         .ThenInclude(x => x.Customer)
                         .ThenInclude(x => x.Person)
                         .ToListAsync();
        }

        public async Task<ReceiptDetail> GetByIdAsync(int id)
        {
            return await db
                        .ReceiptsDetails
                        .FirstOrDefaultAsync(x => x.Id == id);
        }

        public void Update(ReceiptDetail entity)
        {
            db.Entry(entity).State = EntityState.Modified;
        }
    }
}
