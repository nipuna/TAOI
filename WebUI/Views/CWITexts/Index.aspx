<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<DomainModel.Entities.CWIText>>" %>
 
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link rel="stylesheet" type="text/css" href="../../Scripts/jqGrid/themes/ui.jqGrid.css" />
    <link rel="stylesheet" type="text/css" href="../../Content/css/Admin/css.css" />
    <link rel="stylesheet" type="text/css" href="../../Content/css/Admin/CWITexts.css" />
    <link rel="stylesheet" type="text/css" href="../../Scripts/jqGrid/themes/redmond/redmondCustom.css" />
    <script src="../../Scripts/jqgrid/js/jqModal.js" type="text/javascript"></script>
    <script src="../../Scripts/jqgrid/js/jquery.jqGrid.js" type="text/javascript"></script>
    <script src="../../Scripts/jqgrid/js/jqDnR.js" type="text/javascript"></script>
    <script src="../../Scripts/admin/admin.MyGrid.js" type="text/javascript"></script>
    <script src="../../Scripts/JSONParser/JSONParser.js" type="text/javascript"></script>
    <script src="../../Scripts/admin/CWIText.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">

        function deleteConfirmation() {
            return confirm("Are you sure you want to delete ?");
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container_24">
        <div class="grid_6">&nbsp;
        </div>
<%--        <div class="grid_4 mtop"> 
            All CWITexts</div>--%>
        <div class="grid_8">
            &nbsp;</div>
        <div class="clear">
            &nbsp;</div>
        <div class="clear">
            &nbsp;</div>
        <div class="grid_3">&nbsp;
        </div>
        <div id="divBrand" style="width: 200px; float: right; position: relative; top: 30px; left: -145px;" >
            <select onclick="displayDiv('divBrand')" style="width:200px;"  >
              <option value="0">Select</option>
            </select>
        </div>
        <div class="grid_14 ">
            <div class="clear">
                &nbsp;</div>
            <div id="otherGrid" style="width:650px;margin-left:50px;margin-top:20px;margin-bottom:30px;" >
                <div id="search" style="width:300px;" ></div>
                <table id="list" class="scroll" cellpadding="0" cellspacing="0">
                </table>
                <div id="pager" class="scroll" style="text-align: center;"></div>
            </div>
        </div>
        <div class="grid_19">&nbsp;
        </div>
        <div class="grid_2 ">
            <%= Html.ActionLink("Create", "Create", null, new { @class = "btnedit", align = "left" })%>
        </div>
    </div>
    <div id="ScrollCB" style="visibility:hidden;display:none;" >
    </div>
</asp:Content>
