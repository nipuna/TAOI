<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Edit
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript" src="../../Scripts/admin/Vehicles.js"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div class="container_24">
    <div class="grid_8">
        &nbsp;</div>
    <div class="grid_16" style="width:630px" >
        &nbsp;           
            <% if (ViewData["vehicleId"] == "")
               { %>
                    &nbsp;<%= Html.ValidationSummary("Creation was unsuccessful. Please correct the errors and try again.") %>
            <%  } %>
            <% else
                { %>
                    &nbsp;<%= Html.ValidationSummary("Editing was unsuccessful. Please correct the errors and try again.") %>
            <%  } %>
    </div>
    <% using (Html.BeginForm("Edit", "Vehicles", FormMethod.Post, new { enctype = "multipart/form-data" })) 
       { %>
    
        <div class="grid_8">
            &nbsp;</div>
<%--        <div class="grid_5 mtop">
            <% if (ViewData["vehicleId"] == "")
               { %>
            Create Vehicle
            <%  } %>
            <% else
                { %>
            Edit Vehicle
            <%  } %>
        </div>--%>
        <div class="clear">
            &nbsp;</div>
        <div class="grid_8">
            &nbsp;</div>
        <div class="grid_16">&nbsp;
        </div>
        <div class="grid_8">&nbsp;
            </div>
        <div class="grid_9 dborders" id="editboxbg" style="height:160px;" >
            <div class="grid_2 edittext">
                Customer:</div>
            <div class="grid_6">
                <% if (ViewData["vehicleId"] == "")
                   { %>
                        <%= Html.Hidden("Id", "-1" )%>
                        <%= Html.DropDownList("Customer", (SelectList)(ViewData["vehicleCustomer"]), new { @class = "dropdown", onchange = "fillBrands(this.selectedIndex)", style = "width:95%;" })%>
                <%  } %>
                <% else
                    { %>
                        <%= Html.Hidden("Id", ViewData["vehicleId"] )%>
                        <%= Html.DropDownList("Customer", (SelectList)(ViewData["vehicleCustomer"]), new { @class = "dropdown", style = "width:95%;" })%>
                        <%= Html.ValidationMessage("Customer", "*", new { style = "float:right" })%>
                <%  } %>
            </div>
            <div class="clear" style="height: 15px;">
            </div>
            <div class="grid_2 edittext" style="margin-top: 0px">
                Brand:
            </div>
            <% if (ViewData["vehicleId"] == "")
               { %>
                <div class="grid_6">
                    <%= Html.DropDownList("Brand", (SelectList)(ViewData["VehicleBrand"]), new { @class = "dropdown", style = "width:95%;" })%>
                </div>
            <%  } %>
            <% else
               { %>
                <div class="grid_6">
                    <%= Html.DropDownList("Brand", (SelectList)(ViewData["VehicleBrand"]), new { @class = "dropdown", style = "width:95%;" })%>
                    <%= Html.ValidationMessage("Brand", "*", new { style = "float:right" })%>
                </div>
            <%  } %>
            <div class="clear" style="height: 15px;">
                &nbsp;</div>
            <div class="grid_2 edittext" style="margin-top: 0px">
                Model:
            </div>
            <%--<% if (ViewData["vehicleId"] == "")
               { %>--%>
                <div class="grid_6">
                    <div style="float:left;" >
                    <%= Html.TextBox("model", (String)(ViewData["vehicleModel"]), new { @class = "input2", style = "width:95%;margin-top:0px;" })%>
                    </div>
                    <div style="float:left;width:2px;" >
                        <%= Html.ValidationMessage("model", "*", new { style = "float:left" })%>
                    </div>
                </div>
            <%--<%  } %>--%>
        </div>
        <div class="clear">
            &nbsp;</div>
        <div class="grid_24 footerline">
            &nbsp;</div>
        <div class="clear">
            &nbsp;</div>
        <div class="grid_12">
            &nbsp;</div>
        <div class="grid_4">
            <% if (ViewData["vehicleId"] == "")
               { %>
            <input type="submit" value="Create" class="btnedit" />
            <%  } %>
            <% else
                { %>
            <input type="submit" value="Save" class="btnedit" />
            <%  } %>
            <%= Html.ActionLink("Cancel", "Index", new { controller = "Vehicles" , page = 1 }, new { @class = "btndelete" })%>
        </div>
        <div class="grid_8">
            &nbsp;</div>
        <div class="clear">
            &nbsp;</div>
    </div>
    <% } %>
    <script language="javascript" type="text/javascript" >
        var id = '<%= ViewData["vehicleId"] %>';
        //alert(id);
        if (id != "") {
            document.getElementById("Customer").disabled = true;
            document.getElementById("Brand").disabled = true;
        }
    </script>
</asp:Content>
