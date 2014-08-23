 

AllDataGrid = {
    url: "",
    names: new Array(3),
    caption: "",
    pageSize: "",
    setDefaults: function() {
            $.jgrid.defaults = $.extend($.jgrid.defaults, {
                datatype: 'json',
                height: 'auto',
                imgpath: '/Scripts/jqGrid/themes/redmond/images',
                jsonReader: {
                    root: "Rows",
                    page: "Page",
                    total: "Total",
                    records: "Records",
                    repeatitems: false,
                    userdata: "UserData",
                    id: "Id"
                },
                loadui: "block",
                mtype: 'GET',
                multiboxonly: true,
                rowNum: 20,
                rowList: [10, 20, 50],
                viewrecords: true
            });
        },
    sortBy : "",
    setupGrid: function (grid, pager, search) {
        grid.jqGrid({
            colNames: [this.names[0], this.names[1], this.names[2]],
            colModel: [
                          { name: this.names[0], index: this.names[0], align: 'center' },  
                          { name: this.names[1], index: this.names[1], align: 'left' },
                          { name: this.names[2], index: '', align: 'center' }
                      ],
            pager: pager,
            sortname: this.sortBy,
            rowNum: this.pageSize,
            rowList: [10, 20, 50],
            sortorder: "asc",
            url: this.url,
            caption: this.caption
        }).navGrid(pager, { edit: false, add: false, del: false, search: false });
        search.filterGrid("#" + grid.attr("id"), {
            gridModel: false,
            filterModel: [{
                label: 'Search',
                name: 'search',
                stype: 'text'
            } ]
        });
    },
    setupGridUser: function (grid, pager, search) {
        grid.jqGrid({
            colNames: [this.names[0], this.names[1], this.names[2], this.names[3], this.names[4], this.names[5], this.names[6], this.names[7]],
            colModel: [
                          { name: this.names[0], index: this.names[0], width: 100, align: 'center' },
                          { name: this.names[1], index: this.names[1], width: 100, align: 'center' },
                          { name: this.names[2], index: this.names[2], width: 100, align: 'center' },
                          { name: this.names[3], index: this.names[3], width: 150, align: 'center' },
                          { name: this.names[4], index: this.names[4], width: 100, align: 'center' },
                          { name: this.names[5], index: this.names[5], width: 100, align: 'center' },
                          { name: this.names[6], index: this.names[6], width: 100, align: 'center' },
                          { name: this.names[7], index: '', width: 150, align: 'center' }
                      ],
            pager: pager,
            sortname: 'Name',
            width: 900,
            rowNum: this.pageSize,
            rowList: [10, 20, 50],
            sortorder: "asc",
            url: this.url,
            caption: this.caption
        }).navGrid(pager, { edit: false, add: false, del: false, search: false });
        search.filterGrid("#" + grid.attr("id"), {
            gridModel: false,
            filterModel: [{
                label: 'Search',
                name: 'search',
                stype: 'text'
            } ]
        });
    },
    setupGridCWI: function (grid, pager, search) {
        grid.jqGrid({
            colNames: [this.names[0], this.names[1], this.names[2], 'Latest', 'System(Software/Hardware/Serial/Country)', ],
            colModel: [
                          { name: this.names[0], index: this.names[0], width: 150, align: 'left' },
                          { name: this.names[1], index: this.names[1], width: 60, align: 'left' },
                          { name: this.names[2], index: this.names[2], width: 60, align: 'left' },
                          { name: this.names[3], index: this.names[3], width: 60, align: 'left' },
                          { name: this.names[4], index: this.names[4], width: 320, align: 'left' },
                      ],
            pager: pager,
            sortname: 'Type',
            width: 650,
            rowNum: this.pageSize,
            rowList: [10, 20, 50],
            sortorder: "asc",
            url: this.url,
            caption: this.caption
        }).navGrid(pager, { edit: false, add: false, del: false, search: false });
        search.filterGrid("#" + grid.attr("id"), {
            gridModel: false,
            filterModel: [{
                label: 'Search',
                name: 'search',
                stype: 'text'
            }]
        });
    },
    setupGridVehicles: function (grid, pager, search) {
        grid.jqGrid({
            colNames: [this.names[0], this.names[1], this.names[2], this.names[3]],
            colModel: [
                          { name: this.names[0], index: this.names[0], align: 'center' },
                          { name: this.names[1], index: this.names[1], align: 'left' },
                          { name: this.names[2], index: this.names[2], align: 'center' },
                          { name: this.names[3], index: '', align: 'center' }
                      ],
            pager: pager,
            sortname: this.sortBy,
            rowNum: this.pageSize,
            rowList: [10, 20, 50],
            sortorder: "asc",
            url: this.url,
            caption: this.caption
        }).navGrid(pager, { edit: false, add: false, del: false, search: false });
        search.filterGrid("#" + grid.attr("id"), {
            gridModel: false,
            filterModel: [{
                label: 'Search',
                name: 'search',
                stype: 'text'
            }]
        });
    },
    setupGridCwiText: function (grid, pager, search) {
        grid.jqGrid({
            colNames: [this.names[0], this.names[1], this.names[2]],
            colModel: [
                          { name: this.names[0], index: this.names[0], width: 300, align: 'left' },
                          { name: this.names[1], index: this.names[1], width: 250, align: 'left' },
                          { name: this.names[2], index: this.names[2], width: 150, align: 'center' },
                      ],
            pager: pager,
            sortname: this.sortBy,
            width: 700,
            rowNum: this.pageSize,
            rowList: [10, 20, 50],
            sortorder: "asc",
            url: this.url,
            caption: this.caption
        }).navGrid(pager, { edit: false, add: false, del: false, search: false });
        search.filterGrid("#" + grid.attr("id"), {
            gridModel: false,
            filterModel: [{
                label: 'Search',
                name: 'search',
                stype: 'text'
            }]
        });
    }
    
};

