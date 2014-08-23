<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    IOTA--Errors
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container_24">
        <div class="grid_1">
            &nbsp;
        </div>
        <div class="grid_3 mtop">
            Error</div>
        <div class="grid_14">
            &nbsp;</div>
        <div class="clear">
            &nbsp;</div>
        <div class="grid_1">
            &nbsp;
        </div>
        <div class="grid_19 " style="height:100px;" >
            <div style="margin: 30px 30px 30px 0px;font-size:14px;" >
                <%= ViewData["errMsg"] %>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
