using CarRentalSystemClient.UserServiceReference;
using System;
using System.Collections.Generic;
using System.Data;
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

        protected void Button1_Click(object sender, EventArgs e)
        {
            string email = TextBox1.Text;
            string password = TextBox2.Text; 
            
            User user = CheckCredentials(email, password);

            if (user != null)
            {
                // Store user information in session
                Session["UserId"] = user.UserId;

                // Redirect the user based on isAdmin status
                if (user.isadmin)
                {
                    Response.Redirect("Admin.aspx");
                }
                else
                {
                    Response.Redirect("Home.aspx");
                }
            }
            else
            {
                lblMessage.Text = "Invalid username or password.";
            }
        }

        private User CheckCredentials(string email, string password)
        {
            // Call the service method to verify user credentials
            UserServiceReference.UserServiceClient userServiceClient = new UserServiceReference.UserServiceClient();
            return userServiceClient.GetUserByEmail(email); 
        }

    }
}