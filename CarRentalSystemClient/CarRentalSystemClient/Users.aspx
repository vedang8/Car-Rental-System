<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Users.aspx.cs" Inherits="CarRentalSystemClient.Users" %>

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
    cursor:pointer;
}

.data_row{
    background-color: cyan ;
    color: darkblue;
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
    background-color: lightblue;
    color: darkblue;
    cursor: pointer;/* Change to the desired hover color */
 }

</style>
</head>
<body>
    <form id="form1" runat="server">
<div class="navbar">
    <span class="title">Car Rental System</span>
    <a href="Cars.aspx">Cars</a>
    <a href="Users.aspx" class="active">Users</a>
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
     <table>
         <!-- Header row -->
         <tr class="header">
             <th>First Name</th>
             <th>Middle Name</th>
             <th>Last Name</th>
             <th>Email</th>
             <th>Phone</th>
             <th>Address</th>
             <th>Actions</th>
         </tr>
         <!-- Data rows -->
         <asp:Repeater ID="rptUsers" runat="server">
             <ItemTemplate>
                 <tr class="data_row">
                     <td><%# Eval("FirstName") %></td>
                     <td><%# Eval("MiddleName") %></td>
                     <td><%# Eval("LastName") %></td>
                     <td><%# Eval("Email") %></td>
                     <td><%# Eval("Phone") %></td>
                     <td><%# Eval("Address") %></td>
                     <td class="actions">
                         <asp:Button ID="hlDelete" CssClass="delete-button" runat="server" Text="Delete" OnClick="BtnDeleteUser_Click" CommandArgument='<%# Eval("Id") %>'/>
                     </td>
                 </tr>
             </ItemTemplate>
         </asp:Repeater>
     </table>
 </div>
 </form>
</body>
</html>
