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
    public class PersonRepository : IPersonRepository
    {
        private AppDbContext db;

        public PersonRepository(AppDbContext context)
        {
            db = context;
        }

        public async Task AddAsync(Person entity)
        {
            await db.Persons.AddAsync(entity);
        }

        public void Delete(Person entity)
        {
            db.Persons.Remove(entity);
        }

        public async Task DeleteByIdAsync(int id)
        {
            Person person = await db.Persons.FindAsync(id);
            if (person != null)
            {
                db.Persons.Remove(person);
            }
        }

        public async Task<IEnumerable<Person>> GetAllAsync()
        {
            return await db.Persons.ToListAsync();
        }

        public async Task<Person> GetByIdAsync(int id)
        {
            return await db
                .Persons
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public void Update(Person entity)
        {
            db.Entry(entity).State = EntityState.Modified;
        }
    }
}
