<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Bill.aspx.cs" Inherits="CarRentalSystemClient.Bill" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Car Rental System</title>
    <style>
        body {
            margin: 0;
            padding: 0;
            font-family: Arial, sans-serif;
            background-color: #f0f0f0;
        }

        .navbar {
            background-color: #333;
            overflow: hidden;
            display: flex;
            justify-content: flex-start;
            align-items: center;
            padding: 10px 20px;
        }

        .navbar a {
            color: white;
            text-decoration: none;
            padding: 10px 15px;
        }

        .navbar a:hover {
            background-color: #ddd;
            color: black;
        }

        .navbar a.active {
            background-color: #4CAF50;
        }

        .title {
            font-size: 24px;
            color: white;
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
            color: #ffcc00;
            margin-right: 20px;
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
            text-align: left;
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
        .container {
            width: 50%;
            margin: 0 auto;
            background-color: #f9f9f9;
            padding: 20px;
            border: 1px solid #ddd;
        }

        h1 {
            text-align: center;
            margin-bottom: 20px;
            color: #333;
        }

        .bill-info {
            margin-bottom: 20px;
        }

        .bill-info label {
            font-weight: bold;
        }

        .user-info {
            margin-top: 20px;
        }

        .car-details {
            margin-top: 20px;
        }

        .total-cost {
            margin-top: 20px;
            
        }

        .footer {
            margin-top: 50px;
            text-align: center;
            color: #666;
        }
        #btnDownload {
    margin-top: 10px;
    padding: 6px 10px;
    background-color: red;
    color: white;
    border: none;
    border-radius: 4px;
    font-weight: bold;
}

#btnDownload:hover{
    background-color: indianred;
    color: white;
    cursor: pointer;
}
        .car-image {
            width: 5px;
            height: 5px;
            padding: 4px 150px;
        }

        .auto-style1 {
            width: 440px;
            height: 255px;
            margin-left: 136px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="navbar">
        <span class="title">Car Rental System</span>
        <a href="BookCar.aspx" class="active">Book Car</a>
        <a href="History.aspx">History</a>
        <span id="uname">
            <asp:Label ID="Username" CssClass="username" runat="server"></asp:Label>
            <asp:Button ID="btnLogout" runat="server" Text="Log out" OnClick="btnLogout_Click" />
        </span>
    </div>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnDownload" runat="server" Text="Download" OnClick="btnDownload_Click" />
        <div class="container">
        <h1>Car Rental System - Booking Receipt</h1>
            <div class="car-image">
                <img src="assets/images/swift.jpg" alt="Car Image" class="auto-style1" />
            </div>
        <div class="bill-info">
            <label>Booking ID:</label> <asp:Label ID="lblBookingId" runat="server" CssClass="car-detail"></asp:Label><br />
            <label>Booking Date:</label> <asp:Label ID="lblBookingDate" runat="server" CssClass="car-detail"></asp:Label>
        </div>

        <div class="user-info">
            <label>User Name:</label> <asp:Label ID="lblUserName" runat="server" CssClass="user-detail"></asp:Label><br />
        </div>

        <div class="car-details">
            <label>Car Model:</label> <asp:Label ID="lblCarModel" runat="server" CssClass="car-detail"></asp:Label><br />
            <label>Number Plate:</label> <asp:Label ID="lblNumberPlate" runat="server" CssClass="car-detail"></asp:Label><br />
            <label>Rental Car Price:</label> <asp:Label ID="lblRentalCarPrice" runat="server" CssClass="car-detail"></asp:Label><br />
            <label>Fuel Type:</label> <asp:Label ID="lblFuelType" runat="server" CssClass="car-detail"></asp:Label><br />
            <label>Capacity:</label> <asp:Label ID="lblCapacity" runat="server" CssClass="car-detail"></asp:Label><br />
            <label>Driver Cost:</label> <asp:Label ID="lblDriverCost" runat="server" CssClass="car-detail"></asp:Label><br />
            <label>Driver Required:</label> <asp:Label ID="lblDriverRequired" runat="server" CssClass="car-detail"></asp:Label><br />   
            <label>Rental Period:</label> <asp:Label ID="lblRentalPeriod" runat="server" CssClass="car-detail"></asp:Label><br />
        </div>

        <div class="total-cost">
            <label>Total Cost:</label> <asp:Label ID="lblTotalCost" runat="server" CssClass="car-detail"></asp:Label>
        </div>  
    </div>
    <div class="footer">
        Thank you for choosing Car Rental System.
    </div>
    </form>
</body>
</html>
