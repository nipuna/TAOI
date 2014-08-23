

function removingBrand(brandId ) {

    var divToRemove = document.getElementById("imgContainer_" + brandId);
    divToRemove.style.visibility = "hidden";
    divToRemove.style.display = "none";
    //document.removeChild(divToRemove);
    $(divToRemove).remove();
}

function addingBrand() {

    var brandId = document.getElementById("hdBrandId").value;
    var divToCheck = document.getElementById("imgContainer_" + brandId);
    //alert(document.getElementById("hdBrandId").value);

    if (divToCheck == null) {

        $.get("/Customers/getBrand?Id=" + brandId,
                function (myData) {

                    //alert(myData);

                    $(myData).appendTo('#divBrands');

                }
                );
    }
    else {
        alert("brand already exists in the selection");
    }

}

function showPopUpDIv(id, ctlPlaceID) {
    document.getElementById(id).style.display = 'block';
    document.getElementById(id).style.visibility = 'visible';
    document.getElementById(id).style.top = TopPos(document.getElementById(ctlPlaceID), -330);
    document.getElementById(id).style.left = LeftPos(document.getElementById(ctlPlaceID), 100);
}

function hidePopUpDIv(id) {
    document.getElementById(id).style.display = 'none';
    document.getElementById(id).style.visibility = 'hidden';
}

function TopPos(obj, pos)
{
    var topCoord = 0;
	while(obj)
	{
	   topCoord += obj.offsetTop;
	   obj = obj.offsetParent;
	}
	return topCoord + pos + 'px';
}

/*// #A 052005 AP: To get the left position of the object*/
function LeftPos(obj, pos)
{
    var leftCoord = 0;    
    while(obj)
	{
	   leftCoord += obj.offsetLeft;
	   obj = obj.offsetParent;	   
	}	
	return leftCoord + pos + 'px';
}

function format(item) {
        return item;
}

$(document).ready(function () {

    $("#txtBrandName").autocomplete("/Customers/getBrandLike", {
        width: 200,
        multiple: true,
        dataType: "json",
        parse: function (data) {
            return $.map(data, function (row) {
                return {
                    data: row,
                    value: row,
                    result: row
                }
            });
        },
        selectFirst: false,
        formatItem: function (item) {
            return item;
        }
    });

    $("#txtBrandName").result(function (event, data, formatted) {

        if (data) {
            $.get("/Customers/getBrandForName?name=" + data,
                        function (dataR) {
                            
                            var data = eval(dataR);
                            document.getElementById("brandLogo").src = data[0].Logo;
                            $("#brandLogo").src = data[0].Logo;
                            document.getElementById("hdBrandId").value = data[0].ID;

                        });
        }

    });

}

);





