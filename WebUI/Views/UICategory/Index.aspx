<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<DomainModel.Entities.UICategory>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    User Interface Categories
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%foreach (var uiCategory in Model)
      {%>
    <div class="item">
        <h3>
            <%= uiCategory.Name%></h3>
        <%=uiCategory.URL%>
        <%=uiCategory.Icon%>
        <%foreach (var subCat in uiCategory.SubCategories)
          {%>
        <h4>
            <%= subCat.Name%>
        </h4>
        <p><%=subCat.URL%></p>
        <p><%=subCat.Icon%></p>
        <% } %> 
    </div>
    <% } %>
</asp:Content>
