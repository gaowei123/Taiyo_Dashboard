
var apiGetPartsData = window.globalConfig.rootDirectory + '/MOULD_MonthlyHightestReject/GetPartsData';
var apiGetDefectsData = window.globalConfig.rootDirectory + '/MOULD_MonthlyHightestReject/GetDefectsData';




//初始化时间框
$('#txtDateFrom').datetimepicker({
    weekStart: 1,
    todayBtn: 1,
    autoclose: 1,
    todayHighlight: 1,
    startView: 2,
    minView: 2,
    forceParse: 0,
    initialDate: new Date()
});

$('#txtDateTo').datetimepicker({
    weekStart: 1,
    todayBtn: 1,
    autoclose: 1,
    todayHighlight: 1,
    startView: 2,
    minView: 2,
    forceParse: 0,
    initialDate: new Date()
});


$(document).ready(function () {
    //set machine dropdownlist
    setMachineDDL($('#ddlMachineID'), 9);

    //set part no dropdownlist
    setPartNoDDL($('#ddlPartNo'), 'Laser','');

    //set default date
    var datefrom = dateFormat('yyyy-MM-dd',new Date());
    $('#txtDateFrom').val(datefrom);
    $('#txtDateTo').val(datefrom);



    //init table
    $("#tableParts").bootstrapTable({ // 对应table标签的id


        //call后台数据
        method: 'post',
        dataType: 'json',
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
        url: apiGetPartsData,

        cache: false, // 设置为 false 禁用 AJAX 数据缓存， 默认为true

        //sidePagination: 'server', // 设置为服务器端分页
        queryParams: function (params) { // 请求服务器数据时发送的参数，可以在这里添加额外的查询参数，返回false则终止请求



            var aaa = $("#txtDateFrom").val();
            var ccc = $("#txtDateTo").val();
            var ddd = $("#ddlPartNo").val();
            var eee = $("#ddlMachineID").val();


            return {
                //pageSize: params.limit, // 每页要显示的数据条数
                //offset: params.offset, // 每页显示数据的开始行号
                //sort: params.sort, // 要排序的字段
                //sortOrder: params.order, // 排序规则
                //dataId: $("#dataId").val() // 额外添加的参数

                DateFrom: $("#txtDateFrom").val(),
                DateTo: $("#txtDateTo").val(),
                PartNo: $("#ddlPartNo").val(),
                MachineID: $("#ddlMachineID").val()
            }
        },
        //sortName: 'id', // 要排序的字段
        //sortOrder: 'desc', // 排序规则


        //表头样式 undefined, thead-light, thead-dark, 
        theadClasses: 'thead-title',

        //表格样式
        classes: 'table table-bordered table-hover',


        striped: false,  //表格显示条纹，默认为false
        pagination: false, // 在表格底部显示分页组件，默认false
        //pageList: [10, 20], // 设置页面可以显示的数据条数
        //pageSize: 10, // 页面数据条数
        //pageNumber: 1, // 首页页码

        columns: [{
            field: 'partNo',
            title: 'Top Rejected Parts',
            align: 'center',
            valign: 'middle'

        },
        {
            field: 'output',
            title: 'Output',
            align: 'center',
            valign: 'middle'
        },
        {
            field: 'rejQty',
            title: 'Rej Qty',
            align: 'center',
            valign: 'middle'
        },
        {
            field: 'rejRate',
            title: 'Rej Rate',
            align: 'center',
            valign: 'middle'
        },
        {
            field: 'highestDefect_1st',
            title: 'Highest Defect',
            align: 'center',
            valign: 'middle'
        },
        {
            field: 'highestDefect_2nd',
            title: '2nd Highest Defect',
            align: 'center',
            valign: 'middle'
        },
        {
            field: 'highestDefect_3rd',
            title: '3rd Highest Defect',
            align: 'center',
            valign: 'middle'
        }],
        onLoadSuccess: function () {  //加载成功时执行
            console.log("加载成功");
        },
        onLoadError: function () {  //加载失败时执行
            console.log("加载数据失败");
        }

    })

    $("#tableDefects").bootstrapTable({ // 对应table标签的id

        //call后台数据
        method: 'post',
        dataType: 'json',
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
        url: apiGetDefectsData,

        cache: false, // 设置为 false 禁用 AJAX 数据缓存， 默认为true

        //sidePagination: 'server', // 设置为服务器端分页
        queryParams: function (params) { // 请求服务器数据时发送的参数，可以在这里添加额外的查询参数，返回false则终止请求

            return {
                //pageSize: params.limit, // 每页要显示的数据条数
                //offset: params.offset, // 每页显示数据的开始行号
                //sort: params.sort, // 要排序的字段
                //sortOrder: params.order, // 排序规则
                //dataId: $("#dataId").val() // 额外添加的参数

                DateFrom: $("#txtDateFrom").val(),
                DateTo: $("#txtDateTo").val(),
                PartNo: $("#ddlPartNo").val(),
                MachineID: $("#ddlMachineID").val()
            }
        },
        //sortName: 'id', // 要排序的字段
        //sortOrder: 'desc', // 排序规则


        //表头样式 undefined, thead-light, thead-dark, 
        theadClasses: 'thead-title',

        //表格样式
        classes: 'table table-bordered table-hover',


        striped: false,  //表格显示条纹，默认为false
        pagination: false, // 在表格底部显示分页组件，默认false
        //pageList: [10, 20], // 设置页面可以显示的数据条数
        //pageSize: 10, // 页面数据条数
        //pageNumber: 1, // 首页页码

        columns: [{
            field: 'defectCode',
            title: 'Top Defect Type',
            align: 'center',
            valign: 'middle'

        },
        {
            field: 'totalRej',
            title: 'QTY',
            align: 'center',
            valign: 'middle'
        },
        {
            field: 'totalRejRate',
            title: 'Percentage',
            align: 'center',
            valign: 'middle'
        },
        {
            field: 'affectedPart1st',
            title: '1st Affected Part',
            align: 'center',
            valign: 'middle'
        },
        {
            field: 'affectedPart2nd',
            title: '2nd Affected Part',
            align: 'center',
            valign: 'middle'
        },
        {
            field: 'affectedPart3rd',
            title: '3rd Affected Part',
            align: 'center',
            valign: 'middle'
        }],
        onLoadSuccess: function () {  //加载成功时执行
            console.log("加载成功");
        },
        onLoadError: function () {  //加载失败时执行
            console.log("加载数据失败");
        }

    })

});



