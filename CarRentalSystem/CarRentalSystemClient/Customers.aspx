<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Customers.aspx.cs" Inherits="CarRentalSystemClient.Customers" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Customers</title>
    <link rel="stylesheet" href="assets/css/styles.css" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="navbar">
            <a href="Admin.aspx">Home</a>
            <a href="Cars.aspx">Cars</a>
            <a class="active" href="#">Customers</a> <!-- Highlight the current page -->
            <a href="Bookings.aspx">Bookings</a>
            <a href="Logout.aspx">Logout</a>
        </div>

        <div class="content">
            <h2>All Customers</h2>

            <!-- Display customers in a GridView -->
            <asp:GridView ID="gridCustomers" runat="server" AutoGenerateColumns="false">
                <Columns>
                    <asp:BoundField DataField="CustomerId" HeaderText="Customer ID" />
                    <asp:BoundField DataField="Name" HeaderText="Name" />
                    <asp:BoundField DataField="Email" HeaderText="Email" />
                    <asp:BoundField DataField="Phone" HeaderText="Phone" />
                </Columns>
            </asp:GridView>
        </div>
    </form>
</body>
</html>
