# TaxCalculator

Implementation of a tax calculator using design patterns and S.O.L.I.D. principles

## How did I solve the problem?

<b>Assumptions</b><br/>
I made some assumptions to keep things simple:

1. In model classes I ommited things such as ID properties and other properties not relevant to this challenge
2. An order has a Customer (buyer) and a list of OrderProducts
3. The OrderProduct class contains all the info related to the product, including for example, the classification
4. The Customer class contains the billing information (BillingInformation class) which has the location info (country, state, etc)

Note: there are several annotations in the form of "In real life...". This is to clarify how the model should be in a real life implementation. I made this to keep thing simple.

<br/><b>How to handle multiple tax calculators according to the Country?</b><br/>
In this case I used a Strategy pattern where I exposed an abstraction of the tax class (named Tax) and offered a common method: 
```C#
abstract decimal CalculateTax(OrderProduct product, Customer customer)
```

Then, each Country-Specific Tax class derives from Tax class and implements CalculateTax method. If a new country taxation is needed one just has to create a new class that derives from Tax. This fulfills Open/Close Principle (OCP) from S.O.L.I.D. - <i>Software should be open for extension, but closed for modification</i>.

Now, why and abstract class instead of an interface? Using a interface I have a problem:
if somehow in the future I need to provide another method to calculate tax (i.e. using the date of calculation)
then all classes that implements that interface must implement the new method too. Using an abstract class
I don't have that problem. This fulfills Interface Segregation Principle (ISP) - <i>No client should be forced to depend on 
methods it does not use. Many client-specific interfaces are better than one general-purpose interface.</i>

Actually I simulated that future scenario: after I created all classes I just have to add
a new method which could be overrided by the specific Tax class that uses the date in its calculations. The rest
of the classes didn't change.


<br/><b>How does the Order know which specific tax class to instance?</b><br/>
It does not know. In fact, it should not know cause is not part of its responsability (Sigle Responsability Principel 
from S.O.L.I.D. - <i>A class should have only a single responsibility</i>)

To solve this I implemented a TaxFactory interface (factory pattern). This class is able to 
create an instance of the proper Tax class based on the country of the customer (Consumer.BillingInformation.Country).
This class is created dynamically using .NET reflection. 
This ITaxFactory is of course a private property of the Order class which is created in its constructor

<br/><b>About other S.O.L.I.D. principles</b><br/>
Note that by using Tax abstract class and ITaxFactory interface two other S.O.L.I.D principles are fulfilled:
 * Dependency Inversion Principle (DIP) - <i>High-level modules should not depend on low-level modules. Both should depend on abstractions. Abstractions should not depend on details. Details should depend on abstractions.</i>
 
 * Liskov Substitution Principle (LSP) - <i>objects in a program should be replaceable with instances of their subtypes without altering the correctness of that program</i>


## Installation

1. Download
2. Open in Visual Studio
3. Test

## Tests

<b>How to create an order:</b>
<br/>

Create a customer
```C#
Customer juanCamilo = new Customer
{
    Name = "Juan Camilo",
    BillingInformation = new BillingInformation { Country = "Colombia", City = "Medellin" }
};
```
Create some products
```C#
OrderProduct pc = new OrderProduct { Classification = "000001", Price = 350, Quantity = 1 };
OrderProduct mouse = new OrderProduct { Classification = "000002", Price = 15, Quantity = 1 };
OrderProduct keyboard = new OrderProduct { Classification = "000003", Price = 30, Quantity = 2 };
```
Create an order and add some products
```C#
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