$(window).resize(function () {
    $('#tableParts').bootstrapTable('resetView');
    $('#tableDefects').bootstrapTable('resetView');
});




function search() {


    //重新加载必须将原本table清空
    $('#tableParts').bootstrapTable('destroy');
    $('#tableDefects').bootstrapTable('destroy');


    $("#tableParts").bootstrapTable({ // 对应table标签的id


        //call后台数据
        method: 'post',
        dataType: 'json',
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
        url: apiGetPartsData,

        cache: false, // 设置为 false 禁用 AJAX 数据缓存， 默认为true

        //sidePagination: 'server', // 设置为服务器端分页
        queryParams: function (params) { // 请求服务器数据时发送的参数，可以在这里添加额外的查询参数，返回false则终止请求

            return {
                //pageSize: params.limit, // 每页要显示的数据条数
                //offset: params.offset, // 每页显示数据的开始行号
                //sort: params.sort, // 要排序的字段
                //sortOrder: params.order, // 排序规则
                //dataId: $("#dataId").val() // 额外添加的参数

                DateFrom: $("#txtDateFrom").val(),
                DateTo: $("#txtDateTo").val(),
                PartNo: $("#ddlPartNo").val(),
                MachineID: $("#ddlMachineID").val()
            }
        },
        //sortName: 'id', // 要排序的字段
        //sortOrder: 'desc', // 排序规则


        //表头样式 undefined, thead-light, thead-dark, 
        theadClasses: 'thead-title',

        //表格样式
        classes: 'table table-bordered table-hover',


        striped: false,  //表格显示条纹，默认为false
        pagination: false, // 在表格底部显示分页组件，默认false
        //pageList: [10, 20], // 设置页面可以显示的数据条数
        //pageSize: 10, // 页面数据条数
        //pageNumber: 1, // 首页页码

        columns: [{
            field: 'partNo',
            title: 'Top Rejected Parts',
            align: 'center',
            valign: 'middle'

        },
        {
            field: 'output',
            title: 'Output',
            align: 'center',
            valign: 'middle'
        },
        {
            field: 'rejQty',
            title: 'Rej Qty',
            align: 'center',
            valign: 'middle'
        },
        {
            field: 'rejRate',
            title: 'Rej Qty',
            align: 'center',
            valign: 'middle'
        },
        {
            field: 'highestDefect_1st',
            title: '1st Highest Defect',
            align: 'center',
            valign: 'middle'
        },
        {
            field: 'highestDefect_2nd',
            title: '2nd Highest Defect',
            align: 'center',
            valign: 'middle'
        },
        {
            field: 'highestDefect_3rd',
            title: '3rd Highest Defect',
            align: 'center',
            valign: 'middle'
        }],
        onLoadSuccess: function () {  //加载成功时执行
            console.log("加载成功");
        },
        onLoadError: function () {  //加载失败时执行
            console.log("加载数据失败");
        }

    })
    $("#tableDefects").bootstrapTable({ // 对应table标签的id

        //call后台数据
        method: 'post',
        dataType: 'json',
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
        url: apiGetDefectsData,

        cache: false, // 设置为 false 禁用 AJAX 数据缓存， 默认为true

        //sidePagination: 'server', // 设置为服务器端分页
        queryParams: function (params) { // 请求服务器数据时发送的参数，可以在这里添加额外的查询参数，返回false则终止请求

            return {
                //pageSize: params.limit, // 每页要显示的数据条数
                //offset: params.offset, // 每页显示数据的开始行号
                //sort: params.sort, // 要排序的字段
                //sortOrder: params.order, // 排序规则
                //dataId: $("#dataId").val() // 额外添加的参数

                DateFrom: $("#txtDateFrom").val(),
                DateTo: $("#txtDateTo").val(),
                PartNo: $("#ddlPartNo").val(),
                MachineID: $("#ddlMachineID").val()
            }
        },
        //sortName: 'id', // 要排序的字段
        //sortOrder: 'desc', // 排序规则


        //表头样式 undefined, thead-light, thead-dark, 
        theadClasses: 'thead-title',

        //表格样式
        classes: 'table table-bordered table-hover',


        striped: false,  //表格显示条纹，默认为false
        pagination: false, // 在表格底部显示分页组件，默认false
        //pageList: [10, 20], // 设置页面可以显示的数据条数
        //pageSize: 10, // 页面数据条数
        //pageNumber: 1, // 首页页码

        columns: [{
            field: 'defectCode',
            title: 'Top Defect Type',
            align: 'center',
            valign: 'middle'

        },
        {
            field: 'rejQty',
            title: 'QTY',
            align: 'center',
            valign: 'middle'
        },
        {
            field: 'rejRate',
            title: 'Percentage',
            align: 'center',
            valign: 'middle'
        },
        {
            field: 'affectedPart_1st',
            title: '1st Affected Part',
            align: 'center',
            valign: 'middle'
        },
        {
            field: 'affectedPart_2nd',
            title: '2nd Affected Part',
            align: 'center',
            valign: 'middle'
        },
        {
            field: 'affectedPart_3rd',
            title: '3rd Affected Part',
            align: 'center',
            valign: 'middle'
        }],
        onLoadSuccess: function () {  //加载成功时执行
            console.log("加载成功");
        },
        onLoadError: function () {  //加载失败时执行
            console.log("加载数据失败");
        }

    })

}
