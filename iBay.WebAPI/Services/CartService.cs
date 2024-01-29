using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iBay.Entities.Repositories;
using iBay.Entities.Models;
using iBay.WebAPI.Interfaces;


namespace iBay.WebAPI.Services
{
public class CartService : ICartService
{
    private readonly IBasicRepository<CartItem> _cartItemRepository;

    public CartService(IBasicRepository<CartItem> cartItemRepository)
    {
        _cartItemRepository = cartItemRepository;
    }

    // Récupère tous les articles du panier de l'utilisateur
    public async Task<IEnumerable<CartItem>> GetCartItemsAsync(string userName)
    {
        try
        {
            return await _cartItemRepository.GetAllAsync();
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Une erreur s'est produite lors de la récupération du panier.", ex);
        }
    }

    // Ajoute un nouvel article au panier de l'utilisateur
    public async Task AddToCartAsync(CartItem item, string userName)
    {
        try
        {
            item.UserName = userName;
            await _cartItemRepository.AddAsync(item);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Une erreur s'est produite lors de l'ajout de l'article au panier.", ex);
        }
    }

    // Met à jour un article existant dans le panier de l'utilisateur
    public async Task<CartItem> UpdateCartItemAsync(int itemId, CartItem updatedItem, string userName)
    {
        try
        {
            var existingItem = await _cartItemRepository.GetByIdAsync(itemId);

            if (existingItem == null)
            {
                return null;
            }
            if (existingItem.UserName != userName)
            {
                return null;
            }

            existingItem.Quantity = updatedItem.Quantity;
            existingItem.Price = updatedItem.Price;

            await _cartItemRepository.UpdateAsync(existingItem);

            return existingItem;
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Une erreur s'est produite lors de la mise à jour de l'article dans le panier.", ex);
        }
    }

    // Supprime un article du panier de l'utilisateur
    public async Task RemoveFromCartAsync(int itemId, string userName)
    {
        try
        {
            var item = await _cartItemRepository.GetByIdAsync(itemId);

            if (item != null && item.UserName == userName)
            {
                await _cartItemRepository.DeleteAsync(itemId);
            }
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Une erreur s'est produite lors de la suppression de l'article du panier.", ex);
        }
    }

    // Simule le processus de paiement/checkout du panier de l'utilisateur
    public async Task CheckoutAsync(string userName)
    {
        try
        {
            await _cartItemRepository.DeleteAllAsync(c => c.UserName == userName);
        }
        catch (Exception ex)
        {
            // Gérez les exceptions selon les besoins
            throw new ApplicationException("Une erreur s'est produite lors du paiement.", ex);
        }
    }
}
}
