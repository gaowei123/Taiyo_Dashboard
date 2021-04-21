using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace DashboardTTS.Webform.PQC
{
    public partial class PQCButtonReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    if (string.IsNullOrEmpty(Request.QueryString["Description"]))
                    {
                        Common.CommFunctions.ShowMessage(this.Page, "Open error, No report type specified!");
                        return;
                    }


                    //周日, 周一显示 上周五的. 默认显示前一天的.
                    DateTime dLastDay = Common.CommFunctions.GetDefaultReportsSearchingDay();
                    this.txtDateFrom.Text = dLastDay.ToString("yyyy-MM-dd");                    
                    this.lbHeader.Text = Request.QueryString["Description"] == "BUTTON" ? "PQC Button Report" : "Laser & PQC Total Report";


                    BtnGenerate_Click(new object(), new EventArgs());
                }
                catch (Exception ex)
                {
                    DBHelp.Reports.LogFile.Log("ButtonTotalReport_Debug", "Page_Load error : " + ex.ToString());
                }
            }
        }

        protected void BtnGenerate_Click(object sender, EventArgs e)
        {
            try
            {
                #region 这几个条件都不用了.
                string JobNo = "";
                string partNumber = "";
                string model = "";
                string color = "";
                string supplier = "";
                string coating = "";
                #endregion

                DateTime DateFrom = DateTime.Parse(this.txtDateFrom.Text).Date;
                DateTime DateTo = DateFrom.AddDays(1);

                // 用来选定现显示 laser / wip 部分列表
                string reportType = this.ddlType.SelectedItem.Value;

                // "BUTTON": 只显示pqcbom description设定为button的part
                // ""(传空): 不指定type类型, 全显示.
                string sDescription = Request.QueryString["Description"];
                

                ViewModel.PQCButtonReport_ViewModel.Report modelForDisplay = new ViewModel.PQCButtonReport_ViewModel.Report();

                ViewBusiness.ButtonReport_ViewBusiness vBLL = new ViewBusiness.ButtonReport_ViewBusiness();
                DataTable dtReport = vBLL.GetResultDt(DateFrom, DateTo, sDescription, partNumber, JobNo, model, supplier, color, coating, reportType, out modelForDisplay);
                
                Display(dtReport, modelForDisplay);
            }
            catch (Exception ex)
            {
                DBHelp.Reports.LogFile.Log("ButtonTotalReport_Debug", "BtnGenerate_Click   error : " + ex.ToString());
                Common.CommFunctions.ShowMessage(this.Page, "Warning!  catch exception: " + ex.ToString());
                return;
            }
        }
        

        void Display(DataTable dt, ViewModel.PQCButtonReport_ViewModel.Report modelForDisplay)
        {

            try
            {
                if (dt == null || dt.Rows.Count == 0)
                {
                    this.dgButton.Visible = false;
                    Common.CommFunctions.ShowMessage(this.Page, "There is no any data!");
                    return;
                }
                else
                {
                    this.dgButton.Visible = true;
                }


                dgButton.DataSource = dt.DefaultView;
                dgButton.DataBind();


                int sn = 1;

                int iPart2TitleRowIndex = 0;

                foreach (DataGridItem item in dgButton.Items)
                {
                    //处理sn
                    item.Cells[0].Text = sn.ToString();
                    sn++;

                    string sPartRowText = item.Cells[4].Text;

                    for (int i = 0; i < dgButton.Columns.Count; i++)
                    {
                        //处理datetime列信息.  转换成 dd/MM/yyyy
                        string columnName = dgButton.Columns[i].HeaderText;
                        if (columnName.ToUpper().Contains("DATE"))
                        {
                            string sDate = item.Cells[i].Text;
                            if (sDate != "" && sDate != "&nbsp;")
                                item.Cells[i].Text = DateTime.Parse(sDate).ToString("dd/MM/yyyy");
                        }


                        //处理reject rate列信息, 加上 '%'
                        if (columnName.Contains("%"))
                        {
                            if (columnName == "Rej(%)")
                                continue;

                            string sRej = item.Cells[i].Text;
                            if (sRej != "" && sRej != "&nbsp;")
                                item.Cells[i].Text = sRej + "%";
                        }


                        #region 处理summary行, defect code列只有对应分类显示,  other,mould,paint,laser行 lotqty, pass2列值赋空
                        if (sPartRowText == "OTHERS >")
                        {
                            if (columnName.Contains("(TM)") || columnName.Contains("(VM)") || columnName.Contains("(P)") || columnName.Contains("(L)"))
                                item.Cells[i].Text = "";
                            else if (columnName == "Lot Qty" || columnName == "Pass")
                                item.Cells[i].Text = "";
                        }
                        else if (sPartRowText == "TTS MOULD >")
                        {
                            if (columnName.Contains("(VM)") || columnName.Contains("(P)") || columnName.Contains("(L)") || columnName.Contains("(O)"))
                                item.Cells[i].Text = "";
                            else if (columnName == "Pass")
                                item.Cells[i].Text = "";
                        }
                        else if (sPartRowText == "VENDOR MOULD >")
                        {
                            if (columnName.Contains("(TM)") || columnName.Contains("(P)") || columnName.Contains("(L)") || columnName.Contains("(O)"))
                                item.Cells[i].Text = "";
                            else if (columnName == "Pass")
                                item.Cells[i].Text = "";
                        }
                        else if (sPartRowText == "PAINTING >")
                        {
                            if (columnName.Contains("(TM)") || columnName.Contains("(VM)") || columnName.Contains("(L)") || columnName.Contains("(O)"))
                                item.Cells[i].Text = "";
                            else if (columnName == "Lot Qty" || columnName == "Pass")
                                item.Cells[i].Text = "";
                        }
                        else if (sPartRowText == "PAINTING SETUP >")
                        {
                            if (columnName.Contains("(TM)") || columnName.Contains("(VM)") || columnName.Contains("(P)") || columnName.Contains("(L)") || columnName.Contains("(O)"))
                                item.Cells[i].Text = "";
                            else if (columnName == "Lot Qty" || columnName == "Pass")
                                item.Cells[i].Text = "";
                        }
                        else if (sPartRowText == "QA PAINT TEST >")
                        {
                            if (columnName.Contains("(TM)") || columnName.Contains("(VM)") || columnName.Contains("(P)") || columnName.Contains("(L)") || columnName.Contains("(O)"))
                                item.Cells[i].Text = "";
                            else if (columnName == "Lot Qty" || columnName == "Pass")
                                item.Cells[i].Text = "";
                        }

                        else if (sPartRowText == "LASER >")
                        {
                            if (columnName.Contains("(TM)") || columnName.Contains("(VM)") || columnName.Contains("(P)") || columnName.Contains("(O)"))
                                item.Cells[i].Text = "";
                            else if (columnName == "Lot Qty" || columnName == "Pass")
                                item.Cells[i].Text = "";

                        }
                        else if (sPartRowText == "OVERALL >")
                        {
                            if (columnName.Contains("(TM)") || columnName.Contains("(VM)") || columnName.Contains("(P)") || columnName.Contains("(L)") || columnName.Contains("(O)"))
                                item.Cells[i].Text = "";
                        }
                        #endregion


                        #region 处理defect code qty, 为0的 填空,  非0的红色字体加粗.
                        if (columnName.Contains("(TM)") || columnName.Contains("(VM)") || columnName.Contains("(P)") || columnName.Contains("(L)") || columnName.Contains("(O)"))
                        {
                            //特殊行跳过
                            if (sPartRowText == "OTHERS >" || sPartRowText == "TTS MOULD >" ||
                                sPartRowText == "VENDOR MOULD >" || sPartRowText == "PAINTING >" || sPartRowText == "PAINTING SETUP >" ||
                                sPartRowText == "QA PAINT TEST >" || sPartRowText == "LASER >" || sPartRowText == "OVERALL >")
                                continue;


                            //特殊列跳过
                            if (columnName == "(L)Machine" || columnName == "(L)OP" || columnName == "(L)Date" ||
                                columnName == "(P)U/C 1st Coat" || columnName == "(P)1st Machine" || columnName == "(P)1st Date" ||
                                columnName == "(P)M/C 2nd Coat" || columnName == "(P)2nd Machine" || columnName == "(P)2nd Date" ||
                                columnName == "(P)T/C 3rd Coat" || columnName == "(P)3rd Machine" || columnName == "(P)3rd Date" ||
                                columnName == "(L)Machine" || columnName == "(L)OP" || columnName == "(L)Date" ||
                                columnName == "(TM)TotalRej" || columnName == "(VM)TotalRej" || columnName == "(P)TotalRej" ||
                                columnName == "(L)TotalRej" || columnName == "(O)TotalRej" || columnName.Contains("%"))
                                continue;


                            string sQty = item.Cells[i].Text;
                            if (sQty == "0")
                            {
                                item.Cells[i].Text = "";
                            }
                            else
                            {
                                item.Cells[i].Font.Bold = true;
                                item.Cells[i].ForeColor = sPartRowText == "SUB TOTAL>>" ? System.Drawing.Color.DarkRed : System.Drawing.Color.Red;
                            }
                        }
                        #endregion


                    }


                    #region 处理 special row  style
                    string titleText = item.Cells[1].Text;
                    string modelSummaryRowText = item.Cells[4].Text;
                    string summaryRowText = item.Cells[4].Text;
                    string summaryTitleText = item.Cells[2].Text;

                    //part 1 title  style
                    if (titleText == "PART 1: PAINTING ONLY PARTS")
                    {
                        item.Cells[1].ForeColor = System.Drawing.Color.Brown;
                        item.Cells[1].Font.Bold = true;
                        item.Cells[1].Font.Underline = true;
                        item.Cells[1].ColumnSpan = 3;

                        item.Cells[2].ForeColor = System.Drawing.Color.Brown;
                        item.Cells[2].Font.Bold = true;
                        item.Cells[2].Font.Underline = true;
                        item.Cells[2].ColumnSpan = 3;


                        for (int i = 0; i < item.Cells.Count; i++)
                        {
                            item.Cells[i].BorderStyle = BorderStyle.None;
                            item.Cells[i].BackColor = System.Drawing.Color.White;
                        }


                        item.Cells[3].Visible = false;
                        item.Cells[4].Visible = false;
                        item.Cells[5].Visible = false;

                    }
                    //part 2 title  style
                    else if (titleText == "PART 2: LASER PARTS")
                    {
                        item.Cells[1].ForeColor = System.Drawing.Color.Blue;
                        item.Cells[1].Font.Bold = true;
                        item.Cells[1].Font.Underline = true;
                        item.Cells[1].ColumnSpan = 3;

                        item.Cells[2].ForeColor = System.Drawing.Color.Blue;
                        item.Cells[2].Font.Bold = true;
                        item.Cells[2].Font.Underline = true;
                        item.Cells[2].ColumnSpan = 3;


                        for (int i = 0; i < item.Cells.Count; i++)
                        {
                            item.Cells[i].BorderStyle = BorderStyle.None;
                            item.Cells[i].BackColor = System.Drawing.Color.White;
                        }

                        item.Cells[3].Visible = false;
                        item.Cells[4].Visible = false;
                        item.Cells[5].Visible = false;

                        iPart2TitleRowIndex = item.ItemIndex;
                    }
                    //summary row  title style
                    else if (titleText == "SUMMARY:"  || summaryTitleText == "Paint Part Summary" || summaryTitleText == "Laser Part Summary")
                    {
                        item.Cells[1].ForeColor = System.Drawing.Color.DeepPink;
                        item.Cells[1].Font.Bold = true;
                        item.Cells[1].ColumnSpan = 3;

                        item.Cells[2].ForeColor = System.Drawing.Color.DeepPink;
                        item.Cells[2].Font.Bold = true;
                        item.Cells[2].ColumnSpan = 3;

                        for (int i = 0; i < item.Cells.Count; i++)
                        {
                            item.Cells[i].BorderStyle = BorderStyle.None;
                            item.Cells[i].BackColor = System.Drawing.Color.White;
                        }

                        item.Cells[3].Visible = false;
                        item.Cells[4].Visible = false;
                        item.Cells[5].Visible = false;
                    }
                    //sub total row style
                    else if (modelSummaryRowText == "SUB TOTAL>>")
                    {
                        item.BackColor = System.Drawing.Color.Beige;
                        item.Cells[1].Font.Bold = true;
                        item.Cells[4].Font.Bold = true;
                    }
                    else if (summaryRowText == "OTHERS >" || summaryRowText == "TTS MOULD >" || summaryRowText == "VENDOR MOULD >" || summaryRowText == "PAINTING >"
                        || summaryRowText == "PAINTING SETUP >" || summaryRowText == "QA PAINT TEST >" || summaryRowText == "LASER >" || summaryRowText == "OVERALL >")
                    {
                        for (int i = 3; i < 12; i++)
                        {
                            item.Cells[i].Font.Bold = true;
                            item.Cells[i].ForeColor = System.Drawing.Color.Black;
                        }
                    }
                    #endregion


                }


                #region part 1 laser info 区域灰色覆盖

                //part 1 summary row 前一行
                int iPart1LastRowIndex = iPart2TitleRowIndex - 10;

                for (int i = 1; i < iPart1LastRowIndex; i++)
                {
                    for (int x = 0; x < dgButton.Columns.Count; x++)
                    {
                        string columnName = dgButton.Columns[x].HeaderText;
                        if (columnName.Contains("(L)"))
                        {
                            this.dgButton.Items[i].Cells[x].BackColor = System.Drawing.Color.Gainsboro;
                            this.dgButton.Items[i].Cells[x].Text = "";
                        }
                    }
                }



                #endregion





                


                #region defect rej 0的列隐藏
                this.dgButton.Columns[12].Visible = modelForDisplay.TTS_Raw_Part_Scratch == 0 ? false : true;
                this.dgButton.Columns[13].Visible = modelForDisplay.TTS_Oil_Stain == 0 ? false : true;
                this.dgButton.Columns[14].Visible = modelForDisplay.TTS_Dented == 0 ? false : true;
                this.dgButton.Columns[15].Visible = modelForDisplay.TTS_Dust == 0 ? false : true;
                this.dgButton.Columns[16].Visible = modelForDisplay.TTS_Flyout == 0 ? false : true;
                this.dgButton.Columns[17].Visible = modelForDisplay.TTS_Over_Spray == 0 ? false : true;
                this.dgButton.Columns[18].Visible = modelForDisplay.TTS_Weld_line == 0 ? false : true;
                this.dgButton.Columns[19].Visible = modelForDisplay.TTS_Crack == 0 ? false : true;
                this.dgButton.Columns[20].Visible = modelForDisplay.TTS_Gas_mark == 0 ? false : true;
                this.dgButton.Columns[21].Visible = modelForDisplay.TTS_Sink_mark == 0 ? false : true;
                this.dgButton.Columns[22].Visible = modelForDisplay.TTS_Bubble == 0 ? false : true;
                this.dgButton.Columns[23].Visible = modelForDisplay.TTS_White_dot == 0 ? false : true;
                this.dgButton.Columns[24].Visible = modelForDisplay.TTS_Black_dot == 0 ? false : true;
                this.dgButton.Columns[25].Visible = modelForDisplay.TTS_Red_Dot == 0 ? false : true;
                this.dgButton.Columns[26].Visible = modelForDisplay.TTS_Poor_Gate_Cut == 0 ? false : true;
                this.dgButton.Columns[27].Visible = modelForDisplay.TTS_High_Gate == 0 ? false : true;
                this.dgButton.Columns[28].Visible = modelForDisplay.TTS_White_Mark == 0 ? false : true;
                this.dgButton.Columns[29].Visible = modelForDisplay.TTS_Drag_mark == 0 ? false : true;
                this.dgButton.Columns[30].Visible = modelForDisplay.TTS_Foreigh_Material == 0 ? false : true;
                this.dgButton.Columns[31].Visible = modelForDisplay.TTS_Double_Claim == 0 ? false : true;
                this.dgButton.Columns[32].Visible = modelForDisplay.TTS_Short_mould == 0 ? false : true;
                this.dgButton.Columns[33].Visible = modelForDisplay.TTS_Flashing == 0 ? false : true;
                this.dgButton.Columns[34].Visible = modelForDisplay.TTS_Pink_Mark == 0 ? false : true;
                this.dgButton.Columns[35].Visible = modelForDisplay.TTS_Deform == 0 ? false : true;
                this.dgButton.Columns[36].Visible = modelForDisplay.TTS_Damage == 0 ? false : true;
                this.dgButton.Columns[37].Visible = modelForDisplay.TTS_Mould_Dirt == 0 ? false : true;
                this.dgButton.Columns[38].Visible = modelForDisplay.TTS_Yellowish == 0 ? false : true;
                this.dgButton.Columns[39].Visible = modelForDisplay.TTS_Oil_Mark == 0 ? false : true;
                this.dgButton.Columns[40].Visible = modelForDisplay.TTS_Printing_Mark == 0 ? false : true;
                this.dgButton.Columns[41].Visible = modelForDisplay.TTS_Printing_Uneven == 0 ? false : true;
                this.dgButton.Columns[42].Visible = modelForDisplay.TTS_Printing_Color_Dark == 0 ? false : true;
                this.dgButton.Columns[43].Visible = modelForDisplay.TTS_Wrong_Orietation == 0 ? false : true;
                this.dgButton.Columns[44].Visible = modelForDisplay.TTS_Other == 0 ? false : true;

                this.dgButton.Columns[47].Visible = modelForDisplay.Vendor_Raw_Part_Scratch == 0 ? false : true;
                this.dgButton.Columns[48].Visible = modelForDisplay.Vendor_Oil_Stain == 0 ? false : true;
                this.dgButton.Columns[49].Visible = modelForDisplay.Vendor_Dented == 0 ? false : true;
                this.dgButton.Columns[50].Visible = modelForDisplay.Vendor_Dust == 0 ? false : true;
                this.dgButton.Columns[51].Visible = modelForDisplay.Vendor_Flyout == 0 ? false : true;
                this.dgButton.Columns[52].Visible = modelForDisplay.Vendor_Over_Spray == 0 ? false : true;
                this.dgButton.Columns[53].Visible = modelForDisplay.Vendor_Weld_line == 0 ? false : true;
                this.dgButton.Columns[54].Visible = modelForDisplay.Vendor_Crack == 0 ? false : true;
                this.dgButton.Columns[55].Visible = modelForDisplay.Vendor_Gas_mark == 0 ? false : true;
                this.dgButton.Columns[56].Visible = modelForDisplay.Vendor_Sink_mark == 0 ? false : true;
                this.dgButton.Columns[57].Visible = modelForDisplay.Vendor_Bubble == 0 ? false : true;
                this.dgButton.Columns[58].Visible = modelForDisplay.Vendor_White_dot == 0 ? false : true;
                this.dgButton.Columns[59].Visible = modelForDisplay.Vendor_Black_dot == 0 ? false : true;
                this.dgButton.Columns[60].Visible = modelForDisplay.Vendor_Red_Dot == 0 ? false : true;
                this.dgButton.Columns[61].Visible = modelForDisplay.Vendor_Poor_Gate_Cut == 0 ? false : true;
                this.dgButton.Columns[62].Visible = modelForDisplay.Vendor_High_Gate == 0 ? false : true;
                this.dgButton.Columns[63].Visible = modelForDisplay.Vendor_White_Mark == 0 ? false : true;
                this.dgButton.Columns[64].Visible = modelForDisplay.Vendor_Drag_mark == 0 ? false : true;
                this.dgButton.Columns[65].Visible = modelForDisplay.Vendor_Foreigh_Material == 0 ? false : true;
                this.dgButton.Columns[66].Visible = modelForDisplay.Vendor_Double_Claim == 0 ? false : true;
                this.dgButton.Columns[67].Visible = modelForDisplay.Vendor_Short_mould == 0 ? false : true;
                this.dgButton.Columns[68].Visible = modelForDisplay.Vendor_Flashing == 0 ? false : true;
                this.dgButton.Columns[69].Visible = modelForDisplay.Vendor_Pink_Mark == 0 ? false : true;
                this.dgButton.Columns[70].Visible = modelForDisplay.Vendor_Deform == 0 ? false : true;
                this.dgButton.Columns[71].Visible = modelForDisplay.Vendor_Damage == 0 ? false : true;
                this.dgButton.Columns[72].Visible = modelForDisplay.Vendor_Mould_Dirt == 0 ? false : true;
                this.dgButton.Columns[73].Visible = modelForDisplay.Vendor_Yellowish == 0 ? false : true;
                this.dgButton.Columns[74].Visible = modelForDisplay.Vendor_Oil_Mark == 0 ? false : true;
                this.dgButton.Columns[75].Visible = modelForDisplay.Vendor_Printing_Mark == 0 ? false : true;
                this.dgButton.Columns[76].Visible = modelForDisplay.Vendor_Printing_Uneven == 0 ? false : true;
                this.dgButton.Columns[77].Visible = modelForDisplay.Vendor_Printing_Color_Dark == 0 ? false : true;
                this.dgButton.Columns[78].Visible = modelForDisplay.Vendor_Wrong_Orietation == 0 ? false : true;
                this.dgButton.Columns[79].Visible = modelForDisplay.Vendor_Other == 0 ? false : true;

                this.dgButton.Columns[83].Visible = modelForDisplay.Paint_Particle == 0 ? false : true;
                this.dgButton.Columns[84].Visible = modelForDisplay.Paint_Fibre == 0 ? false : true;
                this.dgButton.Columns[85].Visible = modelForDisplay.Paint_Many_particle == 0 ? false : true;
                this.dgButton.Columns[86].Visible = modelForDisplay.Paint_Stain_mark == 0 ? false : true;
                this.dgButton.Columns[87].Visible = modelForDisplay.Paint_Uneven_paint == 0 ? false : true;
                this.dgButton.Columns[88].Visible = modelForDisplay.Paint_Under_coat_uneven_paint == 0 ? false : true;
                this.dgButton.Columns[89].Visible = modelForDisplay.Paint_Under_spray == 0 ? false : true;
                this.dgButton.Columns[90].Visible = modelForDisplay.Paint_White_dot == 0 ? false : true;
                this.dgButton.Columns[91].Visible = modelForDisplay.Paint_Silver_dot == 0 ? false : true;
                this.dgButton.Columns[92].Visible = modelForDisplay.Paint_Dust == 0 ? false : true;
                this.dgButton.Columns[93].Visible = modelForDisplay.Paint_Paint_crack == 0 ? false : true;
                this.dgButton.Columns[94].Visible = modelForDisplay.Paint_Bubble == 0 ? false : true;
                this.dgButton.Columns[95].Visible = modelForDisplay.Paint_Scratch == 0 ? false : true;
                this.dgButton.Columns[96].Visible = modelForDisplay.Paint_Abrasion_Mark == 0 ? false : true;
                this.dgButton.Columns[97].Visible = modelForDisplay.Paint_Paint_Dripping == 0 ? false : true;
                this.dgButton.Columns[98].Visible = modelForDisplay.Paint_Rough_Surface == 0 ? false : true;
                this.dgButton.Columns[99].Visible = modelForDisplay.Paint_Shinning == 0 ? false : true;
                this.dgButton.Columns[100].Visible = modelForDisplay.Paint_Matt == 0 ? false : true;
                this.dgButton.Columns[101].Visible = modelForDisplay.Paint_Paint_Pin_Hole == 0 ? false : true;
                this.dgButton.Columns[102].Visible = modelForDisplay.Paint_Light_Leakage == 0 ? false : true;
                this.dgButton.Columns[103].Visible = modelForDisplay.Paint_White_Mark == 0 ? false : true;
                this.dgButton.Columns[104].Visible = modelForDisplay.Paint_Dented == 0 ? false : true;
                this.dgButton.Columns[105].Visible = modelForDisplay.Paint_Other == 0 ? false : true;
                this.dgButton.Columns[106].Visible = modelForDisplay.Paint_Particle_for_laser_setup == 0 ? false : true;
                this.dgButton.Columns[107].Visible = modelForDisplay.Paint_Buyoff == 0 ? false : true;
                this.dgButton.Columns[108].Visible = modelForDisplay.Paint_Shortage == 0 ? false : true;

                this.dgButton.Columns[124].Visible = modelForDisplay.Laser_Black_Mark == 0 ? false : true;
                this.dgButton.Columns[125].Visible = modelForDisplay.Laser_Black_Dot == 0 ? false : true;
                this.dgButton.Columns[126].Visible = modelForDisplay.Laser_Graphic_Shift_check_by_PQC == 0 ? false : true;
                this.dgButton.Columns[127].Visible = modelForDisplay.Laser_Graphic_Shift_check_by_MC == 0 ? false : true;
                this.dgButton.Columns[128].Visible = modelForDisplay.Laser_Scratch == 0 ? false : true;
                this.dgButton.Columns[129].Visible = modelForDisplay.Laser_Jagged == 0 ? false : true;
                this.dgButton.Columns[130].Visible = modelForDisplay.Laser_Laser_Bubble == 0 ? false : true;
                this.dgButton.Columns[131].Visible = modelForDisplay.Laser_double_outer_line == 0 ? false : true;
                this.dgButton.Columns[132].Visible = modelForDisplay.Laser_Pin_hold == 0 ? false : true;
                this.dgButton.Columns[133].Visible = modelForDisplay.Laser_Poor_Laser == 0 ? false : true;
                this.dgButton.Columns[134].Visible = modelForDisplay.Laser_Burm_Mark == 0 ? false : true;
                this.dgButton.Columns[135].Visible = modelForDisplay.Laser_Stain_Mark == 0 ? false : true;
                this.dgButton.Columns[136].Visible = modelForDisplay.Laser_Graphic_Small == 0 ? false : true;
                this.dgButton.Columns[137].Visible = modelForDisplay.Laser_Double_Laser == 0 ? false : true;
                this.dgButton.Columns[138].Visible = modelForDisplay.Laser_Color_Yellow == 0 ? false : true;
                this.dgButton.Columns[139].Visible = modelForDisplay.Laser_Crack == 0 ? false : true;
                this.dgButton.Columns[140].Visible = modelForDisplay.Laser_Smoke == 0 ? false : true;
                this.dgButton.Columns[141].Visible = modelForDisplay.Laser_Wrong_Orientation == 0 ? false : true;
                this.dgButton.Columns[142].Visible = modelForDisplay.Laser_Dented == 0 ? false : true;
                this.dgButton.Columns[143].Visible = modelForDisplay.Laser_Other == 0 ? false : true;
                this.dgButton.Columns[144].Visible = modelForDisplay.Laser_Buyoff == 0 ? false : true;
                this.dgButton.Columns[145].Visible = modelForDisplay.Laser_Setup == 0 ? false : true;

                this.dgButton.Columns[151].Visible = modelForDisplay.PQC_Scratch == 0 ? false : true;
                this.dgButton.Columns[152].Visible = modelForDisplay.Over_Spray == 0 ? false : true;
                this.dgButton.Columns[153].Visible = modelForDisplay.Bubble == 0 ? false : true;
                this.dgButton.Columns[154].Visible = modelForDisplay.Oil_Stain == 0 ? false : true;
                this.dgButton.Columns[155].Visible = modelForDisplay.Drag_Mark == 0 ? false : true;
                this.dgButton.Columns[156].Visible = modelForDisplay.Light_Leakage == 0 ? false : true;
                this.dgButton.Columns[157].Visible = modelForDisplay.Light_Bubble == 0 ? false : true;
                this.dgButton.Columns[158].Visible = modelForDisplay.White_Dot_in_Material == 0 ? false : true;
                this.dgButton.Columns[159].Visible = modelForDisplay.Other == 0 ? false : true;
                #endregion


            }
            catch (Exception ee)
            {
                DBHelp.Reports.LogFile.Log("ButtonTotalReport_Debug", "Display   error : " + ee.ToString());
                Common.CommFunctions.ShowMessage(this.Page, "Warning!  catch exception: " + ee.ToString());
                return;
            }
           
        }
        

    }
}