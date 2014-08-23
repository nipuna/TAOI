<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<table>
    <tr>
        <td>
            <span id="statusname"><%= ViewData["name"] %> </span>
            <br />
            <span id="statuslogintime"><%= ViewData["logintime"]%></span>
        </td>
        <td>
            <img src="/Content/Images/loginLogo.jpg" alt="NextGen IOTA" />
        </td>
    </tr>
    <tr>
        <td>
            <span id="statuspendingactions"><%= ViewData["pendingActions"] %></span>            
        </td>
        <td style="text-align:center">
            <%= Html.ActionLink(ViewData["linkText"].ToString(), ViewData["actioName"].ToString(), 
            new { controller = ViewData["controller"].ToString(), action = ViewData["actioName"].ToString() },
                                            new { @class = "statusloginlink", style = "text-align:center;" })%>
        </td>
    </tr>
</table>