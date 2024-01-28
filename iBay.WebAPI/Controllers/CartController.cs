using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
//---- à créer ----//
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
//-----------------------//
using iBay.WebAPI.Services;
using iBay.Entities.Models;

namespace iBay.WebAPI.Controllers
{
    // Ce contrôleur est sécurisé, toutes les méthodes nécessitent que l'utilisateur soit authentifié
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class CartController : ControllerBase
    {
        private readonly CartService _cartService;

        // Injecter le service de panier qui gère la logique métier
        public CartController(CartService cartService)
        {
            _cartService = cartService;
        }

        // GET: [controller]
        // Récupère tous les articles du panier de l'utilisateur actuel
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CartItem>>> GetCartItems()
        {
            try
            {
                var items = await _cartService.GetCartItemsAsync(User.Identity.Name);
                return Ok(items);
            }
            catch (Exception)
            {
                return StatusCode(500, "Une erreur s'est produite lors de la récupération de votre panier.");
            }
        }

        // POST: [controller]
        // Ajoute un nouvel article au panier de l'utilisateur
        [HttpPost]
        public async Task<ActionResult> AddToCart([FromBody] CartItem item)
        {
            try
            {
                await _cartService.AddToCartAsync(item, User.Identity.Name);
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(500, "Une erreur s'est produite lors de l'ajout de l'article au panier.");
            }
        }

        // PUT: [controller]/{id}
        // Met à jour un article existant dans le panier de l'utilisateur
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCartItem(int id, [FromBody] CartItem item)
        {
            try
            {
                if (id != item.Id)
                {
                    return BadRequest("L'identifiant de l'article ne correspond pas.");
                }

                var result = await _cartService.UpdateCartItemAsync(item, User.Identity.Name);
                if (result == null)
                {
                    return NotFound("L'article du panier n'a pas été trouvé.");
                }

                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500, "Une erreur s'est produite lors de la mise à jour de l'article dans votre panier.");
            }
        }

        // DELETE: [controller]/{id}
        // Supprime un article du panier de l'utilisateur
        [HttpDelete("{id}")]
        public async Task<ActionResult> RemoveFromCart(int id)
        {
            try
            {
                await _cartService.RemoveFromCartAsync(id, User.Identity.Name);
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(500, "Une erreur s'est produite lors de la suppression de l'article du panier.");
            }
        }

        // POST: [controller]/Checkout
        // Simule le processus de paiement/checkout du panier de l'utilisateur
        [HttpPost("Checkout")]
        public async Task<ActionResult> CheckoutCart()
        {
            try
            {
                await _cartService.CheckoutAsync(User.Identity.Name);
                return Ok("Le paiement a été effectué avec succès.");
            }
            catch (Exception)
            {
                return StatusCode(500, "Une erreur s'est produite lors du paiement.");
            }
        }
    }
}
