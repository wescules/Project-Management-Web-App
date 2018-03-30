<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Project.aspx.cs" Inherits="Projects_Project" %>


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
    <link rel="stylesheet" type="text/css" href="../css/project.css" />

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
                                <li><a href="../Projects/Project.aspx?Name=+<%# Eval("ProjectId")%>"><%# Eval("ProjectName")%></a></li>
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
                                        <p><a href='Tasks.aspx?Name= + <%# Eval("ProjectId")%>'><%# Eval("ProjectName")%></p>
                                        <p>Google</p>
                                    </td>
                                    <td>
                                        <p><%# Eval("ProjectId") %></p>
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

</body>
</html>
