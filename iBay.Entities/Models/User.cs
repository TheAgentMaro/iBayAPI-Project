using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iBay.Entities.Models
{
    //User Model
    public class User
    {
        [Key]
        public Guid UserId { get; set; }
        public string Email { get; set; }
        public string Pseudo { get; set; }
        public string Password { get; set; }

        // Rôle de l'utilisateur (par exemple, vendeur, acheteur, etc.)
        public string Role { get; set; }

        // Collection de produits vendus par l'utilisateur
        public ICollection<Product> Products { get; set; }

        // Collection d'articles dans le panier de l'utilisateur
        public ICollection<CartItem> CartItems { get; set; }
    }
}
