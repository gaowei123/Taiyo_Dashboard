using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace DashboardTTS.Webform.Laser
{
    public partial class LaserMachineDownTimeForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {

                    SetTimeDDL();

                    SetMachineDDL();

                    this.txt_Date.Enabled = false;
                    this.txt_Date.Text = DateTime.Now.ToString("dd/MM/yyyy");

                    this.infDch_Start.CalendarLayout.SelectedDate = DateTime.Now.Date;
                    this.infDch_Start.Value = DateTime.Now.Date;

                    this.infDch_End.CalendarLayout.SelectedDate = DateTime.Now.Date;
                    this.infDch_End.Value = DateTime.Now.Date;


                    #region show detail
                   


                    Common.Class.Model.LMMSMachineDownTime_Model model = (Common.Class.Model.LMMSMachineDownTime_Model)Session["LMMSMachineDownTime_Model"];
                    if (model != null)
                    {
                        Common.Class.BLL.LMMSMachineDownTime_BLL bll = new Common.Class.BLL.LMMSMachineDownTime_BLL();
                        Common.Class.Model.LMMSMachineDownTime_Model modelNew = bll.GetModel(model.machineID, model.date, model.cause);

                        if (model != null)
                        {
                            this.ddl_machineNo.SelectedValue = modelNew.machineID.Replace("Machine", "");
                            this.txt_Date.Text = modelNew.date.ToString("dd/MM/yyyy");


                            this.infDch_Start.CalendarLayout.SelectedDate = modelNew.startTime;
                            this.infDch_Start.Value = modelNew.startTime;
                            this.infDch_End.CalendarLayout.SelectedDate = modelNew.stopTime;
                            this.infDch_End.Value = modelNew.stopTime;
                            this.ddl_Start_hh.SelectedValue = modelNew.startTime.Hour.ToString("00");
                            this.ddl_Start_mm.SelectedValue = modelNew.startTime.Minute.ToString("00");
                            this.ddl_End_hh.SelectedValue = modelNew.stopTime.Hour.ToString("00");
                            this.ddl_End_mm.SelectedValue = modelNew.stopTime.Minute.ToString("00");

                            

                            this.txt_PartRunning.Text = modelNew.partRunning;
                            this.txt_BreakDownCause.Text = modelNew.cause;
                            this.txt_Action.Text = modelNew.action;
                        }
                        else
                        {
                            Common.CommFunctions.ShowMessage(this.Page, "Can not get detail info !");
                        }

                        this.btn_submit.Visible = false;
                        this.btn_cancel.Visible = false;
                    }
                    #endregion

                }
            }
            catch (Exception ee)
            {
            }
        }

      

        protected void btn_submit_Click(object sender, EventArgs e)
        {
            try
            {
                #region check UI value
                if (this.ddl_machineNo.SelectedValue == "")
                {
                    Common.CommFunctions.ShowMessage(this.Page, "Please choose Machine ID");
                    return;
                }

                if (this.ddl_Start_hh.SelectedValue == "")
                {
                    Common.CommFunctions.ShowMessage(this.Page, "Please choose start time");
                    return;
                }
                if (this.ddl_Start_mm.SelectedValue == "")
                {
                    Common.CommFunctions.ShowMessage(this.Page, "Please choose start time");
                    return;
                }
                if (this.ddl_End_hh.SelectedValue == "")
                {
                    Common.CommFunctions.ShowMessage(this.Page, "Please choose end time");
                    return;
                }
                if (this.ddl_End_mm.SelectedValue == "")
                {
                    Common.CommFunctions.ShowMessage(this.Page, "Please choose end time");
                    return;
                }
                
                //if (txt_PartRunning.Text == "")
                //{
                //    Common.CommFunctions.ShowMessage(this.Page, "Please input part running");
                //    this.txt_PartRunning.Focus();
                //    return;
                //}

                if (this.txt_BreakDownCause.Text == "")
                {
                    Common.CommFunctions.ShowMessage(this.Page, "Please input break down cause");
                    this.txt_BreakDownCause.Focus();
                    return;
                }

                if (this.txt_Action.Text == "")
                {
                    Common.CommFunctions.ShowMessage(this.Page, "Please input action");
                    this.txt_Action.Focus();
                    return;
                }


                DateTime StartTime = this.infDch_Start.CalendarLayout.SelectedDate.Date;
                StartTime =  StartTime.AddHours(int.Parse(this.ddl_Start_hh.SelectedValue));
                StartTime =  StartTime.AddMinutes(int.Parse(this.ddl_Start_mm.SelectedValue));

                DateTime EndTime = this.infDch_End.CalendarLayout.SelectedDate.Date;
                EndTime = EndTime.AddHours(int.Parse(this.ddl_End_hh.SelectedValue));
                EndTime = EndTime.AddMinutes(int.Parse(this.ddl_End_mm.SelectedValue));

                if (EndTime < StartTime)
                {
                    Common.CommFunctions.ShowMessage(this.Page, "End Time can not be earlier than start time!");
                    this.ddl_End_hh.SelectedValue = "";
                    this.ddl_End_mm.SelectedValue = "";
                    return;
                }

                #endregion


                string RootPath = Server.MapPath("../../Attachment/");
                string fileName = MyFileUpload.PostedFile.FileName;

                if (MyFileUpload.HasFile)
                {
                    if (fileName.Split('.')[1] != "pdf" && fileName.Split('.')[1] != "PDF")
                    {
                        Common.CommFunctions.ShowMessage(this.Page, "The attachment must be pdf!");
                        return;
                    }
                   
                    DirectoryInfo dir = new DirectoryInfo(RootPath);
                    if (!dir.Exists)
                        dir.Create();

                    MyFileUpload.SaveAs(RootPath + fileName);
                }
                


                Common.Class.Model.LMMSMachineDownTime_Model model = new Common.Class.Model.LMMSMachineDownTime_Model();
           
                System.Globalization.DateTimeFormatInfo dtFormat = new System.Globalization.DateTimeFormatInfo();
                dtFormat.ShortDatePattern = "dd/MM/yyyy";
                model.date = System.Convert.ToDateTime(this.txt_Date.Text, dtFormat);
                model.machineID = this.ddl_machineNo.SelectedValue;
                model.startTime = StartTime;
                model.stopTime = EndTime;
                model.partRunning = this.txt_PartRunning.Text;

                if (MyFileUpload.HasFile)
                    model.attachmentPath = RootPath + fileName;

                model.cause = this.txt_BreakDownCause.Text;
                model.action = this.txt_Action.Text;

                Session["LMMSMachineDownTime_Model"] = model;
                

                string URL = "./Login.aspx?Department="+StaticRes.Global.Department.Laser+"&commandType=DownTimeRecord";
                Response.Redirect(URL, false);

            }
            catch (Exception ee)
            {

                throw;
            }
        }

        protected void btn_cancel_Click(object sender, EventArgs e)
        {
            string strJS = "if (confirm('Your action will not be saved, are you sure?') == true) { ";
            strJS += "window.location.href = \"./LaserMachineDownTimeList.aspx\";  } ";

            ClientScript.RegisterStartupScript(Page.GetType(), "", strJS, true);
        }




        void SetTimeDDL()
        {
            this.ddl_Start_hh.Items.Clear();
            this.ddl_Start_mm.Items.Clear();
            this.ddl_End_hh.Items.Clear();
            this.ddl_End_mm.Items.Clear();

            ListItem LiStartHH = new ListItem();
            LiStartHH.Text = "";
            LiStartHH.Value = "";

            ListItem LiStartMM = new ListItem();
            LiStartMM.Text = "";
            LiStartMM.Value = "";
            
            ListItem LiEndHH = new ListItem();
            LiEndHH.Text = "";
            LiEndHH.Value = "";

            ListItem LiEndMM = new ListItem();
            LiEndMM.Text = "";
            LiEndMM.Value = "";
            

            this.ddl_Start_hh.Items.Add(LiStartHH);
            this.ddl_Start_mm.Items.Add(LiStartMM);
            this.ddl_End_hh.Items.Add(LiEndHH);
            this.ddl_End_mm.Items.Add(LiEndMM);

            for (int i = 0; i <= 60; i++)
            {
                LiStartHH = new ListItem();
                LiStartMM = new ListItem();
                LiEndHH = new ListItem();
                LiEndMM = new ListItem();
                

                LiStartHH.Text = i < 10 ? "0" + i.ToString() : i.ToString();
                LiStartHH.Value = i < 10 ? "0" + i.ToString() : i.ToString();

                LiStartMM.Text = i < 10 ? "0" + i.ToString() : i.ToString();
                LiStartMM.Value = i < 10 ? "0" + i.ToString() : i.ToString();

                LiEndHH.Text = i < 10 ? "0" + i.ToString() : i.ToString();
                LiEndHH.Value = i < 10 ? "0" + i.ToString() : i.ToString();

                LiEndMM.Text = i < 10 ? "0" + i.ToString() : i.ToString();
                LiEndMM.Value = i < 10 ? "0" + i.ToString() : i.ToString();



                if (i <=24 )
                {
                    this.ddl_Start_hh.Items.Add(LiStartHH);
                    this.ddl_End_hh.Items.Add(LiEndHH);
                }

                this.ddl_Start_mm.Items.Add(LiStartMM);
                this.ddl_End_mm.Items.Add(LiEndMM);
            }
        }


        void SetStartHH()
        {

        }





        void SetMachineDDL()
        {
            this.ddl_machineNo.Items.Clear();

            ListItem li = new ListItem();
            li.Text = "";
            li.Value = "";
            ddl_machineNo.Items.Add(li);

            for (int i = 1; i < 9; i++)
            {
                li = new ListItem();
                li.Text = "No." + i.ToString();
                li.Value = i.ToString();
                ddl_machineNo.Items.Add(li);
            }
        }

    
    }
}