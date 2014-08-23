
function fillBrands(sIndex) {
    var tData;
    //alert("i m here");
    document.getElementById("brand").innerHTML = "";
    var dpdCustomers = document.getElementById("customer");

    var customerId = dpdCustomers.options[sIndex].value;
    $.get("/Vehicles/getBrandsForCustomer/", { 'customerId': customerId },
            function (data) {

                //alert("Data Loaded: " + data);
                var names = eval(data);
                $.each(names, function (entryIndex, entry) {
                    var ddBrands = document.getElementById("brand");
                        var optn = document.createElement("OPTION");
                        optn.text = entry['Name'];
                        optn.value = entry['ID'];
                        ddBrands.options.add(optn);
                    }
                
                )
            }
    );

            //document.getElementById("brands").style.width = "100%";

    return false;
}

