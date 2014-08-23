<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	LogIn
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
    <link rel="stylesheet" type="text/css" href="../../Content/css/Admin/Administration.css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
        <div class="container_24">
        <div class="clear">
            &nbsp;</div>
        <div class="grid_8">
        &nbsp;</div>
        <div class="grid_10">
            <%= Html.ValidationSummary("Login was unsuccessful. Please correct the errors and try again.", new { @class = "Message" })%>
        </div>
        <% if((bool?)ViewData["lastLoginFailed"] == true) { %>
            <div class="clear">
            &nbsp;</div>
            <div class="grid_8">
            &nbsp;</div>
            <div class="grid_10 Message">
                Sorry, your login attempt failed. Please try again.
            </div>
        <% } %>
        <% using (Html.BeginForm()) { %>
        <div class="clear">
            &nbsp;</div>
        <div class="grid_8">
            &nbsp;</div>
        <div class="grid_10 dborders" id="loginboxbg" >
                <table style="width:90%">
                    <tr>
                        <td style="text-align:center" colspan="2">
                            Welcome To IOTA
                        </td>
                    </tr>
                    <tr>
                        <td style="font-size:smaller;width:40%;text-align:right;" >
                            User Name:
                        </td>
                        <td style="width:60%" >
                            <%= Html.TextBox("Username", "", new { @class = "input2" })%>
                            <%= Html.ValidationMessage("username", new { @class = "validations" })%>
                        </td>
                    </tr>
                    <tr>
                        <td style="font-size:smaller;width:40%;text-align:right" >
                            Password:
                        </td>
                        <td style="width:60%" >
                            <%= Html.Password("Password", "", new { @class = "input2" })%>
                            <%= Html.ValidationMessage("password", new { @class = "validations" })%>
                        </td>
                    </tr>
                    <tr>
                        <td style="width:40%" >
                            &nbsp;&nbsp;&nbsp;
                        </td>
                        <td style="width:60%;" >
                            <div style="margin-top:5px;width:120px" >
                                <%= Html.CheckBox("RememberMe",false) %><span style="font-size:smaller;padding-left:5px;" >Remember Me</span>
                            </div>
                            <div style="float:right;">
                                <input type="submit" value="Log in" class = "btnedit" />
                            </div>
                        </td>
                        
                    </tr>
                </table>
        </div>
    
    </div>
    <% } %>
    
</asp:Content>
