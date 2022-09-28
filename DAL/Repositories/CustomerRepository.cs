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
    public class CustomerRepository : ICustomerRepository
    {
        private AppDbContext db;

        public CustomerRepository(AppDbContext context)
        {
            db = context;
        }

        public async Task AddAsync(Customer entity)
        {
            await db.Customers.AddAsync(entity);
        }

        public void Delete(Customer entity)
        {
            db.Customers.Remove(entity);
        }

        public async Task DeleteByIdAsync(int id)
        {
            Customer customer = await db.Customers.FindAsync(id);
            if (customer != null)
            {
                db.Customers.Remove(customer);
            }
        }

        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            return await db
                 .Customers
                 .ToListAsync();
        }

        public async Task<IEnumerable<Customer>> GetAllWithDetailsAsync()
        {
            return await db.
                Customers
                .Include(x => x.Person)
                .Include(x => x.Receipts)
                .ThenInclude(x => x.ReceiptDetails)
                .ToListAsync();
        }

        public async Task<Customer> GetByIdAsync(int id)
        {
            return await db
               .Customers
               .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Customer> GetByIdWithDetailsAsync(int id)
        {
            return await db
                .Customers
                .Include(x => x.Person)
                .Include(x => x.Receipts)
                .ThenInclude(x => x.ReceiptDetails)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public void Update(Customer entity)
        {
            db.Entry(entity).State = EntityState.Modified;
        }
    }
}
