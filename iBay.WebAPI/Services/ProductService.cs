using iBay.Entities.Repositories;
using iBay.Entities.Models;
using iBay.WebAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace iBay.WebAPI.Services
{
 public class ProductService : IProductService
    {
        private readonly IBasicRepository<Product> _productRepository;

        public ProductService(IBasicRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            try
            {
                return await _productRepository.GetAllAsync();
            }
            catch (Exception ex)
            {
                // Gérez les exceptions selon les besoins
                throw new ApplicationException("Une erreur s'est produite lors de la récupération des produits.", ex);
            }
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            try
            {
                return await _productRepository.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                // Gérez les exceptions selon les besoins
                throw new ApplicationException("Une erreur s'est produite lors de la récupération du produit.", ex);
            }
        }

        public async Task<Product> CreateProductAsync(Product product)
        {
            try
            {
                await _productRepository.AddAsync(product);
                return product;
            }
            catch (Exception ex)
            {
                // Gérez les exceptions selon les besoins
                throw new ApplicationException("Une erreur s'est produite lors de la création du produit.", ex);
            }
        }

        public async Task<Product> UpdateProductAsync(Product product)
        {
            try
            {
                await _productRepository.UpdateAsync(product);
                return product;
            }
            catch (Exception ex)
            {
                // Gérez les exceptions selon les besoins
                throw new ApplicationException("Une erreur s'est produite lors de la mise à jour du produit.", ex);
            }
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            try
            {
                var product = await _productRepository.GetByIdAsync(id);
                if (product != null)
                {
                    await _productRepository.DeleteAsync(id);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                // Gérez les exceptions selon les besoins
                throw new ApplicationException("Une erreur s'est produite lors de la suppression du produit.", ex);
            }
        }
    }
}
