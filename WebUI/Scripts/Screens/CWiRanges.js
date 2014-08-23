var devices;
var totalDevices;
var deviceId;

function getDevicesForBrands(index) {

    var id = ($("#brand").get()[0])[index].value;
    //($("#brandForProduct").get()[0]).selectedIndex = index;    
    var rnd = Math.random();

    $.get("/CWIRanges/getDevicesForbrand?brandId=" + id + "&" + rnd,
            function (arDevices) {
                devices = eval(arDevices);
                var ddDevices = document.getElementById("Device");
                ddDevices.innerHTML = ""
                clearDeviceDetails();
                $.each(devices, function (entrIndex, entry) {
                    var option = document.createElement("OPTION");
                    option.text = entry['Model'];
                    option.value = entry['ID'];
                    ddDevices.options.add(option);
                    if (entrIndex == 0) {
                        fillDeviceDetails(entry);
                    }

                });

                totalDevices = devices.length;

                document.getElementById("deveicesPager").innerHTML = "1 of " + totalDevices;
                document.getElementById("imgPreviousDevice").disabled = false;
                document.getElementById("imgNextDevice").disabled = false;

            });
            getBrandsForProducts(($("#brand").get()[0]).selectedIndex);
}


function getDetailsForDevice(index) {

    var id = ($("#Device").get()[0])[index].value;
    var rnd = Math.random();

    $.get("/CWIRanges/getDetailsForDevice?deviceId=" + id + "&" + rnd,
            function (arDetail) {
                detail = eval(arDetail);
                clearDeviceDetails();
                $.each(detail, function (entrIndex, entry) {

                    if (entrIndex == 0) {

                        fillDeviceDetails(entry);

                    }

                    if (($("#Product").get()[0]).selectedIndex != 0) {
                        getRangesForProductSelected(($("#Product").get()[0]).selectedIndex);
                    }

                });

            });

            document.getElementById("deveicesPager").innerHTML = index + 1 + " of " + totalDevices;

}

function getDetailsForNextDevice() {

    var index = ($("#Device").get()[0]).selectedIndex + 1;
    if (index >= devices.length) {
        alert("Currently displaying the last record");
        return false;
    }
    ($("#Device").get()[0]).selectedIndex = index;
    var id = ($("#Device").get()[0])[index].value;
    var rnd = Math.random();

    $.get("/CWIRanges/getDetailsForDevice?deviceId=" + id + "&" + rnd,
            function (arDetail) {
                detail = eval(arDetail);
                clearDeviceDetails();
                $.each(detail, function (entrIndex, entry) {

                    fillDeviceDetails(entry);

                    if (($("#Product").get()[0]).selectedIndex != 0) {
                        getRangesForProductSelected(($("#Product").get()[0]).selectedIndex);
                    }
                });

            });

        document.getElementById("deveicesPager").innerHTML = index + 1 + " of " + totalDevices;

}

function getDetailsForPreviousDevice() {

    var index = ($("#Device").get()[0]).selectedIndex - 1;
    if (index < 0) {
        alert("Currently displaying the first record");
        return false;
    }
    ($("#Device").get()[0]).selectedIndex = index;
    var id = ($("#Device").get()[0])[index].value;
    var rnd = Math.random();

    $.get("/CWIRanges/getDetailsForDevice?deviceId=" + id + "&" + rnd,
            function (arDetail) {
                detail = eval(arDetail);
                clearDeviceDetails();
                $.each(detail, function (entrIndex, entry) {

                    fillDeviceDetails(entry);

                    if (($("#Product").get()[0]).selectedIndex != 0) {
                        getRangesForProductSelected(($("#Product").get()[0]).selectedIndex);
                    }
                });

            });

            document.getElementById("deveicesPager").innerHTML = index + 1 + " of " + totalDevices;

}

function fillDeviceDetails(entry) {
    deviceId = entry['ID'];
    document.getElementById("software").value = entry['Software'];
    document.getElementById("software").disabled = true;

    document.getElementById("Hardware").value = entry['hardware'];
    document.getElementById("Hardware").disabled = true;

    document.getElementById("Serial").value = entry['SerialNumber'];
    document.getElementById("Serial").disabled = true;

    document.getElementById("Country").value = entry['Country'];
    document.getElementById("Country").disabled = true;

    document.getElementById("NextGenID").value = entry['NextgenId'];
    document.getElementById("NextGenID").disabled = true;
                   
}

function clearDeviceDetails() {

    document.getElementById("software").value = "";
    document.getElementById("software").disabled = true;

    document.getElementById("Hardware").value = "";
    document.getElementById("Hardware").disabled = true;

    document.getElementById("Serial").value = "";
    document.getElementById("Serial").disabled = true;

    document.getElementById("Country").value = "";
    document.getElementById("Country").disabled = true;

    document.getElementById("NextGenID").value = "";
    document.getElementById("NextGenID").disabled = true;

}


