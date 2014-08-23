<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="WebUI.WebForm1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" type="text/css" href="../../Content/css/Admin/Customers.css" />
    <link href="../../Content/css/Admin/css.css" rel="Stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div id="divBrandsPopUp" style="margin-top: 20px; margin-right: auto; margin-bottom: 0px; margin-left: auto;height: 280px; float: left; width: 280px;">
        <div class="boxbgheading" style="width:280px;float: left;margin-top:0px;" >
            <div style="padding-left:15px;margin-top:5px;float: left;" >Customer Details</div>
            <div style="float: right;">
                <a href="#" class="lbAction" rel="deactivate">
                    <img id="Img7" style="border-width: 0px;" src="Content/Images/close.gif" />
                </a>
            </div>
        </div>
        <div class="boxbg" style="width:280px;height:246px;float: left;" >
            <div class="edittext" style="float:left;margin-left: 30px;text-align:left;width:50px;">Name:</div>
            <div class="brandinputarea" >
                <div id="divSearch" style="float:left;vertical-align:middle;margin-top:5px;" >
                    <img src="Content/Images/search.png" />
                </div>
                <input type="text" name="txtBrandName" style="border:none;margin-left:5px;height:25px;width:120px;" />
            </div>
            <div class="edittext" style="float:left;margin-left: 30px;text-align:left;width:50px;">Logo:</div>
            <div class="edittext" style="text-align:center; vertical-align: middle; height: 110px;width:190px;background-color:#fff;margin-top:10px;">
                <img src='' alt="brand" id="brandLogo" style="width: 100px;height: 100px;" align="middle" />
                <input type="text" name="txtHdBrandId" style="border:none;margin-left:5px;height:25px;width:120px;visibility:hidden;" />
            </div>
            <div class="footerline" style="height:2px;background-color:#5E94CC;margin-top:30px;" >
            &nbsp;</div>
            <div style="width:150px;margin-top:5px;margin-right:auto;margin-left:auto;height:30px;" >
                <div style="float:left;width:auto;height:30px;" >
                    <input type="submit" value="OK" class="btnedit" />
                </div>
                <div style="float:right;height:30px;" >
                    <input type="submit" value="Cancel" class="btnedit" />
                </div>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
