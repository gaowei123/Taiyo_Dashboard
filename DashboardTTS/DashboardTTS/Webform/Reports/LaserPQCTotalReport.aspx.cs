using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace DashboardTTS.Webform.Reports
{
    public partial class LaserPQCTotalReport : System.Web.UI.Page
    {


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    this.lblUserHeader.Text = "PQC & Laser Total Report";


                    //默认显示前一天的.   周日, 周一显示 上周五的.
                    DateTime dLastDay = new DateTime();
                    if (DateTime.Now.DayOfWeek == DayOfWeek.Sunday)
                    {
                        dLastDay = DateTime.Now.AddDays(-2);
                    }
                    else if (DateTime.Now.DayOfWeek == DayOfWeek.Monday)
                    {
                        dLastDay = DateTime.Now.AddDays(-3);
                    }
                    else
                    {
                        dLastDay = DateTime.Now.AddDays(-1);
                    }

                    this.infDchFrom.CalendarLayout.SelectedDate = dLastDay;
                    this.infDchFrom.Value = dLastDay;
                    this.infDchTo.CalendarLayout.SelectedDate = dLastDay;
                    this.infDchTo.Value = dLastDay;


                    SetColorDDL();
                    SetTypeDDL();
                    SetSupplierDDL();
                    SetModelDDL(); 
                    

                    BtnGenerate_Click(new object(), new EventArgs());

                }
                catch (Exception ex)
                {
                    DBHelp.Reports.LogFile.Log("LaserPQCTotalReport", "Page_Load error : " + ex.ToString());
                    Common.CommFunctions.ShowWarning(lblResult, dgPQCLaserTotalReport, StaticRes.Global.ErrorLevel.Exception, ex.ToString());
                }
            }

            Common.CommFunctions.SetAutoComplete(this.Page, "#MainContent_txtPartNumber", "");
            this.dgPQCLaserTotalReport.ItemCommand += DgPQCLaserTotalReport_ItemCommand;
        }
        

        private void DgPQCLaserTotalReport_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "linkBuyoff")
                {
                    string jobNumber = e.Item.Cells[0].Text;
                    Response.Redirect("./BuyOffReport.aspx?jobNumber=" + jobNumber,false);
                }
            }
            catch (Exception ee)
            {
                DBHelp.Reports.LogFile.Log("LaserPQCTotalReport", "DgPQCLaserTotalReport_ItemCommand error : " + ee.ToString());
                Common.CommFunctions.ShowWarning(lblResult, dgPQCLaserTotalReport, StaticRes.Global.ErrorLevel.Exception, ee.ToString());
            }
        }

        protected void BtnGenerate_Click(object sender, EventArgs e)
        {
            try
            {

                DataTable dtLaserPQCTotalTable = new DataTable();

                //累加对应每个defectCode数量, 用于赋值total row.
                Dictionary<string, double> dicDefectRejCountForTotalRow = new Dictionary<string, double>();

                
                //searching paras
                string jobNo = this.txtJobNo.Text.Trim();
                string partNumber = this.txtPartNumber.Text.Trim();
                string model = this.ddlModel.SelectedValue;
                string color = this.ddlColor.SelectedValue;
                string type = this.ddlType.SelectedValue;
                string supplier = this.ddlSupplier.SelectedValue;
                string coating = this.ddlCoating.SelectedValue;
                string description = this.ddlDescription.SelectedValue;

                DateTime DateFrom = this.infDchFrom.CalendarLayout.SelectedDate.Date.AddHours(8);
                DateTime DateTo = this.infDchTo.CalendarLayout.SelectedDate.Date.AddDays(1).AddHours(8);

           



                #region partial table
                Common.Class.BLL.PQCQaViTracking_BLL viTrackingBLL = new Common.Class.BLL.PQCQaViTracking_BLL();
                Common.Class.BLL.PQCQaViDefectTracking_BLL defectTrackingBLL = new Common.Class.BLL.PQCQaViDefectTracking_BLL();
                Common.Class.BLL.PQCDefectSetting_BLL defectSettingBLL = new Common.Class.BLL.PQCDefectSetting_BLL();
                Common.Class.BLL.PaintingTempInfo paintBLL = new Common.Class.BLL.PaintingTempInfo();
                Common.Class.BLL.LMMSBUYOFFLIST_BLL laserBLL = new Common.Class.BLL.LMMSBUYOFFLIST_BLL();


                //主表 job material, total ok, ng  
                DataTable dtPQCMain = viTrackingBLL.getTotalReport(DateFrom, DateTo, partNumber, model, color, type, supplier,description,coating, jobNo);
                if (dtPQCMain == null || dtPQCMain.Rows.Count == 0)
                {
                    Common.CommFunctions.ShowWarning(lblResult, dgPQCLaserTotalReport, StaticRes.Global.ErrorLevel.Warning, "");
                    return;
                }
                

                //每种code的ng数量
                DataTable dtPQCDefect = defectTrackingBLL.getPQCDefect(DateFrom, DateTo, partNumber, model, color, type, supplier, description, coating,"");
                if (dtPQCDefect == null || dtPQCDefect.Rows.Count == 0)
                {
                    return;
                }
                
                //painting buyoff 信息
                DataTable dtPaintingInfo = paintBLL.GetList(DateFrom.AddDays(-7), DateTo,"","");
                if (dtPaintingInfo == null || dtPaintingInfo.Rows.Count == 0)
                {
                    
                }
                
             

                //获取所有defect code, 动态生成column. 
                DataTable dtAllDefectCode = defectSettingBLL.GetAllForPQCLaserTotalReport();
                if (dtAllDefectCode == null || dtAllDefectCode.Rows.Count == 0)
                {
                    DBHelp.Reports.LogFile.Log("LaserPQCTotalReport", "Warning : no defect code setting, during generate table structure ");
                    return;
                }

                DataRow[] drArrDefectCodeMoulding = dtAllDefectCode.Select(" defectDescription = 'Mould' ", "defectCodeID asc");
                DataRow[] drArrDefectCodePainting = dtAllDefectCode.Select(" defectDescription = 'Paint' ", "defectCodeID asc");
                DataRow[] drArrDefectCodeLaser = dtAllDefectCode.Select(" defectDescription = 'Laser' ", "defectCodeID asc");
                DataRow[] drArrDefectCodeOthers = dtAllDefectCode.Select(" defectDescription = 'Others' ", "defectCodeID asc");

                #endregion partial table



                dtLaserPQCTotalTable = dtPQCMain.Copy();


                #region report & dtLaserPQCTotalTable  structure   (dicDefectRejCountForTotalRow)

                #region PQC Main
                BoundColumn bdColumn;
                bdColumn = new BoundColumn();//part no
                bdColumn.DataField = "partNumber";
                bdColumn.HeaderText = "Part No";
                this.dgPQCLaserTotalReport.Columns.Add(bdColumn);

                bdColumn = new BoundColumn();//material no
                bdColumn.DataField = "materialNo";
                bdColumn.HeaderText = "Material No";
                this.dgPQCLaserTotalReport.Columns.Add(bdColumn);

                bdColumn = new BoundColumn();//Lot qty
                bdColumn.DataField = "lotQty";
                bdColumn.HeaderText = "Lot Qty";
                this.dgPQCLaserTotalReport.Columns.Add(bdColumn);

                bdColumn = new BoundColumn();//Total passed qty
                bdColumn.DataField = "passQty";
                bdColumn.HeaderText = "Pass";
                this.dgPQCLaserTotalReport.Columns.Add(bdColumn);

                bdColumn = new BoundColumn();//total rej
                bdColumn.DataField = "PQCTotalRej";
                bdColumn.HeaderText = "RejQty";
                this.dgPQCLaserTotalReport.Columns.Add(bdColumn);

                bdColumn = new BoundColumn();//Total Reject Rate%
                bdColumn.DataField = "totalRejRate";
                bdColumn.HeaderText = "Rej%";
                this.dgPQCLaserTotalReport.Columns.Add(bdColumn);

                bdColumn = new BoundColumn();//Supplier
                bdColumn.DataField = "Supplier";
                bdColumn.HeaderText = "Supplier";
                this.dgPQCLaserTotalReport.Columns.Add(bdColumn);

                
                #endregion


                #region Defect Code -- Moudling
                foreach (DataRow dr in drArrDefectCodeMoulding)
                {
                    bdColumn = new BoundColumn();
                    bdColumn.DataField = dr["DefectCode"].ToString();
                    bdColumn.HeaderText = dr["DefectCode"].ToString().Replace("MOULDING ", "(M)").Replace(" ", "<br/>");

                    //bdColumn.HeaderText = dr["DefectCode"].ToString().Replace("MOULDING ", "").Replace(" ", "<br/>") + "(M)";

                    dicDefectRejCountForTotalRow.Add(dr["DefectCode"].ToString(), 0);
                   

                    this.dgPQCLaserTotalReport.Columns.Add(bdColumn);


                    //report table structure
                    dtLaserPQCTotalTable.Columns.Add(dr["DefectCode"].ToString());

                }

                bdColumn = new BoundColumn();
                bdColumn.DataField = "MOULDINGRejQty";
                bdColumn.HeaderText = "(M)Rej</br>Qty";
                this.dgPQCLaserTotalReport.Columns.Add(bdColumn);

                bdColumn = new BoundColumn();
                bdColumn.DataField = "MOULDINGRejRate";
                bdColumn.HeaderText = "(M)Rej%";
                this.dgPQCLaserTotalReport.Columns.Add(bdColumn);

                //report table structure
                dtLaserPQCTotalTable.Columns.Add("MOULDINGRejQty");
                dtLaserPQCTotalTable.Columns.Add("MOULDINGRejRate");


                bdColumn = new BoundColumn();//MFG Date
                bdColumn.DataField = "MFGDate";
                bdColumn.HeaderText = "(M)MFG Date";
                this.dgPQCLaserTotalReport.Columns.Add(bdColumn);


                #endregion Defect Code -- Moudling


                #region Defect Code -- Painting
                foreach (DataRow dr in drArrDefectCodePainting)
                {
                    string temp = dr["DefectCode"].ToString();

                    bdColumn = new BoundColumn();
                    bdColumn.DataField = temp;

                    #region special defect code 
                    if (temp == ("PAINTING " + "Under coat uneven paint"))
                    {
                        bdColumn.HeaderText = "(P)U/C";
                    }
                    else if (temp == ("PAINTING " + "Particle for laser setup"))
                    {
                        bdColumn.HeaderText = "(P)Particle for</br>laser setup";
                    }
                    else
                    {
                        bdColumn.HeaderText = temp.Replace("PAINTING ", "(P)").Replace(" ", "<br/>");
                    }
                    #endregion


                    dicDefectRejCountForTotalRow.Add(temp, 0);

                    this.dgPQCLaserTotalReport.Columns.Add(bdColumn);

                    //report table structure
                    dtLaserPQCTotalTable.Columns.Add(temp);

                }

                //rej for painting
                bdColumn = new BoundColumn();
                bdColumn.DataField = "PAINTINGRejQty";
                bdColumn.HeaderText = "(P)Rej</br>Qty";
                this.dgPQCLaserTotalReport.Columns.Add(bdColumn);

                bdColumn = new BoundColumn();
                bdColumn.DataField = "PAINTINGRejRate";
                bdColumn.HeaderText = "(P)Rej%";
                this.dgPQCLaserTotalReport.Columns.Add(bdColumn);

                bdColumn = new BoundColumn();//set up rej qty
                bdColumn.DataField = "setupRejQty";
                bdColumn.HeaderText = "(P)Set up<br>Rej Qty";
                this.dgPQCLaserTotalReport.Columns.Add(bdColumn);

                bdColumn = new BoundColumn();//set up rej Rate
                bdColumn.DataField = "setupRejRate";
                bdColumn.HeaderText = "(P)Set up<br/>Rej%";
                this.dgPQCLaserTotalReport.Columns.Add(bdColumn);

                bdColumn = new BoundColumn();//qa test qty
                bdColumn.DataField = "qaTestQty";
                bdColumn.HeaderText = "(P)QA Reliability<br>test Qty";
                this.dgPQCLaserTotalReport.Columns.Add(bdColumn);

                bdColumn = new BoundColumn();//qa test qty Rate
                bdColumn.DataField = "qaTestQtyRate";
                bdColumn.HeaderText = "(P)Qty%";
                this.dgPQCLaserTotalReport.Columns.Add(bdColumn);

                bdColumn = new BoundColumn();//1st Coat
                bdColumn.DataField = "coat_1st";
                bdColumn.HeaderText = "(P)U/C<br/>1st Coat";
                this.dgPQCLaserTotalReport.Columns.Add(bdColumn);

                bdColumn = new BoundColumn();//1st painting machine
                bdColumn.DataField = "pMachine_1st";
                bdColumn.HeaderText = "(P)1st Machine";
                this.dgPQCLaserTotalReport.Columns.Add(bdColumn);

                bdColumn = new BoundColumn();//Painting date 1st
                bdColumn.DataField = "paintingDate_1st";
                bdColumn.HeaderText = "(P)1st Date";
                this.dgPQCLaserTotalReport.Columns.Add(bdColumn);

                bdColumn = new BoundColumn();//2nd Coat
                bdColumn.DataField = "coat_2nd";
                bdColumn.HeaderText = "(P)M/C<br/>2nd Coat";
                this.dgPQCLaserTotalReport.Columns.Add(bdColumn);

                bdColumn = new BoundColumn();//2st painting machine
                bdColumn.DataField = "pMachine_2nd";
                bdColumn.HeaderText = "(P)2nd Machine";
                this.dgPQCLaserTotalReport.Columns.Add(bdColumn);

                bdColumn = new BoundColumn();//Painting date 2nd
                bdColumn.DataField = "paintingDate_2nd";
                bdColumn.HeaderText = "(P)2nd Date";
                this.dgPQCLaserTotalReport.Columns.Add(bdColumn);

                bdColumn = new BoundColumn();//3rd Coat
                bdColumn.DataField = "coat_3rd";
                bdColumn.HeaderText = "(P)T/C<br/>3rd Coat";
                this.dgPQCLaserTotalReport.Columns.Add(bdColumn);

                bdColumn = new BoundColumn();//3st painting machine
                bdColumn.DataField = "pMachine_3rd";
                bdColumn.HeaderText = "(P)3rd Machine";
                this.dgPQCLaserTotalReport.Columns.Add(bdColumn);

                bdColumn = new BoundColumn();//Painting date 3rd
                bdColumn.DataField = "paintingDate_3rd";
                bdColumn.HeaderText = "(P)3rd Date";
                this.dgPQCLaserTotalReport.Columns.Add(bdColumn);

                //dictionary


                //report table structure



                #endregion Defect Code -- Painting


                #region Defect Code -- Laser
                foreach (DataRow dr in drArrDefectCodeLaser)
                {
                    string temp = dr["DefectCode"].ToString();
                    bdColumn = new BoundColumn();
                    bdColumn.DataField = temp;

                    #region special defect code 
                    if (temp == ("LASER " + "Graphic Shift check by PQC"))
                    {
                        bdColumn.HeaderText = "(L)G/S</br>by PQC";
                    }
                    else if (temp == ("LASER " + "Graphic Shift check by M/C"))
                    {
                        bdColumn.HeaderText = "(L)G/S</br>by M/C";
                    }
                    else
                    {
                        bdColumn.HeaderText = temp.Replace("LASER ", "(L)").Replace(" ", "<br/>");
                    }
                    #endregion



                    dicDefectRejCountForTotalRow.Add(temp, 0);
                  


                    this.dgPQCLaserTotalReport.Columns.Add(bdColumn);

                    //report table structure
                    dtLaserPQCTotalTable.Columns.Add(temp);

                }

                bdColumn = new BoundColumn();
                bdColumn.DataField = "LASERRejQty";
                bdColumn.HeaderText = "(L)Rej</br>Qty";
                this.dgPQCLaserTotalReport.Columns.Add(bdColumn);

                bdColumn = new BoundColumn();
                bdColumn.DataField = "LASERRejRate";
                bdColumn.HeaderText = "(L)Rej%";
                this.dgPQCLaserTotalReport.Columns.Add(bdColumn);

                bdColumn = new BoundColumn();//laser Machine
                bdColumn.DataField = "laserMachine";
                bdColumn.HeaderText = "(L)M/C";
                this.dgPQCLaserTotalReport.Columns.Add(bdColumn);

                bdColumn = new BoundColumn();//laser operator
                bdColumn.DataField = "laserOperator";
                bdColumn.HeaderText = "(L)Operator";
                this.dgPQCLaserTotalReport.Columns.Add(bdColumn);

                bdColumn = new BoundColumn();//Laser Date
                bdColumn.DataField = "laserDate";
                bdColumn.HeaderText = "(L)Date";
                this.dgPQCLaserTotalReport.Columns.Add(bdColumn);

                #endregion Defect Code -- Laser


                #region Defect Code -- Others
                foreach (DataRow dr in drArrDefectCodeOthers)
                {
                    bdColumn = new BoundColumn();
                    if (dr["DefectCode"].ToString() == "OTHERS White Dot in Material")
                    {
                        bdColumn.DataField = dr["DefectCode"].ToString();
                        bdColumn.HeaderText = "(O)White</br>Dot in</br>Materia";
                    }
                    else
                    {
                        bdColumn.DataField = dr["DefectCode"].ToString();
                        bdColumn.HeaderText = dr["DefectCode"].ToString().Replace("OTHERS ", "(O)").Replace(" ", "<br/>");
                    }


                    dicDefectRejCountForTotalRow.Add(dr["DefectCode"].ToString(), 0);
                   

                    //bdColumn.Visible = false;

                    this.dgPQCLaserTotalReport.Columns.Add(bdColumn);


                    //report table structure
                    dtLaserPQCTotalTable.Columns.Add(dr["DefectCode"].ToString());

                }
                bdColumn = new BoundColumn();
                bdColumn.DataField = "OTHERSRejQty";
                bdColumn.HeaderText = "(O)Rej<br/>Qty";
                this.dgPQCLaserTotalReport.Columns.Add(bdColumn);

                bdColumn = new BoundColumn();
                bdColumn.DataField = "OTHERSRejRate";
                bdColumn.HeaderText = "(O)Rej%";
                this.dgPQCLaserTotalReport.Columns.Add(bdColumn);

                #endregion Defect Code -- Others


                bdColumn = new BoundColumn();//pqc operator
                bdColumn.DataField = "PQCop";
                bdColumn.HeaderText = "Insp'd By";
                this.dgPQCLaserTotalReport.Columns.Add(bdColumn);




                dtLaserPQCTotalTable.Columns.Add("MFGDate");
                dtLaserPQCTotalTable.Columns.Add("PAINTINGRejQty");
                dtLaserPQCTotalTable.Columns.Add("PAINTINGRejRate");
                dtLaserPQCTotalTable.Columns.Add("setupRejQty");
                dtLaserPQCTotalTable.Columns.Add("setupRejRate");
                dtLaserPQCTotalTable.Columns.Add("qaTestQty");
                dtLaserPQCTotalTable.Columns.Add("qaTestQtyRate");
                dtLaserPQCTotalTable.Columns.Add("coat_1st");
                dtLaserPQCTotalTable.Columns.Add("pMachine_1st");
                dtLaserPQCTotalTable.Columns.Add("paintingDate_1st");
                dtLaserPQCTotalTable.Columns.Add("coat_2nd");
                dtLaserPQCTotalTable.Columns.Add("pMachine_2nd");
                dtLaserPQCTotalTable.Columns.Add("paintingDate_2nd");
                dtLaserPQCTotalTable.Columns.Add("coat_3rd");
                dtLaserPQCTotalTable.Columns.Add("pMachine_3rd");
                dtLaserPQCTotalTable.Columns.Add("paintingDate_3rd");
                dtLaserPQCTotalTable.Columns.Add("LASERRejQty");
                dtLaserPQCTotalTable.Columns.Add("LASERRejRate");
                dtLaserPQCTotalTable.Columns.Add("OTHERSRejQty");
                dtLaserPQCTotalTable.Columns.Add("OTHERSRejRate");


                //dictionary
                dicDefectRejCountForTotalRow.Add("MOULDINGRejRate", 0);
                dicDefectRejCountForTotalRow.Add("PAINTINGRejRate", 0);
                dicDefectRejCountForTotalRow.Add("LASERRejRate", 0);
                dicDefectRejCountForTotalRow.Add("OTHERSRejRate", 0);
             

                #endregion report & dtLaserPQCTotalTable  structure     (dicDefectRejCountForTotalRow)




                #region combine datatable

                #region dtLaserPQCTotalTable struction
                //b.LotNo
                //,a.JobID as jobNumber
                //,a.materialPartNo
                //,a.model
                //,c.partNumber
                // lot qty
                //,d.acceptQty
                //,d.rejectQty as PQCTotalRej
                //,'' as totalRejRate
                //PQCop

                //TTS defect code

                //VENDORS defect code

                //Painting defect code

                //dtLaserPQCTotalTable.Columns.Add("setupRejQty");
                //dtLaserPQCTotalTable.Columns.Add("setupRejRate");
                //dtLaserPQCTotalTable.Columns.Add("qaTestQty");
                //dtLaserPQCTotalTable.Columns.Add("qaTestQtyRate");
                //dtLaserPQCTotalTable.Columns.Add("coat_1st");
                //dtLaserPQCTotalTable.Columns.Add("pMachine_1st");
                //dtLaserPQCTotalTable.Columns.Add("coat_2nd");
                //dtLaserPQCTotalTable.Columns.Add("pMachine_2nd");
                //dtLaserPQCTotalTable.Columns.Add("coat_3rd");
                //dtLaserPQCTotalTable.Columns.Add("pMachine_3rd");

                //laser defect code

                //dtLaserPQCTotalTable.Columns.Add("laserMachine");
                //dtLaserPQCTotalTable.Columns.Add("laserOperator");
                //dtLaserPQCTotalTable.Columns.Add("MFGDate");
                //dtLaserPQCTotalTable.Columns.Add("paintingDate");
                //dtLaserPQCTotalTable.Columns.Add("UCDate");
                //dtLaserPQCTotalTable.Columns.Add("TCDate");
                //dtLaserPQCTotalTable.Columns.Add("laserDate");
                //dtLaserPQCTotalTable.Columns.Add("PQCop");

                #endregion dtLaserPQCTotalTable struction

                //foreach  dtLaserPQCTotalTable
                foreach (DataRow drReport in dtLaserPQCTotalTable.Rows)
                {
                    string sLotNo = drReport["LotNo"].ToString();
                    string sJobnumber = drReport["jobNumber"].ToString();
                    string sMaterialNo = drReport["materialNo"].ToString();


                    
                    #region combine dtPQCDefect
                    if (dtPQCDefect != null)
                    {
                        DataRow[] drArrTemp = dtPQCDefect.Select(" materialNo = '" + sMaterialNo + "' and jobnumber = '" + sJobnumber + "' ");
                        if (drArrTemp.Length != 0)
                        {
                            DataRow drPQCDefect = drArrTemp[0];

                            double laserNG = double.Parse(drPQCDefect["LASER Graphic Shift check by M/C"].ToString());
                            double pqcTotalRej = double.Parse(drReport["PQCTotalRej"].ToString());
                            double lotQty = double.Parse(drReport["lotQty"].ToString());

                            //sql中空着, 在代码中算上laserNG的数量赋值
                            drReport["PQCTotalRej"] = pqcTotalRej + laserNG;
                            drReport["totalRejRate"] = Math.Round((pqcTotalRej + laserNG) / lotQty * 100, 2).ToString() + "%";

                            

                            //foreach each defect code for the material no
                            foreach (DataRow drDefectCode in dtAllDefectCode.Rows)
                            {
                                string sDefectCode = drDefectCode["defectCode"].ToString();
                                
                              
                                string sRejCountTemp = drPQCDefect[sDefectCode].ToString();
                                double dRejCount = sRejCountTemp == "" ? 0 : double.Parse(sRejCountTemp);
                                drReport[sDefectCode] = dRejCount;


                                //add to dic for total row
                                double curRejCount = dicDefectRejCountForTotalRow[sDefectCode];
                                double toltalRejCount = curRejCount + dRejCount;
                                dicDefectRejCountForTotalRow[sDefectCode] = toltalRejCount;
                                
                            }

                            drReport["totalRejRate"] = drReport["totalRejRate"].ToString();

                            drReport["OTHERSRejQty"] = drPQCDefect["OTHERSRejQty"].ToString();
                            drReport["MOULDINGRejQty"] = drPQCDefect["MOULDINGRejQty"].ToString();
                            drReport["PAINTINGRejQty"] = drPQCDefect["PAINTINGRejQty"].ToString();
                            drReport["LASERRejQty"] = drPQCDefect["LASERRejQty"].ToString();

                            drReport["OTHERSRejRate"] = drPQCDefect["OTHERSRejRate"].ToString();
                            drReport["MOULDINGRejRate"] = drPQCDefect["MOULDINGRejRate"].ToString();
                            drReport["PAINTINGRejRate"] = drPQCDefect["PAINTINGRejRate"].ToString();
                            drReport["LASERRejRate"] = drPQCDefect["LASERRejRate"].ToString();

                        }
                    }
                    #endregion
                    

                    #region combine dtPaintingInfo
                    DataRow[] drArrTemp2 = dtPaintingInfo.Select(" LotNo = '" + sLotNo + "' and jobnumber = '" + sJobnumber + "' ");

                    if (drArrTemp2.Length > 0)
                    {
                        DataRow drPaintingInfo = drArrTemp2[0];

                        drReport["setupRejQty"] = drPaintingInfo["setupRejQty"].ToString();
                        drReport["setupRejRate"] = drPaintingInfo["setupRejRate"].ToString();
                        drReport["qaTestQty"] = drPaintingInfo["qaTestQty"].ToString();
                        drReport["qaTestQtyRate"] = drPaintingInfo["qaTestQtyRate"].ToString();
                        drReport["coat_1st"] = drPaintingInfo["coat_1st"].ToString();
                        drReport["pMachine_1st"] = drPaintingInfo["pMachine_1st"].ToString();
                        drReport["coat_2nd"] = drPaintingInfo["coat_2nd"].ToString();
                        drReport["pMachine_2nd"] = drPaintingInfo["pMachine_2nd"].ToString();
                        drReport["coat_3rd"] = drPaintingInfo["coat_3rd"].ToString();
                        drReport["pMachine_3rd"] = drPaintingInfo["pMachine_3rd"].ToString();
                        drReport["MFGDate"] = drPaintingInfo["MFGDate"].ToString();
                    }
                    #endregion
                    
                }

                #endregion combine datatable







                //order by model,lotno,materialno
                dtLaserPQCTotalTable = dtLaserPQCTotalTable.Select("", "model asc, lotno asc, materialno asc").CopyToDataTable();
                


                //add a column sn
                int sn = 1;
                dtLaserPQCTotalTable.Columns.Add("SN");
                foreach (DataRow dr in dtLaserPQCTotalTable.Rows)
                {

                    dr["SN"] = sn;
                    sn++;
                }


                

             
                #region total row
                
                //before add model subTotal row
                //generate the total row first  for  last summary row (moudling, painting, laser)
                double totalLotQty = 0;
                double totalPass = 0;
                double totalRejQty = 0;
                double totalPaintSetup = 0;
                double totalPaintQATest = 0;

                foreach (DataRow dr in dtLaserPQCTotalTable.Rows)
                {
                    if (dr["lotQty"].ToString() != "")      totalLotQty += double.Parse(dr["lotQty"].ToString());
                    if (dr["passQty"].ToString() != "")     totalPass   += double.Parse(dr["passQty"].ToString());
                    if (dr["PQCTotalRej"].ToString() != "") totalRejQty += double.Parse(dr["PQCTotalRej"].ToString());
                    if (dr["setupRejQty"].ToString() != "") totalPaintSetup += double.Parse(dr["setupRejQty"].ToString());
                    if (dr["qaTestQty"].ToString() != "")   totalPaintQATest += double.Parse(dr["qaTestQty"].ToString());
                }


                DataRow drTotal = dtLaserPQCTotalTable.NewRow();
                drTotal["model"] = "Total";
                drTotal["lotQty"] = totalLotQty;
                drTotal["passQty"] = totalPass;
                drTotal["PQCTotalRej"] = totalRejQty;
                drTotal["totalRejRate"] = Math.Round(totalRejQty / totalLotQty * 100, 2).ToString("#0.00") + "%";
                
                //total defect code rej count for total row
                double totalLaserRejCount = 0;
                double totalMouldRejCount = 0;
                double totalPaintRejCount = 0;
                double totalOthersRejCount = 0;

                foreach (DataRow drDefectCode in dtAllDefectCode.Rows)
                {
                    string defectCode = drDefectCode["defectCode"].ToString();
                    drTotal[defectCode] = dicDefectRejCountForTotalRow[defectCode];

                    if (defectCode.Contains("MOULDING"))
                    {
                        totalMouldRejCount += dicDefectRejCountForTotalRow[defectCode];
                    }
                    else if (defectCode.Contains("PAINTING"))
                    {
                        totalPaintRejCount += dicDefectRejCountForTotalRow[defectCode];
                    }
                    else if (defectCode.Contains("LASER"))
                    {
                        totalLaserRejCount += dicDefectRejCountForTotalRow[defectCode];
                    }
                    else if (defectCode.Contains("OTHERS"))
                    {
                        totalOthersRejCount += dicDefectRejCountForTotalRow[defectCode];
                    }
                }

               
                drTotal["MOULDINGRejRate"] = Math.Round(totalMouldRejCount / totalLotQty * 100, 2).ToString("#0.00") + "%";
                drTotal["PAINTINGRejRate"]= Math.Round(totalPaintRejCount / totalLotQty * 100, 2).ToString("#0.00") + "%";
                drTotal["LASERRejRate"]= Math.Round(totalLaserRejCount / totalLotQty * 100, 2).ToString("#0.00") + "%";
                drTotal["OTHERSRejRate"] = Math.Round(totalOthersRejCount / totalLotQty * 100, 2).ToString("#0.00") + "%";




                //split total row to others/tts/vendors/painting/paint set up/paint qa test/laser/overall  row
                DataRow drTotalOthers = dtLaserPQCTotalTable.NewRow();
                drTotalOthers["partNumber"] = "OTHERS >";
                drTotalOthers["PQCTotalRej"] = totalOthersRejCount;
                drTotalOthers["totalRejRate"] = Math.Round(totalOthersRejCount / totalLotQty * 100, 2).ToString("#0.00") + "%";
                drTotalOthers["OTHERSRejRate"] = drTotal["OTHERSRejRate"].ToString();
                foreach (DataRow drDefectCodeOthers in drArrDefectCodeOthers)
                {
                    string defectCode = drDefectCodeOthers["defectCode"].ToString();
                    drTotalOthers[defectCode] = drTotal[defectCode];
                }
                drTotalOthers["supplier"] = "TRGT: 0.05%";



                DataRow drTotalMoulding = dtLaserPQCTotalTable.NewRow();
                drTotalMoulding["partNumber"] = "MOULDING >";
                drTotalMoulding["PQCTotalRej"] = totalMouldRejCount;
                drTotalMoulding["totalRejRate"] = Math.Round(totalMouldRejCount / totalLotQty * 100, 2).ToString("#0.00") + "%";
                drTotalMoulding["MOULDINGRejRate"] = drTotal["MOULDINGRejRate"].ToString();
                foreach (DataRow drDefectCodeOthers in drArrDefectCodeMoulding)
                {
                    string defectCode = drDefectCodeOthers["defectCode"].ToString();
                    drTotalMoulding[defectCode] = drTotal[defectCode];
                }
                



                DataRow drTotalPainting = dtLaserPQCTotalTable.NewRow();
                drTotalPainting["partNumber"] = "PAINTING >";
                drTotalPainting["PQCTotalRej"] = totalPaintRejCount;
                drTotalPainting["totalRejRate"] = Math.Round(totalPaintRejCount / totalLotQty * 100, 2).ToString("#0.00") + "%";
                drTotalPainting["PAINTINGRejRate"] = drTotal["PAINTINGRejRate"].ToString();
                foreach (DataRow drDefectCodeOthers in drArrDefectCodePainting)
                {
                    string defectCode = drDefectCodeOthers["defectCode"].ToString();
                    drTotalPainting[defectCode] = drTotal[defectCode];
                }
                drTotalPainting["supplier"] = "TRGT: 1.00%";



                DataRow drTotalPaintingSetup = dtLaserPQCTotalTable.NewRow();
                drTotalPaintingSetup["partNumber"] = "PAINTING SETUP >";
                drTotalPaintingSetup["PQCTotalRej"] = totalPaintSetup;
                drTotalPaintingSetup["setupRejQty"] = totalPaintSetup;
                drTotalPaintingSetup["setupRejRate"] = Math.Round(totalPaintSetup / totalLotQty * 100, 2).ToString("#0.00") + "%";
                drTotalPaintingSetup["totalRejRate"] = Math.Round(totalPaintSetup / totalLotQty * 100, 2).ToString("#0.00") + "%";


                
                DataRow drTotalPaintingQATest = dtLaserPQCTotalTable.NewRow();
                drTotalPaintingQATest["partNumber"] = "QA PAINT TEST >";
                drTotalPaintingQATest["PQCTotalRej"] = totalPaintQATest;
                drTotalPaintingQATest["qaTestQty"] = totalPaintQATest;
                drTotalPaintingQATest["qaTestQtyRate"] = Math.Round(totalPaintQATest / totalLotQty * 100, 2).ToString("#0.00") + "%";
                drTotalPaintingQATest["totalRejRate"] = Math.Round(totalPaintQATest / totalLotQty * 100, 2).ToString("#0.00") + "%";



                DataRow drTotalLaser = dtLaserPQCTotalTable.NewRow();
                drTotalLaser["partNumber"] = "LASER >";
                drTotalLaser["PQCTotalRej"] = totalLaserRejCount;
                drTotalLaser["totalRejRate"] = Math.Round(totalLaserRejCount / totalLotQty * 100, 2).ToString("#0.00") + "%";
                drTotalLaser["LASERRejRate"] = drTotal["LASERRejRate"].ToString();
                foreach (DataRow drDefectCodeOthers in drArrDefectCodeLaser)
                {
                    string defectCode = drDefectCodeOthers["defectCode"].ToString();
                    drTotalLaser[defectCode] = drTotal[defectCode];
                }
                drTotalLaser["supplier"] = "TRGT: 0.30%";




                double tempAllRej = totalOthersRejCount + totalMouldRejCount + totalPaintRejCount + totalLaserRejCount;

                DataRow drTotalOverall = dtLaserPQCTotalTable.NewRow();
                drTotalOverall["partNumber"] = "OVERALL >";
                drTotalOverall["lotQty"] = totalLotQty;
                drTotalOverall["passQty"] = totalPass;
                drTotalOverall["PQCTotalRej"] = tempAllRej.ToString();// + "($"+ Math.Round(tempAllRej * 0.2,0).ToString() + ")" ;
                drTotalOverall["totalRejRate"] = drTotal["totalRejRate"].ToString();
                drTotalOverall["supplier"] = "TRGT: 3.35%";

                #endregion total row

                


                DataTable dtLaserPQCTotalTable_AddModelTotal = setModelTotalRow(dtLaserPQCTotalTable, dtPQCMain, dicDefectRejCountForTotalRow, dtAllDefectCode);





                dtLaserPQCTotalTable_AddModelTotal.Rows.Add(drTotalOthers.ItemArray);
                dtLaserPQCTotalTable_AddModelTotal.Rows.Add(drTotalMoulding.ItemArray);
                dtLaserPQCTotalTable_AddModelTotal.Rows.Add(drTotalPainting.ItemArray);
                dtLaserPQCTotalTable_AddModelTotal.Rows.Add(drTotalPaintingSetup.ItemArray);
                dtLaserPQCTotalTable_AddModelTotal.Rows.Add(drTotalPaintingQATest.ItemArray);
                dtLaserPQCTotalTable_AddModelTotal.Rows.Add(drTotalLaser.ItemArray);
                dtLaserPQCTotalTable_AddModelTotal.Rows.Add(drTotalOverall.ItemArray);




                display(dtLaserPQCTotalTable_AddModelTotal);



                showCost(dtLaserPQCTotalTable);


            }
            catch (Exception ex)
            {
                DBHelp.Reports.LogFile.Log("LaserPQCTotalReport",  "BtnGenerate_Click error : " + ex.ToString());
                Common.CommFunctions.ShowWarning(lblResult, dgPQCLaserTotalReport, StaticRes.Global.ErrorLevel.Exception, ex.ToString());
            }
        }

        
       
        
        

        DataTable setModelTotalRow(DataTable dtLaserPQCTotalTable, DataTable dtPQCMain, Dictionary<string,double> dicDefectRejCountForTotalRow, DataTable dtAllDefectCode)
        {

            //为了保证SN排序, 通过datatable在收集各个model, 按照model, lotno, materialno 再排序
            //foreach 收集的dtModelList 在dtLaserPQCTotalTable中 获取 同一model的行 并转成 dtModel
            //foreach dtModel 来计算该 model的total row
            //最后将各个dtModel merge起来


            DataTable dtLaserPQCTotalTable_AddModelTotal = dtLaserPQCTotalTable.Clone();

            DataTable dtModelList = getModelList(dtPQCMain);
            foreach (DataRow sModel in dtModelList.Rows)
            {
                #region foreach model
                DataRow[] drrArrModel = dtLaserPQCTotalTable.Select(" model = '" + sModel["model"].ToString() + "'");
                if (drrArrModel.Length <= 0)
                    continue;

                DataTable dtModel = Common.CommFunctions.DataRowToDataTable(drrArrModel);

                


                DataRow drModelSubTotal = dtModel.NewRow();

                double modelTotalLotQty = 0;
                double modelTotalPassQty = 0;
                double modelTotalRejQty = 0;
                double modelTotalMouldRejCount = 0;
                double modelTotalPaintRejCount = 0;
                double modelTotalLaserRejCount = 0;
                double modelTotalOthersRejCount = 0;
                //reset all value = 0
                resetDicByDefectCode(dicDefectRejCountForTotalRow, dtAllDefectCode);

                //foreach row   get count qty
                foreach (DataRow drModel in dtModel.Rows)
                {
                    modelTotalLotQty += double.Parse(drModel["lotQty"].ToString());
                    modelTotalPassQty += double.Parse(drModel["passQty"].ToString());
                    modelTotalRejQty += double.Parse(drModel["PQCTotalRej"].ToString());
                    

                    foreach (DataRow drDefectCode in dtAllDefectCode.Rows)
                    {
                        string sDefectCode = drDefectCode["defectCode"].ToString();

                        double dRejCount = drModel[sDefectCode].ToString() == "" ? 0 : double.Parse(drModel[sDefectCode].ToString());
                        double curRejCount = dicDefectRejCountForTotalRow[sDefectCode];

                        dicDefectRejCountForTotalRow[sDefectCode] = dRejCount + curRejCount;
                    }
                }
                //foreach row   get count qty


                drModelSubTotal["model"] = sModel["model"].ToString();
                drModelSubTotal["partNumber"] = "SUB TOTAL>>";
                drModelSubTotal["lotQty"] = modelTotalLotQty;
                drModelSubTotal["passQty"] = modelTotalPassQty;
                drModelSubTotal["PQCTotalRej"] = modelTotalRejQty;
                drModelSubTotal["totalRejRate"] = Math.Round(modelTotalRejQty / modelTotalLotQty * 100, 2).ToString("#0.00") + "%";

                //assign defect code for total row
                foreach (DataRow drDefectCode in dtAllDefectCode.Rows)
                {
                    string defectCode = drDefectCode["defectCode"].ToString();
                    drModelSubTotal[defectCode] = dicDefectRejCountForTotalRow[defectCode];


                    //sum total rej qty for moulding,painting.....
                    if (defectCode.Contains("MOULDING"))        modelTotalMouldRejCount += dicDefectRejCountForTotalRow[defectCode];
                    else if (defectCode.Contains("PAINTING"))   modelTotalPaintRejCount += dicDefectRejCountForTotalRow[defectCode];
                    else if (defectCode.Contains("LASER"))      modelTotalLaserRejCount += dicDefectRejCountForTotalRow[defectCode];
                    else if (defectCode.Contains("OTHERS"))     modelTotalOthersRejCount += dicDefectRejCountForTotalRow[defectCode];
                }


                drModelSubTotal["MOULDINGRejRate"] = Math.Round(modelTotalMouldRejCount / modelTotalLotQty * 100, 2).ToString("#0.00") + "%";
                drModelSubTotal["PAINTINGRejRate"] = Math.Round(modelTotalPaintRejCount / modelTotalLotQty * 100, 2).ToString("#0.00") + "%";
                drModelSubTotal["LASERRejRate"] = Math.Round(modelTotalLaserRejCount / modelTotalLotQty * 100, 2).ToString("#0.00") + "%";
                drModelSubTotal["OTHERSRejRate"] = Math.Round(modelTotalOthersRejCount / modelTotalLotQty * 100, 2).ToString("#0.00") + "%";

                dtModel.Rows.Add(drModelSubTotal);
                dtModel.Rows.Add(dtModel.NewRow());//空出一行
                #endregion

                dtLaserPQCTotalTable_AddModelTotal.Merge(dtModel);
            }


            return dtLaserPQCTotalTable_AddModelTotal;
        }
        

        void display(DataTable dt)
        {
            if (dt.Rows.Count == 0)
            {
                Common.CommFunctions.ShowWarning(lblResult, dgPQCLaserTotalReport, StaticRes.Global.ErrorLevel.Warning, "");
            }
            else
            {

                this.dgPQCLaserTotalReport.DataSource = dt.DefaultView;
                this.dgPQCLaserTotalReport.DataBind();


                foreach (DataGridItem item in dgPQCLaserTotalReport.Items)
                {
                    if (item.Cells[4].Text == "Total")
                    {
                        item.BackColor = System.Drawing.Color.Beige;
                        continue;
                    }
                    else if (item.Cells[4].Text == "SUB TOTAL>>")
                    {
                        item.BackColor = System.Drawing.Color.Beige;
                        item.Cells[2].Font.Bold = true;
                        item.Cells[4].Font.Bold = true;
                        continue;
                    }
                    else if (item.Cells[4].Text == "OTHERS >" || item.Cells[4].Text == "MOULDING >" ||
                             item.Cells[4].Text == "PAINTING >" || item.Cells[4].Text == "PAINTING SETUP >" ||
                             item.Cells[4].Text == "QA PAINT TEST >" || item.Cells[4].Text == "LASER >" ||
                             item.Cells[4].Text == "OVERALL >" )
                    {
                        //item.BackColor = System.Drawing.Color.Bisque;

                        for (int i = 4; i < 11; i++)
                        {
                            item.Cells[i].Font.Bold = true;
                            item.Cells[i].ForeColor = System.Drawing.Color.Black;
                            item.Cells[i].BackColor = System.Drawing.Color.Khaki;

                        }

                      
                        continue;
                    }


                    // defect code rejQty> 0  set background color
                    for (int i = 10; i < item.Cells.Count; i++)
                    {
                        string temp = item.Cells[i].Text;
                        if (Common.CommFunctions.isNumberic(temp) && temp != "0")
                        {
                            item.Cells[i].BackColor = System.Drawing.Color.Pink;
                        }
                    }
                }

                Common.CommFunctions.HideWarning(lblResult, dgPQCLaserTotalReport);
            }
        }

        void showCost(DataTable dt)
        {
            Common.Class.BLL.PQCBom_BLL bll = new Common.Class.BLL.PQCBom_BLL();
            DataTable dtBom = bll.GetList("");
            
            var result = from a in dt.AsEnumerable()
                      join b in dtBom.AsEnumerable()
                      on a.Field<string>("partnumber") equals b.Field<string>("partNumber")
                      select new
                      {
                          partNo = a.Field<string>("PartNumber"),
                          untiCost = b.Field<decimal>("UnitCost"),
                          rejQty = a.Field<string>("PQCTotalRej")
                      };



            decimal totalCost = 0;
            foreach (var item in result)
            {
                totalCost += item.untiCost * decimal.Parse(item.rejQty);
            }



            this.lbCost.Text = Math.Round(totalCost, 3).ToString("0.000") + "(SGD)";
        }




        void resetDicByDefectCode(Dictionary<string, double> dic, DataTable dt)
        {
            foreach (DataRow dr in dt.Rows)
            {
                string defectCode = dr["defectCode"].ToString();
                dic[defectCode] = 0;
            }
        }

        DataTable getModelList(DataTable dt)
        {
            DataTable dtModelList = new DataTable();
            dtModelList.Columns.Add("model");
            dtModelList.Columns.Add("lotno");
            dtModelList.Columns.Add("materialno");


            foreach (DataRow dr in dt.Rows)
            {
                string model = dr["model"].ToString();
                string lotNo = dr["lotno"].ToString();
                string materialNo = dr["materialno"].ToString();

                if (dtModelList.Select("model = '" + model + "' ").Length > 0)
                {
                    continue;
                }
                else
                {
                    DataRow drNew = dtModelList.NewRow();

                    drNew["model"] = model;
                    drNew["lotno"] = lotNo;
                    drNew["materialno"] = materialNo;

                    dtModelList.Rows.Add(drNew);
                }

            }

            dtModelList = dtModelList.Select("", "model asc, lotno asc, materialno asc").CopyToDataTable();

            return dtModelList;
        }

        private void SetColorDDL()
        {
            this.ddlColor.Items.Clear();

            ListItem Li = new ListItem();
            Li.Text = "All";
            Li.Value = "";
            this.ddlColor.Items.Add(Li);

            Li = new ListItem();
            Li.Text = "Black";
            Li.Value = "Black";
            this.ddlColor.Items.Add(Li);

            Li = new ListItem();
            Li.Text = "Silver";
            Li.Value = "Silver";
            this.ddlColor.Items.Add(Li);

            Li = new ListItem();
            Li.Text = "High gloss";
            Li.Value = "High gloss";
            this.ddlColor.Items.Add(Li);

            Li = new ListItem();
            Li.Text = "Mat black";
            Li.Value = "Mat black";
            this.ddlColor.Items.Add(Li);

            Li = new ListItem();
            Li.Text = "Texture line";
            Li.Value = "Texture line";
            this.ddlColor.Items.Add(Li);
        }

        private void SetTypeDDL()
        {
            this.ddlType.Items.Clear();


            ListItem Li = new ListItem();
            Li.Text = "All";
            Li.Value = "";
            this.ddlType.Items.Add(Li);

            Li = new ListItem();
            Li.Text = "LASER";
            Li.Value = "LASER";
            this.ddlType.Items.Add(Li);

            Li = new ListItem();
            Li.Text = "WIP";
            Li.Value = "WIP";
            this.ddlType.Items.Add(Li);

            //Li = new ListItem();
            //Li.Text = "SBW TKS784";
            //Li.Value = StaticRes.Global.ProductType.SBW_TKS784;
            //this.ddlType.Items.Add(Li);

            //Li = new ListItem();
            //Li.Text = "TMS TKS824";
            //Li.Value = StaticRes.Global.ProductType.TMS_TKS824;
            //this.ddlType.Items.Add(Li);

            //Li = new ListItem();
            //Li.Text = "TAC TKS833";
            //Li.Value = StaticRes.Global.ProductType.TAC_TKS833;
            //this.ddlType.Items.Add(Li);

            //Li = new ListItem();
            //Li.Text = "TRMI 452";
            //Li.Value = StaticRes.Global.ProductType.TRMI_452;
            //this.ddlType.Items.Add(Li);

            //Li = new ListItem();
            //Li.Text = "TRMI 595,656";
            //Li.Value = StaticRes.Global.ProductType.TRMI_595_656;
            //this.ddlType.Items.Add(Li);

            //Li = new ListItem();
            //Li.Text = "320B TKS830";
            //Li.Value = StaticRes.Global.ProductType.TKS830_320B;
            //this.ddlType.Items.Add(Li);


            //Li = new ListItem();
            //Li.Text = "Packers";
            //Li.Value = StaticRes.Global.ProductType.Packers;
            //this.ddlType.Items.Add(Li);

        }
        
        private void SetModelDDL()   
        {
            this.ddlModel.Items.Clear();


            Common.Class.BLL.PQCBom_BLL bll = new Common.Class.BLL.PQCBom_BLL();

            List<string> modelList = bll.GetModelNoList();
            if (modelList == null)
                return;

            ListItem Li = new ListItem();
            Li.Text = "All";
            Li.Value = "";

            this.ddlModel.Items.Add(Li);



            foreach (string model in modelList)
            {
                if (model != "")
                {
                    Li = new ListItem();

                    Li.Text = model;
                    Li.Value = model;

                    this.ddlModel.Items.Add(Li);
                }
            }
        }
        
        private void SetSupplierDDL()
        {
            this.ddlSupplier.Items.Clear();

            Common.Class.BLL.PQCBom_BLL bll = new Common.Class.BLL.PQCBom_BLL();

            List<string> supplierList = bll.GetSupplierList();
            if (supplierList == null)
                return;

            ListItem Li = new ListItem();
            Li.Text = "All";
            Li.Value = "";

            this.ddlSupplier.Items.Add(Li);



            foreach (string model in supplierList)
            {
                if (model != "")
                {
                    Li = new ListItem();

                    Li.Text = model;
                    Li.Value = model;

                    this.ddlSupplier.Items.Add(Li);
                }
            }

        }

        

    }
}