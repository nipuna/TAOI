<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
    <link rel="stylesheet" type="text/css" href="../../Content/css/Admin/Customers.css" />
    <link rel="stylesheet" type="text/css" href="../../Content/jquery-autocomplete/jquery.autocomplete.css" />
    <link type="text/css" href="../../Scripts/jqUIPortlets/ui.all.css" rel="stylesheet" />
    <link rel="stylesheet" type="text/css" href="../../Scripts/jqGrid/themes/redmond/redmondCustom.css" />
    <script src="../../Scripts/admin/customers.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery-1.3.2.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="../../Scripts/jqUIPortlets/js/ui.core.js"></script>
    <script type="text/javascript" src="../../Scripts/jqUIPortlets/js/ui.sortable.js"></script>
    <script src="../../Scripts/jquery-autocomplete/jquery.bgiframe.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery-autocomplete/jquery.ajaxQueue.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery-autocomplete/thickbox-compressed.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery-autocomplete/jquery.autocomplete.js" type="text/javascript"></script>
    <style type="text/css">
        .column
        {
            width: 500px;
            float: left;
            padding-bottom: 100px;
        }
        .portlet
        {
            margin: 0 1em 1em 0;
            background-color: #EDF3F8;
            background-repeat: repeat-y;
        }
        .portlet-header
        {
            margin: 0.3em;
            padding-bottom: 4px;
            padding-left: 0.2em;
            background-color: #5C9CCC;
            background-repeat: repeat;
        }
        .portlet-header .ui-icon
        {
            float: right;
        }
        .portlet-content
        {
            padding: 0.4em;
            color: #00458C;
            font-weight: bold;
            margin-bottom: 4px;
            padding-bottom: 4px;
        }
        .ui-sortable-placeholder
        {
            border: 1px dotted black;
            visibility: visible !important;
            height: 50px !important;
        }
        .ui-sortable-placeholder *
        {
            visibility: hidden;
        }
    </style>
    <script type="text/javascript">

        $(function () {
            $(".column").sortable({
                connectWith: '.column'
            });

            $(".portlet").addClass("ui-widget ui-widget-content ui-helper-clearfix ui-corner-all")
			.find(".portlet-header")
				.addClass("ui-widget-header ui-corner-all")
				.prepend('<span class="ui-icon ui-icon-plusthick"></span>')
				.end()
			.find(".portlet-content");

            $(".portlet-header .ui-icon").click(function () {
                $(this).toggleClass("ui-icon-minusthick");
                $(this).parents(".portlet:first").find(".portlet-content").toggle();
            });

            $(".column").disableSelection();
        });

    </script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Edit
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container_24">
        <div class="grid_3">
            &nbsp;</div>
        <div class="grid_20">
            &nbsp;
            <% if (ViewData["customerId"] == "")
               { %>
            &nbsp;<%= Html.ValidationSummary("Creation was unsuccessful. Please correct the errors and try again.") %>
            <%  } %>
            <% else
                { %>
            &nbsp;<%= Html.ValidationSummary("Editing was unsuccessful. Please correct the errors and try again.") %>
            <%  } %>
        </div>
        <% using (Html.BeginForm("Edit", "Customers", FormMethod.Post, new { enctype = "multipart/form-data" }))
           { %>
        <div class="grid_3">
            &nbsp;</div>
        <div class="grid_10 mtop">
            <% if (ViewData["customerId"] == "")
               { %>
            Create Customer
            <%  } %>
            <% else
                { %>
            Edit Customer
            <%  } %>
        </div>
        <div class="clear">
            &nbsp;</div>
        <div class="grid_3">
            &nbsp;
        </div>
        <div id="divAllPortlets" class="grid_19">
            <div class="portlet">
                <div class="portlet-header">
                    Customer Details
                </div>
                <div class="portlet-content">
                    <div class="grid_18" style="height: 190px;">
                        <div class="grid_2 edittext" style="margin-left: 30px; text-align: left; width: 90px;">
                            Name:</div>
                        <div class="grid_6" style="margin-top: 10px;">
                            <% if (ViewData["customerId"] == "")
                               { %>
                            <%= Html.Hidden("Id", "-1" )%>
                            <%  } %>
                            <% else
                                { %>
                            <%= Html.Hidden("Id", ViewData["customerId"] )%>
                            <%  } %>
                            <%= Html.TextBox("name", (String)(ViewData["customerName"]), new { @class = "input2B", style = "width:90%" })%>
                            <%= Html.ValidationMessage("name", "*" )%>
                        </div>
                        <div class="grid_2 edittext" style="margin-left: 30px; text-align: left;">
                            Website:</div>
                        <div class="grid_6" style="margin-top: 10px;">
                            <%= Html.TextBox("website", (String)(ViewData["website"]), new { @class = "input2B", style = "width:90%" })%>
                            <%= Html.ValidationMessage("website", "*" )%>
                        </div>
                        <div class="clear" style="height: 15px;">
                        </div>
                        <div class="grid_2 edittext" style="margin-left: 30px; text-align: left; width: 90px;
                            margin-top: 10px;">
                            Company No:
                        </div>
                        <div class="grid_6 " style="margin-top: 10px;">
                            <%= Html.TextBox("companynumber", (String)(ViewData["companyNo"]), new { @class = "input2B", style = "width:90%" })%>
                            <%= Html.ValidationMessage("companynumber", "*")%>
                        </div>
                        <div class="grid_2 edittext" style="margin-left: 30px; text-align: left; margin-top: 10px;">
                            VAT No:</div>
                        <div class="grid_6" style="margin-top: 10px;">
                            <%= Html.TextBox("vatnumber", (String)(ViewData["vatNo"]), new { @class = "input2B", style = "width:90%" })%>
                            <%= Html.ValidationMessage("vatnumber", "*")%>
                        </div>
                        <div class="clear" style="height: 15px;">
                        </div>
                        <div class="grid_2 edittext" style="margin-left: 30px; text-align: left; width: 90px;
                            margin-top: 0px;">
                            Comments:
                        </div>
                        <div class="grid_14">
                            <%= Html.TextArea("comments", (String)(ViewData["comment"]), new { @class = "grid_14", style = "border:1px solid #125294;height:75px;margin-bottom:20px;" })%>
                            <%= Html.ValidationMessage("comments", "*")%>
                        </div>
                    </div>
                </div>
            </div>
            <div class="portlet">
                <div class="portlet-header">
                    Brands
                </div>
                <div class="portlet-content">
                    <div id="divCustomerbrands" class="grid_18 " style="height: 140px;">
                        <div id="divBrands" style="margin: 20px 30px 0px 20px; height: 100px; float: left;
                            width: 660px; overflow: auto;">
                        </div>
                        <div style="float: right;">
                            <img id="imgShowPopUp" onmouseover="showPopUpDIv('divPopUp','imgShowPopUp')" style="border-width: 0px;" src="../../Content/Images/Addbrands.jpg" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="grid_24 ">
            &nbsp;</div>
        <div class="clear">
            &nbsp;</div>
        <div style="width:680px;float:left;" >
            &nbsp;</div>
        <div class="grid_4">
            <% if (ViewData["customerId"] == "")
               { %>
            <input type="submit" value="Create" class="btnedit" />
            <%  } %>
            <% else
                { %>
            <input type="submit" value="Save" class="btnedit" />
            <%  } %>
            <%= Html.ActionLink("Cancel", "Index", new { controller = "Customers" , page = 1 }, new { @class = "btndelete" })%>
        </div>
        <div class="grid_3">
            &nbsp;</div>
        <div class="clear">
            &nbsp;</div>
    </div>
    <% } %>
    <div id="divPopUp" style="display: none; margin-top: 20px; margin-right: auto; margin-bottom: 0px;margin-left: auto; height: 282px; float: left; width: 282px; background-color: #fff;position: absolute; z-index: 1000;">
        <div id="divBrandsPopUp" class="dborders" style="margin-top: 20px; margin-right: auto; margin-bottom: 0px;margin-left: auto; height: 282px; float: left; width: 280px; background-color:#D9F3FF;">
            <div class="boxbgheading" style="width: 280px; float: left; margin-top: 0px;">
                <div style="padding-left: 15px; margin-top: 5px; float: left;">
                    Search for Brand</div>
            </div>
            <div class="boxbg" style="width: 280px; height: 246px; float: left;">
                <div class="edittext" style="float: left; margin-left: 30px; text-align: left; width: 50px;">
                    Name:</div>
                <div class="brandinputarea" style="width:185px;" >
                    <div id="divSearch" style="float: left; vertical-align: middle; margin-top: 5px;">
                        <img src="/Content/Images/search.png" />
                    </div>
                    <input type="text" id="txtBrandName" style="border: none; border-style: none; margin-left: 5px;
                        height: 15px; width: 150px;" />
                </div>
                <div class="edittext" style="float: left; margin-left: 30px; text-align: left; width: 50px;">
                    Logo:</div>
                <div class="edittext" style="text-align: center; vertical-align: middle; height: 110px;
                    width: 190px; background-color: #fff; margin-top: 10px; float: left;">
                    <img src='' alt="brand" id="brandLogo" style="width: 100px; height: 100px;" align="middle" />
                    <input type="hidden" id="hdBrandId" name="txtHdBrandId" />
                </div>
                <div class="footerline" style="height: 2px; background-color: #5E94CC; margin-top: 30px;float: left; width: 100%;">
                    &nbsp;</div>
                <div style="width: 190px; margin-top: 5px; margin-right: auto; margin-left: auto;
                    height: 30px;">
                    <div style="float: left; width: auto; height: 30px;">
                        <input type="submit" onclick="addingBrand()" value="Add" class="btnedit" />
                    </div>
                    <div style="float: left; height: 30px;">
                        <input type="submit" value="Clear" onclick="document.getElementById('txtBrandName').value=''" class="btnedit" />
                    </div>
                    <div style="float: left;">
                        <%--<img id="Img7" style="border-width: 0px; height: 28px; width: 20px" src="/Content/Images/close.gif"/>--%>
                        <a style="color: rgb(255, 255, 255);" class="btnedit" href="#" onclick="hidePopUpDIv('divPopUp')" >Close</a>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        var allImgdivs = '<%= ViewData["brands"] %>';
        //alert(allImgdivs);
        if (allImgdivs != "") {
            $('#divBrands').empty();
            $(allImgdivs).appendTo('#divBrands');
        }
        //        allImgdivs = '<%= ViewData["allBrands"] %>';
        //        $('#divBrandsPopUp').empty();
        //        $(allImgdivs).appendTo('#divBrandsPopUp');


    </script>
</asp:Content>
