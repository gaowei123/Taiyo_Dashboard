
var apiGetHomeData = window.globalConfig.rootDirectory + '/Home/GetRefreshData';


$(function () {
   
    refresh();


    //2min 刷新一次页面
    setInterval("refresh()", 120000);
});




function refresh() {
    $.ajax({
        type: "POST",
        dataType: "json",
        url: apiGetHomeData,
        data: { },
        success: function (data) {

            console.log(data); //调试用的

            if (data == null || data.length == 0) {
                console.log('ajax call back receive null!');
                return false;
            }

            
            var mouldModel = data[0];
            var paintModel = data[1];
            var laserModel = data[2];
            var pqcOnlineModel = data[3];
            var pqcWIPModel = data[4];
            var pqcPackingModel = data[5];


            setMoulding(mouldModel);
            setPainting(paintModel);
            setLaser(laserModel);
            setOnline(pqcOnlineModel);
            setWIP(pqcWIPModel);
            setPacking(pqcPackingModel);

            setAssy();
        },
        error: function (e) {
            console.log("refresh catch error!");
        }
    });


    setDate();
}



function setMoulding(mouldModel) {
    var divSectionHtml = '<a href="' + window.globalConfig.rootDirectory + '/Webform/Moulding/MachineRealStatus.aspx">' + mouldModel.department + '</a>';
    var divOutputHtml = '<a href="' + window.globalConfig.rootDirectory + '/Webform/Moulding/MouldingProductionReport.aspx">' + mouldModel.output + '</a>';
    var divInventoryHtml = mouldModel.inventory;

    $('#divMouldingSection').html(divSectionHtml);
    $('#divMouldingOutput').html(divOutputHtml);
    $('#divMouldingInventory').html(divInventoryHtml);


    $('#divMouldingMachineStatus').html('');
    for (var i = 0; i < mouldModel.machineStatusList.length; i++) {

        var machineModel = mouldModel.machineStatusList[i];

        var strHtml = '<div class="divController">\
                           <table class="mcStatusTable">\
                               <tr><td class="mcStatusTD"><label>'+ machineModel.machineID + '</label></td></tr>\
                               <tr><td class="mcStatusTD"><label class="lbStatus" style="background-color:'+ machineModel.statusColor + ';">' + machineModel.status + '</label></td></tr>\
                           </table>\
                       </div>';

        $('#divMouldingMachineStatus').append(strHtml);
    }
}

function setPainting(paintModel) {
    var divSectionHtml = paintModel.department;
    var divOutputHtml = '<a href="' + window.globalConfig.rootDirectory + '/Webform/Painting/PaintingDeliveryOperatingHis.aspx">' + paintModel.output + '</a>';
    var divInventoryHtml = paintModel.inventory;

    $('#divPaintingSection').html(divSectionHtml);
    $('#divPaintingOutput').html(divOutputHtml);
    $('#divPaintingInventory').html(divInventoryHtml);


    $('#divPaintingMachineStatus').html('');
    for (var i = 1; i < 10; i++) {

        var machineID = 'Machine ' + i;
        var status = 'Under develop';
        var color = 'Gray';

        var strHtml = '<div class="divController">\
                           <table class="mcStatusTable">\
                               <tr><td class="mcStatusTD"><label>' + machineID + '</label></td></tr>\
                               <tr><td class="mcStatusTD"><label class="lbStatus" style="background-color:' + color + ';">' + status + '</label></td></tr>\
                           </table>\
                       </div>';

        $('#divPaintingMachineStatus').append(strHtml);
    }
}

function setLaser(laserModel) {
    var divSectionHtml = '<a href="' + window.globalConfig.rootDirectory + '/Webform/Laser/MachineStatus.aspx">' + laserModel.department + '</a>';
    var divOutputHtml = '<a href="' + window.globalConfig.rootDirectory + '/Webform/Laser/ProductivityDetail.aspx">' + laserModel.output + '</a>';
    var divInventoryHtml = '<a href="' + window.globalConfig.rootDirectory + '/Webform/Laser/InventoryReport.aspx">' + laserModel.inventory + '</a>';

    $('#divLaserSection').html(divSectionHtml);
    $('#divLaserOutput').html(divOutputHtml);
    $('#divLaserInventory').html(divInventoryHtml);

    $('#divLaserMachineStatus').html('');//清空div
    for (var i = 0; i < laserModel.machineStatusList.length; i++) {

        var machineModel = laserModel.machineStatusList[i];
        var strHtml = '<div class="divController">\
                           <table class="mcStatusTable">\
                               <tr><td class="mcStatusTD"><label>'+ machineModel.machineID + '</label></td></tr>\
                               <tr><td class="mcStatusTD"><label class="lbStatus" style="background-color:'+ machineModel.statusColor + ';">' + machineModel.status + '</label></td></tr>\
                           </table>\
                       </div>';

        $('#divLaserMachineStatus').append(strHtml);
    }
}

