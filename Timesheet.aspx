<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Timesheet.aspx.cs" Inherits="Timesheet" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
    <style>
        @import url(https://fonts.googleapis.com/css?family=Roboto:400,500,700);
        
        .border-none{
            border: none;
        }

        .table th, .table td { 
            border-top: none !important; 
        }

        #app{
            font-family: 'Roboto', sans-serif;
        }

        #ddDate{
            width: 15%;
        }
    </style>

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

                    <div class="collapse navbar-collapse" id="bs-example-navbar-collapse-1">
                        <ul class="nav navbar-nav navbar-right">
                            <li><a href="#">Page</a></li>
                            <li><a href="#">Page</a></li>
                            <li><a href="#">Page</a></li>
                            <li><a href="#">Page</a></li>
                        </ul>
                    </div>
                </div>
            </nav>
            <form name="frm" runat="server">
                <h1>Timesheet</h1>
                <h4> <asp:Label ID="week" runat="server"></asp:Label></h4>
                    <div id="app">
           
                      <br />
                      <br />
                      <table class="table table-condensed">
                      <thead>
                        
                        <tr>
                          <th>Project</th>
                          <th>Sun</th>
                          <th>Mon</th>
                          <th>Tue</th>
                          <th>Wed</th>
                          <th>Thu</th>
                          <th>Fri</th>
                          <th>Sat</th>
                          <th>Total</th>
                        </tr>
                      </thead>
                      <tbody>
                        <asp:Repeater ID="Repeater1" runat="server">
                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <asp:Label ID="projID" style="display:none" runat="server" Text='<%# Eval("ProjectID")%>' ></asp:Label>
                                        <asp:Label ID="projName" runat="server" Text='<%# Eval("ProjectName")%>' ></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="sunHours" runat="server" CssClass="form-control hours sun" type="number" min="0" max="24" step=".5" Text='<%# Eval("SunHours")%>'></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="monHours" runat="server" CssClass="form-control hours mon" type="number" min="0" max="24" step=".5" Text='<%# Eval("MonHours")%>'></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tueHours" runat="server" CssClass="form-control hours tue" type="number" min="0" max="24" step=".5" Text='<%# Eval("TuesHours")%>'></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="wedHours" runat="server" CssClass="form-control hours wed" type="number" min="0" max="24" step=".5" Text='<%# Eval("WedsHours")%>'></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="thuHours" runat="server" CssClass="form-control hours thu" type="number" min="0" max="24" step=".5" Text='<%# Eval("ThursHours")%>'></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="friHours" runat="server" CssClass="form-control hours fri" type="number" min="0" max="24" step=".5" Text='<%# Eval("FriHours")%>'></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="satHours" runat="server" CssClass="form-control hours sat" type="number" min="0" max="24" step=".5" Text='<%# Eval("SatHours")%>'></asp:TextBox>
                                    </td>
                                    <td class="total">

                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
  
                        
                      </tbody>
                    </table>
                    <div class="col-md-12">
              
                        <asp:Button ID="submit" runat="server" type="button" CssClass="btn btn-success" Text="Save" OnClick="Submit_Click"/>
                
                    </div>
                  </div>
                    
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

    <script type="text/javascript">
        $(document).ready(function () {
            window.setInterval(function () {
                //iterate through each row in the table
                $('tr').each(function () {

                    var sum = 0;
                    $(this).find('.form-control').filter('.hours').each(function () {
                        
                        //find the combat elements in the current row and sum it
                        var hours = $(this).val();

                        if (!isNaN(hours) && hours.length !== 0) {
                            sum += parseFloat(hours);
                        }
                    
                    });

                    //set the value of currents rows sum to the total element in the current row
                    $('.total', this).html(sum);
                });
            }, 500);
            
        });
    </script>

    <script type="text/javascript">
        //$(document).ready(function () {
        //    $('#submit').click(function () {
        //        var hoursList = [];

        //        $('tr', 'tbody').each(function (index, element) {
        //            hoursList[index] = { project: "", sunHours: "", monHours: "", tueHours: "", wedHours: "", thuHours: "", friHours: "", satHours: "" };
        //            hoursList[index]['project'] = $(element).find('#Repeater1_projID_'+index).text();
        //            hoursList[index]['sunHours'] = $(element).find('#Repeater1_sunHours_' + index).val();
        //            hoursList[index]['monHours'] = $(element).find('#Repeater1_monHours_' + index).val();
        //            hoursList[index]['tueHours'] = $(element).find('#Repeater1_tueHours_' + index).val();
        //            hoursList[index]['wedHours'] = $(element).find('#Repeater1_wedHours_' + index).val();
        //            hoursList[index]['thuHours'] = $(element).find('#Repeater1_thuHours_' + index).val();
        //            hoursList[index]['friHours'] = $(element).find('#Repeater1_friHours_' + index).val();
        //            hoursList[index]['satHours'] = $(element).find('#Repeater1_satHours_' + index).val();
        //        });

        //        console.log(JSON.stringify(hoursList));

        //        $.ajax({
        //            type: "POST",
        //            url: "/Timesheet.aspx/ParseTimesheetData",
        //            data: JSON.stringify({ "timesheetData": hoursList }),
        //            contentType: "application/json; charset=utf-8",
        //            complete: function () {
        //                //alert("success");
        //            },
        //            failure: function (XMLHttpRequest, textStatus, errorThrown) {
        //                alert("Status: " + textStatus); alert("Error: " + errorThrown);
        //            }
        //        });
        //    });
        //});

            
    </script>


    
</body>
</html>
