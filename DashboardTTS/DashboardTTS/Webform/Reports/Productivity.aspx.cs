using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace DashboardTTS.Webform.Reports
{
    public partial class Productivity : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    this.lb_Header.Text = "Productivity Report";
                    DataTable dt = new DataTable();
                   
                    DateTime dDay = DateTime.Now.Date.AddDays(-1); 
                    this.infDchFrom.Value = dDay;
                    infDchFrom.CalendarLayout.SelectedDate = dDay;

                    getReport(dDay);

                }
            }
            catch (Exception ex)
            {
                DBHelp.Reports.LogFile.Log("Debug", "PageLoad Exception: " + ex.ToString());
            }
        }

      
        protected void btnGenerate_Click(object sender, EventArgs e)
        {
            DateTime dDay = infDchFrom.CalendarLayout.SelectedDate ;
            getReport(dDay);
        }






        public void getReport(DateTime dDay)
        {
            //===== Auto =====//

            #region laser
            try
            {
                Common.BLL.LMMSWatchLog_BLL bllLaser = new Common.BLL.LMMSWatchLog_BLL();
                DataTable dtLaserDayShift = bllLaser.getProductivityReportForLaser(dDay, StaticRes.Global.Shift.Day, StaticRes.Global.Department.Laser);
                DataTable dtLaserNightShift = bllLaser.getProductivityReportForLaser(dDay, StaticRes.Global.Shift.Night, StaticRes.Global.Department.Laser);

                this.dg_Laser_Day.DataSource = dtLaserDayShift.DefaultView;
                this.dg_Laser_Day.DataBind();
                this.dg_Laser_Night.DataSource = dtLaserNightShift.DefaultView;
                this.dg_Laser_Night.DataBind();


                MergeDataGridRow(dg_Laser_Day, dtLaserDayShift.Rows.Count, StaticRes.Global.Department.Laser);
                MergeDataGridRow(dg_Laser_Night, dtLaserNightShift.Rows.Count, StaticRes.Global.Department.Laser);

            }
            catch (Exception ex)
            {
                DBHelp.Reports.LogFile.Log("OverAll_Productivity", "getReport Exception for laser : " + ex.ToString());
            }
            #endregion


            #region moulding
            try
            {

                Common.Class.BLL.MouldingViHistory_BLL bllMoulding = new Common.Class.BLL.MouldingViHistory_BLL();
                DataTable dtMouldingDayShift = bllMoulding.getProductivityReportForMoulding(dDay, StaticRes.Global.Shift.Day, StaticRes.Global.Department.Moulding);
                DataTable dtMouldingNightShift = bllMoulding.getProductivityReportForMoulding(dDay, StaticRes.Global.Shift.Night, StaticRes.Global.Department.Moulding);

                this.dgMouldingDay.DataSource = dtMouldingDayShift.DefaultView;
                this.dgMouldingDay.DataBind();
                this.dgMouldingNight.DataSource = dtMouldingNightShift.DefaultView;
                this.dgMouldingNight.DataBind();

                MergeDataGridRow(dgMouldingDay, dtMouldingDayShift.Rows.Count, StaticRes.Global.Department.Moulding);
                MergeDataGridRow(dgMouldingNight, dtMouldingNightShift.Rows.Count, StaticRes.Global.Department.Moulding);

            }
            catch (Exception ex)
            {
                DBHelp.Reports.LogFile.Log("OverAll_Productivity", "getReport Exception for moulding : " + ex.ToString());
            }

            #endregion


            #region PQC auto generate
            try
            {

                Common.Class.BLL.PQCQaViTracking_BLL bllPQC = new Common.Class.BLL.PQCQaViTracking_BLL();
                DataTable dtPQCDayShift = bllPQC.getProductivityReportForPQC(dDay, StaticRes.Global.Shift.Day, StaticRes.Global.Department.PQC);
                DataTable dtPQCNightShift = bllPQC.getProductivityReportForPQC(dDay, StaticRes.Global.Shift.Night, StaticRes.Global.Department.PQC);

                this.dgPQCDay.DataSource = dtPQCDayShift.DefaultView;
                this.dgPQCDay.DataBind();
                this.dgPQCNight.DataSource = dtPQCNightShift.DefaultView;
                this.dgPQCNight.DataBind();

                MergeDataGridRow(dgPQCDay, dtPQCDayShift.Rows.Count, StaticRes.Global.Department.PQC);
                MergeDataGridRow(dgPQCNight, dtPQCNightShift.Rows.Count, StaticRes.Global.Department.PQC);

            }
            catch (Exception ex)
            {
                DBHelp.Reports.LogFile.Log("OverAll_Productivity", "getReport Exception for PQC : " + ex.ToString());
            }
            #endregion

            //===== Auto =====//





            //===== mannual update =====//

            #region PQC  manual key    no use
            //try
            //{

            //    Common.Class.BLL.PQCQaViTracking_BLL bllPQC = new Common.Class.BLL.PQCQaViTracking_BLL();
            //    DataTable dtPQCDayShift = bllPQC.getProductivityReportForPQC(dDay, StaticRes.Global.Shift.Day, StaticRes.Global.Department.PQC);
            //    this.dgPQCDay.DataSource = dtPQCDayShift.DefaultView;
            //    this.dgPQCDay.DataBind();


            //    DataTable dtPQCNightShift = bllPQC.getProductivityReportForPQC(dDay, StaticRes.Global.Shift.Night, StaticRes.Global.Department.PQC);
            //    this.dgPQCNight.DataSource = dtPQCNightShift.DefaultView;
            //    this.dgPQCNight.DataBind();



            //    Common.Class.BLL.TempProductivityData_BLL bllProd = new Common.Class.BLL.TempProductivityData_BLL();
            //    bool isUpdated = bllProd.IsUpdated(StaticRes.Global.Department.PQC, dDay);


            //    if (isUpdated)
            //    {
            //        this.btnEditPQC.Visible = true;
            //        this.btnUpdatePQC.Visible = false;
            //        this.lbMessagePQC.Visible = false;
            //        MergeDataGridRow(dgPQCDay, dtPQCDayShift.Rows.Count, StaticRes.Global.Department.PQC);
            //        MergeDataGridRow(dgPQCNight, dtPQCNightShift.Rows.Count, StaticRes.Global.Department.PQC);


            //        if (dtPQCNightShift.Select(" ProductType = 'Night Shift Total' ")[0]["TotalQty"].ToString() == "0")
            //        {
            //            this.dgPQCNight.Visible = false;
            //        }
            //    }
            //    else
            //    {
            //        this.btnEditPQC.Visible = false;
            //        this.btnUpdatePQC.Visible = true;


            //        this.lbMessagePQC.Text = "There is no data. Please update first!";
            //        this.lbMessagePQC.ForeColor = System.Drawing.Color.Red;
            //        this.lbMessagePQC.Visible = true;

            //        initEditStylePQC(this.dgPQCDay);
            //        initEditStylePQC(this.dgPQCNight);
            //    }

            //}
            //catch (Exception ex)
            //{
            //    DBHelp.Reports.LogFile.Log("OverAll_Productivity", "getReport Exception for PQC : " + ex.ToString());
            //}
            #endregion


            #region painting
            try
            {

                DataTable dtPaintingDay = getProductivityReportForPainting(dDay, StaticRes.Global.Shift.Day, StaticRes.Global.Department.Painting);


                this.dgPaintingDay.DataSource = dtPaintingDay.DefaultView;
                this.dgPaintingDay.DataBind();


                Common.Class.BLL.TempProductivityData_BLL bllProd = new Common.Class.BLL.TempProductivityData_BLL();
                bool isUpdated = bllProd.IsUpdated(StaticRes.Global.Department.Painting, dDay);
                

                if (isUpdated)
                {
                    this.btnEditPainting.Visible = true;
                    this.btnUpdatePainting.Visible = false;
                    this.lbMessagepainting.Visible = false;
                    MergeDataGridRow(dgPaintingDay, dtPaintingDay.Rows.Count, StaticRes.Global.Department.Painting);
                }
                else
                {
                    this.btnEditPainting.Visible = false;
                    this.btnUpdatePainting.Visible = true;


                    this.lbMessagepainting.Text = "There is no data. Please update first!";
                    this.lbMessagepainting.ForeColor = System.Drawing.Color.Red;
                    this.lbMessagepainting.Visible = true;

                    initEditStylePainting();

                }

            }
            catch (Exception ex)
            {
                DBHelp.Reports.LogFile.Log("OverAll_Productivity", "getReport Exception for Painting : " + ex.ToString());
            }
            #endregion
            
            
            #region Assembly
            try
            {
                
                DataTable dtAssyDay = getProductivityReportForAssy(dDay, StaticRes.Global.Shift.Day, StaticRes.Global.Department.Assembly);
                
                this.dgAssyDay.DataSource = dtAssyDay.DefaultView;
                this.dgAssyDay.DataBind();
                
                Common.Class.BLL.TempProductivityData_BLL bllProd = new Common.Class.BLL.TempProductivityData_BLL();
                bool isUpdated = bllProd.IsUpdated(StaticRes.Global.Department.Assembly, dDay);
                
                if (isUpdated)
                {
                    this.btnEditAssy.Visible = true;
                    this.btnUpdateAssy.Visible = false;
                    this.lbMessageAssy.Visible = false;
                    MergeDataGridRow(dgAssyDay, dtAssyDay.Rows.Count, StaticRes.Global.Department.Assembly);
                }
                else
                {
                    this.btnEditAssy.Visible = false;
                    this.btnUpdateAssy.Visible = true;


                    this.lbMessageAssy.Text = "There is no data. Please update first!";
                    this.lbMessageAssy.ForeColor = System.Drawing.Color.Red;
                    this.lbMessageAssy.Visible = true;

                    initEditStyleAssy();
                }
                
            }
            catch (Exception ex)
            {
                DBHelp.Reports.LogFile.Log("OverAll_Productivity", "getReport Exception for assembly : " + ex.ToString());
            }
            #endregion

            //===== mannual update =====//

        }



        private void MergeDataGridRow(DataGrid dg, int RowCount, string sDepartment)
        {
           
            for (int i = 0; i < RowCount; i++)
            {
              

                if (sDepartment == StaticRes.Global.Department.Laser)
                {
                    #region Laser
                    if (i == 0)
                    {
                        dg.Items[i].Cells[1].RowSpan = RowCount;
                        dg.Items[i].Cells[2].RowSpan = RowCount;
                        dg.Items[i].Cells[3].RowSpan = RowCount;
                        dg.Items[i].Cells[4].RowSpan = RowCount;
                        //dg.Items[i].Cells[10].RowSpan = RowCount;
                        dg.Items[i].Cells[11].RowSpan = RowCount;
                        dg.Items[i].Cells[12].RowSpan = RowCount;

                        #region  CSS for Cell
                        dg.Items[i].Cells[1].BackColor = System.Drawing.Color.White;
                        dg.Items[i].Cells[2].BackColor = System.Drawing.Color.White;
                        dg.Items[i].Cells[3].BackColor = System.Drawing.Color.White;
                        dg.Items[i].Cells[4].BackColor = System.Drawing.Color.White;
                        //dg.Items[i].Cells[10].BackColor = System.Drawing.Color.White;
                        dg.Items[i].Cells[11].BackColor = System.Drawing.Color.White;
                        dg.Items[i].Cells[12].BackColor = System.Drawing.Color.White;

                        dg.Items[i].Cells[1].BorderStyle = BorderStyle.Solid;
                        dg.Items[i].Cells[2].BorderStyle = BorderStyle.Solid;
                        dg.Items[i].Cells[3].BorderStyle = BorderStyle.Solid;
                        dg.Items[i].Cells[4].BorderStyle = BorderStyle.Solid;
                        //dg.Items[i].Cells[10].BorderStyle = BorderStyle.Solid;
                        dg.Items[i].Cells[11].BorderStyle = BorderStyle.Solid;
                        dg.Items[i].Cells[12].BorderStyle = BorderStyle.Solid;

                        dg.Items[i].Cells[1].BorderWidth = 1;
                        dg.Items[i].Cells[2].BorderWidth = 1;
                        dg.Items[i].Cells[3].BorderWidth = 1;
                        dg.Items[i].Cells[4].BorderWidth = 1;
                        //dg.Items[i].Cells[10].BorderWidth = 1;
                        dg.Items[i].Cells[11].BorderWidth = 1;
                        dg.Items[i].Cells[12].BorderWidth = 1;

                        dg.Items[i].Cells[1].BorderColor = System.Drawing.Color.LightGray;
                        dg.Items[i].Cells[2].BorderColor = System.Drawing.Color.LightGray;
                        dg.Items[i].Cells[3].BorderColor = System.Drawing.Color.LightGray;
                        dg.Items[i].Cells[4].BorderColor = System.Drawing.Color.LightGray;
                        //dg.Items[i].Cells[10].BorderColor = System.Drawing.Color.LightGray;
                        dg.Items[i].Cells[11].BorderColor = System.Drawing.Color.LightGray;
                        dg.Items[i].Cells[12].BorderColor = System.Drawing.Color.LightGray;
                        #endregion  CSS for Cell

                    }
                    else
                    {
                        dg.Items[i].Cells[1].Visible = false;
                        dg.Items[i].Cells[2].Visible = false;
                        dg.Items[i].Cells[3].Visible = false;
                        dg.Items[i].Cells[4].Visible = false;
                        //dg.Items[i].Cells[10].Visible = false;
                        dg.Items[i].Cells[11].Visible = false;
                        dg.Items[i].Cells[12].Visible = false;
                    }
                    #endregion
                }
                else if (sDepartment == StaticRes.Global.Department.Moulding)
                {
                    #region Moulding
                    if (i == 0)
                    {
                        dg.Items[i].Cells[1].RowSpan = RowCount;
                        dg.Items[i].Cells[2].RowSpan = RowCount;
                        dg.Items[i].Cells[3].RowSpan = RowCount;
                        dg.Items[i].Cells[4].RowSpan = RowCount;
                        //dg.Items[i].Cells[11].RowSpan = RowCount;
                        dg.Items[i].Cells[12].RowSpan = RowCount;

                        #region  CSS for Cell
                        dg.Items[i].Cells[1].BackColor = System.Drawing.Color.White;
                        dg.Items[i].Cells[2].BackColor = System.Drawing.Color.White;
                        dg.Items[i].Cells[3].BackColor = System.Drawing.Color.White;
                        dg.Items[i].Cells[4].BackColor = System.Drawing.Color.White;
                        //dg.Items[i].Cells[11].BackColor = System.Drawing.Color.White;
                        dg.Items[i].Cells[12].BackColor = System.Drawing.Color.White;

                        dg.Items[i].Cells[1].BorderStyle = BorderStyle.Solid;
                        dg.Items[i].Cells[2].BorderStyle = BorderStyle.Solid;
                        dg.Items[i].Cells[3].BorderStyle = BorderStyle.Solid;
                        dg.Items[i].Cells[4].BorderStyle = BorderStyle.Solid;
                        //dg.Items[i].Cells[11].BorderStyle = BorderStyle.Solid;
                        dg.Items[i].Cells[12].BorderStyle = BorderStyle.Solid;

                        dg.Items[i].Cells[1].BorderWidth = 1;
                        dg.Items[i].Cells[2].BorderWidth = 1;
                        dg.Items[i].Cells[3].BorderWidth = 1;
                        dg.Items[i].Cells[4].BorderWidth = 1;
                        //dg.Items[i].Cells[11].BorderWidth = 1;
                        dg.Items[i].Cells[12].BorderWidth = 1;

                        dg.Items[i].Cells[1].BorderColor = System.Drawing.Color.LightGray;
                        dg.Items[i].Cells[2].BorderColor = System.Drawing.Color.LightGray;
                        dg.Items[i].Cells[3].BorderColor = System.Drawing.Color.LightGray;
                        dg.Items[i].Cells[4].BorderColor = System.Drawing.Color.LightGray;
                        //dg.Items[i].Cells[11].BorderColor = System.Drawing.Color.LightGray;
                        dg.Items[i].Cells[12].BorderColor = System.Drawing.Color.LightGray;
                        #endregion  CSS for Cell

                    }
                    else
                    {
                        dg.Items[i].Cells[1].Visible = false;
                        dg.Items[i].Cells[2].Visible = false;
                        dg.Items[i].Cells[3].Visible = false;
                        dg.Items[i].Cells[4].Visible = false;
                        //dg.Items[i].Cells[11].Visible = false;
                        dg.Items[i].Cells[12].Visible = false;
                    }
                    #endregion
                }
                else if (sDepartment == StaticRes.Global.Department.PQC)
                {
                    #region PQC

                    dg.Columns[4].Visible = true;
                    dg.Columns[5].Visible = true;
                    dg.Columns[6].Visible = true;
                    dg.Columns[7].Visible = true;
                    dg.Columns[8].Visible = true;
                    dg.Columns[9].Visible = true;
                    dg.Columns[10].Visible = true;
                    //dg.Columns[11].Visible = true;
                    //dg.Columns[12].Visible = true;
                    //dg.Columns[13].Visible = true;
                    //dg.Columns[14].Visible = true;


                    //dg.Columns[15].Visible = false;
                    //dg.Columns[16].Visible = false;
                    //dg.Columns[17].Visible = false;
                    //dg.Columns[18].Visible = false;
                    //dg.Columns[19].Visible = false;
                    //dg.Columns[20].Visible = false;
                    //dg.Columns[21].Visible = false;
                    //dg.Columns[22].Visible = false;
                    //dg.Columns[23].Visible = false;
                    //dg.Columns[24].Visible = false;
                 

                    if (i == 0)
                    {
                        dg.Items[i].Cells[1].RowSpan = RowCount;
                        dg.Items[i].Cells[2].RowSpan = RowCount;
                        dg.Items[i].Cells[3].RowSpan = RowCount;
                        dg.Items[i].Cells[4].RowSpan = RowCount;
                        //dg.Items[i].Cells[10].RowSpan = RowCount;
                        //dg.Items[i].Cells[11].RowSpan = RowCount;

                        #region  CSS for Cell
                        dg.Items[i].Cells[1].BackColor = System.Drawing.Color.White;
                        dg.Items[i].Cells[2].BackColor = System.Drawing.Color.White;
                        dg.Items[i].Cells[3].BackColor = System.Drawing.Color.White;
                        dg.Items[i].Cells[4].BackColor = System.Drawing.Color.White;
                        //dg.Items[i].Cells[10].BackColor = System.Drawing.Color.White;
                        //dg.Items[i].Cells[11].BackColor = System.Drawing.Color.White;

                        dg.Items[i].Cells[1].BorderStyle = BorderStyle.Solid;
                        dg.Items[i].Cells[2].BorderStyle = BorderStyle.Solid;
                        dg.Items[i].Cells[3].BorderStyle = BorderStyle.Solid;
                        dg.Items[i].Cells[4].BorderStyle = BorderStyle.Solid;
                        //dg.Items[i].Cells[10].BorderStyle = BorderStyle.Solid;
                        //dg.Items[i].Cells[11].BorderStyle = BorderStyle.Solid;

                        dg.Items[i].Cells[1].BorderWidth = 1;
                        dg.Items[i].Cells[2].BorderWidth = 1;
                        dg.Items[i].Cells[3].BorderWidth = 1;
                        dg.Items[i].Cells[4].BorderWidth = 1;
                        //dg.Items[i].Cells[10].BorderWidth = 1;
                        //dg.Items[i].Cells[11].BorderWidth = 1;

                        dg.Items[i].Cells[1].BorderColor = System.Drawing.Color.LightGray;
                        dg.Items[i].Cells[2].BorderColor = System.Drawing.Color.LightGray;
                        dg.Items[i].Cells[3].BorderColor = System.Drawing.Color.LightGray;
                        dg.Items[i].Cells[4].BorderColor = System.Drawing.Color.LightGray;
                        //dg.Items[i].Cells[10].BorderColor = System.Drawing.Color.LightGray;
                        //dg.Items[i].Cells[11].BorderColor = System.Drawing.Color.LightGray;
                        #endregion  CSS for Cell

                    }
                    else
                    {
                        dg.Items[i].Cells[1].Visible = false;
                        dg.Items[i].Cells[2].Visible = false;
                        dg.Items[i].Cells[3].Visible = false;
                        dg.Items[i].Cells[4].Visible = false;
                        //dg.Items[i].Cells[10].Visible = false;
                        //dg.Items[i].Cells[11].Visible = false;
                    }
                    #endregion
                }
                else if (sDepartment == StaticRes.Global.Department.Painting)
                {
                    #region Painting

                    dg.Columns[4].Visible = true;
                    dg.Columns[5].Visible = true;
                    dg.Columns[6].Visible = true;
                    dg.Columns[7].Visible = true;
                    dg.Columns[8].Visible = true;
                    //dg.Columns[9].Visible = true;
                    //dg.Columns[10].Visible = true;
                    dg.Columns[11].Visible = true;

                    dg.Columns[12].Visible = false;
                    dg.Columns[13].Visible = false;
                    dg.Columns[14].Visible = false;
                    dg.Columns[15].Visible = false;
                    dg.Columns[16].Visible = false;
                    //dg.Columns[17].Visible = false;





                 
                    if (i == 0)
                    {
                        dg.Items[i].Cells[1].RowSpan = RowCount;
                        dg.Items[i].Cells[2].RowSpan = RowCount;
                        dg.Items[i].Cells[3].RowSpan = RowCount;
                        dg.Items[i].Cells[4].RowSpan = RowCount;
                        //dg.Items[i].Cells[10].RowSpan = RowCount;
                        dg.Items[i].Cells[11].RowSpan = RowCount;
                     

                        #region  CSS for Cell
                        dg.Items[i].Cells[1].BackColor = System.Drawing.Color.White;
                        dg.Items[i].Cells[2].BackColor = System.Drawing.Color.White;
                        dg.Items[i].Cells[3].BackColor = System.Drawing.Color.White;
                        dg.Items[i].Cells[4].BackColor = System.Drawing.Color.White;
                        //dg.Items[i].Cells[10].BackColor = System.Drawing.Color.White;
                        dg.Items[i].Cells[11].BackColor = System.Drawing.Color.White;
                      

                        dg.Items[i].Cells[1].BorderStyle = BorderStyle.Solid;
                        dg.Items[i].Cells[2].BorderStyle = BorderStyle.Solid;
                        dg.Items[i].Cells[3].BorderStyle = BorderStyle.Solid;
                        dg.Items[i].Cells[4].BorderStyle = BorderStyle.Solid;
                        //dg.Items[i].Cells[10].BorderStyle = BorderStyle.Solid;
                        dg.Items[i].Cells[11].BorderStyle = BorderStyle.Solid;
                     

                        dg.Items[i].Cells[1].BorderWidth = 1;
                        dg.Items[i].Cells[2].BorderWidth = 1;
                        dg.Items[i].Cells[3].BorderWidth = 1;
                        dg.Items[i].Cells[4].BorderWidth = 1;
                        //dg.Items[i].Cells[10].BorderWidth = 1;
                        dg.Items[i].Cells[11].BorderWidth = 1;
                       

                        dg.Items[i].Cells[1].BorderColor = System.Drawing.Color.LightGray;
                        dg.Items[i].Cells[2].BorderColor = System.Drawing.Color.LightGray;
                        dg.Items[i].Cells[3].BorderColor = System.Drawing.Color.LightGray;
                        dg.Items[i].Cells[4].BorderColor = System.Drawing.Color.LightGray;
                        //dg.Items[i].Cells[10].BorderColor = System.Drawing.Color.LightGray;
                        dg.Items[i].Cells[11].BorderColor = System.Drawing.Color.LightGray;
                       
                        #endregion  CSS for Cell

                    }
                    else
                    {
                        dg.Items[i].Cells[1].Visible = false;
                        dg.Items[i].Cells[2].Visible = false;
                        dg.Items[i].Cells[3].Visible = false;
                        dg.Items[i].Cells[4].Visible = false;
                        //dg.Items[i].Cells[10].Visible = false;
                        dg.Items[i].Cells[11].Visible = false;
                     
                    }
                    #endregion
                }
                else if (sDepartment == StaticRes.Global.Department.Assembly)
                {
                    #region Assy

                    dg.Columns[4].Visible = true;
                    dg.Columns[5].Visible = true;
                    dg.Columns[6].Visible = true;
                    dg.Columns[7].Visible = true;
                    dg.Columns[8].Visible = true;
                    dg.Columns[9].Visible = true;
                    dg.Columns[10].Visible = true;
                    dg.Columns[11].Visible = true;
                    dg.Columns[12].Visible = true;
                    dg.Columns[13].Visible = true;
                    dg.Columns[14].Visible = true;
                    dg.Columns[15].Visible = true;


                    dg.Columns[16].Visible = false;
                    dg.Columns[17].Visible = false;
                    dg.Columns[18].Visible = false;
                    dg.Columns[19].Visible = false;
                    dg.Columns[20].Visible = false;
                    dg.Columns[21].Visible = false;
                    dg.Columns[22].Visible = false;
                    dg.Columns[23].Visible = false;
                    dg.Columns[24].Visible = false;
                    dg.Columns[25].Visible = false;
                   
                    



                    if (i == 0)
                    {
                        dg.Items[i].Cells[1].RowSpan = RowCount;
                        dg.Items[i].Cells[2].RowSpan = RowCount;
                        dg.Items[i].Cells[3].RowSpan = RowCount;
                        dg.Items[i].Cells[4].RowSpan = RowCount;
                        dg.Items[i].Cells[15].RowSpan = RowCount;


                        #region  CSS for Cell
                        dg.Items[i].Cells[1].BackColor = System.Drawing.Color.White;
                        dg.Items[i].Cells[2].BackColor = System.Drawing.Color.White;
                        dg.Items[i].Cells[3].BackColor = System.Drawing.Color.White;
                        dg.Items[i].Cells[4].BackColor = System.Drawing.Color.White;
                        dg.Items[i].Cells[15].BackColor = System.Drawing.Color.White;


                        dg.Items[i].Cells[1].BorderStyle = BorderStyle.Solid;
                        dg.Items[i].Cells[2].BorderStyle = BorderStyle.Solid;
                        dg.Items[i].Cells[3].BorderStyle = BorderStyle.Solid;
                        dg.Items[i].Cells[4].BorderStyle = BorderStyle.Solid;
                        dg.Items[i].Cells[15].BorderStyle = BorderStyle.Solid;


                        dg.Items[i].Cells[1].BorderWidth = 1;
                        dg.Items[i].Cells[2].BorderWidth = 1;
                        dg.Items[i].Cells[3].BorderWidth = 1;
                        dg.Items[i].Cells[4].BorderWidth = 1;
                        dg.Items[i].Cells[15].BorderWidth = 1;


                        dg.Items[i].Cells[1].BorderColor = System.Drawing.Color.LightGray;
                        dg.Items[i].Cells[2].BorderColor = System.Drawing.Color.LightGray;
                        dg.Items[i].Cells[3].BorderColor = System.Drawing.Color.LightGray;
                        dg.Items[i].Cells[4].BorderColor = System.Drawing.Color.LightGray;
                        dg.Items[i].Cells[15].BorderColor = System.Drawing.Color.LightGray;

                        #endregion  CSS for Cell

                    }
                    else
                    {
                        dg.Items[i].Cells[1].Visible = false;
                        dg.Items[i].Cells[2].Visible = false;
                        dg.Items[i].Cells[3].Visible = false;
                        dg.Items[i].Cells[4].Visible = false;
                        dg.Items[i].Cells[15].Visible = false;

                    }
                    #endregion
                }



                //total row  back color
                if (dg.Items[i].Cells[0].Text == "Day Shift Total" || dg.Items[i].Cells[0].Text == "Night Shift Total")
                {
                    dg.Items[i].BackColor = System.Drawing.Color.SandyBrown;
                }

                if (dg.Items[i].Cells[0].Text.Contains("Unknown Type"))
                {
                    dg.Items[i].ForeColor = System.Drawing.Color.Red;
                }
            }
        }
        

        private int GetSN(string type)
        {            int SN = 0;
            switch (type)
            {
                case "BUTTON(U&M/C)":
                    SN = 1;
                    break;
                case "LASER BUTTON(T/C)":
                    SN = 2;
                    break;
                case "WIP BUTTON(T/C)":
                    SN = 3;
                    break;
                case "257B-BEZEL(U/C)":
                    SN = 4;
                    break;
                case "257B-BEZEL(T/C)":
                    SN = 5;
                    break;
                case "320B-BEZEL(T/C)":
                    SN = 6;
                    break;
                case "601B-PANEL(T/C)":
                    SN = 7;
                    break;
                case "Project Testing":
                    SN = 8;
                    break;


                case "PASDL 305B":
                    SN = 1;
                    break;
                case "PASDL 336B/291B":
                    SN = 2;
                    break;
                case "SWS/785A":
                    SN = 3;
                    break;
                case "SWS 440A/443A":
                    SN = 4;
                    break;
                case "Chrysler JL Dual":
                    SN = 5;
                    break;
                case "Chrysler JL Singel":
                    SN = 6;
                    break;
                case "Other":
                    SN = 7;
                    break;
                    

                case "Day Shift Total":
                    SN = 99;
                    break;
                case "Night Shift Total":
                    SN = 99;
                    break;
            }
            return SN;
        }


        #region PQC   manual key in  no use
        
        //protected void btnUpdatePQC_Click(object sender, EventArgs e)
        //{
        //    List<Common.Class.Model.TempProductivityData_Model> listProData = new List<Common.Class.Model.TempProductivityData_Model>();

        //    bool isAbleToUpdate = true;
            

        //    #region foreach dg pqc day

        //    string sTargetHR = ((TextBox)this.dgPQCDay.Items[0].Cells[15].FindControl("txtTargetHR")).Text == "" ? "0" : ((TextBox)this.dgPQCDay.Items[0].Cells[15].FindControl("txtTargetHR")).Text;

        //    foreach (DataGridItem item in dgPQCDay.Items)
        //    {

        //        string sType = item.Cells[0].Text;

        //        if (sType.Contains("Total"))
        //        {
        //            continue;
        //        }

        //        string sActualHR = ((TextBox)item.Cells[16].FindControl("txtActualHR")).Text.Trim() == "" ? "0" : ((TextBox)item.Cells[16].FindControl("txtActualHR")).Text.Trim();
        //        string sTargetQty = ((TextBox)item.Cells[17].FindControl("txtTargetQty")).Text.Trim() == "" ? "0" : ((TextBox)item.Cells[17].FindControl("txtTargetQty")).Text.Trim();
        //        string sTotalQty = ((TextBox)item.Cells[18].FindControl("txtTotalQty")).Text.Trim() == "" ? "0" : ((TextBox)item.Cells[18].FindControl("txtTotalQty")).Text.Trim();
        //        string sActualQty = ((TextBox)item.Cells[19].FindControl("txtActualQty")).Text.Trim() == "" ? "0" : ((TextBox)item.Cells[19].FindControl("txtActualQty")).Text.Trim();

        //        string sMouldRej = ((TextBox)item.Cells[20].FindControl("txtMouldRej")).Text.Trim() == "" ? "0" : ((TextBox)item.Cells[20].FindControl("txtMouldRej")).Text.Trim();
        //        string sPaintRej = ((TextBox)item.Cells[21].FindControl("txtPaintRej")).Text == "" ? "0" : ((TextBox)item.Cells[21].FindControl("txtPaintRej")).Text.Trim();
        //        string sLaserRej = ((TextBox)item.Cells[22].FindControl("txtLaserRej")).Text.Trim() == "" ? "0" : ((TextBox)item.Cells[22].FindControl("txtLaserRej")).Text.Trim();
        //        string sVendorRej = ((TextBox)item.Cells[23].FindControl("txtVendorRej")).Text.Trim() == "" ? "0" : ((TextBox)item.Cells[23].FindControl("txtVendorRej")).Text.Trim();
        //        string sPrintRej = ((TextBox)item.Cells[24].FindControl("txtPrintRej")).Text.Trim() == "" ? "0" : ((TextBox)item.Cells[24].FindControl("txtPrintRej")).Text.Trim();


        //        #region  validate 
        //        if (!Common.CommFunctions.isNumberic(sTargetQty))
        //        {
        //            if (sTargetQty != "")
        //                ((TextBox)item.Cells[17].FindControl("txtTargetQty")).ForeColor = System.Drawing.Color.Red;

        //            isAbleToUpdate = false;
        //        }
        //        else
        //        {
        //            ((TextBox)item.Cells[17].FindControl("txtTargetQty")).ForeColor = System.Drawing.Color.Black;
        //        }

        //        if (!Common.CommFunctions.isNumberic(sTotalQty))
        //        {
        //            if (sTotalQty != "")
        //                ((TextBox)item.Cells[18].FindControl("txtTotalQty")).ForeColor = System.Drawing.Color.Red;
        //            isAbleToUpdate = false;
        //        }
        //        else
        //        {
        //            ((TextBox)item.Cells[18].FindControl("txtTotalQty")).ForeColor = System.Drawing.Color.Black;
        //        }

        //        if (!Common.CommFunctions.isNumberic(sActualQty))
        //        {
        //            if (sActualQty != "")
        //                ((TextBox)item.Cells[19].FindControl("txtActualQty")).ForeColor = System.Drawing.Color.Red;
        //            isAbleToUpdate = false;
        //        }
        //        else
        //        {
        //            ((TextBox)item.Cells[19].FindControl("txtActualQty")).ForeColor = System.Drawing.Color.Black;
        //        }

                
        //        if (!Common.CommFunctions.isNumberic(sMouldRej))
        //        {
        //            if (sMouldRej != "")
        //                ((TextBox)item.Cells[20].FindControl("txtMouldRej")).ForeColor = System.Drawing.Color.Red;
        //            isAbleToUpdate = false;
        //        }
        //        else
        //        {
        //            ((TextBox)item.Cells[20].FindControl("txtMouldRej")).ForeColor = System.Drawing.Color.Black;
        //        }

        //        if (!Common.CommFunctions.isNumberic(sPaintRej))
        //        {
        //            if (sPaintRej != "")
        //                ((TextBox)item.Cells[21].FindControl("txtPaintRej")).ForeColor = System.Drawing.Color.Red;
        //            isAbleToUpdate = false;
        //        }
        //        else
        //        {
        //            ((TextBox)item.Cells[21].FindControl("txtPaintRej")).ForeColor = System.Drawing.Color.Black;
        //        }

        //        if (!Common.CommFunctions.isNumberic(sLaserRej))
        //        {
        //            if (sLaserRej != "")
        //                ((TextBox)item.Cells[22].FindControl("txtLaserRej")).ForeColor = System.Drawing.Color.Red;
        //            isAbleToUpdate = false;
        //        }
        //        else
        //        {
        //            ((TextBox)item.Cells[22].FindControl("txtLaserRej")).ForeColor = System.Drawing.Color.Black;
        //        }


        //        if (!Common.CommFunctions.isNumberic(sVendorRej))
        //        {
        //            if (sVendorRej != "")
        //                ((TextBox)item.Cells[23].FindControl("txtVendorRej")).ForeColor = System.Drawing.Color.Red;
        //            isAbleToUpdate = false;
        //        }
        //        else
        //        {
        //            ((TextBox)item.Cells[23].FindControl("txtVendorRej")).ForeColor = System.Drawing.Color.Black;
        //        }

        //        if (!Common.CommFunctions.isNumberic(sPrintRej))
        //        {
        //            if (sPrintRej != "")
        //                ((TextBox)item.Cells[24].FindControl("txtPrintRej")).ForeColor = System.Drawing.Color.Red;
        //            isAbleToUpdate = false;
        //        }
        //        else
        //        {
        //            ((TextBox)item.Cells[24].FindControl("txtPrintRej")).ForeColor = System.Drawing.Color.Black;
        //        }
                
        //        #endregion


        //        Common.Class.Model.TempProductivityData_Model model = new Common.Class.Model.TempProductivityData_Model();

        //        model.day = infDchFrom.CalendarLayout.SelectedDate.Date;
        //        model.shift = StaticRes.Global.Shift.Day;
        //        model.department = StaticRes.Global.Department.PQC;
        //        model.Type = sType;
        //        model.targetHR = sTargetHR;
        //        model.actualHR = sActualHR;
        //        model.targetQty = sTargetQty;
        //        model.totalQty = sTotalQty;
        //        model.actualQty = sActualQty;

        //        model.mouldRejCount = sMouldRej;
        //        model.paintRejCount = sPaintRej;
        //        model.laserRejCount = sLaserRej;
        //        model.vendorRejCount = sVendorRej;
        //        model.printRejCount = sPrintRej;

        //        model.createdTime = DateTime.Now;
        //        model.updatedTime = DateTime.Now;

        //        listProData.Add(model);
        //    }
        //    #endregion


        //    #region foreach dg pqc  night

        //    sTargetHR = ((TextBox)this.dgPQCNight.Items[0].Cells[15].FindControl("txtTargetHR")).Text.Trim() == "" ? "0" : ((TextBox)this.dgPQCNight.Items[0].Cells[15].FindControl("txtTargetHR")).Text.Trim();

        //    foreach (DataGridItem item in dgPQCNight.Items)
        //    {

        //        string sType = item.Cells[0].Text;

        //        if (sType.Contains("Total"))
        //        {
        //            continue;
        //        }

        //        string sActualHR = ((TextBox)item.Cells[16].FindControl("txtActualHR")).Text.Trim() == "" ? "0" : ((TextBox)item.Cells[16].FindControl("txtActualHR")).Text.Trim();
        //        string sTargetQty = ((TextBox)item.Cells[17].FindControl("txtTargetQty")).Text.Trim() == "" ? "0" : ((TextBox)item.Cells[17].FindControl("txtTargetQty")).Text.Trim();
        //        string sTotalQty = ((TextBox)item.Cells[18].FindControl("txtTotalQty")).Text.Trim() == "" ? "0" : ((TextBox)item.Cells[18].FindControl("txtTotalQty")).Text.Trim();
        //        string sActualQty = ((TextBox)item.Cells[19].FindControl("txtActualQty")).Text.Trim() == "" ? "0" : ((TextBox)item.Cells[19].FindControl("txtActualQty")).Text.Trim();

        //        string sMouldRej = ((TextBox)item.Cells[20].FindControl("txtMouldRej")).Text.Trim() == "" ? "0" : ((TextBox)item.Cells[20].FindControl("txtMouldRej")).Text.Trim();
        //        string sPaintRej = ((TextBox)item.Cells[21].FindControl("txtPaintRej")).Text == "" ? "0" : ((TextBox)item.Cells[21].FindControl("txtPaintRej")).Text.Trim();
        //        string sLaserRej = ((TextBox)item.Cells[22].FindControl("txtLaserRej")).Text.Trim() == "" ? "0" : ((TextBox)item.Cells[22].FindControl("txtLaserRej")).Text.Trim();
        //        string sVendorRej = ((TextBox)item.Cells[23].FindControl("txtVendorRej")).Text.Trim() == "" ? "0" : ((TextBox)item.Cells[23].FindControl("txtVendorRej")).Text.Trim();
        //        string sPrintRej = ((TextBox)item.Cells[24].FindControl("txtPrintRej")).Text.Trim() == "" ? "0" : ((TextBox)item.Cells[24].FindControl("txtPrintRej")).Text.Trim();


        //        #region  validate 
        //        if (!Common.CommFunctions.isNumberic(sTargetQty))
        //        {
        //            if (sTargetQty != "")
        //                ((TextBox)item.Cells[17].FindControl("txtTargetQty")).ForeColor = System.Drawing.Color.Red;

        //            isAbleToUpdate = false;
        //        }
        //        else
        //        {
        //            ((TextBox)item.Cells[17].FindControl("txtTargetQty")).ForeColor = System.Drawing.Color.Black;
        //        }

        //        if (!Common.CommFunctions.isNumberic(sTotalQty))
        //        {
        //            if (sTotalQty != "")
        //                ((TextBox)item.Cells[18].FindControl("txtTotalQty")).ForeColor = System.Drawing.Color.Red;
        //            isAbleToUpdate = false;
        //        }
        //        else
        //        {
        //            ((TextBox)item.Cells[18].FindControl("txtTotalQty")).ForeColor = System.Drawing.Color.Black;
        //        }

        //        if (!Common.CommFunctions.isNumberic(sActualQty))
        //        {
        //            if (sActualQty != "")
        //                ((TextBox)item.Cells[19].FindControl("txtActualQty")).ForeColor = System.Drawing.Color.Red;
        //            isAbleToUpdate = false;
        //        }
        //        else
        //        {
        //            ((TextBox)item.Cells[19].FindControl("txtActualQty")).ForeColor = System.Drawing.Color.Black;
        //        }


        //        if (!Common.CommFunctions.isNumberic(sMouldRej) && sMouldRej != "")
        //        {
        //            if (sMouldRej != "")
        //                ((TextBox)item.Cells[20].FindControl("txtMouldRej")).ForeColor = System.Drawing.Color.Red;
        //            isAbleToUpdate = false;
        //        }
        //        else
        //        {
        //            ((TextBox)item.Cells[20].FindControl("txtMouldRej")).ForeColor = System.Drawing.Color.Black;
        //        }

        //        if (!Common.CommFunctions.isNumberic(sPaintRej) && sPaintRej != "")
        //        {
        //            if (sPaintRej != "")
        //                ((TextBox)item.Cells[21].FindControl("txtPaintRej")).ForeColor = System.Drawing.Color.Red;
        //            isAbleToUpdate = false;
        //        }
        //        else
        //        {
        //            ((TextBox)item.Cells[21].FindControl("txtPaintRej")).ForeColor = System.Drawing.Color.Black;
        //        }

        //        if (!Common.CommFunctions.isNumberic(sLaserRej) && sLaserRej != "")
        //        {
        //            if (sLaserRej != "")
        //                ((TextBox)item.Cells[22].FindControl("txtLaserRej")).ForeColor = System.Drawing.Color.Red;
        //            isAbleToUpdate = false;
        //        }
        //        else
        //        {
        //            ((TextBox)item.Cells[22].FindControl("txtLaserRej")).ForeColor = System.Drawing.Color.Black;
        //        }


        //        if (!Common.CommFunctions.isNumberic(sVendorRej) && sVendorRej != "")
        //        {
        //            if (sVendorRej != "")
        //                ((TextBox)item.Cells[23].FindControl("txtVendorRej")).ForeColor = System.Drawing.Color.Red;
        //            isAbleToUpdate = false;
        //        }
        //        else
        //        {
        //            ((TextBox)item.Cells[23].FindControl("txtVendorRej")).ForeColor = System.Drawing.Color.Black;
        //        }
        //        if (!Common.CommFunctions.isNumberic(sPrintRej) && sPrintRej != "")
        //        {
        //            if (sPrintRej != "")
        //                ((TextBox)item.Cells[24].FindControl("txtPrintRej")).ForeColor = System.Drawing.Color.Red;
        //            isAbleToUpdate = false;
        //        }
        //        else
        //        {
        //            ((TextBox)item.Cells[24].FindControl("txtPrintRej")).ForeColor = System.Drawing.Color.Black;
        //        }
        //        #endregion


        //        Common.Class.Model.TempProductivityData_Model model = new Common.Class.Model.TempProductivityData_Model();

        //        model.day = infDchFrom.CalendarLayout.SelectedDate.Date;
        //        model.shift = StaticRes.Global.Shift.Night;
        //        model.department = StaticRes.Global.Department.PQC;
        //        model.Type = sType;
        //        model.targetHR = sTargetHR;
        //        model.actualHR = sActualHR;
        //        model.targetQty = sTargetQty;
        //        model.totalQty = sTotalQty;
        //        model.actualQty = sActualQty;

        //        model.mouldRejCount = sMouldRej;
        //        model.paintRejCount = sPaintRej;
        //        model.laserRejCount = sLaserRej;
        //        model.vendorRejCount = sVendorRej;
        //        model.printRejCount = sPrintRej;

        //        model.createdTime = DateTime.Now;
        //        model.updatedTime = DateTime.Now;

        //        listProData.Add(model);
        //    }
        //    #endregion


        //    if (!isAbleToUpdate)
        //    {
        //        Common.CommFunctions.ShowMessage(this.Page, "Some data is wrong, please check!");
        //        return;
        //    }



        //    Session["ListTempProductivityData_Model"] = listProData;

        //    Response.Redirect("../Laser/Login.aspx?Department=" + StaticRes.Global.Department.PQC + "&commandType=UpdateProductivity", false);
        //}

        //protected void btnEditPQC_Click(object sender, EventArgs e)
        //{
        //    this.dgPQCNight.Visible = true;

        //    initEditStylePQC(this.dgPQCDay);
        //    initEditStylePQC(this.dgPQCNight);

        //    this.btnUpdatePQC.Visible = true;
        //    this.btnEditPQC.Visible = false;
        //}
        
        //private void initEditStylePQC(DataGrid dg)
        //{
           
        //    dg.Columns[4].Visible = false;
        //    dg.Columns[5].Visible = false;
        //    dg.Columns[6].Visible = false;
        //    dg.Columns[7].Visible = false;
        //    dg.Columns[8].Visible = false;
        //    dg.Columns[9].Visible = false;
        //    dg.Columns[10].Visible = false;
        //    dg.Columns[11].Visible = false;
        //    dg.Columns[12].Visible = false;
        //    dg.Columns[13].Visible = false;
        //    dg.Columns[14].Visible = false;


        //    dg.Columns[15].Visible = true;
        //    dg.Columns[16].Visible = true;
        //    dg.Columns[17].Visible = true;
        //    dg.Columns[18].Visible = true;
        //    dg.Columns[19].Visible = true;
        //    dg.Columns[20].Visible = true;
        //    dg.Columns[21].Visible = true;
        //    //dg.Columns[22].Visible = true;
        //    //dg.Columns[23].Visible = true;
        //    //dg.Columns[24].Visible = true;


        //    #region style
        //    int rowSpan = dg.Items.Count;

        //    foreach (DataGridItem item in dg.Items)
        //    {

        //        if (item.ItemIndex == 0)
        //        {
        //            item.Cells[1].RowSpan = rowSpan; //ManPower
        //            item.Cells[2].RowSpan = rowSpan; //Attendance
        //            item.Cells[3].RowSpan = rowSpan; //AttendRate
        //            item.Cells[15].RowSpan = rowSpan; //TargetHRS edit box



        //            item.Cells[1].BackColor = System.Drawing.Color.White;
        //            item.Cells[2].BackColor = System.Drawing.Color.White;
        //            item.Cells[3].BackColor = System.Drawing.Color.White;
        //            item.Cells[15].BackColor = System.Drawing.Color.White;


        //            item.Cells[1].BorderStyle = BorderStyle.Solid;
        //            item.Cells[2].BorderStyle = BorderStyle.Solid;
        //            item.Cells[3].BorderStyle = BorderStyle.Solid;
        //            item.Cells[15].BorderStyle = BorderStyle.Solid;


        //            item.Cells[1].BorderWidth = 1;
        //            item.Cells[2].BorderWidth = 1;
        //            item.Cells[3].BorderWidth = 1;
        //            item.Cells[15].BorderWidth = 1;


        //            item.Cells[1].BorderColor = System.Drawing.Color.LightGray;
        //            item.Cells[2].BorderColor = System.Drawing.Color.LightGray;
        //            item.Cells[3].BorderColor = System.Drawing.Color.LightGray;
        //            item.Cells[15].BorderColor = System.Drawing.Color.LightGray;

        //        }
        //        else
        //        {
        //            item.Cells[1].Visible = false;
        //            item.Cells[2].Visible = false;
        //            item.Cells[3].Visible = false;
        //            item.Cells[15].Visible = false;
        //        }



        //        //total row  back color
        //        if (item.Cells[0].Text == "Day Shift Total" || item.Cells[0].Text == "Night Shift Total")
        //        {
        //            item.BackColor = System.Drawing.Color.SandyBrown;
        //        }

        //        if (item.Cells[0].Text.Contains("Unknown Type"))
        //        {
        //            item.ForeColor = System.Drawing.Color.Red;
        //        }

        //    }

        //    #endregion

            

        //    foreach (DataGridItem item in dg.Items)
        //    {
                
        //        if (item.Cells[0].Text.Contains("Total"))
        //        {
        //            item.Visible = false;
        //            continue;
        //        }


        //        try
        //        {
        //            string[] arrTime = item.Cells[4].Text.Split(':');

        //            if (arrTime.Length == 1)
        //            {
        //                ((TextBox)item.Cells[15].FindControl("txtTargetHR")).Text = item.Cells[4].Text == "&nbsp;" ? "" : item.Cells[4].Text;
        //            }
        //            else
        //            {
        //                double seconds = double.Parse(arrTime[0]) * 3600 + double.Parse(arrTime[1]) * 60 + double.Parse(arrTime[2]);

        //                ((TextBox)item.Cells[15].FindControl("txtTargetHR")).Text = Math.Round(seconds / 3600, 0).ToString();
        //            }
        //        }
        //        catch (Exception ee)
        //        {
        //            ((TextBox)item.Cells[15].FindControl("txtTargetHR")).Text = "00:00:00";
        //        }

        //        try
        //        {
        //            string[] arrTime = item.Cells[5].Text.Split(':');
        //            if (arrTime.Length == 1)
        //            {
        //                ((TextBox)item.Cells[16].FindControl("txtActualHR")).Text = item.Cells[5].Text == "&nbsp;" ? "" : item.Cells[5].Text;
        //            }
        //            else
        //            {
        //                double seconds = double.Parse(arrTime[0]) * 3600 + double.Parse(arrTime[1]) * 60 + double.Parse(arrTime[2]);

        //                ((TextBox)item.Cells[16].FindControl("txtActualHR")).Text = Math.Round(seconds / 3600, 0).ToString();
        //            }
        //        }
        //        catch (Exception)
        //        {
        //            ((TextBox)item.Cells[16].FindControl("txtActualHR")).Text = "00:00:00";
        //        }

        //            ((TextBox)item.Cells[17].FindControl("txtTargetQty")).Text = item.Cells[6].Text == "&nbsp;" ? "" : item.Cells[6].Text;
        //        ((TextBox)item.Cells[18].FindControl("txtTotalQty")).Text = item.Cells[7].Text == "&nbsp;" ? "" : item.Cells[7].Text;
        //        ((TextBox)item.Cells[19].FindControl("txtActualQty")).Text = item.Cells[8].Text == "&nbsp;" ? "" : item.Cells[8].Text;
        //        ((TextBox)item.Cells[20].FindControl("txtMouldRej")).Text = item.Cells[9].Text == "&nbsp;" ? "" : item.Cells[9].Text;
        //        ((TextBox)item.Cells[21].FindControl("txtPaintRej")).Text = item.Cells[10].Text == "&nbsp;" ? "" : item.Cells[10].Text;
        //        ((TextBox)item.Cells[22].FindControl("txtLaserRej")).Text = item.Cells[11].Text == "&nbsp;" ? "" : item.Cells[11].Text;
        //        ((TextBox)item.Cells[23].FindControl("txtVendorRej")).Text = item.Cells[12].Text == "&nbsp;" ? "" : item.Cells[12].Text;
        //        ((TextBox)item.Cells[24].FindControl("txtPrintRej")).Text = item.Cells[13].Text == "&nbsp;" ? "" : item.Cells[13].Text;




        //    }

        //}


        #endregion


        #region painting

        protected void btnUpdatePainting_Click(object sender, EventArgs e)
        {

            List<Common.Class.Model.TempProductivityData_Model> listProData = new List<Common.Class.Model.TempProductivityData_Model>();

            bool isAbleToUpdate = true;


            string sTargetHR = ((TextBox)this.dgPaintingDay.Items[0].Cells[12].FindControl("txtTargetHR")).Text;


            foreach (DataGridItem item in dgPaintingDay.Items)
            {

                string sType = item.Cells[0].Text;

                if (sType.Contains("Total"))
                {
                    continue;
                }

                string sActualHR = ((TextBox)item.Cells[13].FindControl("txtActualHR")).Text.Trim();
                string sTargetQty = ((TextBox)item.Cells[14].FindControl("txtTargetQty")).Text.Trim();
                string sTotalQty = ((TextBox)item.Cells[15].FindControl("txtTotalQty")).Text.Trim();
                string sActualQty = ((TextBox)item.Cells[16].FindControl("txtActualQty")).Text.Trim();
                string sRejectQty = ((TextBox)item.Cells[17].FindControl("txtRejectQty")).Text.Trim() == "" ? "0" : ((TextBox)item.Cells[17].FindControl("txtRejectQty")).Text.Trim();
                //string sTypeRej = ((TextBox)item.Cells[18].FindControl("txtRejRate")).Text;
                //string sUtilization = ((TextBox)item.Cells[19].FindControl("txtUtilization")).Text;


                #region validation
                if (!Common.CommFunctions.isNumberic(sTargetQty))
                {
                    if (sTargetQty != "")
                        ((TextBox)item.Cells[14].FindControl("txtTargetQty")).ForeColor = System.Drawing.Color.Red;
                    isAbleToUpdate = false;
                }
                else
                {
                    ((TextBox)item.Cells[14].FindControl("txtTargetQty")).ForeColor = System.Drawing.Color.Black;
                }

                if (!Common.CommFunctions.isNumberic(sTotalQty))
                {
                    if (sTotalQty != "")
                        ((TextBox)item.Cells[15].FindControl("txtTotalQty")).ForeColor = System.Drawing.Color.Red;
                    isAbleToUpdate = false;
                }
                else
                {
                    ((TextBox)item.Cells[15].FindControl("txtTotalQty")).ForeColor = System.Drawing.Color.Black;
                }

                if (!Common.CommFunctions.isNumberic(sActualQty))
                {
                    if (sActualQty != "")
                        ((TextBox)item.Cells[16].FindControl("txtActualQty")).ForeColor = System.Drawing.Color.Red;
                    isAbleToUpdate = false;
                }
                else
                {
                    ((TextBox)item.Cells[16].FindControl("txtActualQty")).ForeColor = System.Drawing.Color.Black;
                }

                if (!Common.CommFunctions.isNumberic(sRejectQty))
                {
                    if (sRejectQty != "")
                        ((TextBox)item.Cells[17].FindControl("txtRejectQty")).ForeColor = System.Drawing.Color.Red;

                    isAbleToUpdate = false;
                }
                else
                {
                    ((TextBox)item.Cells[17].FindControl("txtRejectQty")).ForeColor = System.Drawing.Color.Black;
                }


                //if (Common.CommFunctions.isNumberic(sActualQty) &&
                //    Common.CommFunctions.isNumberic(sRejectQty) &&
                //    Common.CommFunctions.isNumberic(sTotalQty))
                //{
                //    if (double.Parse(sActualQty) + double.Parse(sRejectQty) > double.Parse(sTotalQty))
                //    {
                //        if (sTotalQty != "")
                //            ((TextBox)item.Cells[15].FindControl("txtTotalQty")).ForeColor = System.Drawing.Color.Red;
                //        if (sActualQty != "")
                //            ((TextBox)item.Cells[16].FindControl("txtActualQty")).ForeColor = System.Drawing.Color.Red;
                //        if (sRejectQty != "")
                //            ((TextBox)item.Cells[17].FindControl("txtRejectQty")).ForeColor = System.Drawing.Color.Red;

                //        isAbleToUpdate = false;
                //    }
                //}
                #endregion


                Common.Class.Model.TempProductivityData_Model model = new Common.Class.Model.TempProductivityData_Model();

                model.day = infDchFrom.CalendarLayout.SelectedDate.Date;
                model.shift = StaticRes.Global.Shift.Day;
                model.department = StaticRes.Global.Department.Painting;
                model.Type = sType;
                model.targetHR = sTargetHR;
                model.actualHR = sActualHR;
                model.targetQty = sTargetQty;
                model.totalQty = sTotalQty;
                model.actualQty = sActualQty;
                model.rejectQty = sRejectQty;
                //model.typeRej = sTypeRej;
                //model.utilization = sUtilization;

                model.createdTime = DateTime.Now;
                model.updatedTime = DateTime.Now;

                listProData.Add(model);
            }

            if (!isAbleToUpdate)
            {
                Common.CommFunctions.ShowMessage(this.Page, "Some data is wrong, please check!");
                return;
            }


            Session["ListTempProductivityData_Model"] = listProData;

            Response.Redirect("../Laser/Login.aspx?Department=" + StaticRes.Global.Department.Painting + "&commandType=UpdateProductivity", false);
        }

        protected void btnEditPainting_Click(object sender, EventArgs e)
        {
            initEditStylePainting();

            this.btnUpdatePainting.Visible = true;
            this.btnEditPainting.Visible = false;
        }



        public DataTable getProductivityReportForPainting(DateTime dDay, string sShift, string sDepartment)
        {

            Common.Class.DAL.TempProductivityData_DAL dal = new Common.Class.DAL.TempProductivityData_DAL();
            DataTable dtProduct = dal.GetgetProductivtiyReportForTemp(sDepartment, dDay, sShift);



            #region attendance
            double ManPower = 0;
            double Attendance = 0;
            string AttendRate = "";

            Common.BLL.LMMSWatchLog_BLL lmmswatchLogBLL = new Common.BLL.LMMSWatchLog_BLL();
            DataTable dt = lmmswatchLogBLL.getAttendance(dDay, sShift, sDepartment);

            if (dt != null && dt.Rows.Count != 0)
            {
                ManPower = double.Parse(dt.Rows[0]["Man Power"].ToString());
                Attendance = double.Parse(dt.Rows[0]["Attendance"].ToString());
                AttendRate = ManPower == 0 ? "0%" : Math.Round(Attendance / ManPower * 100, 2).ToString() + "%";
            }
            #endregion attendance




            #region foreach for total Row
            double Total_ActualTime = 0;
            double Total_ActualQty = 0;//实际良品
            double Total_TargetQty = 0;//理论总量
            double Total_RejQty = 0;


            //===foreach===//
            foreach (DataRow dr in dtProduct.Rows)
            {
                if (dr["ActualHR"].ToString() != "")
                    Total_ActualTime += double.Parse(dr["ActualHR"].ToString()) * 3600;

                if (dr["ActualQty"].ToString() != "")
                    Total_ActualQty += double.Parse(dr["ActualQty"].ToString());

                if (dr["TargetQty"].ToString() != "")
                    Total_TargetQty += double.Parse(dr["TargetQty"].ToString());

                if (dr["RejectQty"].ToString() != "")
                    Total_RejQty += double.Parse(dr["RejectQty"].ToString());
            }
            //===foreach===//


            double TotalRejRate = Total_ActualQty == 0 ? 0.00 : Math.Round(Total_RejQty / (Total_ActualQty + Total_RejQty) * 100, 2);
            string Utilization = Total_ActualTime == 0 ? "0.00%" : Math.Round(Total_ActualTime / 3600 / 96 * 100, 2).ToString() + "%";
            double dProductivity = Total_TargetQty == 0 ? 0.00 : Math.Round(Total_ActualQty / Total_TargetQty * 100, 2);


            //===dr total===//
            DataRow dr_total = dtProduct.NewRow();
            dr_total["ManPower"] = ManPower;
            dr_total["Attendance"] = Attendance;
            dr_total["AttendRate"] = AttendRate;
            dr_total["Utilization"] = Utilization;

            dr_total["SN"] = GetSN("Day Shift Total");
            dr_total["ProductType"] = sShift == StaticRes.Global.Shift.Day ? "Day Shift Total" : "Night Shift Total";
            dr_total["ActualQty"] = Total_ActualQty;
            dr_total["RejectQty"] = Total_RejQty;
            dr_total["TotalQty"] = Total_ActualQty + Total_RejQty;
            dr_total["ActualHR"] = Math.Round(Total_ActualTime / 3600, 2);
            dr_total["TargetQty"] = Total_TargetQty;
            dr_total["TargetHR"] = 12 * 8;
            dr_total["RejRate"] = TotalRejRate.ToString() + "%";
            //===dr total===//


            dtProduct.Rows.Add(dr_total);

            #endregion


            #region add all type

            //BUTTON(U & M / C)
            //LASER BUTTON(T / C)
            //WIP BUTTON(T / C)
            //257B - BEZEL(U / C)
            //257B - BEZEL(T / C)
            //320B - BEZEL(T / C)
            //601B - PANEL(T / C)
            //Project Testing


            List<string> listType = new List<string>();
            listType.Add("BUTTON(U&M/C)");
            listType.Add("LASER BUTTON(T/C)");
            listType.Add("WIP BUTTON(T/C)");
            listType.Add("257B-BEZEL(U/C)");
            listType.Add("257B-BEZEL(T/C)");
            listType.Add("320B-BEZEL(T/C)");
            listType.Add("601B-PANEL(T/C)");
            listType.Add("Project Testing");


            foreach (string type in listType)
            {
                DataRow[] drArr = dtProduct.Select(" ProductType = '" + type + "'");
                if (drArr.Length == 0)
                {
                    DataRow dr = dtProduct.NewRow();
                    dr["SN"] = GetSN(type);
                    dr["ProductType"] = type;
                    dr["ManPower"] = ManPower;
                    dr["Attendance"] = Attendance;
                    dr["AttendRate"] = AttendRate;


                    dr["TargetQty"] = 0;
                    dr["ActualQty"] = 0;
                    dr["RejectQty"] = 0;
                    dr["TotalQty"] = 0;
                    dr["ActualHR"] = 0;
                    dr["TargetHR"] = 12 * 8;
                    dr["RejRate"] = "0%";
                    dr["Utilization"] = Utilization;

                    dr["TargetQty"] = "";
                    dr["ActualQty"] = "";
                    dr["RejectQty"] = "";
                    dr["TotalQty"] = "";
                    dr["ActualHR"] = "";
                    dr["TargetHR"] = "";
                    dr["RejRate"] = "";
                    dr["Utilization"] = "";


                    dtProduct.Rows.Add(dr.ItemArray);
                }
                else
                {
                    drArr[0]["ManPower"] = ManPower;
                    drArr[0]["Attendance"] = Attendance;
                    drArr[0]["AttendRate"] = AttendRate;
                    drArr[0]["Utilization"] = Utilization;
                }
            }
            #endregion


            //order by sn
            dtProduct = dtProduct.Select("", "SN asc").CopyToDataTable();


            return dtProduct;
        }
        
        private void initEditStylePainting()
        {

            this.dgPaintingDay.Columns[4].Visible = false;
            this.dgPaintingDay.Columns[5].Visible = false;
            this.dgPaintingDay.Columns[6].Visible = false;
            this.dgPaintingDay.Columns[7].Visible = false;
            this.dgPaintingDay.Columns[8].Visible = false;
            //this.dgPaintingDay.Columns[9].Visible = false;
            //this.dgPaintingDay.Columns[10].Visible = false;
            this.dgPaintingDay.Columns[11].Visible = false;

            this.dgPaintingDay.Columns[12].Visible = true;
            this.dgPaintingDay.Columns[13].Visible = true;
            this.dgPaintingDay.Columns[14].Visible = true;
            this.dgPaintingDay.Columns[15].Visible = true;
            this.dgPaintingDay.Columns[16].Visible = true;
            //this.dgPaintingDay.Columns[17].Visible = true;




            int rowSpan = this.dgPaintingDay.Items.Count;

            foreach (DataGridItem item in this.dgPaintingDay.Items)
            {

                if (item.ItemIndex == 0)
                {
                    item.Cells[1].RowSpan = rowSpan; //ManPower
                    item.Cells[2].RowSpan = rowSpan; //Attendance
                    item.Cells[3].RowSpan = rowSpan; //AttendRate
                    item.Cells[12].RowSpan = rowSpan; //TargetHRS edit box



                    #region  style for Cell
                    item.Cells[1].BackColor = System.Drawing.Color.White;
                    item.Cells[2].BackColor = System.Drawing.Color.White;
                    item.Cells[3].BackColor = System.Drawing.Color.White;
                    item.Cells[12].BackColor = System.Drawing.Color.White;


                    item.Cells[1].BorderStyle = BorderStyle.Solid;
                    item.Cells[2].BorderStyle = BorderStyle.Solid;
                    item.Cells[3].BorderStyle = BorderStyle.Solid;
                    item.Cells[12].BorderStyle = BorderStyle.Solid;


                    item.Cells[1].BorderWidth = 1;
                    item.Cells[2].BorderWidth = 1;
                    item.Cells[3].BorderWidth = 1;
                    item.Cells[12].BorderWidth = 1;


                    item.Cells[1].BorderColor = System.Drawing.Color.LightGray;
                    item.Cells[2].BorderColor = System.Drawing.Color.LightGray;
                    item.Cells[3].BorderColor = System.Drawing.Color.LightGray;
                    item.Cells[12].BorderColor = System.Drawing.Color.LightGray;

                    #endregion  CSS for Cell
                }
                else
                {
                    item.Cells[1].Visible = false;
                    item.Cells[2].Visible = false;
                    item.Cells[3].Visible = false;
                    item.Cells[12].Visible = false;
                }



                //total row  back color
                if (item.Cells[0].Text == "Day Shift Total" || item.Cells[0].Text == "Night Shift Total")
                {
                    item.BackColor = System.Drawing.Color.SandyBrown;
                }

                if (item.Cells[0].Text.Contains("Unknown Type"))
                {
                    item.ForeColor = System.Drawing.Color.Red;
                }

            }


            foreach (DataGridItem item in this.dgPaintingDay.Items)
            {
                if (item.Cells[0].Text.Contains("Total"))
                {
                    item.Visible = false;
                    continue;
                }


                ((TextBox)item.Cells[12].FindControl("txtTargetHR")).Text = item.Cells[4].Text == "&nbsp;" ? "" : item.Cells[4].Text;
                ((TextBox)item.Cells[13].FindControl("txtActualHR")).Text = item.Cells[5].Text == "&nbsp;" ? "" : item.Cells[5].Text;
                ((TextBox)item.Cells[14].FindControl("txtTargetQty")).Text = item.Cells[6].Text == "&nbsp;" ? "" : item.Cells[6].Text;
                ((TextBox)item.Cells[15].FindControl("txtTotalQty")).Text = item.Cells[7].Text == "&nbsp;" ? "" : item.Cells[7].Text;
                ((TextBox)item.Cells[16].FindControl("txtActualQty")).Text = item.Cells[8].Text == "&nbsp;" ? "" : item.Cells[8].Text;
                //((TextBox)item.Cells[17].FindControl("txtRejectQty")).Text = item.Cells[9].Text == "&nbsp;" ? "" : item.Cells[9].Text;

            }


        }

        #endregion

        
        #region assy

        protected void btnUpdateAssy_Click(object sender, EventArgs e)
        {
            List<Common.Class.Model.TempProductivityData_Model> listProData = new List<Common.Class.Model.TempProductivityData_Model>();

            bool isAbleToUpdate = true;


            string sTargetHR = ((TextBox)this.dgAssyDay.Items[0].Cells[16].FindControl("txtTargetHR")).Text;


            foreach (DataGridItem item in dgAssyDay.Items)
            {

                string sType = item.Cells[0].Text;

                if (sType.Contains("Total"))
                {
                    continue;
                }

                string sActualHR = ((TextBox)item.Cells[17].FindControl("txtActualHR")).Text.Trim();
                string sTargetQty = ((TextBox)item.Cells[18].FindControl("txtTargetQty")).Text.Trim();
                string sTotalQty = ((TextBox)item.Cells[19].FindControl("txtTotalQty")).Text.Trim();
                string sActualQty = ((TextBox)item.Cells[20].FindControl("txtActualQty")).Text.Trim();

                string sRejectBezel = ((TextBox)item.Cells[20].FindControl("txtRejBezel")).Text.Trim() == "" ? "0" : ((TextBox)item.Cells[20].FindControl("txtRejBezel")).Text.Trim();
                string sRejectCover = ((TextBox)item.Cells[21].FindControl("txtRejectCover")).Text == "" ? "0" : ((TextBox)item.Cells[21].FindControl("txtRejectCover")).Text.Trim();
                string sRejectESC = ((TextBox)item.Cells[22].FindControl("txtRejectESC")).Text.Trim() == "" ? "0" : ((TextBox)item.Cells[22].FindControl("txtRejectESC")).Text.Trim();
                string sRejectBtn = ((TextBox)item.Cells[23].FindControl("txtRejectBtn")).Text.Trim() == "" ? "0" : ((TextBox)item.Cells[23].FindControl("txtRejectBtn")).Text.Trim();
                string sRejectIndicator = ((TextBox)item.Cells[24].FindControl("txtRejectIndicator")).Text.Trim() == "" ? "0" : ((TextBox)item.Cells[24].FindControl("txtRejectIndicator")).Text.Trim();


                #region  validate 
                if (!Common.CommFunctions.isNumberic(sTargetQty))
                {
                    if (sTargetQty != "")
                        ((TextBox)item.Cells[17].FindControl("txtTargetQty")).ForeColor = System.Drawing.Color.Red;

                    isAbleToUpdate = false;
                }
                else
                {
                    ((TextBox)item.Cells[17].FindControl("txtTargetQty")).ForeColor = System.Drawing.Color.Black;
                }

                if (!Common.CommFunctions.isNumberic(sTotalQty))
                {
                    if (sTotalQty != "")
                        ((TextBox)item.Cells[18].FindControl("txtTotalQty")).ForeColor = System.Drawing.Color.Red;
                    isAbleToUpdate = false;
                }
                else
                {
                    ((TextBox)item.Cells[18].FindControl("txtTotalQty")).ForeColor = System.Drawing.Color.Black;
                }

                if (!Common.CommFunctions.isNumberic(sActualQty))
                {
                    if (sActualQty != "")
                        ((TextBox)item.Cells[19].FindControl("txtActualQty")).ForeColor = System.Drawing.Color.Red;
                    isAbleToUpdate = false;
                }
                else
                {
                    ((TextBox)item.Cells[19].FindControl("txtActualQty")).ForeColor = System.Drawing.Color.Black;
                }


                if (!Common.CommFunctions.isNumberic(sRejectBezel))
                {
                    if (sRejectBezel != "")
                        ((TextBox)item.Cells[20].FindControl("txtRejBezel")).ForeColor = System.Drawing.Color.Red;
                    isAbleToUpdate = false;
                }
                else
                {
                    ((TextBox)item.Cells[20].FindControl("txtRejBezel")).ForeColor = System.Drawing.Color.Black;
                }

                if (!Common.CommFunctions.isNumberic(sRejectCover))
                {
                    if (sRejectCover != "")
                        ((TextBox)item.Cells[21].FindControl("txtRejectCover")).ForeColor = System.Drawing.Color.Red;
                    isAbleToUpdate = false;
                }
                else
                {
                    ((TextBox)item.Cells[21].FindControl("txtRejectCover")).ForeColor = System.Drawing.Color.Black;
                }

                if (!Common.CommFunctions.isNumberic(sRejectESC))
                {
                    if (sRejectESC != "")
                        ((TextBox)item.Cells[22].FindControl("txtRejectESC")).ForeColor = System.Drawing.Color.Red;
                    isAbleToUpdate = false;
                }
                else
                {
                    ((TextBox)item.Cells[22].FindControl("txtRejectESC")).ForeColor = System.Drawing.Color.Black;
                }


                if (!Common.CommFunctions.isNumberic(sRejectBtn))
                {
                    if (sRejectBtn != "")
                        ((TextBox)item.Cells[23].FindControl("txtRejectBtn")).ForeColor = System.Drawing.Color.Red;
                    isAbleToUpdate = false;
                }
                else
                {
                    ((TextBox)item.Cells[23].FindControl("txtRejectBtn")).ForeColor = System.Drawing.Color.Black;
                }
                if (!Common.CommFunctions.isNumberic(sRejectIndicator))
                {
                    if (sRejectIndicator != "")
                        ((TextBox)item.Cells[24].FindControl("txtRejectIndicator")).ForeColor = System.Drawing.Color.Red;
                    isAbleToUpdate = false;
                }
                else
                {
                    ((TextBox)item.Cells[24].FindControl("txtRejectIndicator")).ForeColor = System.Drawing.Color.Black;
                }

                if (Common.CommFunctions.isNumberic(sActualQty) &&
                    Common.CommFunctions.isNumberic(sRejectBezel) &&
                    Common.CommFunctions.isNumberic(sRejectCover) &&
                    Common.CommFunctions.isNumberic(sRejectESC) &&
                    Common.CommFunctions.isNumberic(sRejectBtn) &&
                    Common.CommFunctions.isNumberic(sRejectIndicator))
                {
                    double total = double.Parse(sActualQty) + double.Parse(sRejectBezel) + double.Parse(sRejectCover) + double.Parse(sRejectESC) + double.Parse(sRejectBtn) + double.Parse(sRejectIndicator);
                    if (total > double.Parse(sTotalQty))
                    {
                        if (sTotalQty != "")
                            ((TextBox)item.Cells[18].FindControl("txtTotalQty")).ForeColor = System.Drawing.Color.Red;
                        if (sActualQty != "")
                            ((TextBox)item.Cells[19].FindControl("txtActualQty")).ForeColor = System.Drawing.Color.Red;
                        if (sRejectBezel != "")
                            ((TextBox)item.Cells[20].FindControl("txtRejBezel")).ForeColor = System.Drawing.Color.Red;
                        if (sRejectCover != "")
                            ((TextBox)item.Cells[21].FindControl("txtRejectCover")).ForeColor = System.Drawing.Color.Red;
                        if (sRejectESC != "")
                            ((TextBox)item.Cells[22].FindControl("txtRejectESC")).ForeColor = System.Drawing.Color.Red;
                        if (sRejectBtn != "")
                            ((TextBox)item.Cells[23].FindControl("txtRejectBtn")).ForeColor = System.Drawing.Color.Red;
                        if (sRejectIndicator != "")
                            ((TextBox)item.Cells[24].FindControl("txtRejectIndicator")).ForeColor = System.Drawing.Color.Red;

                        isAbleToUpdate = false;
                    }
                }



                #endregion


                Common.Class.Model.TempProductivityData_Model model = new Common.Class.Model.TempProductivityData_Model();

                model.day = infDchFrom.CalendarLayout.SelectedDate.Date;
                model.shift = StaticRes.Global.Shift.Day;
                model.department = StaticRes.Global.Department.Assembly;
                model.Type = sType;
                model.targetHR = sTargetHR;
                model.actualHR = sActualHR;
                model.targetQty = sTargetQty;
                model.totalQty = sTotalQty;
                model.actualQty = sActualQty;

                model.bezelRejQty = sRejectBezel;
                model.coverRejQty = sRejectCover;
                model.escRejQty = sRejectESC;
                model.btnRejQty = sRejectBtn;
                model.indicatorRejQty = sRejectIndicator;

                model.createdTime = DateTime.Now;
                model.updatedTime = DateTime.Now;

                listProData.Add(model);
            }


            if (!isAbleToUpdate)
            {
                Common.CommFunctions.ShowMessage(this.Page, "Some data is wrong, please check!");
                return;
            }



            Session["ListTempProductivityData_Model"] = listProData;

            Response.Redirect("../Laser/Login.aspx?Department=" + StaticRes.Global.Department.Assembly + "&commandType=UpdateProductivity", false);

        }

        protected void btnEditAssy_Click(object sender, EventArgs e)
        {
            initEditStyleAssy();

            this.btnUpdateAssy.Visible = true;
            this.btnEditAssy.Visible = false;

        }

        
        public DataTable getProductivityReportForAssy(DateTime dDay, string sShift, string sDepartment)
        {
            Common.Class.DAL.TempProductivityData_DAL dal = new Common.Class.DAL.TempProductivityData_DAL();
            DataTable dtProduct = dal.GetgetProductivtiyReportForTemp(sDepartment, dDay, sShift);


            #region attendance 

            double ManPower = 0;
            double Attendance = 0;
            string AttendRate = "";


            Common.BLL.LMMSWatchLog_BLL lmmswatchLogBLL = new Common.BLL.LMMSWatchLog_BLL();
           DataTable dt = lmmswatchLogBLL.getAttendance(dDay, sShift, sDepartment);
            
            if (dt != null && dt.Rows.Count != 0)
            {
                ManPower = double.Parse(dt.Rows[0]["Man Power"].ToString());
                Attendance = double.Parse(dt.Rows[0]["Attendance"].ToString());
                AttendRate = ManPower == 0 ? "0%" : Math.Round(Attendance / ManPower * 100, 2).ToString() + "%";
            }
            #endregion


            #region foreach for total Row
            double Total_ActualTime = 0;
            double Total_ActualQty = 0;//实际良品
            double Total_TargetQty = 0;//理论总量

            double Total_bezelRejQty = 0;
            double Total_coverRejQty = 0;
            double Total_escRejQty = 0;
            double Total_btnRejQty = 0;
            double Total_indicatorRejQty = 0;

            foreach (DataRow dr in dtProduct.Rows)
            {
                if (dr["ActualHR"].ToString() != "")
                    Total_ActualTime += double.Parse(dr["ActualHR"].ToString()) * 3600;

                if (dr["ActualQty"].ToString() != "")
                    Total_ActualQty += double.Parse(dr["ActualQty"].ToString());

                if (dr["TargetQty"].ToString() != "")
                    Total_TargetQty += double.Parse(dr["TargetQty"].ToString());

                if (dr["bezelRejQty"].ToString() != "")
                    Total_bezelRejQty += double.Parse(dr["bezelRejQty"].ToString());

                if (dr["coverRejQty"].ToString() != "")
                    Total_coverRejQty += double.Parse(dr["coverRejQty"].ToString());

                if (dr["escRejQty"].ToString() != "")
                    Total_escRejQty += double.Parse(dr["escRejQty"].ToString());

                if (dr["btnRejQty"].ToString() != "")
                    Total_btnRejQty += double.Parse(dr["btnRejQty"].ToString());

                if (dr["indicatorRejQty"].ToString() != "")
                    Total_indicatorRejQty += double.Parse(dr["indicatorRejQty"].ToString());
            }

            double Total_Rej = Total_bezelRejQty + Total_coverRejQty + Total_escRejQty + Total_btnRejQty + Total_indicatorRejQty;


            double TotalRejRate = Total_ActualQty == 0 ? 0.00 : Math.Round(Total_Rej / (Total_ActualQty + Total_Rej) * 100, 2);
            string Utilization = Total_ActualTime == 0 ? "0.00%" : Math.Round(Total_ActualTime / 3600 / 96 * 100, 2).ToString() + "%";
            double dProductivity = Total_TargetQty == 0 ? 0.00 : Math.Round(Total_ActualQty / Total_TargetQty * 100, 2);


            //=== dr total ===//
            DataRow dr_total = dtProduct.NewRow();
            dr_total["ManPower"] = ManPower;
            dr_total["Attendance"] = Attendance;
            dr_total["AttendRate"] = AttendRate;
            dr_total["Utilization"] = Utilization;

            dr_total["SN"] = GetSN("Day Shift Total");
            dr_total["ProductType"] = sShift == StaticRes.Global.Shift.Day ? "Day Shift Total" : "Night Shift Total";
            dr_total["ActualQty"] = Total_ActualQty;

            dr_total["bezelRejQty"] = Total_bezelRejQty;
            dr_total["coverRejQty"] = Total_coverRejQty;
            dr_total["escRejQty"] = Total_escRejQty;
            dr_total["btnRejQty"] = Total_btnRejQty;
            dr_total["indicatorRejQty"] = Total_indicatorRejQty;

            dr_total["TotalQty"] = Total_ActualQty + Total_Rej;
            dr_total["ActualHR"] = Math.Round(Total_ActualTime / 3600, 2);
            dr_total["TargetQty"] = Total_TargetQty;
            dr_total["TargetHR"] = 12 * 8;
            dr_total["RejRate"] = TotalRejRate.ToString() + "%";
            
            dtProduct.Rows.Add(dr_total);
            //=== dr total ===//
            #endregion


            #region add all type
            List<string> listType = new List<string>();

            listType.Add("PASDL 305B");
            listType.Add("PASDL 336B/291B");
            listType.Add("SWS/785A");
            listType.Add("SWS 440A/443A");
            listType.Add("Chrysler JL Dual");
            listType.Add("Chrysler JL Singel");
            listType.Add("Other");


            foreach (string type in listType)
            {
                DataRow[] drArr = dtProduct.Select(" ProductType = '" + type + "'");
                if (drArr.Length == 0)
                {
                    DataRow dr = dtProduct.NewRow();
                    dr["SN"] = GetSN(type);
                    dr["ProductType"] = type;
                    dr["ManPower"] = ManPower;
                    dr["Attendance"] = Attendance;
                    dr["AttendRate"] = AttendRate;


                    dr["TargetQty"] = "";
                    dr["ActualQty"] = "";
                    dr["TotalQty"] = "";
                    dr["ActualHR"] = "";
                    dr["TargetHR"] = "";

                    dr["bezelRejQty"] = "";
                    dr["coverRejQty"] = "";
                    dr["escRejQty"] = "";
                    dr["btnRejQty"] = "";
                    dr["indicatorRejQty"] = "";
                    dr["Utilization"] = "";


                    dtProduct.Rows.Add(dr.ItemArray);
                }
                else
                {
                    drArr[0]["ManPower"] = ManPower;
                    drArr[0]["Attendance"] = Attendance;
                    drArr[0]["AttendRate"] = AttendRate;
                    drArr[0]["Utilization"] = Utilization;
                }
            }
            #endregion


            //order by sn
            dtProduct = dtProduct.Select("", "SN asc").CopyToDataTable();


            return dtProduct;
        }
        
        private void initEditStyleAssy()
        {

            this.dgAssyDay.Columns[4].Visible = false;
            this.dgAssyDay.Columns[5].Visible = false;
            this.dgAssyDay.Columns[6].Visible = false;
            this.dgAssyDay.Columns[7].Visible = false;
            this.dgAssyDay.Columns[8].Visible = false;
            this.dgAssyDay.Columns[9].Visible = false;
            this.dgAssyDay.Columns[10].Visible = false;
            this.dgAssyDay.Columns[11].Visible = false;
            this.dgAssyDay.Columns[12].Visible = false;
            this.dgAssyDay.Columns[13].Visible = false;
            this.dgAssyDay.Columns[14].Visible = false;
            this.dgAssyDay.Columns[15].Visible = false;

            this.dgAssyDay.Columns[16].Visible = true;
            this.dgAssyDay.Columns[17].Visible = true;
            this.dgAssyDay.Columns[18].Visible = true;
            this.dgAssyDay.Columns[19].Visible = true;
            this.dgAssyDay.Columns[20].Visible = true;
            this.dgAssyDay.Columns[21].Visible = true;
            this.dgAssyDay.Columns[22].Visible = true;
            this.dgAssyDay.Columns[23].Visible = true;
            this.dgAssyDay.Columns[24].Visible = true;
            this.dgAssyDay.Columns[25].Visible = true;



            int rowSpan = this.dgAssyDay.Items.Count;

            foreach (DataGridItem item in this.dgAssyDay.Items)
            {

                if (item.ItemIndex == 0)
                {
                    item.Cells[1].RowSpan = rowSpan; //ManPower
                    item.Cells[2].RowSpan = rowSpan; //Attendance
                    item.Cells[3].RowSpan = rowSpan; //AttendRate
                    item.Cells[16].RowSpan = rowSpan; //TargetHRS edit box



                    #region  style for Cell
                    item.Cells[1].BackColor = System.Drawing.Color.White;
                    item.Cells[2].BackColor = System.Drawing.Color.White;
                    item.Cells[3].BackColor = System.Drawing.Color.White;
                    item.Cells[16].BackColor = System.Drawing.Color.White;


                    item.Cells[1].BorderStyle = BorderStyle.Solid;
                    item.Cells[2].BorderStyle = BorderStyle.Solid;
                    item.Cells[3].BorderStyle = BorderStyle.Solid;
                    item.Cells[16].BorderStyle = BorderStyle.Solid;


                    item.Cells[1].BorderWidth = 1;
                    item.Cells[2].BorderWidth = 1;
                    item.Cells[3].BorderWidth = 1;
                    item.Cells[16].BorderWidth = 1;


                    item.Cells[1].BorderColor = System.Drawing.Color.LightGray;
                    item.Cells[2].BorderColor = System.Drawing.Color.LightGray;
                    item.Cells[3].BorderColor = System.Drawing.Color.LightGray;
                    item.Cells[16].BorderColor = System.Drawing.Color.LightGray;

                    #endregion  CSS for Cell
                }
                else
                {
                    item.Cells[1].Visible = false;
                    item.Cells[2].Visible = false;
                    item.Cells[3].Visible = false;
                    item.Cells[16].Visible = false;
                }



                //total row  back color
                if (item.Cells[0].Text == "Day Shift Total" || item.Cells[0].Text == "Night Shift Total")
                {
                    item.BackColor = System.Drawing.Color.SandyBrown;
                }

                if (item.Cells[0].Text.Contains("Unknown Type"))
                {
                    item.ForeColor = System.Drawing.Color.Red;
                }

            }


            foreach (DataGridItem item in this.dgAssyDay.Items)
            {
                if (item.Cells[0].Text.Contains("Total"))
                {
                    item.Visible = false;
                    continue;
                }


                ((TextBox)item.Cells[16].FindControl("txtTargetHR")).Text = item.Cells[4].Text == "&nbsp;" ? "" : item.Cells[4].Text;
                ((TextBox)item.Cells[17].FindControl("txtActualHR")).Text = item.Cells[5].Text == "&nbsp;" ? "" : item.Cells[5].Text;
                ((TextBox)item.Cells[18].FindControl("txtTargetQty")).Text = item.Cells[6].Text == "&nbsp;" ? "" : item.Cells[6].Text;
                ((TextBox)item.Cells[19].FindControl("txtTotalQty")).Text = item.Cells[7].Text == "&nbsp;" ? "" : item.Cells[7].Text;
                ((TextBox)item.Cells[20].FindControl("txtActualQty")).Text = item.Cells[8].Text == "&nbsp;" ? "" : item.Cells[8].Text;
                ((TextBox)item.Cells[21].FindControl("txtRejBezel")).Text = item.Cells[9].Text == "&nbsp;" ? "" : item.Cells[9].Text;
                ((TextBox)item.Cells[22].FindControl("txtRejectCover")).Text = item.Cells[10].Text == "&nbsp;" ? "" : item.Cells[10].Text;
                ((TextBox)item.Cells[23].FindControl("txtRejectESC")).Text = item.Cells[11].Text == "&nbsp;" ? "" : item.Cells[11].Text;
                ((TextBox)item.Cells[24].FindControl("txtRejectBtn")).Text = item.Cells[12].Text == "&nbsp;" ? "" : item.Cells[12].Text;
                ((TextBox)item.Cells[25].FindControl("txtRejectIndicator")).Text = item.Cells[13].Text == "&nbsp;" ? "" : item.Cells[13].Text;

            }

        }



        #endregion

        
    }
}