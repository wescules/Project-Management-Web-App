<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Register.aspx.cs" Inherits="_Default"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script runat="server">
    protected void TextBox3_TextChanged(object sender, EventArgs e)
    {
    }
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <!--[if lt IE 7]> <html class="no-js ie6 oldie" lang="en"> <![endif]-->
    <!--[if IE 7]>    <html class="no-js ie7 oldie" lang="en"> <![endif]-->
    <!--[if IE 8]>    <html class="no-js ie8 oldie" lang="en"> <![endif]-->
    <!--[if IE 9]> <html class="no-js ie9 oldie" lang="en"> <![endif]-->
   
    <!-- Set the viewport width to device width for mobile -->
    
    
    <title>Project Manager</title>
    <!-- ============ Google fonts ============ -->
    <link href='http://fonts.googleapis.com/css?family=EB+Garamond' rel='stylesheet'
        type='text/css' />
    <link href='http://fonts.googleapis.com/css?family=Open+Sans:400,600,700,300,800'
        rel='stylesheet' type='text/css' />
    <!-- ============ Add custom CSS here ============ -->
    <link href="css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="css/style.css" rel="stylesheet" type="text/css" />
    <link href="css/font-awesome.css" rel="stylesheet" type="text/css" />
    <link href="css/login.css" rel="Stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div id="custom-bootstrap-menu" class="navbar navbar-default ">
        <div class="container">
            <div class="navbar-header">
                <!--<a class="navbar-brand" href="#">Project Manager</a>-->

               <img class="navbar-brand"   border="0" alt="W3Schools" src="img/dash.png"/>
                <button type="button" class="navbar-toggle" >
                    <span class="sr-only">Toggle navigation</span><span class="icon-bar"></span><span
                        class="icon-bar"></span><span class="icon-bar"></span>
                </button>
            </div>
            <div class="collapse navbar-collapse navbar-menubuilder">
                <ul class="nav navbar-nav navbar-right">
                    <li><a href="/">Home</a> </li>
                    <li><a href="/products">Products</a> </li>
                    <li><a href="/about-us">About Us</a> </li>
                    <li><a href="/contact">Contact Us</a> </li>
                    <asp:Button ID="Button2" runat="server" CssClass="btn btn-primary" Text="Login" OnClick="login_Click" />
                </ul>
            </div>
        </div>
    </div>
    <div class="container">
        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12 text-center">
            <div id="banner">
                <h1>
                    <strong>Project Manager</strong> for .net developers</h1>
                <h5>
                    <strong></strong></h5>
            </div>
        </div>
        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
            <div class="registrationform">
                <div class="form-horizontal">
                    <fieldset>
                        <legend>Registration Form <i class="fa fa-pencil pull-right"></i></legend>
                        <div class="form-group">
                            <asp:Label ID="Label1" runat="server" Text="Email" CssClass="col-lg-2 control-label"></asp:Label>
                            <div class="col-lg-10">
                                <asp:TextBox ID="txtEmail" type="email" runat="server" placeholder="Email" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator id="emailValidator" runat="server" 
                                    ControlToValidate="txtEmail" ErrorMessage="Email is required" ForeColor="Red">
                                </asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="form-group">
                            <asp:Label ID="Label2" runat="server" Text="Password" CssClass="col-lg-2 control-label"></asp:Label>
                            <div class="col-lg-10">
                                <asp:TextBox ID="txtPass" runat="server" placeholder="Password" CssClass="form-control"
                                    TextMode="Password"></asp:TextBox>
                                <asp:RequiredFieldValidator id="passValidator" runat="server" 
                                    ControlToValidate="txtPass" ErrorMessage="Password is required" ForeColor="Red">
                                </asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="form-group">
                            <asp:Label ID="Label3" runat="server" Text="Confirm Password" CssClass="col-lg-2 control-label"></asp:Label>
                            <div class="col-lg-10">
                                <asp:TextBox ID="txtConfirmPass" runat="server" placeholder="Confirm Password" CssClass="form-control" TextMode="Password"></asp:TextBox>
                                <asp:CompareValidator runat="server" id="comparePass" controltovalidate="txtConfirmPass" controltocompare="txtPass" operator="Equal" errormessage="Passwords do not match" ForeColor="Red"/>
                            </div>
                        </div>
                       <div class="form-group">
                            <asp:Label ID="Label4" runat="server" Text="First Name" CssClass="col-lg-2 control-label"></asp:Label>
                            <div class="col-lg-10">
                                <asp:TextBox ID="txtFName" runat="server" placeholder="First Name" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator id="fNameValidator" runat="server" 
                                    ControlToValidate="txtFName" ErrorMessage="First Name is required" ForeColor="Red">
                                </asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="form-group">
                            <asp:Label ID="Label5" runat="server" Text="Last Name" CssClass="col-lg-2 control-label"></asp:Label>
                            <div class="col-lg-10">
                                <asp:TextBox ID="txtLName" runat="server" placeholder="Last Name" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator id="lNameValidator" runat="server" 
                                    ControlToValidate="txtLName" ErrorMessage="Last Name is required" ForeColor="Red">
                                </asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="form-group">
                            <asp:Label ID="Label6" runat="server" Text="Department" CssClass="col-lg-2 control-label"></asp:Label>
                            <div class="col-lg-10">
                                <asp:DropDownList ID="dptDropDown" runat="server" CssClass="form-control"></asp:DropDownList>
                                <asp:RequiredFieldValidator id="dptValidator" runat="server" 
                                    ControlToValidate="dptDropDown" ErrorMessage="Department is required" ForeColor="Red">
                                </asp:RequiredFieldValidator>
                            </div>
                            
                        </div>
                        <div class="form-group">
                            <asp:Label ID="Label7" runat="server" Text="Positiion" CssClass="col-lg-2 control-label"></asp:Label>
                            <div class="col-lg-10">
                                <asp:TextBox ID="txtPosition" runat="server" placeholder="Position" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator id="postionValidator" runat="server" 
                                    ControlToValidate="txtPosition" ErrorMessage="Postition is required" ForeColor="Red">
                                </asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="form-group">
                            <asp:Label ID="Label8" runat="server" Text="Are You a Manager?" CssClass="col-lg-2 control-label"></asp:Label>
                            <div class="col-lg-10">
                                
                                <asp:RadioButtonList ID="isManagerRadio" runat="server" RepeatDirection="Horizontal" CssClass="form-control">
                                    <asp:ListItem Text="Yes" Value="yes"/>
                                    <asp:ListItem Text="No" Value="no"/>
                                </asp:RadioButtonList>
                                <asp:RequiredFieldValidator id="isManagerValidator" runat="server" 
                                    ControlToValidate="isManagerRadio" ErrorMessage="This field is required" ForeColor="Red">
                                </asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="form-group">
                            <asp:Label ID="Label9" runat="server" Text="Pay Rate" CssClass="col-lg-2 control-label"></asp:Label>
                            <div class="col-lg-10">
                                <asp:TextBox ID="txtPayRate" runat="server" placeholder="Pay Rate" CssClass="form-control"></asp:TextBox>
                                <asp:RangeValidator ControlToValidate="txtPayRate" MinimumValue="0" MaximumValue="100000" Type="Currency"
                                    EnableClientScript="false" Text="Only Numberic allowed,  value must be from 0 to 100000!" ForeColor="Red" runat="server" />
                            </div>
                        </div>
                        <div class="form-group">
                            <asp:Label ID="Label10" runat="server" Text="Start Date" CssClass="col-lg-2 control-label"></asp:Label>
                            <div class="col-lg-10">
                                <asp:TextBox ID="txtStartDate" runat="server" TextMode="Date" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator id="RequiredFieldValidator1" runat="server" 
                                    ControlToValidate="txtStartDate" ErrorMessage="Start Date is required" ForeColor="Red">
                                </asp:RequiredFieldValidator>
                                <asp:CustomValidator runat="server" ID="valDateRange" ControlToValidate="txtStartDate" 
                                    OnServerValidate="valDateRange_ServerValidate" ErrorMessage="Enter valid date" ForeColor="Red"/>
                            </div>
                        </div>

                        
        <div class="col-lg-10 col-lg-offset-2">
			<div class="aro-pswd_info">
				<div id="pswd_info">
					<h4>Password must be requirements</h4>
					<ul>
						<li id="letter" class="invalid">At least <strong>one letter</strong></li>
						<li id="capital" class="invalid">At least <strong>one capital letter</strong></li>
						<li id="number" class="invalid">At least <strong>one number</strong></li>
						<li id="length" class="invalid">Be at least <strong>8 characters</strong></li>
						<li id="space" class="invalid">be<strong> use [~,!,@,#,$,%,^,&,*,-,=,.,;,']</strong></li>
					</ul>
				</div>
			</div>
		</div>
                        <div class="form-group">
                            <div class="col-lg-10 col-lg-offset-2">
                                <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-primary" Text="Submit" OnClick="Submitbtn_Click" />
                                  <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-warning" Text="Cancel" />                              
                            </div>
                        </div>
                    </fieldset>
                </div>
            </div>
        </div>
    </div>
    <script src="js/jquery.js" type="text/javascript"></script>
    <script src="js/bootstrap.min.js" type="text/javascript"></script>
    <script src="js/jquery.backstretch.js" type="text/javascript"></script>
    <script src="js/PasswordValidation.js"  type="text/javascript"></script>
    <script type="text/javascript">
        'use strict';
        /* ========================== */
        /* ::::::: Backstrech ::::::: */
        /* ========================== */
        // You may also attach Backstretch to a block-level element
        $.backstretch(
        [
            "img/44.jpg",
            "img/colorful.jpg",
            "img/34.jpg",
            "img/images.jpg"
        ],
        {
            duration: 4500,
            fade: 1500
        }
    );
    </script>
    </form>
</body>
</html>