using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;


namespace DashboardTTS.Webform.Reports
{
    public partial class BuyOffReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {

                    string sJobNumber = Request.QueryString["jobNumber"] == null ? "" : Request.QueryString["jobNumber"].ToString();

                    if (sJobNumber != "")
                    {
                        this.txtJobnumber.Text = sJobNumber;

                        ShowBaseInfo(sJobNumber);

                        ShowPQCInspection(sJobNumber, StaticRes.Global.Department.Laser);
                        ShowPQCInspection(sJobNumber, StaticRes.Global.Department.Painting);
                        ShowPQCInspection(sJobNumber, StaticRes.Global.Department.Moulding);
                        ShowPQCInspection(sJobNumber, "Others");

                        Display();
                    }
                   
                }
            }
            catch (Exception ee)
            {
                DBHelp.Reports.LogFile.Log("BuyOffReport", "Page_Load Exception: " + ee.ToString());
            }
        }
        

        private void ShowLaserInfo(DataRow dr)
        {

            this.lbMachineID.Text = dr["MACHINE_ID"].ToString();
            this.lbLotNo.Text = dr["LotNo"].ToString();
            this.lbPartNo.Text = dr["PART_NO"].ToString();
            this.lbCurrent.Text = dr["CurrentPower"].ToString() == "" ? "" : dr["CurrentPower"].ToString() + "%";
            this.lbMCOperator.Text = dr["MC_OPERATOR"].ToString();
            this.lbBuyoffBy.Text = dr["BUYOFF_BY"].ToString();
            this.lbApprovedBy.Text = dr["APPROVED_BY"].ToString();
            this.lbCheckBy.Text = dr["CHECK_BY"].ToString();
            this.lblLotQty.Text = dr["lotQty"].ToString();


            #region Check box
            string cbResult = "";

            cbResult = dr["BLACK_MARK_1ST"].ToString();
            if (cbResult == "OK")
            {
                this.cb_Black_Mark_OK.Checked = true;
                this.cb_Black_Mark_OK.BackColor = StaticRes.InspectionResColor.OK;
            }
            else if (cbResult == "NG")
            {
                this.cb_Black_Mark_NG.Checked = true;
                this.cb_Black_Mark_NG.BackColor = StaticRes.InspectionResColor.NG;
            }

            cbResult = dr["BLACK_DOT_1ST"].ToString();
            if (cbResult == "OK")
            {
                this.cb_Black_Dot_OK.Checked = true;
                this.cb_Black_Dot_OK.BackColor = StaticRes.InspectionResColor.OK;
            }
            else if (cbResult == "NG")
            {
                this.cb_Black_Dot_NG.Checked = true;
                this.cb_Black_Dot_NG.BackColor = StaticRes.InspectionResColor.NG;
            }

            cbResult = dr["PIN_HOLE_1ST"].ToString();
            if (cbResult == "OK")
            {
                this.cb_Pin_Hole_OK.Checked = true;
                this.cb_Pin_Hole_OK.BackColor = StaticRes.InspectionResColor.OK;
            }
            else if (cbResult == "NG")
            {
                this.cb_Pin_Hole_NG.Checked = true;
                this.cb_Pin_Hole_NG.BackColor = StaticRes.InspectionResColor.NG;
            }

            cbResult = dr["JAGGED_1ST"].ToString();
            if (cbResult == "OK")
            {
                this.cb_Jagged_OK.Checked = true;
                this.cb_Jagged_OK.BackColor = StaticRes.InspectionResColor.OK;
            }
            else if (cbResult == "NG")
            {
                this.cb_Jagged_NG.Checked = true;
                this.cb_Jagged_NG.BackColor = StaticRes.InspectionResColor.NG;
            }

            cbResult = dr["CHECK_GULED_1ST"].ToString();
            if (cbResult == "OK")
            {
                this.cb_Check_Guled_OK.Checked = true;
                this.cb_Check_Guled_OK.BackColor = StaticRes.InspectionResColor.OK;
            }
            else if (cbResult == "NG")
            {
                this.cb_Check_Guled_NG.Checked = true;
                this.cb_Check_Guled_NG.BackColor = StaticRes.InspectionResColor.NG;
            }

            cbResult = dr["NAVITAS_1ST"].ToString();
            if (cbResult == "OK")
            {
                this.cb_Navitas_OK.Checked = true;
                this.cb_Navitas_OK.BackColor = StaticRes.InspectionResColor.OK;
            }
            else if (cbResult == "NG")
            {
                this.cb_Navitas_NG.Checked = true;
                this.cb_Navitas_NG.BackColor = StaticRes.InspectionResColor.NG;
            }

            cbResult = dr["SMART_SCOPE_1ST"].ToString();
            if (cbResult == "OK")
            {
                this.cb_Smart_Scope_OK.Checked = true;
                this.cb_Smart_Scope_OK.BackColor = StaticRes.InspectionResColor.OK;
            }
            else if (cbResult == "NG")
            {
                this.cb_Smart_Scope_NG.Checked = true;
                this.cb_Smart_Scope_NG.BackColor = StaticRes.InspectionResColor.NG;
            }




            cbResult = dr["BLACK_MARK_2ND"].ToString();
            if (cbResult == "OK")
            {
                this.cb_Black_Mark_OK1.Checked = true;
                this.cb_Black_Mark_OK1.BackColor = StaticRes.InspectionResColor.OK;
            }
            else if (cbResult == "NG")
            {
                this.cb_Black_Mark_NG1.Checked = true;
                this.cb_Black_Mark_NG1.BackColor = StaticRes.InspectionResColor.NG;
            }

            cbResult = dr["BLACK_DOT_2ND"].ToString();
            if (cbResult == "OK")
            {
                this.cb_Black_Dot_OK1.Checked = true;
                this.cb_Black_Dot_OK1.BackColor = StaticRes.InspectionResColor.OK;
            }
            else if (cbResult == "NG")
            {
                this.cb_Black_Dot_NG1.Checked = true;
                this.cb_Black_Dot_NG1.BackColor = StaticRes.InspectionResColor.NG;
            }

            cbResult = dr["PIN_HOLE_2ND"].ToString();
            if (cbResult == "OK")
            {
                this.cb_Pin_Hole_OK1.Checked = true;
                this.cb_Pin_Hole_OK1.BackColor = StaticRes.InspectionResColor.OK;
            }
            else if (cbResult == "NG")
            {
                this.cb_Pin_Hole_NG1.Checked = true;
                this.cb_Pin_Hole_NG1.BackColor = StaticRes.InspectionResColor.NG;
            }

            cbResult = dr["JAGGED_2ND"].ToString();
            if (cbResult == "OK")
            {
                this.cb_Jagged_OK1.Checked = true;
                this.cb_Jagged_OK1.BackColor = StaticRes.InspectionResColor.OK;
            }
            else if (cbResult == "NG")
            {
                this.cb_Jagged_NG1.Checked = true;
                this.cb_Jagged_NG1.BackColor = StaticRes.InspectionResColor.NG;
            }

            cbResult = dr["CHECK_GULED_2ND"].ToString();
            if (cbResult == "OK")
            {
                this.cb_Check_Guled_OK1.Checked = true;
                this.cb_Check_Guled_OK1.BackColor = StaticRes.InspectionResColor.OK;
            }
            else if (cbResult == "NG")
            {
                this.cb_Check_Guled_NG1.Checked = true;
                this.cb_Check_Guled_NG1.BackColor = StaticRes.InspectionResColor.NG;



            }

            cbResult = dr["NAVITAS_2ND"].ToString();
            if (cbResult == "OK")
            {
                this.cb_Navitas_OK1.Checked = true;
                this.cb_Navitas_OK1.BackColor = StaticRes.InspectionResColor.OK;
            }
            else if (cbResult == "NG")
            {
                this.cb_Navitas_NG1.Checked = true;
                this.cb_Navitas_NG1.BackColor = StaticRes.InspectionResColor.NG;
            }

            cbResult = dr["SMART_SCOPE_2ND"].ToString();
            if (cbResult == "OK")
            {
                this.cb_Smart_Scope_OK1.Checked = true;
                this.cb_Smart_Scope_OK1.BackColor = StaticRes.InspectionResColor.OK;
            }
            else if (cbResult == "NG")
            {
                this.cb_Smart_Scope_NG1.Checked = true;
                this.cb_Smart_Scope_NG1.BackColor = StaticRes.InspectionResColor.NG;
            }




            cbResult = dr["BLACK_MARK_IN"].ToString();
            if (cbResult == "OK")
            {
                this.cb_Black_Mark_OK2.Checked = true;
                this.cb_Black_Mark_OK2.BackColor = StaticRes.InspectionResColor.OK;
            }
            else if (cbResult == "NG")
            {
                this.cb_Black_Mark_NG2.Checked = true;
                this.cb_Black_Mark_NG2.BackColor = StaticRes.InspectionResColor.NG;
            }

            cbResult = dr["BLACK_DOT_IN"].ToString();
            if (cbResult == "OK")
            {
                this.cb_Black_Dot_OK2.Checked = true;
                this.cb_Black_Dot_OK2.BackColor = StaticRes.InspectionResColor.OK;
            }
            else if (cbResult == "NG")
            {
                this.cb_Black_Dot_NG2.Checked = true;
                this.cb_Black_Dot_NG2.BackColor = StaticRes.InspectionResColor.NG;
            }

            cbResult = dr["PIN_HOLE_IN"].ToString();
            if (cbResult == "OK")
            {
                this.cb_Pin_Hole_OK2.Checked = true;
                this.cb_Pin_Hole_OK2.BackColor = StaticRes.InspectionResColor.OK;
            }
            else if (cbResult == "NG")
            {
                this.cb_Pin_Hole_NG2.Checked = true;
                this.cb_Pin_Hole_NG2.BackColor = StaticRes.InspectionResColor.NG;
            }

            cbResult = dr["JAGGED_IN"].ToString();
            if (cbResult == "OK")
            {
                this.cb_Jagged_OK2.Checked = true;
                this.cb_Jagged_OK2.BackColor = StaticRes.InspectionResColor.OK;
            }
            else if (cbResult == "NG")
            {
                this.cb_Jagged_NG2.Checked = true;
                this.cb_Jagged_NG2.BackColor = StaticRes.InspectionResColor.NG;
            }

            cbResult = dr["CHECK_GULED_IN"].ToString();
            if (cbResult == "OK")
            {
                this.cb_Check_Guled_OK2.Checked = true;
                this.cb_Check_Guled_OK2.BackColor = StaticRes.InspectionResColor.OK;
            }
            else if (cbResult == "NG")
            {
                this.cb_Check_Guled_NG2.Checked = true;
                this.cb_Check_Guled_NG2.BackColor = StaticRes.InspectionResColor.NG;
            }

            cbResult = dr["NAVITAS_IN"].ToString();
            if (cbResult == "OK")
            {
                this.cb_Navitas_OK2.Checked = true;
                this.cb_Navitas_OK2.BackColor = StaticRes.InspectionResColor.OK;
            }
            else if (cbResult == "NG")
            {
                this.cb_Navitas_NG2.Checked = true;
                this.cb_Navitas_NG2.BackColor = StaticRes.InspectionResColor.NG;
            }

            cbResult = dr["SMART_SCOPE_IN"].ToString();
            if (cbResult == "OK")
            {
                this.cb_Smart_Scope_OK2.Checked = true;
                this.cb_Smart_Scope_OK2.BackColor = StaticRes.InspectionResColor.OK;
            }
            else if (cbResult == "NG")
            {
                this.cb_Smart_Scope_NG2.Checked = true;
                this.cb_Smart_Scope_NG2.BackColor = StaticRes.InspectionResColor.NG;
            }


            #endregion

        }

        private void ShowWIPInfo(DataRow dr)
        {
            this.lbLotNo.Text = dr["LotNo"].ToString();
            this.lblLotQty.Text = dr["inQuantity"].ToString();
            this.lbPartNo.Text = dr["partNumber"].ToString();


            #region Reset Laser Info

            this.lbCurrent.Text = "";
            this.lbMCOperator.Text = "";
            this.lbApprovedBy.Text = "";
            this.lbCheckBy.Text = "";
            this.lbBuyoffBy.Text = "";


            this.cb_Black_Mark_OK.Checked = false;
            this.cb_Black_Mark_OK.BackColor = StaticRes.InspectionResColor.Empty;
            this.cb_Black_Mark_NG.Checked = false;
            this.cb_Black_Mark_NG.BackColor = StaticRes.InspectionResColor.Empty;


            this.cb_Black_Dot_OK.Checked = false;
            this.cb_Black_Dot_OK.BackColor = StaticRes.InspectionResColor.Empty;
            this.cb_Black_Dot_NG.Checked = false;
            this.cb_Black_Dot_NG.BackColor = StaticRes.InspectionResColor.Empty;

            

            this.cb_Pin_Hole_OK.Checked = false;
            this.cb_Pin_Hole_OK.BackColor = StaticRes.InspectionResColor.Empty;
            this.cb_Pin_Hole_NG.Checked = false;
            this.cb_Pin_Hole_NG.BackColor = StaticRes.InspectionResColor.Empty;


            this.cb_Jagged_OK.Checked = false;
            this.cb_Jagged_OK.BackColor = StaticRes.InspectionResColor.Empty;
            this.cb_Jagged_NG.Checked = false;
            this.cb_Jagged_NG.BackColor = StaticRes.InspectionResColor.Empty;

            this.cb_Check_Guled_OK.Checked = false;
            this.cb_Check_Guled_OK.BackColor = StaticRes.InspectionResColor.Empty;
            this.cb_Check_Guled_NG.Checked = false;
            this.cb_Check_Guled_NG.BackColor = StaticRes.InspectionResColor.Empty;





            this.cb_Navitas_OK.Checked = false;
            this.cb_Navitas_OK.BackColor = StaticRes.InspectionResColor.Empty;
            this.cb_Navitas_NG.Checked = false;
            this.cb_Navitas_NG.BackColor = StaticRes.InspectionResColor.Empty;

            this.cb_Smart_Scope_OK.Checked = false;
            this.cb_Smart_Scope_OK.BackColor = StaticRes.InspectionResColor.Empty;
            this.cb_Smart_Scope_NG.Checked = false;
            this.cb_Smart_Scope_NG.BackColor = StaticRes.InspectionResColor.Empty;

            this.cb_Black_Mark_OK1.Checked = false;
            this.cb_Black_Mark_OK1.BackColor = StaticRes.InspectionResColor.Empty;
            this.cb_Black_Mark_NG1.Checked = false;
            this.cb_Black_Mark_NG1.BackColor = StaticRes.InspectionResColor.Empty;

            
            this.cb_Black_Dot_OK1.Checked = false;
            this.cb_Black_Dot_OK1.BackColor = StaticRes.InspectionResColor.Empty;
            this.cb_Black_Dot_NG1.Checked = false;
            this.cb_Black_Dot_NG1.BackColor = StaticRes.InspectionResColor.Empty;



            this.cb_Pin_Hole_OK1.Checked = false;
            this.cb_Pin_Hole_OK1.BackColor = StaticRes.InspectionResColor.Empty;
            this.cb_Pin_Hole_NG1.Checked = false;
            this.cb_Pin_Hole_NG1.BackColor = StaticRes.InspectionResColor.Empty;



            this.cb_Jagged_OK1.Checked = false;
            this.cb_Jagged_OK1.BackColor = StaticRes.InspectionResColor.Empty;
            this.cb_Jagged_NG1.Checked = false;
            this.cb_Jagged_NG1.BackColor = StaticRes.InspectionResColor.Empty;



            this.cb_Check_Guled_OK1.Checked = false;
            this.cb_Check_Guled_OK1.BackColor = StaticRes.InspectionResColor.Empty;
            this.cb_Check_Guled_NG1.Checked = false;
            this.cb_Check_Guled_NG1.BackColor = StaticRes.InspectionResColor.Empty;

            
            this.cb_Navitas_OK1.Checked = false;
            this.cb_Navitas_OK1.BackColor = StaticRes.InspectionResColor.Empty;
            this.cb_Navitas_NG1.Checked = false;
            this.cb_Navitas_NG1.BackColor = StaticRes.InspectionResColor.Empty;

            
            this.cb_Smart_Scope_OK1.Checked = false;
            this.cb_Smart_Scope_OK1.BackColor = StaticRes.InspectionResColor.Empty;
            this.cb_Smart_Scope_NG1.Checked = false;
            this.cb_Smart_Scope_NG1.BackColor = StaticRes.InspectionResColor.Empty;

            
            this.cb_Black_Mark_OK2.Checked = false;
            this.cb_Black_Mark_OK2.BackColor = StaticRes.InspectionResColor.Empty;
            this.cb_Black_Mark_NG2.Checked = false;
            this.cb_Black_Mark_NG2.BackColor = StaticRes.InspectionResColor.Empty;

            
            this.cb_Black_Dot_OK2.Checked = false;
            this.cb_Black_Dot_OK2.BackColor = StaticRes.InspectionResColor.Empty;
            this.cb_Black_Dot_NG2.Checked = false;
            this.cb_Black_Dot_NG2.BackColor = StaticRes.InspectionResColor.Empty;
            

            this.cb_Pin_Hole_OK2.Checked = false;
            this.cb_Pin_Hole_OK2.BackColor = StaticRes.InspectionResColor.Empty;
            this.cb_Pin_Hole_NG2.Checked = false;
            this.cb_Pin_Hole_NG2.BackColor = StaticRes.InspectionResColor.Empty;
            

            this.cb_Jagged_OK2.Checked = false;
            this.cb_Jagged_OK2.BackColor = StaticRes.InspectionResColor.Empty;
            this.cb_Jagged_NG2.Checked = false;
            this.cb_Jagged_NG2.BackColor = StaticRes.InspectionResColor.Empty;
            

            this.cb_Check_Guled_OK2.Checked = false;
            this.cb_Check_Guled_OK2.BackColor = StaticRes.InspectionResColor.Empty;
            this.cb_Check_Guled_NG2.Checked = false;
            this.cb_Check_Guled_NG2.BackColor = StaticRes.InspectionResColor.Empty;
            

            this.cb_Navitas_OK2.Checked = false;
            this.cb_Navitas_OK2.BackColor = StaticRes.InspectionResColor.Empty;
            this.cb_Navitas_NG2.Checked = false;
            this.cb_Navitas_NG2.BackColor = StaticRes.InspectionResColor.Empty;
            

            this.cb_Smart_Scope_OK2.Checked = false;
            this.cb_Smart_Scope_OK2.BackColor = StaticRes.InspectionResColor.Empty;
            this.cb_Smart_Scope_NG2.Checked = false;
            this.cb_Smart_Scope_NG2.BackColor = StaticRes.InspectionResColor.Empty;
            #endregion
        }

        private void ShowBaseInfo(string Jobnumber)
        {

            Common.Class.BLL.LMMSBUYOFFLIST_BLL bll = new Common.Class.BLL.LMMSBUYOFFLIST_BLL();
            DataTable dt = bll.GetBuyofflist(Jobnumber, "", "", "", "", "", null, null);

            if (dt == null || dt.Rows.Count == 0)
            {
                Common.Class.BLL.PaintingDeliveryHis_BLL paintBLL = new Common.Class.BLL.PaintingDeliveryHis_BLL();
                DataTable dtPaint = paintBLL.GetList(DateTime.Now.AddDays(-120), DateTime.Now, Jobnumber, "", "", "");
                if (dtPaint != null &&dtPaint.Rows.Count != 0)
                {
                    ShowWIPInfo(dtPaint.Rows[0]);
                }
            }
            else
            {
                ShowLaserInfo(dt.Rows[0]);
            }



            Common.Class.BLL.LMMSVisionMachineSettingHis_BLL settingBLL = new Common.Class.BLL.LMMSVisionMachineSettingHis_BLL();
            Common.Class.Model.LMMSVisionMachineSettingHis_Model model = settingBLL.GetModel(Jobnumber);
            if (model != null)
            {
                this.lbRate.Text = model.rate;
                this.lbFrequency.Text = model.frequency;
                this.lbPower.Text = model.power;
                this.lbRepeat.Text = model.repeat;
            }
         

        }



        private void ShowPQCInspection(string Jobnumber,string Department)
        {
            try
            {
                Common.Class.BLL.PQCQaViDefectTracking_BLL bll = new Common.Class.BLL.PQCQaViDefectTracking_BLL();
                DataTable dt = bll.getBuyoffReport(Department, Jobnumber);
                

                if (Department == StaticRes.Global.Department.Laser)
                {
                    #region laser
                    if (dt == null || dt.Rows.Count == 0)
                    {
                        this.dgPQCLaserInspection.Visible = false;
                    }
                    else
                    {
                        dgPQCLaserInspection.AutoGenerateColumns = false;

                        for (int i = 0; i < dt.Columns.Count; i++)
                        {
                            BoundColumn bdColumn = new BoundColumn();
                            bdColumn.DataField = dt.Columns[i].ColumnName;

                            

                            if (dt.Columns[i].Caption == "Graphic Shift check by PQC"  )
                            {
                                bdColumn.HeaderText = "G/S<br/>By PQC";
                            }
                            else if (dt.Columns[i].Caption == "Graphic Shift check by M/C")
                            {
                                bdColumn.HeaderText = "G/S<br/>By MC";
                            }
                            else
                            {
                                bdColumn.HeaderText = dt.Columns[i].Caption.Replace(" ", "<br/>");
                            }
                           

                            this.dgPQCLaserInspection.Columns.Add(bdColumn);
                        }


                        this.dgPQCLaserInspection.Visible = true;
                        this.dgPQCLaserInspection.DataSource = dt.DefaultView;
                        this.dgPQCLaserInspection.DataBind();
                    }
                    #endregion
                }
                else if (Department == StaticRes.Global.Department.Moulding)
                {
                    #region Moulding
                    if (dt == null || dt.Rows.Count == 0)
                    {
                        this.dgPQCMouldingInspection.Visible = false;
                        this.dgPQCMouldingInspection02.Visible = false;
                    }
                    else
                    {
                        dgPQCMouldingInspection.AutoGenerateColumns = true;
                        dgPQCMouldingInspection02.AutoGenerateColumns = true;


                        DataTable dt2 = dt.Copy();

                        for (int i = 0; i < 17; i++)
                        {
                            dt.Columns.RemoveAt(17);
                        }

                        for (int i = 0; i < 17; i++)
                        {
                            dt2.Columns.RemoveAt(0);
                        }
                        

                        this.dgPQCMouldingInspection.Visible = true;
                        this.dgPQCMouldingInspection.DataSource = dt.DefaultView;
                        this.dgPQCMouldingInspection.DataBind();

                        this.dgPQCMouldingInspection02.Visible = true;
                        this.dgPQCMouldingInspection02.DataSource = dt2.DefaultView;
                        this.dgPQCMouldingInspection02.DataBind();
                    }
                    #endregion
                }
                else if (Department == StaticRes.Global.Department.Painting)
                {
                    #region
                    if (dt == null || dt.Rows.Count == 0)
                    {
                        this.dgPQCPaintingInspection.Visible = false;
                    }
                    else
                    {

                        dgPQCPaintingInspection.AutoGenerateColumns = false;

                        for (int i = 0; i < dt.Columns.Count; i++)
                        {
                            BoundColumn bdColumn = new BoundColumn();
                            bdColumn.DataField = dt.Columns[i].ColumnName;
                            bdColumn.HeaderText = dt.Columns[i].Caption.Replace(" ", "<br/>");

                            this.dgPQCPaintingInspection.Columns.Add(bdColumn);
                        }

                        this.dgPQCPaintingInspection.DataSource = dt;
                        this.dgPQCPaintingInspection.DataBind();


                        this.dgPQCPaintingInspection.Visible = true;
                        this.dgPQCPaintingInspection.DataSource = dt.DefaultView;
                        this.dgPQCPaintingInspection.DataBind();
                    }
                    #endregion
                }
                else
                {
                    #region
                    if (dt == null || dt.Rows.Count == 0)
                    {
                        this.dgPQCOthersInspection.Visible = false;
                    }
                    else
                    {

                        dgPQCOthersInspection.AutoGenerateColumns = false;

                        for (int i = 0; i < dt.Columns.Count; i++)
                        {
                            BoundColumn bdColumn = new BoundColumn();
                            bdColumn.DataField = dt.Columns[i].ColumnName;
                            bdColumn.HeaderText = dt.Columns[i].Caption;

                            this.dgPQCOthersInspection.Columns.Add(bdColumn);
                        }

                        this.dgPQCOthersInspection.DataSource = dt;
                        this.dgPQCOthersInspection.DataBind();


                        this.dgPQCOthersInspection.Visible = true;
                        this.dgPQCOthersInspection.DataSource = dt.DefaultView;
                        this.dgPQCOthersInspection.DataBind();
                    }
                    #endregion
                }
            }
            catch (Exception ee)
            {
                DBHelp.Reports.LogFile.Log("getPQCInspection", "getPQCLaserInspection Exception: " + ee.ToString());
            }
        }

       

        protected void txtDate_TextChanged(object sender, EventArgs e)
        {
            DateTime dDate = DateTime.Parse(txtDate.Text);

            this.ddlJobList.Items.Clear();


            Common.Class.BLL.PQCQaViTracking_BLL bll = new Common.Class.BLL.PQCQaViTracking_BLL();
            DataTable dt = bll.GetList("", "", dDate.Date, dDate.Date.AddDays(1), "", "", "", "","");

           

            if (dt != null && dt.Rows.Count != 0)
            {
                dt = dt.Select("", "JobID asc").CopyToDataTable();

                ListItem li = new ListItem("", "");
                this.ddlJobList.Items.Add(li);

                foreach (DataRow dr in dt.Rows)
                {
                    string sJobnumber = dr["JobID"].ToString().ToUpper();
                    
                    li = new ListItem(sJobnumber, sJobnumber);

                    if (sJobnumber == "" || this.ddlJobList.Items.Contains(li))
                    {
                        continue;
                    }
                    else
                    {
                        this.ddlJobList.Items.Add(li);
                    }
                }
            }
        }

        protected void ddlJobList_SelectedIndexChanged(object sender, EventArgs e)
        {
            string jobNumber = this.ddlJobList.SelectedValue;

            ShowBaseInfo(jobNumber);
            ShowPQCInspection(jobNumber, StaticRes.Global.Department.Laser);
            ShowPQCInspection(jobNumber, StaticRes.Global.Department.Painting);
            ShowPQCInspection(jobNumber, StaticRes.Global.Department.Moulding);
            ShowPQCInspection(jobNumber, "Others");

            Display();
        }


        void Display()
        {

            foreach (DataGridItem item in this.dgPQCMouldingInspection.Items)
            {
                for (int i = 0; i < item.Cells.Count; i++)
                {
                    string value = item.Cells[i].Text;

                    if (value == "0")
                    {
                        item.Cells[i].Text = "";
                    }
                    else if (Common.CommFunctions.isNumberic(value) && value != "0")
                    {
                        item.Cells[i].BackColor = System.Drawing.Color.Pink;
                    }
                }
            }


            foreach (DataGridItem item in this.dgPQCMouldingInspection02.Items)
            {
                for (int i = 0; i < item.Cells.Count; i++)
                {
                    string value = item.Cells[i].Text;

                    if (value == "0")
                    {
                        item.Cells[i].Text = "";
                    }
                    else if (Common.CommFunctions.isNumberic(value) && value != "0")
                    {
                        item.Cells[i].BackColor = System.Drawing.Color.Pink;
                    }
                }
            }



            foreach (DataGridItem item in this.dgPQCPaintingInspection.Items)
            {
                for (int i = 0; i < item.Cells.Count; i++)
                {
                    string value = item.Cells[i].Text;

                    if (value == "0")
                    {
                        item.Cells[i].Text = "";
                    }
                    else if (Common.CommFunctions.isNumberic(value) && value != "0")
                    {
                        item.Cells[i].BackColor = System.Drawing.Color.Pink;
                    }
                }
            }


            foreach (DataGridItem item in this.dgPQCLaserInspection.Items)
            {
                for (int i = 0; i < item.Cells.Count; i++)
                {
                    string value = item.Cells[i].Text;

                    if (value == "0")
                    {
                        item.Cells[i].Text = "";
                    }
                    else if (Common.CommFunctions.isNumberic(value) && value != "0")
                    {
                        item.Cells[i].BackColor = System.Drawing.Color.Pink;
                    }
                }
            }


            foreach (DataGridItem item in this.dgPQCOthersInspection.Items)
            {
                for (int i = 0; i < item.Cells.Count; i++)
                {
                    string value = item.Cells[i].Text;

                    if (value == "0")
                    {
                        item.Cells[i].Text = "";
                    }
                    else if (Common.CommFunctions.isNumberic(value) && value != "0")
                    {
                        item.Cells[i].BackColor = System.Drawing.Color.Pink;
                    }
                }
            }

        }
    }
}