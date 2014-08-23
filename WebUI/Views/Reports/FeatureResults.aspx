<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    FeatureResults
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
        <iframe src="../../Reports/ReportViewer.aspx?Report=FeatureResults" width="960px" height="2000px">
        <p>
            Ooops! Your browser does not support iframes.</p>
    </iframe>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
