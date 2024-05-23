<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditCar.aspx.cs" Inherits="CarRentalSystemClient.EditCar" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Car Rental System</title>
    <link rel="stylesheet" href="assets/css/styles.css" />
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
        .username {
            color: #ffcc00;
            margin-right: 20px;
        }
        #uname {
            position: absolute;
            right: 20px;
        }
        .container {
            width: 50%;
            margin: 20px auto;
            padding: 20px;
            border: 1px solid #ccc;
            border-radius: 8px;
            background-color: #fff;
            box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
        }
        h2 {
            text-align: center;
            margin-bottom: 20px;
        }
        .car-detail {
            display: block;
            margin-bottom: 10px;
            font-size: 16px;
        }
        .btn-back {
            padding: 6px 10px;
            background-color: deepskyblue;
            color: darkblue;
            border: none;
            border-radius: 3px;
        }
        .btn-back:hover {
            background-color: dodgerblue;
            color: white;
            cursor: pointer;
        }

        .car-detail label {
        display: inline-block;
        width: 120px; /* Adjust width as needed */
        font-weight: bold;
    }

    .car-detail {
        margin-bottom: 10px;
        font-size: 16px;
    }

    .model label {
        color: #FF5733; /* Color for Model */
    }

    .number-plate label {
        color: #C70039; /* Color for Number Plate */
    }

    .rent-price label {
        color: #FFC300; /* Color for Rent Price */
    }

    .capacity label {
        color: #900C3F; /* Color for Capacity */
    }

    .fuel-type label {
        color: #00A8CC; /* Color for Fuel Type */
    }

    .mileage label {
        color: #005C97; /* Color for Mileage */
    }

    .driver-cost label {
        color: #7D3C98; /* Color for Driver Cost */
    }

    .car-image {
    width: 5px;
    height: 5px;
    padding: 4px 150px ;
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
        .auto-style1 {
            width: 455px;
            height: 255px;
            margin-left: 52px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="navbar">
            <span class="title">Car Rental System</span>
            <a href="Cars.aspx" class="active">Cars</a>
            <a href="Users.aspx">Users</a>
            <a href="AllBookings.aspx">Bookings</a>
            <a href="Analytics.aspx">Analytics</a>
            <span id="uname">
                <asp:Label ID="Username" CssClass="username" runat="server"></asp:Label>
                <asp:Button ID="btnLogout" runat="server" Text="Log out" OnClick="btnLogout_Click" />
            </span>
        </div>
        <div class="container">
            <h2>Edit Car Details</h2>
            <div>
                <!-- Car image and details section -->
                <div class="car-image">
                    <img src="assets/images/swift.jpg" alt="Car Image" class="auto-style1" />
                </div>
                <div class="model">
                    <label>Model:</label>
                    <asp:TextBox ID="txtModel" runat="server" CssClass="car-detail"></asp:TextBox>
                </div>
                <div class="number-plate">
                    <label>Number Plate:</label>
                    <asp:TextBox ID="txtNumberPlate" runat="server" CssClass="car-detail"></asp:TextBox>
                </div>
                <div class="rent-price">
                    <label>Rent Price:</label>
                    <asp:TextBox ID="txtRentPrice" runat="server" CssClass="car-detail"></asp:TextBox>
                </div>
                <div class="capacity">
                    <label>Capacity:</label>
                    <asp:TextBox ID="txtCapacity" runat="server" CssClass="car-detail"></asp:TextBox>
                </div>
                <div class="fuel-type">
                    <label>Fuel Type:</label>
                    <asp:TextBox ID="txtFuelType" runat="server" CssClass="car-detail"></asp:TextBox>
                </div>
                <div class="mileage">
                    <label>Mileage:</label>
                    <asp:TextBox ID="txtMileage" runat="server" CssClass="car-detail"></asp:TextBox>
                </div>
                <div class="driver-cost">
                    <label>Driver Cost:</label>
                    <asp:TextBox ID="txtDriverCost" runat="server" CssClass="car-detail"></asp:TextBox>
                </div>
                <asp:Button ID="btnUpdate" runat="server" Text="Update" OnClick="btnUpdate_Click" CssClass="btn-back" Height="45px" />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Label ID="lblMessage" runat="server" Text="Label" Visible="false"></asp:Label>
            </div>
        </div>
    </form>
</body>
</html>
