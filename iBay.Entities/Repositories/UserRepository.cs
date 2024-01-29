using iBay.Entities.Contexts;
using iBay.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
 

namespace iBay.Entities.Repositories
{
public class UserRepository : IBasicRepository<User>
    {
        private readonly iBayContext _context;

        public UserRepository(iBayContext context)
        {
            _context = context;
        }
        public async Task<User> GetSingleAsync(Expression<Func<User, bool>> predicate)
        {
            return await _context.Users.SingleOrDefaultAsync(predicate);
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task AddAsync(User entity)
        {
            _context.Users.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(User entity)
        {
            _context.Users.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                _context.Users.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteAllAsync(Func<User, bool> predicate)
        {
            var entities = _context.Users.Where(predicate);
            _context.Users.RemoveRange(entities);
            await _context.SaveChangesAsync();
        }
    }
}
