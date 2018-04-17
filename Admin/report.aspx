<%@ Page Language="C#" AutoEventWireup="true" CodeFile="report.aspx.cs" Inherits="Admin_report" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
    <style type = "text/css">

        /* Table Design #1 */
        table.one
        {
            text-align: center;
            margin-left:auto; 
            margin-right:auto; 
            display: inline-block;
            font-family: "Lato","sans-serif";
            border-collapse: collapse; 
        }
        table.one th
        {
            border-color: #e8503a;
            padding: 1em;
            text-align: center;
            background-color: #e8503a;
            color: white;
        }
        table.one tr
        {
            height: 1em;
            
        }
        table.one tr:nth-child(even)
        {
            background-color: #eee;
        }
        table.one tr:nth-child(odd)
        {
            background-color: #fff;
        }
       

        /* Table Design #2 */

        table.two
        {
            text-align: center;
            margin-left:auto; 
            margin-right:auto; 
            display: inline-block;
            font-family: "Lato","sans-serif";
            border-collapse: collapse; 
        }
        table.two th
        {
            border-color: #000000;
            padding: 1em;
            text-align: center;
            background-color: #000000;
            color: white;
        }
        table.two tr
        {
            height: 1em;
            
        }
        table.two tr:nth-child(even)
        {
            background-color: #eee;
        }
        table.two tr:nth-child(odd)
        {
            background-color: #fff;
        }
        table.two tr:hover
        {
            background-color: yellow;
            font-weight: bold;
        }

        /* Table Design #3 */
        table.three
        {
            text-align: center;
            margin-left:auto; 
            margin-right:auto; 
            display: inline-block;
            font-family: "Lato","sans-serif";
            border-collapse: collapse;
            background-coor: #000000;
        }
        table.three th
        {
            border-color: #000000;
            padding: 1em;
            text-align: center;
            background-color: #000000;
            color: greenyellow;
        }
        table.three tr
        {
            height: 2em;
            background-color: black;
            
        }
        table.three tr:nth-child(even)
        {
            color: white;
            /*background-color: #eee; */
        }
        table.three tr:nth-child(odd)
        {
            color: white;
            /*  background-color: #fff;*/
        }
        table.three tr:hover
        {
            background-color: lightskyblue;
            color: black;
            font-weight: bold;
        }

         /* Table Design #4 */
        table.four
        {
            text-align: center;
            margin-left:auto; 
            margin-right:auto; 
            display: table;
            font-family: "Lato","sans-serif";
            border-collapse: collapse;
            background-coor: #000000;
            
        }
        table.four th
        {
            border-color: white;
            padding: 1em;
            text-align: center;
            background-color: white;
            color: black;
        }
        table.four tr
        {
            height: 2em;
            padding: 1em;
            background-color: white;
            
        }
        table.four tr:nth-child(even)
        {
            color: white;
            background-color: red
        }
        table.four tr:nth-child(odd)
        {
            color: black;
            background-color: aliceblue
        }
        

    </style>
<body style = "background-color:powderblue;">
    <form id="form1" runat="server">
        <div>
            <table  class="four" >
                
                 <thread>
                    <tr>
                
                <th >First Name</th>
                <th >Project ID</th>
                <th >Monday Hours</th>
                <th >Tuesday Hours</th>
                <th >Wednesday Hours</th>
                <th >Thursday Hours</th>
                <th >Friday Hours</th>
                <th >Saturday Hours</th>
                <th >Sunday Hours</th>
                
                </tr>
                </thread>

                <asp:Repeater ID="Repeater2" runat="server">
                    <ItemTemplate>

                        <tr >
                            <td><%# Eval("fname")%></td>
                            <td align="center"><%# Eval("pid")%></td>
                            <td align="center"><%# Eval("MonHours")%></td>
                            <td align="center"><%# Eval("TuesHours")%></td>
                            <td align="center"><%# Eval("WedsHours")%></td>
                            <td align="center"><%# Eval("ThursHours")%></td>
                            <td align="center"><%# Eval("FriHours")%></td>
                            <td align="center"><%# Eval("SatHours")%></t>
                            <td align="center"><%# Eval("SunHours")%></td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>

            </table>
        </div>
    </form>
</body>
</html>
