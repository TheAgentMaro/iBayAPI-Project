using iBay.Entities.Contexts;
using iBay.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace iBay.Entities.Repositories
{
public class CartItemRepository : IBasicRepository<CartItem>
{
    private readonly iBayContext _context;

    public CartItemRepository(iBayContext context)
    {
        _context = context;
    }

    public async Task<CartItem> GetByIdAsync(int id)
    {
        return await _context.CartItems.FindAsync(id);
    }

    public async Task<IEnumerable<CartItem>> GetAllAsync()
    {
        return await Task.FromResult(_context.CartItems.ToList());
    }

    public async Task AddAsync(CartItem entity)
    {
        _context.CartItems.Add(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(CartItem entity)
    {
        _context.CartItems.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await GetByIdAsync(id);
        if (entity != null)
        {
            _context.CartItems.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }

    public async Task DeleteAllAsync(Func<CartItem, bool> predicate)
    {
        var entities = _context.CartItems.Where(predicate);
        _context.CartItems.RemoveRange(entities);
        await _context.SaveChangesAsync();
    }
}
}


