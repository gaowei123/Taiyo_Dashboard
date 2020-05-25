using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DashboardTTS.Webform.Molding
{
    public partial class MachineInformationDetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    string ListType = Request.QueryString["ListType"] == null ? "" : Request.QueryString["ListType"].ToString();
                    string MachineID = Request.QueryString["MachineID"] == null ? "" : Request.QueryString["MachineID"].ToString();
                    string MakerModel = Request.QueryString["MakerModel"] == null ? "" : Request.QueryString["MakerModel"].ToString();
                    string Maker = Request.QueryString["Maker"] == null ? "" : Request.QueryString["Maker"].ToString();
                    string Model = Request.QueryString["Model"] == null ? "" : Request.QueryString["Model"].ToString();
                    string Date = Request.QueryString["Date"] == null ? "" : Request.QueryString["Date"].ToString();
                    string SerialNo = Request.QueryString["SerialNo"] == null ? "" : Request.QueryString["SerialNo"].ToString();
                    string ControllerType = Request.QueryString["ControllerType"] == null ? "" : Request.QueryString["ControllerType"].ToString();
                    string ControllerSerialNo = Request.QueryString["ControllerSerialNo"] == null ? "" : Request.QueryString["ControllerSerialNo"].ToString();
                    string Type = Request.QueryString["Type"] == null ? "" : Request.QueryString["Type"].ToString();
                    string DateOfManu = Request.QueryString["DateOfManu"] == null ? "" : Request.QueryString["DateOfManu"].ToString();
                    string CTRL = Request.QueryString["CTRL"] == null ? "" : Request.QueryString["CTRL"].ToString();

                    string Info = Request.QueryString["Info"] == null ? "" : Request.QueryString["Info"].ToString();
                    string ScrewDiameter = Request.QueryString["ScrewDiameter"] == null ? "" : Request.QueryString["ScrewDiameter"].ToString();
                    string MaxOPNStroke = Request.QueryString["MaxOPNStroke"] == null ? "" : Request.QueryString["MaxOPNStroke"].ToString();
                    string EJTStroke = Request.QueryString["EJTStroke"] == null ? "" : Request.QueryString["EJTStroke"].ToString();
                    string TiebarDistance = Request.QueryString["TiebarDistance"] == null ? "" : Request.QueryString["TiebarDistance"].ToString();
                    string MinMoldSize = Request.QueryString["MinMoldSize"] == null ? "" : Request.QueryString["MinMoldSize"].ToString();
                    string MinMoldThickness = Request.QueryString["MinMoldThickness"] == null ? "" : Request.QueryString["MinMoldThickness"].ToString();
                    string Dimensions = Request.QueryString["Dimensions"] == null ? "" : Request.QueryString["Dimensions"].ToString();


                    this.ddl_machineID.Items.FindByValue(MachineID.Replace("Machine", "")).Selected = true;
                    this.ddl_machineID.Enabled = false;

                    //Temperature    Dryer    RobotArm       Machine
                    if (ListType == StaticRes.Global.MouldingModelType.Machine)
                    {
                        this.lb_Machine_Type.Visible = true;
                        this.lb_Machine_SerialNo.Visible = true; 
                        this.lb_Machine_MakerModel.Visible = true;
                        this.lb_Machine_ManufactureDate.Visible = true;
                        this.lb_Machine_CTRL.Visible = true;

                        this.infDch_Machine.Visible = true;
                        this.txt_Machine_CTRL.Visible = true;
                        this.txt_Machine_MakerModel.Visible = true;
                        this.txt_Machine_SerialNo.Visible = true;
                        this.txt_Machine_Type.Visible = true;

                        this.infDch_Machine.CalendarLayout.SelectedDate = DateTime.Parse(DateOfManu);
                        this.infDch_Machine.Value = DateTime.Parse(DateOfManu);
                        this.txt_Machine_CTRL.Text = CTRL;
                        this.txt_Machine_MakerModel.Text = MakerModel;
                        this.txt_Machine_SerialNo.Text = SerialNo;
                        this.txt_Machine_Type.Text = Type;
                    }
                    else if (ListType == StaticRes.Global.MouldingModelType.RobotArm)
                    {
                        this.lb_RobotArm_ControllerSerialNo.Visible = true;
                        this.lb_RobotArm_ControllerType.Visible = true;
                        this.lb_RobotArm_Model.Visible = true;
                        this.lb_RobotArm_SerialNo.Visible = true;
                        this.lb_RobotArm_Date.Visible = true;

                        this.txt_RobotArm_ControllerSerialNo.Visible = true;
                        this.txt_RobotArm_ControllerType.Visible = true;
                        this.txt_RobotArm_Model.Visible = true;
                        this.txt_RobotArm_SerialNo.Visible = true;
                        this.txt_RobotArm_Date.Visible = true;//   infDch_RobotArm.Visible = true;

                        this.txt_RobotArm_ControllerSerialNo.Text = ControllerSerialNo;
                        this.txt_RobotArm_ControllerType.Text = ControllerType;
                        this.txt_RobotArm_Model.Text = Model;
                        this.txt_RobotArm_SerialNo.Text = SerialNo;
                        this.txt_RobotArm_Date.Text = Date;// infDch_RobotArm.CalendarLayout.SelectedDate = DateTime.Parse(Date);
                        //this.infDch_RobotArm.Value = DateTime.Parse(Date);
                    }
                    else if (ListType == StaticRes.Global.MouldingModelType.Temperature  || ListType == StaticRes.Global.MouldingModelType.Dryer)
                    {
                        this.lb_Temp_Dryer_Date.Visible = true;
                        this.lb_Temp_Dryer_Maker.Visible = true;
                        this.lb_Temp_Dryer_Model.Visible = true;

                        this.txt_Temp_Dryer_Model.Visible = true;
                        //this.infDch_Temp_Dryer.Visible = true;
                        this.txt_Temp_Dryer_Date.Visible = true;
                        this.txt_Temp_Dryer_Maker.Visible = true;

                        this.txt_Temp_Dryer_Model.Text = Model;
                        //this.infDch_Temp_Dryer.CalendarLayout.SelectedDate = DateTime.Parse(Date);
                        //this.infDch_Temp_Dryer.Value = DateTime.Parse(Date);
                        this.txt_Temp_Dryer_Date.Text = Date;
                        this.txt_Temp_Dryer_Maker.Text = Maker;
                    }
                    else if (ListType == StaticRes.Global.MouldingModelType.Main)
                    {
                        this.lb_Main_Maker.Visible = true;
                        this.lb_Main_Info.Visible = true;
                        this.lb_Main_Model.Visible = true;
                        this.lb_Main_DateOfManu.Visible = true;
                        this.lb_Main_ScrewDiameter.Visible = true;
                        this.lb_Main_MaxOPNStroke.Visible = true;
                        this.lb_Main_EJTStroke.Visible = true;
                        this.lb_Main_TiebarDistance.Visible = true;
                        this.lb_Main_MinMoldSize.Visible = true;
                        this.lb_Main_MinMoldThickness.Visible = true;
                        this.lb_Main_Dimensions.Visible = true;

                        this.txt_Main_Maker.Visible = true;
                        this.txt_Main_Info.Visible = true;
                        this.txt_Main_Model.Visible = true;
                        this.infDch_Main.Visible = true;
                        this.txt_Main_ScrewDiameter.Visible = true;
                        this.txt_Main_MaxOPNStroke.Visible = true;

                        this.txt_Main_EJTStroke.Visible = true;
                        this.txt_Main_TiebarDistance.Visible = true;
                        this.txt_Main_MinMoldSize.Visible = true;
                        this.txt_Main_MinMoldThickness.Visible = true;
                        this.txt_Main_Dimensions.Visible = true;

                        this.txt_Main_Maker.Text = Maker;
                        this.txt_Main_Info.Text = Info;
                        this.txt_Main_Model.Text = Model;
                        this.infDch_Main.CalendarLayout.SelectedDate = DateTime.Parse(DateOfManu);
                        this.infDch_Main.Value = DateTime.Parse(DateOfManu);
                        this.txt_Main_ScrewDiameter.Text = ScrewDiameter;
                        this.txt_Main_MaxOPNStroke.Text = MaxOPNStroke;
                        this.txt_Main_EJTStroke.Text  = EJTStroke;
                        this.txt_Main_TiebarDistance.Text  = TiebarDistance;
                        this.txt_Main_MinMoldSize.Text  = MinMoldSize ;
                        this.txt_Main_MinMoldThickness.Text  = MinMoldThickness;
                        this.txt_Main_Dimensions.Text  = Dimensions;
                    }
                }
            }
            catch (Exception ee)
            {
                DBHelp.Reports.LogFile.Log("MachineInformationDetail", "PageLoad Exception: " + ee.ToString());
            }
            
        }

        protected void btn_submit_Click(object sender, EventArgs e)
        {
            Common.Class.Model.MouldingMachineInformation_Model model = new Common.Class.Model.MouldingMachineInformation_Model();
            model.MachineID = this.ddl_machineID.SelectedValue;
            model.UpdatedDate = DateTime.Now;

            string ListType = Request.QueryString["ListType"] == null ? "" : Request.QueryString["ListType"].ToString();
            if (ListType == StaticRes.Global.MouldingModelType.Machine)
            {
                model.PartModel = StaticRes.Global.MouldingModelType.Machine;
                model.CTRL = this.txt_Machine_CTRL.Text;
                model.MakerModel = this.txt_Machine_MakerModel.Text;
                model.SerialNo = this.txt_Machine_SerialNo.Text;
                model.Type = this.txt_Machine_Type.Text;
                model.DateOfManu = this.infDch_Machine.CalendarLayout.SelectedDate;
            }
            else if (ListType == StaticRes.Global.MouldingModelType.RobotArm)
            {
                model.PartModel = StaticRes.Global.MouldingModelType.RobotArm;
                model.ControllerSerialNo = this.txt_RobotArm_ControllerSerialNo.Text;
                model.ControllerType = this.txt_RobotArm_ControllerType.Text;
                model.Model = this.txt_RobotArm_Model.Text;
                model.SerialNo = this.txt_RobotArm_SerialNo.Text;
                model.Date = this.txt_RobotArm_Date.Text;// this.infDch_RobotArm.CalendarLayout.SelectedDate;
            }
            else if (ListType == StaticRes.Global.MouldingModelType.Temperature || ListType == StaticRes.Global.MouldingModelType.Dryer)
            {
                model.PartModel = ListType == StaticRes.Global.MouldingModelType.Temperature ? StaticRes.Global.MouldingModelType.Temperature : StaticRes.Global.MouldingModelType.Dryer;
                model.Date = this.txt_Temp_Dryer_Date.Text;// this.infDch_Temp_Dryer.CalendarLayout.SelectedDate;
                model.Maker = this.txt_Temp_Dryer_Maker.Text;
                model.Model = this.txt_Temp_Dryer_Model.Text;
            }
            else if (ListType == StaticRes.Global.MouldingModelType.Main)
            {
                model.PartModel = ListType;
                model.Maker = this.txt_Main_Maker.Text;
                model.Info = this.txt_Main_Info.Text;
                model.Model = this.txt_Main_Model.Text;
                model.DateOfManu = this.infDch_Main.CalendarLayout.SelectedDate;
                model.ScrewDiameter = decimal.Parse(this.txt_Main_ScrewDiameter.Text.Trim('m'));
                model.MaxOPNStroke = decimal.Parse(this.txt_Main_MaxOPNStroke.Text.Trim('m'));
                model.EJTStroke = decimal.Parse(this.txt_Main_EJTStroke.Text.Trim('m'));
                model.TiebarDistance = this.txt_Main_TiebarDistance.Text;
                model.MinMoldSize = this.txt_Main_MinMoldSize.Text;
                model.MinMoldThickness = decimal.Parse(this.txt_Main_MinMoldThickness.Text.Trim('m'));
                model.Dimensions = this.txt_Main_Dimensions.Text;
            }
            model.PartModel = ListType;

            Session["MouldingMachineInformation"] = model;


            Response.Redirect("../Laser/Login.aspx?commandType=UpdateMachineInfo&Department="+StaticRes.Global.Department.Moulding,false);

        }

        protected void btn_cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("./MachineInformation.aspx",false);
        }
    }
}