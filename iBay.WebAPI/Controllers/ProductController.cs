using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using iBay.WebAPI.Services;
using iBay.Entities.Models;
//---- à créer ----//
using System;
//-----------------------//

namespace iBay.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ProductService _productService;

        public ProductController(ProductService productService)
        {
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
        }

        // GET: api/Product
        // Récupère la liste de tous les produits.
        [HttpGet]
        public IActionResult GetAllProducts()
        {
            try
            {
                var products = _productService.GetAllProducts();
                return Ok(products);
            }
            catch (Exception ex)
            {
                // Enregistrez les détails de l'exception ici.
                return StatusCode(500, "Une erreur est survenue lors de la récupération des produits.");
            }
        }

        // GET: api/Product/{id}
        // Récupère un produit spécifique par son identifiant.
        [HttpGet("{id}")]
        public IActionResult GetProductById(int id)
        {
            try
            {
                var product = _productService.GetProductById(id);
                if (product == null)
                {
                    return NotFound();
                }

                return Ok(product);
            }
            catch (Exception ex)
            {
                // Enregistrez les détails de l'exception ici.
                return StatusCode(500, "Une erreur est survenue lors de la récupération du produit.");
            }
        }

        // POST: api/Product
        // Crée un nouveau produit.
        [Authorize(Roles = "Seller")] // Supposons que seul un utilisateur avec le rôle "Seller" peut créer des produits.
        [HttpPost]
        public IActionResult CreateProduct([FromBody] Product product)
        {
            try
            {
                var createdProduct = _productService.CreateProduct(product);
                return CreatedAtAction(nameof(GetProductById), new { id = createdProduct.Id }, createdProduct);
            }
            catch (Exception ex)
            {
                // Enregistrez les détails de l'exception ici.
                return StatusCode(500, "Une erreur est survenue lors de la création du produit.");
            }
        }

        // PUT: api/Product/{id}
        // Met à jour un produit existant.
        [Authorize(Roles = "Seller")] // Supposons que seul un utilisateur avec le rôle "Seller" peut mettre à jour des produits.
        [HttpPut("{id}")]
        public IActionResult UpdateProduct(int id, [FromBody] Product product)
        {
            try
            {
                if (id != product.Id)
                {
                    return BadRequest("L'identifiant du produit ne correspond pas.");
                }

                var updatedProduct = _productService.UpdateProduct(product);
                if (updatedProduct == null)
                {
                    return NotFound();
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                // Enregistrez les détails de l'exception ici.
                return StatusCode(500, "Une erreur est survenue lors de la mise à jour du produit.");
            }
        }

        // DELETE: api/Product/{id}
        // Supprime un produit.
        [Authorize(Roles = "Seller")] // Supposons que seul un utilisateur avec le rôle "Seller" peut supprimer des produits.
        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            try
            {
                var success = _productService.DeleteProduct(id);
                if (!success)
                {
                    return NotFound();
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                // Enregistrez les détails de l'exception ici.
                return StatusCode(500, "Une erreur est survenue lors de la suppression du produit.");
            }
        }
    }
}
