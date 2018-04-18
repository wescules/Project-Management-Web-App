<%@ Page Language="C#" AutoEventWireup="true" CodeFile="report.aspx.cs" Inherits="Admin_report" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <style>
       table.blueTable {
  font-family: 'Poppins', sans-serif;
  border: 1px solid #1C6EA4;
  background-color: #EEEEEE;
  width: 100%;
  height: 100%;
  text-align: center;
  border-collapse: collapse;
}
table.blueTable td, table.blueTable th {
  border: 1px solid #000000;
  padding: 3px 2px;
}
table.blueTable tbody td {
  font-size: 13px;
  color: #000000;
}
table.blueTable tr:nth-child(even) {
  background: #D0E4F5;
}
table.blueTable thead {
  background: #1C6EA4;
  background: -moz-linear-gradient(top, #5592bb 0%, #327cad 66%, #1C6EA4 100%);
  background: -webkit-linear-gradient(top, #5592bb 0%, #327cad 66%, #1C6EA4 100%);
  background: linear-gradient(to bottom, #5592bb 0%, #327cad 66%, #1C6EA4 100%);
  border-bottom: 2px solid #444444;
}
table.blueTable thead th {
  font-size: 15px;
  font-weight: bold;
  color: #FFFFFF;
  text-align: center;
  border-left: 2px solid #D0E4F5;
}
table.blueTable thead th:first-child {
  border-left: none;
}









    table.minimalistBlack {
  font-family: 'Poppins', sans-serif;
  border: 3px solid #000000;
  
  width: 100%;
  height: 200px;
  text-align: center;
  border-collapse: collapse;
}
table.minimalistBlack td, table.minimalistBlack th {
  border: 1px solid #000000;
  padding: 7px 7px;
}
table.minimalistBlack tbody td {
  font-size: 13px;
  color: #000000;
}
table.minimalistBlack tr:nth-child(even) {
  background: #2B93FF;
}
table.minimalistBlack thead {
  background: #000000;
  border-bottom: 3px solid #000000;
}
table.minimalistBlack thead th {
  font-size: 16px;
  font-weight: normal;
  color: #2B93FF;
  text-align: center;
  background: #000000
}
table.minimalistBlack tfoot td {
  font-size: 14px;
}







table.grayTable {
    font-family: 'Poppins' , sans-serif;
  border: 2px solid #FFFFFF;
  width: 100%;
  text-align: center;
  border-collapse: collapse;
}
table.grayTable td, table.grayTable th {
  border: 1px solid #000000;
  padding: 3px 4px;
}
table.grayTable tbody td {
  font-size: 16px;
  color: #000000;
}
table.grayTable tr:nth-child(even) {
  background: #C9C9C9;
}
table.grayTable thead {
  background: #FFFFFF;
  border-bottom: 3px solid #000000;
}
table.grayTable thead th {
  font-size: 15px;
  font-weight: bold;
  color: #333333;
  text-align: center;
  border-left: 3px solid #000000;
}
table.grayTable thead th:first-child {
  border-left: none;
}

table.grayTable tfoot td {
  font-size: 14px;
}


</style>



<style>

    table.fancyTable a:link {
	color: #666;
	font-weight: bold;
	text-decoration:none;
}
table.fancyTable a:visited {
	color: #999999;
	font-weight:bold;
	text-decoration:none;
}
table.fancyTable a:active,
table.fancyTable a:hover {
	color: #bd5a35;
	text-decoration:underline;
}
table.fancyTable {
	font-family:Arial, Helvetica, sans-serif;
	color:#666;
	font-size:12px;
	text-shadow: 1px 1px 0px #fff;
	background:#eaebec;
	margin:20px;
	border:#ccc 1px solid;

	-moz-border-radius:3px;
	-webkit-border-radius:3px;
	border-radius:3px;

	-moz-box-shadow: 0 1px 2px #d1d1d1;
	-webkit-box-shadow: 0 1px 2px #d1d1d1;
	box-shadow: 0 1px 2px #d1d1d1;
}
table.fancyTable th {
	padding:21px 25px 22px 25px;
	border-top:1px solid #fafafa;
	border-bottom:1px solid #e0e0e0;

	background: #ededed;
	background: -webkit-gradient(linear, left top, left bottom, from(#ededed), to(#ebebeb));
	background: -moz-linear-gradient(top,  #ededed,  #ebebeb);
}
table.fancyTable th:first-child {
	text-align: left;
	padding-left:20px;
}
table.fancyTable tr:first-child th:first-child {
	-moz-border-radius-topleft:3px;
	-webkit-border-top-left-radius:3px;
	border-top-left-radius:3px;
}
table.fancyTable tr:first-child th:last-child {
	-moz-border-radius-topright:3px;
	-webkit-border-top-right-radius:3px;
	border-top-right-radius:3px;
}
table.fancyTable tr {
	text-align: center;
	padding-left:20px;
}
table.fancyTable td:first-child {
	text-align: left;
	padding-left:20px;
	border-left: 0;
}
table.fancyTable td {
	padding:18px;
	border-top: 1px solid #ffffff;
	border-bottom:1px solid #e0e0e0;
	border-left: 1px solid #e0e0e0;

	background: #fafafa;
	background: -webkit-gradient(linear, left top, left bottom, from(#fbfbfb), to(#fafafa));
	background: -moz-linear-gradient(top,  #fbfbfb,  #fafafa);
}
table.fancyTable tr.even td {
	background: #f6f6f6;
	background: -webkit-gradient(linear, left top, left bottom, from(#f8f8f8), to(#f6f6f6));
	background: -moz-linear-gradient(top,  #f8f8f8,  #f6f6f6);
}
table.fancyTable tr:last-child td {
	border-bottom:0;
}
table.fancyTable tr:last-child td:first-child {
	-moz-border-radius-bottomleft:3px;
	-webkit-border-bottom-left-radius:3px;
	border-bottom-left-radius:3px;
}
table.fancyTable tr:last-child td:last-child {
	-moz-border-radius-bottomright:3px;
	-webkit-border-bottom-right-radius:3px;
	border-bottom-right-radius:3px;
}
table.fancyTable tr:hover td {
	background: #f2f2f2;
	background: -webkit-gradient(linear, left top, left bottom, from(#f2f2f2), to(#f0f0f0));
	background: -moz-linear-gradient(top,  #f2f2f2,  #f0f0f0);	
}
</style>

<style>
    
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


    <form id="form1" runat="server">
        <div>
            <table class ="minimalistBlack">
               <%-- Josh's Tables: "blueTable" , "minimalistBlack" , "grayTable" , "fancyTable"  --%>
                <%-- Brandon's Tables: "one" , "two" , "three" , "four" --%>
                <thead>
                <tr>
                <th>First Name</th>
                <th>Project ID</th>
                <th>Monday Hours</th>
                <th>Tuesday Hours</th>
                <th>Wednesday Hours</th>
                <th>Thursday Hours</th>
                <th>Friday Hours</th>
                <th>Saturday Hours</th>
                <th>Sunday Hours</th>
                </tr>
                    </thead>

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
  





    
        <div>
            <table class="fancyTable">
                <thead>
                    <tr>
                        <th>Project Name</th>
                        <th>Budget</th>
                        <th>Start Date</th>
                        <th>End Date</th>
                    </tr>
                </thead>

                <asp:Repeater ID ="Repeater3" runat="server">
                    <ItemTemplate>
                        <tr>
                                  <td><%# Eval("pname")%></td>
                            <td><%# Eval("budget")%></td>
                            <td><%# Eval("startdate") %></td>
                            <td><%# Eval("enddate") %></td>


                        </tr>
                    </ItemTemplate>

                </asp:Repeater>
            </table>
        </div>


        <div id="piechart"></div>

<script type="text/javascript" src="https://www.gstatic.com/charts/loader.js"></script>

<script type="text/javascript">
// Load google charts
google.charts.load('current', {'packages':['corechart']});
google.charts.setOnLoadCallback(drawChart);

// Draw the chart and set the chart values
function drawChart() {
  var data = google.visualization.arrayToDataTable([
  ['Task', 'Hours per Day'],
  ['Work', 8],
  ['Eat', 2],
  ['TV', 4],
  ['Gym', 2],
  ['Sleep', 8]
]);

  // Optional; add a title and set the width and height of the chart
  var options = {'title':'My Average Day', 'width':550, 'height':400};

  // Display the chart inside the <div> element with id="piechart"
  var chart = new google.visualization.PieChart(document.getElementById('piechart'));
  chart.draw(data, options);
}
</script>



    
      </form>


</body>
</html>