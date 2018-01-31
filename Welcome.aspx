<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Welcome.aspx.cs" Inherits="Welcome" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Label ID="txtuser" runat="server" Text =""></asp:Label>
        <asp:Button ID="logoutbtn" runat="server" Text ="Logout" OnClick="logoutbtn_Click"/>
        

    </div>
    </form>
</body>
</html>
