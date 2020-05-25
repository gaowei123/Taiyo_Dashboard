using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace DashboardTTS.Webform.Moulding
{
    public partial class MouldingMachineStatus : System.Web.UI.Page
    {

        public class MouldMachineStatus : Common.Class.Model.MouldingMachineStatus_Model
        {
            public string Duration { get; set; }
            public string sDay { get; set; }
            public string TotalQty { get; set; }
            public string PassQty { get; set; }
            public string RejQty { get; set; }
            public string OPID { get; set; }

        }


        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!this.IsPostBack)
                {
                    
                    string sDateFrom = Request.QueryString["DateFrom"] == null ? "" : Request.QueryString["DateFrom"].ToString();
                    string sDateTo = Request.QueryString["DateTo"] == null ? "" : Request.QueryString["DateTo"].ToString();
                    string sShift = Request.QueryString["Shift"] == null ? "" : Request.QueryString["Shift"].ToString();
                    string sMachineID = Request.QueryString["MachineID"] == null ? "" : Request.QueryString["MachineID"].ToString();
                    string sStatus = Request.QueryString["Status"] == null ? "" : Request.QueryString["Status"];


                    this.txtDateFrom.Text = sDateFrom != "" ? DateTime.Parse(sDateFrom).ToString("yyyy-MM-dd"): DateTime.Now.ToString("yyyy-MM-dd");
                    this.txtDateTo.Text = sDateTo != "" ? DateTime.Parse(sDateTo).ToString("yyyy-MM-dd") : DateTime.Now.ToString("yyyy-MM-dd");

                    if (sShift != "")
                        this.ddlShift.SelectedValue = sShift;

                    if (sMachineID  != "")
                        this.ddlMachineNo.SelectedValue = sMachineID;

                    if (sStatus != "")
                        this.ddlStatus.SelectedValue = sStatus;



                    btn_Generate_Click(new object(), new EventArgs());
                }
            }
            catch (Exception ee)
            {
                DBHelp.Reports.LogFile.Log("MouldingMachineStatus", "Page_Load Exception: " + ee.ToString());
            }
        } 

        protected void btn_Generate_Click(object sender, EventArgs e)
        {
            try
            {

                DateTime DateFrom = DateTime.Parse(this.txtDateFrom.Text.Trim());
                DateTime DateTo = DateTime.Parse(this.txtDateTo.Text.Trim());
                DateTo = DateTo.AddDays(1);
                string Shift = this.ddlShift.SelectedItem.Value;
                string MachineID = this.ddlMachineNo.SelectedItem.Value;
                string Status = this.ddlStatus.SelectedItem.Value;

                
        
                Common.Class.BLL.MouldingMachineStatus_BLL bll = new Common.Class.BLL.MouldingMachineStatus_BLL();
                List<Common.Class.Model.MouldingMachineStatus_Model> modelList = bll.GetModelList(DateFrom, DateTo, Shift, MachineID, Status);

                if (modelList == null || modelList.Count == 0)
                {
                    this.lblResult.Text = "There is no record!";
                    this.lblResult.ForeColor = System.Drawing.Color.Red;
                    this.lblResult.Visible = true;
                    this.dg_MachineSummary.Visible = false;

                    return;
                }
                else
                {
                    this.dg_MachineSummary.Visible = true;
                    this.lblResult.Visible = false;


                    Common.Class.BLL.MouldingViHistory_BLL viBLL = new Common.Class.BLL.MouldingViHistory_BLL();
                    DataTable dt = viBLL.GetTestingList(DateFrom, DateTo, Shift, MachineID);

                    List<MouldMachineStatus> statusList = new List<MouldMachineStatus>();
                    foreach (Common.Class.Model.MouldingMachineStatus_Model model in modelList)
                    {
                        MouldMachineStatus statusModel = new MouldMachineStatus();
                        statusModel.MachineID = model.MachineID;
                        statusModel.sDay = model.Day.ToString("yyyy/MM/dd");
                        statusModel.Shift = model.Shift;
                        statusModel.MachineStatus = model.MachineStatus == "No Material" ? "Mc Stop" : model.MachineStatus;
                        statusModel.StartTime = model.StartTime;
                        statusModel.EndTime = model.EndTime;
                        double totalHours = (model.EndTime - model.StartTime).TotalSeconds / 3600;
                        statusModel.Duration = Common.CommFunctions.ConvertDateTimeShort(totalHours.ToString());
                        statusModel.Remark = model.Remark;





                        DataRow[] drArr = dt.Select(" datetime > '" + model.StartTime + "' and datetime < '" + model.EndTime + "'   ");

                        if (drArr.Length == 0)
                        {
                            statusModel.PartNo = "";
                            statusModel.TotalQty = "";
                            statusModel.PassQty = "";
                            statusModel.RejQty = "";
                            statusModel.OPID = "";
                        }
                        else
                        {
                            statusModel.PartNo = drArr[0]["partNumber"].ToString();
                            statusModel.TotalQty = drArr[0]["acountReading"].ToString();
                            statusModel.PassQty = drArr[0]["acceptQty"].ToString();
                            statusModel.RejQty = drArr[0]["rejectQty"].ToString();
                            statusModel.OPID = drArr[0]["userID"].ToString();
                        }



                        statusList.Add(statusModel);
                    }


                    this.dg_MachineSummary.DataSource = statusList;
                    this.dg_MachineSummary.DataBind();
                    
                }



            }
            catch (Exception ee)
            {
                DBHelp.Reports.LogFile.Log("MouldingMachineStatus", "btn_Generate_Click Exception; " + ee.ToString());
            }

        }

        
    }
}