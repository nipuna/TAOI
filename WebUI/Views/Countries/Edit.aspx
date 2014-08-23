<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<DomainModel.Entities.Country>>" %>

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
        <div class="grid_16" style="width: 630px">
            &nbsp;
            <% if (ViewData["countryId"] == "")
               { %>
            &nbsp;<%= Html.ValidationSummary("Creation was unsuccessful. Please correct the errors and try again.") %>
            <%  } %>
            <% else
                { %>
            &nbsp;<%= Html.ValidationSummary("Editing was unsuccessful. Please correct the errors and try again.") %>
            <%  } %>
        </div>
        <% using (Html.BeginForm("Edit", "Countries", FormMethod.Post, new { enctype = "multipart/form-data" , id = "frmCountriesEdit" }))
           { %>
        <div class="grid_8">
            &nbsp;</div>
<%--        <div class="grid_5 mtop">
            <% if (ViewData["countryId"] == "")
               { %>
            Create Country
            <%  } %>
            <% else
                { %>
            Edit Country
            <%  } %>
        </div>--%>
        <div class="clear">
            &nbsp;</div>
        <div class="grid_8">
            &nbsp;</div>
        <div class="grid_16">
            &nbsp;
        </div>
        <div class="grid_8">
            &nbsp;
        </div>
        <div class="grid_9 dborders" id="editboxbg">
            <div class="grid_2 edittext">
                Country:</div>
            <div class="grid_6">
                <% if (ViewData["countryId"] == "")
                   { %>
                <%= Html.Hidden("Id", "-1" )%>
                <%  } %>
                <% else
                    { %>
                <%= Html.Hidden("Id", ViewData["countryId"] )%>
                <%  } %>
                <%= Html.TextBox("name", (String)(ViewData["countryName"]), new { @class = "input2" ,style = "width:94%" })%>
                <%= Html.ValidationMessage("Name", "*" )%>
            </div>
            <div class="clear" style="height: 15px;">
            </div>
            <div class="grid_2 edittext" style="margin-top: 0px">
                Image:
            </div>
            <% if (ViewData["countryId"] == "")
               { %>
                    <div class="grid_6">
                        <input type="file" name="logo" class="btn" id="File1" value="Browse" />
                        <%= Html.ValidationMessage("logo", "*", new { style = "float:right" })%>
                    </div>
            <%  } %>
            <% else
                { %>
                    <div class="grid_6" style="text-align: center; vertical-align: middle; height: 240px;border: 1px solid #125294;">
                        <% 
                            if (ViewData["CountryLogo"] == null || ViewData["CountryLogo"].ToString() == "")
                           { %>
                                <div id="divNoImageSelected" style="width: 100px;height: 100px;margin-top:100px;margin-right:auto;margin-left:auto;font-size:14px;" >
                                    <label id="noImageSelected" >No Image Selected</label>
                                </div>
                                <div id="divCountryLogo" class="preview" style="width: auto;height: auto;margin-top:70px;display:none;visibility:hidden;" >
                                    <img src='<%=(String)(ViewData["CountryLogo"])%>' alt="abc" id="countryLogo" style="width: 100px; height: 100px;" align="middle" />
                    			</div>
                                <%= Html.Hidden("cntLogo", ViewData["CountryLogo"])%>
                        <% }
                           else
                           { %>
                                <div id="divNoImageSelected" style="width: 100px;height: 100px;margin-top:100px;margin-right:auto;margin-left:auto;font-size:14px;display:none;visibility:hidden;" >
                                    <label id="noImageSelected" >No Image Selected</label>
                                </div>
                                <div id="divCountryLogo" class="preview" style="margin-top:70px;width: 100px;height: 100px;margin-left:auto;margin-right:auto;" >
                                    <img src='<%=(String)(ViewData["CountryLogo"])%>' alt="abc" id="countryLogo" style="width: 100px; height: 100px;" align="middle" />
                                </div>
                                <%= Html.Hidden("cntLogo", ViewData["CountryLogo"])%>
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
                    <% if (ViewData["countryId"] != "")
                       { %>
                            <div id="divUploadButton" style="width:80px;height:30px;position:relative;float:right;" >
                               <input type="file" name="logo" onchange="fillIamge(this , 'frmCountriesEdit','/countries/edit','countryLogo', 'divCountryLogo')" class="btn" id="imageUpload" value="Browse" />
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
            <% if (ViewData["countryId"] == "")
               { %>
            <input type="submit" value="Create" class="btnedit" />
            <%  } %>
            <% else
                { %>
            <input type="submit" value="Save" class="btnedit" />
            <%  } %>
            <%= Html.ActionLink("Cancel", "Index", new { controller = "Countries" , page = 1 }, new { @class = "btndelete" })%>
        </div>
        <div class="grid_6">
            &nbsp;</div>
        <div class="clear">
            &nbsp;</div>
    </div>
    <% } %>
</asp:Content>
