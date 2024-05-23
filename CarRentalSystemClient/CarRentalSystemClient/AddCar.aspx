<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddCar.aspx.cs" Inherits="CarRentalSystemClient.AddCar" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Add Car</title>
    <link rel="stylesheet" href="assets/css/styles.css" />
    <style>
        body {
            margin: 0;
            padding: 0;
            font-family: Arial, sans-serif;
            background-color: #f0f0f0;
        }
        /* Centering the form */
        .container {
            width: 50%;
            margin: 0 auto;
            padding: 20px;
            border: 1px solid #ccc;
            border-radius: 8px;
            background-color: #fff;
            box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
        }

        /* Form title */
        .form-title {
            margin-top: 0;
            margin-bottom: 20px;
            font-size: 24px;
            text-align: center;
        }

        /* Form fields */
        .form-group {
            margin-bottom: 20px;
        }
        .form-group label {
            display: block;
            font-weight: bold;
            margin-bottom: 5px;
        }
        .form-group input[type="text"],
        .form-group input[type="number"],
        .form-group select {
            width: 100%;
            padding: 10px;
            border: 1px solid #ccc;
            border-radius: 4px;
            box-sizing: border-box;
            font-size: 16px;
        }

        /* Save button */
        .form-group button {
            width: 100%;
            padding: 10px 15px;
            border: none;
            border-radius: 4px;
            background-color: #4CAF50;
            color: white;
            font-size: 16px;
            cursor: pointer;
            transition: background-color 0.3s;
        }
        .form-group button:hover {
            background-color: #45a049;
            cursor: pointer;
        }

        /* Error message */
        .error-message {
            color: red;
            font-size: 14px;
        }

        /* Back link */
        .back-link {
            display: block;
            text-align: center;
            margin-top: 20px;
            font-size: 16px;
            color: #333;
            text-decoration: none;
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

    
#hlAdd {
    display: inline-block;
    padding: 10px 10px;
    background-color: green;
    color: #fff;
    text-decoration: none;
    border-radius: 4px;
}

#hlAdd:hover {
    background-color: forestgreen;
    padding: 10px 10px;
}

#btnBack{
    padding: 10px 20px;
    margin-top: 10px;
    background-color: deepskyblue;
    color: darkblue;
    border: none;
    border-radius: 4px;
    cursor: pointer;
    position: absolute;
}

#btnBack:hover{
    color: white;
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
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
        <asp:Button ID="btnBack" runat="server" OnClick="BtnBack_Click" Text="Back" />
        <div class="container">
            <h2 class="form-title">Add Car</h2>
            <div class="form-group">
                <label for="txtModel">Model:</label>
                <asp:TextBox ID="txtModel" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="txtNumberPlate">Number Plate:</label>
                <asp:TextBox ID="txtNumberPlate" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="txtRentalPrice">Rental Price:</label>
                <asp:TextBox ID="txtRentalPrice" runat="server" CssClass="form-control" type="number"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="txtCapacity">Capacity:</label>
                <asp:TextBox ID="txtCapacity" runat="server" CssClass="form-control" type="number"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="txtFuelType">Fuel Type:</label>
                <select id="txtFuelType" runat="server" class="form-control">
                    <option value="Petrol">Petrol</option>
                    <option value="Diesel">Diesel</option>
                    <option value="Electric">Electric</option>
                </select>
            </div>
            <div class="form-group">
                <label for="txtMileage">Mileage:</label>
                <asp:TextBox ID="txtMileage" runat="server" CssClass="form-control" type="number"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="txtDriverCost">Driver Cost:</label>
                <asp:TextBox ID="txtDriverCost" runat="server" CssClass="form-control" type="number"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="fileImage">Image:</label>
                <asp:FileUpload ID="fileImage" runat="server" CssClass="form-control" />
            </div>

            <div class="form-group">
                <asp:Button ID="hlAdd" runat="server" Text="Save" OnClick="btnSaveCar_Click" />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Label ID="lblAddCarMessage" runat="server" CssClass="error-message" ForeColor="#009933"></asp:Label>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            </div>

        </div>
    </form>
</body>
</html>