function getBrandsForProducts(index) {

    var id = ($("#brand").get()[0])[index].value;
    var rnd = Math.random();

    $.get("/CWIRanges/getBrandsForProducts?BrandId=" + id + "&" + rnd,
            function (arProducts) {
                products = eval(arProducts);
                var ddProducts = document.getElementById("brandForProduct");
                ddProducts.innerHTML = ""
                var option = document.createElement("OPTION");
                option.text = "Select";
                option.value = "0";
                ddProducts.options.add(option);
                $.each(products, function (entrIndex, entry) {
                    var option = document.createElement("OPTION");
                    option.text = entry['Name'];
                    option.value = entry['ID'];
                    ddProducts.options.add(option);
                });

            });

}



function getProductsForBrand(index) {

    var id = ($("#brandForProduct").get()[0])[index].value;
    var rnd = Math.random();

    $.get("/CWIRanges/getProductForBrand?BrandId=" + id + "&" + rnd,
            function (arProducts) {
                products = eval(arProducts);
                var ddProducts = document.getElementById("Product");
                ddProducts.innerHTML = ""
                var option = document.createElement("OPTION");
                option.text = "Select";
                option.value = "0";
                ddProducts.options.add(option);
                $.each(products, function (entrIndex, entry) {
                    var option = document.createElement("OPTION");
                    option.text = entry['Model'];
                    option.value = entry['ID'];
                    ddProducts.options.add(option);
                });

            });

}


function getRangesForProductSelected(index) {

    var productId = ($("#Product").get()[0])[index].value;

    var rnd = Math.random();

    $.get("/CWIRanges/getRangesForDeviceSelected?deviceId=" + deviceId + "&productId=" + productId + "&" + rnd,
            function (data) {
                $("#ranges").empty();
                $("#ranges").append(data);

                var sIndex = ($("#brand").get()[0]).selectedIndex;
                var deviceBrand = ($("#brand").get()[0])[sIndex].text;
                var sIndex = ($("#Device").get()[0]).selectedIndex;
                var deviceS = ($("#Device").get()[0])[sIndex].text;
                var sIndex = ($("#brandForProduct").get()[0]).selectedIndex;
                var productBrand = ($("#brandForProduct").get()[0])[sIndex].text;
                var sIndex = ($("#Product").get()[0]).selectedIndex;
                var product = ($("#Product").get()[0])[sIndex].text;
                $("#deviceName").empty();
                $("#deviceName").append("Ranges For " + deviceBrand + " " + deviceS + " and " + productBrand + " " + product);
                $("#vehicleName").empty();
                $("#vehicleName").append("Other Ranges for " + productBrand + " " + product);

                document.getElementById("gridRanges").style.display = 'block';
                document.getElementById("gridRanges").style.visibility = 'visible';
                document.getElementById("gridOtherRanges").style.display = 'block';
                document.getElementById("gridOtherRanges").style.visibility = 'visible';
                $("#gbox_list").remove();
                $("#otherGrid").append("<table id='list' class='scroll' cellpadding='0' cellspacing='0' ></table>");
                AllDataGrid.url = '/CWIRanges/getJQgridData?vehicleId=' + productId;
                AllDataGrid.names[0] = 'Type';
                AllDataGrid.names[1] = 'From';
                AllDataGrid.names[2] = 'To';
                AllDataGrid.names[3] = 'IsLatest';
                AllDataGrid.names[4] = 'System';
                AllDataGrid.pageSize = 10;
                AllDataGrid.sortBy = 'Type';
                AllDataGrid.setDefaults();
                AllDataGrid.setupGridCWI($("#list"), $("#pager"), $("#search"));

            });

}

function removeRangeDiv(index) {
    $("#mainRangeDiv" + index).remove();
}

