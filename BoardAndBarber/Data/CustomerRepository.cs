using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoardAndBarber.Models;

namespace BoardAndBarber.Data
{
    public class CustomerRepository
    {
        // properties
        static List<Customer> _customers = new List<Customer>();

        // methods
        // POST METHOD
        public void Add(Customer customerToAdd)
        {
            int newId = 1;
            if (_customers.Count > 0)
            {
                newId = _customers.Select(p => p.Id).Max() + 1;
            }

            customerToAdd.Id = newId;

            _customers.Add(customerToAdd);
        }

        // GET METHODS
        public List<Customer> GetAll()
        {
            return _customers;
        }

        public Customer GetById(int id)
        {
            return _customers.FirstOrDefault(c => c.Id == id);
        }

        // POST METHOD
        public Customer Update(int id, Customer customer)
        {
            var customerToUpdate = GetById(id);

            customerToUpdate.Birthday = customer.Birthday;
            customerToUpdate.Name = customer.Name;
            customerToUpdate.FavoriteBarber = customer.FavoriteBarber;
            customerToUpdate.Notes = customer.Notes;

            return customerToUpdate;
        }

        // DELETE METHOD
        public void Remove(int id)
        {
            var customerToDelete = GetById(id);

            _customers.Remove(customerToDelete);
        }
    }
}
