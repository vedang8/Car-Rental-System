using CarRentalSystemClient.CarServiceReference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CarRentalSystemClient
{
    public partial class EditCar : System.Web.UI.Page
    {
        int carId;
        protected void Page_Load(object sender, EventArgs e)
        {
            UserServiceReference.User user = (UserServiceReference.User)Session["CurrentUser"];
            if (user == null)
            {
                Response.Redirect("Login.aspx");
            }
            Username.Text = user.firstname.ToUpper();
            if (!IsPostBack)
            {
                // Retrieve car ID from URL parameter
                if (Request.QueryString["CarId"] != null)
                {
                    carId = Convert.ToInt32(Request.QueryString["CarId"]);

                    // Fetch car details from the database using ICarService.GetCar method
                    CarServiceClient carService = new CarServiceClient(); // Initialize your service
                    Car car = carService.GetCar(carId);

                    // Populate UI with car details
                    txtModel.Text = car.CarModel;
                    txtNumberPlate.Text = car.NumberPlate;
                    txtRentPrice.Text = car.RentPrice.ToString();
                    txtFuelType.Text = car.FuelType;
                    txtCapacity.Text = car.Capacity.ToString();
                    txtMileage.Text = car.Mileage.ToString();
                    txtDriverCost.Text = car.DriverCost.ToString();
                }
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                // Retrieve car ID from the query string
                int carId = Convert.ToInt32(Request.QueryString["CarId"]);

                // Retrieve updated car details from the TextBoxes
                string model = txtModel.Text;
                string numberPlate = txtNumberPlate.Text;
                int rentPrice = Convert.ToInt32(txtRentPrice.Text);
                int capacity = Convert.ToInt32(txtCapacity.Text);
                string fuelType = txtFuelType.Text;
                int mileage = Convert.ToInt32(txtMileage.Text);
                int driverCost = Convert.ToInt32(txtDriverCost.Text);

                // Create a Car object with the updated details
                Car updatedCar = new Car
                {
                    CarId = carId,
                    CarModel = model,
                    NumberPlate = numberPlate,
                    RentPrice = rentPrice,
                    Capacity = capacity,
                    FuelType = fuelType,
                    Mileage = mileage,
                    DriverCost = driverCost
                };

                CarServiceClient carService = new CarServiceClient();

                // Call the service method to edit the car
                bool editSuccess = carService.EditCar(updatedCar);

                if (editSuccess)
                {
                    // Redirect to the view car page with the updated car ID
                    Response.Redirect("Cars.aspx");
                }
                else
                {
                    lblMessage.Text = "Failed to update car details. Please try again.";
                    lblMessage.Visible = true;
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions
                lblMessage.Text = "An error occurred: " + ex.Message;
                lblMessage.Visible = true;
            }
        }


        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("Login.aspx");
        }
    }
}