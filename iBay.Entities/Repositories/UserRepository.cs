using iBay.Entities.Contexts;
using iBay.Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace iBay.Entities.Repositories
{
    public class UserRepository : IBasicRepository<ApplicationUser>
    {
        private readonly iBayContext _context;

        public UserRepository(iBayContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<ApplicationUser> GetSingleAsync(Expression<Func<ApplicationUser, bool>> predicate)
        {
            return await _context.Users.SingleOrDefaultAsync(predicate);
        }

        public async Task<ApplicationUser> GetByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<IEnumerable<ApplicationUser>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task AddAsync(ApplicationUser entity)
        {
            _context.Users.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ApplicationUser entity)
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

        public async Task DeleteAllAsync(Func<ApplicationUser, bool> predicate)
        {
            var entities = _context.Users.Where(predicate);
            _context.Users.RemoveRange(entities);
            await _context.SaveChangesAsync();
        }
    }
}