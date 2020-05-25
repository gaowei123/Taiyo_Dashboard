using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;

namespace DashboardTTS.Webform.Laser
{
    public partial class BUY_OFF : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

                SetUserDDL();
                SetMachineDDL();
               

                SetRateDDL();
                SetFrequencyDDL();

                this.txt_Job_ID.Enabled = false;
                this.txt_Part_No.Enabled = false;
                this.txt_current.Enabled = false;


                ShowBuyoffReport();




                string result = Request.QueryString["Result"] == null ? "" : Request.QueryString["Result"].ToString();
                if (result=="TRUE")
                {
                    Common.CommFunctions.ShowMessage(this.Page, "Add Success");
                }
                else if (result == "FALSE")
                {
                    Common.CommFunctions.ShowMessage(this.Page, "Add Fail");
                }
            }
        }

        protected void ddlMachineID_SelectedIndexChanged(object sender, EventArgs e)
        {
            string MachineID = this.ddlMachineID.SelectedValue;

            if (string.IsNullOrEmpty(MachineID))
            {
                return;
            }

           
            Common.BLL.LMMSWatchDog_His_BLL bll = new Common.BLL.LMMSWatchDog_His_BLL();
            DataTable dt = bll.GetMachineInfo(MachineID);

            if (dt == null || dt.Rows.Count == 0)
            {
                Common.CommFunctions.ShowMessage(this.Page, "Exception !!  Can not find any data from database LMMSWatchdog about Machine" + MachineID);
                return;
            }

            //get part, job, current info  automatically
            string PartNumber = dt.Rows[0]["partNumber"].ToString();
            string JobNumber = dt.Rows[0]["jobNumber"].ToString();
            string Current = dt.Rows[0]["CurrentPower"].ToString();

            //1. check job whether scan in machineid client
            if (string.IsNullOrEmpty(PartNumber) || string.IsNullOrEmpty(JobNumber))
            {
                StringBuilder strJS = new StringBuilder();
                strJS.Append(@"if (confirm('Machine" + MachineID + " has not scan in any job, Do you want to key in Manually?') == false) { ");
                strJS.Append(" window.location.href = \"./Buy_Off_List.aspx\";  } ");
                ClientScript.RegisterStartupScript(Page.GetType(), "", strJS.ToString(), true);
                
                this.txt_Part_No.Enabled = true;
                this.txt_Part_No.Text = "";
                this.txt_current.Enabled = true;
                this.txt_current.Text = "";
                this.txt_Job_ID.Enabled = true;
                this.txt_Job_ID.Text = "JOT" + DateTime.Now.Year.ToString().Substring(2, 2);
                this.txt_Job_ID.Focus();
            }
            else
            {
                this.txt_Part_No.Text = PartNumber;
                this.txt_Part_No.Enabled = false;
                this.txt_Job_ID.Text = JobNumber;
                this.txt_Job_ID.Enabled = false;
                this.txt_current.Text = Current;
                this.txt_current.Enabled = false;

                txt_Job_ID_TextChanged(new object(), new EventArgs());
            }
        }

        protected void txt_Job_ID_TextChanged(object sender, EventArgs e)
        {
            string JobNumber = this.txt_Job_ID.Text.ToUpper();

            if (string.IsNullOrEmpty(JobNumber))
            {
                InitTbJob();
                this.txt_Job_ID.Text = "";
                return;
            }

            //check format  JOT1900002393  13位
            if ( !JobNumber.Contains("JOT")  || JobNumber.Length !=13)
            {
                InitTbJob();
                Common.CommFunctions.ShowMessage(this.Page, "Job number format error, Please Check and key in again !");
                return;
            }

            //check inventory
            Common.Class.BLL.LMMSInventoty_BLL bll_Inventory = new Common.Class.BLL.LMMSInventoty_BLL();
            if (!bll_Inventory.Exist(JobNumber))
            {
                InitTbJob();
                Common.CommFunctions.ShowMessage(this.Page, "Warnning!! Please scan this job in Job Order first.");
                return;
            }

            //check watchdogshift  totalpass+totalfail 
            Common.BLL.LMMSWatchLog_BLL bll_WatchLog = new Common.BLL.LMMSWatchLog_BLL();
            if (bll_WatchLog.IsJobFinished(JobNumber))
            {
                InitTbJob();
                Common.CommFunctions.ShowMessage(this.Page, "Warnning!! The Job is already finished, Can not add to Buyoff Report.");
                return;
            }

            //check repeat
            Common.Class.BLL.LMMSBUYOFFLIST_BLL bll_Buyoff = new Common.Class.BLL.LMMSBUYOFFLIST_BLL();
            if (bll_Buyoff.Exist(JobNumber))
            {
                InitTbJob();
                Common.CommFunctions.ShowMessage(this.Page, "Warnning!! The Job is already added, Can not add again!.");
                return;
            }




            //Get Part, current
            Common.BLL.LMMSWatchDog_His_BLL BLL_Dog = new Common.BLL.LMMSWatchDog_His_BLL();

            DataTable dt = BLL_Dog.GetJobInfo(JobNumber);
            string PartNo = dt.Rows[0]["partNumber"].ToString();
            string Current = dt.Rows[0]["CurrentPower"].ToString();
            string LotNo = dt.Rows[0]["lotNo"].ToString();
            string MFGDate = dt.Rows[0]["MFGDate"].ToString();
            string LotQty = dt.Rows[0]["lotqty"].ToString();

            this.txt_Part_No.Text = PartNo;
            this.txt_current.Text = Current == "" ? "NA" : Current + "%";
       
           
            
        }
        
        #region Set Check Box
        protected void cb_Black_Mark_OK_CheckedChanged(object sender, EventArgs e)
        {
            this.cb_Black_Mark_NG.Checked = cb_Black_Mark_OK.Checked ? false : true;
            SetOKColor(cb_Black_Mark_OK);
            SetNGColor(cb_Black_Mark_NG);
        }

        protected void cb_Black_Mark_NG_CheckedChanged(object sender, EventArgs e)
        {
            this.cb_Black_Mark_OK.Checked = cb_Black_Mark_NG.Checked ? false : true;
            SetOKColor(cb_Black_Mark_OK);
            SetNGColor(cb_Black_Mark_NG);
        }

        protected void cb_Black_Dot_OK_CheckedChanged(object sender, EventArgs e)
        {
            this.cb_Black_Dot_NG.Checked = cb_Black_Dot_OK.Checked ? false : true;
            SetOKColor(cb_Black_Dot_OK);
            SetNGColor(cb_Black_Dot_NG);
        }

        protected void cb_Black_Dot_NG_CheckedChanged(object sender, EventArgs e)
        {
            this.cb_Black_Dot_OK.Checked = cb_Black_Dot_NG.Checked ? false : true;
            SetOKColor(cb_Black_Dot_OK);
            SetNGColor(cb_Black_Dot_NG);
        }

        protected void cb_Pin_Hole_OK_CheckedChanged(object sender, EventArgs e)
        {
            this.cb_Pin_Hole_NG.Checked = cb_Pin_Hole_OK.Checked ? false : true;
            SetOKColor(cb_Pin_Hole_OK);
            SetNGColor(cb_Pin_Hole_NG);
        }

        protected void cb_Pin_Hole_NG_CheckedChanged(object sender, EventArgs e)
        {
            this.cb_Pin_Hole_OK.Checked = cb_Pin_Hole_NG.Checked ? false : true;
            SetOKColor(cb_Pin_Hole_OK);
            SetNGColor(cb_Pin_Hole_NG);
        }

        protected void cb_Jagged_OK_CheckedChanged(object sender, EventArgs e)
        {
            this.cb_Jagged_NG.Checked = cb_Jagged_OK.Checked ? false : true;
            SetOKColor(cb_Jagged_OK);
            SetNGColor(cb_Jagged_NG);
        }

        protected void cb_Jagged_NG_CheckedChanged(object sender, EventArgs e)
        {
            this.cb_Jagged_OK.Checked = cb_Jagged_NG.Checked ? false : true;
            SetOKColor(cb_Jagged_OK);
            SetNGColor(cb_Jagged_NG);
        }

        protected void cb_Check_Guled_OK_CheckedChanged(object sender, EventArgs e)
        {
            this.cb_Check_Guled_NG.Checked = cb_Check_Guled_OK.Checked ? false : true;
            SetOKColor(cb_Check_Guled_OK);
            SetNGColor(cb_Check_Guled_NG);
        }

        protected void cb_Check_Guled_NG_CheckedChanged(object sender, EventArgs e)
        {
            this.cb_Check_Guled_OK.Checked = cb_Check_Guled_NG.Checked ? false : true;
            SetOKColor(cb_Check_Guled_OK);
            SetNGColor(cb_Check_Guled_NG);
        }

        protected void cb_Navitas_OK_CheckedChanged(object sender, EventArgs e)
        {
            this.cb_Navitas_NG.Checked = cb_Navitas_OK.Checked ? false : true;
            SetOKColor(cb_Navitas_OK);
            SetNGColor(cb_Navitas_NG);
        }

        protected void cb_Navitas_NG_CheckedChanged(object sender, EventArgs e)
        {
            this.cb_Navitas_OK.Checked = cb_Navitas_NG.Checked ? false : true;
            SetOKColor(cb_Navitas_OK);
            SetNGColor(cb_Navitas_NG);
        }

        protected void cb_Smart_Scope_OK_CheckedChanged(object sender, EventArgs e)
        {
            this.cb_Smart_Scope_NG.Checked = cb_Smart_Scope_OK.Checked ? false : true;
            SetOKColor(cb_Smart_Scope_OK);
            SetNGColor(cb_Smart_Scope_NG);
        }

        protected void cb_Smart_Scope_NG_CheckedChanged(object sender, EventArgs e)
        {
            this.cb_Smart_Scope_OK.Checked = cb_Smart_Scope_NG.Checked ? false : true;
            SetOKColor(cb_Smart_Scope_OK);
            SetNGColor(cb_Smart_Scope_NG);
        }

        protected void cb_Black_Mark_OK1_CheckedChanged(object sender, EventArgs e)
        {
            this.cb_Black_Mark_NG1.Checked = cb_Black_Mark_OK1.Checked ? false : true;
            SetOKColor(cb_Black_Mark_OK1);
            SetNGColor(cb_Black_Mark_NG1);
        }

        protected void cb_Black_Mark_NG1_CheckedChanged(object sender, EventArgs e)
        {
            this.cb_Black_Mark_OK1.Checked = cb_Black_Mark_NG1.Checked ? false : true;
            SetOKColor(cb_Black_Mark_OK1);
            SetNGColor(cb_Black_Mark_NG1);
        }

        protected void cb_Black_Dot_OK1_CheckedChanged(object sender, EventArgs e)
        {
            this.cb_Black_Dot_NG1.Checked = cb_Black_Dot_OK1.Checked ? false : true;
            SetOKColor(cb_Black_Dot_OK1);
            SetNGColor(cb_Black_Dot_NG1);
        }

        protected void cb_Black_Dot_NG1_CheckedChanged(object sender, EventArgs e)
        {
            this.cb_Black_Dot_OK1.Checked = cb_Black_Dot_NG1.Checked ? false : true;
            SetOKColor(cb_Black_Dot_OK1);
            SetNGColor(cb_Black_Dot_NG1);
        }

        protected void cb_Pin_Hole_OK1_CheckedChanged(object sender, EventArgs e)
        {
            this.cb_Pin_Hole_NG1.Checked = cb_Pin_Hole_OK1.Checked ? false : true;
            SetOKColor(cb_Pin_Hole_OK1);
            SetNGColor(cb_Pin_Hole_NG1);
        }

        protected void cb_Pin_Hole_NG1_CheckedChanged(object sender, EventArgs e)
        {
            this.cb_Pin_Hole_OK1.Checked = cb_Pin_Hole_NG1.Checked ? false : true;
            SetOKColor(cb_Pin_Hole_OK1);
            SetNGColor(cb_Pin_Hole_NG1);
        }

        protected void cb_Jagged_OK1_CheckedChanged(object sender, EventArgs e)
        {
            this.cb_Jagged_NG1.Checked = cb_Jagged_OK1.Checked ? false : true;
            SetOKColor(cb_Jagged_OK1);
            SetNGColor(cb_Jagged_NG1);
        }

        protected void cb_Jagged_NG1_CheckedChanged(object sender, EventArgs e)
        {
            this.cb_Jagged_OK1.Checked = cb_Jagged_NG1.Checked ? false : true;
            SetOKColor(cb_Jagged_OK1);
            SetNGColor(cb_Jagged_NG1);
        }

        protected void cb_Check_Guled_OK1_CheckedChanged(object sender, EventArgs e)
        {
            this.cb_Check_Guled_NG1.Checked = cb_Check_Guled_OK1.Checked ? false : true;
            SetOKColor(cb_Check_Guled_OK1);
            SetNGColor(cb_Check_Guled_NG1);
        }

        protected void cb_Check_Guled_NG1_CheckedChanged(object sender, EventArgs e)
        {
            this.cb_Check_Guled_OK1.Checked = cb_Check_Guled_NG1.Checked ? false : true;
            SetOKColor(cb_Check_Guled_OK1);
            SetNGColor(cb_Check_Guled_NG1);
        }

        protected void cb_Navitas_OK1_CheckedChanged(object sender, EventArgs e)
        {
            this.cb_Navitas_NG1.Checked = cb_Navitas_OK1.Checked ? false : true;
            SetOKColor(cb_Navitas_OK1);
            SetNGColor(cb_Navitas_NG1);
        }

        protected void cb_Navitas_NG1_CheckedChanged(object sender, EventArgs e)
        {
            this.cb_Navitas_OK1.Checked = cb_Navitas_NG1.Checked ? false : true;
            SetOKColor(cb_Navitas_OK1);
            SetNGColor(cb_Navitas_NG1);
        }

        protected void cb_Smart_Scope_OK1_CheckedChanged(object sender, EventArgs e)
        {
            this.cb_Smart_Scope_NG1.Checked = cb_Smart_Scope_OK1.Checked ? false : true;
            SetOKColor(cb_Smart_Scope_OK1);
            SetNGColor(cb_Smart_Scope_NG1);
        }

        protected void cb_Smart_Scope_NG1_CheckedChanged(object sender, EventArgs e)
        {
            this.cb_Smart_Scope_OK1.Checked = cb_Smart_Scope_NG1.Checked ? false : true;
            SetOKColor(cb_Smart_Scope_OK1);
            SetNGColor(cb_Smart_Scope_NG1);
        }

        protected void cb_Black_Mark_OK2_CheckedChanged(object sender, EventArgs e)
        {
            this.cb_Black_Mark_NG2.Checked = cb_Black_Mark_OK2.Checked ? false : true;
            SetOKColor(cb_Black_Mark_OK2);
            SetNGColor(cb_Black_Mark_NG2);
        }

        protected void cb_Black_Mark_NG2_CheckedChanged(object sender, EventArgs e)
        {
            this.cb_Black_Mark_OK2.Checked = cb_Black_Mark_NG2.Checked ? false : true;
            SetOKColor(cb_Black_Mark_OK2);
            SetNGColor(cb_Black_Mark_NG2);
        }

        protected void cb_Black_Dot_OK2_CheckedChanged(object sender, EventArgs e)
        {
            this.cb_Black_Dot_NG2.Checked = cb_Black_Dot_OK2.Checked ? false : true;
            SetOKColor(cb_Black_Dot_OK2);
            SetNGColor(cb_Black_Dot_NG2);
        }

        protected void cb_Black_Dot_NG2_CheckedChanged(object sender, EventArgs e)
        {
            this.cb_Black_Dot_OK2.Checked = cb_Black_Dot_NG2.Checked ? false : true;
            SetOKColor(cb_Black_Dot_OK2);
            SetNGColor(cb_Black_Dot_NG2);
        }

        protected void cb_Pin_Hole_OK2_CheckedChanged(object sender, EventArgs e)
        {
            this.cb_Pin_Hole_NG2.Checked = cb_Pin_Hole_OK2.Checked ? false : true;
            SetOKColor(cb_Pin_Hole_OK2);
            SetNGColor(cb_Pin_Hole_NG2);
        }

        protected void cb_Pin_Hole_NG2_CheckedChanged(object sender, EventArgs e)
        {
            this.cb_Pin_Hole_OK2.Checked = cb_Pin_Hole_NG2.Checked ? false : true;
            SetOKColor(cb_Pin_Hole_OK2);
            SetNGColor(cb_Pin_Hole_NG2);
        }

        protected void cb_Jagged_OK2_CheckedChanged(object sender, EventArgs e)
        {
            this.cb_Jagged_NG2.Checked = cb_Jagged_OK2.Checked ? false : true;
            SetOKColor(cb_Jagged_OK2);
            SetNGColor(cb_Jagged_NG2);
        }

        protected void cb_Jagged_NG2_CheckedChanged(object sender, EventArgs e)
        {
            this.cb_Jagged_OK2.Checked = cb_Jagged_NG2.Checked ? false : true;
            SetOKColor(cb_Jagged_OK2);
            SetNGColor(cb_Jagged_NG2);
        }

        protected void cb_Check_Guled_OK2_CheckedChanged(object sender, EventArgs e)
        {
            this.cb_Check_Guled_NG2.Checked = cb_Check_Guled_OK2.Checked ? false : true;
            SetOKColor(cb_Check_Guled_OK2);
            SetNGColor(cb_Check_Guled_NG2);
        }

        protected void cb_Check_Guled_NG2_CheckedChanged(object sender, EventArgs e)
        {
            this.cb_Check_Guled_OK2.Checked = cb_Check_Guled_NG2.Checked ? false : true;
            SetOKColor(cb_Check_Guled_OK2);
            SetNGColor(cb_Check_Guled_NG2);
        }

        protected void cb_Navitas_OK2_CheckedChanged(object sender, EventArgs e)
        {
            this.cb_Navitas_NG2.Checked = cb_Navitas_OK2.Checked ? false : true;
            SetOKColor(cb_Navitas_OK2);
            SetNGColor(cb_Navitas_NG2);
        }

        protected void cb_Navitas_NG2_CheckedChanged(object sender, EventArgs e)
        {
            this.cb_Navitas_OK2.Checked = cb_Navitas_NG2.Checked ? false : true;
            SetOKColor(cb_Navitas_OK2);
            SetNGColor(cb_Navitas_NG2);
        }

        protected void cb_Smart_Scope_OK2_CheckedChanged(object sender, EventArgs e)
        {
            this.cb_Smart_Scope_NG2.Checked = cb_Smart_Scope_OK2.Checked ? false : true;
            SetOKColor(cb_Smart_Scope_OK2);
            SetNGColor(cb_Smart_Scope_NG2);
        }

        protected void cb_Smart_Scope_NG2_CheckedChanged(object sender, EventArgs e)
        {
            this.cb_Smart_Scope_OK2.Checked = cb_Smart_Scope_NG2.Checked ? false : true;
            SetOKColor(cb_Smart_Scope_OK2);
            SetNGColor(cb_Smart_Scope_NG2);
        }


        protected void cb_Appera_AllOK_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked)
            {
                this.cb_Black_Mark_OK.Checked = true;
                SetOKColor(cb_Black_Mark_OK);
                this.cb_Black_Dot_OK.Checked = true;
                SetOKColor(cb_Black_Dot_OK);
                this.cb_Pin_Hole_OK.Checked = true;
                SetOKColor(cb_Pin_Hole_OK);
                this.cb_Jagged_OK.Checked = true;
                SetOKColor(cb_Jagged_OK);


                this.cb_Black_Mark_OK1.Checked = true;
                SetOKColor(cb_Black_Mark_OK1);
                this.cb_Black_Dot_OK1.Checked = true;
                SetOKColor(cb_Black_Dot_OK1);
                this.cb_Pin_Hole_OK1.Checked = true;
                SetOKColor(cb_Pin_Hole_OK1);
                this.cb_Jagged_OK1.Checked = true;
                SetOKColor(cb_Jagged_OK1);


                this.cb_Black_Mark_OK2.Checked = true;
                SetOKColor(cb_Black_Mark_OK2);
                this.cb_Black_Dot_OK2.Checked = true;
                SetOKColor(cb_Black_Dot_OK2);
                this.cb_Pin_Hole_OK2.Checked = true;
                SetOKColor(cb_Pin_Hole_OK2);
                this.cb_Jagged_OK2.Checked = true;
                SetOKColor(cb_Jagged_OK2);

                this.cb_Black_Mark_NG.Checked = false;
                SetNGColor(cb_Black_Mark_NG);
                this.cb_Black_Dot_NG.Checked = false;
                SetNGColor(cb_Black_Dot_NG);
                this.cb_Pin_Hole_NG.Checked = false;
                SetNGColor(cb_Pin_Hole_NG);
                this.cb_Jagged_NG.Checked = false;
                SetNGColor(cb_Jagged_NG);


                this.cb_Black_Mark_NG1.Checked = false;
                SetNGColor(cb_Black_Mark_NG1);
                this.cb_Black_Dot_NG1.Checked = false;
                SetNGColor(cb_Black_Dot_NG1);
                this.cb_Pin_Hole_NG1.Checked = false;
                SetNGColor(cb_Pin_Hole_NG1);
                this.cb_Jagged_NG1.Checked = false;
                SetNGColor(cb_Jagged_NG1);


                this.cb_Black_Mark_NG2.Checked = false;
                SetNGColor(cb_Black_Mark_NG2);
                this.cb_Black_Dot_NG2.Checked = false;
                SetNGColor(cb_Black_Dot_NG2);
                this.cb_Pin_Hole_NG2.Checked = false;
                SetNGColor(cb_Pin_Hole_NG2);
                this.cb_Jagged_NG2.Checked = false;
                SetNGColor(cb_Jagged_NG2);
            }
            else
            {
                this.cb_Black_Mark_OK.Checked = false;
                SetOKColor(cb_Black_Mark_OK);
                this.cb_Black_Dot_OK.Checked = false;
                SetOKColor(cb_Black_Dot_OK);
                this.cb_Pin_Hole_OK.Checked = false;
                SetOKColor(cb_Pin_Hole_OK);
                this.cb_Jagged_OK.Checked = false;
                SetOKColor(cb_Jagged_OK);


                this.cb_Black_Mark_OK1.Checked = false;
                SetOKColor(cb_Black_Mark_OK1);
                this.cb_Black_Dot_OK1.Checked = false;
                SetOKColor(cb_Black_Dot_OK1);
                this.cb_Pin_Hole_OK1.Checked = false;
                SetOKColor(cb_Pin_Hole_OK1);
                this.cb_Jagged_OK1.Checked = false;
                SetOKColor(cb_Jagged_OK1);


                this.cb_Black_Mark_OK2.Checked = false;
                SetOKColor(cb_Black_Mark_OK2);
                this.cb_Black_Dot_OK2.Checked = false;
                SetOKColor(cb_Black_Dot_OK2);
                this.cb_Pin_Hole_OK2.Checked = false;
                SetOKColor(cb_Pin_Hole_OK2);
                this.cb_Jagged_OK2.Checked = false;
                SetOKColor(cb_Jagged_OK2);
            }

        }

        protected void cb_Graph_AllOK_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked)
            {
                this.cb_Check_Guled_OK.Checked = true;
                SetOKColor(cb_Check_Guled_OK);
                this.cb_Navitas_OK.Checked = true;
                SetOKColor(cb_Navitas_OK);
                this.cb_Smart_Scope_OK.Checked = true;
                SetOKColor(cb_Smart_Scope_OK);


                this.cb_Check_Guled_OK1.Checked = true;
                SetOKColor(cb_Check_Guled_OK1);
                this.cb_Navitas_OK1.Checked = true;
                SetOKColor(cb_Navitas_OK1);
                this.cb_Smart_Scope_OK1.Checked = true;
                SetOKColor(cb_Smart_Scope_OK1);


                this.cb_Check_Guled_OK2.Checked = true;
                SetOKColor(cb_Check_Guled_OK2);
                this.cb_Navitas_OK2.Checked = true;
                SetOKColor(cb_Navitas_OK2);
                this.cb_Smart_Scope_OK2.Checked = true;
                SetOKColor(cb_Smart_Scope_OK2);

                this.cb_Check_Guled_NG.Checked = false;
                SetNGColor(cb_Check_Guled_NG);
                this.cb_Navitas_NG.Checked = false;
                SetNGColor(cb_Navitas_NG);
                this.cb_Smart_Scope_NG.Checked = false;
                SetNGColor(cb_Smart_Scope_NG);


                this.cb_Check_Guled_NG1.Checked = false;
                SetNGColor(cb_Check_Guled_NG1);
                this.cb_Navitas_NG1.Checked = false;
                SetNGColor(cb_Navitas_NG1);
                this.cb_Smart_Scope_NG1.Checked = false;
                SetNGColor(cb_Smart_Scope_NG1);


                this.cb_Check_Guled_NG2.Checked = false;
                SetNGColor(cb_Check_Guled_NG2);
                this.cb_Navitas_NG2.Checked = false;
                SetNGColor(cb_Navitas_NG2);
                this.cb_Smart_Scope_NG2.Checked = false;
                SetNGColor(cb_Smart_Scope_NG2);
            }
            else
            {
                this.cb_Check_Guled_OK.Checked = false;
                SetOKColor(cb_Check_Guled_OK);
                this.cb_Navitas_OK.Checked = false;
                SetOKColor(cb_Navitas_OK);
                this.cb_Smart_Scope_OK.Checked = false;
                SetOKColor(cb_Smart_Scope_OK);


                this.cb_Check_Guled_OK1.Checked = false;
                SetOKColor(cb_Check_Guled_OK1);
                this.cb_Navitas_OK1.Checked = false;
                SetOKColor(cb_Navitas_OK1);
                this.cb_Smart_Scope_OK1.Checked = false;
                SetOKColor(cb_Smart_Scope_OK1);


                this.cb_Check_Guled_OK2.Checked = false;
                SetOKColor(cb_Check_Guled_OK2);
                this.cb_Navitas_OK2.Checked = false;
                SetOKColor(cb_Navitas_OK2);
                this.cb_Smart_Scope_OK2.Checked = false;
                SetOKColor(cb_Smart_Scope_OK2);
            }
        }
        #endregion



        protected void btnGet_Click(object sender, EventArgs e)
        {
           
        }


        protected void btn_Confirm_Click(object sender, EventArgs e)
        {
            //check repeat
            Common.Class.BLL.LMMSBUYOFFLIST_BLL bll_Buyoff = new Common.Class.BLL.LMMSBUYOFFLIST_BLL();
            if (bll_Buyoff.Exist(txt_Job_ID.Text))
            {
                InitTbJob();
                Common.CommFunctions.ShowMessage(this.Page, "Warnning!! The Job is already added, Can not add again!.");
                return;
            }

            #region validation
            string laserMachineID = ddlMachineID.SelectedValue;
            string jobNumber = txt_Job_ID.Text;
            string buyoffBy = ddl_Buyoff_By.SelectedValue;
            string laserOp = ddl_MC_Operator.SelectedValue;
            string partNumber = txt_Part_No.Text;
    
       
            if (laserMachineID == "")
            {
                Common.CommFunctions.ShowMessage(Page, "Please choose Machine ID !");
                return;
            }

            if (jobNumber == "")
            {
                Common.CommFunctions.ShowMessage(Page, "Please keyin Job ID!");
                return;
            }

            if (buyoffBy == "")
            {
                Common.CommFunctions.ShowMessage(Page, "Please choose Buyoff By!");
                return;
            }

            if (laserOp == "")
            {
                Common.CommFunctions.ShowMessage(Page, "Please choose MC Operator!");
                return;
            }

            if (ddlRate.SelectedItem.Value == "")
            {
                Common.CommFunctions.ShowMessage(Page, "Rate can not be empty,Please choose!");
                return;
            }

            if (ddlFrequency.SelectedItem.Value == "")
            {
                Common.CommFunctions.ShowMessage(Page, "Frequency can not be empty,Please choose!");
                return;
            }

            if (txtPower.Text.Trim() == "")
            {
                Common.CommFunctions.ShowMessage(Page, "Power can not be empty,Please input!");
                return;
            }

            if (ddlRepeat.SelectedItem.Value == "")
            {
                Common.CommFunctions.ShowMessage(Page, "Repeat can not be empty,Please choose!");
                return;
            }

            #endregion




            #region set  Model
            Common.Class.Model.LMMSBuyOffList_Mode Buyoff_model = new Common.Class.Model.LMMSBuyOffList_Mode();
            Buyoff_model.BUYOFF_ID = "B-" + System.DateTime.Now.ToString("yyMMddHHmmssfff");
            Buyoff_model.JOB_ID = jobNumber;
            Buyoff_model.PART_NO = partNumber;
            Buyoff_model.MACHINE_ID = laserMachineID;
            Buyoff_model.MC_OPERATOR = laserOp;
            Buyoff_model.BUYOFF_BY = buyoffBy;
            Buyoff_model.APPROVED_BY = txt_Approved_By.Text;
            Buyoff_model.CHECK_BY = "";
            Buyoff_model.DATE_TIME = DateTime.Now;


            #region assigement checkbox value
            string Result = "";
            if (cb_Black_Dot_OK.Checked)
                Result = "OK";
            else if (cb_Black_Dot_NG.Checked)
                Result = "NG";
            else
                Result = "";

            Buyoff_model.BLACK_DOT_1ST = Result;


            if (cb_Black_Dot_OK1.Checked)
                Result = "OK";
            else if (cb_Black_Dot_NG1.Checked)
                Result = "NG";
            else
                Result = "";

            Buyoff_model.BLACK_DOT_2ND = Result;


            if (cb_Black_Dot_OK2.Checked)
                Result = "OK";
            else if (cb_Black_Dot_NG2.Checked)
                Result = "NG";
            else
                Result = "";

            Buyoff_model.BLACK_DOT_IN = Result;


            if (cb_Black_Mark_OK.Checked)
                Result = "OK";
            else if (cb_Black_Dot_NG.Checked)
                Result = "NG";
            else
                Result = "";

            Buyoff_model.BLACK_MARK_1ST = Result;


            if (cb_Black_Mark_OK1.Checked)
                Result = "OK";
            else if (cb_Black_Dot_NG1.Checked)
                Result = "NG";
            else
                Result = "";
            Buyoff_model.BLACK_MARK_2ND = Result;


            if (cb_Black_Mark_OK2.Checked)
                Result = "OK";
            else if (cb_Black_Dot_NG2.Checked)
                Result = "NG";
            else
                Result = "";
            Buyoff_model.BLACK_MARK_IN = Result;

            if (cb_Check_Guled_OK.Checked)
                Result = "OK";
            else if (cb_Check_Guled_NG.Checked)
                Result = "NG";
            else
                Result = "";
            Buyoff_model.CHECK_GULED_1ST = Result;


            if (cb_Check_Guled_OK1.Checked)
                Result = "OK";
            else if (cb_Check_Guled_NG1.Checked)
                Result = "NG";
            else
                Result = "";
            Buyoff_model.CHECK_GULED_2ND = Result;

            if (cb_Check_Guled_OK2.Checked)
                Result = "OK";
            else if (cb_Check_Guled_NG2.Checked)
                Result = "NG";
            else
                Result = "";
            Buyoff_model.CHECK_GULED_IN = Result;


            if (cb_Jagged_OK.Checked)
                Result = "OK";
            else if (cb_Jagged_OK.Checked)
                Result = "NG";
            else
                Result = "";
            Buyoff_model.JAGGED_1ST = Result;

            if (cb_Jagged_OK1.Checked)
                Result = "OK";
            else if (cb_Jagged_OK1.Checked)
                Result = "NG";
            else
                Result = "";
            Buyoff_model.JAGGED_2ND = Result;

            if (cb_Jagged_OK2.Checked)
                Result = "OK";
            else if (cb_Jagged_OK2.Checked)
                Result = "NG";
            else
                Result = "";
            Buyoff_model.JAGGED_IN = Result;

            if (cb_Navitas_OK.Checked)
                Result = "OK";
            else if (cb_Navitas_NG.Checked)
                Result = "NG";
            else
                Result = "";
            Buyoff_model.NAVITAS_1ST = Result;

            if (cb_Navitas_OK1.Checked)
                Result = "OK";
            else if (cb_Navitas_NG1.Checked)
                Result = "NG";
            else
                Result = "";
            Buyoff_model.NAVITAS_2ND = Result;

            if (cb_Navitas_OK2.Checked)
                Result = "OK";
            else if (cb_Navitas_NG2.Checked)
                Result = "NG";
            else
                Result = "";
            Buyoff_model.NAVITAS_IN = Result;

            if (cb_Pin_Hole_OK.Checked)
                Result = "OK";
            else if (cb_Pin_Hole_NG.Checked)
                Result = "NG";
            else
                Result = "";
            Buyoff_model.PIN_HOLE_1ST = Result;

            if (cb_Pin_Hole_OK1.Checked)
                Result = "OK";
            else if (cb_Pin_Hole_NG1.Checked)
                Result = "NG";
            else
                Result = "";
            Buyoff_model.PIN_HOLE_2ND = Result;

            if (cb_Pin_Hole_OK2.Checked)
                Result = "OK";
            else if (cb_Pin_Hole_NG2.Checked)
                Result = "NG";
            else
                Result = "";
            Buyoff_model.PIN_HOLE_IN = Result;

            if (cb_Smart_Scope_OK.Checked)
                Result = "OK";
            else if (cb_Smart_Scope_OK.Checked)
                Result = "NG";
            else
                Result = "";
            Buyoff_model.SMART_SCOPE_1ST = Result;

            if (cb_Smart_Scope_OK1.Checked)
                Result = "OK";
            else if (cb_Smart_Scope_OK1.Checked)
                Result = "NG";
            else
                Result = "";
            Buyoff_model.SMART_SCOPE_2ND = Result;

            if (cb_Smart_Scope_OK2.Checked)
                Result = "OK";
            else if (cb_Smart_Scope_OK2.Checked)
                Result = "NG";
            else
                Result = "";
            #endregion

            Buyoff_model.SMART_SCOPE_IN = Result;
            Session["Buyoff_model"] = Buyoff_model;

            #endregion


            #region set machine setting model
            Common.Class.Model.LMMSVisionMachineSettingHis_Model visionSettingModel = new Common.Class.Model.LMMSVisionMachineSettingHis_Model();

            visionSettingModel.day = DateTime.Now.AddHours(-8).Date;
            //8:00 <= datetimeNow < 20:00  = day
            visionSettingModel.shift = DateTime.Now < visionSettingModel.day.Value.AddHours(20) && DateTime.Now >= visionSettingModel.day.Value.AddHours(8) ? StaticRes.Global.Shift.Day : StaticRes.Global.Shift.Night;
            visionSettingModel.machineID = ddlMachineID.SelectedItem.Value;
            visionSettingModel.jobNumber = txt_Job_ID.Text;
            visionSettingModel.partNumber = txt_Part_No.Text;
            //visionSettingModel.lighting = txtlighting.Text.Trim();
            //visionSettingModel.camera = txtCamera.Text.Trim();
            visionSettingModel.power = txtPower.Text.Trim();
            visionSettingModel.rate = ddlRate.SelectedItem.Value;
            visionSettingModel.frequency = ddlFrequency.SelectedItem.Value;
            //visionSettingModel.fill = ddlFill.SelectedItem.Value;
            visionSettingModel.repeat = ddlRepeat.SelectedItem.Value;
            visionSettingModel.dateTime = DateTime.Now;
            visionSettingModel.updatedTime = DateTime.Now;

            Session["LMMSVisionMachineSettingHis_Model"] = visionSettingModel;
            #endregion


            

            Response.Redirect("./Login.aspx?commandType=Addbuyoff&Department=Laser", false);
        }

        protected void btn_Cancel_Click(object sender, EventArgs e)
        {
            string strJS = "if (confirm('Your action will not be saved, are you sure?') == true) { ";
            strJS += "window.location.href = \"./Buy_Off_List.aspx\";  } ";
            ClientScript.RegisterStartupScript(Page.GetType(), "", strJS, true);
        }




        //==============================Func=================================//
        private void ShowBuyoffReport()
        {
            string Jobnumber = Request.QueryString["JobNumber"] == null ? "" : Request.QueryString["JobNumber"].ToString();
            if (!string.IsNullOrEmpty(Jobnumber))
            {

                #region Laser buyoff

                Common.Class.BLL.LMMSBUYOFFLIST_BLL bll = new Common.Class.BLL.LMMSBUYOFFLIST_BLL();
                DataTable dt = bll.GetBuyofflist(Jobnumber, "", "", "", "", "", null, null);
                DataRow dr = dt.Rows[0];

                //通过selected属性选定, 避免触发ddl selectedchange事件
                this.ddlMachineID.Items.FindByValue(dr["MACHINE_ID"].ToString()).Selected = true;
                this.txt_Job_ID.Text = dr["JOB_ID"].ToString();
                this.txt_Part_No.Text = dr["PART_NO"].ToString();
                this.txt_current.Text = dr["CurrentPower"].ToString() == "" ? "" : dr["CurrentPower"].ToString() + "%";
                this.ddl_MC_Operator.SelectedValue = dr["MC_OPERATOR"].ToString();
                this.ddl_Buyoff_By.SelectedValue = dr["BUYOFF_BY"].ToString();
                this.txt_Approved_By.Text = dr["APPROVED_BY"].ToString();
                this.txt_CheckBy.Text = dr["CHECK_BY"].ToString() ;

                #region Check box
                string cbResult = "";
                
                cbResult = dr["BLACK_MARK_1ST"].ToString();
                if (cbResult == "OK")
                {
                    this.cb_Black_Mark_OK.Checked = true;
                    this.cb_Black_Mark_OK.BackColor = StaticRes.InspectionResColor.OK;
                }
                else if(cbResult == "NG")
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
                else if(cbResult == "NG")
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

                #region disenable controller
                this.cb_Black_Mark_OK.Enabled = false;
                this.cb_Black_Mark_NG.Enabled = false;
                this.cb_Black_Dot_OK.Enabled = false;
                this.cb_Black_Dot_NG.Enabled = false;
                this.cb_Pin_Hole_OK.Enabled = false;
                this.cb_Pin_Hole_NG.Enabled = false;
                this.cb_Jagged_OK.Enabled = false;
                this.cb_Jagged_NG.Enabled = false;
                this.cb_Check_Guled_OK.Enabled = false;
                this.cb_Check_Guled_NG.Enabled = false;
                this.cb_Navitas_OK.Enabled = false;
                this.cb_Navitas_NG.Enabled = false;
                this.cb_Smart_Scope_OK.Enabled = false;
                this.cb_Smart_Scope_NG.Enabled = false;

                this.cb_Black_Mark_OK1.Enabled = false;
                this.cb_Black_Mark_NG1.Enabled = false;
                this.cb_Black_Dot_OK1.Enabled = false;
                this.cb_Black_Dot_NG1.Enabled = false;
                this.cb_Pin_Hole_OK1.Enabled = false;
                this.cb_Pin_Hole_NG1.Enabled = false;
                this.cb_Jagged_OK1.Enabled = false;
                this.cb_Jagged_NG1.Enabled = false;
                this.cb_Check_Guled_OK1.Enabled = false;
                this.cb_Check_Guled_NG1.Enabled = false;
                this.cb_Navitas_OK1.Enabled = false;
                this.cb_Navitas_NG1.Enabled = false;
                this.cb_Smart_Scope_OK1.Enabled = false;
                this.cb_Smart_Scope_NG1.Enabled = false;

                this.cb_Black_Mark_OK2.Enabled = false;
                this.cb_Black_Mark_NG2.Enabled = false;
                this.cb_Black_Dot_OK2.Enabled = false;
                this.cb_Black_Dot_NG2.Enabled = false;
                this.cb_Pin_Hole_OK2.Enabled = false;
                this.cb_Pin_Hole_NG2.Enabled = false;
                this.cb_Jagged_OK2.Enabled = false;
                this.cb_Jagged_NG2.Enabled = false;
                this.cb_Check_Guled_OK2.Enabled = false;
                this.cb_Check_Guled_NG2.Enabled = false;
                this.cb_Navitas_OK2.Enabled = false;
                this.cb_Navitas_NG2.Enabled = false;
                this.cb_Smart_Scope_OK2.Enabled = false;
                this.cb_Smart_Scope_NG2.Enabled = false;

                this.btn_Confirm.Visible = false;
                this.btn_Cancel.Visible = false;

                this.ddlMachineID.Enabled = false;
                this.ddl_Buyoff_By.Enabled = false;
                this.ddl_MC_Operator.Enabled = false;

                this.txt_Job_ID.Enabled = false;
                this.txt_Part_No.Enabled = false;
                this.txt_current.Enabled = false;
                this.txt_Approved_By.Enabled = false;
                this.lb_CheckBy.Visible = true;
                this.txt_CheckBy.Visible = true;
                this.txt_CheckBy.Enabled = false;

                this.cb_Appera_AllOK.Visible = false;
                this.cb_Graph_AllOK.Visible = false;
                #endregion


                #endregion


                #region vision setting

                Common.Class.BLL.LMMSVisionMachineSettingHis_BLL visionBLL = new Common.Class.BLL.LMMSVisionMachineSettingHis_BLL();
                Common.Class.Model.LMMSVisionMachineSettingHis_Model visionModel = visionBLL.GetModel(Jobnumber);

                if (visionModel != null)
                {
                    //this.txtlighting.Text = visionModel.lighting;
                    //this.txtCamera.Text = visionModel.camera;

                    this.txtPower.Text = visionModel.power;
                    this.ddlRate.SelectedValue = visionModel.rate;
                    this.ddlFrequency.SelectedValue = visionModel.frequency;
                    //this.ddlFill.SelectedValue = visionModel.fill;
                    this.ddlRepeat.SelectedValue = visionModel.repeat;




                    //this.txtlighting.Enabled = false;
                    //this.txtCamera.Enabled = false;
                    this.txtPower.Enabled = false;
                    this.ddlRate.Enabled = false;
                    this.ddlFrequency.Enabled = false;
                    //this.ddlFill.Enabled = false;
                    this.ddlRepeat.Enabled = false;
                }

                #endregion


            }
        }

        private void SetUserDDL()
        {
            //清空
            this.ddl_MC_Operator.Items.Clear();
            this.ddl_Buyoff_By.Items.Clear();

            //添加一个空选项
            this.ddl_MC_Operator.Items.Add(new ListItem("",""));
            this.ddl_Buyoff_By.Items.Add(new ListItem("", ""));


            Common.Class.BLL.User_DB_BLL _us = new Common.Class.BLL.User_DB_BLL();
            List<string> usernameList = _us.GetUsernameList(StaticRes.Global.Department.Laser);

            foreach (string userName in usernameList)
            {
                this.ddl_MC_Operator.Items.Add(new ListItem(userName, userName));
                this.ddl_Buyoff_By.Items.Add(new ListItem(userName, userName));
            }
        }

        private void SetMachineDDL()
        {
            this.ddlMachineID.Items.Clear();

            ListItem Li = new ListItem();
            Li.Text = "";
            Li.Value = "";

            this.ddlMachineID.Items.Add(Li);


            for (int i = 1; i < 9; i++)
            {
                Li = new ListItem();
                Li.Text = "No." + i.ToString();
                Li.Value = i.ToString();

                this.ddlMachineID.Items.Add(Li);
            }

          
        }
        
        private void SetOKColor(CheckBox cb)
        {
            if (cb.Checked)
            {
                cb.BackColor = StaticRes.InspectionResColor.OK;
            }else
            {
                cb.BackColor = StaticRes.InspectionResColor.Empty;
            }
        }
        
        private void SetNGColor(CheckBox cb)
        {
            if (cb.Checked)
            {
                cb.BackColor = StaticRes.InspectionResColor.NG;
            }
            else
            {
                cb.BackColor = StaticRes.InspectionResColor.Empty;
            }
        }
        
        private void InitTbJob()
        {
            this.txt_Job_ID.Text = "JOT" + DateTime.Now.Year.ToString().Substring(2, 2);
            this.txt_Job_ID.Focus();
            this.txt_Job_ID.Enabled = true;
            this.txt_Part_No.Enabled = false;
            this.txt_current.Enabled = false;

            this.txt_Part_No.Text = "";
            this.txt_current.Text = "";
        }


        private void SetRateDDL()
        {
            this.ddlRate.Items.Clear();

            this.ddlRate.Items.Add(new ListItem("", ""));

            for (int i = 500; i <= 3000; i=i+50)
            {
                this.ddlRate.Items.Add(new ListItem(i.ToString(), i.ToString()));
            }
        }
       
        private void SetFrequencyDDL()
        {
            this.ddlFrequency.Items.Clear();

            this.ddlFrequency.Items.Add(new ListItem("", ""));

            for (int i = 0 ; i <= 200; i=i+5)
            {
                this.ddlFrequency.Items.Add(new ListItem(i.ToString(), i.ToString()));
            }
        }

        
    }
}