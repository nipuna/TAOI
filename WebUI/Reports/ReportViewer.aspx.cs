using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;

namespace WebUI.Reports
{
    public partial class ReportViewer : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {

            string sReport;
            string sPath;
            string sUserID;

            ReportViewerWeb.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
            ReportViewerWeb.ServerReport.ReportServerUrl = new Uri(@"http://localhost/reportserver/");
            sReport = this.Request.QueryString.Get("Report");

            switch (sReport)
            {
                case "FeatureResults":
                    sPath = "/IOTA Reports/Phone Feature Results Summary";
                    break;
                case "IOCompatibility":
                    sPath = "/IOTA Reports/Phone Interoperability Comparison Overview";
                    break;
                case "TestComments":
                    sPath = "/IOTA Reports/Phone Comment Results Summary";
                    break;
                case "Translations":
                    sPath = "/IOTA Reports/Customer Translations";
                    break;
                default:
                    sPath = "";
                    break;
            }

            ReportViewerWeb.ServerReport.ReportPath = sPath;

            sUserID = "";
            sUserID += Session["userId"];

            List<ReportParameter> paramList = new List<ReportParameter>();
            paramList.Add(new ReportParameter("UserID", sUserID, false));
            ReportViewerWeb.ServerReport.SetParameters(paramList);

            ReportViewerWeb.ServerReport.Refresh();

        }

    }
}
