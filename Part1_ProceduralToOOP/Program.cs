namespace OOP_Assignment
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var customers = new List<Customer>();
            var products = new List<Product>();
            var orders = new List<Order>();

            SeedSampleData();

            RunDemoScenario();

            PrintCustomers();
            PrintProducts();
            PrintAllOrders();

            Console.WriteLine(
                $"\nPaid sales total after demo: {TotalSalesPaidOnly():0.00}");

            RunInteractiveMenu();


            void AddCustomer(
                int id,
                string name,
                string email,
                string city,
                bool isVip)
            {
                if (customers.Any(c => c.Id == id))
                {
                    Console.WriteLine(
                        $"ERROR: customer id {id} already exists.");
                    return;
                }

                customers.Add(
                    new Customer(id, name, email, city, isVip));
            }


            void PrintCustomers()
            {
                Console.WriteLine(
                    $"\n=== CUSTOMERS ({customers.Count}) ===");

                foreach (var customer in customers)
                {
                    Console.WriteLine(
                        $"#{customer.Id}  " +
                        $"{customer.Name}  " +
                        $"<{customer.Email}>  " +
                        $"{customer.City}  " +
                        $"vip={(customer.IsVip ? "yes" : "no")}");
                }
            }


            void AddProduct(
                int id,
                string name,
                decimal price,
                int stock)
            {
                if (products.Any(p => p.ProductId == id))
                {
                    Console.WriteLine(
                        $"ERROR: product id {id} already exists.");
                    return;
                }

                products.Add(
                    new Product(id, name, price, stock));
            }


            void PrintProducts()
            {
                Console.WriteLine(
                    $"\n=== PRODUCTS ({products.Count}) ===");

                foreach (var product in products)
                {
                    Console.WriteLine(
                        $"#{product.ProductId}  " +
                        $"{product.ProductName}  " +
                        $"price={product.ProductPrice:0.00}  " +
                        $"stock={product.ProductStock}");
                }
            }


            Order? FindOrderById(int id)
            {
                return orders.FirstOrDefault(o => o.Id == id);
            }


            Product? FindProductById(int id)
            {
                return products.FirstOrDefault(
                    p => p.ProductId == id);
            }


            Customer? FindCustomerById(int id)
            {
                return customers.FirstOrDefault(
                    c => c.Id == id);
            }


            Order? CreateOrder(
                int orderId,
                int customerId,
                DateTime date)
            {
                if (FindOrderById(orderId) != null)
                {
                    Console.WriteLine(
                        $"ERROR: order id {orderId} already exists.");

                    return null;
                }

                var customer = FindCustomerById(customerId);

                if (customer == null)
                {
                    Console.WriteLine(
                        $"ERROR: customer id {customerId} not found.");

                    return null;
                }

                var order = new Order(
                    orderId,
                    customer,
                    date);

                orders.Add(order);

                return order;
            }


            void AddLineToOrder(
                int orderId,
                int productId,
                int quantity)
            {
                var order = FindOrderById(orderId);

                if (order == null)
                {
                    Console.WriteLine(
                        $"ERROR: order id {orderId} not found.");

                    return;
                }

                var product = FindProductById(productId);

                if (product == null)
                {
                    Console.WriteLine(
                        $"ERROR: product id {productId} not found.");

                    return;
                }

                try
                {
                    order.AddLine(product, quantity);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"ERROR: {ex.Message}");
                }
            }


            void MarkOrderPaid(int orderId)
            {
                var order = FindOrderById(orderId);

                if (order == null)
                {
                    Console.WriteLine(
                        $"ERROR: order id {orderId} not found.");

                    return;
                }

                try
                {
                    order.MarkAsPaid();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"ERROR: {ex.Message}");
                }
            }


            void PrintOrder(int orderId)
            {
                var order = FindOrderById(orderId);

                if (order == null)
                {
                    Console.WriteLine(
                        $"ERROR: order id {orderId} not found.");

                    return;
                }

                Console.WriteLine(
                    $"\n=== ORDER #{order.Id} ===");

                Console.WriteLine(
                    $"Date: {order.Date:yyyy-MM-dd}");

                Console.WriteLine(
                    $"Customer: {order.Customer.Name} " +
                    $"(#{order.Customer.Id})");

                Console.WriteLine(
                    $"Paid: {(order.IsPaid ? "yes" : "no")}");

                Console.WriteLine("Lines:");

                foreach (var line in order.OrderLines)
                {
                    var lineTotal = line.GetTotal();

                    Console.WriteLine(
                        $"  - {line.Product.ProductName}  " +
                        $"x{line.Quantity}  " +
                        $"@{line.Product.ProductPrice:0.00}  " +
                        $"= {lineTotal:0.00}");
                }

                Console.WriteLine(
                    $"TOTAL: {order.CalculateTotal():0.00}");
            }


            void PrintAllOrders()
            {
                Console.WriteLine(
                    $"\n=== ALL ORDERS ({orders.Count}) ===");

                foreach (var order in orders)
                {
                    PrintOrder(order.Id);
                }
            }


            decimal TotalSalesPaidOnly()
            {
                decimal sum = 0;

                foreach (var order in orders)
                {
                    if (order.IsPaid)
                    {
                        sum += order.CalculateTotal();
                    }
                }

                return sum;
            }


            void SeedSampleData()
            {
                AddCustomer(
                    1,
                    "Mona Ali",
                    "mona@example.com",
                    "Cairo",
                    true);

                AddCustomer(
                    2,
                    "Omar Hassan",
                    "omar@example.com",
                    "Alexandria",
                    false);

                AddCustomer(
                    3,
                    "Sara Nabil",
                    "sara@example.com",
                    "Giza",
                    false);


                AddProduct(
                    101,
                    "USB Cable",
                    50m,
                    100);

                AddProduct(
                    102,
                    "Wireless Mouse",
                    250m,
                    40);

                AddProduct(
                    103,
                    "Mechanical Keyboard",
                    1200m,
                    15);

                AddProduct(
                    104,
                    "Laptop Stand",
                    400m,
                    25);
            }


            void RunDemoScenario()
            {
                var order1 = CreateOrder(
                    1001,
                    1,
                    new DateTime(2026, 9, 15));

                if (order1 != null)
                {
                    AddLineToOrder(1001, 101, 2);
                    AddLineToOrder(1001, 102, 1);
                    MarkOrderPaid(1001);
                }


                CreateOrder(
                    1002,
                    2,
                    new DateTime(2026, 9, 15));

                AddLineToOrder(1002, 103, 1);
                AddLineToOrder(1002, 104, 1);


                CreateOrder(
                    1003,
                    3,
                    new DateTime(2026, 9, 16));

                AddLineToOrder(1003, 101, 5);
                MarkOrderPaid(1003);
            }


            void RunInteractiveMenu()
            {
                int choice = -1;

                while (choice != 0)
                {
                    Console.WriteLine("\n---------- MENU ----------");
                    Console.WriteLine("1) Print customers");
                    Console.WriteLine("2) Print products");
                    Console.WriteLine("3) Print all orders");
                    Console.WriteLine("4) Print one order by id");
                    Console.WriteLine("5) Create order");
                    Console.WriteLine("6) Add line to order");
                    Console.WriteLine("7) Mark order paid");
                    Console.WriteLine("8) Show paid sales total");
                    Console.WriteLine("0) Exit");
                    Console.Write("Choice: ");

                    if (!int.TryParse(Console.ReadLine(), out choice))
                    {
                        Console.WriteLine("Invalid choice.");
                        continue;
                    }

                    switch (choice)
                    {
                        case 1:
                            PrintCustomers();
                            break;

                        case 2:
                            PrintProducts();
                            break;

                        case 3:
                            PrintAllOrders();
                            break;

                        case 4:
                            Console.Write("Order id: ");

                            if (int.TryParse(
                                Console.ReadLine(),
                                out int orderId))
                            {
                                PrintOrder(orderId);
                            }

                            break;

                        case 5:
                            Console.Write("Order id: ");
                            int.TryParse(
                                Console.ReadLine(),
                                out int newOrderId);

                            Console.Write("Customer id: ");
                            int.TryParse(
                                Console.ReadLine(),
                                out int customerId);

                            Console.Write("Date (YYYY-MM-DD): ");
                            var dateInput = Console.ReadLine();

                            if (DateTime.TryParse(
                                dateInput,
                                out DateTime date))
                            {
                                CreateOrder(
                                    newOrderId,
                                    customerId,
                                    date);
                            }
                            else
                            {
                                Console.WriteLine("Invalid date.");
                            }

                            break;

                        case 6:
                            Console.Write("Order id: ");
                            int.TryParse(
                                Console.ReadLine(),
                                out int lineOrderId);

                            Console.Write("Product id: ");
                            int.TryParse(
                                Console.ReadLine(),
                                out int productId);

                            Console.Write("Quantity: ");
                            int.TryParse(
                                Console.ReadLine(),
                                out int quantity);

                            AddLineToOrder(
                                lineOrderId,
                                productId,
                                quantity);

                            break;

                        case 7:
                            Console.Write("Order id: ");
                            int.TryParse(
                                Console.ReadLine(),
                                out int paidOrderId);

                            MarkOrderPaid(paidOrderId);
                            break;

                        case 8:
                            Console.WriteLine(
                                $"Paid sales total: " +
                                $"{TotalSalesPaidOnly():0.00}");

                            break;

                        case 0:
                            Console.WriteLine("Bye.");
                            break;

                        default:
                            Console.WriteLine("Unknown choice.");
                            break;
                    }
                }
            }

            Console.ReadLine();
        }
    }
}
