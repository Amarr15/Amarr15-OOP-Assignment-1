# Critique

## 1. Global Variables

The program uses many global variables and arrays for customers, products, and orders.

I think this is a problem because any function can access and modify these variables directly. There is no proper encapsulation or clear control over who can change the data. This can make the program harder to maintain and can lead to invalid data.

## 2. Parallel Arrays

The data for one customer is stored in different arrays, such as `customerIds`, `customerNames`, `customerEmails`, `customerCities`, and `customerIsVip`.

I think this makes the code harder to understand because all the information that belongs to one customer is not grouped together. A `Customer` object would be a better way to keep related data together.

## 4. Fixed Size Arrays

The program uses fixed limits such as `MAX_CUSTOMERS = 50`, `MAX_PRODUCTS = 50`, and `MAX_ORDERS = 100`.

This makes the system less flexible because it cannot easily handle more data than these limits. The program also needs extra checks to handle the case when an array becomes full.

## 5. Using Indexes Instead of Objects

Orders and order lines refer to customers and products using integer indexes such as `orderCustomerIndexes` and `lineProductIndexes`.

I think this makes the relationships between objects harder to understand and maintain. In an OOP design, an `Order` can directly reference a `Customer`, and an `OrderLine` can reference a `Product`.

## 6. No Encapsulation of Business Rules

Important business rules are implemented inside separate functions instead of being controlled by the objects that own the data.

For example, `markOrderPaid()` changes the payment state of an order, while other functions can access the order data directly because it is global.

Using classes would allow the object itself to control its own state and make sure its rules are respected.

## 7. Too Much Dependency on Global State

Many functions depend on the same global arrays and counters.

For example, `createOrder()`, `addLineToOrder()`, `calculateOrderTotal()`, and `printOrder()` all depend on global order and product data.

This creates strong coupling between the functions and the data, which makes the code harder to modify or reuse.

## 8. Functions Use Array Indexes Instead of Meaningful Objects

Some functions receive an index instead of an actual domain object.

For example:

```cpp
double calculateOrderTotal(int orderIndex)
```

The function needs to know how the internal arrays work before it can do its job.

In an OOP design, it would be clearer for the method to work with an `Order` object directly.

## 9. Lack of Input Validation for Some Data

Some functions check certain conditions, but there is no general validation when creating customers or products.

For example, the program does not properly prevent things like an empty customer name, a negative product price, or negative stock.

This means invalid data can enter the system.

## 10. Different Responsibilities Are Mixed Together

Some functions are responsible for more than one thing.

For example, `addLineToOrder()` checks the order, checks the product, validates the quantity, checks stock, changes stock, and adds the order line.

Having too many responsibilities in one function can make the code harder to understand, test, and change.

## 11. UI Logic Is Mixed With Application Logic

The `runInteractiveMenu()` function handles user input and also directly calls the application functions.

This makes the program more tightly coupled to the console interface. If we wanted to change the application to use a web API or another interface, more code would need to be changed.

## 12. Dates Are Stored as Strings

Order dates are stored using `string`:

```cpp
string orderDates[MAX_ORDERS];
```

This makes it difficult to validate dates or perform date-related operations. Using a date type such as `DateTime` in C# would make the data more meaningful and easier to work with.

## 13. The Code Relies on Manual Counters

The program uses variables such as `customerCount`, `productCount`, and `orderCount` to keep track of the number of items.

These counters have to stay synchronized with the arrays manually. This adds more state that can become incorrect if it is not updated properly.

## 14. Error Handling Is Done by Printing Messages

Many functions handle errors by printing a message and returning.

For example:

```cpp
cout << "ERROR: customer id " << id << " already exists.\n";
return;
```

This means the function does not provide a structured way for the caller to know exactly what went wrong. A better design can use exceptions or app