using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace DashboardTTS.Webform.PQC
{
    public partial class PQCBezelPanelReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {

                    string reportType = Request.QueryString["Description"].ToString();
                    this.lblUserHeader.Text = "PQC " + reportType + " Report";


                    this.lbBezelPanelName.Text = reportType + " No:";


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


                    this.txtDateFrom.Text = dLastDay.ToString("yyyy-MM-dd");
                    this.txtDateTo.Text = dLastDay.ToString("yyyy-MM-dd");




                    setNumber(reportType);

                 
                    BtnGenerate_Click(new object(), new EventArgs());

                }
                catch (Exception ee)
                {
                    DBHelp.Reports.LogFile.Log("PQCBezelPanelReport", "Page_Load error : " + ee.ToString());
                }
            }
        }


        protected void BtnGenerate_Click(object sender, EventArgs e)
        {
            try
            {
                //搜索条件
                DateTime? dDateFrom = new DateTime();
                DateTime? dDateTo = new DateTime();
                DateTime? dMFGDate = new DateTime();
                DateTime? dPaintDate = new DateTime();

                try { dDateFrom = DateTime.Parse(txtDateFrom.Text); } catch { dDateFrom = null; }
                try { dDateTo = DateTime.Parse(txtDateTo.Text); } catch { dDateTo = null; }
                try { dPaintDate = DateTime.Parse(txtPaintDate.Text); } catch { dPaintDate = null; }
                try { dMFGDate = DateTime.Parse(txtMFGDate.Text); } catch { dMFGDate = null; }

                if (dDateFrom == null || dDateTo == null)
                {
                    Common.CommFunctions.ShowMessage(this.Page, "Please choose date from - to!");
                    return;
                }

                dDateFrom = dDateFrom.Value.AddHours(8);
                dDateTo = dDateTo.Value.AddDays(1).AddHours(8);



                string sType = "";
                string sDescription = Request.QueryString["Description"].ToString();
                string sNumber = this.ddlNumber.SelectedValue;

                string sPIC = this.txtPIC.Text.Trim();






                Common.Class.BLL.PQCQaViTracking_BLL trackingBLL = new Common.Class.BLL.PQCQaViTracking_BLL();
                DataTable dtMain = trackingBLL.getBezelPanelReport(dDateFrom.Value, dDateTo.Value, sType, sDescription, sNumber, dPaintDate, sPIC, dMFGDate);
                if (dtMain == null || dtMain.Rows.Count == 0)
                {
                    Common.CommFunctions.ShowMessage(this.Page, "No any data found,  Please change searching date!");
                    return;
                }



                Common.Class.BLL.PQCQaViDefectTracking_BLL defectBLL = new Common.Class.BLL.PQCQaViDefectTracking_BLL();
                DataTable dtPQCDefect = defectBLL.getPQCDefectForBezelPanelReport(dDateFrom.Value, dDateTo.Value, sType, sDescription, sNumber);
                if (dtPQCDefect == null || dtPQCDefect.Rows.Count == 0)
                {
                    Common.CommFunctions.ShowMessage(this.Page, "No any data found,  Please change searching date!");
                    return;
                }

                Common.Class.BLL.PQCDefectSetting_BLL defectSettingBLL = new Common.Class.BLL.PQCDefectSetting_BLL();
                DataTable dtAllDefectCode = defectSettingBLL.GetAllForPQCLaserTotalReport();



                #region dtOutput struction
                DataTable dtOutput = new DataTable();
                dtOutput.Columns.Add("Part No");
                dtOutput.Columns.Add("Lot No");
                dtOutput.Columns.Add("Job No");
                dtOutput.Columns.Add("Lot Qty");
                //dtOutput.Columns.Add("Total Checked Qty");
                dtOutput.Columns.Add("MFG Date");
                dtOutput.Columns.Add("Painting Under Coat Date");
                dtOutput.Columns.Add("Painting Under Coat M/C No");
                dtOutput.Columns.Add("Painting Under Coat M/C running time");
                dtOutput.Columns.Add("Painting Under Coat oven time");
                dtOutput.Columns.Add("Paint Lot Under Coat");
                dtOutput.Columns.Add("Thinners Lot Under Coat");
                dtOutput.Columns.Add("Painting Thickness Under Coat");
                dtOutput.Columns.Add("Painting PIC Under Coat");
                dtOutput.Columns.Add("Painting Middle Coat Date");
                dtOutput.Columns.Add("Painting Middle Coat M/C No");
                dtOutput.Columns.Add("Painting Middle Coat M/C running time");
                dtOutput.Columns.Add("Painting Middle Coat oven time");
                dtOutput.Columns.Add("Paint Lot Middle Coat");
                dtOutput.Columns.Add("Thinners Lot Middle Coat");
                dtOutput.Columns.Add("Painting Thickness Middle Coat");
                dtOutput.Columns.Add("Painting PIC Middle Coat");
                dtOutput.Columns.Add("Painting Top Coat Date");
                dtOutput.Columns.Add("Painting Top Coat M/C No");
                dtOutput.Columns.Add("Painting Top Coat M/C running time");
                dtOutput.Columns.Add("Painting Top Coat oven time");
                dtOutput.Columns.Add("Paint Lot Top Coat");
                dtOutput.Columns.Add("Thinners Lot Top Coat");
                dtOutput.Columns.Add("Painting Thickness Top Coat");
                dtOutput.Columns.Add("Painting PIC Top Coat");

                dtOutput.Columns.Add("Temperature Front");
                dtOutput.Columns.Add("Temperature Rear");
                dtOutput.Columns.Add("Humidity Front");
                dtOutput.Columns.Add("Humidity Rear");


                dtOutput.Columns.Add("Laser & Checking Date");
                dtOutput.Columns.Add("Laser M/C No");
                dtOutput.Columns.Add("Laser PIC");
                dtOutput.Columns.Add("PQC PIC");


                DataRow[] drArrMoulding = dtAllDefectCode.Select("defectDescription = 'Mould'", "defectCodeID asc");
                DataRow[] drArrPainting = dtAllDefectCode.Select("defectDescription = 'Paint'", "defectCodeID asc");
                DataRow[] drArrLaser = dtAllDefectCode.Select("defectDescription = 'Laser'", "defectCodeID asc");
                DataRow[] drArrOthers = dtAllDefectCode.Select("defectDescription = 'Others'", "defectCodeID asc");


                dtOutput.Columns.Add("Moulding Defect");
                foreach (DataRow dr in drArrMoulding)
                {
                    DataColumn dcNew = new DataColumn();
                    dcNew.ColumnName = dr["defectCodeID"].ToString();
                    dcNew.Caption = dr["defectCodeSource"].ToString();
                    dtOutput.Columns.Add(dcNew);
                }

                dtOutput.Columns.Add("Painting Defect");
                foreach (DataRow dr in drArrPainting)
                {
                    DataColumn dcNew = new DataColumn();
                    dcNew.ColumnName = dr["defectCodeID"].ToString();
                    dcNew.Caption = dr["defectCodeSource"].ToString();
                    dtOutput.Columns.Add(dcNew);
                }

                dtOutput.Columns.Add("Laser Defect");
                foreach (DataRow dr in drArrLaser)
                {
                    DataColumn dcNew = new DataColumn();
                    dcNew.ColumnName = dr["defectCodeID"].ToString();
                    dcNew.Caption = dr["defectCodeSource"].ToString();
                    dtOutput.Columns.Add(dcNew);
                }

                dtOutput.Columns.Add("Others Defect");
                foreach (DataRow dr in drArrOthers)
                {
                    DataColumn dcNew = new DataColumn();
                    dcNew.ColumnName = dr["defectCodeID"].ToString();
                    dcNew.Caption = dr["defectCodeSource"].ToString();
                    dtOutput.Columns.Add(dcNew);
                }

                dtOutput.Columns.Add("Total REJ QTY");
                dtOutput.Columns.Add("Total REJ AMT");
                dtOutput.Columns.Add("Total Mould REJ%");

                dtOutput.Columns.Add("Painting Particle REJ%");
                dtOutput.Columns.Add("Painting Many Particle REJ%");
                dtOutput.Columns.Add("Painting Fiber REJ%");
                dtOutput.Columns.Add("Painting Dust REJ%");
                dtOutput.Columns.Add("Painting Scratch REJ%");

                dtOutput.Columns.Add("Painting REJ%");
                dtOutput.Columns.Add("Total Laser REJ%");
                dtOutput.Columns.Add("Total Others REJ%");
                dtOutput.Columns.Add("Total REJ%");
                dtOutput.Columns.Add("OK QTY");
                #endregion



                #region combine data
                foreach (DataRow dr in dtMain.Rows)
                {
                    string sJobNumber = dr["JobNumber"].ToString();

                    DataRow drOutput = dtOutput.NewRow();
                    drOutput["Part No"] = dr["PartNumber"].ToString();
                    drOutput["Lot No"] = dr["LotNo"].ToString();
                    //带a连接标签的jobno, 做跳转连接
                    drOutput["Job No"] = string.Format("<a href=\"../../Buyoff/OverallBuyoff?JobNumber={0}\" target=\"_blank\">{1}</a>", sJobNumber,sJobNumber);

                    drOutput["MFG Date"] = dr["MFG Date"].ToString();
                    drOutput["Painting Under Coat Date"] = dr["Painting Under Coat Date"].ToString();
                    drOutput["Painting Under Coat M/C No"] = dr["Painting Under Coat M/C No"].ToString();
                    drOutput["Painting Under Coat M/C running time"] = dr["Painting Under Coat M/C running time"].ToString();
                    drOutput["Painting Under Coat oven time"] = dr["Painting Under Coat oven time"].ToString();
                    drOutput["Paint Lot Under Coat"] = dr["Paint Lot Under Coat"].ToString();
                    drOutput["Thinners Lot Under Coat"] = dr["Thinners Lot Under Coat"].ToString();
                    drOutput["Painting Thickness Under Coat"] = dr["Painting Thickness Under Coat"].ToString();
                    drOutput["Painting PIC Under Coat"] = dr["Painting PIC Under Coat"].ToString();

                    drOutput["Painting Middle Coat Date"] = dr["Painting Middle Coat Date"].ToString();
                    drOutput["Painting Middle Coat M/C No"] = dr["Painting Middle Coat M/C No"].ToString();
                    drOutput["Painting Middle Coat M/C running time"] = dr["Painting Middle Coat M/C running time"].ToString();
                    drOutput["Painting Middle Coat oven time"] = dr["Painting Middle Coat oven time"].ToString();
                    drOutput["Paint Lot Middle Coat"] = dr["Paint Lot Middle Coat"].ToString();
                    drOutput["Thinners Lot Middle Coat"] = dr["Thinners Lot Middle Coat"].ToString();
                    drOutput["Painting Thickness Middle Coat"] = dr["Painting Thickness Middle Coat"].ToString();
                    drOutput["Painting PIC Middle Coat"] = dr["Painting PIC Middle Coat"].ToString();

                    drOutput["Painting Top Coat Date"] = dr["Painting Top Coat Date"].ToString();
                    drOutput["Painting Top Coat M/C No"] = dr["Painting Top Coat M/C No"].ToString();
                    drOutput["Painting Top Coat M/C running time"] = dr["Painting Top Coat M/C running time"].ToString();
                    drOutput["Painting Top Coat oven time"] = dr["Painting Top Coat oven time"].ToString();
                    drOutput["Paint Lot Top Coat"] = dr["Paint Lot Top Coat"].ToString();
                    drOutput["Thinners Lot Top Coat"] = dr["Thinners Lot Top Coat"].ToString();
                    drOutput["Painting Thickness Top Coat"] = dr["Painting Thickness Top Coat"].ToString();
                    drOutput["Painting PIC Top Coat"] = dr["Painting PIC Top Coat"].ToString();


                    drOutput["Temperature Front"] = dr["Temperature Front"].ToString();
                    drOutput["Temperature Rear"] = dr["Temperature Rear"].ToString();
                    drOutput["Humidity Front"] = dr["Humidity Front"].ToString();
                    drOutput["Humidity Rear"] = dr["Humidity Rear"].ToString();
                 

                    drOutput["PQC PIC"] = dr["PQC PIC"].ToString();
                    //drOutput["Total Checked Qty"] = dr["Total Checked Qty"].ToString();
                    drOutput["OK QTY"] = dr["OK QTY"].ToString();
                    drOutput["Lot Qty"] = dtPQCDefect.Select(" JobNumber = '" + sJobNumber + "' ")[0]["lotQty"].ToString();

                    drOutput["Laser & Checking Date"] = dr["Laser & Checking Date"].ToString();
                    drOutput["Laser M/C No"] = dr["Laser M/C No"].ToString();
                    drOutput["Laser PIC"] = dr["Laser PIC"].ToString();




                    foreach (DataRow drDefect in dtAllDefectCode.Rows)
                    {
                        string defectCodeID = drDefect["DefectCodeID"].ToString();


                        string sTemp = dtPQCDefect.Select(" JobNumber = '" + sJobNumber + "' ")[0][defectCodeID].ToString();
                        if (sTemp == "")
                        {
                            drOutput[defectCodeID] = "0";
                        }
                        else
                        {
                            drOutput[defectCodeID] = sTemp;
                        }
                    }

                    drOutput["Total REJ QTY"] = dtPQCDefect.Select(" JobNumber = '" + sJobNumber + "' ")[0]["Total REJ QTY"].ToString();
                    drOutput["Total REJ AMT"] = dtPQCDefect.Select(" JobNumber = '" + sJobNumber + "' ")[0]["Total REJ AMT"].ToString();
                    drOutput["Total Mould REJ%"] = dtPQCDefect.Select(" JobNumber = '" + sJobNumber + "' ")[0]["Total Mould REJ%"].ToString();
                    drOutput["Painting Particle REJ%"] = dtPQCDefect.Select(" JobNumber = '" + sJobNumber + "' ")[0]["Painting Particle REJ%"].ToString();
                    drOutput["Painting Many Particle REJ%"] = dtPQCDefect.Select(" JobNumber = '" + sJobNumber + "' ")[0]["Painting Many Particle REJ%"].ToString();
                    drOutput["Painting Fiber REJ%"] = dtPQCDefect.Select(" JobNumber = '" + sJobNumber + "' ")[0]["Painting Fiber REJ%"].ToString();
                    drOutput["Painting Dust REJ%"] = dtPQCDefect.Select(" JobNumber = '" + sJobNumber + "' ")[0]["Painting Dust REJ%"].ToString();
                    drOutput["Painting Scratch REJ%"] = dtPQCDefect.Select(" JobNumber = '" + sJobNumber + "' ")[0]["Painting Scratch REJ%"].ToString();

                    drOutput["Painting REJ%"] = dtPQCDefect.Select(" JobNumber = '" + sJobNumber + "' ")[0]["Painting REJ%"].ToString();
                    drOutput["Total Laser REJ%"] = dtPQCDefect.Select(" JobNumber = '" + sJobNumber + "' ")[0]["Total Laser REJ%"].ToString();
                    drOutput["Total Others REJ%"] = dtPQCDefect.Select(" JobNumber = '" + sJobNumber + "' ")[0]["Total Others REJ%"].ToString();
                    drOutput["Total REJ%"] = dtPQCDefect.Select(" JobNumber = '" + sJobNumber + "' ")[0]["Total REJ%"].ToString();


                    dtOutput.Rows.Add(drOutput);
                }
                #endregion








                #region add a summary row for dtoutput

                double totalMRPQty = 0;
                double totalCheckQty = 0;
                int totalOKQty = 0;
                decimal totalRejCost = 0;
                int totalPaintingParticleRej = 0;
                int totalPaintingManyParticleRej = 0;
                int totalPaintingFiberRej = 0;
                int totalPaintingDustRej = 0;
                int totalPaintingScratchRej = 0;
                Dictionary<string, int> dicMouldingRejTotal = new Dictionary<string, int>();
                Dictionary<string, int> dicPaintingRejTotal = new Dictionary<string, int>();
                Dictionary<string, int> dicLaserRejTotal = new Dictionary<string, int>();
                Dictionary<string, int> dicOthersRejTotal = new Dictionary<string, int>();

                foreach (DataRow dr in dtOutput.Rows)
                {
                    totalMRPQty += double.Parse(dr["Lot Qty"].ToString());

                    //totalCheckQty += double.Parse(dr["Total Checked Qty"].ToString());
                    totalOKQty += int.Parse(dr["OK QTY"].ToString());
                    totalRejCost += decimal.Parse(dr["Total REJ AMT"].ToString().Trim('$'));

                    totalPaintingParticleRej += int.Parse(dr["34"].ToString());
                    totalPaintingFiberRej += int.Parse(dr["35"].ToString());
                    totalPaintingManyParticleRej += int.Parse(dr["36"].ToString());
                    totalPaintingDustRej += int.Parse(dr["43"].ToString());
                    totalPaintingScratchRej += int.Parse(dr["46"].ToString());


                    foreach (DataRow drDefectCode in drArrMoulding)
                    {
                        string codeID = drDefectCode["defectCodeID"].ToString();

                        string sTemp = dr[codeID].ToString();
                        int dRejCount = sTemp == "" ? 0 : int.Parse(sTemp);

                        if (!dicMouldingRejTotal.ContainsKey(codeID))
                            dicMouldingRejTotal.Add(codeID, dRejCount);
                        else
                            dicMouldingRejTotal[codeID] += dRejCount;
                    }


                    foreach (DataRow drDefectCode in drArrPainting)
                    {
                        string codeID = drDefectCode["defectCodeID"].ToString();

                        string sTemp = dr[codeID].ToString();
                        int dRejCount = sTemp == "" ? 0 : int.Parse(sTemp);


                        if (!dicPaintingRejTotal.ContainsKey(codeID))
                            dicPaintingRejTotal.Add(codeID, dRejCount);
                        else
                            dicPaintingRejTotal[codeID] += dRejCount;
                    }


                    foreach (DataRow drDefectCode in drArrLaser)
                    {
                        string codeID = drDefectCode["defectCodeID"].ToString();

                        string sTemp = dr[codeID].ToString();
                        int dRejCount = sTemp == "" ? 0 : int.Parse(sTemp);

                        if (!dicLaserRejTotal.ContainsKey(codeID))
                            dicLaserRejTotal.Add(codeID, dRejCount);
                        else
                            dicLaserRejTotal[codeID] += dRejCount;
                    }

                    foreach (DataRow drDefectCode in drArrOthers)
                    {
                        string codeID = drDefectCode["defectCodeID"].ToString();

                        string tempQty = dr[codeID].ToString();
                        int rejQty = tempQty == "" ? 0 : int.Parse(tempQty);

                        if (!dicOthersRejTotal.ContainsKey(codeID))
                            dicOthersRejTotal.Add(codeID, rejQty);
                        else
                            dicOthersRejTotal[codeID] += rejQty;
                    }
                }

                DataRow drOutputSummary = dtOutput.NewRow();
                DataRow drOutputSummaryRate = dtOutput.NewRow();

                drOutputSummary["Lot Qty"] = totalMRPQty;
                //drOutputSummary["Total Checked Qty"] = totalCheckQty;
                //drOutputSummaryRate["Total Checked Qty"] = "Rej%";


                int totalMouldingRej = 0;
                int totalPaintingRej = 0;
                int totalLaserRej = 0;
                int totalOthersRej = 0;
                int totalRej = 0;

                foreach (KeyValuePair<string, int> kv in dicMouldingRejTotal)
                {
                    totalMouldingRej += kv.Value;
                    totalRej += kv.Value;
                    //drOutputSummary[kv.Key] = kv.Value;
                    //drOutputSummaryRate[kv.Key] = Math.Round(kv.Value / totalCheckQty * 100.0, 2).ToString() + "%";


                    drOutputSummary[kv.Key] = string.Format("{0} ({1})", kv.Value, Math.Round(kv.Value / totalMRPQty * 100.0, 2).ToString() + "%");

                }

                foreach (KeyValuePair<string, int> kv in dicPaintingRejTotal)
                {
                    totalPaintingRej += kv.Value;
                    totalRej += kv.Value;
                    //drOutputSummary[kv.Key] = kv.Value;
                    //drOutputSummaryRate[kv.Key] = Math.Round(kv.Value / totalCheckQty * 100.0, 2).ToString() + "%";


                    drOutputSummary[kv.Key] = string.Format("{0} ({1})", kv.Value, Math.Round(kv.Value / totalMRPQty * 100.0, 2).ToString() + "%");
                }

                foreach (KeyValuePair<string, int> kv in dicLaserRejTotal)
                {
                    totalLaserRej += kv.Value;
                    totalRej += kv.Value;
                    //drOutputSummary[kv.Key] = kv.Value;
                    //drOutputSummaryRate[kv.Key] = Math.Round(kv.Value / totalCheckQty * 100.0, 2).ToString() + "%";


                    drOutputSummary[kv.Key] = string.Format("{0} ({1})", kv.Value, Math.Round(kv.Value / totalMRPQty * 100.0, 2).ToString() + "%");
                }

                foreach (KeyValuePair<string, int> kv in dicOthersRejTotal)
                {
                    totalOthersRej += kv.Value;
                    totalRej += kv.Value;
                    //drOutputSummary[kv.Key] = kv.Value;
                    //drOutputSummaryRate[kv.Key] = Math.Round(kv.Value / totalCheckQty * 100.0, 2).ToString() + "%";


                    drOutputSummary[kv.Key] = string.Format("{0} ({1})", kv.Value, Math.Round(kv.Value / totalMRPQty * 100.0, 2).ToString() + "%");
                }

                drOutputSummary["Moulding Defect"] = totalMouldingRej;
                drOutputSummary["Painting Defect"] = totalPaintingRej;
                drOutputSummary["Laser Defect"] = totalLaserRej;
                drOutputSummary["Others Defect"] = totalOthersRej;
                drOutputSummary["Total REJ QTY"] = totalRej;
                drOutputSummary["Total REJ AMT"] = "$" + Math.Round(totalRejCost, 2).ToString();



                //drOutputSummaryRate["Total Mould REJ%"] = Math.Round(totalMouldingRej / totalMRPQty * 100.0, 2).ToString("0.00") + "%";

                //drOutputSummaryRate["Painting Particle REJ%"] = Math.Round(totalPaintingParticleRej / totalMRPQty * 100.0, 2).ToString("0.00") + "%";
                //drOutputSummaryRate["Painting Fiber REJ%"] = Math.Round(totalPaintingFiberRej / totalMRPQty * 100.0, 2).ToString("0.00") + "%";
                //drOutputSummaryRate["Painting Many Particle REJ%"] = Math.Round(totalPaintingManyParticleRej / totalMRPQty * 100.0, 2).ToString("0.00") + "%";
                //drOutputSummaryRate["Painting Dust REJ%"] = Math.Round(totalPaintingDustRej / totalMRPQty * 100.0, 2).ToString("0.00") + "%";
                //drOutputSummaryRate["Painting Scratch REJ%"] = Math.Round(totalPaintingScratchRej / totalMRPQty * 100.0, 2).ToString("0.00") + "%";

                //drOutputSummaryRate["Painting REJ%"] = Math.Round(totalPaintingRej / totalMRPQty * 100.0, 2).ToString("0.00") + "%";
                //drOutputSummaryRate["Total Laser REJ%"] = Math.Round(totalLaserRej / totalMRPQty * 100.0, 2).ToString("0.00") + "%";
                //drOutputSummaryRate["Total Others REJ%"] = Math.Round(totalOthersRej / totalMRPQty * 100.0, 2).ToString("0.00") + "%";
                //drOutputSummaryRate["Total REJ%"] = Math.Round(totalRej / totalMRPQty * 100.0, 2).ToString("0.00") + "%";



                drOutputSummary["Total Mould REJ%"] = Math.Round(totalMouldingRej / totalMRPQty * 100.0, 2).ToString("0.00") + "%";

                drOutputSummary["Painting Particle REJ%"] = Math.Round(totalPaintingParticleRej / totalMRPQty * 100.0, 2).ToString("0.00") + "%";
                drOutputSummary["Painting Fiber REJ%"] = Math.Round(totalPaintingFiberRej / totalMRPQty * 100.0, 2).ToString("0.00") + "%";
                drOutputSummary["Painting Many Particle REJ%"] = Math.Round(totalPaintingManyParticleRej / totalMRPQty * 100.0, 2).ToString("0.00") + "%";
                drOutputSummary["Painting Dust REJ%"] = Math.Round(totalPaintingDustRej / totalMRPQty * 100.0, 2).ToString("0.00") + "%";
                drOutputSummary["Painting Scratch REJ%"] = Math.Round(totalPaintingScratchRej / totalMRPQty * 100.0, 2).ToString("0.00") + "%";

                drOutputSummary["Painting REJ%"] = Math.Round(totalPaintingRej / totalMRPQty * 100.0, 2).ToString("0.00") + "%";
                drOutputSummary["Total Laser REJ%"] = Math.Round(totalLaserRej / totalMRPQty * 100.0, 2).ToString("0.00") + "%";
                drOutputSummary["Total Others REJ%"] = Math.Round(totalOthersRej / totalMRPQty * 100.0, 2).ToString("0.00") + "%";
                drOutputSummary["Total REJ%"] = Math.Round(totalRej / totalMRPQty * 100.0, 2).ToString("0.00") + "%";

                drOutputSummary["OK QTY"] = totalOKQty;

                dtOutput.Rows.Add(drOutputSummary);
                //dtOutput.Rows.Add(drOutputSummaryRate);


                #endregion



                DataTable dtRowColumnConverted = Datatable_RowConvertColumn(dtOutput);

                Display(dtRowColumnConverted);
            }
            catch (Exception ee)
            {
                DBHelp.Reports.LogFile.Log("PQCBezelPanelReport", "BtnGenerate_Click error : " + ee.ToString());

                if (ee.ToString().ToUpper().Contains("TIMEOUT"))
                {
                    Common.CommFunctions.ShowMessage(this.Page, "Searching timeout, please reduce the searching days!");
                }
            }
        }







        void Display(DataTable dt)
        {
            this.dgBezelPanel.DataSource = dt.DefaultView;
            this.dgBezelPanel.DataBind();
            this.dgBezelPanel.ShowHeader = false;

            //判断自动隐藏 coat的flag
            bool hasUnderCoatData = false;
            bool hasMiddleCoatData = false;
            bool hasTopCoatData = false;

            //判断隐藏0 并设置背景色 的其实结束行
            int hide_0_StartRowIndex = 0;
            int hide_0_EndRowIndex = 0;
            int hide_0_StartColumnIndex = 1;
            int hide_0_EndColumnIndex = dt.Columns.Count - 3;

            foreach (DataGridItem item in this.dgBezelPanel.Items)
            {
                //最后一列红字黄底  第三行起, 最后第12行内. 
                if (item.ItemIndex >= 2 && item.ItemIndex < (dgBezelPanel.Items.Count - 13))
                {
                    item.Cells[dt.Columns.Count - 1].ForeColor = System.Drawing.Color.Red;
                    item.Cells[dt.Columns.Count - 1].Font.Bold = true;
                    //item.Cells[dt.Columns.Count - 1].BackColor = System.Drawing.Color.Yellow;

                    //item.Cells[dt.Columns.Count - 2].ForeColor = System.Drawing.Color.Red;
                    //item.Cells[dt.Columns.Count - 2].Font.Bold = true;
                    //item.Cells[dt.Columns.Count - 2].BackColor = System.Drawing.Color.Yellow;
                }


                string rowName = item.Cells[0].Text;
                switch (rowName)
                {
                    #region special row style
                    case "PartNumber":
                        item.BackColor = System.Drawing.ColorTranslator.FromHtml("#92cddc");
                        break;
                    case "Lotno":
                        item.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffff00");
                        break;
                    case "Total Checked Qty":
                        //item.BackColor = System.Drawing.ColorTranslator.FromHtml("#ccc0da");
                        break;
                    case "MFG Date":
                        item.BackColor = System.Drawing.ColorTranslator.FromHtml("#ebf1de");
                        break;

                    case "Painting Under Coat Date":
                        item.BackColor = System.Drawing.ColorTranslator.FromHtml("#ccc0da");
                        break;
                    case "Painting Under Coat M/C No":
                        item.BackColor = System.Drawing.ColorTranslator.FromHtml("#c4d79b");
                        break;
                    case "Painting Under Coat M/C running time":
                        item.BackColor = System.Drawing.ColorTranslator.FromHtml("#c4d79b");
                        break;
                    case "Painting Under Coat oven time":
                        item.BackColor = System.Drawing.ColorTranslator.FromHtml("#c4d79b");
                        break;
                    case "Paint Lot Under Coat":
                        item.BackColor = System.Drawing.ColorTranslator.FromHtml("#c4d79b");
                        break;
                    case "Thinners Lot Under Coat":
                        item.BackColor = System.Drawing.ColorTranslator.FromHtml("#c4d79b");
                        break;
                    case "Painting Thickness Under Coat":
                        item.BackColor = System.Drawing.ColorTranslator.FromHtml("#c4d79b");
                        break;
                    case "Painting PIC Under Coat":
                        item.BackColor = System.Drawing.ColorTranslator.FromHtml("#c4d79b");
                        break;


                    case "Laser & Checking Date":
                        item.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffe5ff");
                        break;
                    case "Laser M/C No":
                        item.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffe5ff");
                        break;
                    case "Laser PIC":
                        item.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffe5ff");
                        break;
                    case "PQC PIC":
                        item.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffe5ff");
                        break;


                    case "Moulding Defect":
                        item.BackColor = System.Drawing.ColorTranslator.FromHtml("#ff9900");
                        item.Cells[0].Font.Bold = true;
                        hide_0_StartRowIndex = item.ItemIndex + 1;
                        break;
                    case "Painting Defect":
                        item.BackColor = System.Drawing.ColorTranslator.FromHtml("#ff9900");
                        item.Cells[0].Font.Bold = true;
                        break;
                    case "Laser Defect":
                        item.BackColor = System.Drawing.ColorTranslator.FromHtml("#ff9900");
                        item.Cells[0].Font.Bold = true;
                        break;
                    case "Others Defect":
                        item.BackColor = System.Drawing.ColorTranslator.FromHtml("#ff9900");
                        item.Cells[0].Font.Bold = true;
                        break;


                    case "Total REJ QTY":
                        //item.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffff00");
                        item.Font.Bold = true;

                        hide_0_EndRowIndex = item.ItemIndex - 1;
                        break;
                    case "Total REJ%":
                        //item.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffff00");
                        item.Font.Bold = true;
                        break;


                    case "Total REJ AMT":
                        item.Font.Bold = true;
                        break;
                    case "Total Mould REJ%":
                        item.Font.Bold = true;
                        break;
                    case "Painting Particle REJ%":
                        item.Font.Bold = true;
                        break;
                    case "Painting Many Particle REJ%":
                        item.Font.Bold = true;
                        break;
                    case "Painting Fiber REJ%":
                        item.Font.Bold = true;
                        break;
                    case "Painting Dust REJ%":
                        item.Font.Bold = true;
                        break;
                    case "Painting Scratch REJ%":
                        item.Font.Bold = true;
                        break;
                    case "Painting REJ%":
                        item.Font.Bold = true;
                        break;
                    case "Total Laser REJ%":
                        item.Font.Bold = true;
                        break;
                    case "Total Others REJ%":
                        item.Font.Bold = true;
                        break;



                    case "OK QTY":
                        //item.BackColor = System.Drawing.ColorTranslator.FromHtml("#ccffcc");
                        item.Font.Bold = true;
                        break;

                    default:
                        break;
                        #endregion
                }


                #region set under, middle, top coat flag
                if (rowName == "Painting Under Coat Date" || rowName == "Painting Under Coat M/C No" || rowName == "Painting Under Coat M/C running time" || rowName == "Painting Under Coat oven time" ||
                    rowName == "Paint Lot Under Coat" || rowName == "Thinners Lot Under Coat" || rowName == "Painting Thickness Under Coat" || rowName == "Painting PIC Under Coat")
                {
                    for (int i = 1; i < item.Cells.Count; i++)
                    {
                        string sTemp = item.Cells[i].Text.Replace("&nbsp;", "");
                        if (sTemp != "" || hasUnderCoatData)
                        {
                            hasUnderCoatData = true;
                        }
                    }
                }
                else if (rowName == "Painting Middle Coat Date" || rowName == "Painting Middle Coat M/C No" || rowName == "Painting Middle Coat M/C running time" || rowName == "Painting Middle Coat oven time" ||
                    rowName == "Paint Lot Middle Coat" || rowName == "Thinners Lot Middle Coat" || rowName == "Painting Thickness Middle Coat" || rowName == "Painting PIC Middle Coat")
                {
                    for (int i = 1; i < item.Cells.Count; i++)
                    {
                        string sTemp = item.Cells[i].Text.Replace("&nbsp;", "");
                        if (sTemp != "" || hasMiddleCoatData)
                        {
                            hasMiddleCoatData = true;
                        }
                    }
                }
                else if (rowName == "Painting Top Coat Date" || rowName == "Painting Top Coat M/C No" || rowName == "Painting Top Coat M/C running time" || rowName == "Painting Top Coat oven time" ||
                    rowName == "Paint Lot Top Coat" || rowName == "Thinners Lot Top Coat" || rowName == "Painting Thickness Top Coat" || rowName == "Painting PIC Top Coat")
                {
                    for (int i = 1; i < item.Cells.Count; i++)
                    {
                        string sTemp = item.Cells[i].Text.Replace("&nbsp;", "");
                        if (sTemp != "" || hasTopCoatData)
                        {
                            hasTopCoatData = true;
                        }
                    }
                }
                #endregion



                //第一列之外, 全部居中
                for (int i = 1; i < item.Cells.Count; i++)
                {
                    item.Cells[i].HorizontalAlign = HorizontalAlign.Center;
                }
            }

            #region 自动隐藏 under,middle,top 行
            foreach (DataGridItem item in this.dgBezelPanel.Items)
            {
                string rowName = item.Cells[0].Text;
                switch (rowName)
                {
                    case "Painting Under Coat Date":
                        if (!hasUnderCoatData)
                        {
                            item.Visible = false;
                        }
                        break;
                    case "Painting Under Coat M/C No":
                        if (!hasUnderCoatData)
                        {
                            item.Visible = false;
                        }
                        break;
                    case "Painting Under Coat M/C running time":
                        if (!hasUnderCoatData)
                        {
                            item.Visible = false;
                        }
                        break;
                    case "Painting Under Coat oven time":
                        if (!hasUnderCoatData)
                        {
                            item.Visible = false;
                        }
                        break;
                    case "Paint Lot Under Coat":
                        if (!hasUnderCoatData)
                        {
                            item.Visible = false;
                        }
                        break;
                    case "Thinners Lot Under Coat":
                        if (!hasUnderCoatData)
                        {
                            item.Visible = false;
                        }
                        break;
                    case "Painting Thickness Under Coat":
                        if (!hasUnderCoatData)
                        {
                            item.Visible = false;
                        }
                        break;
                    case "Painting PIC Under Coat":
                        if (!hasUnderCoatData)
                        {
                            item.Visible = false;
                        }
                        break;



                    case "Painting Middle Coat Date":
                        if (!hasMiddleCoatData)
                        {
                            item.Visible = false;
                        }
                        break;
                    case "Painting Middle Coat M/C No":
                        if (!hasMiddleCoatData)
                        {
                            item.Visible = false;
                        }
                        break;
                    case "Painting Middle Coat M/C running time":
                        if (!hasMiddleCoatData)
                        {
                            item.Visible = false;
                        }
                        break;
                    case "Painting Middle Coat oven time":
                        if (!hasMiddleCoatData)
                        {
                            item.Visible = false;
                        }
                        break;
                    case "Paint Lot Middle Coat":
                        if (!hasMiddleCoatData)
                        {
                            item.Visible = false;
                        }
                        break;
                    case "Thinners Lot Middle Coat":
                        if (!hasMiddleCoatData)
                        {
                            item.Visible = false;
                        }
                        break;
                    case "Painting Thickness Middle Coat":
                        if (!hasMiddleCoatData)
                        {
                            item.Visible = false;
                        }
                        break;
                    case "Painting PIC Middle Coat":
                        if (!hasMiddleCoatData)
                        {
                            item.Visible = false;
                        }
                        break;





                    case "Painting Top Coat Date":
                        if (!hasTopCoatData)
                        {
                            item.Visible = false;
                        }
                        break;
                    case "Painting Top Coat M/C No":
                        if (!hasTopCoatData)
                        {
                            item.Visible = false;
                        }
                        break;
                    case "Painting Top Coat M/C running time":
                        if (!hasTopCoatData)
                        {
                            item.Visible = false;
                        }
                        break;
                    case "Painting Top Coat oven time":
                        if (!hasTopCoatData)
                        {
                            item.Visible = false;
                        }
                        break;
                    case "Paint Lot Top Coat":
                        if (!hasTopCoatData)
                        {
                            item.Visible = false;
                        }
                        break;
                    case "Thinners Lot Top Coat":
                        if (!hasTopCoatData)
                        {
                            item.Visible = false;
                        }
                        break;
                    case "Painting Thickness Top Coat":
                        if (!hasTopCoatData)
                        {
                            item.Visible = false;
                        }
                        break;
                    case "Painting PIC Top Coat":
                        if (!hasTopCoatData)
                        {
                            item.Visible = false;
                        }
                        break;



                    default:
                        break;
                }
            }
            #endregion


            //隐藏 defectCode 为0的 cells,  非0的defect value设置红字 粗体
            for (int i = hide_0_StartRowIndex - 1; i <= hide_0_EndRowIndex; i++)
            {
                for (int j = hide_0_StartColumnIndex; j <= hide_0_EndColumnIndex; j++)
                {
                    string sValue = this.dgBezelPanel.Items[i].Cells[j].Text;

                    sValue = sValue == "&nbsp;" ? "" : sValue;

                    if (sValue == "")
                        continue;
                    else if (sValue == "0")
                    {
                        this.dgBezelPanel.Items[i].Cells[j].Text = "";
                    }
                    else if (sValue != "0" && sValue != "")
                    {
                        //this.dgBezelPanel.Items[i].Cells[j].BackColor = System.Drawing.Color.Pink;

                        this.dgBezelPanel.Items[i].Cells[j].ForeColor = System.Drawing.Color.Red;
                        this.dgBezelPanel.Items[i].Cells[j].Font.Bold = true;
                    }
                }
            }


        }






        public DataTable Datatable_RowConvertColumn(DataTable dtOld)
        {

            DataTable dtNew = new DataTable();

            int newTableColumnCount = dtOld.Rows.Count;
            int newTableRowCount = dtOld.Columns.Count;


            //set new table column 
            //第一列 列名不变
            dtNew.Columns.Add(dtOld.Columns[0].ColumnName);
            //原table, 每行第一列追加到column.
            for (int i = 0; i < newTableColumnCount; i++)
            {
                dtNew.Columns.Add(i.ToString());
            }



            //set new table row
            for (int i = 0; i < newTableRowCount; i++)
            {
                DataRow drNew = dtNew.NewRow();

                //新行第一列, 原table column name
                if (Common.CommFunctions.isNumberic(dtOld.Columns[i].ColumnName))
                {
                    drNew[0] = dtOld.Columns[i].Caption;
                }
                else
                {
                    drNew[0] = dtOld.Columns[i].ColumnName;
                }


                for (int x = 0; x < dtOld.Rows.Count; x++)
                {
                    drNew[x + 1] = dtOld.Rows[x][i].ToString();
                }

                dtNew.Rows.Add(drNew);

            }

            return dtNew;
        }






        private void setNumber(string reportType)
        {

            this.ddlNumber.Items.Clear();

            ListItem li = new ListItem();
            li.Text = "All";
            li.Value = "";
            this.ddlNumber.Items.Add(li);


            Common.Class.BLL.PQCBom_BLL bll = new Common.Class.BLL.PQCBom_BLL();
            DataTable dt = bll.GetList("");
            if (dt == null || dt.Rows.Count == 0)
                return;

            DataRow[] drArr = dt.Select(" description = '" + reportType.ToUpper() + "' ", " number asc ");
            if (drArr.Length == 0)
                return;

            foreach (DataRow dr in drArr)
            {
                li = new ListItem();

                string number = dr["number"].ToString();
                string description = dr["description"].ToString();

                if (description.ToUpper() == reportType.ToUpper() && number.Trim() != "")
                {
                    li.Text = number;
                    li.Value = number;
                    if (!this.ddlNumber.Items.Contains(li))
                    {
                        this.ddlNumber.Items.Add(li);
                    }
                }
            }
        }


       

    }
}