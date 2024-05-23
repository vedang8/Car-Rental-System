using CarRentalSystemClient.UserServiceReference;
using CarRentalSystemClient.BookingServiceReference;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;
using System.Net;

namespace CarRentalSystemClient
{
    public partial class BookCar : System.Web.UI.Page
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
                
                BindCars();

            }
            
        }
        
        private void BindCars()
        {
            // Call the ICarService.GetCars() method to retrieve cars
            CarServiceReference.CarServiceClient carService = new CarServiceReference.CarServiceClient();
            DataSet carsDataSet = carService.GetCars();

            if (carsDataSet != null && carsDataSet.Tables["cars"].Rows.Count > 0)
            {
                rptCars.DataSource = carsDataSet.Tables["cars"];
                rptCars.DataBind();

            }
            else
            {
                lblMessage.Text = "There are no Cars in the list!";
            }
        }

        protected void btnFindCars_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime startDate = DateTime.ParseExact(txtStartDate.Text, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                DateTime endDate = DateTime.ParseExact(txtEndDate.Text, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                bool driverRequired = chkDriverRequired.Checked;

                // Find available cars for the specified dates
                BookingServiceReference.BookingServiceClient bookingService = new BookingServiceReference.BookingServiceClient();

                // Retrieve the dataset from the service
                DataSet availableCarsDataSet = bookingService.FindAvailableCars(startDate, endDate);

                // Check if the dataset has any tables and rows
                if (availableCarsDataSet != null && availableCarsDataSet.Tables.Count > 0 && availableCarsDataSet.Tables[0].Rows.Count > 0)
                {
                    // Bind the dataset to the repeater control
                    rptCars.DataSource = availableCarsDataSet.Tables[0];
                    rptCars.DataBind();
                    lblMessage.Text = ""; // Clear any previous messages
                }
                else
                {
                    lblMessage.Text = "Sorry, no cars are available for the selected dates.";
                }
            }
            catch (FormatException)
            {
                // Handle the format exception, indicating that the date format is incorrect
                lblMessage.Text = "Please enter valid dates in the format YYYY-MM-DD.";
            }
        }

        protected void BtnViewCar_Click(object sender, EventArgs e)
        {
            Button btnView = (Button)sender;
            int carId = Convert.ToInt32(btnView.CommandArgument);
            // Redirect to the view page with the car ID as a parameter
            Response.Redirect("ViewCarUser.aspx?CarId=" + carId);
        }

        protected void BtnBook_Click(object sender, EventArgs e)
        {
            User user = (User)Session["CurrentUser"];
            int userId = user.UserId;
            int carId = Convert.ToInt32(((Button)sender).CommandArgument);
            DateTime startDate = DateTime.Parse(txtStartDate.Text);
            DateTime endDate = DateTime.Parse(txtEndDate.Text);
            bool driverRequired = chkDriverRequired.Checked;
            Booking booking = new Booking
            {
                UserId = userId, // Assuming you have the userId available
                CarId = carId,
                StartDate = startDate,
                EndDate = endDate,
                DriverRequired = driverRequired
            };
            
            // Book the selected car
            BookingServiceClient bookingService = new BookingServiceClient();
            int bookingId = bookingService.AddBooking(booking);

            if (bookingId > 0)
            {
                lblBookingMessage.Text = "Car is booked successfully!";
                btnGenearateBill.Visible = true;
                ClearFields();
                Session["BookingId"] = bookingId;
               // string userEmail = "vedang2607@gmail.com";
                string name = user.firstname.ToUpper();
                //SendBookingConfirmationEmail(userEmail, name);
            }
            else
            {
                lblMessage.Text = "Booking failed. Please try again.";
            }
        }

        private void ClearFields()
        {
            txtStartDate.Text = "";
            txtEndDate.Text = "";
            chkDriverRequired.Checked = false;
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Remove("CurrentUser");
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
                    rptCars.DataSource = carsDataSet.Tables["cars"];
                    rptCars.DataBind();
                }
                else
                {
                    lblMessage.Text = "There are no such cars!";
                    // Optionally, show a message indicating no matching cars found
                    rptCars.DataSource = null;
                    rptCars.DataBind();
                }
            }
            else
            {
                // If search term is empty, bind all cars
                BindCars();
            }
        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            Response.Redirect("BookCar.aspx");
        }

        protected void btnGenerateBill_Click(object sender, EventArgs e)
        {
            int bookingId = (int)Session["BookingId"]; // Retrieve the booking ID from the session
                                                       // Redirect to the billing page with the booking ID as a query parameter
            Response.Redirect($"Bill.aspx?bookingId={bookingId}");
        }
         
        protected void SendBookingConfirmationEmail(string userEmail, string userName)
    {
        try
        {
            // Set the sender's email address and display name
            string senderEmail = "vedang2607@gmail.com";
            string senderName = "Car Rental System";

            // Set the recipient's email address
            string recipientEmail = userEmail;

            // Set the subject of the email
            string subject = "Booking Confirmation - Car Rental System";

            // Construct the HTML body of the email
            string body = @"
            <!DOCTYPE html>
            <html lang='en'>
            <head>
                <meta charset='UTF-8'>
                <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                <title>Booking Confirmation - Car Rental System</title>
                <style>
                    body {
                        font-family: Arial, sans-serif;
                        background-color: #f0f0f0;
                        margin: 0;
                        padding: 0;
                    }
                    .container {
                        max-width: 600px;
                        margin: 20px auto;
                        background-color: #fff;
                        padding: 20px;
                        border-radius: 5px;
                        box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
                    }
                    h1 {
                        color: #333;
                        text-align: center;
                    }
                    p {
                        color: #555;
                        line-height: 1.6;
                    }
                    .footer {
                        text-align: center;
                        margin-top: 30px;
                        color: #777;
                    }
                </style>
            </head>
            <body>
                <div class='container'>
                    <h1>Booking Confirmation - Car Rental System</h1>
                    <p>Dear " + userName + @",</p>
                    <p>Your booking with Car Rental System has been confirmed.</p>
                    <p>Thank you for choosing our services.</p>
                    <p>Best regards,<br>Car Rental System</p>
                </div>
                <div class='footer'>
                    <p>Thank you for choosing Car Rental System.</p>
                </div>
            </body>
            </html>
        ";

            // Create a new MailMessage instance
            MailMessage message = new MailMessage();
            message.From = new MailAddress(senderEmail, senderName);
            message.To.Add(recipientEmail);
            message.Subject = subject;

            // Create the HTML view for the email
            AlternateView htmlView = AlternateView.CreateAlternateViewFromString(body, null, "text/html");
            message.AlternateViews.Add(htmlView);

            // Create a new SmtpClient instance
            SmtpClient smtpClient = new SmtpClient("smtp.example.com");
            smtpClient.Port = 587; // Set the SMTP port (usually 587 for TLS/STARTTLS)
            smtpClient.Credentials = new NetworkCredential("vedang2607@gmail.com", "riiiwuklihrxljuw"); // Set your SMTP credentials
            smtpClient.EnableSsl = true; // Enable SSL/TLS encryption

            // Send the email
            smtpClient.Send(message);
        }
        catch (Exception ex)
        {
                Response.Write(ex);
        }
    }

    }
}