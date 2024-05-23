using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace CarRentalSystem
{
    public class UserService : IUserService
    {
        DataSet IUserService.GetUser()
        {
            SqlDataAdapter da = new SqlDataAdapter("Select Id, FirstName, MiddleName, LastName, Email, Phone, Address, Password, isAdmin FROM [User]",
                @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=CarRentalSystem;Integrated Security=True;Connect Timeout=30;Encrypt=False;");

            DataSet ds = new DataSet();
            da.Fill(ds, "users");
            return ds;
        }

        User IUserService.GetUserByEmail(string email)
        {
            SqlConnection cnn = new SqlConnection(
                @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=CarRentalSystem;Integrated Security=True;Connect Timeout=30;Encrypt=False;");

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cnn;

            cmd.CommandText = "SELECT Id, FirstName, MiddleName, LastName, Email, Phone, Address, Password, isAdmin FROM [User] WHERE Email = @Email";

            SqlParameter p = new SqlParameter("@Email", email); // Changed parameter name to match SQL query

            cmd.Parameters.Add(p);
            cnn.Open();

            SqlDataReader reader = cmd.ExecuteReader();

            User user = null; // Initialize user as null

            // Check if any rows are returned
            if (reader.Read())
            {
                // If a row is returned, create the user object and populate its properties
                user = new User
                {
                    UserId = reader.GetInt32(0),
                    firstname = reader.GetString(1),
                    middlename = reader.GetString(2),
                    lastname = reader.GetString(3),
                    email = reader.GetString(4),
                    phone = reader.GetString(5),
                    address = reader.GetString(6),
                    password = reader.GetString(7),
                    isadmin = reader.GetBoolean(8)
                };
            }

            reader.Close();
            cnn.Close();
            return user; // Return the user object, which may be null if no user is found
        }

        bool IUserService.AddUser(User user)
        {
            using (SqlConnection cn = new SqlConnection(
                @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=CarRentalSystem;Integrated Security=True;Connect Timeout=30;Encrypt=False;"))
            {
                cn.Open();

                SqlTransaction transaction = cn.BeginTransaction();

                try
                {
                    //check if the user already exists based on the email Id
                    SqlCommand cmdCheckUser = new SqlCommand();
                    cmdCheckUser.Connection = cn;
                    cmdCheckUser.Transaction = transaction;
                    cmdCheckUser.CommandText = "SELECT COUNT(*) FROM [User] WHERE Email=@email";
                    cmdCheckUser.Parameters.AddWithValue("@email", user.email);

                    int existingUserCount = (int)cmdCheckUser.ExecuteScalar();

                    if (existingUserCount > 0)
                    {
                        throw new Exception("User with this email already exists.");
                    }

                    SqlCommand cmdUser = new SqlCommand();
                    cmdUser.Connection = cn;
                    cmdUser.Transaction = transaction;
                    cmdUser.CommandText = "INSERT INTO [User] (FirstName, MiddleName, LastName, Email, Phone, Address, Password, isAdmin) VALUES (@firstname, @middlename, @lastname, @email, @phone, @address, @password, @isadmin)";

                    cmdUser.Parameters.AddWithValue("@firstname", user.firstname);
                    cmdUser.Parameters.AddWithValue("@middlename", user.middlename);
                    cmdUser.Parameters.AddWithValue("@lastname", user.lastname);
                    cmdUser.Parameters.AddWithValue("@email", user.email);
                    cmdUser.Parameters.AddWithValue("@phone", user.phone);
                    cmdUser.Parameters.AddWithValue("@address", user.address);
                    cmdUser.Parameters.AddWithValue("@password", user.password);
                    cmdUser.Parameters.AddWithValue("@isadmin", user.isadmin);

                    int rowsAffected = cmdUser.ExecuteNonQuery();

                    transaction.Commit();

                    return rowsAffected > 0;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw;
                }
                finally
                {
                    cn.Close();
                }
            }



            /*
            SqlConnection cnn = new SqlConnection(
                @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=CarRentalSystem;Integrated Security=True;Connect Timeout=30;Encrypt=False;");

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cnn;

            cmd.CommandText = "INSERT INTO [User] (FirstName, MiddleName, LastName, Email, Phone, Address, Password, isAdmin)" + 
                            "VALUES (@FirstName, @MiddleName, @LastName, @Email, @Phone, @Address, @Password, @isAdmin)";

            cmd.Parameters.AddWithValue("@FirstName", user.firstname);
            cmd.Parameters.AddWithValue("@MiddleName", user.middlename);
            cmd.Parameters.AddWithValue("@LastName", user.lastname);
            cmd.Parameters.AddWithValue("@Email", user.email);
            cmd.Parameters.AddWithValue("@Phone", user.phone);
            cmd.Parameters.AddWithValue("@Address", user.address);
            cmd.Parameters.AddWithValue("@Password", user.password); 
            cmd.Parameters.AddWithValue("@isAdmin", user.isadmin);

            try
            {
                cnn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                // Handle any exceptions here, e.g., log the error
                Console.WriteLine("Error adding user: " + ex.Message);
                return false;
            }
            */
        }

        bool IUserService.RemoveUser(int userId)
        {
            SqlConnection cnn = new SqlConnection(
                @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=CarRentalSystem;Integrated Security=True;Connect Timeout=30;Encrypt=False;");

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cnn;
            cmd.CommandText = "DELETE FROM [User] WHERE Id = @Id";

            cmd.Parameters.AddWithValue("@Id", userId);

            cnn.Open();
            int rowsAffected = cmd.ExecuteNonQuery();
            cnn.Close();

            return rowsAffected > 0;
        }

        string IUserService.GetUserName(int userId)
        {
            string name = "";
            SqlConnection cnn = new SqlConnection(
                @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=CarRentalSystem;Integrated Security=True;Connect Timeout=30;Encrypt=False;");

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cnn;
            cmd.CommandText = "SELECT FirstName FROM [User] WHERE Id = @Id";

            cmd.Parameters.AddWithValue("@Id", userId);
            cnn.Open();
            object result = cmd.ExecuteScalar();

            // Check if the result is not null
            if (result != null)
            {
                name = result.ToString();
            }
            cnn.Close();

            return name;
        }
    }
}

