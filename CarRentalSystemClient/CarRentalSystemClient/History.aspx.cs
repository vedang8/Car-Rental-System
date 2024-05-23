using System;
using CarRentalSystemClient.UserServiceReference;
using CarRentalSystemClient.BookingServiceReference;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace CarRentalSystemClient
{
    public partial class History : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            User user = (User)Session["CurrentUser"];
            if (user == null)
            {
                Response.Redirect("Login.aspx");
            }
            Username.Text = user.firstname.ToUpper();
            if (!IsPostBack)
            {
                // Call the method to retrieve all bookings from your service
                // and bind the result to the repeater control
                BindHistoryData();
            }
        }

        private void BindHistoryData()
        {
            // Assuming you have an instance of your booking service
            // named bookingService declared elsewhere and initialized
            // to an appropriate implementation.
            User user = (User)Session["CurrentUser"];
            int userId = user.UserId;

            // Call the method to retrieve all bookings
            BookingServiceClient bookingService = new BookingServiceClient();
            Booking[] bookings = bookingService.GetBookingByUserId(userId);

            if (bookings != null && bookings.Length > 0)
            {
                // Create a DataTable to store the booking data
                DataTable bookingDataTable = new DataTable();
                bookingDataTable.Columns.Add("Id", typeof(int));
                bookingDataTable.Columns.Add("car_id", typeof(int));
                bookingDataTable.Columns.Add("user_id", typeof(int));
                bookingDataTable.Columns.Add("start_date", typeof(DateTime));
                bookingDataTable.Columns.Add("end_date", typeof(DateTime));
                bookingDataTable.Columns.Add("booking_date", typeof(DateTime));
                bookingDataTable.Columns.Add("price", typeof(int));
                bookingDataTable.Columns.Add("driver_required", typeof(bool));
                bookingDataTable.Columns.Add("car_model", typeof(string)); // Add column for car model

                foreach (Booking booking in bookings)
                {
                    DataRow newRow = bookingDataTable.NewRow();
                    newRow["Id"] = booking.BookingId;
                    newRow["car_id"] = booking.CarId;
                    newRow["user_id"] = booking.UserId;
                    newRow["start_date"] = booking.StartDate;
                    newRow["end_date"] = booking.EndDate;
                    newRow["booking_date"] = booking.BookingDate;
                    newRow["price"] = booking.Price;
                    newRow["driver_required"] = booking.DriverRequired;

                    // Fetch car name
                    CarServiceReference.CarServiceClient carService = new CarServiceReference.CarServiceClient();
                    string carName = carService.GetCarName(booking.CarId);
                    newRow["car_model"] = carName; // Add car name to the row

                    bookingDataTable.Rows.Add(newRow);
                }

                // Bind the DataTable to the repeater
                rptHistory.DataSource = bookingDataTable;
                rptHistory.DataBind();
            }
        }


        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Remove("CurrentUser");
            Response.Redirect("Login.aspx");
        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            Response.Redirect("History.aspx");
        }
    }
}