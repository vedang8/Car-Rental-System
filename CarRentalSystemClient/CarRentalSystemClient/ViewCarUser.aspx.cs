using CarRentalSystemClient.CarServiceReference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CarRentalSystemClient
{
    public partial class ViewCarUser : System.Web.UI.Page
    {
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
                    int carId = Convert.ToInt32(Request.QueryString["CarId"]);

                    // Fetch car details from the database using ICarService.GetCar method
                    CarServiceClient carService = new CarServiceClient(); // Initialize your service
                    Car car = carService.GetCar(carId);

                    // Populate UI with car details
                    lblModel.Text = car.CarModel;
                    lblNumberPlate.Text = car.NumberPlate;
                    lblRentPrice.Text = car.RentPrice.ToString();
                    lblFuelType.Text = car.FuelType;
                    lblCapacity.Text = car.Capacity.ToString();
                    lblMileage.Text = car.Mileage.ToString();
                    lblDriverCost.Text = car.DriverCost.ToString();
                }
            }
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("Login.aspx");
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("BookCar.aspx");
        }
    }
}