function createRangeDiv(index) {

    $("#createRangeDiv").remove();
    var divNewRange = "";
    var count = $("div[id^=mainRangeDiv]").get().length + 1;
    
    divNewRange = divNewRange + "<div id='mainRangeDiv" + count + "' style='margin-left:0px;width:610px;' >";
    divNewRange = divNewRange + "<div id='rangeType" + count + "' style='width:160px;text-align:center;font-weight:bold;margin:5px;float:left;' >";
    divNewRange = divNewRange + "<select id='ddRangeType" + count + "' onchange='renderRangeSelector(" + count + ")' >";
    divNewRange = divNewRange + "<option value='0' >Select</option>";
    divNewRange = divNewRange + "<option value='Range' >Range</option>";
    divNewRange = divNewRange + "<option value='Expression' >Regular Expression</option>";
    divNewRange = divNewRange + "</select>";
    divNewRange = divNewRange + "</div>";
    divNewRange = divNewRange + "<div id='rangeInserter" + count + "' style='width:220px;font-weight:bold;float:left;' >";
    divNewRange = divNewRange + "<div id='rangeFrom" + count + "' style='width:100px;text-align:center;font-weight:bold;margin:5px;float:left;' >";
    divNewRange = divNewRange + "<input type='text' class='required' title='*' maxlength='1000' name='rangesChoosen[" + (count - 1) + "].RangeStart' style='width:100px;' />";
    divNewRange = divNewRange + "</div>";
    divNewRange = divNewRange + "<div id='rangeTo" + count + "' style='width:100px;text-align:center;font-weight:bold;float:left;margin:5px;' >";
    divNewRange = divNewRange + "<input type='text' class='required' title='*'  maxlength='1000' name='rangesChoosen[" + (count - 1) + "].RangeEnd' style='width:100px;' />";
    divNewRange = divNewRange + "</div>";
    divNewRange = divNewRange + "</div>";
    divNewRange = divNewRange + "<div id='rangeLatest" + count + "' style='width:100px;text-align:center;font-weight:bold;float:left;margin:5px;' >";
    divNewRange = divNewRange + "<input value='false' onclick = 'checkUncheckRadioButtons(this)' type='radio' name='rangesChoosen[" + (count - 1) + "].IsLatest' value='' />";
    divNewRange = divNewRange + "</div>";
    divNewRange = divNewRange + "<div id='rangeActions" + count + "' style='width:100px;text-align:center;font-weight:bold;float:left;margin:5px;' >";
    divNewRange = divNewRange + "<img src='../../Content/Images/Admin/deleteB.gif' onclick='removeRangeDiv(" + count + ")' />";
    divNewRange = divNewRange + "</div>";
    divNewRange = divNewRange + "</div>";
    divNewRange = divNewRange + "<div id='createRangeDiv' style='width:100px;float:right;margin-top:5px;' >";
    divNewRange = divNewRange + "<img src='../../Content/Images/Admin/createB.gif' onclick='createRangeDiv()' />";
    divNewRange = divNewRange + "</div>";

    $("#ranges").append(divNewRange);
    appplyValidations();
}

function renderRangeSelector(index) {

    var sIndex = ($("#ddRangeType" + index).get()[0]).selectedIndex;
    var rangeType = ($("#ddRangeType" + index).get()[0])[sIndex].value;
    var divNewRange = "";
    $("#rangeInserter" + index).empty();
    var count = $("div[id^=mainRangeDiv]").get().length + 1;
    
    if (rangeType == "Range") {
        divNewRange = divNewRange + "<div id='rangeFrom" + index + "' style='width:100px;text-align:center;font-weight:bold;margin:5px;float:left;' >";
        divNewRange = divNewRange + "<input type='text' class='required' title='*' maxlength='1000' name='rangesChoosen[" + (index - 1) + "].RangeStart' style='width:100px;' />";
        divNewRange = divNewRange + "</div>";
        divNewRange = divNewRange + "<div id='rangeTo" + index + "' style='width:100px;text-align:center;font-weight:bold;float:left;margin:5px;' >";
        divNewRange = divNewRange + "<input type='text' class='required' title='*' maxlength='1000' name='rangesChoosen[" + (index - 1) + "].RangeEnd' style='width:100px;' />";
        divNewRange = divNewRange + "</div>";
    }
    else if (rangeType == "Expression") {
        divNewRange = divNewRange + "<div id='RegularExpression" + index + "' style='width:100px;text-align:center;font-weight:bold;margin:5px;float:left;' >";
        divNewRange = divNewRange + "<input type='text' class='required' title='*' maxlength='1000' name='rangesChoosen[" + (index - 1) + "].RegularExpression' style='width:200px;' />";
        divNewRange = divNewRange + "</div>";
    }
    $("#rangeInserter" + index).append(divNewRange);

}

function appplyValidations() {
    var a = 1;
    a = a + 1;
    $(":input[type='text']").rules("add", {
        required: true,
        messages: { required: "*" }
    });

}
var radioName = "rangesChoosen";

function checkUncheckRadioButtons(chk) {
    if (chk.checked == true) {
        $("INPUT[type='radio']").attr('checked', false);
        $("INPUT[type='radio']").attr('value', "false");
        //checkStatus = true;
        chk.checked = true;
        chk.value = "true";
        return;
    }

//    if (chk.checked == true) {
//        chk.checked = false;
//    }

}

