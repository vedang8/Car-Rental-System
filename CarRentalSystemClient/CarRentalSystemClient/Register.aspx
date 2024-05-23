<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="CarRentalSystemClient.Register" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Register</title>
    <style>
        body {
            font-family: Arial, sans-serif;
            margin: 0;
            padding: 0;
            background-color: #f4f4f4;
        }

        #formContainer {
            width: 80%;
            max-width: 400px;
            margin: 50px auto;
            background-color: #fff;
            padding: 20px;
            border-radius: 5px;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
        }

        #formContainer h2 {
            text-align: center;
            margin-bottom: 20px;
        }

        #formContainer label {
            display: block;
            margin-bottom: 5px;
        }

        #formContainer input[type="text"],
        #formContainer input[type="password"],
        #formContainer input[type="email"],
        #formContainer textarea {
            width: calc(100% - 22px);
            padding: 10px;
            margin-bottom: 15px;
            border: 1px solid #ccc;
            border-radius: 3px;
        }

        #formContainer input[type="checkbox"] {
            margin-right: 5px;
        }

        #btnRegister {
            width: 100%;
            padding: 10px;
            background-color: #007bff;
            color: #fff;
            border: none;
            border-radius: 3px;
            cursor: pointer;
            transition: background-color 0.3s ease;
        }

        #btnRegister:hover {
            background-color: #0056b3;
        }

        #formContainer .message {
            text-align: center;
            margin-top: 10px;
            color: green;
        }

        #formContainer .login-link {
            text-align: center;
            margin-top: 10px;
        }

        #formContainer .login-link a {
            color: #007bff;
            text-decoration: none;
        }

        @media screen and (max-width: 600px) {
            #formContainer {
                width: 90%;
            }
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div id="formContainer">
            <h2>Register</h2>
            <label for="txtFirstName">First Name:</label>
            <asp:TextBox ID="txtFirstName" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvFirstName" runat="server" ControlToValidate="txtFirstName" ErrorMessage="First Name is required" ForeColor="red" />

            <label for="txtMiddleName">Middle Name:</label>
            <asp:TextBox ID="txtMiddleName" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvMiddleName" runat="server" ControlToValidate="txtMiddleName" ErrorMessage="Middle Name is required" ForeColor ="red"/>

            <label for="txtLastName">Last Name:</label>
            <asp:TextBox ID="txtLastName" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvLastName" runat="server" ControlToValidate="txtLastName" ErrorMessage="Last Name is required" ForeColor ="red"/>

            <label for="txtEmail">Email:</label>
            <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail" ErrorMessage="Email is required" ForeColor ="red"/>

            <label for="txtPhone">Phone:</label>
            <asp:TextBox ID="txtPhone" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvPhone" runat="server" ControlToValidate="txtPhone" ErrorMessage="Phone is required" ForeColor ="red"/>

            <label for="txtAddress">Address:</label>
            <asp:TextBox ID="txtAddress" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvAddress" runat="server" ControlToValidate="txtAddress" ErrorMessage="Address is required" ForeColor ="red"/>

            <label for="txtPassword">Password:</label>
            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ControlToValidate="txtPassword" ErrorMessage="Password is required" ForeColor ="red"/>

            <label for="chkIsAdmin">Admin:</label>
            <asp:CheckBox ID="chkIsAdmin" runat="server" Text="Yes" />

            <asp:Button ID="btnRegister" runat="server" Text="Register" OnClick="btnRegister_Click" />

            <div class="message" runat="server" id="registerMessage" visible="false">
                <asp:Label ID="lblMessage" runat="server"></asp:Label>
            </div>

            <div class="login-link">
                <asp:HyperLink ID="lnkLogin" runat="server" NavigateUrl="~/Login.aspx">Go to Login</asp:HyperLink>
            </div>
        </div>
    </form>
</body>
</html>
