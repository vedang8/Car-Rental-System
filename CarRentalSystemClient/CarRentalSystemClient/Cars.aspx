<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Cars.aspx.cs" Inherits="CarRentalSystemClient.Cars" %>

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
        table tr:hover {
            background-color: lightgreen;
            color: darkolivegreen;
            cursor: pointer;/* Change to the desired hover color */
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

        .search-container #btnAddCar{
            padding: 10px 20px;
            background-color: sandybrown;
            color: saddlebrown;
            border: none;
            border-radius: 4px;
            cursor: pointer;
        }

        .search-container #btnAddCar:hover {
            background-color: lightsalmon;
            color: white;
        }

        .delete-button {
            padding: 6px 10px;
            background-color: red;
            color: #fff;
            border: none;
            border-radius: 3px;
            font-weight: bold;
        }

        .delete-button:hover {
            background-color: orangered;
            cursor: pointer;
        }

        .view-button {
            padding: 6px 10px;
            background-color: darkblue;
            color: #fff;
            border: none;
            border-radius: 3px;
            font-weight: bold;
        }

        .view-button:hover{
            background-color: blue;
            cursor: pointer;
        }

        .edit-button {
            padding: 6px 10px;
            background-color: darkorange;
            color: #fff;
            border: none;
            border-radius: 3px;
            font-weight: bold;
        }

        .edit-button:hover{
            background-color: orange;
            cursor: pointer;
        }

        .data_row{
            background-color: lawngreen ;
        }
        .filter-container {
            margin-bottom: 20px;
        }

        .filter-container select {
            padding: 10px;
            margin-right: 10px;
            border: 1px solid #ccc;
            border-radius: 4px;
        }

        .filter-container #btnFilter {
            padding: 10px 20px;
            background-color: #333;
            color: #fff;
            border: none;
            border-radius: 4px;
            cursor: pointer;
        }

        .filter-container #btnFilter:hover {
            background-color: #555;
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
<asp:Button ID="btnRefresh" runat="server" Text="Refresh" OnClick="btnRefresh_Click" />
        <div class="container">
            <div class="search-container">
                <asp:TextBox ID="inputTextBox" runat="server" placeholder="Search Car by model ..." AutoPostBack="false"></asp:TextBox>
                <asp:Button ID="btnSearch" runat="server" OnClick="BtnSearch_Click" Text="Search" />

                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
     <asp:Button ID="btnAddCar" runat="server" OnClick="BtnAddCar_Click" Text="Add Car" />
            </div>
            <div class="filter-container">
                <asp:DropDownList ID="ddlFuelTypeFilter" runat="server" AppendDataBoundItems="true" AutoPostBack="false">
                    <asp:ListItem Text="Fuel Type" Value=""></asp:ListItem>
                    <asp:ListItem Text="Petrol" Value="Petrol"></asp:ListItem>
                    <asp:ListItem Text="Diesel" Value="Diesel"></asp:ListItem>
                    <asp:ListItem Text="Electric" Value="Electric"></asp:ListItem>
                </asp:DropDownList>
                <asp:Button ID="btnFilter" OnClick="BtnFilter_Click" runat="server" Text="Apply Filter" />
            </div>
       
        </div>
        <div class="message" runat="server" id="carMessage" visible="false">
            <asp:Label ID="lblMessage" runat="server"></asp:Label>
        </div>
    <div class="container">
        <table>
            <!-- Header row -->
            <tr class="header">
                <th>Model</th>
                <th>Number Plate</th>
                <th>Rental Price</th>
                <th>Capacity</th>
                <th>Fuel Type</th>
                <th>Mileage</th>
                <th>Driver Cost</th>
                <th>Actions</th>
            </tr>
            <!-- Data rows -->
            <asp:Repeater ID="rptCars" runat="server">
                <ItemTemplate>
                    <tr class="data_row">
                        <td><%# Eval("car_model") %></td>
                        <td><%# Eval("car_number_plate") %></td>
                        <td>₹ <%# Eval("rental_price") %></td>
                        <td><%# Eval("capacity") %></td>
                        <td><%# Eval("fuel_type") %></td>
                        <td><%# Eval("mileage") %></td>
                        <td>₹ <%# Eval("driver_cost") %></td>
                        <td class="actions">
                            <asp:Button ID="hlView" CssClass="view-button" runat="server" Text="View" OnClick="BtnViewCar_Click" CommandArgument='<%# Eval("Id") %>'/>
                            <asp:Button ID="hlEdit" CssClass="edit-button" runat="server" Text="Edit" OnClick="BtnEditCar_Click" CommandArgument='<%# Eval("Id") %>'/>
                            <asp:Button ID="hlDelete" CssClass="delete-button" runat="server" Text="Delete" OnClick="BtnDeleteCar_Click" CommandArgument='<%# Eval("Id") %>'/>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </table>
    </div>
    </form>
</body>
</html>
