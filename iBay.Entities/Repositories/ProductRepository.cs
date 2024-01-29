using iBay.Entities.Contexts;
using iBay.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace iBay.Entities.Repositories
{
     public class ProductRepository : IBasicRepository<Product>
    {
        private readonly iBayContext _context;

        public ProductRepository(iBayContext context)
        {
            _context = context;
        }
        public async Task<Product> GetSingleAsync(Expression<Func<Product, bool>> predicate)
        {
            return await _context.Products.SingleOrDefaultAsync(predicate);
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            return await _context.Products.FindAsync(id);
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task AddAsync(Product entity)
        {
            _context.Products.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Product entity)
        {
            _context.Products.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Products.FindAsync(id);
            if (entity != null)
            {
                _context.Products.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteAllAsync(Func<Product, bool> predicate)
        {
            var entities = _context.Products.Where(predicate);
            _context.Products.RemoveRange(entities);
            await _context.SaveChangesAsync();
        }
    }
}
