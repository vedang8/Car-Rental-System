using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data.SqlClient;


namespace CarRentalSystem
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class CarService : ICarService
    {
        DataSet ICarService.GetCars()
        {
            SqlDataAdapter da = new SqlDataAdapter("Select Id, car_model, car_number_plate, rental_price, capacity, fuel_type, mileage, driver_cost, image FROM [Car]",
                @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=CarRentalSystem;Integrated Security=True;Connect Timeout=30;Encrypt=False;");

            DataSet ds = new DataSet();
            da.Fill(ds, "cars");
            return ds;
        }

        Car ICarService.GetCar(int id)
        {
            SqlConnection cnn = new SqlConnection(
                @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=CarRentalSystem;Integrated Security=True;Connect Timeout=30;Encrypt=False;");

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cnn;
            cmd.CommandText = "SELECT Id, car_model, car_number_plate, rental_price, capacity, fuel_type, mileage, driver_cost, image FROM [Car] WHERE Id = @id";
            SqlParameter p = new SqlParameter("@id", id);
            cmd.Parameters.Add(p);
            cnn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            Car car = new Car();

            while (reader.Read())
            {
                car.CarId = reader.GetInt32(0);
                car.CarModel = reader.GetString(1);
                car.NumberPlate = reader.GetString(2);
                car.RentPrice = reader.GetInt32(3);
                car.Capacity = reader.GetInt32(4);
                car.FuelType = reader.GetString(5);
                car.Mileage = reader.GetInt32(6);
                car.DriverCost = reader.GetInt32(7);
                car.Image = reader.GetString(8);
            }

            reader.Close();
            cnn.Close();
            return car;
        }

        public bool AddCar(Car car)
        {
            using (SqlConnection cnn = new SqlConnection(
                @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=CarRentalSystem;Integrated Security=True;Connect Timeout=30;Encrypt=False;"))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn;
                cmd.CommandText = @"INSERT INTO [Car] (car_model, car_number_plate, rental_price, capacity, fuel_type, mileage, driver_cost, image)" +
                                    "VALUES (@car_model, @car_number_plate, @rental_price, @capacity, @fuel_type, @mileage, @driver_cost, @image)";

                // Parameters adjusted to match the property names of the Car object
                cmd.Parameters.AddWithValue("@car_model", car.CarModel);
                cmd.Parameters.AddWithValue("@car_number_plate", car.NumberPlate);
                cmd.Parameters.AddWithValue("@rental_price", car.RentPrice);
                cmd.Parameters.AddWithValue("@capacity", car.Capacity);
                cmd.Parameters.AddWithValue("@fuel_type", car.FuelType);
                cmd.Parameters.AddWithValue("@mileage", car.Mileage);
                cmd.Parameters.AddWithValue("@driver_cost", car.DriverCost);
                cmd.Parameters.AddWithValue("@image", car.Image);
                cnn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                cnn.Close();
                return rowsAffected > 0;
            }
        }

        bool ICarService.EditCar(Car car)
        {
            try
            {
                using (SqlConnection cnn = new SqlConnection(
                    @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=CarRentalSystem;Integrated Security=True;Connect Timeout=30;Encrypt=False;"))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = cnn;
                        cmd.CommandText = "UPDATE Car SET car_model = @Model, car_number_plate = @NumberPlate, rental_price = @RentalPrice, capacity = @Capacity, fuel_type = @FuelType, mileage = @Mileage, driver_cost = @DriverCost WHERE Id = @CarId";
                        cmd.Parameters.AddWithValue("@Model", car.CarModel);
                        cmd.Parameters.AddWithValue("@NumberPlate", car.NumberPlate);
                        cmd.Parameters.AddWithValue("@RentalPrice", car.RentPrice);
                        cmd.Parameters.AddWithValue("@Capacity", car.Capacity);
                        cmd.Parameters.AddWithValue("@FuelType", car.FuelType);
                        cmd.Parameters.AddWithValue("@Mileage", car.Mileage);
                        cmd.Parameters.AddWithValue("@DriverCost", car.DriverCost);
                        cmd.Parameters.AddWithValue("@CarId", car.CarId);

                        cnn.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();

                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions appropriately
                throw ex;
            }
        }

        bool ICarService.DeleteCar(int id)
        {
            SqlConnection cnn = new SqlConnection(
                @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=CarRentalSystem;Integrated Security=True;Connect Timeout=30;Encrypt=False;");

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cnn;
            cmd.CommandText = "DELETE FROM [Car] WHERE Id = @id";

            cmd.Parameters.AddWithValue("@id", id);

            cnn.Open();
            int rowsAffected = cmd.ExecuteNonQuery();
            cnn.Close();

            return rowsAffected > 0;
        }

        string ICarService.GetCarName(int id)
        {
            string carName = "";
            SqlConnection cnn = new SqlConnection(
                @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=CarRentalSystem;Integrated Security=True;Connect Timeout=30;Encrypt=False;");

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cnn;
            cmd.CommandText = "SELECT car_model FROM [Car] WHERE Id = @id";
            cmd.Parameters.AddWithValue("@id", id);
            cnn.Open();
            // Execute the query to fetch the car name
            object result = cmd.ExecuteScalar();

            // Check if the result is not null
            if (result != null)
            {
                carName = result.ToString();
            }
            cnn.Close();

            return carName;
        }

        DataSet ICarService.GetCarsByModel(string model)
        {
            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=CarRentalSystem;Integrated Security=True;Connect Timeout=30;Encrypt=False;";
            DataSet dataSet = new DataSet();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = "SELECT * FROM [dbo].[Car] WHERE [car_model] LIKE '%' + @searchModel + '%'";
                command.Parameters.AddWithValue("@searchModel", model);

                SqlDataAdapter adapter = new SqlDataAdapter(command);

                try
                {
                    connection.Open();
                    adapter.Fill(dataSet, "cars");
                }
                catch (Exception ex)
                {
                    // Handle exceptions here (e.g., logging, throwing)
                    throw ex;
                }
            }

            return dataSet;
        }

        DataSet ICarService.GetCarsByFuelType(string fuelType)
        {
            // Implement your logic to fetch cars by fuel type from the database
            SqlConnection cnn = new SqlConnection(
                @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=CarRentalSystem;Integrated Security=True;Connect Timeout=30;Encrypt=False;");

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cnn;
            cmd.CommandText = "SELECT * FROM Car WHERE fuel_type = @fuelType";
            cmd.Parameters.AddWithValue("@fuelType", fuelType);

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataSet carsDataSet = new DataSet();

            try
            {
                cnn.Open();
                adapter.Fill(carsDataSet, "cars");
            }
            finally
            {
                cnn.Close();
            }

            return carsDataSet;
        }
    }
}