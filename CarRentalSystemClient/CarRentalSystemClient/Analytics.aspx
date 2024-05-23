<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Analytics.aspx.cs" Inherits="CarRentalSystemClient.Analytics" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Car Rental System</title>
    <script src="https://cdn.jsdelivr.net/npm/chart.js"></script>
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
                .chart-container {
    margin: 0 auto; /* Center the container horizontally */
    max-width: 600px; /* Set maximum width for the container */
    padding: 20px; /* Add some padding for spacing */
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

    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="navbar">
            <span class="title">Car Rental System</span>
            <a href="Cars.aspx">Cars</a>
            <a href="Users.aspx">Users</a>
            <a href="AllBookings.aspx">Bookings</a>
            <a href="Analytics.aspx" class="active">Analytics</a>
            <span id="uname">
                <asp:Label ID="Username" CssClass="username" runat="server"></asp:Label>
                <asp:Button ID="btnLogout" runat="server" Text="Log out" OnClick="btnLogout_Click" />
            </span>
        </div>

        <div class="chart-container">
            <asp:Chart ID="bookingsChart" runat="server" Width="400" Height="300">
                <Series>
                    <asp:Series Name="Bookings" ChartType="Bar"></asp:Series>
                </Series>
                <ChartAreas>
                    <asp:ChartArea Name="ChartArea1"></asp:ChartArea>
                </ChartAreas>
            </asp:Chart>
        </div>
    </form>
</body>
</html>
