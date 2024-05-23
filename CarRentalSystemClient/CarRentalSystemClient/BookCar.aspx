<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BookCar.aspx.cs" Inherits="CarRentalSystemClient.BookCar" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Car Rental System</title>
    <script src="https://code.jquery.com/jquery-1.12.4.js"></script>
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
    <link href="https://code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css" rel="stylesheet" />
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script>
        $(function () {
            $("#<%= txtStartDate.ClientID %>").datepicker({
                dateFormat: "yy-mm-dd" // Format as YYYY-MM-DD
            });
            $("#<%= txtEndDate.ClientID %>").datepicker({
                dateFormat: "yy-mm-dd" // Format as YYYY-MM-DD
            });
        });
    </script>
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

#hlBook {
    background-color: green;
    color: #fff;
    text-decoration: none;
    border-radius: 3px;
}

#hlBook:hover {
    background-color: forestgreen;
    padding: 10px 10px;
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

        .book-button {
            padding: 6px 10px;
            background-color: orangered;
            color: white;
            border: none;
            border-radius: 4px;
            font-weight: bold;
        }

            .book-button:hover {
                background-color: darkorange;
                color: white;
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

.search-container #btnGenearateBill {
    padding: 10px 20px;
    background-color: lightskyblue;
    color: darkblue;
    border: none;
    border-radius: 4px;
    cursor: pointer;
}

.search-container #btnGenerateBill:hover {
    background-color: lightblue;
    cursor:pointer;
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

#btnFindCars{
    padding: 6px 10px;
background-color: purple;
color: #fff;
border: none;
border-radius: 3px;
font-weight: bold;
}
#btnFindCars:hover{
    background-color: mediumorchid;
cursor: pointer;
}
.data_row {
    background-color: white;
    color: #6C244C;
}
table tr:hover {
    background-color: moccasin;
    color: saddlebrown;
    cursor: pointer;/* Change to the desired hover color */
    transition: color 0.3s ease;
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
                <asp:Button ID="btnLogout" runat="server" Text="Log out" OnClick="btnLogout_Click"/>
            </span>
        </div>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnRefresh" runat="server" Text="Refresh" OnClick="btnRefresh_Click" />
        
       
        <div class="container">
            <div class="search-container">
                <asp:TextBox ID="inputTextBox" runat="server" placeholder="Search Car by model ..." AutoPostBack="false"></asp:TextBox>
                <asp:Button ID="btnSearch" runat="server" OnClick="BtnSearch_Click" Text="Search" />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnGenearateBill" runat="server" Text="Generate Bill" OnClick="btnGenerateBill_Click" Visible="False" />
            </div>
            <div>
                
<asp:Label ID="lblStartDate" runat="server" Text="Start Date"></asp:Label>
<asp:TextBox ID="txtStartDate" runat="server" TextMode="Date"></asp:TextBox>

<br />

<asp:Label ID="lblEndDate" runat="server" Text="End Date"></asp:Label>
<asp:TextBox ID="txtEndDate" runat="server" TextMode="Date"></asp:TextBox>

<br />

<asp:CheckBox ID="chkDriverRequired" runat="server" Text="Driver Required" />

<br />

                <asp:Button ID="btnFindCars" runat="server" Text="Find Cars" OnClick="btnFindCars_Click" />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Label ID="lblBookingMessage" runat="server" ForeColor="Green"></asp:Label>
                &nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
                &nbsp;
                <br />
            </div>
        </div>
        
        <br />
        <div>
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
                        <asp:Button ID="hlBookCar" CSSClass="book-button" runat="server" Text="Book" OnClick="BtnBook_Click" CommandArgument='<%# Eval("Id") %>'/>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
</div>
        </div>
    </form>
</body>
</html>
