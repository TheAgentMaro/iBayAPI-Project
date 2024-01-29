using System.Collections.Generic;
using System.Threading.Tasks;
using iBay.Entities.Models;

namespace iBay.WebAPI.Interfaces
{
    public interface ICartService
    {
        Task<IEnumerable<CartItem>> GetCartItemsAsync(string userName);
        Task AddToCartAsync(CartItem item, string userName);
        Task<CartItem> UpdateCartItemAsync(int itemId, CartItem updatedItem, string userName);
        Task RemoveFromCartAsync(int itemId, string userName);
        Task CheckoutAsync(string userName);
    }
}
