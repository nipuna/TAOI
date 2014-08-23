<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<DomainModel.Entities.Culture>>" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Languages
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
    <link rel="stylesheet" type="text/css" href="/Content/css/Admin/languages.css"
        title="languages" media="screen" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div id="language">
        <ul>
            <% foreach (var language in Model)
               { %>
            <li>
                <%= Html.ActionLink(language.Locale, "Toggle", "Languages", new { id = language.ID }, new { @class = language.IsSupported ? "supported" : "" })%>
            </li>
            <% } %>
        </ul>
    </div>
</asp:Content>
