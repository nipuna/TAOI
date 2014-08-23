<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
    <link rel="stylesheet" type="text/css" href="../../Scripts/jqGrid/themes/ui.jqGrid.css" />
    <link rel="stylesheet" type="text/css" href="../../Scripts/jqGrid/themes/redmond/redmondCustom.css" />
    <script src="../../Scripts/jqgrid/js/jqModal.js" type="text/javascript"></script>
    <script src="../../Scripts/jqgrid/js/jquery.jqGrid.js" type="text/javascript"></script>
    <script src="../../Scripts/jqgrid/js/jqDnR.js" type="text/javascript"></script>
    <script src="../../Scripts/admin/admin.MyGrid.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            AllDataGrid.url = '/Customers/getJQgridData/';
            AllDataGrid.names[0] = 'Logo';
            AllDataGrid.names[1] = 'Name';
            AllDataGrid.names[2] = 'Action';
            AllDataGrid.pageSize = 10;
            AllDataGrid.sortBy = 'Name';
            AllDataGrid.setDefaults();
            AllDataGrid.setupGrid($("#list"), $("#pager"), $("#search"));
        });

        function deleteConfirmation() {
            return confirm("Are you sure you want to delete ?");
        }
    </script>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Index
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container_24">
        <div class="grid_6">&nbsp;
        </div>
        <div class="grid_8">
            &nbsp;</div>
        <div class="clear">
            &nbsp;</div>
        <div class="clear">
            &nbsp;</div>
        <div class="grid_6">&nbsp;
        </div>
        <div class="grid_14 ">
            <div class="clear">
                &nbsp;</div>
            <div class="grid_12">
                &nbsp;</div>
            <div class="clear">
                &nbsp;</div>
            <div id="search"></div>
            <table id="list" class="scroll" cellpadding="0" cellspacing="0">
            </table>
            <div id="pager" class="scroll" style="text-align: center;">
            </div>
        </div>
        <div style="width:610px;float:left;" >&nbsp;
        </div>
        <div class="grid_2" style="margin-top:20px;" >
                <%= Html.ActionLink("Create", "Create", null, new { @class = "btnedit", align = "left" })%>
        </div>
    </div>

</asp:Content>

