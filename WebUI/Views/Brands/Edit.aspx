<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<DomainModel.Entities.Brand>>" %>

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
            <% if (ViewData["brandId"] == "")
               { %>
                    &nbsp;<%= Html.ValidationSummary("Creation was unsuccessful. Please correct the errors and try again.") %>
            <%  } %>
            <% else
                { %>
                    &nbsp;<%= Html.ValidationSummary("Editing was unsuccessful. Please correct the errors and try again.") %>
            <%  } %>
    </div>
    <% using (Html.BeginForm("Edit", "Brands", FormMethod.Post, new { enctype = "multipart/form-data" ,id = "frmBrandsEdit" })) 
       { %>
    
        <div class="grid_8">
            &nbsp;</div>
<%--        <div class="grid_5 mtop">
            <% if (ViewData["brandId"] == "")
               { %>
            Create Brand
            <%  } %>
            <% else
                { %>
            Edit Brand
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
        <div class="grid_9 dborders" id="editboxbg">
            <div class="grid_2 edittext">
                Brand:</div>
            <div class="grid_6">
                <% if (ViewData["brandId"] == "")
                   { %>
                        <%= Html.Hidden("Id", "-1" )%>
                <%  } %>
                <% else
                    { %>
                        <%= Html.Hidden("Id", ViewData["brandId"] )%>
                <%  } %>
                <%= Html.TextBox("name", (String)(ViewData["brandName"]), new { @class = "input2" ,style = "width:90%" })%>
                <%= Html.ValidationMessage("Name", "*" )%>
            </div>
            <div class="clear" style="height: 15px;">
            </div>
            <div class="grid_2 edittext" style="margin-top: 0px">
                Image:
            </div>
            <% if (ViewData["brandId"] == "")
               { %>
            <div class="grid_6">
                <table class="grid_5">
                    <tr>
                        <td>
                            <input type="file" name="logo" class="btn" id="File1" value="Browse" />
                            <%= Html.ValidationMessage("logo", "*", new { style = "float:right" })%>
                        </td>
                    </tr>
                </table>
            </div>
            <%  } %>
            <% else
                { %>
                    <div class="grid_6" style="text-align: center; vertical-align: middle; height: 240px;border: 1px solid #125294;">
                        <% 
                           if ( ViewData["brandLogo"] == null || ViewData["brandLogo"].ToString() == "")
                           { %>
                                <div id="divNoImageSelected" style="width: 100px;height: 100px;margin-top:100px;margin-right:auto;margin-left:auto;font-size:14px;" >
                                    <label id="noImageSelected" >No Image Selected</label>
                                </div>
                                <div id="divBrandLogo" class="preview" style="width: auto;height: auto;margin-top:70px;display:none;visibility:hidden;" >
                                    <img src='<%=(String)(ViewData["brandLogo"])%>' alt="abc" id="brandLogo" style="width: 100px;height: 100px;" align="middle" />
                    			</div>
                                <%= Html.Hidden("bdLogo", ViewData["brandLogo"])%>
                        <% }
                           else
                           { %>
                                <div id="divNoImageSelected" style="width: 100px;height: 100px;margin-top:100px;margin-right:auto;margin-left:auto;font-size:14px;display:none;visibility:hidden;" >
                                    <label id="noImageSelected" >No Image Selected</label>
                                </div>
                                <div id="divBrandLogo" class="preview" style="margin-top:70px;width: 100px;height: 100px;margin-left:auto;margin-right:auto;" >
                                    <img src='<%=(String)(ViewData["brandLogo"])%>' alt="abc" id="brandLogo" style="width: 100px;height: 100px;" align="middle" />
                                </div>
                                <%= Html.Hidden("bdLogo", ViewData["brandLogo"])%>
                         <%   }
                           %>
                    </div>
            <%  } %>
            <div class="clear" style="height: 15px;">
                &nbsp;</div>
            <div style="height: 15px;width:52px;float:left;">
            </div>
            <div class="grid_5">
                <label>
                    <% if (ViewData["brandId"] != "")
                       { %>
                            <div id="divUploadButton" style="width:80px;height:30px;position:relative;float:right;" >
                                <input type="file" name="logo" onchange="fillIamge(this ,'frmBrandsEdit','/brands/edit', 'brandLogo', 'divBrandLogo' )" class="btn" id="imageUpload" value="Browse" />
                                <div class="fakefile">
                                        <img src="../../Content/Images/button_select.gif" />
                                </div>
                            </div>
                    <%  } %>
                </label>
            </div>
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
            <% if (ViewData["brandId"] == "")
               { %>
            <input type="submit" value="Create" class="btnedit" />
            <%  } %>
            <% else
                { %>
            <input type="submit" value="Save" class="btnedit" />
            <%  } %>
            <%= Html.ActionLink("Cancel", "Index", new { controller = "Brands" , page = 1 }, new { @class = "btndelete" })%>
        </div>
        <div class="grid_6">
            &nbsp;</div>
        <div class="clear">
            &nbsp;</div>
    </div>
    <% } %>
</asp:Content>
