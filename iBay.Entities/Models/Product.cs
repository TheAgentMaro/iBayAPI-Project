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
        public string Name { get; set; }
        public string Image { get; set; }
        public decimal Price { get; set; }
        public bool Available { get; set; }
        public DateTime AddedTime { get; set; }

        // Identifiant de l'utilisateur vendeur
        public int SellerId { get; set; }

        // Utilisateur vendeur
        public User? Seller { get; set; }
    }
}
