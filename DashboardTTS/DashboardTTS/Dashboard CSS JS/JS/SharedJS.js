
var apiGetPartList = window.globalConfig.rootDirectory + '/Common/GetPartList';
var apiGetModelList = window.globalConfig.rootDirectory + '/Common/GetModelList';
var apiGetJigList = window.globalConfig.rootDirectory + '/Common/GetJigNoList';
var apiGetLastReportDay = window.globalConfig.rootDirectory + '/Common/GetLastReportDay';
var apiGetMouldingDefectList = window.globalConfig.rootDirectory + '/Common/GetMouldingDefectList';
var apiGetUserIDList = window.globalConfig.rootDirectory + '/Common/GetUserIDList';



//设置机器列表下拉框
function setMachineDDL(selectControl, maxLength) {

    
    selectControl.empty();//清空


    selectControl.append("<option value=''></option>");//添加一个空选项

    for (var i = 1; i < maxLength +1; i++) {
        selectControl.append("<option value='" + i + "'>No." + i + "</option>");
    }

    selectControl.selectpicker("refresh");

}


//设置part No下拉框
function setPartNoDDL(selectControl, department, defaultValue) {


    $.ajax({
        type: "POST",
        dataType: "json",
        url: apiGetPartList,
        data: {
            "Department": department
        },
        success: function (data) {

           
            //调试用的
            //console.log(data);

            selectControl.empty();//清空
            selectControl.append($("<option value=''>All</option>"));//添加一个空选项

            for (var i = 0; i < data.length; i++) {
                selectControl.append($("<option value='" + data[i] + "'>" + data[i] + "</option>"));
            }

            
            selectControl.selectpicker("refresh");


            if (defaultValue != '') {
                selectControl.val(defaultValue);
                selectControl.selectpicker("refresh");
            }

        },
        error: function () {
            alert("Setting part no drop down list error !");
        }
    });

}

//设置jig no下拉框
function setJigNoDDL(selectControl, department) {
    $.ajax({
        type: "POST",
        dataType: "json",
        url: apiGetJigList,
        data: {
            "Department": department
        },
        success: function (data) {


            //调试用的
            //console.log(data);

            selectControl.empty();//清空
            selectControl.append($("<option value=''></option>"));//添加一个空选项

            for (var i = 0; i < data.length; i++) {
                selectControl.append($("<option value='" + data[i] + "'>" + data[i] + "</option>"));
            }


            selectControl.selectpicker("refresh");

        },
        error: function () {
            alert("Setting jig no drop down list error !");
        }
    });
}

//设置Model下拉框
function setModelDDL(selectControl, department) {


    $.ajax({
        type: "POST",
        dataType: "json",
        url: apiGetModelList,
        data: {
            "Department": department
        },
        success: function (data) {


            //调试用的
            //console.log(data);

            selectControl.empty();//清空
            selectControl.append($("<option value=''></option>"));//添加一个空选项

            for (var i = 0; i < data.length; i++) {
                selectControl.append($("<option value='" + data[i] + "'>" + data[i] + "</option>"));
            }


            selectControl.selectpicker("refresh");

        },
        error: function () {
            alert("Setting part no drop down list error !");
        }
    });
}

//设置moulding defect code下拉框
function setMouldingDefectCodeDDL(selectControl,defaultValue) {
    $.ajax({
        type: "POST",
        dataType: "json",
        url: apiGetMouldingDefectList,
        data:{},
        success: function (data) {

            
            selectControl.empty();//清空
            selectControl.append($("<option value=''></option>"));//添加一个空选项

            for (var i = 0; i < data.length; i++) {
                selectControl.append($("<option value='" + data[i] + "'>" + data[i] + "</option>"));
            }
            


            selectControl.selectpicker("refresh");

            if (defaultValue != '') {
                selectControl.val(defaultValue);
                selectControl.selectpicker('refresh');
            }
         

        },
        error: function () {
            alert("Setting part no drop down list error !");
        }
    });
}


//设置User ID下拉框
function setUserIDDDL(selectControl, department) {
    $.ajax({
        type: "POST",
        dataType: "json",
        url: apiGetUserIDList,
        data: {
            "Department": department
        },
        success: function (data) {


            //调试用的
            //console.log(data);

            selectControl.empty();//清空
            selectControl.append($("<option value=''>All</option>"));//添加一个空选项

            for (var i = 0; i < data.length; i++) {
                selectControl.append($("<option value='" + data[i] + "'>" + data[i] + "</option>"));
            }


            selectControl.selectpicker("refresh");


            //if (defaultValue != '') {
            //    selectControl.val(defaultValue);
            //    selectControl.selectpicker("refresh");
            //}

        },
        error: function () {
            alert("Setting part no drop down list error !");
        }
    });
}





function dateFormat(fmt, date) {
    let ret;
    const opt = {
        "y+": date.getFullYear().toString(),        // 年
        "M+": (date.getMonth() + 1).toString(),     // 月
        "d+": date.getDate().toString(),            // 日
        "H+": date.getHours().toString(),           // 时
        "m+": date.getMinutes().toString(),         // 分
        "s+": date.getSeconds().toString()          // 秒
        // 有其他格式化字符需求可以继续添加，必须转化成字符串
    };
    for (let k in opt) {
        ret = new RegExp("(" + k + ")").exec(fmt);
        if (ret) {
            fmt = fmt.replace(ret[1], (ret[1].length == 1) ? (opt[k]) : (opt[k].padStart(ret[1].length, "0")))
        };
    };
    return fmt;
}


function getWeekends(days) {
    

    switch (days) {
        case 1:
            days = 'Monday';
            break;
        case 2:
            days = 'Tuesday';
            break;
        case 3:
            days = 'Wednesday';
            break;
        case 4:
            days = 'Thursday';
            break;
        case 5:
            days = 'Friday';
            break;
        case 6:
            days = 'Saturday';
            break;
        case 0:
            days = 'Sunday';
            break;
    }

    return days;
}




function setAutoComplete(controlID, department) {

    $.ajax({
        type: "POST",
        dataType: "json",
        url: apiGetPartList,
        data: {
            "Department": department
        },
        success: function (data) {
            if (data == null || data == undefined || data.length == 0) {
                alert('Can not get part info!');
            }

            var partData = new Array();

            for (var i = 0; i < data.length; i++) {
                partData.push({ title: data[i] });
            }


            $(controlID).bigAutocomplete({
                //width: controlID.width(),
                data: partData,
                callback: function (data) {
                    //alert(data.title);
                }
            });

        },
        error: function () {
            alert("Setting part no drop down list error !");
        }
    });
}


function getLastReportDay() {

    var today = new Date();

    var lastDay = new Date();
   
    if (today.getDay() == 0) {
        lastDay.setDate(today.getDate() - 2);
    }else if (today.getDay() == 1) {
        lastDay.setDate(today.getDate() - 3);
    }
    else {
        lastDay.setDate(today.getDate() - 1);
    }




    return lastDay;
}



function getUrlParam(name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)"); //构造一个含有目标参数的正则表达式对象  
    var r = window.location.search.substr(1).match(reg);  //匹配目标参数   
    if (r != null) return unescape(r[2]); return null; //返回参数值  
}


