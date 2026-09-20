using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuilderPattern
{
    public class Order
    {

        public string OrderNumber { get; }
        public DateTime OrderDate { get; }
        public decimal Subtotal { get; }
        public decimal Tax { get; }
        public decimal Discount { get; }
        public decimal Total { get; }
        public string PaymentMethod { get; }
        public string Currency { get; }
        public bool IsPaid { get; }
        public string Notes { get; }
        public Order(string orderNumber, DateTime orderDate, decimal subtotal, decimal tax, decimal discount, decimal total, string paymentMethod, string currency, bool isPaid, string notes)
        {
            OrderNumber = orderNumber;
            OrderDate = orderDate;
            Subtotal = subtotal;
            Tax = tax;
            Discount = discount;
            Total = total;
            PaymentMethod = paymentMethod;
            Currency = currency;
            IsPaid = isPaid;
            Notes = notes;
        }
    }
}
