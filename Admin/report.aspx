<%@ Page Language="C#" AutoEventWireup="true" CodeFile="report.aspx.cs" Inherits="Admin_report" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table>
                <td>First Name</td>
                <td>Project ID</td>
                <td>Monday Hours</td>
                <td>Tuesday Hours</td>
                <td>Wednesday Hours</td>
                <td>Thursday Hours</td>
                <td>Friday Hours</td>
                <td>Saturday Hours</td>
                <td>Sunday Hours</td>

                <asp:Repeater ID="Repeater2" runat="server">
                    <ItemTemplate>

                        <tr>
                            <td><%# Eval("fname")%></td>
                            <td><%# Eval("pid")%></td>
                            <td><%# Eval("MonHours")%></td>
                            <td><%# Eval("TuesHours")%></td>
                            <td><%# Eval("WedsHours")%></td>
                            <td><%# Eval("ThursHours")%></td>
                            <td><%# Eval("FriHours")%></td>
                            <td><%# Eval("SatHours")%></td>
                            <td><%# Eval("SunHours")%></td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>

            </table>
        </div>
    </form>
</body>
</html>
