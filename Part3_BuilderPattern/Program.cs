namespace BuilderPattern
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var billingAddress = new AddressBuilder()
    .SetStreet("Main Street")
    .SetCity("Mansoura")
    .SetState("Dakahlia")
    .SetCountry("Egypt")
    .SetPostalCode("35511")
    .Build();


            var shippingAddress = new AddressBuilder()
                .SetStreet("University Street")
                .SetCity("Mansoura")
                .SetState("Dakahlia")
                .SetCountry("Egypt")
                .SetPostalCode("35512")
                .Build();


            var order = new OrderBuilder()
                .SetOrderNumber("ORD-1001")
                .SetOrderDate(DateTime.Now)
                .SetSubtotal(2000m)
                .SetTax(300m)
                .SetDiscount(100m)
                .SetTotal(2200m)
                .SetPaymentMethod("Credit Card")
                .SetCurrency("EGP")
                .SetIsPaid(true)
                .SetNotes("First order")
                .Build();


            var invoice = new InvoiceBuilder()
                .SetCustomerName("Ahmed Yasser")
                .SetCustomerEmail("ahmed@example.com")
                .SetBillingAddress(billingAddress)
                .SetShippingAddress(shippingAddress)
                .SetOrder(order)
                .Build();


            Console.WriteLine("===== INVOICE =====");

            Console.WriteLine(
                $"Customer: {invoice.CustomerName}");

            Console.WriteLine(
                $"Email: {invoice.CustomerEmail}");

            Console.WriteLine(
                $"Billing Address: " +
                $"{invoice.BillingAddress.Street}, " +
                $"{invoice.BillingAddress.City}, " +
                $"{invoice.BillingAddress.Country}");

            Console.WriteLine(
                $"Shipping Address: " +
                $"{invoice.ShippingAddress.Street}, " +
                $"{invoice.ShippingAddress.City}, " +
                $"{invoice.ShippingAddress.Country}");

            Console.WriteLine(
                $"Order Number: {invoice.Order.OrderNumber}");

            Console.WriteLine(
                $"Order Date: {invoice.Order.OrderDate:yyyy-MM-dd}");

            Console.WriteLine(
                $"Subtotal: {invoice.Order.Subtotal:0.00}");

            Console.WriteLine(
                $"Tax: {invoice.Order.Tax:0.00}");

            Console.WriteLine(
                $"Discount: {invoice.Order.Discount:0.00}");

            Console.WriteLine(
                $"Total: {invoice.Order.Total:0.00}");

            Console.WriteLine(
                $"Payment Method: {invoice.Order.PaymentMethod}");

            Console.WriteLine(
                $"Currency: {invoice.Order.Currency}");

            Console.WriteLine(
                $"Paid: {invoice.Order.IsPaid}");

            Console.WriteLine(
                $"Notes: {invoice.Order.Notes}");
        }
    }
}
