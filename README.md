# TaxCalculator

Implementation of a tax calculator using design patterns and S.O.L.I.D. principles

## Motivation

This small project is part of a test for an architect job application in SproutLoud

## Installation

1. Download
2. Open in Visual Studio
3. Test

## Tests

<b>How to create an order:</b>
<br/>

Create a customer
```javascript
Customer juanCamilo = new Customer
{
    Name = "Juan Camilo",
    BillingInformation = new BillingInformation { Country = "Colombia", City = "Medellin" }
};
```
Create some products
```javascript
OrderProduct pc = new OrderProduct { Classification = "000001", Price = 350, Quantity = 1 };
OrderProduct mouse = new OrderProduct { Classification = "000002", Price = 15, Quantity = 1 };
OrderProduct keyboard = new OrderProduct { Classification = "000003", Price = 30, Quantity = 2 };
```
Create an order and add some products
```javascript
Order customerOrder = new Order(juanCamilo);
customerOrder.AddOrderProduct(pc);
customerOrder.AddOrderProduct(keyboard);
```

<b>How to calculate total (including taxes):</b>
<br/>

Simply use CalculateOrderTotal method
```javascript
Customer juanCamilo = new Customer
var total = customerOrder.CalculateOrderTotal();
```

## Contributor

Juan Camilo Marin
