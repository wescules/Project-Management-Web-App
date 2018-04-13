<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Timesheet.aspx.cs" Inherits="Timesheet" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <style>
table {
    font-family: arial, sans-serif;
    border-collapse: collapse;
    width: 100%;
}

td, th {
    border: 1px solid #dddddd;
    text-align: left;
    padding: 8px;
}

tr:nth-child(odd) {
    background-color: #1c3e75;
    color: white;
}
</style>
<meta charset="utf-8">
        <meta charset="utf-8">
        <meta name="viewport" content="width=device-width, initial-scale=1.0">
        <meta http-equiv="X-UA-Compatible" content="IE=edge">

    <title>Collapsible sidebar using Bootstrap 3</title>

    <!-- Bootstrap CSS CDN -->
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css">
    <!-- Our Custom CSS -->
    <link rel="stylesheet" type="text/css" href="../css/DashUI.css" />
    <link rel="stylesheet" href="//cdnjs.cloudflare.com/ajax/libs/timepicker/1.3.5/jquery.timepicker.min.css">
    <link rel="stylesheet" type="text/css" href="css/jquery.timepicker.css" />
    <link href="css/font-awesome.css" rel="stylesheet" type="text/css" />

</head>
<body>

        

    <div class="wrapper">
        <!-- Sidebar Holder -->
        <nav id="sidebar">
            <div class="sidebar-header">
                <h3>DASH</h3>
                <strong>DASH</strong>
            </div>

            <ul class="list-unstyled components">
                <li class="active">


                    <a href="../Dashboard.aspx">
                        <i class="glyphicon glyphicon-briefcase"></i>
                        Dashboard
                        </a>
                    <a href="#homeSubmenu" data-toggle="collapse" aria-expanded="false">
                        <i class="glyphicon glyphicon-home"></i>
                        Departments
                        </a>
                  
                        <%-- Need repeater here --%>
                        <ul class="collapse list-unstyled" id="homeSubmenu">
                            <asp:Repeater ID="Repeater2" runat="server">
                                <ItemTemplate>
                                    <li><a href="../Projects/Project.aspx?Name=+<%# Eval("DepartmentID")%>"><%# Eval("DepartmentName")%></a></li>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ul>
                    </li>
                    <li>
                        
                        <a href="#pageSubmenu" data-toggle="collapse" aria-expanded="false">
                            <i class="glyphicon glyphicon-duplicate"></i>
                            Private Boards
                        </a>
                    <ul class="collapse list-unstyled" id="pageSubmenu">
                        <asp:Repeater ID="Repeater3" runat="server">
                            <ItemTemplate>
                                <li><a href="../Admin/AdminTasks.aspx?Name=+<%# Eval("ProjectID")%>"><%# Eval("ProjectName")%></a></li>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ul>
                </li>
                <li>
                    <a href="../Timesheet.aspx">
                        <i class="glyphicon glyphicon-link"></i>
                        TimeSheet
                        </a>
                </li>

            </ul>

            <%--<ul class="list-unstyled CTAs">
                    <li><a href="https://bootstrapious.com/tutorial/files/sidebar.zip" class="download">Download source</a></li>
                    <li><a href="https://bootstrapious.com/p/bootstrap-sidebar" class="article">Back to article</a></li>
                </ul>--%>
        </nav>

        <!-- Page Content Holder -->
        <div id="content">

            <nav class="navbar navbar-default">
                <div class="container-fluid">

                    <div class="navbar-header">
                        <button type="button" id="sidebarCollapse" class="btn btn-info navbar-btn">
                            <i class="glyphicon glyphicon-align-left"></i>
                            <span>Toggle Sidebar</span>
                        </button>
                    </div>
  
                <form name="frm" runat="server">
                    
                    <table id="timesheet">
                        <tr>
                            <th>Project</th>
                            <th>Sunday</th>
                            <th>Monday</th>
                            <th>Tuesday</th>
                            <th>Wednesday</th>
                            <th>Thursday</th>
                            <th>Friday</th>
                            <th>Saturday</th>
                        </tr>
                        <tr>

                        </tr>
                        
                    </table>
                    
                    <!--<asp:DropDownList ID="ddlSubject" runat="server" AppendDataBoundItems="true">
                        <asp:ListItem Text="<Select Project>" Value="0" />
                    </asp:DropDownList>

                    <label for="Starttime">Start Time: </label>
                    <input id="Starttime" type="time" name="Starttime">

                    <br />
                    <br />
                    <label for="EndTime">End Time: </label>
                    <input id="EndTime" type="time" name="EndTime">
                    <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-primary" Text="Submit" OnClick="Submitbtn_Click" />--%>

                    <script src="https://ajax.googleapis.com/ajax/libs/jquery/2.1.1/jquery.min.js"></script>
<script src="https://cdnjs.cloudflare.com/ajax/libs/moment.js/2.18.1/moment.min.js"></script>

                     <br /> <br />
                     <asp:Label ID="Label2" runat="server" Text="Start Time:"></asp:Label>
                    <asp:TextBox type="datetime-local" name="input-time" ID="starttime" runat="server"></asp:TextBox>
                    <br /> <br />
                    <asp:Label ID="Label3" runat="server" Text="End Time:" ></asp:Label>
                    <asp:TextBox ID="endtime" type="datetime-local" onchange="myFunction()" runat="server"></asp:TextBox>
                     <br /> <br />
                    <asp:Label ID="Label1" runat="server" Text="Total Hours:"></asp:Label>
                    <asp:TextBox type="text" ID="totalhours" placeholder="Total Hours" runat="server"></asp:TextBox>
                    <br /> <br />
                    
                    <asp:Button ID="Button1" runat="server" Text="Submit" OnClick="Submitbtn_Click" CssClass="btn btn-primary"/>-->
                </form>

                
                
            </div>
        </div>

    <!-- jQuery CDN -->
    <script src="https://code.jquery.com/jquery-1.12.0.min.js"></script>
    <!-- Bootstrap Js CDN -->
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            $('#sidebarCollapse').on('click', function () {
                $('#sidebar').toggleClass('active');
            });
        });
         </script>
    <script type="text/javascript">
        function myFunction() {
            var initialTime = document.getElementById("starttime");
            //
            // test if the start time is not empty
            //
            if (initialTime.value.trim() == '') {
                document.getElementById("starttime").focus();
                return;
            }
            var initialTimeFormat = moment(initialTime.value);
            var endTime = document.getElementById("endtime");
            var endTimeFormat = moment(endTime.value);
            var totalHours = endTimeFormat.diff(initialTimeFormat, "hours");


            $("#totalhours").val(totalHours);
        }
        </script>
</body>
</html>
