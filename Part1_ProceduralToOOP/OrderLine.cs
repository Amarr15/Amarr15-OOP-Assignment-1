using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Assignment
{
    public class OrderLine
    {

        public Product Product { get; }
        public int Quantity { get; }
        public OrderLine(Product product, int quantity)
        {
            if (product == null) throw new ArgumentNullException(nameof(product));
            if (quantity <= 0) throw new ArgumentOutOfRangeException(nameof(quantity));

            Product = product;
            Quantity = quantity;
        }
        public decimal GetTotal()
        {
            return Product.ProductPrice * Quantity;
        }

    }
}
