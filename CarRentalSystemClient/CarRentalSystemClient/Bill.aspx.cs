using System;
using CarRentalSystemClient.UserServiceReference;
using CarRentalSystemClient.BookingServiceReference;
using CarRentalSystemClient.CarServiceReference;
using System.Web.UI;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;

namespace CarRentalSystemClient
{
    public partial class Bill : Page
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
                PopulateBookingDetails();
                PopulateUserInfo();
            }
        }

        private void PopulateBookingDetails()
        {
            // Retrieve the booking ID from the query string
            if (Request.QueryString["bookingId"] != null)
            {
                int bookingId;
                if (int.TryParse(Request.QueryString["bookingId"], out bookingId))
                {
                    // Call the booking service to get the booking details
                    BookingServiceClient bookingService = new BookingServiceClient();
                    Booking booking = bookingService.GetBookingById(bookingId);

                    if (booking != null)
                    {
                        // Populate the booking details on the page
                        lblBookingId.Text = bookingId.ToString();
                        lblBookingDate.Text = DateTime.Now.ToString("MMMM dd, yyyy");
                        lblRentalPeriod.Text = $"{booking.StartDate.ToString("MMMM dd, yyyy")} - {booking.EndDate.ToString("MMMM dd, yyyy")}";
                        lblTotalCost.Text =   booking.Price.ToString("C");

                        Session["BookingId"] = lblBookingId.Text;
                        Session["BookingDate"] = lblBookingDate.Text;
                        Session["RentalPeriod"] = lblRentalPeriod.Text;
                        Session["TotalCost"] = lblTotalCost.Text;
                        // Get car details
                        CarServiceClient carService = new CarServiceClient();
                        Car car = carService.GetCar(booking.CarId);
                        if (car != null)
                        {
                            lblCarModel.Text = car.CarModel;
                            lblNumberPlate.Text = car.NumberPlate;
                            lblRentalCarPrice.Text =   "₹"+ car.RentPrice.ToString();
                            lblFuelType.Text = car.FuelType;
                            lblCapacity.Text = car.Capacity.ToString();
                            lblDriverCost.Text = "₹" + car.DriverCost.ToString();
                            lblDriverRequired.Text = booking.DriverRequired ? "Yes" : "No";

                            Session["CarModel"] = lblCarModel.Text;
                            Session["NumPlate"] = lblNumberPlate.Text;
                            Session["RentalCarPrice"] = lblRentalCarPrice.Text;
                            Session["FuelType"] = lblFuelType.Text;
                            Session["Capacity"] = lblCapacity.Text;
                            Session["DriverCost"] = lblDriverCost.Text;
                            Session["DriverReq"] = lblDriverRequired.Text;
                        }
                    }
                    else
                    {
                        // Redirect to the booking page if booking details are not found
                        Response.Redirect("BookCar.aspx");
                    }
                }
            }
        }

        private void PopulateUserInfo()
        {
            // Get user information
            UserServiceReference.User user = (UserServiceReference.User)Session["CurrentUser"]; // Assuming this method exists to retrieve the current user
            if (user != null)
            {
                lblUserName.Text = user.firstname.ToUpper() + " " + user.lastname.ToUpper();
                Session["UserName"] = lblUserName.Text;
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }

        protected void btnDownload_Click(object sender, EventArgs e)
        {
            // Create the PDF document
            Document document = new Document(PageSize.A4, 50, 50, 25, 25);

            // Define a memory stream to hold the generated PDF
            MemoryStream memoryStream = new MemoryStream();

            try
            {
                // Create a PdfWriter to write the document to the memory stream
                PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);

                // Open the document for writing
                document.Open();

                // Add Car Rental System heading
                Paragraph heading = new Paragraph("Car Rental System", new Font(Font.FontFamily.HELVETICA, 24, Font.BOLD));
                heading.Alignment = Element.ALIGN_CENTER;
                heading.SpacingAfter = 20f;
                document.Add(heading);

                // Add user information
                Paragraph userInfo = new Paragraph("User Information", new Font(Font.FontFamily.HELVETICA, 16, Font.BOLD));
                userInfo.SpacingAfter = 10f;
                document.Add(userInfo);
                document.Add(GetUserDetails());

                // Add booking details
                Paragraph bookingDetails = new Paragraph("Booking Details", new Font(Font.FontFamily.HELVETICA, 16, Font.BOLD));
                bookingDetails.SpacingBefore = 20f;
                bookingDetails.SpacingAfter = 10f;
                document.Add(bookingDetails);
                document.Add(GetBookingDetails());

                // Add a thank-you footer
                Paragraph footer = new Paragraph("Thank you for choosing Car Rental System.", new Font(Font.FontFamily.HELVETICA, 10, Font.NORMAL));
                footer.Alignment = Element.ALIGN_CENTER;
                footer.SpacingBefore = 50f;
                document.Add(footer);
            }
            finally
            {
                // Close the document
                document.Close();
            }

            // Send the generated PDF file to the client for download
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=bill.pdf");
            Response.Buffer = true;
            Response.Clear();
            Response.OutputStream.Write(memoryStream.GetBuffer(), 0, memoryStream.GetBuffer().Length);
            Response.OutputStream.Flush();
            Response.End();
        }

        private PdfPTable GetUserDetails()
        {
            PdfPTable table = new PdfPTable(2);
            table.WidthPercentage = 100;
            table.SpacingAfter = 20f;

            UserServiceReference.User user = (UserServiceReference.User)Session["CurrentUser"];
            if (user != null)
            {
                table.AddCell(GetCell("User Name:", 1, 0, Font.BOLD));
                table.AddCell(GetCell(user.firstname.ToUpper() + " " + user.lastname.ToUpper(), 1, 0));
                
                table.AddCell(GetCell("Address:", 1, 0, Font.BOLD));
                table.AddCell(GetCell(user.address.ToUpper(), 1, 0));
            }

            return table;
        }

        private PdfPTable GetBookingDetails()
        {
            PdfPTable table = new PdfPTable(2);
            table.WidthPercentage = 100;
            table.SpacingAfter = 20f;

            string carModel = Session["CarModel"] != null ? Session["CarModel"].ToString() : "N/A";
            table.AddCell(GetCell("Car Model:", 1, 0, Font.BOLD));
            table.AddCell(GetCell(carModel, 1, 0));

            table.AddCell(GetCell("Number Plate:", 1, 0, Font.BOLD));
            table.AddCell(GetCell(Session["NumPlate"].ToString(), 1, 0));
            table.AddCell(GetCell("Rent of Car:", 1, 0, Font.BOLD));
            table.AddCell(GetCell(Session["RentalCarPrice"].ToString(), 1, 0));
            table.AddCell(GetCell("Fuel Type:", 1, 0, Font.BOLD));
            table.AddCell(GetCell(Session["FuelType"].ToString(), 1, 0));
            table.AddCell(GetCell("Capacity:", 1, 0, Font.BOLD));
            table.AddCell(GetCell(Session["Capacity"].ToString(), 1, 0));
            table.AddCell(GetCell("Driver Cost:", 1, 0, Font.BOLD));
            table.AddCell(GetCell(Session["DriverCost"].ToString(), 1, 0));
            table.AddCell(GetCell("Driver Required:", 1, 0, Font.BOLD));
            table.AddCell(GetCell(Session["DriverReq"].ToString(), 1, 0));
            table.AddCell(GetCell("Total Cost:", 1, 0, Font.BOLD));
            table.AddCell(GetCell(Session["TotalCost"].ToString(), 1, 0));

            return table;
        }

        private PdfPCell GetCell(string text, int colspan, int rowspan, int fontStyle = Font.NORMAL)
        {
            PdfPCell cell = new PdfPCell(new Phrase(text, new Font(Font.FontFamily.HELVETICA, 10, fontStyle)));
            cell.Colspan = colspan;
            cell.Rowspan = rowspan;
            return cell;
        }


        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("Login.aspx");
        }
    }
}
