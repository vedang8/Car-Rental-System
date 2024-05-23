using CarRentalSystemClient.BookingServiceReference;
using CarRentalSystemClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CarRentalSystemClient
{
    public partial class AllBookings : System.Web.UI.Page
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
                // Call the method to retrieve all bookings from your service
                // and bind the result to the repeater control
                BindBookingData();
            }
        }

        private void BindBookingData()
        {
            // Assuming you have an instance of your booking service
            // named bookingService declared elsewhere and initialized
            // to an appropriate implementation.

            // Call the method to retrieve all bookings
            BookingServiceClient bookingService = new BookingServiceClient();
            DataSet bookingDataSet = bookingService.GetAllBooking();

            // Check if the DataSet contains data
            if (bookingDataSet != null && bookingDataSet.Tables.Count > 0)
            {
                // Ensure that the DataTable has the column "FirstName"
                if (!bookingDataSet.Tables[0].Columns.Contains("FirstName"))
                {
                    bookingDataSet.Tables[0].Columns.Add("FirstName");
                }

                if (!bookingDataSet.Tables[0].Columns.Contains("car_model"))
                {
                    bookingDataSet.Tables[0].Columns.Add("car_model");
                }

                // Loop through each row in the DataTable
                foreach (DataRow row in bookingDataSet.Tables[0].Rows)
                {
                    // Fetch user name
                    int userId = Convert.ToInt32(row["user_id"]);
                    UserServiceReference.UserServiceClient userService = new UserServiceReference.UserServiceClient();
                    string userName = userService.GetUserName(userId);

                    // Fetch car name
                    int carId = Convert.ToInt32(row["car_id"]);
                    CarServiceReference.CarServiceClient carService = new  CarServiceReference.CarServiceClient();
                    string carName = carService.GetCarName(carId);

                    // Update DataRow with user name and car name
                    row["FirstName"] = userName;
                    row["car_model"] = carName;
                }

                // Bind the modified DataTable to the repeater
                rptBookings.DataSource = bookingDataSet.Tables[0];
                rptBookings.DataBind();
            }
        }

        

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("Login.aspx");
        }

        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            string searchTerm = inputTextBox.Text.Trim();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                // Call the ICarService.GetCarsByModel() method to retrieve cars matching the search term
                CarServiceReference.CarServiceClient carService = new CarServiceReference.CarServiceClient();
                DataSet carsDataSet = carService.GetCarsByModel(searchTerm);

                if (carsDataSet != null && carsDataSet.Tables["cars"].Rows.Count > 0)
                {
                    rptBookings.DataSource = carsDataSet.Tables["cars"];
                    rptBookings.DataBind();
                }
                else
                {
                    // Optionally, show a message indicating no matching cars found
                    rptBookings.DataSource = null;
                    rptBookings.DataBind();
                }
            }
            else
            {
                // If search term is empty, bind all cars
                BindBookingData();
            }
        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            Response.Redirect("AllBookings.aspx");
        }
    }
}