<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<DomainModel.Entities.User>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link rel="stylesheet" type="text/css" href="../../Scripts/jqGrid/themes/ui.jqGrid.css" />
    <link rel="stylesheet" type="text/css" href="../../Scripts/jqGrid/themes/redmond/redmondCustom.css" />
    <link rel="stylesheet" type="text/css" href="../../Content/css/Admin/User.css" />
    <script src="../../Scripts/jqgrid/js/jqModal.js" type="text/javascript"></script>
    <script src="../../Scripts/jqgrid/js/jquery.jqGrid.js" type="text/javascript"></script>
    <script src="../../Scripts/jqgrid/js/jqDnR.js" type="text/javascript"></script>
    <script src="../../Scripts/admin/admin.MyGrid.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            AllDataGrid.url = '/Users/getJQgridData/';
            AllDataGrid.names[0] = 'Active';
            AllDataGrid.names[1] = 'Name';
            AllDataGrid.names[2] = 'Username';
            AllDataGrid.names[3] = 'Email';
            AllDataGrid.names[4] = 'Phone';
            AllDataGrid.names[5] = 'Brand';
            AllDataGrid.names[6] = 'Location';
            AllDataGrid.names[7] = 'Actions';
            AllDataGrid.pageSize = 10;
            AllDataGrid.sortBy = 'Name';
            AllDataGrid.setDefaults();
            AllDataGrid.setupGridUser($("#list"), $("#pager"), $("#search"));
        });

        function deleteConfirmation() 
        {
            return confirm("Are you sure you want to delete ?");
        }
    </script>
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container_24">
        <div class="grid_1">
            &nbsp;
        </div>
        <div class="grid_3 mtop">
            All Users</div>
        <div class="grid_14">
            &nbsp;</div>
        <div class="clear">
            &nbsp;</div>
        <div class="grid_1">
            &nbsp;
        </div>
        <div class="grid_19 ">
            <div class="clear">
                &nbsp;</div>
            <div class="grid_23">
                &nbsp;</div>
            <div class="clear">
                &nbsp;</div>
            <div id="search">
            </div>
            <table id="list" class="scroll" cellpadding="0" cellspacing="0">
            </table>
            <div id="pager" class="scroll" style="text-align: center;">
            </div>
        </div>
        <div style="width:860px;float:left;" >
            &nbsp;
        </div>
        <div class="grid_2" style="float:left;margin-top:20px;" >
            <%= Html.ActionLink("Create", "Create", null, new { @class = "btnedit", align = "left" })%>
        </div>
    </div>
</asp:Content>
