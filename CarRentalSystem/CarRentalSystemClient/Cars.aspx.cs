using System;
using CarRentalSystemClient.CarServiceReference;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace CarRentalSystemClient
{
    public partial class Cars : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Bind cars list on initial load
                BindCarsList();
            }
        }

        protected void btnAddCar_Click(object sender, EventArgs e)
        {
            
            string car_model = txtModel.Text;
            string np = txtNumberPlate.Text;
            string ft = txtFuelType.Text;   
            int dc = Convert.ToInt32(txtDriverCost.Text);
            int rental_price = Convert.ToInt32(txtRentalPrice.Text);
            int capacity = Convert.ToInt32(txtCapacity.Text);
            int mileage = Convert.ToInt32(txtMileage.Text);
            // Create a new Car object
            Car newCar = new Car
            {
                CarModel = car_model,
                NumberPlate = np,
                RentPrice = rental_price,
                Capacity = capacity,
                Mileage = mileage,
                FuelType = ft,
                DriverCost = dc,
            };

            // Call the AddCar method to add the new car
            AddCar(newCar);

            // Refresh the cars list after adding a new car
            BindCarsList();
        }
        private void BindCarsList()
        {
            // Call a method to retrieve the list of cars from the database
            CarServiceClient carServiceClient = new CarServiceClient();
            DataSet dataSet = carServiceClient.GetCars();

            List<Car> cars = new List<Car>();
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                Car car = new Car
                {
                    CarModel = row["car_model"].ToString(),
                    NumberPlate = row["car_number_plate"].ToString(),
                    RentPrice = Convert.ToInt32(row["rental_price"]),
                    Capacity = Convert.ToInt32(row["capacity"]),
                    FuelType = row["fuel_type"].ToString(),
                    Mileage = Convert.ToInt32(row["mileage"]),
                    DriverCost = Convert.ToInt32(row["driver_cost"])
                };
                cars.Add(car);
            }

            // Bind the cars list to the GridView
            gridCars.DataSource = cars;
            gridCars.DataBind();
        }

        private void AddCar(Car car)
        {
            // Call a method to add the car to the database
            CarServiceClient carServiceClient = new CarServiceClient();
            carServiceClient.AddCar(car);
        }

        /*
        protected void btnUpdateCar_Click(object sender, EventArgs e)
        {
            // Check if a car is selected
            if (gridCars.SelectedIndex >= 0)
            {
                int carId = Convert.ToInt32(gridCars.SelectedDataKey.Value);
                string make = txtMake.Text;
                string model = txtModel.Text;
                int year = Convert.ToInt32(txtYear.Text);

                // Create a new Car object
                Car updatedCar = new Car
                {
                    Id = carId,
                    Make = make,
                    Model = model,
                    Year = year
                };

                // Call the UpdateCar method to update the selected car
                UpdateCar(updatedCar);

                // Refresh the cars list after updating the car
                BindCarsList();
            }
            else
            {
                lblOperationMessage.Text = "Please select a car to update.";
            }
        }  */

       /* private void UpdateCar(Car car)
        {
            // Call a method to update the car in the database
            CarServiceClient carServiceClient = new CarServiceClient();
            carServiceClient.UpdateCar(car);
        } */
        protected void btnDeleteCar_Click(object sender, EventArgs e)
        {
            // Check if a car is selected
            if (gridCars.SelectedIndex >= 0)
            {
                int carId = Convert.ToInt32(gridCars.SelectedDataKey.Value);

                // Call the DeleteCar method to delete the selected car
                DeleteCar(carId);

                // Refresh the cars list after deleting the car
                BindCarsList();
            }
            else
            {
                lblOperationMessage.Text = "Please select a car to delete.";
            }
        }
        private void DeleteCar(int carId)
        {
            // Call a method to delete the car from the database
            CarServiceClient carServiceClient = new CarServiceClient();
            carServiceClient.DeleteCar(carId);
        }
    }
}