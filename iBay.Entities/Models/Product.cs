using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace iBay.Entities.Models
{
    //Product Model
    public class Product
    {
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Le nom du produit est requis")]
        public string Name { get; set; }

        [Required(ErrorMessage = "L'image du produit est requise")]
        public string Image { get; set; }

        [Required(ErrorMessage = "Le prix du produit est requis")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Le prix doit être supérieur à zéro")]
        public decimal Price { get; set; }

        public bool Available { get; set; }

        [Required(ErrorMessage = "La date d'ajout du produit est requise")]
        public DateTime AddedTime { get; set; }

        [Required(ErrorMessage = "L'identifiant du vendeur est requis")]
        public string SellerId { get; set; }

        public ApplicationUser? Seller { get; set; }
    }

}
