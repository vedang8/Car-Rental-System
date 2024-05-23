using System;
using CarRentalSystemClient.CarServiceReference;
using CarRentalSystemClient.BookingServiceReference;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CarRentalSystemClient
{
    public partial class BookCar : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                calStartDate.SelectedDate = DateTime.Today;
                calEndDate.SelectedDate = DateTime.Today.AddDays(1);
                BindAvailableCars(DateTime.Today, DateTime.Today.AddDays(1));
            }
        }
        protected void calStartDate_SelectionChanged(object sender, EventArgs e)
        {
            BindAvailableCars(calStartDate.SelectedDate, calEndDate.SelectedDate);
        }

        protected void calEndDate_SelectionChanged(object sender, EventArgs e)
        {
            BindAvailableCars(calStartDate.SelectedDate, calEndDate.SelectedDate);
        }

        private void BindAvailableCars(DateTime startDate, DateTime endDate)
        {
            CarServiceReference.CarServiceClient carServiceClient = new CarServiceReference.CarServiceClient();
            DataSet dataSet = carServiceClient.GetCars();
            DataTable availableCars = dataSet.Tables["bookings"];
            gridAvailableCars.DataSource = availableCars;
            gridAvailableCars.DataBind();
        }

        protected void btnBook_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow row in gridAvailableCars.Rows)
            {
                RadioButton radioSelect = (RadioButton)row.FindControl("radioSelect");
                if (radioSelect.Checked)
                {
                    int carId = Convert.ToInt32(gridAvailableCars.DataKeys[row.RowIndex].Value);
                    int userId = GetLoggedInUserId(); // Implement this method to get the logged-in user ID
                    bool driverRequired = chkDriver.Checked;

                    DateTime startDate = calStartDate.SelectedDate;
                    DateTime endDate = calEndDate.SelectedDate;
                    int price = 1000;
                    Booking booking = new Booking
                    {
                        UserId = userId,
                        CarId = carId,
                        StartDate = startDate,
                        EndDate = endDate,
                        Price = price,
                        DriverRequired = driverRequired
                    };

                    BookingServiceReference.BookingServiceClient bookingService = new BookingServiceReference.BookingServiceClient();
                    bool bookingResult = bookingService.AddBooking(booking);

                    if (bookingResult)
                    {
                        // Booking successful, show confirmation message or redirect
                        Response.Write("Booking successful!");
                    }
                    else
                    {
                        // Booking failed, show error message
                        Response.Write("Booking failed!");
                    }

                    break; // Exit loop after booking the selected car
                }
            }
        }

        private int GetLoggedInUserId()
        {
            if (Session["UserId"] != null)
            {
                return Convert.ToInt32(Session["UserId"]);
            }
            else
            {
                // Handle the case when the user is not logged in or session is expired
                // You can redirect the user to the login page or take appropriate action
                // For now, we'll return a default value
                return -1; // Return a default value indicating no user ID
            }
        }
    }
}