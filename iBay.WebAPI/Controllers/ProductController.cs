using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging; // Importation manquante pour ILogger
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using iBay.Entities.Models;
using iBay.WebAPI.Interfaces;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;


namespace iBay.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ILogger<ProductController> _logger;

        public ProductController(IProductService productService, ILogger<ProductController> logger)
        {
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }


        /// <summary>Récupère la liste de tous les produits trié.</summary>
        /// <remarks>
        /// Exemple de requête :
        ///
        /// GET /api/Product?sortBy=date&limit=10
        ///
        /// </remarks>
        /// <param name="sortBy">Le critère de tri des produits (date, name, price).</param>
        /// <param name="limit">Le nombre maximum de produits à retourner.</param>
        /// <returns>Une liste triée et paginée de tous les produits.</returns>
        /// <response code="200">Retourne la liste triée et paginée de tous les produits.</response>
        /// <response code="400">Si les paramètres de requête sont invalides.</response>
        /// <response code="500">Si une erreur survient lors de la récupération des produits.</response>
        [HttpGet]
        public async Task<IActionResult> GetAllProducts(
            [FromQuery] string sortBy = "date",
            [FromQuery] int limit = 10)
        {
            try
            {
                if (limit <= 0)
                {
                    return BadRequest("La limite doit être supérieure à zéro.");
                }
                var allProducts = await _productService.GetAllProductsAsync();

                IEnumerable<Product> products = null;
                switch (sortBy.ToLower())
                {
                    case "date":
                        products = allProducts.OrderBy(p => p.AddedTime);
                        break;
                    case "name":
                        products = allProducts.OrderBy(p => p.Name);
                        break;
                    case "price":
                        products = allProducts.OrderBy(p => p.Price);
                        break;
                    default:
                        return BadRequest("Le critère de tri spécifié n'est pas valide.");
                }

                products = products.Take(limit);

                return Ok(products);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Une erreur s'est produite lors de la récupération des produits.");
                return StatusCode(500, "Une erreur s'est produite lors de la récupération des produits.");
            }
        }

        /// <summary>Récupère un produit spécifique par son identifiant.</summary>
        /// <remarks>
        /// Exemple de requête :
        ///
        /// GET /api/Product/{id}
        ///
        /// </remarks>
        /// <param name="id">L'identifiant du produit.</param>
        /// <returns>Le produit correspondant à l'identifiant spécifié.</returns>
        /// <response code="200">Retourne le produit correspondant à l'identifiant.</response>
        /// <response code="404">Si le produit n'est pas trouvé.</response>
        /// <response code="500">Si une erreur survient lors de la récupération du produit.</response>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            try
            {
                var product = await _productService.GetProductByIdAsync(id);
                if (product == null)
                {
                    return NotFound();
                }

                return Ok(product);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Une erreur s'est produite lors de la récupération du produit.");
                return StatusCode(500, "Une erreur s'est produite lors de la récupération du produit.");
            }
        }


        /// <summary>Crée un nouveau produit.</summary>
        /// <remarks>
        /// Exemple de requête :
        ///
        /// POST /api/Product
        /// {
        ///   "productId": 0,
        ///   "productName": "Nom du produit",
        ///   "sellerId": 1,
        ///   "price": 20.5
        /// }
        ///
        /// </remarks>
        /// <param name="product">Les détails du nouveau produit à créer.</param>
        /// <returns>Code de statut HTTP indiquant le résultat de la création.</returns>
        /// <response code="201">Opération réussie, le produit a été créé avec succès.</response>
        /// <response code="401">Si l'utilisateur n'est pas autorisé à créer un produit (non vendeur).</response>
        /// <response code="500">Si une erreur survient lors de la création du produit.</response>
        [HttpPost]
        [Authorize(Roles = "Seller")]
        public async Task<IActionResult> CreateProduct([FromBody] Product product)
        {
            try
            {
                var createdProduct = await _productService.CreateProductAsync(product);
                return CreatedAtAction(nameof(GetProductById), new { id = createdProduct.ProductId }, createdProduct);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Une erreur s'est produite lors de la création du produit.");
                return StatusCode(500, "Une erreur s'est produite lors de la création du produit.");
            }
        }


        /// <summary>Met à jour un produit existant.</summary>
        /// <remarks>
        /// Exemple de requête :
        ///
        /// PUT /api/Product/{id}
        /// {
        ///   "productId": 1,
        ///   "productName": "Nouveau nom",
        ///   "sellerId": 1,
        ///   "price": 25.0
        /// }
        ///
        /// </remarks>
        /// <param name="id">L'identifiant du produit à mettre à jour.</param>
        /// <param name="product">Les détails mis à jour du produit.</param>
        /// <returns>Code de statut HTTP indiquant le résultat de la mise à jour.</returns>
        /// <response code="204">Opération réussie, le produit a été mis à jour avec succès.</response>
        /// <response code="400">Si les données du produit sont invalides ou l'identifiant ne correspond pas.</response>
        /// <response code="401">Si l'utilisateur n'est pas autorisé à mettre à jour le produit (non vendeur).</response>
        /// <response code="404">Si le produit à mettre à jour n'est pas trouvé.</response>
        /// <response code="500">Si une erreur survient lors de la mise à jour du produit.</response>
        [HttpPut("{id}")]
        [Authorize(Roles = "Seller")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] Product product)
        {
            try
            {

                var updatedProduct = await _productService.UpdateProductAsync(product);
                if (updatedProduct == null)
                {
                    return NotFound();
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Une erreur s'est produite lors de la mise à jour du produit.");
                return StatusCode(500, "Une erreur s'est produite lors de la mise à jour du produit.");
            }
        }

        /// <summary>Supprime un produit.</summary>
        /// <remarks>
        /// Exemple de requête :
        ///
        /// DELETE /api/Product/{id}
        ///
        /// </remarks>
        /// <param name="id">L'identifiant du produit à supprimer.</param>
        /// <returns>Code de statut HTTP indiquant le résultat de la suppression.</returns>
        /// <response code="204">Opération réussie, le produit a été supprimé avec succès.</response>
        /// <response code="401">Si l'utilisateur n'est pas autorisé à supprimer le produit (non vendeur).</response>
        /// <response code="404">Si le produit à supprimer n'est pas trouvé.</response>
        /// <response code="500">Si une erreur survient lors de la suppression du produit.</response>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Seller")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {

                var success = await _productService.DeleteProductAsync(id);
                if (!success)
                {
                    return NotFound();
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Une erreur s'est produite lors de la suppression du produit.");
                return StatusCode(500, "Une erreur s'est produite lors de la suppression du produit.");
            }
        }
    }
}
