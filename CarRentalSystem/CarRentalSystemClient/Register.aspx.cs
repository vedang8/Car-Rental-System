using CarRentalSystemClient.UserServiceReference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CarRentalSystemClient
{
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            string firstName = txtFirstName.Text;
            string lastName = txtLastName.Text;
            string email = txtEmail.Text;
            string phone = txtPhone.Text;
            string address = txtAddress.Text;
            string password = txtPassword.Text;
            bool isAdmin = chkIsAdmin.Checked;

            // Create a new User object
            User newUser = new User
            {
                firstname = firstName,
                lastname = lastName,
                email = email,
                phone = phone,
                address = address,
                password = password,
                isadmin = isAdmin
            };

            // Call a method to register the user
            bool isRegistered = AddUser(newUser);

            if (isRegistered)
            {
                // Redirect the user to the login page or another appropriate page
                Response.Redirect("Login.aspx");
            }
            else
            {
                lblMessage.Text = "Registration failed. Please try again.";
            }
        }

        private bool AddUser(User user)
        {
            // Create an instance of the service client
            UserServiceClient userServiceClient = new UserServiceClient();

            // Call the AddUser method of the service
            return userServiceClient.AddUser(user);
        }
    }
}