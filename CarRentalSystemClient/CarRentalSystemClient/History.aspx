<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="History.aspx.cs" Inherits="CarRentalSystemClient.History" %>

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

        table tr:hover {
            background-color: #E6E6FA;
            color: purple;
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

        .data_row {
            background-color: #FFF0F5;
            color: #6C244C;
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

            #btnRefresh:hover {
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
            <a href="BookCar.aspx">Book Car</a>
            <a href="History.aspx"  class="active">History</a>
            <span id="uname">
                <asp:Label ID="Username" CssClass="username" runat="server"></asp:Label>
                <asp:Button ID="btnLogout" runat="server" Text="Log out" OnClick="btnLogout_Click" />
            </span>
        </div>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
<asp:Button ID="btnRefresh" runat="server" Text="Refresh" OnClick="btnRefresh_Click" />
        <div class="container">
            <table>
                <!-- Header row -->
                <tr class="header">
                    <th>Booking Date</th>
                    <th>Car Name</th>
                    <th>Start Date</th>
                    <th>End Date</th>
                    <th>Price</th>
                    <th>Driver Required</th>
                </tr>
                <!-- Data rows -->
                <asp:Repeater ID="rptHistory" runat="server">
                    <ItemTemplate>
                        <tr class="data_row">
                            <td><%# Eval("booking_date", "{0:yyyy-MM-dd}") %></td>
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

