using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BoardAndBarber.Models;
using BoardAndBarber.Data;

namespace BoardAndBarber.Controllers
{
    [Route("api/customers")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        //properties
        public object Id { get; private set; }

        // fields
        CustomerRepository _repo;

        // constructor
        public CustomersController()
        {
            _repo = new CustomerRepository();
        }

        // methods
        // POST
        [HttpPost]
        public IActionResult CreateCustomer(Customer customer)
        {
            _repo.Add(customer);

            return Created($"/ai/customers/{customer.Id}", customer);
        }

        // GET
        [HttpGet]
        public IActionResult GetAllCustomers()
        {
            var allCustomers = _repo.GetAll();

            return Ok(allCustomers);
        }

        // PUT
        [HttpPut("{id}")]
        public IActionResult UpdateCustomer(int id, Customer customer)
        {
            var updatedCustomer = _repo.Update(id, customer);

            return Ok(updatedCustomer);
        }

        // DELETE
        [HttpDelete("{id}")]
        public IActionResult DeleteCustomer(int id)
        {
            if (_repo.GetById(id) == null)
            {
                return NotFound();
            }

            _repo.Remove(id);

            return Ok();
        }
    }
}
