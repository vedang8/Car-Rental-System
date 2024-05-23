<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BookCar.aspx.cs" Inherits="CarRentalSystemClient.BookCar" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Book a Car</title>
    <style>
        .calendar {
            display: inline-block;
            margin-bottom: 10px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>Select Dates</h2>
            <div class="calendar">
                <asp:Label ID="lblStartDate" runat="server" Text="Start Date: "></asp:Label>
                <asp:Calendar ID="calStartDate" runat="server" OnSelectionChanged="calStartDate_SelectionChanged"></asp:Calendar>
            </div>
            <div class="calendar">
                <asp:Label ID="lblEndDate" runat="server" Text="End Date: "></asp:Label>
                <asp:Calendar ID="calEndDate" runat="server" OnSelectionChanged="calEndDate_SelectionChanged"></asp:Calendar>
            </div>
            <br />
            <h2>Available Cars</h2>
            <asp:GridView ID="gridAvailableCars" runat="server" AutoGenerateColumns="false" DataKeyNames="Id">
                <Columns>
                    <asp:BoundField DataField="Model" HeaderText="Model" />
                    <asp:BoundField DataField="NumberPlate" HeaderText="Number Plate" />
                    <asp:BoundField DataField="RentalPrice" HeaderText="Rental Price" />
                    <asp:TemplateField HeaderText="Select">
                        <ItemTemplate>
                            <asp:RadioButton ID="radioSelect" runat="server" GroupName="carSelection" Text="Select" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <br />
            <h2>Additional Options</h2>
            <asp:CheckBox ID="chkDriver" runat="server" Text="Driver Required" />
            <br />
            <br />
            <asp:Button ID="btnBook" runat="server" Text="Book Selected Car" OnClick="btnBook_Click" />
        </div>
    </form>
</body>
</html>
