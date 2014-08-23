<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<DomainModel.Entities.User>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Edit Users
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
    <link type="text/css" href="../../Scripts/jqUIPortlets/ui.all.css" rel="stylesheet" />
    <%--<link type="text/css" href="../../Content/checkboxtree/checkboxtree.css" rel="stylesheet" />--%>
    <link rel="stylesheet" type="text/css" href="../../Scripts/jqGrid/themes/redmond/redmondCustom.css" />
    <link rel="stylesheet" type="text/css" href="../../Content/css/Admin/User.css" />
    <script type="text/javascript" src="../../Scripts/jqUIPortlets/js/ui.core.js"></script>
    <script type="text/javascript" src="../../Scripts/jqUIPortlets/js/ui.sortable.js"></script>
    <script src="../../Scripts/checkboxtree/jquery.checkboxtree.js" type="text/javascript" language="JavaScript"></script>
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
            background-color: #5382B2;
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
        var brand = "brand";
        var culture = "culture";
        var region = "region";


        jQuery(document).ready(function () {
            jQuery(".unorderedlisttree").checkboxTree({
                collapsedarrow: "/Content/images/checkboxtree/img-arrow-collapsed.gif",
                expandedarrow: "/Content/images/checkboxtree/img-arrow-expanded.gif",
                blankarrow: "/Content/images/checkboxtree/img-arrow-blank.gif",
                checkchildren: true
            });
        });

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

        var checkStatus = false;

        function checkAllCheckBoxes(chk, chkname) {
            if (chk.checked == true) {
                $("INPUT[id^=" + chkname + "][type='checkbox']").attr('checked', true);
                checkStatus = true;
            }
            else {
                $("INPUT[id^=" + chkname + "][type='checkbox']").attr('checked', false);
                checkStatus = false;
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container_24">
        <div class="grid_3">
            &nbsp;
        </div>
        <div class="grid_4 mtop">
            <% if (ViewData["userId"] == "")
               { %>
                    Create Users
            <%  } %>
            <% else
                { %>
                    Edit Users
            <%  } %>
        </div>
        <div class="clear">
            &nbsp;</div>
        <div class="grid_3">
            &nbsp;
        </div>
        <div class="grid_12" style="margin:10px 0px 10px 0px;" >
                <%= Html.ValidationSummary("Edit was unsuccessful. Please correct the errors and try again.") %>
            &nbsp;</div>
        <div class="clear">
            &nbsp;</div>
        <div class="grid_3">
            &nbsp;
        </div>
        <% using (Html.BeginForm("Edit", "Users", FormMethod.Post,
         new { enctype = "multipart/form-data" }))
           { %>
        <div id="divAllPortlets" class="grid_19">
                <% if (ViewData["userId"] == "")
                       { %>
                    <div class="portlet">
                        <div class="portlet-header">
                            User Details
                        </div>
                        <div class="portlet-content">
                            <div class="grid_1">
                                &nbsp;
                            </div>
                            <div class="grid_17" style="padding-bottom:15px;" >
                                <table style="text-align: left">
                                    <tr>
                                        <td style="width: 5%">
                                            Forename:
                                        </td>
                                        <td style="width: 35%">
                                            <%= Html.TextBox("forename", "", new { @class = "inputU" })%>
                                            <%= Html.ValidationMessage("forename", "*")%>
                                            <%= Html.Hidden("Id", "-1")%>
                                        </td>
                                        <td style="width: 5%">
                                        </td>
                                        <td style="width: 5%">
                                            Email:
                                        </td>
                                        <td style="width: 35%">
                                            <%= Html.TextBox("email", "", new { @class = "inputU" })%>
                                            <%= Html.ValidationMessage("email", "*")%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 5%">
                                            Surname:
                                        </td>
                                        <td style="width: 35%">
                                            <%= Html.TextBox("surname", "", new { @class = "inputU" })%>
                                            <%= Html.ValidationMessage("surname", "*")%>
                                        </td>
                                        <td style="width: 5%">
                                        </td>
                                        <td style="width: 5%">
                                            Phone:
                                        </td>
                                        <td style="width: 35%">
                                            <%= Html.TextBox("phone", "", new { @class = "inputU"})%>
                                            <%= Html.ValidationMessage("surname", "*")%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 5%">
                                            Username:
                                        </td>
                                        <td style="width: 35%">
                                            <%= Html.TextBox("Username", "", new { @class = "inputU" })%>
                                        </td>
                                        <td style="width: 5%">
                                        </td>
                                        <td style="width: 5%">
                                            Location:
                                        </td>
                                        <td style="width: 35%">
                                            <%= Html.DropDownList("Location", (SelectList)(ViewData["location"]), new { style = "width:90%;border:1px solid #125294;" })%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 5%">
                                            Password:
                                        </td>
                                        <td style="width: 5%">
                                            <%= Html.Password("Password", "", new { @class = "inputU" })%>
                                        </td>
                                        <td style="width: 5%">
                                        </td>
                                        <td style="width: 5%">
                                            Active:
                                        </td>
                                        <td style="width: 5%">
                                            <%= Html.CheckBox("active", true)%>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>
                    <div class="portlet">
                        <div class="portlet-header">
                            Customer Brands</div>
                        <div class="portlet-content">
                            <div class="grid_1">
                                &nbsp;
                            </div>
                            <div id="brands" class="grid_17" style="padding-bottom:5px;" >
                                <%--<%= Html.CheckBoxList("customer", (List<details>)(ViewData["brands"]), null)%>--%>
                            </div>
                        </div>
                    </div>
                    <div class="portlet">
                        <div class="portlet-header">
                            User Rights</div>
                        <div class="portlet-content">
                            <div class="grid_1">
                                &nbsp;
                            </div>
                            <div id="userRights" class="grid_17" style="padding-bottom:5px;" >
                                  
                            </div>
                        </div>
                    </div>
                    <div class="portlet">
                        <div class="portlet-header">
                            Languages</div>
                        <div class="portlet-content">
                            <div class="grid_1">
                                &nbsp;
                            </div>
                            <div id="cultures" class="grid_17" style="padding-bottom:5px;" >
                                  <%--<%= Html.CheckBoxList("culture", (List<details>)(ViewData["cultures"]), null)%>--%>
                            </div>
                        </div>
                    </div>
                    <div class="portlet">
                        <div class="portlet-header">
                            Regions</div>
                        <div class="portlet-content">
                            <div class="grid_1">
                                &nbsp;
                            </div>
                            <div id="regions" class="grid_17" style="padding-bottom:5px;" >
                                    <%--<%= Html.CheckBoxList("country", (List<details>)(ViewData["regions"]), null)%>--%>
                            </div>
                        </div>
                    </div>
                    <div class="portlet">
                        <div class="portlet-header">
                            Comments</div>
                        <div class="portlet-content">
                            <div class="grid_1">
                                &nbsp;
                            </div>
                            <div class="grid_17" style="padding-bottom:10px;" >
                                    <%= Html.TextArea("comment", "", new { @class = "grid_17", style = "border:1px solid #125294;" })%>
                            </div>    
                        </div>
                    </div>
                    <%  } %>
                <% else
                    { %>
                    <div class="portlet">
                        <div class="portlet-header">
                            User Details
                        </div>
                        <div class="portlet-content">
                            <div class="grid_1">
                                &nbsp;
                            </div>
                            <div class="grid_17" style="padding-bottom:15px;" >
                                <table style="text-align: left">
                                    <tr>
                                        <td style="width: 5%">
                                            Forename:
                                        </td>
                                        <td style="width: 35%">
                                            <%= Html.TextBox("forename", Model.Contact == null ? " " : Model.Contact.Forename, new { @class = "inputU" })%>
                                            <%= Html.ValidationMessage("forename", "*")%>
                                            <%= Html.Hidden("Id", Model.ID.ToString())%>
                                        </td>
                                        <td style="width: 5%">
                                        </td>
                                        <td style="width: 5%">
                                            Email:
                                        </td>
                                        <td style="width: 35%">
                                            <%= Html.TextBox("email", Model.Contact == null ? " " : Model.Contact.Email, new { @class = "inputU" })%>
                                            <%= Html.ValidationMessage("forename", "*")%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 5%">
                                            Surname:
                                        </td>
                                        <td style="width: 35%">
                                            <%= Html.TextBox("surname", Model.Contact == null ? " " : Model.Contact.Surname, new { @class = "inputU" })%>
                                            <%= Html.ValidationMessage("surname", "*")%>
                                        </td>
                                        <td style="width: 5%">
                                        </td>
                                        <td style="width: 5%">
                                            Phone:
                                        </td>
                                        <td style="width: 35%">
                                            <%= Html.TextBox("phone", Model.Contact == null ? " " : Model.Contact.Phone, new { @class = "inputU"})%>
                                            <%= Html.ValidationMessage("surname", "*")%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 5%">
                                            Username:
                                        </td>
                                        <td style="width: 35%">
                                            <%= Html.TextBox("Username", Model.Username, new { @class = "inputU" })%>
                                        </td>
                                        <td style="width: 5%">
                                        </td>
                                        <td style="width: 5%">
                                            Location:
                                        </td>
                                        <td style="width: 35%">
                                            <%= Html.DropDownList("Location", (SelectList)(ViewData["location"]), new { style = "width:90%;border:1px solid #125294;" })%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 5%">
                                            Password:
                                        </td>
                                        <td style="width: 5%">
                                            <%= Html.Password("Password", Model.Password, new { @class = "inputU" })%>
                                        </td>
                                        <td style="width: 5%">
                                        </td>
                                        <td style="width: 5%">
                                            Active:
                                        </td>
                                        <td style="width: 5%">
                                            <%= Html.CheckBox("active", Model.Active)%>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>
                    <div class="portlet">
                        <div class="portlet-header">
                            Customer Brands</div>
                        <div class="portlet-content">
                            <div class="grid_1">
                                &nbsp;
                            </div>
                            <div id="brands" class="grid_17" style="padding-bottom:5px;" >
                                <%--<%= Html.CheckBoxList("customer", (List<details>)(ViewData["brands"]), null)%>--%>
                            </div>
                        </div>
                    </div>
                    <div style="height:auto;" class="portlet" >
                        <div class="portlet-header">
                            User Rights</div>
                        <div style="height:auto;" class="portlet-content">
                            <div class="grid_1">
                                &nbsp;
                            </div>
                            <div id="userRights" class="grid_17" style="padding-bottom:20px;" >
                                  
                            </div>
                        </div>
                    </div>
                    <div class="portlet">
                        <div class="portlet-header">
                            Languages</div>
                        <div class="portlet-content">
                            <div class="grid_1">
                                &nbsp;
                            </div>
                            <div id="cultures" class="grid_17" style="padding-bottom:5px;" >
                                  <%--<%= Html.CheckBoxList("culture", (List<details>)(ViewData["cultures"]), null)%>--%>
                            </div>
                        </div>
                    </div>
                    <div class="portlet">
                        <div class="portlet-header">
                            Regions</div>
                        <div class="portlet-content">
                            <div class="grid_1">
                                &nbsp;
                            </div>
                            <div id="regions" class="grid_17" style="padding-bottom:5px;" >
                                    <%--<%= Html.CheckBoxList("country", (List<details>)(ViewData["regions"]), null)%>--%>
                            </div>
                        </div>
                    </div>
                    <div class="portlet">
                        <div class="portlet-header">
                            Comments</div>
                        <div class="portlet-content">
                            <div class="grid_1">
                                &nbsp;
                            </div>
                            <div class="grid_17" style="padding-bottom:10px;" >
                                    <%= Html.TextArea("comment", Model.Comment, new { @class = "grid_17", style = "border:1px solid #125294;height:60px;margin-top:20px;margin-bottom:20px;" })%>
                            </div>    
                        </div>
                    </div>
                <%  } %>
        </div>
        <div class="grid_24 ">
            &nbsp;</div>
        <div class="clear">
            &nbsp;</div>
        <div style="width:690px;float:left;" >
            &nbsp;</div>
        <div class="grid_4" style="float:left;" >
            <% if (ViewData["userId"] == "")
               { %>
            <input type="submit" value="Create" class="btnedit" />
            <%  } %>
            <% else
                { %>
            <input type="submit" value="Save" class="btnedit" />
            <%  } %>
            <%= Html.ActionLink("Cancel", "Index", new { controller = "Users" , page = 1 }, new { @class = "btndelete" })%>
        </div>
        <div class="grid_2">
            &nbsp;</div>
        <div class="clear">
            &nbsp;</div>
        <% } %>
    </div>
    
    <script type="text/javascript">
        var brands = '<%= ViewData["brands"] %>';
        $('#brands').empty();
        $(brands).appendTo('#brands');
        var cultures = '<%= ViewData["cultures"] %>';
        $('#cultures').empty();
        $(cultures).appendTo('#cultures');
        var regions = '<%= ViewData["regions"] %>';
        $('#regions').empty();
        $(regions).appendTo('#regions');

        var userRights = '<%= ViewData["userRights"] %>';
        $('#userRights').empty();
        $(userRights).appendTo('#userRights');

    </script>
</asp:Content>
