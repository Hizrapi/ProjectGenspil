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

        private static CustomerList? _instance;
        private static readonly object _lock = new object();
        public static CustomerList Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new CustomerList();
                    }
                    return _instance;
                }
            }
        }

        public List<Customer> _customers = new List<Customer>();

        public CustomerList()
        {
            _customers = new List<Customer>();
        }

        //Adds a customer to the list.
        public void AddCustomer()
        {
            int newCustomerID = GenerateID(0);

            Console.WriteLine($"Skriv kundens navn");
            string? name = Console.ReadLine();
            Console.WriteLine($"Skriv kundens Adresse");
            string? address = Console.ReadLine();

            Customer customer = new Customer(newCustomerID, name, address);

            _customers.Add(customer);

            Console.WriteLine($"Kunde {name} med ID {newCustomerID} tilføjet.");
        }

        //Removes a customer to the list.
        public void RemoveCustomer(Customer customer)
        {
            _customers.Remove(customer);
        }

        public List<Customer> SearchCustomers()
        {
            DisplayCustomers();


            Console.Write("Indtast navn - eller del af den: ");
            string searchName = Console.ReadLine();

            //Søg efter indput
            var foundCustomers = _customers.FindAll(g => g.Name.Contains(searchName, StringComparison.OrdinalIgnoreCase));
            if (foundCustomers.Count > 0)
            {
                Console.WriteLine($"--- Fundne Kunder (Søgning: '{searchName}') ---");
                foreach (var customer in foundCustomers)
                {
                    Console.WriteLine(customer);
                }
                Console.WriteLine("----------------------------------------------");
            }
            else
            {
                Console.WriteLine($"Ingen kunde ved navn {searchName} fundet.");
            }
            Console.ReadLine();

            return foundCustomers;

        }

        //Findes a customer on the list.
        public Customer? GetCustomerByID(int customerID)
        {
            return _customers.Find(c => c.CustomerID == customerID);
        }
        public Customer? GetCustomerByName(string name)
        {
            return _customers.Find(c => c.Name == name);
        }
        public Customer? GetCustomerByAddress(string address)
        {
            return _customers.Find(c => c.Address == address);
        }


        public void DisplayCustomers()
        {
            if (_customers.Count == 0)
            {
                Console.WriteLine($"Ingen kunder fundet.");
                return;
            }

            Console.WriteLine($"Customers List:");
            foreach (var customer in _customers)
            {
                Console.WriteLine($"{customer.CustomerID}: {customer.Name} - {customer.Address}");
            }
        }

        //Update function isn't completed.
        public void UpdateCustomer(int customerID, string newName, string newAddress) 
        {
            var customer = _customers.Find(c => c.CustomerID == customerID);

            if (customer != null)
            {
                //If customer exists, update the name and address using the setter methods
                customer.SetName(newName);
                customer.SetAddress(newAddress);

                Console.WriteLine($"Customer with ID {customerID} has been updated.");
            }
            else
            {
                //If the customer doesn't exist
                Console.WriteLine($"Customer with ID {customerID} not found.");
            }
        }

        public void RemoveCustomer()
        {
            //Lists customers
            DisplayCustomers();

            //What customer do you want to remove?
            Console.WriteLine("Skrive; navn, adresse eller ID på den kunde som du vil fjerne:");
            string input = Console.ReadLine();

            if (int.TryParse(input, out int customerID))
            {
                //Find the customer by ID
                var customerToRemove = GetCustomerByID(customerID);

                if (customerToRemove != null)
                {
                    RemoveCustomer(customerToRemove);
                    Console.WriteLine($"Kunde med ID {customerID} er blevet fjernet.");
                }
                else
                {
                    Console.WriteLine($"Kunde med ID {customerID} blev ikke fundet.");
                }
            }
            else if (!string.IsNullOrWhiteSpace(input))
            {

                    var customerByName = GetCustomerByName(input);
                    if (customerByName != null)
                    {
                        RemoveCustomer(customerByName);
                        Console.WriteLine($"Kunde ved navn {input} er blevet fjernet.");
                        return;
                    }

                    var customerByAddress = GetCustomerByAddress(input);
                    if (customerByAddress != null)
                    {
                        RemoveCustomer(customerByAddress);
                        Console.WriteLine($"Kunde med adresse {input} er blevet fjernet.");
                        return;
                    }
                    Console.WriteLine($"Kunde med navn eller adresse '{input}' blev ikke fundet.");
                }
                else
                {
                    Console.WriteLine("Skriv et navn, en adresse eller et ID");
                }
            }
        

        //public void RemoveCustomerByName()
        //{
        //    //Lists customers
        //    DisplayCustomers();

        //    //What customer do you want to remove?
        //    Console.WriteLine("Skriv navnet på kunden som du vil fjerne:");

        //    if (int.TryParse(Console.ReadLine(), out int name))
        //    {
        //        //Find the customer by name
        //        var customerToRemove = GetCustomerByID(name);

        //        if (customerToRemove != null)
        //        {
        //            //Remove the customer if found
        //            RemoveCustomer(customerToRemove);
        //            Console.WriteLine($"Kunder ved navn {name} er blevet fjernet.");
        //        }
        //        else
        //        {
        //            Console.WriteLine($"kunde with navn {name} blev ikke fundet.");
        //        }
        //    }
        //    else
        //    {
        //        Console.WriteLine("Invalid input. Please enter a valid name.");
        //    }
        //}

        public int GenerateID(int customerID)
        {
            if (_customers.Count == 0)
            {
                return 1;
            }

            return _customers.Max(c => c.CustomerID) + 1;
        }
      
    public int GenerateID(int customerID)
    {
        if (_customers.Count == 0)
        {
            return 1;
        }

        return _customers.Max(c => c.CustomerID) + 1;
    }

    public void Clear()
    {
        _customers.Clear();
    }
    }
}