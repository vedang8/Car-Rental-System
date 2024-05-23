using CarRentalSystemClient.CarServiceReference;
using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CarRentalSystemClient
{
    public partial class AddCar : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            UserServiceReference.User user = (UserServiceReference.User)Session["CurrentUser"];
            if (user == null)
            {
                Response.Redirect("Login.aspx");
            }
            Username.Text = user.firstname.ToUpper();

        }

        protected void btnSaveCar_Click(object sender, EventArgs e)
        {
            string model = txtModel.Text.Trim();
            string numberPlate = txtNumberPlate.Text;
            int rentalPrice = Convert.ToInt32(txtRentalPrice.Text);
            int capacity = Convert.ToInt32(txtCapacity.Text);
            string fuelType = txtFuelType.Value;
            int mileage = Convert.ToInt32(txtMileage.Text);
            int driverCost = Convert.ToInt32(txtDriverCost.Text);

            // Check if an image file has been uploaded
            if (fileImage.HasFile)
            {
                // Create a new Cloudinary instance with your Cloudinary credentials
                var cloudinary = new CloudinaryDotNet.Cloudinary(new Account(
                    ConfigurationManager.AppSettings["Cloudinary:CloudName"],
                    ConfigurationManager.AppSettings["Cloudinary:ApiKey"],
                    ConfigurationManager.AppSettings["Cloudinary:ApiSecret"]));

                try
                {
                    // Upload image to Cloudinary
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(fileImage.FileName, fileImage.PostedFile.InputStream)
                    };

                    var uploadResult = cloudinary.Upload(uploadParams);

                    // Create a new Car object with image URL
                    Car newCar = new Car
                    {
                        CarModel = model,
                        NumberPlate = numberPlate,
                        RentPrice = rentalPrice,
                        Capacity = capacity,
                        FuelType = fuelType,
                        Mileage = mileage,
                        DriverCost = driverCost,
                        Image = uploadResult.Url.ToString()
                    };

                    // Call the AddCar method of the CarServiceClient
                    CarServiceClient carService = new CarServiceClient();
                    bool carAdded = carService.AddCar(newCar);

                    if (carAdded)
                    {
                        lblAddCarMessage.Text = "Car added successfully.";
                    }
                    else
                    {
                        lblAddCarMessage.Text = "Failed to add car. Please try again.";
                    }
                }
                catch (Exception ex)
                {
                    lblAddCarMessage.Text = "Error uploading image: " + ex.Message;
                }
            }
            else
            {
                lblAddCarMessage.Text = "Please select an image file.";
            }
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("Login.aspx");
        }

        protected void BtnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Cars.aspx");
        }
    }
}