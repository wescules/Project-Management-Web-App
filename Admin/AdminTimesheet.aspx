<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AdminTimesheet.aspx.cs" Inherits="Admin_AdminTimesheet" %>

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
        <link rel="stylesheet" type="text/css" href="../css/Search.css" />
    <link rel="stylesheet" type="text/css" href="../css/DashUI.css" />
    <link rel="stylesheet" href="//cdnjs.cloudflare.com/ajax/libs/timepicker/1.3.5/jquery.timepicker.min.css">
    <link rel="stylesheet" type="text/css" href="css/jquery.timepicker.css" />
    <link href="css/font-awesome.css" rel="stylesheet" type="text/css" />
        <link rel="stylesheet" type="text/css" href="../css/PopUp Input.css" />

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
    <form name="frm" runat="server">



    <div class="wrapper">
        <!-- Sidebar Holder -->
        <nav id="sidebar">
            <div class="sidebar-header">
                <h3>DASH</h3>
                <strong>DASH</strong>
            </div>

            <ul class="list-unstyled components">
                <li class="active">


                    <a href="../Admin/AdminDashboard.aspx">
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
                                <li><a href="../Admin/AdminProject.aspx?Name=+<%# Eval("DepartmentID")%>"><%# Eval("DepartmentName")%></a></li>
                            </ItemTemplate>
                        </asp:Repeater>
                                              <li><a>
                            <div id="contact1">Add Department</div>

                            <div id="contactForm1">
                                <h3>Add New Department</h3>
                                <asp:Label ID="Label12" runat="server" Text="Name:"></asp:Label>
                                <asp:TextBox name="Title" ID="ProjNameDept" runat="server" PlaceHolder="Department Name:"></asp:TextBox>
                                <br />
                                <br />
                                <asp:Button ID="Button3" runat='server' type="button" class='btn btn-success' CommandName='taskform' Text='Submit' OnClick="AddNewDepartmentButton"></asp:Button>
                            </div>
                                              </a></li>
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
                        <li><a>
                            <div id="contact">Add Private Board</div>

                            <div id="contactForm">
                                <h3>Add New Private Board</h3>
                                <asp:Label ID="Label313" runat="server" Text="Name:"></asp:Label>
                                <asp:TextBox name="Title" ID="ProjName" runat="server" PlaceHolder="Name:"></asp:TextBox>
                                <br />
                                <asp:Label ID="Label3" runat="server" Text="Start Date:"></asp:Label>
                                <asp:TextBox type="datetime-local" name="StartDate" ID="StartDate" runat="server"></asp:TextBox>
                                <br />
                                <asp:Label ID="Label4" runat="server" Text="End Date:"></asp:Label>
                                <asp:TextBox type="datetime-local" name="EndDate" ID="EndDate" runat="server"></asp:TextBox>
                                <br />
                                <br />
                                <asp:Button ID="Button1" runat='server' type="button" class='btn btn-success' CommandName='taskform' Text='Submit' OnClick="button2_Click"></asp:Button>
                            </div>
                        </a></li>
                    </ul>
                </li>
                <li>
                    <a href="../Admin/AdminTimesheet.aspx">
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
                        <nav class="navbar">
                <div class="container-fluid">

                    <div class="navbar-header">
                        <div class="search-wrapper">
                            <div class="input-holder">
                                    <asp:TextBox ID="searchInput" runat="server" CssClass="search-input" placeholder="Type to search"></asp:TextBox>
                                    <asp:ImageButton ID="ImageButton1" runat="server" CssClass="search-icon" OnClick="Search_Click" OnClientClick="searchToggle(this, event);" ImageUrl="../img/search.png" />
                                
                                <%--<input type="text" class="search-input" placeholder="Type to search" />--%>

                                <%--<button class="search-icon" onclick="searchToggle(this, event);"><span></span></button>--%>
                            </div>

                        </div>

                    </div>
                </div>
            </nav>
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
                    
            



        </div>
    </div>


</form>

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

    <script>
        $(function () {

            // contact form animations
            $('#contact').click(function () {
                $('#contactForm').fadeToggle();
            })
            $(document).mouseup(function (e) {
                var container = $("#contactForm");

                if (!container.is(e.target) // if the target of the click isn't the container...
                    && container.has(e.target).length === 0) // ... nor a descendant of the container
                {
                    container.fadeOut();
                }
            });

        });
    </script>
        <script>
        $(function () {

            // contact form animations
            $('#contact1').click(function () {
                $('#contactForm1').fadeToggle();
            })
            $(document).mouseup(function (e) {
                var container = $("#contactForm1");

                if (!container.is(e.target) // if the target of the click isn't the container...
                    && container.has(e.target).length === 0) // ... nor a descendant of the container
                {
                    container.fadeOut();
                }
            });

        });
    </script>

                <script type="text/javascript">
        function searchToggle(obj, evt) {
            var container = $(obj).closest('.search-wrapper');
            if (!container.hasClass('active')) {
                container.addClass('active');
                evt.preventDefault();
            }


        }
    </script>
    
</body>
</html>