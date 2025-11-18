using System.Collections.Generic;
using System.Text;

namespace OnlineOrdering
{
    public class Order
    {
        private List<Product> _products;
        private Customer _customer;

        public Order(List<Product> products, Customer customer)
        {
            _products = products;
            _customer = customer;
        }

        public double GetTotalCost()
        {
            double subtotal = 0;
            foreach (Product product in _products)
            {
                subtotal += product.GetTotalCost();
            }

            double shippingCost = _customer.IsInUSA() ? 5.00 : 35.00;

            return subtotal + shippingCost;
        }

        public string GetPackingLabel()
        {
            StringBuilder label = new StringBuilder();
            foreach (Product product in _products)
            {
                label.AppendLine($"ID: {product.GetProductID()} | Name: {product.GetName()}");
            }
            return label.ToString();
        }

        public string GetShippingLabel()
        {
            string name = _customer.GetName();
            string address = _customer.GetAddress().GetFullAddressString();
            return $"{name}\n{address}";
        }
    }
}