<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript" language="javascript" src="../../Scripts/screens/CWIRanges.js"></script>
    <link rel="stylesheet" type="text/css" href="../../Scripts/jqGrid/themes/ui.jqGrid.css" />
    <link rel="stylesheet" type="text/css" href="../../Scripts/jqGrid/themes/redmond/redmondCustom.css" />
    <script src="../../Scripts/jqgrid/js/jqModal.js" type="text/javascript"></script>
    <script src="../../Scripts/jqgrid/js/jquery.jqGrid.js" type="text/javascript"></script>
    <script src="../../Scripts/jqgrid/js/jqDnR.js" type="text/javascript"></script>
    <script src="../../Scripts/admin/admin.MyGrid.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.validate.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            $("#cwiranges").validate();

        });

//        // this one requires the text "buga", we define a default message, too
//        $.validator.addMethod("buga", function (value) {
//            return value == "buga";
//        }, 'Please enter "buga"!');

//        // this one requires the value to be the same as the first parameter
//        $.validator.methods.equal = function (value, element, param) {
//            return value == param;
//        };

//        $().ready(function () {
//            var validator = $("#cwiranges").bind("invalid-form.validate", function () {
//                $("#summary").html("Your form contains errors, see details below.");
//                $("#summary").addClass("divError");
//            }).validate({
//                debug: true,
//                errorContainer: $("#summary")
//            });

//        });

        function deleteConfirmation() {
            return confirm("Are you sure you want to delete ?");
        }
    </script>
    
