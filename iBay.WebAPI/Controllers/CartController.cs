using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using iBay.Entities.Models;
using iBay.WebAPI.Interfaces;


namespace iBay.WebAPI.Controllers
{
    // Ce contrôleur est sécurisé, toutes les méthodes nécessitent que l'utilisateur soit authentifié
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;
        private readonly ILogger<CartController> _logger;


        // Injecter le service de panier qui gère la logique métier
        public CartController(ICartService cartService, ILogger<CartController> logger)
        {
            _cartService = cartService ?? throw new ArgumentNullException(nameof(cartService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>Récupère tous les articles du panier de l'utilisateur actuel.</summary>
        /// <remarks>
        /// Exemple de requête :
        ///
        /// GET /api/Cart
        ///
        /// </remarks>
        /// <returns>Une liste d'articles du panier de l'utilisateur.</returns>
        /// <response code="200">Retourne la liste des articles du panier.</response>
        /// <response code="500">Si une erreur survient lors de la récupération du panier.</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CartItem>>> GetCartItems()
        {
            try
            {
                var items = await _cartService.GetCartItemsAsync(User.Identity.Name);
                return Ok(items);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Une erreur s'est produite lors de la récupération de votre panier.");
                return StatusCode(500, "Une erreur s'est produite lors de la récupération de votre panier.");
            }
        }


        /// <summary>Ajoute un nouvel article au panier de l'utilisateur.</summary>
        /// <remarks>
        /// Exemple de requête :
        ///
        /// POST /api/Cart
        /// {
        ///   "cartItemId": 0,
        ///   "productId": 1,
        ///   "quantity": 2
        /// }
        ///
        /// </remarks>
        /// <param name="item">Les détails de l'article à ajouter au panier.</param>
        /// <returns>Code de statut HTTP indiquant le résultat de l'opération.</returns>
        /// <response code="200">Opération réussie, l'article a été ajouté au panier.</response>
        /// <response code="500">Si une erreur survient lors de l'ajout de l'article au panier.</response>
        [HttpPost]
        public async Task<ActionResult> AddToCart([FromBody] CartItem item)
        {
            try
            {
                await _cartService.AddToCartAsync(item, User.Identity.Name);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Une erreur s'est produite lors de l'ajout de l'article au panier.");
                return StatusCode(500, "Une erreur s'est produite lors de l'ajout de l'article au panier.");
            }
        }

        /// <summary>Met à jour un article existant dans le panier de l'utilisateur.</summary>
        /// <remarks>
        /// Exemple de requête :
        ///
        /// PUT /api/Cart/{id}
        /// {
        ///   "cartItemId": 1,
        ///   "productId": 1,
        ///   "quantity": 3
        /// }
        ///
        /// </remarks>
        /// <param name="id">L'identifiant de l'article dans le panier.</param>
        /// <param name="item">Les détails mis à jour de l'article.</param>
        /// <returns>Code de statut HTTP indiquant le résultat de la mise à jour.</returns>
        /// <response code="200">Opération réussie, l'article dans le panier a été mis à jour.</response>
        /// <response code="400">Si les données de l'article sont invalides ou l'identifiant ne correspond pas.</response>
        /// <response code="404">Si l'article dans le panier n'est pas trouvé.</response>
        /// <response code="500">Si une erreur survient lors de la mise à jour de l'article dans le panier.</response>
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCartItem(int id, [FromBody] CartItem item)
        {
            try
            {
                if (id != item.CartItemId)
                {
                    return BadRequest("L'identifiant de l'article ne correspond pas.");
                }

                var result = await _cartService.UpdateCartItemAsync(id, item, User.Identity.Name);
                if (result == null)
                {
                    return NotFound("L'article du panier n'a pas été trouvé.");
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "Une erreur s'est produite lors de la mise à jour de l'article dans votre panier.");
                return StatusCode(500,
                    "Une erreur s'est produite lors de la mise à jour de l'article dans votre panier.");
            }
        }

        /// <summary>Supprime un article du panier de l'utilisateur.</summary>
        /// <remarks>
        /// Exemple de requête :
        ///
        /// DELETE /api/Cart/{id}
        ///
        /// </remarks>
        /// <param name="id">L'identifiant de l'article dans le panier.</param>
        /// <returns>Code de statut HTTP indiquant le résultat de la suppression.</returns>
        /// <response code="200">Opération réussie, l'article a été supprimé du panier.</response>
        /// <response code="404">Si l'article dans le panier n'est pas trouvé.</response>
        /// <response code="500">Si une erreur survient lors de la suppression de l'article du panier.</response>
        [HttpDelete("{id}")]
        public async Task<ActionResult> RemoveFromCart(int id)
        {
            try
            {
                await _cartService.RemoveFromCartAsync(id, User.Identity.Name);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Une erreur s'est produite lors de la suppression de l'article du panier.");
                return StatusCode(500, "Une erreur s'est produite lors de la suppression de l'article du panier.");
            }
        }


        /// <summary>Simule le processus de paiement/checkout du panier de l'utilisateur.</summary>
        /// <remarks>
        /// Exemple de requête :
        ///
        /// POST /api/Cart/Checkout
        ///
        /// </remarks>
        /// <returns>Code de statut HTTP indiquant le résultat du paiement.</returns>
        /// <response code="200">Opération réussie, le paiement a été effectué avec succès.</response>
        /// <response code="500">Si une erreur survient lors du paiement.</response>
        [HttpPost("Checkout")]
        public async Task<ActionResult> CheckoutCart()
        {
            try
            {
                var cartItems = await _cartService.GetCartItemsAsync(User.Identity.Name);

                // Marquer chaque article comme payé
                foreach (var item in cartItems)
                {
                    item.IsPaid = true;
                }

                // Enregistrer les modifications dans la base de données
                foreach (var item in cartItems)
                {
                    await _cartService.UpdateCartItemAsync(item.CartItemId, item, User.Identity.Name);
                }

                return Ok("Le paiement a été effectué avec succès.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Une erreur s'est produite lors du paiement.");
                return StatusCode(500, "Une erreur s'est produite lors du paiement.");
            }
        }
    }
}
