using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuilderPattern
{
    public class OrderBuilder
    {
        private string? _orderNumber;
        private DateTime? _orderDate;
        private decimal? _subtotal;
        private decimal? _tax;
        private decimal? _discount;
        private decimal? _total;
        private string? _paymentMethod;
        private string? _currency;
        private bool? _isPaid;
        private string? _notes;

        public OrderBuilder SetOrderNumber(string orderNumber)
        {
            _orderNumber = orderNumber;
            return this;
        }

        public OrderBuilder SetOrderDate(DateTime orderDate)
        {
            _orderDate = orderDate;
            return this;
        }

        public OrderBuilder SetSubtotal(decimal subtotal)
        {
            _subtotal = subtotal;
            return this;
        }

        public OrderBuilder SetTax(decimal tax)
        {
            _tax = tax;
            return this;
        }

        public OrderBuilder SetDiscount(decimal discount)
        {
            _discount = discount;
            return this;
        }

        public OrderBuilder SetTotal(decimal total)
        {
            _total = total;
            return this;
        }

        public OrderBuilder SetPaymentMethod(string paymentMethod)
        {
            _paymentMethod = paymentMethod;
            return this;
        }

        public OrderBuilder SetCurrency(string currency)
        {
            _currency = currency;
            return this;
        }

        public OrderBuilder SetIsPaid(bool isPaid)
        {
            _isPaid = isPaid;
            return this;
        }

        public OrderBuilder SetNotes(string notes)
        {
            _notes = notes;
            return this;
        }

        public Order Build()
        {
            if (string.IsNullOrWhiteSpace(_orderNumber))
                throw new InvalidOperationException(
                    "Order number is required.");

            if (_orderDate == null)
                throw new InvalidOperationException(
                    "Order date is required.");

            if (_subtotal == null)
                throw new InvalidOperationException(
                    "Subtotal is required.");

            if (_total == null)
                throw new InvalidOperationException(
                    "Total is required.");

            if (string.IsNullOrWhiteSpace(_paymentMethod))
                throw new InvalidOperationException(
                    "Payment method is required.");

            if (string.IsNullOrWhiteSpace(_currency))
                throw new InvalidOperationException(
                    "Currency is required.");

            if (_isPaid == null)
                throw new InvalidOperationException(
                    "Payment status is required.");

            return new Order(
                _orderNumber,
                _orderDate.Value,
                _subtotal.Value,
                _tax ?? 0,
                _discount ?? 0,
                _total.Value,
                _paymentMethod,
                _currency,
                _isPaid.Value,
                _notes ?? "");
        }
    }
}
