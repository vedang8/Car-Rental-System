using System;
using System.Data;
using System.Web.UI.WebControls;
using CarRentalSystemClient.CarServiceReference;

namespace CarRentalSystemClient
{
    public partial class Cars : System.Web.UI.Page
    {
        private CarServiceClient carService = new CarServiceClient();

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

        protected void BtnFilter_Click(object sender, EventArgs e)
        {
            string selectedFuelType = ddlFuelTypeFilter.SelectedValue;

            if (string.IsNullOrEmpty(selectedFuelType))
            {
                lblMessage.Text = "Please select a fuel type to apply filter.";
                lblMessage.Visible = true;
                return;
            }

            DataSet carsDataSet = carService.GetCarsByFuelType(selectedFuelType);

            if (carsDataSet.Tables["cars"].Rows.Count == 0)
            {
                lblMessage.Text = "No cars found with fuel type " + selectedFuelType + ".";
                lblMessage.Visible = true;
                rptCars.DataSource = carsDataSet;
                rptCars.DataBind();
            }
            else
            {
                lblMessage.Visible = false;
                rptCars.DataSource = carsDataSet;
                rptCars.DataBind();
            }
        }

        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            string searchModel = inputTextBox.Text.Trim();
            DataSet carsDataSet = carService.GetCarsByModel(searchModel);

            if (carsDataSet.Tables["cars"].Rows.Count == 0)
            {
                lblMessage.Text = "No cars found with model " + searchModel + ".";
                lblMessage.Visible = true;
                rptCars.DataSource = carsDataSet;
                rptCars.DataBind();
            }
            else
            {
                lblMessage.Visible = false;
                rptCars.DataSource = carsDataSet;
                rptCars.DataBind();
            }
        }

        protected void BtnAddCar_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddCar.aspx");
        }

        protected void BtnViewCar_Click(object sender, EventArgs e)
        {
            Button btnView = (Button)sender;
            int carId = Convert.ToInt32(btnView.CommandArgument);
            // Redirect to the view page with the car ID as a parameter
            Response.Redirect("ViewCar.aspx?CarId=" + carId);
        }

        protected void BtnEditCar_Click(object sender, EventArgs e)
        {
            Button btnView = (Button)sender;
            int carId = Convert.ToInt32(btnView.CommandArgument);
            // Redirect to the view page with the car ID as a parameter
            Response.Redirect("EditCar.aspx?CarId=" + carId);
        }

        protected void BtnDeleteCar_Click(object sender, EventArgs e)
        {
            Button btnDelete = (Button)sender;
            int carId = Convert.ToInt32(btnDelete.CommandArgument);
            bool isDeleted = carService.DeleteCar(carId);

            if (isDeleted)
            {
                lblMessage.Text = "Car deleted successfully.";
                lblMessage.Visible = true;
                BindCars(); // Refresh the car list after deletion
            }
            else
            {
                lblMessage.Text = "Failed to delete car. Please try again.";
                lblMessage.Visible = true;
            }
        }

        private void BindCars()
        {
            DataSet carsDataSet = carService.GetCars();
            rptCars.DataSource = carsDataSet;
            rptCars.DataBind();
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("Login.aspx");
        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            Response.Redirect("Cars.aspx");
        }
    }
}

