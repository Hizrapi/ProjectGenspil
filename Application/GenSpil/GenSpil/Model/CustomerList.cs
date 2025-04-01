using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GenSpil.Model
{
    public class CustomerList
    {
        private List<Customer> _customers;

        public CustomerList()
        {
            _customers = new List<Customer>();
        }

        public void AddCustomer(Customer customer)
        {
            _customers.Add(customer);
        }

        public void RemoveCustomer(Customer customer)
        {
            _customers.Remove(customer);
        }

        public Customer? GetCustomerByID(int customerID)
        {
            return _customers.Find(c => c.CustomerID == customerID);
        }

        public void UpdateCustomer(Customer customer) 
        {
            
        }

        public int GenerateID(int customerID)
        {
            if (_customers.Count == 0)
            {
                return 1;
            }

            return _customers.Max(c => c.CustomerID) + 1;
        }
    }
}
