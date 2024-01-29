using iBay.Entities.Contexts;
using iBay.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace iBay.Entities.Repositories
{
    public class PaymentRepository : IBasicRepository<Payment>
    {
        private readonly iBayContext _context;

        public PaymentRepository(iBayContext context)
        {
            _context = context;
        }

        public async Task<Payment> GetByIdAsync(int id)
        {
            return await _context.Payments.FindAsync(id);
        }

        public async Task<IEnumerable<Payment>> GetAllAsync()
        {
            return await _context.Payments.ToListAsync();
        }

        public async Task AddAsync(Payment entity)
        {
            _context.Payments.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Payment entity)
        {
            _context.Payments.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var payment = await _context.Payments.FindAsync(id);
            if (payment != null)
            {
                _context.Payments.Remove(payment);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteAllAsync(Func<Payment, bool> predicate)
        {
            var paymentsToDelete = _context.Payments.Where(predicate).ToList();
            _context.Payments.RemoveRange(paymentsToDelete);
            await _context.SaveChangesAsync();
        }
    }
}