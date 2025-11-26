using System;
using System.Collections.Generic;
using OnlineOrdering; // <--- ADDED LINE: This tells the compiler where to find the classes

public class Program
{
    public static void Main(string[] args)
    {
        Address address1 = new Address("123 Main St", "Anytown", "CA", "USA");
        Customer customer1 = new Customer("John Smith", address1);

        Product p1 = new Product("Laptop Charger", "LC-001", 15.50, 2);
        Product p2 = new Product("Wireless Mouse", "WM-045", 25.00, 1);
        
        List<Product> products1 = new List<Product> { p1, p2 };
        Order order1 = new Order(products1, customer1);

        Address address2 = new Address("78 Rue de la Paix", "Paris", "Ile-de-France", "France");
        Customer customer2 = new Customer("Marie Dubois", address2);

        Product p3 = new Product("Coffee Mug", "CM-300", 8.99, 4);
        Product p4 = new Product("Keyboard Cover", "KC-777", 12.00, 1);
        Product p5 = new Product("USB Hub", "UH-900", 30.00, 1);

        List<Product> products2 = new List<Product> { p3, p4, p5 };
        Order order2 = new Order(products2, customer2);

        Console.WriteLine("--- Order 1 (USA Customer) ---");
        Console.WriteLine("Packing Label:");
        Console.WriteLine(order1.GetPackingLabel());
        Console.WriteLine("Shipping Label:");
        Console.WriteLine(order1.GetShippingLabel());
        Console.WriteLine($"Total Cost: ${order1.GetTotalCost():F2}\n");

        Console.WriteLine("--- Order 2 (International Customer) ---");
        Console.WriteLine("Packing Label:");
        Console.WriteLine(order2.GetPackingLabel());
        Console.WriteLine("Shipping Label:");
        Console.WriteLine(order2.GetShippingLabel());
        Console.WriteLine($"Total Cost: ${order2.GetTotalCost():F2}\n");
    }
}