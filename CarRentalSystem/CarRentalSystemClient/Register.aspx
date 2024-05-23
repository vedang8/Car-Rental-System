<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="CarRentalSystemClient.Register" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
     <form id="form1" runat="server">
        <div>
            <h2>Register</h2>
            <div>
                <label for="txtFirstName">First Name:</label>
                <asp:TextBox ID="txtFirstName" runat="server"></asp:TextBox>
                <br />
            </div>
            <div>
                <label for="txtLastName">Last Name:</label>
                <asp:TextBox ID="txtLastName" runat="server"></asp:TextBox>
                <br />
            </div>
            <div>
                <label for="txtEmail">Email:</label>
                <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
                <br />
            </div>
            <div>
                <label for="txtPhone">Phone:</label>
                <asp:TextBox ID="txtPhone" runat="server"></asp:TextBox>
                <br />
            </div>
            <div>
                <label for="txtAddress">Address:</label>
                <asp:TextBox ID="txtAddress" runat="server"></asp:TextBox>
                <br />
            </div>
            <div>
                <label for="txtPassword">Password:</label>
                <asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox>
                <br />
            </div>
            <div>
                <label for="chkIsAdmin">Is Admin:</label>
                <asp:CheckBox ID="chkIsAdmin" runat="server" Text="Yes" />
                <br />
            </div>
            <div>
                <asp:Button ID="btnRegister" runat="server" Text="Register" OnClick="btnRegister_Click" />
            </div>
            <div>
                <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
            </div>
        </div>
    </form>
</body>
</html>
