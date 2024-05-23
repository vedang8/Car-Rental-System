<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="CarRentalSystemClient.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login</title>
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
        #formContainer input[type="password"] {
            width: calc(100% - 22px);
            padding: 10px;
            margin-bottom: 15px;
            border: 1px solid #ccc;
            border-radius: 3px;
        }

        #btnLogin {
            width: 100%;
            padding: 10px;
            background-color: #007bff;
            color: #fff;
            border: none;
            border-radius: 3px;
            cursor: pointer;
            transition: background-color 0.3s ease;
        }

        #btnLogin:hover {
            background-color: #0056b3;
        }

        #formContainer .message {
            text-align: center;
            margin-top: 10px;
            color: red;
        }

        @media screen and (max-width: 600px) {
            #formContainer {
                width: 90%;
            }
        }
        #formContainer .register-link {
            text-align: center;
            margin-top: 10px;
        }

        #formContainer .register-link a {
            color: #007bff;
            text-decoration: none;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div id="formContainer">
            <h2>Welcome back, Please Login!</h2>
            <label for="txtEmail">Email:</label>
            <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>

            <label for="txtPassword">Password:</label>
            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox>

            <asp:Button ID="btnLogin" runat="server" Text="Login" OnClick="btnLogin_Click" />

            <div class="message" runat="server" id="loginMessage">
                <asp:Label ID="lblMessage" runat="server"></asp:Label>
            </div>

            <div class="register-link">
                <asp:HyperLink ID="lnkRegister" runat="server" NavigateUrl="~/Register.aspx">Don't have an Account?</asp:HyperLink>
            </div>
        </div>
    </form>
</body>
</html>

