using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using BoardAndBarber.Models;

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
        public List<Customer> GetAll()
        {
            using var connection = new SqlConnection(_connectionString);

            connection.Open();

            var command = connection.CreateCommand();
            var query = "select * from Customer";

            command.CommandText = query;

            var reader = command.ExecuteReader();
            var customers = new List<Customer>();

            while(reader.Read())
            {
                var customer = MapToCustomer(reader);
                customers.Add(customer);
            }
            return customers;
        }

        public Customer GetById(int id)
        {
            // the "Server-localhost..." can be replaced using the _connectionString
            // field above
            // I didn't do this because I wanted the example. It can be replaced
            // after the next commit
            using var connection = new SqlConnection("Server-localhost; Database-BoardAndBarber;Trusted_Connection-True;");
            connection.Open();

            var command = connection.CreateCommand();
            var query = @"select *
                          from Customer
                          where id = {id}";
            command.CommandText = query;

            // run this query and I don't care about the results
            //command.ExecuteNonQuery()

            // run this query, and only return the top row's leftmost column
            // command.ExecuteScalar()

            //run this query and give me the results, one row at a time
            var reader = command.ExecuteReader();
            //sql server has executed the command and is waiting to give us the results

            if (reader.Read())
            {
                // this block can be replaced by the method above
                // I did not replace this block with that method because I wanted the example here
                // uploaded to github. It can be replaced after a commit.
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
            else
            {
                return null;
            }
        }

        // PUT METHOD
        public Customer Update(int id, Customer customer)
        {
            var query = @"UPDATE [dbo].[Customer]
                             SET [Name] = <Name, varchar(100),>
                            ,[Birthday] = <Birthday, datetime,>
                            ,[FavoriteBarber] = <FavoriteBarber, varchar(100),>
                            ,[Notes] = <Notes, varchar(2000),>
                            Output inserted.*
                        WHERE <Search Conditions,,>";

            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = query;

            command.Parameters.AddWithValue("name", customer.Name);
            command.Parameters.AddWithValue("birthday", customer.Birthday);
            command.Parameters.AddWithValue("favoriteBarber", customer.FavoriteBarber);
            command.Parameters.AddWithValue("notes", customer.Notes);
            command.Parameters.AddWithValue("id", id);

            var reader = command.ExecuteReader();

            if(reader.Read())
            {
                return MapToCustomer(reader);
            }

            return null;
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