<style type="text/css">
input.error {
border:1px dotted red;
}
.divError { color:Red ; }
</style>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Index
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<% using (Html.BeginForm("Index", "CWIRanges", FormMethod.Post, new { enctype = "multipart/form-data" , id = "cwiranges" })) 
   { %>
    <div class="container_24">
        <div class="grid_1">
            &nbsp;
        </div>
<%--        <div class="grid_4 mtop">
            CWI Ranges</div>--%>
        <div class="grid_8">
            &nbsp;</div>
        <div class="clear">
            &nbsp;</div>
        <div class="grid_1">
            &nbsp;
        </div>
        <div class="grid_12" style="margin: 10px 0px 10px 0px;">
            <%= Html.ValidationSummary("Errors") %>
            <%= Html.ValidationMessage("CWIRanges", "*")%>
        </div>
        <div class="clear">
            &nbsp;</div>
        <div class="grid_2">
            &nbsp;
        </div>
        <div class="grid_20" style="color:#00458C;" >
            <div class="clear">
                &nbsp;</div>
            <div class="grid_9 dborders" style="float: left; margin: 15px; font-weight: bold;height:330px;">
                <div style="text-align: center; font-size: 18px; margin-top: 10px; margin-bottom: 20px;float: left; width: 100%;">
                    Device
                </div>
                <div style="float:left; margin-left: 30px;margin-bottom:5px;margin-top:5px;" >
                    <div style="width: 70px; text-align: left; font-size: 12px; float: left;padding-top:5px;">
                        Brand:
                    </div>
                    <div style="width: 200px;text-align: left; font-size: 12px; float: left;">
                        <%= Html.DropDownList("brand", (SelectList)(ViewData["VehicleBrand"]), new { @class = "dropdown", style = "width:100%;", onchange = "getDevicesForBrands(this.selectedIndex)" })%>
                    </div>
                </div>
                <div style="float:left; margin-left: 30px;margin-bottom:5px;margin-top:5px;" >
                    <div style="width: 70px; text-align: left; font-size: 12px; float: left;padding-top:5px;">
                        Model:
                    </div>
                    <div style="width: 200px; text-align: left; font-size: 12px; float: left;">
                        <%= Html.DropDownList("Device", (SelectList)(ViewData["VehicleDevice"]), new { @class = "dropdown", style = "width:100%;", onchange = "getDetailsForDevice(this.selectedIndex)" })%>
                    </div>
                </div>
                <div style="float:left; margin-left: 30px;margin-bottom:5px;margin-top:5px;" >
                    <div style="width: 70px; text-align: left; font-size: 12px; float: left;padding-top:5px;">
                        Software:
                    </div>
                    <div style="width: 200px; text-align: left; font-size: 12px; float: left;">
                        <%= Html.TextBox("software", ViewData["software"], new { @class = "input2" ,style = "width:100%;margin-top:0px;" })%>
                    </div>
                </div>
                <div style="float:left; margin-left: 30px;margin-bottom:5px;margin-top:5px;" >
                    <div style="width: 70px; text-align: left; font-size: 12px; float: left;padding-top:5px;">
                        Hardware:
                    </div>
                    <div style="width: 200px; text-align: left; font-size: 12px; float: left;">
                        <%= Html.TextBox("Hardware", ViewData["Hardware"], new { @class = "input2", style = "width:100%;margin-top:0px;" })%>
                    </div>
                </div>
                <div style="float:left; margin-left: 30px;margin-bottom:5px;margin-top:5px;" >
                    <div style="width: 70px; text-align: left; font-size: 12px; float: left;padding-top:5px;">
                        Serial:
                    </div>
                    <div style="width: 200px; text-align: left; font-size: 12px; float: left;">
                        <%= Html.TextBox("Serial", ViewData["Serial"], new { @class = "input2", style = "width:100%;margin-top:0px;" })%>
                    </div>
                </div>
                <div style="float:left; margin-left: 30px;margin-bottom:5px;margin-top:5px;" >
                    <div style="width: 70px; text-align: left; font-size: 12px; float: left;padding-top:5px;">
                        Region:
                    </div>
                    <div style="width: 200px; text-align: left; font-size: 12px; float: left;">
                        <%= Html.TextBox("Country", ViewData["Country"], new { @class = "input2", style = "width:100%;margin-top:0px;" })%>
                    </div>
                </div>
                <div style="float:left; margin-left: 30px;margin-bottom:5px;margin-top:5px;" >
                    <div style="width: 70px; text-align: left; font-size: 12px; float: left;padding-top:5px;">
                        NextGen ID:
                    </div>
                    <div style="width: 200px; text-align: left; font-size: 12px; float: left;">
                        <%= Html.TextBox("NextGenID", ViewData["NextGenID"], new { @class = "input2", style = "width:100%;margin-top:0px;" })%>
                    </div>
                </div>
                <div style="width:200px;margin-left:110px;" >
                    <div style="width:50px;float:left;" ><img id="imgPreviousDevice" disabled="true" src="../../Content/Images/Admin/left.gif" onclick="getDetailsForPreviousDevice()" /></div>
                    <div id="deveicesPager" style="width:100px;float:left;font-size:12px;text-align:center;" ></div>
                    <div style="width:50px;float:left;" ><img id="imgNextDevice" disabled="true" src="../../Content/Images/Admin/right.gif" onclick="getDetailsForNextDevice()" /></div>
                </div>
            </div>
            <div class="grid_9 dborders" style="float: left; margin-top: 15px; margin-bottom: 15px; margin-left: 40px;font-weight: bold;height:330px;">
                <div style="text-align: center; font-size: 18px; margin-top: 10px; margin-bottom: 20px;float: left; width: 100%;">
                    Product
                </div>
                <div style="float:left;margin-left: 30px;margin-bottom:5px;margin-top:5px;" >
                    <div style="width: 70px; text-align: left; font-size: 12px; float: left;padding-top:5px;">
                        Brand:
                    </div>
                    <div style="width: 200px;text-align: left; font-size: 12px; float: left;">
                        <%= Html.DropDownList("brandForProduct", (SelectList)(ViewData["VehicleBrandForProducts"]), new { @class = "dropdown", style = "width:100%;", onchange = "getProductsForBrand(this.selectedIndex)" })%>
                    </div>
                </div>
                <div style="float:left; margin-left: 30px;margin-bottom:5px;margin-top:5px;" >
                    <div style="width: 70px; text-align: left; font-size: 12px; float: left;padding-top:5px;">
                        Product:
                    </div>
                    <div style="width: 200px; text-align: left; font-size: 12px; float: left;">
                        <%= Html.DropDownList("Product", (SelectList)(ViewData["VehicleProduct"]), new { @class = "dropdown", style = "width:100%;" , onchange = "getRangesForProductSelected(this.selectedIndex)" })%>
                    </div>
                </div>
            </div>
            <div id="gridRanges" class="grid_19 dborders" style="float: left; margin: 15px; font-weight: bold;height:330px;display:none;visibility:hidden;">
                <div style="text-align: center; font-size: 18px; margin-top: 10px; margin-bottom: 5px;float: left; width: 100%;">
                    <div id="deviceName" style="width:auto;float: left;margin-left:150px;" > 
                        Ranges For Landrover L322 and Land Rover Discovery
                    </div>
                </div>
                <div id="errors" style="float: left; width:70%;margin-left:150px;" >
                	<h3 id="summary"></h3>
                </div>
                <div id="ranges" style="float: left; width:630px;margin-left:100px;margin-right:auto;height:245px;overflow:auto;" >
                    <div style="margin-left:100px;width:650px;" >
                        <div style="width:160px;text-align:center;font-weight:bold;margin:5px;float:left;" > Type</div>
                        <div style="width:100px;text-align:center;font-weight:bold;margin:5px;float:left;" > From </div>
                        <div style="width:100px;text-align:center;font-weight:bold;float:left;margin:5px;" > To </div>
                        <div style="width:100px;text-align:center;font-weight:bold;float:left;margin:5px;" > Latest </div>
                        <div style="width:100px;text-align:center;font-weight:bold;float:left;margin:5px;" > Actions </div>
                    </div>
                </div>
                <div style="width:100px;float:right;margin-top:5px;" >
                    <input type="submit" value="Save" class="btnedit" style="width:60px;height:26px;" />
                </div>
            </div>
            <div id="gridOtherRanges" class="grid_19 dborders" style="float: left; margin: 15px; font-weight: bold;height:auto;display:none;visibility:hidden;">
                <div id="vehicleName" style="width:auto;margin-left:200px;margin-top:20px;font-size:18px;" >
                    Other Ranges for Landrover Discovery
                </div>
                <div id="otherGrid" style="width:650px;margin-left:50px;margin-top:20px;margin-bottom:30px;" >
                    <div id="search1"></div>
                    <table id="list" class="scroll" cellpadding="0" cellspacing="0" >
                    </table>
                    <div id="pager1" class="scroll" style="text-align: center;" >
                    </div>
                </div>
            </div>
            <div class="clear">
                &nbsp;</div>
        </div>
        <div class="grid_16">
            &nbsp;
        </div>
    </div>
<% } %>
</asp:Content>
