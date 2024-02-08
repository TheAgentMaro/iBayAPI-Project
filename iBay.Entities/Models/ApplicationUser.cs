using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iBay.Entities.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required(ErrorMessage = "Le rôle de l'utilisateur est requis")]
        public string Role { get; set; }
        // Collection de produits vendus par l'utilisateur
        public ICollection<Product>? Products { get; set; }

        // Collection d'articles dans le panier de l'utilisateur
        public ICollection<CartItem>? CartItems { get; set; }
    }
}
