<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AllBookings.aspx.cs" Inherits="CarRentalSystemClient.AllBookings" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Car Rental System</title>
        <style>
        body {
            margin: 0;
            padding: 0;
            font-family: Arial, sans-serif;
            background-color: #f0f0f0; /* Background color suitable for your project */
        }

        .navbar {
            background-color: #333; /* Dark background color for the navbar */
            overflow: hidden;
            display: flex;
            justify-content: flex-start; /* Align items to the start and end of the flex container */
            align-items: center;
            padding: 10px 20px; /* Add padding for spacing */
        }

        .navbar a {
            color: white;
            text-decoration: none;
            padding: 10px 15px; /* Adjust padding for spacing */
        }

        .navbar a:hover {
            background-color: #ddd; /* Hover color for the navbar links */
            color: black;
        }

        .navbar a.active {
            background-color: #4CAF50; /* Active link color */
        }

        .title {
            font-size: 24px; /* Increase the font size of the title */
            color: white;
            left: 0;
            margin-right: 50px;
        }

            #btnLogout {
                padding: 6px 10px;
                background-color: darkred;
                color: #fff;
                border: none;
                border-radius: 3px;
                font-weight: bold;
            }

            #btnLogout:hover {
                background-color: red;
                cursor: pointer;
            }

        .username {
            color: #ffcc00; /* Color for the username */
            margin-right: 20px; /* Add some space between the username and the logout button */
        }

        #uname {
            position: absolute;
            right: 20px;
        }

        table {
    width: 100%;
    border-collapse: collapse;
}

th, td {
    padding: 10px;
    border: 1px solid #ccc;
    text-align: center;
}

th {
    background-color: #333;
    color: #fff;
}

.actions {
    text-align: center;
}
 .container {
    margin: 20px auto;
    width: 80%;
}

.actions a {
    display: inline-block;
    margin: 5px;
    padding: 5px 10px;
    background-color: #333;
    color: #fff;
    text-decoration: none;
    border-radius: 3px;
}

.actions a:hover {
    background-color: #555;
}
    
#hlAdd {
    display: inline-block;
    position: absolute;
    right: 10%;
    padding: 10px 10px;
    background-color: green;
    color: #fff;
    text-decoration: none;
    border-radius: 3px;
}

#hlAdd:hover {
    background-color: forestgreen;
    padding: 10px 10px;
}

.search-container {
    margin-bottom: 20px;
}

.search-container #inputTextBox {
    padding: 10px;
    margin-right: 10px;
    border: 1px solid #ccc;
    border-radius: 4px;
    width: 40%;
}

.search-container #btnSearch {
    padding: 10px 20px;
    background-color: #333;
    color: #fff;
    border: none;
    border-radius: 4px;
    cursor: pointer;
}

.search-container #btnSearch:hover {
    background-color: #555;
}
.data_row{
    background-color: #FFD700 ;
}
#btnRefresh {
    margin-top: 10px;
    padding: 6px 10px;
    background-color: lawngreen;
    color: darkgreen;
    border: none;
    border-radius: 4px;
    font-weight: bold;
}

#btnRefresh:hover{
    background-color: greenyellow;
    color: green;
    cursor: pointer;
}
table tr:hover {
    background-color: lightgoldenrodyellow;
    color: darkgoldenrod;
    cursor: pointer;/* Change to the desired hover color */
 }
    </style>
</head>
<body>

    <form id="form2" runat="server">
<div class="navbar">
    <span class="title">Car Rental System</span>
    <a href="Cars.aspx">Cars</a>
    <a href="Users.aspx">Users</a>
    <a href="AllBookings.aspx" class="active">Bookings</a>
    <a href="Analytics.aspx">Analytics</a>
    <span id="uname">
        <asp:Label ID="Username" CssClass="username" runat="server"></asp:Label>
        <asp:Button ID="btnLogout" runat="server" Text="Log out" OnClick="btnLogout_Click"/>
    </span>
</div>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
<asp:Button ID="btnRefresh" runat="server" Text="Refresh" OnClick="btnRefresh_Click" />
        <div class="container">
    <div class="search-container">
        <asp:TextBox ID="inputTextBox" runat="server" placeholder="Search Car by model ..." AutoPostBack="false"></asp:TextBox>
        <asp:Button ID="btnSearch" runat="server" onclick="BtnSearch_Click" Text="Search"/>
    </div>
</div>
<div class="container">
    <table>
        <!-- Header row -->
        <tr class="header">
            <th>Booking Date</th>
            <th>User Name</th>
            <th>Car Name</th>
            <th>Start Date</th>
            <th>End Date</th>
            <th>Price</th>
            <th>Driver Required</th>
        </tr>
        <!-- Data rows -->
        <asp:Repeater ID="rptBookings" runat="server">
            <ItemTemplate>
                <tr class="data_row">
                <td><%# Eval("booking_date", "{0:yyyy-MM-dd}") %></td>
                <td><%# Eval("FirstName") %></td>
                <td><%# Eval("car_model") %></td>
                <td><%# Eval("start_date", "{0:yyyy-MM-dd}") %></td>
                <td><%# Eval("end_date", "{0:yyyy-MM-dd}") %></td>
                <td>₹ <%# Eval("price") %></td>
                <td><%# Eval("driver_required") %></td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
</div>
</form>
</body>
</html>
