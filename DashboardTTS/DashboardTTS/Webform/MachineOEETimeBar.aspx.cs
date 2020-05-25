using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
namespace DashboardTTS.Webform
{
    public partial class MachineOEETimeBar : System.Web.UI.Page
    {

        private class SearchParaCls
        {
            public string PartNo;
            public string MachineID;
            public DateTime dateFrom;
            public DateTime dateTo;
            public string JobID;
        }
     


        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {

                    this.txtDateFrom.Text = DateTime.Now.Date.ToString("yyyy-MM-dd");
                    this.txtDateTo.Text = DateTime.Now.Date.ToString("yyyy-MM-dd");
              

                    SearchParaCls sp = new SearchParaCls();
                    sp.dateFrom = DateTime.Now.Date;
                    sp.dateFrom = DateTime.Now.Date;
                    sp.MachineID = "";

                    this.WebUserControlTimeBar1.Visible = false;
                    this.WebUserControlTimeBar2.Visible = false;
                    this.WebUserControlTimeBar3.Visible = false;
                    this.WebUserControlTimeBar4.Visible = false;
                    this.WebUserControlTimeBar5.Visible = false;
                    this.WebUserControlTimeBar6.Visible = false;
                    this.WebUserControlTimeBar7.Visible = false;
                    this.WebUserControlTimeBar8.Visible = false;


                    btnGenerate_Click(new object(), new EventArgs());
                }
            }
            catch (Exception ex)
            {

            }
        }



        protected void btnGenerate_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime dTimeFrom = DateTime.Parse(this.txtDateFrom.Text).AddHours(8);
                DateTime dTimeTo = DateTime.Parse(this.txtDateTo.Text).AddHours(8);
                string sMachineNo = ddlMachineNo.SelectedItem.Value;
              

                SearchParaCls sp = new SearchParaCls();
                sp.dateFrom = dTimeFrom;
                sp.dateTo = dTimeTo;
                sp.MachineID = sMachineNo;
             

                Dictionary<string, Dictionary<DateTime, StaticRes.Global.StatusType>> dtMacList = new Dictionary<string, Dictionary<DateTime, StaticRes.Global.StatusType>>();
                for (int i = 1; i < 9; i++)
                {
                    dtMacList.Add(i.ToString(), null);
                }

                //get OEE data
                Common.BLL.LMMSEventLog_BLL EventLog = new Common.BLL.LMMSEventLog_BLL();
                Dictionary<DateTime, StaticRes.Global.StatusType> dPoints = new Dictionary<DateTime, StaticRes.Global.StatusType>();

               
                if (sMachineNo == "")
                {
                    for (int i = 1; i < 9; i++)
                    {
                        dPoints = new Dictionary<DateTime, StaticRes.Global.StatusType>();
                        dPoints = EventLog.getOEE(dTimeFrom, dTimeTo, i.ToString(), "",StaticRes.Global.Shift.ALL,"",false);

                        if (dPoints == null || dPoints.Count == 0)
                        {

                        }
                        else
                        {
                            dtMacList[i.ToString()] = dPoints;
                        }

                    }
                }
                else
                {
                    dPoints = new Dictionary<DateTime, StaticRes.Global.StatusType>();
                    dPoints = EventLog.getOEE(dTimeFrom, dTimeTo, sMachineNo, "", StaticRes.Global.Shift.ALL,"",false);

                    if (dPoints == null || dPoints.Count == 0)
                    {

                    }
                    else
                    {
                        dtMacList[sMachineNo] = dPoints;
                    }
                }

           

                ChartDisplay_Job(dtMacList, sp);

            }
            catch (Exception ex)
            {
                DBHelp.Reports.LogFile.DebugLog("AUTOCODE", "NameSpace: MachineOEETimeBar.aspx.cs", "Class:MachineOEETimeBar", "Function:	btnGenerate_Click " + "TableName:LMMSEventLog", ex.ToString());
            }
        }


        private void ChartDisplay_Job(Dictionary<string, Dictionary<DateTime, StaticRes.Global.StatusType>> dtList, SearchParaCls sp)
        {
            try
            {
                foreach (KeyValuePair<string, Dictionary<DateTime, StaticRes.Global.StatusType>> key in dtList)
                {
                    if (key.Value == null)
                    {
                        #region  set visible false

                        switch (key.Key)
                        {
                            case ("1"):
                                {
                                    this.WebUserControlTimeBar1.Visible = false;
                                    break;
                                }
                            case ("2"):
                                {
                                    this.WebUserControlTimeBar2.Visible = false;
                                    break;
                                }
                            case ("3"):
                                {
                                    this.WebUserControlTimeBar3.Visible = false;
                                    break;
                                }
                            case ("4"):
                                {
                                    this.WebUserControlTimeBar4.Visible = false;
                                    break;
                                }
                            case ("5"):
                                {
                                    this.WebUserControlTimeBar5.Visible = false;
                                    break;
                                }
                            case ("6"):
                                {
                                    this.WebUserControlTimeBar6.Visible = false;
                                    break;
                                }
                            case ("7"):
                                {
                                    this.WebUserControlTimeBar7.Visible = false;
                                    break;
                                }
                            case ("8"):
                                {
                                    this.WebUserControlTimeBar8.Visible = false;
                                    break;
                                }
                        }
                        #endregion
                    }
                    else
                    {
                        sp.MachineID = key.Key;
                        switch (key.Key)
                        {
                            case ("1"):
                                {
                                    this.WebUserControlTimeBar1.Visible = true;
                                    sp.MachineID = "1";
                                    setChart1(key.Value, sp);
                                    break;
                                }
                            case ("2"):
                                {
                                    this.WebUserControlTimeBar2.Visible = true;
                                    sp.MachineID = "2";
                                    setChart2(key.Value, sp);
                                    break;
                                }
                            case ("3"):
                                {
                                    this.WebUserControlTimeBar3.Visible = true;
                                    sp.MachineID = "3";
                                    setChart3(key.Value, sp);
                                    break;
                                }
                            case ("4"):
                                {
                                    this.WebUserControlTimeBar4.Visible = true;
                                    sp.MachineID = "4";
                                    setChart4(key.Value, sp);
                                    break;
                                }
                            case ("5"):
                                {
                                    this.WebUserControlTimeBar5.Visible = true;
                                    sp.MachineID = "5";
                                    setChart5(key.Value, sp);
                                    break;
                                }
                            case ("6"):
                                {
                                    this.WebUserControlTimeBar6.Visible = true;
                                    sp.MachineID = "6";
                                    setChart6(key.Value, sp);
                                    break;
                                }
                            case ("7"):
                                {
                                    this.WebUserControlTimeBar7.Visible = true;
                                    sp.MachineID = "7";
                                    setChart7(key.Value, sp);
                                    break;
                                }
                            case ("8"):
                                {
                                    this.WebUserControlTimeBar8.Visible = true;
                                    sp.MachineID = "8";
                                    setChart8(key.Value, sp);
                                    break;
                                }
                        }
                    }
                }
            }
            catch (Exception ee)
            {
                DBHelp.Reports.LogFile.Log("MachineOEETimeBar_debug", " Execption:"+ee.ToString());
            }

        }

       

        private void setChart1(Dictionary<DateTime, StaticRes.Global.StatusType> dPoints, SearchParaCls sp)
        {
            #region userControl
            WebUserControlTimeBar1.MachineNo = "Machine #" + sp.MachineID;
            WebUserControlTimeBar1.StartTime = sp.dateFrom;
            WebUserControlTimeBar1.EndTime = sp.dateTo.AddDays(1);



            UserControl.WebUserControlTimeBar.clsBarPoint pPos = new UserControl.WebUserControlTimeBar.clsBarPoint();
            foreach (KeyValuePair<DateTime, StaticRes.Global.StatusType> key in dPoints)
            {
                
                //if (key.Key >= DateTime.Parse(infDchFrom.CalendarLayout.SelectedDate.ToString()).AddHours(8))
                //{
                    pPos = new UserControl.WebUserControlTimeBar.clsBarPoint();
                    pPos.PointTime = key.Key;
                    pPos.OeeStatus = key.Value;
              
                    WebUserControlTimeBar1.Points.Add(pPos);
                //}
            }
         

            WebUserControlTimeBar1.reflash();
         
            #endregion
        }

        private void setChart2(Dictionary<DateTime, StaticRes.Global.StatusType> dPoints, SearchParaCls sp)
        {
            #region userControl
            WebUserControlTimeBar2.MachineNo = "Machine #" + sp.MachineID;
            WebUserControlTimeBar2.StartTime = sp.dateFrom;
            WebUserControlTimeBar2.EndTime = sp.dateTo.AddDays(1);
          

            UserControl.WebUserControlTimeBar.clsBarPoint pPos = new UserControl.WebUserControlTimeBar.clsBarPoint();
            foreach (KeyValuePair<DateTime, StaticRes.Global.StatusType> key in dPoints)
            {
                pPos = new UserControl.WebUserControlTimeBar.clsBarPoint();
                pPos.PointTime = key.Key;
                pPos.OeeStatus = key.Value;
               
                WebUserControlTimeBar2.Points.Add(pPos);
            }

            WebUserControlTimeBar2.reflash();
          
            #endregion

        }
        private void setChart3(Dictionary<DateTime, StaticRes.Global.StatusType> dPoints, SearchParaCls sp)
        {
            #region userControl
            WebUserControlTimeBar3.MachineNo = "Machine #" + sp.MachineID;
            WebUserControlTimeBar3.StartTime = sp.dateFrom;
            WebUserControlTimeBar3.EndTime = sp.dateTo.AddDays(1);
          

            UserControl.WebUserControlTimeBar.clsBarPoint pPos = new UserControl.WebUserControlTimeBar.clsBarPoint();
            foreach (KeyValuePair<DateTime, StaticRes.Global.StatusType> key in dPoints)
            {
                pPos = new UserControl.WebUserControlTimeBar.clsBarPoint();
                pPos.PointTime = key.Key;
                pPos.OeeStatus = key.Value;
               
                WebUserControlTimeBar3.Points.Add(pPos);
            }

            WebUserControlTimeBar3.reflash();
          
            #endregion

        }
        private void setChart4(Dictionary<DateTime, StaticRes.Global.StatusType> dPoints, SearchParaCls sp)
        {
            #region userControl
            WebUserControlTimeBar4.MachineNo = "Machine #" + sp.MachineID;
            WebUserControlTimeBar4.StartTime = sp.dateFrom;
            WebUserControlTimeBar4.EndTime = sp.dateTo.AddDays(1);
           

            UserControl.WebUserControlTimeBar.clsBarPoint pPos = new UserControl.WebUserControlTimeBar.clsBarPoint();
            foreach (KeyValuePair<DateTime, StaticRes.Global.StatusType> key in dPoints)
            {
                pPos = new UserControl.WebUserControlTimeBar.clsBarPoint();
                pPos.PointTime = key.Key;
                pPos.OeeStatus = key.Value;
             
                WebUserControlTimeBar4.Points.Add(pPos);
            }

            WebUserControlTimeBar4.reflash();
         
            #endregion

        }
        private void setChart5(Dictionary<DateTime, StaticRes.Global.StatusType> dPoints, SearchParaCls sp)
        {
            #region userControl
            WebUserControlTimeBar5.MachineNo = "Machine #" + sp.MachineID;
            WebUserControlTimeBar5.StartTime = sp.dateFrom;
            WebUserControlTimeBar5.EndTime = sp.dateTo.AddDays(1);
            
            UserControl.WebUserControlTimeBar.clsBarPoint pPos = new UserControl.WebUserControlTimeBar.clsBarPoint();
            foreach (KeyValuePair<DateTime, StaticRes.Global.StatusType> key in dPoints)
            {
                pPos = new UserControl.WebUserControlTimeBar.clsBarPoint();
                pPos.PointTime = key.Key;
                pPos.OeeStatus = key.Value;
             
                WebUserControlTimeBar5.Points.Add(pPos);
            }

            WebUserControlTimeBar5.reflash();
          
            #endregion

        }
        private void setChart6(Dictionary<DateTime, StaticRes.Global.StatusType> dPoints, SearchParaCls sp)
        {
            #region userControl
            WebUserControlTimeBar6.MachineNo = "Machine #" + sp.MachineID;
            WebUserControlTimeBar6.StartTime = sp.dateFrom;
            WebUserControlTimeBar6.EndTime = sp.dateTo.AddDays(1);
           

            UserControl.WebUserControlTimeBar.clsBarPoint pPos = new UserControl.WebUserControlTimeBar.clsBarPoint();
            foreach (KeyValuePair<DateTime, StaticRes.Global.StatusType> key in dPoints)
            {
                pPos = new UserControl.WebUserControlTimeBar.clsBarPoint();
                pPos.PointTime = key.Key;
                pPos.OeeStatus = key.Value;
              
                WebUserControlTimeBar6.Points.Add(pPos);
            }

            WebUserControlTimeBar6.reflash();
          
            #endregion

        }
        private void setChart7(Dictionary<DateTime, StaticRes.Global.StatusType> dPoints, SearchParaCls sp)
        {
            #region userControl
            WebUserControlTimeBar7.MachineNo = "Machine #" + sp.MachineID;
            WebUserControlTimeBar7.StartTime = sp.dateFrom;
            WebUserControlTimeBar7.EndTime = sp.dateTo.AddDays(1);
          

            UserControl.WebUserControlTimeBar.clsBarPoint pPos = new UserControl.WebUserControlTimeBar.clsBarPoint();
            foreach (KeyValuePair<DateTime, StaticRes.Global.StatusType> key in dPoints)
            {
                pPos = new UserControl.WebUserControlTimeBar.clsBarPoint();
                pPos.PointTime = key.Key;
                pPos.OeeStatus = key.Value;
                WebUserControlTimeBar7.Points.Add(pPos);
            }

            WebUserControlTimeBar7.reflash();
         
            #endregion

        }
        private void setChart8(Dictionary<DateTime, StaticRes.Global.StatusType> dPoints, SearchParaCls sp)
        {
            #region userControl
            WebUserControlTimeBar8.MachineNo = "Machine #" + sp.MachineID;
            WebUserControlTimeBar8.StartTime = sp.dateFrom;
            WebUserControlTimeBar8.EndTime = sp.dateTo.AddDays(1);
       

            UserControl.WebUserControlTimeBar.clsBarPoint pPos = new UserControl.WebUserControlTimeBar.clsBarPoint();
            foreach (KeyValuePair<DateTime, StaticRes.Global.StatusType> key in dPoints)
            {
                pPos = new UserControl.WebUserControlTimeBar.clsBarPoint();
                pPos.PointTime = key.Key;
                pPos.OeeStatus = key.Value;
                WebUserControlTimeBar8.Points.Add(pPos);
            }

            WebUserControlTimeBar8.reflash();
        
            #endregion
        }

    }
}