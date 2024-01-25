using iBay.Entities.Contexts;
using iBay.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iBay.Entities.Repositories
{
    public class CartItemRepository : IBasicRepository<CartItem>
    {
        private readonly iBayContext _context;

        public CartItemRepository(iBayContext context)
        {
            _context = context;
        }

        public CartItem GetById(int id)
        {
            return _context.CartItems.Find(id);
        }

        public IEnumerable<CartItem> GetAll()
        {
            return _context.CartItems;
        }

        public void Add(CartItem entity)
        {
            _context.CartItems.Add(entity);
            _context.SaveChanges();
        }

        public void Update(CartItem entity)
        {
            _context.CartItems.Update(entity);
            _context.SaveChanges();
        }

        public void Delete(CartItem entity)
        {
            _context.CartItems.Remove(entity);
            _context.SaveChanges();
        }
    }
}
