using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace OOP_Assignment
{
    public class Product
    {

        public int ProductId { get; }
        public string ProductName { get; } = null!;
        public decimal ProductPrice { get; }
        public int ProductStock { get; private set; }
        public Product(int productId, string productName, decimal productPrice, int productStock)
        {
            if (productId <= 0) throw new ArgumentOutOfRangeException(nameof(productId));
            if (string.IsNullOrWhiteSpace(productName)) throw new ArgumentException(nameof(productName));
            if (productPrice <= 0) throw new ArgumentOutOfRangeException(nameof(productPrice));
            if (productStock < 0) throw new ArgumentNullException(nameof(productStock));
            this.ProductId = productId;
            this.ProductName = productName;
            this.ProductPrice = productPrice;
            this.ProductStock = productStock;
        }

        public void DecreaseStock(int quantity)
        {
            if (quantity <= 0)
                throw new ArgumentOutOfRangeException(nameof(quantity));

            if (quantity > ProductStock)
                throw new InvalidOperationException("Not enough stock ");

            ProductStock -= quantity;
        }
    }
}
