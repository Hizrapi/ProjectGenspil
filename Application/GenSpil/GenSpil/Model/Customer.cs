using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GenSpil.Model
{
    public class Customer
    {
        public int CustomerID { get; private set; }
        string Name { get; set; }
        string Address { get; set; }

        public Customer(int customerID, string name, string address)
        {
            CustomerID = customerID;
            Name = name;
            Address = address;
        }

        public static int GetCustomerID(Customer customer)
        {
            return customer.CustomerID;
        }

        public static string GetName(Customer customer)
        {
            return customer.Name;
        }

        public static string GetAddress(Customer customer)
        {
            return customer.Address;
        }

        public void SetName(string name)
        {
            Name = name;
        }

        public void SetAddress(string address)
        {
            Address = address;
        }

        public override string ToString()
        {
            return $"CustomerID: {CustomerID}, Name: {Name}, Address: {Address}";
        }
    }
}
