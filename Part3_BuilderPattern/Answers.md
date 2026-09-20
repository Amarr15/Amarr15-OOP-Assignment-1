# Part 3 — Builder Pattern

## 3.1 Why is a 20-parameter constructor a problem?

A constructor with 20 parameters is hard to read and hard to use.

For example:

```csharp
new Invoice(
    customerName,
    customerEmail,
    billingStreet,
    billingCity,
    billingCountry,
    shippingStreet,
    shippingCity,
    shippingCountry,
    orderNumber,
    orderDate,
    subtotal,
    tax,
    discount,
    total,
    paymentMethod,
    currency,
    isPaid,
    notes,
    ...
);
```

It becomes difficult to remember which value belongs to which parameter, especially when several parameters have the same type.

It is also easy to pass values in the wrong order.

The deeper design problem is that the `Invoice` contains different groups of related data, such as billing address, shipping address, order information, and payment information.

Putting all of them into one large constructor makes the object harder to understand, validate, and construct.

---

## 3.2 Why use the Builder Pattern?

The Builder Pattern allows the object to be created step by step using meaningful method names.

For example:

```csharp
var invoice = new InvoiceBuilder()
    .SetCustomerName("Ahmed")
    .SetBillingAddress(...)
    .SetShippingAddress(...)
    .SetOrderNumber("ORD-1001")
    .SetPaymentMethod("Card")
    .Build();
```

This is easier to read and makes the construction process clearer.

The `Build()` method can also validate that all mandatory information has been provided before creating the final `Invoice`.

---

## 3.3 Why split AddressBuilder and OrderBuilder?

Splitting the builders gives each builder a focused responsibility.

`AddressBuilder` handles address-related information and validation.

`OrderBuilder` handles order and payment-related information.

This follows the Single Responsibility Principle and makes the individual builders easier to understand, test, validate, and reuse.

They can also be composed together when creating the final `Invoice`.

For example:

```csharp
var invoice = new InvoiceBuilder()
    .SetBillingAddress(billingAddress)
    .SetShippingAddress(shippingAddress)
    .SetOrder(order)
    .Build();
```

This makes the final invoice construction clearer and avoids putting all construction logic into one large builder.