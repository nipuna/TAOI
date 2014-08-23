<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<DomainModel.Entities.UICategory>>" %>
<ul id="menu">
    <%= ViewData["menus"] %>
</ul>
