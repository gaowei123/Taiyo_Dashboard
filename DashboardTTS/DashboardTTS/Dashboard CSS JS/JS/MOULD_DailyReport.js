

var apiGetDailyData = window.globalConfig.rootDirectory + '/MOULD_DailyReport/GetData';



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

    setPartNoDDL($('#ddlPartNo'), 'Moulding','');
    setJigNoDDL($('#ddlJigNo'), 'Moulding');

    var datefrom = dateFormat('yyyy-MM-dd', new Date());
    $('#txtDateFrom').val(datefrom);
    $('#txtDateTo').val(datefrom);


    $('#tableDailyReport').bootstrapTable('destroy');
    $('#tableDailyReport').bootstrapTable({ // 对应table标签的id


        //call后台数据
        method: 'post',
        dataType: 'json',
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
        url: apiGetDailyData,

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
                JigNo: $("#ddlJigNo").val(),
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

       

        rowStyle: function (row, index) {

            //晚班的行颜色加深, 方便区别
            if (row.shift == "Night") {
                return {
                    css: { "background": "#D3D3D3", "overflow": "hidden", "text-overflow": "ellipsis", "white-space": "nowrap" }
                }
            } else {
                return {
                    css: { "overflow": "hidden", "text-overflow": "ellipsis", "white-space": "nowrap" }
                }
            }
        },



        columns: [
        {
            field: 'date',
            title: 'Date',
            align: 'center',
            valign: 'middle',
            sortable: true
        },
        {
            field: 'shift',
            title: 'Shift',
            align: 'center',
            valign: 'middle',
            sortable: true
        },
        {
            field: 'machineID',
            title: 'M/C No',
            align: 'center',
            valign: 'middle',
            sortable: true
        },
        {
            field: 'jigNo',
            title: 'Jig No',
            align: 'center',
            valign: 'middle'
        },
        {
            field: 'partNo',
            title: 'Part No',
            align: 'center',
            valign: 'middle',
            sortable: true
        },
        {
            field: 'output',
            title: 'Output',
            align: 'center',
            valign: 'middle',
            sortable: true
        },
        {
            field: 'passQty',
            title: 'Pass Qty',
            align: 'center',
            valign: 'middle',
            sortable: true
        },
        {
            field: 'rejQty',
            title: 'Rej Qty',
            align: 'center',
            valign: 'middle',
            sortable: true
        },
        {
            field: 'rejRate',
            title: 'Rej Rate',
            align: 'center',
            valign: 'middle',
            sortable: true
        },
        {
            field: 'whiteDot',
            title: 'White Dot',
            align: 'center',
            valign: 'middle',
            sortable: true
        },
        {
            field: 'scratches',
            title: 'Scratches',
            align: 'center',
            valign: 'middle',
            sortable: true
        },
        {
            field: 'dentedMark',
            title: 'Dented Mark',
            align: 'center',
            valign: 'middle',
            sortable: true
        },
        {
            field: 'shinningDot',
            title: 'Shinning Dot',
            align: 'center',
            valign: 'middle',
            sortable: true
        },
        {
            field: 'blackMark',
            title: 'Black Mark',
            align: 'center',
            valign: 'middle',
            sortable: true
        },
        {
            field: 'sinkMark',
            title: 'Sink Mark',
            align: 'center',
            valign: 'middle',
            sortable: true
        },
        {
            field: 'flowMark',
            title: 'Flow Mark',
            align: 'center',
            valign: 'middle',
            sortable: true
        },
        {
            field: 'highGate',
            title: 'High Gate',
            align: 'center',
            valign: 'middle',
            sortable: true
        },
        {
            field: 'silverSteak',
            title: 'Silver Steak',
            align: 'center',
            valign: 'middle',
            sortable: true
        },
        {
            field: 'blackDot',
            title: 'Black Dot',
            align: 'center',
            valign: 'middle',
            sortable: true
        },
        {
            field: 'oilStain',
            title: 'Oil Stain',
            align: 'center',
            valign: 'middle',
            sortable: true
        },
        {
            field: 'flowLine',
            title: 'Flow Line',
            align: 'center',
            valign: 'middle',
            sortable: true
        },
        {
            field: 'overCut',
            title: 'Over Cut',
            align: 'center',
            valign: 'middle',
            sortable: true
        },
        {
            field: 'crack',
            title: 'Crack',
            align: 'center',
            valign: 'middle',
            sortable: true
        },
        {
            field: 'shortMold',
            title: 'Short Mold',
            align: 'center',
            valign: 'middle',
            sortable: true
        },
        {
            field: 'stainMark',
            title: 'Stain Mark',
            align: 'center',
            valign: 'middle',
            sortable: true
        },
        {
            field: 'weldLine',
            title: 'Weld Line',
            align: 'center',
            valign: 'middle',
            sortable: true
        },
        {
            field: 'flashes',
            title: 'Flashes',
            align: 'center',
            valign: 'middle',
            sortable: true
        },
        {
            field: 'foreignMaterial',
            title: 'Foreign Material',
            align: 'center',
            valign: 'middle',
            sortable: true
        },
        {
            field: 'drag',
            title: 'Drag',
            align: 'center',
            valign: 'middle',
            sortable: true
        },
        {
            field: 'materialBleed',
            title: 'Material leed',
            align: 'center',
            valign: 'middle',
            sortable: true
        },
        {
            field: 'bent',
            title: 'Bent',
            align: 'center',
            valign: 'middle',
            sortable: true
        },
        {
            field: 'deform',
            title: 'Deform',
            align: 'center',
            valign: 'middle',
            sortable: true
        },
        {
            field: 'gasMark',
            title: 'Gas Mark',
            align: 'center',
            valign: 'middle',
            sortable: true
        },
        {
            field: 'opID',
            title: 'OPID',
            align: 'center',
            valign: 'middle',
            sortable: true
        }


        ],

        onLoadSuccess: function (data) {  //加载成功时执行
            //console.log(data);


            var whiteDot = 0;
            var scratches = 0;
            var dentedMark = 0;
            var shinningDot = 0;
            var blackMark = 0;
            var sinkMark = 0;
            var flowMark = 0;
            var highGate = 0;
            var silverSteak = 0;
            var BlackDot = 0;
            var oilStain = 0;
            var flowLine = 0;
            var overCut = 0;
            var crack = 0;
            var shortMold = 0;
            var stainMark = 0;
            var weldLine = 0;
            var flashes = 0;
            var foreignMaterial = 0;
            var drag = 0;
            var materialBleed = 0;
            var bent = 0;
            var deform = 0;
            var gasMark = 0;


            for (var i = 0; i < data.length; i++) {
                whiteDot += data[i].whiteDot;
                scratches += data[i].scratches;
                dentedMark += data[i].dentedMark;
                shinningDot += data[i].shinningDot;
                blackMark += data[i].blackMark;
                sinkMark += data[i].sinkMark;
                flowMark += data[i].flowMark;
                highGate += data[i].highGate;
                silverSteak += data[i].silverSteak;
                BlackDot += data[i].BlackDot;
                oilStain += data[i].oilStain;
                flowLine += data[i].flowLine;
                overCut += data[i].overCut;
                crack += data[i].crack;
                shortMold += data[i].shortMold;
                stainMark += data[i].stainMark;
                weldLine += data[i].weldLine;
                flashes += data[i].flashes;
                foreignMaterial += data[i].foreignMaterial;
                drag += data[i].drag;
                materialBleed += data[i].materialBleed;
                bent += data[i].bent;
                deform += data[i].deform;
                gasMark += data[i].gasMark;
            }

            if (whiteDot == 0) $('#tableDailyReport').bootstrapTable('hideColumn', 'whiteDot');
            if (scratches == 0) $('#tableDailyReport').bootstrapTable('hideColumn', 'scratches');
            if (dentedMark == 0) $('#tableDailyReport').bootstrapTable('hideColumn', 'dentedMark');
            if (shinningDot == 0) $('#tableDailyReport').bootstrapTable('hideColumn', 'shinningDot');
            if (blackMark == 0) $('#tableDailyReport').bootstrapTable('hideColumn', 'blackMark');
            if (sinkMark == 0) $('#tableDailyReport').bootstrapTable('hideColumn', 'sinkMark');
            if (flowMark == 0) $('#tableDailyReport').bootstrapTable('hideColumn', 'flowMark');
            if (highGate == 0) $('#tableDailyReport').bootstrapTable('hideColumn', 'highGate');
            if (silverSteak == 0) $('#tableDailyReport').bootstrapTable('hideColumn', 'silverSteak');
            if (BlackDot == 0) $('#tableDailyReport').bootstrapTable('hideColumn', 'BlackDot');
            if (oilStain == 0) $('#tableDailyReport').bootstrapTable('hideColumn', 'oilStain');
            if (flowLine == 0) $('#tableDailyReport').bootstrapTable('hideColumn', 'flowLine');
            if (overCut == 0) $('#tableDailyReport').bootstrapTable('hideColumn', 'overCut');
            if (crack == 0) $('#tableDailyReport').bootstrapTable('hideColumn', 'crack');
            if (shortMold == 0) $('#tableDailyReport').bootstrapTable('hideColumn', 'shortMold');
            if (stainMark == 0) $('#tableDailyReport').bootstrapTable('hideColumn', 'stainMark');
            if (weldLine == 0) $('#tableDailyReport').bootstrapTable('hideColumn', 'weldLine');
            if (flashes == 0) $('#tableDailyReport').bootstrapTable('hideColumn', 'flashes');
            if (foreignMaterial == 0) $('#tableDailyReport').bootstrapTable('hideColumn', 'foreignMaterial');
            if (drag == 0) $('#tableDailyReport').bootstrapTable('hideColumn', 'drag');
            if (materialBleed == 0) $('#tableDailyReport').bootstrapTable('hideColumn', 'materialBleed');
            if (bent == 0) $('#tableDailyReport').bootstrapTable('hideColumn', 'bent');
            if (deform == 0) $('#tableDailyReport').bootstrapTable('hideColumn', 'deform');
            if (gasMark == 0) $('#tableDailyReport').bootstrapTable('hideColumn', 'gasMark');


        },
        onLoadError: function () {  //加载失败时执行
            console.log("加载数据失败");
        }

    });

});


