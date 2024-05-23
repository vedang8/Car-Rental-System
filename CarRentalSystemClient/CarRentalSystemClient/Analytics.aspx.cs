using CarRentalSystemClient.BookingServiceReference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.DataVisualization.Charting;
using System.Data;

namespace CarRentalSystemClient
{
    public partial class Analytics : System.Web.UI.Page
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
                BindBookingsChart();
            }     
        }

        protected void BindBookingsChart()
        {
            // Call your service method to retrieve booking data
            BookingServiceClient bookingService = new BookingServiceClient();
            DataSet bookingDataSet = bookingService.GetAllBooking();

            // Convert DataSet to list of Booking objects
            List<Booking> bookings = ConvertDataSetToBookingsList(bookingDataSet);

            // Now you have a list of Booking objects that you can use to populate your chart
            // Implement chart binding logic here
            // Prepare data for chart
            string[] months = { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
            int[] bookingsCountByMonth = new int[12];

            // Iterate through booking data and update bookings count for corresponding month
            foreach (Booking booking in bookings)
            {
                int monthIndex = booking.StartDate.Month - 1; // Get month index (0-based) from the start date of booking
                bookingsCountByMonth[monthIndex]++;
            }

            // Bind data to the chart
            bookingsChart.Series["Bookings"].Points.DataBindXY(months, bookingsCountByMonth);
        }

        protected List<Booking> ConvertDataSetToBookingsList(DataSet bookingDataSet)
        {
            List<Booking> bookings = new List<Booking>();

            // Check if the DataSet contains data
            if (bookingDataSet != null && bookingDataSet.Tables.Count > 0)
            {
                // Iterate through each row in the DataTable
                foreach (DataRow row in bookingDataSet.Tables[0].Rows)
                {
                    // Create a new Booking object and populate its properties
                    Booking booking = new Booking
                    {
                        // Populate booking properties from DataRow
                        // Adjust property names according to your actual data structure
                        BookingId = Convert.ToInt32(row["Id"]),
                        UserId = Convert.ToInt32(row["user_id"]),
                        CarId = Convert.ToInt32(row["car_id"]),
                        StartDate = Convert.ToDateTime(row["start_date"]),
                        EndDate = Convert.ToDateTime(row["end_date"]),
                        BookingDate = Convert.ToDateTime(row["booking_date"]),
                        Price = Convert.ToInt32(row["price"]),
                        DriverRequired = Convert.ToBoolean(row["driver_required"])
                    };

                    // Add the booking object to the list
                    bookings.Add(booking);
                }
            }

            return bookings;
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Remove("CurrentUser");
            Response.Redirect("Login.aspx");
        }
    }
}