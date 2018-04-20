<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Search.aspx.cs" Inherits="Admin_Search" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">

    <title>Collapsible sidebar using Bootstrap 3</title>

    <!-- Bootstrap CSS CDN -->
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css">
    <!-- Our Custom CSS -->
    <link rel="stylesheet" type="text/css" href="../css/DashUI.css" />
    <link rel="stylesheet" type="text/css" href="../css/dashboard.css" />
    <link rel="stylesheet" type="text/css" href="../css/Search.css" />
        <link rel="stylesheet" type="text/css" href="../css/PopUp Input.css" />

    <style>
        .navbar {
            padding: 15px 10px;
            background: transparent !important;
            border: none;
            border-radius: 0;
            margin-bottom: 40px;
            box-shadow: 1px 1px 3px rgba(0, 0, 0, 0.1);
        }
    </style>
</head>
<body>

    <form id="Form1" runat="server">

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
                         <li><a><div id="contact">Add Private Board</div>

                        <div id="contactForm">
                            <h3>Add New Private Board</h3>
                            <asp:Label ID="Label313" runat="server" Text="Name:"></asp:Label>
                            <asp:TextBox name="Title" ID="ProjName" runat="server" PlaceHolder="Name:"></asp:TextBox>
                            <br />
                            <asp:Label ID="Label312" runat="server" Text="Start Date:"></asp:Label>
                            <asp:TextBox type="datetime-local" name="StartDate1" ID="StartDate" runat="server"></asp:TextBox>
                            <br />
                            <asp:Label ID="Label13" runat="server" Text="End Date:"></asp:Label>
                            <asp:TextBox type="datetime-local" name="EndDate1" ID="EndDate" runat="server"></asp:TextBox>
                            <br />
                            <asp:Button ID="taskbutton" runat='server' type="button" class='btn btn-success' CommandName='taskform' Text='Submit' OnClick="button2_Click"></asp:Button>
                        </div></a></li>
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
                                    <asp:ImageButton ID="Button1" runat="server" CssClass="search-icon" OnClick="Search_Click" OnClientClick="searchToggle(this, event);" ImageUrl="../img/search.png" />
                                
                                <%--<input type="text" class="search-input" placeholder="Type to search" />--%>

                                <%--<button class="search-icon" onclick="searchToggle(this, event);"><span></span></button>--%>
                            </div>

                        </div>

                    </div>
                </div>
            </nav>

            <div class="container projects">
                <div class="projects-inner">
                    <header class="projects-header">
                        <div class="title">Search Results</div>

                        <div class="count">|
                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("cnt")%>'></asp:Label>
                            Results</div>

                    </header>
                    <table class="projects-table">
                        <thead>
                            <tr>
                                <th>Project</th>
                                <th>Deadline</th>
                                <th>Leader + Team</th>
                                <th>Budget</th>
                                <th>Status</th>
                            </tr>
                        </thead>


                        <asp:Repeater ID="Repeater1" runat="server">
                            <ItemTemplate>
                                <tr class="danger-item">
                                    <td>
                                        <p><%# Eval("TaskName")%></p>
                                        <p>Google</p>
                                    </td>
                                    <td>
                                        <p><%# Eval("DateCompleted") %></p>
                                        <p class="danger-text">Overdue</p>
                                    </td>
                                    <td class="member">
                                        <figure>
                                            <img src="https://s3-us-west-2.amazonaws.com/s.cdpn.io/584938/people_8.png" />
                                        </figure>
                                        <div class="member-info">
                                            <p>Myrtle Erickson</p>
                                            <p>UK Design Team</p>
                                        </div>
                                    </td>
                                    <td>
                                        <p>$4,670</p>
                                        <p>Paid</p>
                                    </td>
                                    <td class="status"><span class="status-text status-blue">Early stages</span>

                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>


                    </table>
                </div>
            </div>
        </div>
    </div>
    </form>



    <script type="text/javascript">var Selectize = /** @class */ (function () {
            function Selectize() {
                this.init();
            }
            Selectize.prototype.init = function () {
                var initValue;
                $('.action-box').selectric({
                    onInit: function (element) {
                        initValue = $(this).val();
                    },
                    onChange: function (element) {
                        if ($(this).val() !== initValue)
                            $(element).parents('form').submit();
                    }
                });
            };
            return Selectize;
        }());
        new Selectize();
</script>
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
    <%--        <script type="text/javascript">
            $(function() {

            var $sidebar   = $("#sidebar"), 
                $window    = $(window),
                offset     = $sidebar.offset(),
                topPadding = 0;

            $window.scroll(function() {
                if ($window.scrollTop() > offset.top) {
                    $sidebar.stop().animate({
                        marginTop: $window.scrollTop() - offset.top + topPadding
                    });
                } else {
                        $sidebar.stop().animate({
                         marginTop: 0
                           });
                  }
                });
    
          });
        </script>--%>
    <script type="text/javascript">
        function searchToggle(obj, evt) {
            var container = $(obj).closest('.search-wrapper');
            if (!container.hasClass('active')) {
                container.addClass('active');
                evt.preventDefault();
            }


        }
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
        <script>
        $(function () {

            // contact form animations
            $('#contact2').click(function () {
                $('#contactForm2').fadeToggle();
            })
            $(document).mouseup(function (e) {
                var container = $("#contactForm2");

                if (!container.is(e.target) // if the target of the click isn't the container...
                    && container.has(e.target).length === 0) // ... nor a descendant of the container
                {
                    container.fadeOut();
                }
            });

        });
    </script>
</body>
</html>
