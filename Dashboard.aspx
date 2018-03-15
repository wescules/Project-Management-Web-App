<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Dashboard.aspx.cs" Inherits="Dashboard" %>

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
    <link rel="stylesheet" type="text/css" href="css/DashUI.css" />
    <style>
        .card-list {
  margin-top: 15px;
  width: 100%;
}
.card-list:before,
.card-list:after {
  content: " ";
  display: table;
}
.card-list:after {
  clear: both;
}
.card {
  border: 5px solid #1b2431;
  border-radius: 8px;
  color: white;
  float: left;
  padding: 10px;
  position: relative;
  width: 25%;
}
.card .glyphicon {
  color: white;
  font-size: 28px;
  opacity: 0.3;
  position: absolute;
  right: 13px;
  top: 13px;
}
.card .stat {
  border-top: 1px solid rgba(255, 255, 255, 0.3);
  font-size: 8px;
  margin-top: 25px;
  padding: 10px 10px 0;
  text-transform: uppercase;
}
.card .title {
  display: inline-block;
  font-size: 8px;
  padding: 10px 10px 0;
  text-transform: uppercase;
}
.card .value {
  font-size: 28px;
  padding: 0 10px;
}
.card.blue {
  background-color: #2298F1;
}
.card.green {
  background-color: #66B92E;
}
.card.orange {
  background-color: #DA932C;
}
.card.red {
  background-color: #D65B4A;
}
.projects {
  background-color: #273142;
  border: 4px solid #1b2431;
  width: 100%;
}
.projects-inner {
  border: 1px solid #313d4f;
  border-radius: 4px;
}
.projects-header {
  color: white;
  padding: 22px;
}
.projects-header .count,
.projects-header .title {
  display: inline-block;
}
.projects-header .count {
  color: #738297;
}
.projects-header .glyphicon {
  cursor: pointer;
  float: right;
  font-size: 16px;
  margin: 5px 0;
}
.projects-header .title {
  font-size: 21px;
}
.projects-header .title + .count {
  margin-left: 5px;
}
.projects-table {
  width: 100%;
}
.projects-table td,
.projects-table th {
  color: white;
  padding: 10px 22px;
  vertical-align: middle;
}
.projects-table td p {
  font-size: 12px;
}
.projects-table td p:last-of-type {
  color: #738297;
  font-size: 11px;
}
.projects-table th {
  background-color: #313d4f;
}
.projects-table tr:not(:last-of-type) {
  border-bottom: 1px solid #313d4f;
}
.projects-table .member figure,
.projects-table .member .member-info {
  display: inline-block;
  vertical-align: top;
}
.projects-table .member figure + .member-info {
  margin-left: 7px;
}
.projects-table .member img {
  border-radius: 50%;
  height: 32px;
  width: 32px;
}
.projects-table .status > form {
  float: right;
}
.projects-table .status-text {
  display: inline-block;
  font-size: 12px;
  margin: 11px 0;
  padding-left: 20px;
  position: relative;
}
.projects-table .status-text:before {
  border: 3px solid;
  border-radius: 50%;
  content: "";
  height: 14px;
  left: 0;
  position: absolute;
  top: 1px;
  width: 14px;
}
.projects-table .status-text.status-blue:before {
  border-color: #1C93ED;
}
.projects-table .status-text.status-green:before {
  border-color: #66B92E;
}
.projects-table .status-text.status-orange:before {
  border-color: #DA932C;
}
.projects-table .status-text.status-red:before {
  border-color: #D65B4A;
}
.selectric {
  background-color: transparent;
  border-color: #313d4f;
  border-radius: 4px;
}
.selectric .label {
  color: #738297;
  line-height: 34px;
  margin-right: 10px;
  text-align: left;
}
.selectric-wrapper {
  float: right;
  width: 150px;
}
.danger-item {
  border-left: 4px solid #A84D43;
}
.danger-text {
  color: #A84D43 !important;
}
body {
  background-color: #1b2431;
  color: #202020;
  font-family: "Montserrat", "Helvetica", "Open Sans", "Arial";
  font-size: 13px;
}
p,
figure,
ul,
li {
  margin: 0;
  padding: 0;
}
.container {
  margin-left: auto;
  margin-right: auto;
  padding: 0;
  max-width: 960px;
  width: 100%;
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
                        <li><a href="../Projects/Project.aspx">Department 1</a></li>
                        <li><a href="#">Department 2</a></li>
                        <li><a href="#">Department 3</a></li>
                    </ul>
                </li>
                <li>

                    <a href="#pageSubmenu" data-toggle="collapse" aria-expanded="false">
                        <i class="glyphicon glyphicon-duplicate"></i>
                        Private Boards
                    </a>
                    <ul class="collapse list-unstyled" id="pageSubmenu">
                        <li><a href="#">Page 1</a></li>
                        <li><a href="#">Page 2</a></li>
                        <li><a href="#">Page 3</a></li>
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

           <%-- <nav class="navbar navbar-default">
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

            <h2>Collapsible Sidebar Using Bootstrap 3</h2>
            <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.</p>
            <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.</p>
            <h2>TOP 10 Upcoming Deadlines:  </h2>
            <br />
            <br />
            <asp:Repeater ID="Repeater1" runat="server">
                <ItemTemplate>
                    <div style="border: 2px dotted #c0c0c0; padding: 10px; margin: 10px;">
                        <b><%# Eval("AssignmentNote")%> </b><b><%# Eval("AssignmentEnd") %></b><br />
                    </div>
                </ItemTemplate>
            </asp:Repeater>--%>
            
            <div class="container card-list">
                <div class="card blue">
                    <div class="title">all projects</div>
                    <span class="glyphicon glyphicon-upload"></span>
                    <div class="value">89</div>
                    <div class="stat"><b>13</b>% increase</div>
                </div>
                <div class="card green">
                    <div class="title">team members</div>
                    <span class="glyphicon glyphicon-upload"></span>
                    <div class="value">5,990</div>
                    <div class="stat"><b>4</b>% increase</div>
                </div>
                <div class="card orange">
                    <div class="title">total budget</div>
                    <span class="glyphicon glyphicon-download"></span>
                    <div class="value">$80,990</div>
                    <div class="stat"><b>13</b>% decrease</div>
                </div>
                <div class="card red">
                    <div class="title">new customers</div>
                    <span class="glyphicon glyphicon-download"></span>
                    <div class="value">3</div>
                    <div class="stat"><b>13</b>% decrease</div>
                </div>
            </div>
            <div class="container projects">
                <div class="projects-inner">
                    <header class="projects-header">
                        <div class="title">Ongoing Projects</div>
                        <div class="count">| 32 Projects</div>
                        <span class="glyphicon glyphicon-download-alt"></span>
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
                                        <p><%# Eval("AssignmentNote")%></p>
                                        <p>Google</p>
                                    </td>
                                    <td>
                                        <p><%# Eval("AssignmentEnd") %></p>
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








                                <%--<div style="border: 2px dotted #c0c0c0; padding: 10px; margin: 10px;">
                                    <b><%# Eval("AssignmentNote")%> </b><b><%# Eval("AssignmentEnd") %></b><br />
                                </div>--%>
                            </ItemTemplate>
                        </asp:Repeater>
                        
                       
                    </table>
                </div>
            </div>
        </div>
    </div>




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
</body>
</html>
