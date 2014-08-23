<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Index
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
    <link rel="stylesheet" type="text/css" href="../../Scripts/jqGrid/themes/ui.jqGrid.css" />
    <link rel="stylesheet" type="text/css" href="../../Content/css/Admin/css.css" />
    <link rel="stylesheet" type="text/css" href="../../Scripts/jqGrid/themes/redmond/redmondCustom.css" />
    <script src="../../Scripts/jqgrid/js/jqModal.js" type="text/javascript"></script>
    <script src="../../Scripts/jqgrid/js/jquery.jqGrid.js" type="text/javascript"></script>
    <script src="../../Scripts/jqgrid/js/jqDnR.js" type="text/javascript"></script>
    <script src="../../Scripts/admin/admin.MyGrid.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            AllDataGrid.url = '/Vehicles/getJQgridData/';
            AllDataGrid.names[0] = 'Customer';
            AllDataGrid.names[1] = 'Brand';
            AllDataGrid.names[2] = 'Model';
            AllDataGrid.names[3] = 'Action';
            AllDataGrid.pageSize = 10;
            AllDataGrid.sortBy = 'Customer';
            AllDataGrid.setDefaults();
            AllDataGrid.setupGridVehicles($("#list"), $("#pager"), $("#search"));
        });

        function deleteConfirmation() {
            return confirm("Are you sure you want to delete ?");
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container_24">
<%--        <div class="grid_4 mtop">
            All Vehicles</div>--%>
        <div class="grid_20" style="margin: 5px 0px 0px 0px;" >
                <%= Html.ValidationSummary("Errors") %>
                <%= Html.ValidationMessage("Vehicle", "*")%>
        </div>
        <div class="grid_6">&nbsp;
        </div>
        <div class="grid_15 ">
            <div class="clear">
                &nbsp;</div>
            <div class="clear">
                &nbsp;</div>
            <div id="search"></div>
            <table id="list" class="scroll" cellpadding="0" cellspacing="0">
            </table>
            <div id="pager" class="scroll" style="text-align: center;">
            </div>
        </div>
        <div class="grid_19">&nbsp;
        </div>
        <div class="grid_2" style="margin-left:5px;margin-top:20px;" >
                <%= Html.ActionLink("Create", "Create", null, new { @class = "btnedit", align = "left" })%>
        </div>
    </div>

</asp:Content>

