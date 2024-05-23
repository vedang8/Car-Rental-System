using System;
using CarRentalSystemClient.UserServiceReference;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace CarRentalSystemClient
{
    public partial class Customers : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindCustomersGrid();
            }
        }

        private void BindCustomersGrid()
        {
            // Retrieve customers from the database using your service layer
            UserServiceReference.UserServiceClient userServiceClient = new UserServiceReference.UserServiceClient();
            DataSet customersDataSet = userServiceClient.GetUser();

            // Bind the customers DataSet to the GridView
            gridCustomers.DataSource = customersDataSet.Tables["users"]; // Assuming "users" is the table name in the DataSet
            gridCustomers.DataBind();
        }

    }
}