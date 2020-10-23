using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using BoardAndBarber.Models;
using Dapper;

namespace BoardAndBarber.Data
{
    public class CustomerRepository
    {
        // fields
        static List<Customer> _customers = new List<Customer>();

        const string _connectionString = "Server=localhost;Database=BoardAndBarber;Trusted_Connection=True;";


    // methods

    // POST METHOD
    public void Add(Customer customerToAdd)
        {
            var query = @"INSERT INTO[dbo].[Customer]
            (               [Name]
                           ,[Birthday]
                           ,[FavoriteBarber]
                           ,[Notes])
                    Output Inserted.Id
                    VALUES
                        (@name,@birthday,@favoriteBarber,@notes)";

            using var db = new SqlConnection(_connectionString);

            var newId = db.ExecuteScalar<int>(query, customerToAdd);

            customerToAdd.Id = newId;
        }

        // GET METHODS
        public IEnumerable<Customer> GetAll()
        {
            using var db = new SqlConnection(_connectionString);

            var query = "select * from Customer";

            var customers = db.Query<Customer>(query);

            return customers;
        }

        public Customer GetById(int customerId)
        {
            using var db = new SqlConnection(_connectionString);

            var query = @"select *
                          from Customer
                          where id = @cid";

            var parameters = new { cid = customerId };

            var customer = db.QueryFirstOrDefault<Customer>(query, parameters);

            return customer;
        }

        // PUT METHOD
        public Customer Update(int id, Customer customer)
        {
            var query = @"UPDATE [dbo].[Customer]
                             SET [Name] = @name
                            ,[Birthday] = @birthday
                            ,[FavoriteBarber] = @favoriteBarber
                            ,[Notes] = @notes
                            Output inserted.*
                        WHERE id = @id";

            using var db = new SqlConnection(_connectionString);

            var parameters = new
            {
                // You don't need to do it this way if your names match between your parameter and the object property on the right
                Name = customer.Name,
                Birthday = customer.Birthday,
                FavoriteBarber = customer.FavoriteBarber,
                Notes = customer.Notes,
                id = id
            };

            var updatedCustomer = db.QueryFirstOrDefault<Customer>(query, parameters);

            return updatedCustomer;
        }

        // DELETE METHOD
        public void Remove(int customerId)
        {
            var query = @"DELETE
                          FROM [dbo].[Customer]
                          WHERE Id = @id";

            using var db = new SqlConnection(_connectionString);

            db.Execute(query, new { id = customerId });
        }
    }
}