function search() {
    //重新加载必须将原本table清空
    $('#tableDailyReport').bootstrapTable('destroy');
    $('#tableDailyReport').bootstrapTable({ // 对应table标签的id


        //call后台数据
        method: 'post',
        dataType: 'json',
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
        url: apiGetDailyData,

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
                JigNo: $("#ddlJigNo").val(),
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


        rowStyle: function (row, index) {
          
            if (row.shift == "Night") {
                return {
                    css: { "background": "#D3D3D3", "overflow": "hidden", "text-overflow": "ellipsis", "white-space": "nowrap" }
                }
            } else {
                return {
                    css: { "overflow": "hidden", "text-overflow": "ellipsis", "white-space": "nowrap" }
                }
            }
        },

        columns: [

        {
             field: 'date',
             title: 'Date',
             align: 'center',
             valign: 'middle',
             sortable: true
         },
        {
            field: 'shift',
            title: 'Shift',
            align: 'center',
            valign: 'middle',
            sortable: true
        },
        {
            field: 'machineID',
            title: 'M/C No',
            align: 'center',
            valign: 'middle',
            sortable: true
        },
        {
            field: 'jigNo',
            title: 'Jig No',
            align: 'center',
            valign: 'middle'
        },
        {
            field: 'partNo',
            title: 'Part No',
            align: 'center',
            valign: 'middle',
            sortable: true
        },
        {
            field: 'output',
            title: 'Output',
            align: 'center',
            valign: 'middle',
            sortable: true
        },
        {
            field: 'passQty',
            title: 'Pass Qty',
            align: 'center',
            valign: 'middle',
            sortable: true
        },
        {
            field: 'rejQty',
            title: 'Rej Qty',
            align: 'center',
            valign: 'middle',
            sortable: true
        },
        {
            field: 'rejRate',
            title: 'Rej Rate',
            align: 'center',
            valign: 'middle',
            sortable: true
        },
        {
            field: 'whiteDot',
            title: 'White Dot',
            align: 'center',
            valign: 'middle',
            sortable: true
        },
        {
            field: 'scratches',
            title: 'Scratches',
            align: 'center',
            valign: 'middle',
            sortable: true
        },
        {
            field: 'dentedMark',
            title: 'Dented Mark',
            align: 'center',
            valign: 'middle',
            sortable: true
        },
        {
            field: 'shinningDot',
            title: 'Shinning Dot',
            align: 'center',
            valign: 'middle',
            sortable: true
        },
        {
            field: 'blackMark',
            title: 'Black Mark',
            align: 'center',
            valign: 'middle',
            sortable: true
        },
        {
            field: 'sinkMark',
            title: 'Sink Mark',
            align: 'center',
            valign: 'middle',
            sortable: true
        },
        {
            field: 'flowMark',
            title: 'Flow Mark',
            align: 'center',
            valign: 'middle',
            sortable: true
        },
        {
            field: 'highGate',
            title: 'High Gate',
            align: 'center',
            valign: 'middle',
            sortable: true
        },
        {
            field: 'silverSteak',
            title: 'Silver Steak',
            align: 'center',
            valign: 'middle',
            sortable: true
        },
        {
            field: 'blackDot',
            title: 'Black Dot',
            align: 'center',
            valign: 'middle',
            sortable: true
        },
        {
            field: 'oilStain',
            title: 'Oil Stain',
            align: 'center',
            valign: 'middle',
            sortable: true
        },
        {
            field: 'flowLine',
            title: 'Flow Line',
            align: 'center',
            valign: 'middle',
            sortable: true
        },
        {
            field: 'overCut',
            title: 'Over Cut',
            align: 'center',
            valign: 'middle',
            sortable: true
        },
        {
            field: 'crack',
            title: 'Crack',
            align: 'center',
            valign: 'middle',
            sortable: true
        },
        {
            field: 'shortMold',
            title: 'Short Mold',
            align: 'center',
            valign: 'middle',
            sortable: true
        },
        {
            field: 'stainMark',
            title: 'Stain Mark',
            align: 'center',
            valign: 'middle',
            sortable: true
        },
        {
            field: 'weldLine',
            title: 'Weld Line',
            align: 'center',
            valign: 'middle',
            sortable: true
        },
        {
            field: 'flashes',
            title: 'Flashes',
            align: 'center',
            valign: 'middle',
            sortable: true
        },
        {
            field: 'foreignMaterial',
            title: 'Foreign Material',
            align: 'center',
            valign: 'middle',
            sortable: true
        },
        {
            field: 'drag',
            title: 'Drag',
            align: 'center',
            valign: 'middle',
            sortable: true
        },
        {
            field: 'materialBleed',
            title: 'Material Bleed',
            align: 'center',
            valign: 'middle',
            sortable: true
        },
        {
            field: 'bent',
            title: 'Bent',
            align: 'center',
            valign: 'middle',
            sortable: true
        },
        {
            field: 'deform',
            title: 'Deform',
            align: 'center',
            valign: 'middle',
            sortable: true
        },
        {
            field: 'gasMark',
            title: 'Gas Mark',
            align: 'center',
            valign: 'middle',
            sortable: true
        },
        {
            field: 'opID',
            title: 'OPID',
            align: 'center',
            valign: 'middle',
            sortable: true
        }


        ],

        onLoadSuccess: function (data) {  //加载成功时执行
            //console.log(data);


            var whiteDot = 0;
            var scratches = 0;
            var dentedMark = 0;
            var shinningDot = 0;
            var blackMark = 0;
            var sinkMark = 0;
            var flowMark = 0;
            var highGate = 0;
            var silverSteak = 0;
            var BlackDot = 0;
            var oilStain = 0;
            var flowLine = 0;
            var overCut = 0;
            var crack = 0;
            var shortMold = 0;
            var stainMark = 0;
            var weldLine = 0;
            var flashes = 0;
            var foreignMaterial = 0;
            var drag = 0;
            var materialBleed = 0;
            var bent = 0;
            var deform = 0;
            var gasMark = 0;


            for (var i = 0; i < data.length; i++) {
                whiteDot += data[i].whiteDot;
                scratches += data[i].scratches;
                dentedMark += data[i].dentedMark;
                shinningDot += data[i].shinningDot;
                blackMark += data[i].blackMark;
                sinkMark += data[i].sinkMark;
                flowMark += data[i].flowMark;
                highGate += data[i].highGate;
                silverSteak += data[i].silverSteak;
                BlackDot += data[i].BlackDot;
                oilStain += data[i].oilStain;
                flowLine += data[i].flowLine;
                overCut += data[i].overCut;
                crack += data[i].crack;
                shortMold += data[i].shortMold;
                stainMark += data[i].stainMark;
                weldLine += data[i].weldLine;
                flashes += data[i].flashes;
                foreignMaterial += data[i].foreignMaterial;
                drag += data[i].drag;
                materialBleed += data[i].materialBleed;
                bent += data[i].bent;
                deform += data[i].deform;
                gasMark += data[i].gasMark;
            }

            if (whiteDot == 0) $('#tableDailyReport').bootstrapTable('hideColumn', 'whiteDot');
            if (scratches == 0) $('#tableDailyReport').bootstrapTable('hideColumn', 'scratches');
            if (dentedMark == 0) $('#tableDailyReport').bootstrapTable('hideColumn', 'dentedMark');
            if (shinningDot == 0) $('#tableDailyReport').bootstrapTable('hideColumn', 'shinningDot');
            if (blackMark == 0) $('#tableDailyReport').bootstrapTable('hideColumn', 'blackMark');
            if (sinkMark == 0) $('#tableDailyReport').bootstrapTable('hideColumn', 'sinkMark');
            if (flowMark == 0) $('#tableDailyReport').bootstrapTable('hideColumn', 'flowMark');
            if (highGate == 0) $('#tableDailyReport').bootstrapTable('hideColumn', 'highGate');
            if (silverSteak == 0) $('#tableDailyReport').bootstrapTable('hideColumn', 'silverSteak');
            if (BlackDot == 0) $('#tableDailyReport').bootstrapTable('hideColumn', 'BlackDot');
            if (oilStain == 0) $('#tableDailyReport').bootstrapTable('hideColumn', 'oilStain');
            if (flowLine == 0) $('#tableDailyReport').bootstrapTable('hideColumn', 'flowLine');
            if (overCut == 0) $('#tableDailyReport').bootstrapTable('hideColumn', 'overCut');
            if (crack == 0) $('#tableDailyReport').bootstrapTable('hideColumn', 'crack');
            if (shortMold == 0) $('#tableDailyReport').bootstrapTable('hideColumn', 'shortMold');
            if (stainMark == 0) $('#tableDailyReport').bootstrapTable('hideColumn', 'stainMark');
            if (weldLine == 0) $('#tableDailyReport').bootstrapTable('hideColumn', 'weldLine');
            if (flashes == 0) $('#tableDailyReport').bootstrapTable('hideColumn', 'flashes');
            if (foreignMaterial == 0) $('#tableDailyReport').bootstrapTable('hideColumn', 'foreignMaterial');
            if (drag == 0) $('#tableDailyReport').bootstrapTable('hideColumn', 'drag');
            if (materialBleed == 0) $('#tableDailyReport').bootstrapTable('hideColumn', 'materialBleed');
            if (bent == 0) $('#tableDailyReport').bootstrapTable('hideColumn', 'bent');
            if (deform == 0) $('#tableDailyReport').bootstrapTable('hideColumn', 'deform');
            if (gasMark == 0) $('#tableDailyReport').bootstrapTable('hideColumn', 'gasMark');
           

        },
        onLoadError: function () {  //加载失败时执行
            console.log("加载数据失败");
        }

    });

}


$(window).resize(function () {
    $('#tableDailyReport').bootstrapTable('resetView');
});
