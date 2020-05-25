
var apiGetLaserPara = window.globalConfig.rootDirectory + '/LASER_MachineSetting/GetData';


function generateChart() {
    
    //表单验证
    if ($("#txtDateFrom").val() == '') {
        alert('Date From can not be empty, please choose !')
        return false;
    }
    if ($("#txtDateTo").val() == '') {
        alert('Date To can not be empty, please choose !')
        return false;
    }
    //part, machine, 不能同时双选
    if ($("#ddlPartNo").val().length > 1 && $("#ddlMachineID").length > 1) {
        alert('Part No, Machine ID can not be both mutiple !')
        return false;
    }
    //machine选定一台的情况下, 不允许选择part
    if ($("#ddlMachineID").val().length == 1 && $("#ddlPartNo").val().length>1) {
        alert('Can not choose part no, if choose one machine!')
        return false;
    }

    //console.log(window.globalConfig.rootDirectory);


    $.ajax({
        type: "POST",
        dataType: "json",
        url: apiGetLaserPara,
        data: {
            "PartNo": $("#ddlPartNo").val(),
            "MachineID": $("#ddlMachineID").val(),
            "DateFrom": $("#txtDateFrom").val(),
            "DateTo": $("#txtDateTo").val()
        },
        success: function (data) {

            //console.log(data); //调试用的

            if (data == null || data.length==0) {
                alert('There is no record found, Please try again!');
                return false;
            }


            var partList = $("#ddlPartNo").val();
            var mcList = $("#ddlMachineID").val();



            var rateSeries = new Array();
            var frequencySeries = new Array();
            var powerSeries = new Array();
            var legendData = new Array();
            var axisXLabelData = new Array();//x轴坐标


            //只选择一台机器, 不选择part, 显示所有part曲线
            if (mcList.length == 1 && partList.length == 0) {

                //获取该机器所有做过的part
                var allParts =  new Array();
                for (var i = 0; i < data.length; i++) {
                    allParts.push(data[i].partNo);
                }
                allParts = Array.from(new Set(allParts));//去重


                for (var i = 0; i < allParts.length; i++) {

                    //获取单个part的所有集合
                    var partDatas = data.filter(function (x) {
                        return x.partNo == allParts[i];
                    });

                    var dataRate = new Array();
                    var dataFrequency = new Array();
                    var dataPower = new Array();
                    for (var x = 0; x < partDatas.length; x++) {

                        axisXLabelData.push(partDatas[x].sDateTimeDisplay);//添加所有时间点到X轴.

                        dataRate[x] ={
                            value: [partDatas[x].sDateTimeDisplay,partDatas[x].rate],
                            url: '/Webform/Reports/BuyOffReport.aspx?jobNumber=' + partDatas[x].jobNo//链接属性
                        };
                        dataFrequency[x] = {
                            value: [partDatas[x].sDateTimeDisplay,partDatas[x].frequency],
                            url: '/Webform/Reports/BuyOffReport.aspx?jobNumber=' + partDatas[x].jobNo//链接属性
                        };
                        dataPower[x] = {
                            value: [partDatas[x].sDateTimeDisplay,partDatas[x].power],
                            url: '/Webform/Reports/BuyOffReport.aspx?jobNumber=' + partDatas[x].jobNo//链接属性
                        };
                    }

                    rateSeries[i] = {
                        name: allParts[i],
                        type: 'line',
                        data: dataRate,
                        connectNulls: true//这是让h刻度缺失的点，数据正常连接不断开
                    };
                    frequencySeries[i] = {
                        name: allParts[i],
                        type: 'line',
                        data: dataFrequency,
                        connectNulls: true //这是让h刻度缺失的点，数据正常连接不断开
                    };
                    powerSeries[i] = {
                        name: allParts[i],
                        type: 'line',
                        data: dataPower,
                        connectNulls: true//这是让h刻度缺失的点，数据正常连接不断开
                    };

                    legendData[i] = allParts[i];
                }
            }

            //多part选择, 每个part 一条线
            else if (partList.length > 1) {

                for (var i = 0; i < partList.length; i++) {

                    //获取单个part的所有集合
                    var partDatas = data.filter(function (x) {
                        return x.partNo == partList[i];
                    });
                    
                    
                    var dataRate = new Array();
                    var dataFrequency = new Array();
                    var dataPower = new Array();
                    for (var x = 0; x < partDatas.length; x++) {
                      
                        axisXLabelData.push(partDatas[x].sDateTimeDisplay);//添加所有时间点到X轴.

                        dataRate[x] = {
                            value: [partDatas[x].sDateTimeDisplay, partDatas[x].rate],
                            url: '/Webform/Reports/BuyOffReport.aspx?jobNumber=' + partDatas[x].jobNo//链接属性
                        };
                        dataFrequency[x] = {
                            value: [partDatas[x].sDateTimeDisplay, partDatas[x].frequency],
                            url: '/Webform/Reports/BuyOffReport.aspx?jobNumber=' + partDatas[x].jobNo//链接属性
                        };
                        dataPower[x] = {
                            value: [partDatas[x].sDateTimeDisplay, partDatas[x].power],
                            url: '/Webform/Reports/BuyOffReport.aspx?jobNumber=' + partDatas[x].jobNo//链接属性
                        };
                    }


                    rateSeries[i] =  {
                        name: partList[i],
                        type: 'line',
                        data: dataRate,
                        connectNulls: true//这是让h刻度缺失的点，数据正常连接不断开
                    };
                    frequencySeries[i] =   {
                        name: partList[i],
                        type: 'line',
                        data: dataFrequency,
                        connectNulls: true //这是让h刻度缺失的点，数据正常连接不断开
                    };
                    powerSeries[i] =  {
                        name: partList[i],
                        type: 'line',
                        data: dataPower,
                        connectNulls: true//这是让h刻度缺失的点，数据正常连接不断开
                    };

                    legendData[i] = partList[i];
                }
            }

            //多machine选择, 每个machine一条线
            else if (mcList.length > 1) {
                for (var i = 0; i < mcList.length; i++) {

                    var mcDatas = data.filter(function (x) {
                        return x.machineID == mcList[i];
                    })

             
                    var dataRate = new Array();
                    var dataFrequency = new Array();
                    var dataPower = new Array();
                    for (var x = 0; x < mcDatas.length; x++) {
                        axisXLabelData.push(mcDatas[x].sDateTimeDisplay);//添加所有时间点到X轴.

                        dataRate[x] = {
                            value: [mcDatas[x].sDateTimeDisplay,mcDatas[x].rate],//y轴值
                            url: '/Webform/Reports/BuyOffReport.aspx?jobNumber=' + mcDatas[x].jobNo//链接属性
                        };
                        dataFrequency[x] = {
                            value: [mcDatas[x].sDateTimeDisplay, mcDatas[x].frequency],//y轴值
                            url: '/Webform/Reports/BuyOffReport.aspx?jobNumber=' + mcDatas[x].jobNo//链接属性
                        };
                        dataPower[x] = {
                            value: [mcDatas[x].sDateTimeDisplay, mcDatas[x].power],//y轴值
                            url: '/Webform/Reports/BuyOffReport.aspx?jobNumber=' + mcDatas[x].jobNo//链接属性
                        };
                    }


                    rateSeries[i] =  {
                        name: 'Machine ' + mcList[i],
                        type: 'line',
                        data: dataRate,
                        connectNulls: true//这是让h刻度缺失的点，数据正常连接不断开
                    };
                    frequencySeries[i] =  {
                        name: 'Machine ' + mcList[i],
                        type: 'line',
                        data: dataFrequency,
                        connectNulls: true//这是让h刻度缺失的点，数据正常连接不断开
                    };
                    powerSeries[i] =  {
                        name: 'Machine ' + mcList[i],
                        type: 'line',
                        data: dataPower,
                        connectNulls: true//这是让h刻度缺失的点，数据正常连接不断开
                    };

                    legendData[i] = 'Machine ' + mcList[i]
                }
            }

            //就一条线. 
            else {
                var dataRate = new Array();
                var dataFrequency = new Array();
                var dataPower = new Array();
                for (var x = 0; x < data.length; x++) {

                    axisXLabelData.push(data[x].sDateTimeDisplay);//添加所有时间点到X轴.

                    dataRate[x] = {
                        value: [data[x].sDateTimeDisplay,data[x].rate], //value [x轴坐标, y轴坐标]
                        url: '/Webform/Reports/BuyOffReport.aspx?jobNumber=' + data[x].jobNo//链接属性
                    };
                    dataFrequency[x] = {
                        value: [data[x].sDateTimeDisplay, data[x].frequency],
                        url: '/Webform/Reports/BuyOffReport.aspx?jobNumber=' + data[x].jobNo//链接属性
                    };
                    dataPower[x] = {
                        value: [data[x].sDateTimeDisplay, data[x].power],
                        url: '/Webform/Reports/BuyOffReport.aspx?jobNumber=' + data[x].jobNo//链接属性
                    };
                }

                rateSeries[0] =  {
                    name: 'Job',
                    type: 'line',
                    data: dataRate,
                    connectNulls: true//这是让h刻度缺失的点，数据正常连接不断开
                };
                frequencySeries[0] = {
                    name: 'Job',
                    type: 'line',
                    data: dataFrequency,
                    connectNulls: true//这是让h刻度缺失的点，数据正常连接不断开
                }
                powerSeries[0] = {
                    name: 'Job',
                    type: 'line',
                    data: dataPower,
                    connectNulls: true//这是让h刻度缺失的点，数据正常连接不断开
                }

                legendData[0] = 'Job';
            }


            //console.log(rateSeries);


            axisXLabelData = Array.from(new Set(axisXLabelData));
            axisXLabelData.sort(item=>item.sDateTimeDisplay);

            //console.log(axisXLabelData);


            var rateOption = {
                tooltip: {
                    show: true,
                    //trigger: 'item',

                    //自定义tooltip格式 
                    formatter: function (params) {
                        //console.log(params)

                        var dataModel = data.find(function(x) {
                            return x.sDateTimeDisplay == params.name;
                        })
                        //console.log(dataModel);

                        var res = '<div>';
                        res += '<p>' + params.marker + ' ' + dataModel.jobNo + ' </p>';
                        res += '<p>Part No: ' + dataModel.partNo + ' </p>';
                        res += '<p>Machine ID: ' + dataModel.machineID + ' </p>';
                        res += '<p>Rate: ' + dataModel.rate + 'mm/sec </p>';
                        res += '<p>Date: ' + dataModel.sDateTime + ' </p>';
                        res += "</div>";
                        return res;
                    }
                },
                title: {
                    text: 'Rate Trend Chart'
                },
                xAxis: {
                    type: 'category',
                    name:'Time',
                    boundaryGap: false,
                    data: axisXLabelData,//X轴数据 数组

                    axisLabel: {
                        rotate: -40   //X轴 倾斜角度
                    }
                },
                yAxis: {
                    name: 'mm/sec',
                    max: 3000,

                    nameTextStyle: {
                        fontWeight: 'bold'
                    }
                },

                legend: {
                    data: legendData
                },

                series: rateSeries
            };

            var frequencyOption = {
                tooltip: {
                    show: true,

                    //自定义tooltip格式 
                    formatter: function (params) {
                        //console.log(params)

                        let dataModel = data.find(function (x) {
                            return x.sDateTimeDisplay == params.name;
                        })
                        //console.log(dataModel);

                        var res = '<div>';
                        res += '<p>' + params.marker + ' ' + dataModel.jobNo + ' </p>';
                        res += '<p>Part No: ' + dataModel.partNo + ' </p>';
                        res += '<p>Machine ID: ' + dataModel.machineID + ' </p>';
                        res += '<p>Frequency: ' + dataModel.frequency + 'KHz </p>';
                        res += '<p>Date: ' + dataModel.sDateTimeDisplay + ' </p>';
                        res += "</div>";
                        return res;
                    }
                },
                title: {
                    text: 'Frequency Trend Chart'
                },
                legend: {
                    data: legendData
                },
                xAxis: {
                    type: 'category',
                    name: 'Time',
                    boundaryGap: false,
                    data: axisXLabelData,//X轴数据 数组

                    axisLabel: {
                        rotate: -40   //X轴 倾斜角度
                    }
                },
                yAxis: {
                    name: 'KHz',
                    max: 200,
                    nameTextStyle: {
                        fontWeight: 'bold'
                    }
                },
                series: frequencySeries
            };

            var powerOption = {
                tooltip: {
                    show: true,

                    //自定义tooltip格式 
                    formatter: function (params) {
                        //console.log(params)

                        let dataModel = data.find(function (x) {
                            return x.sDateTimeDisplay == params.name;
                        })
                        //console.log(dataModel);

                        var res = '<div>';
                        res += '<p>' + params.marker + ' ' + dataModel.jobNo + ' </p>';
                        res += '<p>Part No: ' + dataModel.partNo + ' </p>';
                        res += '<p>Machine ID: ' + dataModel.machineID + ' </p>';
                        res += '<p>Power: ' + dataModel.power + '% </p>';
                        res += '<p>Date: ' + dataModel.sDateTimeDisplay + ' </p>';
                        res += "</div>";
                        return res;
                    }
                },

                title: {
                    text: 'Power Trend Chart'
                },
                legend: {
                    data: legendData
                },
                xAxis: {
                    type: 'category',
                    name: 'Time',
                    boundaryGap: false,
                    data: axisXLabelData,//X轴数据 数组

                    axisLabel: {
                        rotate: -40   //X轴 倾斜角度
                    }
                },
                yAxis: {
                    name: '%',
                    max: 100,
                    nameTextStyle: {
                        fontWeight: 'bolder'
                    }
                },
                series: powerSeries
            };


            chartRate.clear();
            chartFrequency.clear();
            chartPower.clear();


            //使用刚指定的配置项和数据显示图表。
            chartRate.setOption(rateOption);
            chartFrequency.setOption(frequencyOption);
            chartPower.setOption(powerOption);

            //绑定点击曲线跳转地址
            chartRate.on('click', function (e) {
                window.open(e.data.url);
            });
            chartFrequency.on('click', function (e) {
                window.open(e.data.url);
            });
            chartPower.on('click', function (e) {
                window.open(e.data.url);
            });

            //chart自动缩放
            window.addEventListener("resize", function () {
                chartRate.resize();
                chartFrequency.resize();
                chartPower.resize();
            });
        },
        error: function (e) {
            alert("Get chart data error !");
        }
    });
}