function setOnline(onlineModel) {
    var divSectionHtml = '<a href="' + window.globalConfig.rootDirectory + '/Webform/PQC/PQCRealTime.aspx?type=Online">' + onlineModel.department + '</a>';
    var divOutputHtml = '<a href="' + window.globalConfig.rootDirectory + '/Webform/PQC/PQCDailyReport.aspx">' + onlineModel.output + '</a>';
    var divInventoryHtml = onlineModel.inventory

    $('#divOnlineSection').html(divSectionHtml);
    $('#divOnlineOutput').html(divOutputHtml);
    $('#divOnlineInventory').html(divInventoryHtml);


    $('#divOnlineMachineStatus').html('');
    for (var i = 0; i < onlineModel.machineStatusList.length; i++) {

        var machineModel = onlineModel.machineStatusList[i];
        var strHtml = '<div class="divController">\
                           <table class="mcStatusTable">\
                               <tr><td class="mcStatusTD"><label>'+ machineModel.machineID + '</label></td></tr>\
                               <tr><td class="mcStatusTD"><label class="lbStatus" style="background-color:'+ machineModel.statusColor + ';">' + machineModel.status + '</label></td></tr>\
                           </table>\
                       </div>';


        $('#divOnlineMachineStatus').append(strHtml);
    }
}

function setWIP(wipModel) {
    var divSectionHtml = '<a href="' + window.globalConfig.rootDirectory + '/Webform/PQC/PQCRealTime.aspx?type=WIP">' + wipModel.department + '</a>';
    var divOutputHtml = '<a href="' + window.globalConfig.rootDirectory + '/Webform/PQC/PQCDailyReport.aspx">' + wipModel.output + '</a>';
    var divInventoryHtml = '<a href="' + window.globalConfig.rootDirectory + '/Webform/PQC/PQCInventoryReport.aspx">' + wipModel.inventory + '</a>';

    $('#divWIPSection').html(divSectionHtml);
    $('#divWIPOutput').html(divOutputHtml);
    $('#divWIPInventory').html(divInventoryHtml);


    $('#divWIPMachineStatus').html('');
    for (var i = 0; i < wipModel.machineStatusList.length; i++) {

        var machineModel = wipModel.machineStatusList[i];
        var strHtml = '<div class="divController">\
                           <table class="mcStatusTable">\
                               <tr><td class="mcStatusTD"><label>'+ machineModel.machineID + '</label></td></tr>\
                               <tr><td class="mcStatusTD"><label class="lbStatus" style="background-color:'+ machineModel.statusColor + ';">' + machineModel.status + '</label></td></tr>\
                           </table>\
                       </div>';

        $('#divWIPMachineStatus').append(strHtml);
    }
}

function setPacking(packingModel) {
    var divSectionHtml = '<a href="' + window.globalConfig.rootDirectory + '/Webform/PQC/PQCRealTime.aspx?type=Packing">' + packingModel.department + '</a>';
    var divOutputHtml = '<a href="' + window.globalConfig.rootDirectory + '/Webform/PQC/PQCDailyReport.aspx">' + packingModel.output + '</a>';
    var divInventoryHtml = packingModel.inventory

    $('#divPackSection').html(divSectionHtml);
    $('#divPackOutput').html(divOutputHtml);
    $('#divPackInventory').html(divInventoryHtml);


    //清空div
    $('#divPackMachineStatus').html('');
    for (var i = 0; i < packingModel.machineStatusList.length; i++) {

        var machineModel = packingModel.machineStatusList[i];
        var strHtml = '<div class="divController">\
                           <table class="mcStatusTable">\
                               <tr><td class="mcStatusTD"><label>'+ machineModel.machineID + '</label></td></tr>\
                               <tr><td class="mcStatusTD"><label class="lbStatus" style="background-color:'+ machineModel.statusColor + ';">' + machineModel.status + '</label></td></tr>\
                           </table>\
                       </div>';

        $('#divPackMachineStatus').append(strHtml);
    }
}

function setAssy() {
    var divSectionHtml = '';
    var divOutputHtml = 'NA';
    var divInventoryHtml = 'NA';

    $('#divAssySection').html(divSectionHtml);
    $('#divAssyOutput').html(divOutputHtml);
    $('#divAssyInventory').html(divInventoryHtml);

    $('#divAssyMachineStatus').html('');
    for (var i = 1; i < 6; i++) {

        var machineID = 'Line ' + i;
        var status = 'Under develop';
        var color = 'Gray';


        var strHtml = '<div class="divController">\
                           <table class="mcStatusTable">\
                               <tr><td class="mcStatusTD"><label>' + machineID + '</label></td></tr>\
                               <tr><td class="mcStatusTD"><label class="lbStatus" style="background-color:' + color + ';">' + status + '</label></td></tr>\
                           </table>\
                       </div>';

        $('#divAssyMachineStatus').append(strHtml);

    }
}




function setDate() {
    var today = new Date();
    var sDay = dateFormat("dd/MM/yyyy", today)
    var sWeekend = getWeekends(today.getDay());

    var str = sDay + ' ' + sWeekend;

    $('#lbDate').text(str);
}