<%@ Page Language="C#" UnobtrusiveValidationMode="None" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="SSIS.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.1.1/jquery.min.js"></script>

    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
    <link rel="stylesheet" type="text/css" href="..https://fonts.googleapis.com/css?family=Montserrat" />
    <link rel="stylesheet" href="Styles/StyleSheet.css" />
    <!--Datatable-->
    <script src="//maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css"></script>
    <script src="https://cdn.datatables.net/1.10.13/css/dataTables.bootstrap.min.css"></script>
    <script src="https://cdn.datatables.net/responsive/2.1.1/css/responsive.bootstrap.min.css"></script>
    <!--Datatable-->

    <!--Script-->
    <script src="~/Scripts/customjs.js"></script>
    <script src="https://code.jquery.com/jquery-1.12.4.js"></script>
    <script src="https://cdn.datatables.net/1.10.13/js/jquery.dataTables.min.js"></script>

    <link rel="stylesheet" href="https://cdn.datatables.net/1.10.13/css/dataTables.bootstrap.min.css" />
    <link rel="stylesheet" href="https://cdn.datatables.net/responsive/2.1.1/css/responsive.bootstrap.min.css" />

    <script src="https://cdn.datatables.net/1.10.13/js/dataTables.bootstrap.min.js"></script>
    <script src="https://cdn.datatables.net/responsive/2.1.1/js/dataTables.responsive.min.js"></script>
    <script src="https://cdn.datatables.net/responsive/2.1.1/js/responsive.bootstrap.min.js"></script>
    <!--Script-->
    <!-- For Title-->
    <title>Logic University Stationery Store Inventory System</title>
    <!-- End of Title -->
    <style>
        .bg-4 {
            background-color: #2f2f2f; /* Black Gray */
            color: #fff;
        }

        .container-fluid1 {
            padding-top: 70px;
            padding-bottom: 70px;
        }

        footer .glyphicon {
            font-size: 20px;
            margin-bottom: 20px;
            color: #f4511e;
        }
    </style>
</head>
<body>

    <!-- Menu -->
    <form id="form1" runat="server">

        <h1>
            <img src="Styles/logo.png" height="60" />
            Logic University SSIS</h1>
        <nav class="navbar navbar-default">
            <div class="container-fluid">
                <div class="navbar-header">
                    <button type="button" class="navbar-toggle" data-toggle="collapse" data-target="#myNavbar">
                        <span class="icon-bar"></span>
                        <span class="icon-bar"></span>
                        <span class="icon-bar"></span>
                    </button>

                </div>
                <div class="collapse navbar-collapse" id="myNavbar">
                    <ul class="nav navbar-nav navbar-left">
                    </ul>
                </div>
            </div>
        </nav>

        <!-- End of Menu-->

        <div class="container">
            <div class="row">
                                <div class="col-sm-3">
                                                        <h2>Login</h2>

                    <div class="form-group">
                        <asp:Label ID="lbUserId" runat="server" Text="User ID:"></asp:Label>
                    </div>

                    <div class="form-group">
                        <asp:TextBox ID="tbUserId" runat="server" Style="text-transform: uppercase"></asp:TextBox>
                    </div>

                    <div class="form-group">
                        <asp:Label ID="lbPassword" runat="server" Text="Password:"></asp:Label>
                    </div>

                    <div class="form-group">
                        <asp:TextBox ID="tbPassword" runat="server" TextMode="Password"></asp:TextBox>
                    </div>

                    <asp:Button ID="btnSubmit" class="btn btn-default btn-md" runat="server" Text="Login" OnClick="btnSubmit_Click" />

                    <br /><br /><asp:ValidationSummary ForeColor="Red" runat="server" ID="validationSummary"></asp:ValidationSummary>

                                </div>
                <div class="col-sm-7 col-sm-offset-2" style="margin-top:20px">

                    <div id="myCarousel" class="carousel slide" data-ride="carousel">
                        <!-- Indicators -->
                        <ol class="carousel-indicators">
                            <li data-target="#myCarousel" data-slide-to="0" class="active"></li>
                            <li data-target="#myCarousel" data-slide-to="1"></li>
                            <li data-target="#myCarousel" data-slide-to="2"></li>
                            <li data-target="#myCarousel" data-slide-to="3"></li>
                        </ol>

                        <!-- Wrapper for slides -->
                        <div class="carousel-inner" role="listbox">
                            <div class="item active">
                                <img src="Styles/a.jpg">
                                <div class="carousel-caption">
                                    <h3>Logic University</h3>
                                </div>
                            </div>

                        </div>

                        <!-- Left and right controls -->
                        <a class="left carousel-control" href="#myCarousel" role="button" data-slide="prev">
                            <span class="glyphicon glyphicon-chevron-left" aria-hidden="true"></span>
                            <span class="sr-only">Previous</span>
                        </a>
                        <a class="right carousel-control" href="#myCarousel" role="button" data-slide="next">
                            <span class="glyphicon glyphicon-chevron-right" aria-hidden="true"></span>
                            <span class="sr-only">Next</span>
                        </a>
                    </div>

                </div>
            </div>
        </div>

        <asp:RequiredFieldValidator
            runat="server"
            ID="requiredValidatorStartDate"
            Display="None"
            ControlToValidate="tbUserId"
            ErrorMessage="Please enter User ID" />

        <asp:RequiredFieldValidator
            runat="server"
            ID="RequiredFieldValidator1"
            Display="None"
            ControlToValidate="tbPassword"
            ErrorMessage="Please enter password" />

    </form>
</body>
</html>