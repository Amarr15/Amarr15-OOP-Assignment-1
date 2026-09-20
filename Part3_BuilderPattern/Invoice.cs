using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuilderPattern
{
    public class Invoice
    {

        public string CustomerName { get; }
        public string CustomerEmail { get; }

        public Address BillingAddress { get; }
        public Address ShippingAddress { get; }

        public Order Order { get; }
        public Invoice(string customerName, string customerEmail, Address billingAddress, Address shippingAddress, Order order)
        {
            CustomerName = customerName;
            CustomerEmail = customerEmail;
            BillingAddress = billingAddress;
            ShippingAddress = shippingAddress;
            Order = order;
        }

    }
}
