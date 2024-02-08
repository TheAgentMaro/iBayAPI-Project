using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iBay.Entities.Models
{
    //CartItem Model
    public class CartItem
    {
        [Key]
        public int CartItemId { get; set; }

        [Required(ErrorMessage = "L'identifiant de l'utilisateur est requis")]
        public string UserId { get; set; }

        [Required(ErrorMessage = "Le nom de l'utilisateur est requis")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "L'utilisateur est requis")]
        public ApplicationUser User { get; set; }

        [Required(ErrorMessage = "L'identifiant du produit est requis")]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Le produit est requis")]
        public Product Product { get; set; }

        [Required(ErrorMessage = "La quantité est requise")]
        [Range(1, int.MaxValue, ErrorMessage = "La quantité doit être supérieure à zéro")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Le prix est requis")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Le prix doit être supérieur à zéro")]
        public decimal Price { get; set; }

        public bool IsPaid { get; set; }
    }
}
}
