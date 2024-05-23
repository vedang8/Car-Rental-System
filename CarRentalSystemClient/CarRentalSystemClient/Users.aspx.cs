using System;
using CarRentalSystemClient.UserServiceReference;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CarRentalSystemClient
{
    public partial class Users : System.Web.UI.Page
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
                BindUsers();
            }
        }
        private void BindUsers()
        {
            // Call the ICarService.GetCars() method to retrieve cars
            UserServiceReference.UserServiceClient userService = new UserServiceReference.UserServiceClient();
            DataSet usersDataSet = userService.GetUser();

            if (usersDataSet != null && usersDataSet.Tables["users"].Rows.Count > 0)
            {
                rptUsers.DataSource = usersDataSet.Tables["users"];
                rptUsers.DataBind();

            }
            else
            {

            }
        }

        protected void BtnDeleteUser_Click(object sender, EventArgs e)
        {
            // Get the button that triggered the event
            Button btnDelete = (Button)sender;

            // Get the corresponding car ID
            int userId = Convert.ToInt32(btnDelete.CommandArgument);
            UserServiceClient userService = new UserServiceClient();
            bool userDeleted = userService.RemoveUser(userId);

            if (userDeleted)
            {
                // Reload questions after deletion
                Response.Redirect("Users.aspx");
            }
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("Login.aspx");
        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            Response.Redirect("Users.aspx");
        }
    }
}