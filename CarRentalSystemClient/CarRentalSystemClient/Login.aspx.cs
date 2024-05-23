using CarRentalSystemClient.UserServiceReference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CarRentalSystemClient
{
    public partial class Login : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text;
            string password = txtPassword.Text;

            UserServiceClient userService = new UserServiceClient();
            User user = userService.GetUserByEmail(email);

            if (user == null)
            {
                lblMessage.Text = "User not found";
                lblMessage.Visible = true;
            }
            else
            {
                // Check if the user object and password are not null before accessing them
                if (user.password != null && password.Trim() == user.password.Trim())
                {
                    if (user.isadmin == false)
                    {
                        Session["CurrentUser"] = user;
                        Response.Redirect("~/BookCar.aspx");
                    }
                    else if (user.isadmin == true)
                    {
                        Session["CurrentUser"] = user;
                        Response.Redirect("~/Cars.aspx");
                    }
                }
                else
                {
                    lblMessage.Text = "Invalid Credentials!";
                    lblMessage.Visible = true;
                }
            }
            ClearTextBoxes(this);
        }

        private void ClearTextBoxes(Control parent)
        {
            foreach (Control control in parent.Controls)
            {
                if (control is TextBox)
                {
                    ((TextBox)control).Text = string.Empty;
                }
                else if (control.HasControls())
                {
                    ClearTextBoxes(control);
                }
            }
        }
    }
}