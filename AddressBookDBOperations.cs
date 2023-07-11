using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Collections;

namespace AddressBookUsingAdo
{
    public class AddressBookDBOperations
    {
        private string connection = $"Data source= PRAJWAL; Database = AddressBook; Integrated Security = true; TrustServerCertificate = true";

        private SqlConnection sqlConnection;

        public AddressBookDBOperations()
        {
            sqlConnection = new SqlConnection(connection);
        }

        // it's worth mentioning that directly interpolating values into SQL queries can make your code vulnerable to SQL injection attacks.
        // To mitigate this risk, it's recommended to use parameterized queries and pass the values as parameters instead of directly including them in the query string.
        // This approach helps prevent malicious input from altering the structure or behavior of your SQL queries.
        // so dont use this type of methods 

        /*public bool AddContact(Contact contact)
        {
            try
            {
                sqlConnection.Open();
                string query = $"INSERT INTO Contacts VALUES ('{contact.FirstName}','{contact.LastName}','{contact.PhoneNumber}','{contact.Email}','{contact.City}','{contact.PinCode}','{contact.Country}','{contact.Sstate}')";
                string query2 = $"INSERT INTO PhoneNumber VALUES ('{contact.Id}','{contact.PhoneNumber2}')";
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                SqlCommand sqlCommand2 = new SqlCommand(query2, sqlConnection);
                int result = sqlCommand.ExecuteNonQuery();
                int result2 = sqlCommand2.ExecuteNonQuery();
                if (result > 0 & result2 > 0)
                {
                    Console.WriteLine($"{result} number of rows affected in Contact Table");
                    Console.WriteLine($"{result2} number of rows affected in PhoneNumber Table");
                }
                else
                {
                    Console.WriteLine("Something went wrong");
                    sqlConnection.Close();
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
            finally
            {
                sqlConnection.Close();
            }
        }*/
        public bool AddContact(Contact contact)
        {
            try
            {
                sqlConnection.Open();
                string query = $"INSERT INTO Contacts VALUES (@FirstName,@LastName,@PhoneNumber,@Email,@City,@PinCode,@Country,@Sstate)";
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                sqlCommand.Parameters.AddWithValue("@FirstName", contact.FirstName);
                sqlCommand.Parameters.AddWithValue("@LastName", contact.LastName);
                sqlCommand.Parameters.AddWithValue("@PhoneNumber", contact.PhoneNumber);
                sqlCommand.Parameters.AddWithValue("@Email", contact.Email);
                sqlCommand.Parameters.AddWithValue("@City", contact.City);
                sqlCommand.Parameters.AddWithValue("@PinCode", contact.PinCode);
                sqlCommand.Parameters.AddWithValue("@Country", contact.Country);
                sqlCommand.Parameters.AddWithValue("@Sstate", contact.Sstate);
                int result = sqlCommand.ExecuteNonQuery();

                string query2 = $"INSERT INTO PhoneNumber VALUES (@Id, @PhoneNumber2)";
                SqlCommand sqlCommand2 = new SqlCommand(query2, sqlConnection);
                sqlCommand2.Parameters.AddWithValue("@Id", contact.Id);
                sqlCommand2.Parameters.AddWithValue("@PhoneNumber2", contact.PhoneNumber2);
                int result2 = sqlCommand2.ExecuteNonQuery();
                if (result > 0 && result2 > 0)
                {
                    Console.WriteLine($"{result} number of rows affected in Contact Table");
                    Console.WriteLine($"{result2} number of rows affected in PhoneNumber Table");
                }
                else
                {
                    Console.WriteLine("Something went wrong");
                    sqlConnection.Close();
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public bool AddByStroedProcedure(Contact contact)
        {
            try
            {
                sqlConnection.Open();
                string query = "AddContact";
                SqlTransaction sqlTransaction = sqlConnection.BeginTransaction(); /// add this to make it transactional ->1
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection, sqlTransaction); /// paas that object here    ->2
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@FirstName", contact.FirstName);
                sqlCommand.Parameters.AddWithValue("@LastName", contact.LastName);
                sqlCommand.Parameters.AddWithValue("@PhoneNumber1", contact.PhoneNumber);
                sqlCommand.Parameters.AddWithValue("@Email", contact.Email);
                sqlCommand.Parameters.AddWithValue("@City", contact.City);
                sqlCommand.Parameters.AddWithValue("@PinCode", contact.PinCode);
                sqlCommand.Parameters.AddWithValue("@Country", contact.Country);
                sqlCommand.Parameters.AddWithValue("@Sstate", contact.Sstate);
                sqlCommand.Parameters.AddWithValue("@PhoneNumber2", contact.PhoneNumber);
                try
                {
                    int result = sqlCommand.ExecuteNonQuery();
                    sqlTransaction.Commit();
                    if (result > 0)
                    {
                        Console.WriteLine($"{result} number of rows affected in Contact Table");
                        Console.WriteLine("Data added .....");
                        return true;
                    }
                    else
                    {
                        Console.WriteLine("Something went wrong");
                        sqlConnection.Close();
                    }
                }
                catch (Exception)
                {
                    sqlTransaction.Rollback();
                    Console.WriteLine("Rollback changes ");
                }
                return true;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
            finally
            {
                sqlConnection.Close();
            }
        }


        public bool Delete(int id)
        {
            try
            {
                sqlConnection.Open();
                string query1 = $"Delete FROM PhoneNumber WHERE Id = 1";
                string query = $"Delete FROM Contacts WHERE Id = 1";
                SqlCommand sqlCommand = new SqlCommand(query1, sqlConnection);
                SqlCommand sqlCommand2 = new SqlCommand(query, sqlConnection);
                sqlCommand.ExecuteNonQuery();
                sqlCommand2.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);              
                return false;
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public List<Contact> GetAllContact()
        {
            List<Contact> list = new List<Contact>();
            sqlConnection.Open();
            string query = $"SELECT * From Contacts AS c JOIN PhoneNumber As p ON c.Id = p.Id";
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

            SqlDataReader reader = sqlCommand.ExecuteReader();
            while (reader.Read())
            {
                Contact emp = new Contact()
                {
                    Id = (int)reader["Id"],
                    FirstName = (string)reader["FirstName"],
                    LastName = (string)reader["LastName"],
                    PhoneNumber = (string)reader["PhoneNumber1"],
                    PhoneNumber2 = (string)reader["PhoneNumber2"],
                    Email = (string)reader["Email"],
                    City = (string)reader["City"],
                    Sstate = (string)reader["Sstate"],
                    PinCode = (string)reader["PinCode"],
                    Country = (string)reader["Country"]
                };
                list.Add(emp);
            }
            foreach (Contact contact in list)
            {
                Console.WriteLine($"Id: {contact.Id}\t Name:- {contact.FirstName}\t LastName:- {contact.LastName}" +
                    $"\tPhoneNumber:- {contact.PhoneNumber}  \tAlternate PhoneNumber: - {contact.PhoneNumber2} \tEmail:- {contact.Email} \tCity:- {contact.City} \tState:- {contact.Sstate} \tPinCode:- {contact.PinCode}" +
                    $"\tCountry:- {contact.Country}");
            }
            sqlConnection.Close();
            return list;
        }
        public bool GetContactByID(int id)
        {
            try
            {
                sqlConnection.Open();
                string query = $"SELECT * From Contacts AS c JOIN PhoneNumber As p ON c.Id = p.Id where c.Id = {id}";
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                List<Contact> list = new List<Contact>();

                SqlDataReader reader = sqlCommand.ExecuteReader();
                while (reader.Read())
                {
                    Contact contact = new Contact()
                    {
                        Id = (int)reader["Id"],
                        FirstName = (string)reader["FirstName"],
                        LastName = (string)reader["LastName"],
                        PhoneNumber = (string)reader["PhoneNumber1"],
                        PhoneNumber2 = (string)reader["PhoneNumber2"],
                        Email = (string)reader["Email"],
                        City = (string)reader["City"],
                        Sstate = (string)reader["Sstate"],
                        PinCode = (string)reader["PinCode"],
                        Country = (string)reader["Country"]
                    };
                    list.Add(contact);
                }
                foreach (Contact contact in list)
                {
                    if (contact.Id == id)
                    {
                        Console.WriteLine($"Id: {contact.Id}\t Name:- {contact.FirstName}\t LastName:- {contact.LastName}" +
                            $"\tPhoneNumber:- {contact.PhoneNumber}  \tAlternate PhoneNumber: - {contact.PhoneNumber2} \tEmail:- {contact.Email} \tCity:- {contact.City} \tState:- {contact.Sstate} \tPinCode:- {contact.PinCode}" +
                            $"\tCountry:- {contact.Country}");

                    }
                    else
                    {
                        Console.WriteLine($"No data found For Id {id}");
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public bool UpdateContact(Contact contact)
        {
            try
            {
                sqlConnection.Open();
                //string query = $"UPDATE Contacts SET Email = '{contact.Email}' WHERE Id = '{contact.Id}'";
                string query1 = $"UPDATE Contacts SET Email = @Emailid WHERE Id = @Id";
                SqlCommand sqlCommand = new SqlCommand(query1, sqlConnection);
                sqlCommand.Parameters.AddWithValue("@Emailid",contact.Email);
                sqlCommand.Parameters.AddWithValue("@Id",contact.Id);
                int result =sqlCommand.ExecuteNonQuery();
                if(result>0)
                {
                    Console.WriteLine("Data Updated...");
                    Console.WriteLine($"{result} rows affected");
                }
                return true;
            }
            catch (Exception)
            {
                sqlConnection.Close();
                return false;
            }
            finally
            {
                sqlConnection.Close();
            }
        }


        public bool DisplayByStoredProcedure()
        {
            try
            {
                List<Contact> list = new List<Contact>();
                sqlConnection.Open();
                string Query = "GetAllContact";
                SqlCommand sqlCommand = new SqlCommand(Query, sqlConnection);

                SqlDataReader reader = sqlCommand.ExecuteReader();
                while (reader.Read())
                {
                    Contact contact = new Contact()
                    {
                        Id = (int)reader["Id"],
                        FirstName = (string)reader["FirstName"],
                        LastName = (string)reader["LastName"],
                        PhoneNumber = (string)reader["PhoneNumber1"],
                        PhoneNumber2 = (string)reader["PhoneNumber2"],
                        Email = (string)reader["Email"],
                        City = (string)reader["City"],
                        Sstate = (string)reader["Sstate"],
                        PinCode = (string)reader["PinCode"],
                        Country = (string)reader["Country"]
                    };
                    list.Add(contact);
                }
                foreach (Contact contact in list)
                {
                    Console.WriteLine($"Id: {contact.Id}\t Name:- {contact.FirstName}\t LastName:- {contact.LastName}" +
                        $"\tPhoneNumber:- {contact.PhoneNumber}  \tAlternate PhoneNumber: - {contact.PhoneNumber2} \tEmail:- {contact.Email} \tCity:- {contact.City} \tState:- {contact.Sstate} \tPinCode:- {contact.PinCode}" +
                        $"\tCountry:- {contact.Country}");
                }
                sqlConnection.Close();
                return true;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Console.WriteLine("Something went wrong");
                return false;
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        
    }
}