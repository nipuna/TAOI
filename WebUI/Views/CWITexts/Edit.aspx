<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<DomainModel.Entities.CWIText>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Edit
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="../../Scripts/admin/asynchronousUpload.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div class="container_24">
    <div class="grid_8">
        &nbsp;</div>
    <div class="grid_16" style="width:630px" >
        &nbsp;           
            <% if (ViewData["CWITextId"] == "")
               { %>
                    &nbsp;<%= Html.ValidationSummary("Creation was unsuccessful. Please correct the errors and try again.") %>
            <%  } %>
            <% else
                { %>
                    &nbsp;<%= Html.ValidationSummary("Editing was unsuccessful. Please correct the errors and try again.") %>
            <%  } %>
    </div>
    <% using (Html.BeginForm("Edit", "CWITexts", FormMethod.Post, new { enctype = "multipart/form-data" ,id = "frmCWITextsEdit" })) 
       { %>
    
        <div class="grid_8">
            &nbsp;</div>
<%--        <div class="grid_5 mtop">
            <% if (ViewData["CWITextId"] == "")
               { %>
            Create CWIText
            <%  } %>
            <% else
                { %>
            Edit CWIText
            <%  } %>
        </div>--%>
        <div class="clear">
            &nbsp;</div>
        <div class="grid_8">
            &nbsp;</div>
        <div class="grid_16">&nbsp;
        </div>
        <div class="grid_5" style="float:left;" >&nbsp;
            </div>
        <div class="grid_14 dborders" style="float:left;height:300px;" id="editboxbg">
            <div class="grid_2 edittext" >
                CWIText:</div>
            <div class="clear">
            &nbsp;</div>
            <div class="grid_12" style="float:left;margin-left:40px;" >
                <% if (ViewData["CWITextId"] == "")
                   { %>
                        <%= Html.Hidden("Id", "-1" )%>
                <%  } %>
                <% else
                    { %>
                        <%= Html.Hidden("Id", ViewData["CWITextId"] )%>
                <%  } %>
                <%= Html.TextArea("CwiText", (String)(ViewData["CWIText"]), new { @class = "grid_12", style = "border:1px solid #125294;height:80px;margin-top:20px;margin-bottom:20px;" })%>
                <%= Html.ValidationMessage("CwiText", "*")%>
            </div>
            <div class="clear" style="height: 15px;">
            </div>
            <div class="grid_5 edittext" style="margin-top: 0px">
                Assigned Customer brands:
            </div>
            <% if (ViewData["CWITextId"] == "")
               { %>
                    <div id="cwiBrands" class="grid_13" style="padding-bottom:5px;margin-left:40px;" >
                          
                    </div>
            <%  } %>
            <% else
                { %>
                    <div id="cwiBrands" class="grid_13" style="padding-bottom:5px;margin-left:40px;" >
                          
                    </div>
            <%  } %>
        </div>
        <div class="clear">
            &nbsp;</div>
        <div class="grid_24 ">
            &nbsp;</div>
        <div class="clear">
            &nbsp;</div>
        <div style="width:442px;float:left;" >
            &nbsp;</div>
        <div class="grid_4">
            <% if (ViewData["CWITextId"] == "")
               { %>
            <input type="submit" value="Create" class="btnedit" />
            <%  } %>
            <% else
                { %>
            <input type="submit" value="Save" class="btnedit" />
            <%  } %>
            <%= Html.ActionLink("Cancel", "Index", new { controller = "CWITexts", page = 1 }, new { @class = "btndelete" })%>
        </div>
        <div class="grid_6">
            &nbsp;</div>
        <div class="clear">
            &nbsp;</div>
    </div>
    <% } %>
    
    <script type="text/javascript">
        var brands = '<%= ViewData["brands"] %>';
        $('#cwiBrands').empty();
        $(brands).appendTo('#cwiBrands');
    </script>
</asp:Content>
