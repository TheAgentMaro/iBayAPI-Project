using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iBay.Entities.Models
{
    //CartItem Model
    public class CartItem
    {
        public int CartItemId { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public User User { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
