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
    // private method to create Customer object when returning info from db
    Customer MapToCustomer(SqlDataReader reader)
        {
            var CustomerFromDb = new Customer();
            // do something with the one result
            CustomerFromDb.Id = (int)reader["id"];
            CustomerFromDb.Name =
                reader["Name"] as string;
            CustomerFromDb.Birthday = DateTime.Parse(reader["Birthday"].ToString());
            CustomerFromDb.FavoriteBarber = reader["FavoriteBarber"].ToString();
            CustomerFromDb.Notes = reader["Notes"].ToString();

            return CustomerFromDb;
        }

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

            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = query;

            command.Parameters.AddWithValue("name", customerToAdd.Name);
            command.Parameters.AddWithValue("birthday", customerToAdd.Birthday);
            command.Parameters.AddWithValue("favoriteBarber", customerToAdd.FavoriteBarber);
            command.Parameters.AddWithValue("notes", customerToAdd.Notes);

            var newId = (int)command.ExecuteScalar();

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

            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = query;

            command.Parameters.AddWithValue("id", customerId);

            var rows = command.ExecuteNonQuery();

            // then do an if statement, so if there's not a customerId passed in it does something?

        }
    }
}
