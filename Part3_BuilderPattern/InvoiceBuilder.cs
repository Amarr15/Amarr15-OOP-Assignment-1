using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuilderPattern
{
    public class InvoiceBuilder
    {
        private string? _customerName;
        private string? _customerEmail;

        private Address? _billingAddress;
        private Address? _shippingAddress;

        private Order? _order;

        public InvoiceBuilder SetCustomerName(string customerName)
        {
            _customerName = customerName;
            return this;
        }

        public InvoiceBuilder SetCustomerEmail(string customerEmail)
        {
            _customerEmail = customerEmail;
            return this;
        }

        public InvoiceBuilder SetBillingAddress(Address billingAddress)
        {
            _billingAddress = billingAddress;
            return this;
        }

        public InvoiceBuilder SetShippingAddress(Address shippingAddress)
        {
            _shippingAddress = shippingAddress;
            return this;
        }

        public InvoiceBuilder SetOrder(Order order)
        {
            _order = order;
            return this;
        }

        public Invoice Build()
        {
            if (string.IsNullOrWhiteSpace(_customerName))
                throw new InvalidOperationException(
                    "Customer name is required.");

            if (string.IsNullOrWhiteSpace(_customerEmail))
                throw new InvalidOperationException(
                    "Customer email is required.");

            if (_billingAddress == null)
                throw new InvalidOperationException(
                    "Billing address is required.");

            if (_shippingAddress == null)
                throw new InvalidOperationException(
                    "Shipping address is required.");

            if (_order == null)
                throw new InvalidOperationException(
                    "Order is required.");

            return new Invoice(
                _customerName,
                _customerEmail,
                _billingAddress,
                _shippingAddress,
                _order);
        }
    }
}
}
