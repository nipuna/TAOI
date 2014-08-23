

$(document).ready(function () {
    AllDataGrid.url = '/CWITexts/getJQgridData/';
    AllDataGrid.names[0] = 'CWIText';
    AllDataGrid.names[1] = 'BrandName';
    AllDataGrid.names[2] = 'Action';
    AllDataGrid.pageSize = 10;
    AllDataGrid.sortBy = 'CWIText';
    AllDataGrid.setDefaults();
    AllDataGrid.setupGridCwiText($("#list"), $("#pager"), $("#search"));
    var tdI = $(".ui-pg-input")[0].parentNode;
    tdI.style.width = "100px";
    var rnd = Math.random();

    $.get("/CWITexts/fillBrandsDropDown?" + rnd,
    function (myData) {
        //alert(myData);
        $(myData).appendTo('#ScrollCB');

    }
    );

});


function displayDiv(divName) {
    var divB = document.getElementById(divName);
    var divToDisp = document.getElementById("ScrollCB");
    //alert(divToDisp.style.visibility);
    if (divToDisp.style.visibility == "hidden") {
        divToDisp.style.top = TopPos(divB, 20);
        divToDisp.style.left = LeftPos(divB, 0);
        divToDisp.style.display = "inline";
        divToDisp.style.visibility = "visible";
    }
    else {
        document.getElementById('ScrollCB').style.visibility = 'hidden';
        document.getElementById('ScrollCB').style.display = 'none';
        filteringForBrand();
    }
}

function hideDiv()
{
    document.getElementById('ScrollCB').style.visibility = 'hidden';
    document.getElementById('ScrollCB').style.display = 'none';
}


function TopPos(obj, pos) {
    var topCoord = 0;
    while (obj) {
        topCoord += obj.offsetTop;
        obj = obj.offsetParent;
    }
    return topCoord + pos + 'px';
}

/*// #A 052005 AP: To get the left position of the object*/
function LeftPos(obj, pos) {
    var leftCoord = 0;
    while (obj) {
        leftCoord += obj.offsetLeft;
        obj = obj.offsetParent;
    }
    return leftCoord + pos + 'px';
}



function filteringForBrand() {

    var chkBoxs = $("#ScrollCB > Input[checked='true']").get();
    var brandIds = [];
    for (var x in chkBoxs) {
        brandIds[x] = chkBoxs[x].value;
    }
    var sbrandIds = JSON.stringify(brandIds);
    var rnd = Math.random();
    $("#search").remove();
    $("#gbox_list").remove();
    $("#otherGrid").append("<div id='search' style='width:300px;' ></div><table id='list' class='scroll' cellpadding='0' cellspacing='0' ></table><div id='pager' class='scroll' style='text-align: center;'></div>");
    AllDataGrid.url = '/CWITexts/filteringForBrand?brandIds=' + sbrandIds + "&" + rnd;
    AllDataGrid.names[0] = 'CWIText';
    AllDataGrid.names[1] = 'BrandName';
    AllDataGrid.names[2] = 'Action';
    AllDataGrid.pageSize = 10;
    AllDataGrid.sortBy = 'CWIText';
    AllDataGrid.setDefaults();
    AllDataGrid.setupGridCwiText($("#list"), $("#pager"), $("#search"));
    var tdI = $(".ui-pg-input")[0].parentNode;
    tdI.style.width = "100px";

}
