using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem
{
    public class BookingService : IBookingService
    {
        DataSet IBookingService.GetAllBooking()
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT Id, user_id, car_id, start_date, end_date, booking_date, price, driver_required FROM [Booking]",
                @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=CarRentalSystem;Integrated Security=True;Connect Timeout=30;Encrypt=False;");

            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;
        }

        public int AddBooking(Booking booking)
        {
            using (SqlConnection cn = new SqlConnection(
                @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=CarRentalSystem;Integrated Security=True;Connect Timeout=30;Encrypt=False;"))
            {
                cn.Open();

                SqlTransaction transaction = cn.BeginTransaction();

                try
                {
                    SqlCommand cmdCheckExistingBookings = new SqlCommand(
                        "SELECT COUNT(*) FROM [Booking] WHERE car_id = @car_id AND ((@start_date BETWEEN start_date AND end_date) OR (@end_date BETWEEN start_date AND end_date))",
                        cn, transaction);

                    cmdCheckExistingBookings.Parameters.AddWithValue("@car_id", booking.CarId);
                    cmdCheckExistingBookings.Parameters.AddWithValue("@start_date", booking.StartDate);
                    cmdCheckExistingBookings.Parameters.AddWithValue("@end_date", booking.EndDate);

                    int existingBookingsCount = (int)cmdCheckExistingBookings.ExecuteScalar();

                    if (existingBookingsCount == 0)
                    {
                        booking.booking_date = DateTime.Now;
                        //number of days
                        int numberOfDays = (int)(booking.EndDate - booking.StartDate).TotalDays;
                        numberOfDays = numberOfDays + 1;

                        //getting price per day for particular car
                        SqlCommand cmdGetRentalPrice = new SqlCommand(
                            "SELECT rental_price FROM [Car] WHERE Id = @car_id", cn, transaction);

                        cmdGetRentalPrice.Parameters.AddWithValue("@car_id", booking.CarId);

                        int perDayPrice = (int)cmdGetRentalPrice.ExecuteScalar();

                        //calculating total price without cost of driver
                        int totalPrice = numberOfDays * perDayPrice;

                        // If driver is required, add the additional cost of the driver to the total price
                        if (booking.DriverRequired)
                        {
                            SqlCommand cmdDriverCost = new SqlCommand(
                                "SELECT driver_cost FROM [Car] WHERE Id = @car_id", cn, transaction);

                            cmdDriverCost.Parameters.AddWithValue("@car_id", booking.CarId);

                            int driverCostPerDay = (int)cmdDriverCost.ExecuteScalar();

                            // Add driver cost to the total price
                            totalPrice += driverCostPerDay * numberOfDays;
                        }

                        //inserting value of total price
                        SqlCommand cmdAddBooking = new SqlCommand(
                            "INSERT INTO [Booking] (user_id, car_id, start_date, end_date, booking_date, price, driver_required) VALUES (@user_id, @car_id, @start_date, @end_date, @booking_date, @price, @driver_required); SELECT SCOPE_IDENTITY()",
                            cn, transaction);

                        cmdAddBooking.Parameters.AddWithValue("@user_id", booking.UserId);
                        cmdAddBooking.Parameters.AddWithValue("@car_id", booking.CarId);
                        cmdAddBooking.Parameters.AddWithValue("@start_date", booking.StartDate);
                        cmdAddBooking.Parameters.AddWithValue("@end_date", booking.EndDate);
                        cmdAddBooking.Parameters.AddWithValue("@booking_date", booking.booking_date);
                        cmdAddBooking.Parameters.AddWithValue("@price", totalPrice);
                        cmdAddBooking.Parameters.AddWithValue("@driver_required", booking.DriverRequired);

                        // Execute the command and return the generated booking ID
                        int bookingId = Convert.ToInt32(cmdAddBooking.ExecuteScalar());

                        transaction.Commit(); // Commit the transaction if the insert was successful
                        return bookingId;
                    }
                    else
                    {
                        transaction.Rollback();

                        // Return -1 to indicate failed booking due to existing bookings
                        return -1;
                    }
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
        }

        public Booking GetBookingById(int bookingId)
        {
            Booking booking = null;
            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=CarRentalSystem;Integrated Security=True;Connect Timeout=30;Encrypt=False;";
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand cmdGetBooking = new SqlCommand(
                        "SELECT * FROM [Booking] WHERE Id = @bookingId", connection);

                    cmdGetBooking.Parameters.AddWithValue("@bookingId", bookingId);

                    SqlDataReader reader = cmdGetBooking.ExecuteReader();

                    if (reader.Read())
                    {
                        booking = new Booking
                        {
                            Id = (int)reader["Id"],
                            user_id = (int)reader["user_id"],
                            car_id = (int)reader["car_id"],
                            start_date = (DateTime)reader["start_date"],
                            end_date = (DateTime)reader["end_date"],
                            booking_date = (DateTime)reader["booking_date"],
                            price = (int)reader["price"],
                            driver_required = (bool)reader["driver_required"]
                        };
                    }

                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions
                Console.WriteLine("Error: " + ex.Message);
            }

            return booking;
        }

        bool IBookingService.RemoveBooking(int bookingId)
        {
            SqlConnection cnn = new SqlConnection(
                @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=CarRentalSystem;Integrated Security=True;Connect Timeout=30;Encrypt=False;");

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cnn;
            cmd.CommandText = "DELETE FROM [Booking] WHERE Id = @Id";

            cmd.Parameters.AddWithValue("@id", bookingId);

            cnn.Open();
            int rowsAffected = cmd.ExecuteNonQuery();
            cnn.Close();

            return rowsAffected > 0;
        }

        Booking[] IBookingService.GetBookingByUserId(int userId)
        {
            List<Booking> bookings = new List<Booking>();

            using (SqlConnection cn = new SqlConnection(
                @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=CarRentalSystem;Integrated Security=True;Connect Timeout=30;Encrypt=False;"))
            {
                cn.Open();

                SqlCommand cmdGetBookings = new SqlCommand(
                "SELECT * FROM [Booking] WHERE user_id = @userId", cn);

                cmdGetBookings.Parameters.AddWithValue("@userId", userId);

                using (SqlDataReader reader = cmdGetBookings.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Booking booking = new Booking
                        {
                            BookingId = (int)reader["Id"],
                            CarId = (int)reader["car_id"],
                            UserId = (int)reader["user_id"],
                            StartDate = (DateTime)reader["start_date"],
                            EndDate = (DateTime)reader["end_date"],
                            BookingDate = (DateTime)reader["booking_date"],
                            Price = (int)reader["price"],
                            DriverRequired = (bool)reader["driver_required"],
                        };
                        bookings.Add(booking);
                    }
                }
                cn.Close();
            }
            return bookings.ToArray();
        }

        Booking[] IBookingService.GetBookingByCarId(int carId)
        {
            List<Booking> bookings = new List<Booking>();

            using (SqlConnection cn = new SqlConnection(
                @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=CarRentalSystem;Integrated Security=True;Connect Timeout=30;Encrypt=False;"))
            {
                cn.Open();

                SqlCommand cmdGetBookings = new SqlCommand(
                    "SELECT * FROM [Booking] WHERE car_id = @car_id", cn);

                cmdGetBookings.Parameters.AddWithValue("@car_id", carId);

                using (SqlDataReader reader = cmdGetBookings.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Booking booking = new Booking
                        {
                            BookingId = (int)reader["Id"],
                            CarId = (int)reader["car_id"],
                            UserId = (int)reader["user_id"],
                            StartDate = (DateTime)reader["start_date"],
                            EndDate = (DateTime)reader["end_date"],
                            BookingDate = (DateTime)reader["booking_date"],
                            Price = (int)reader["price"],
                            DriverRequired = (bool)reader["driver_required"],
                        };
                        bookings.Add(booking);
                    }
                }

                cn.Close();
            }
            return bookings.ToArray();
        }

        public DataSet FindAvailableCars(DateTime startDate, DateTime endDate)
        {
            DataSet availableCarsDataSet = new DataSet();

            try
            {
                string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=CarRentalSystem;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = @"SELECT Id, car_model, car_number_plate, rental_price, capacity, fuel_type, mileage, driver_cost FROM Car WHERE Id NOT IN 
                (SELECT car_id FROM Booking 
                WHERE (@StartDate BETWEEN start_date AND end_date) 
                OR (@EndDate BETWEEN start_date AND end_date) OR
                   (start_date BETWEEN @StartDate AND @EndDate) OR
                        (end_date BETWEEN @StartDate AND @EndDate))";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@StartDate", startDate);
                        command.Parameters.AddWithValue("@EndDate", endDate);
                        connection.Open();

                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        adapter.Fill(availableCarsDataSet);
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions
                Console.WriteLine("Error: " + ex.Message);
            }

            return availableCarsDataSet;
        }

        public List<int> GetCarIdsByFuelType(string fuelType)
        {
            List<int> carIds = new List<int>();
            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=CarRentalSystem;Integrated Security=True";
            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                string query = "SELECT Id FROM Car WHERE fuel_type = @fuelType";

                using (SqlCommand cmd = new SqlCommand(query, cnn))
                {
                    cmd.Parameters.AddWithValue("@fuelType", fuelType);

                    cnn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            carIds.Add(reader.GetInt32(0));
                        }
                    }
                }
            }

            return carIds;
        }

        public DataSet GetBookingsByFuelType(string fuelType)
        {
            List<int> carIds = GetCarIdsByFuelType(fuelType);

            DataSet bookingsDataSet = new DataSet();
            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=CarRentalSystem;Integrated Security=True";
            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Booking WHERE car_id IN (";

                // Append car IDs to the query
                for (int i = 0; i < carIds.Count; i++)
                {
                    query += carIds[i];
                    if (i < carIds.Count - 1)
                    {
                        query += ",";
                    }
                }

                query += ")";

                using (SqlCommand cmd = new SqlCommand(query, cnn))
                {
                    cnn.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(bookingsDataSet, "bookings");
                }
            }

            return bookingsDataSet;
        }
    }
}
