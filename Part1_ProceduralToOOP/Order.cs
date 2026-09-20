using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Assignment
{
    public class Order
    {

        public int Id { get; }
        public Customer Customer { get; }
        public DateTime Date { get; }
        public bool IsPaid { get; private set; }

        private readonly List<OrderLine> _orderLines = new();
        public IReadOnlyList<OrderLine> OrderLines => _orderLines;

        public Order(int id, Customer customer, DateTime date)
        {
            if (id <= 0) throw new ArgumentOutOfRangeException(nameof(id));
            Customer = customer ?? throw new ArgumentNullException(nameof(customer));
            
            Id = id;
            Date = date;
            IsPaid = false;
        }

        public void AddLine(Product product, int quantity)
        {
            if (product == null) throw new ArgumentNullException(nameof(product));

            if (IsPaid) throw new InvalidOperationException("Cannot add lines to a paid order");

            if (quantity <= 0) throw new ArgumentOutOfRangeException(nameof(quantity));

            product.DecreaseStock(quantity);

            _orderLines.Add(new OrderLine(product, quantity));
        }
        public void MarkAsPaid()
        {
            if (_orderLines.Count == 0) throw new InvalidOperationException("Cannot pay an empty order");

            IsPaid = true;
        }
        public decimal CalculateTotal()
        {
            decimal total = 0;

            foreach (var line in _orderLines)
            {
                total += line.GetTotal();
            }

            if (Customer.IsVip)
            {
                total *= 0.90m;
            }

            return total;
        }

    }
}
