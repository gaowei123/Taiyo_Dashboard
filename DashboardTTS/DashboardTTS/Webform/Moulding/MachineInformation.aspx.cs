using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace DashboardTTS.Webform.Molding
{
    public partial class MachineInformation : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    this.lb_Header.Text = "Moulding Machine Information";


                    Common.Class.BLL.MouldingMachineInformation_BLL Bll = new Common.Class.BLL.MouldingMachineInformation_BLL();

                    DataTable dt = Bll.SelectList();

                    DataRow[] dr_Main = dt.Select("PartModel = '" + StaticRes.Global.MouldingModelType.Main + "'");
                    DataRow[] dr_Machine = dt.Select("PartModel = '" + StaticRes.Global.MouldingModelType.Machine + "'");
                    DataRow[] dr_RobotArm = dt.Select("PartModel = '" + StaticRes.Global.MouldingModelType.RobotArm + "'");
                    DataRow[] dr_Temp = dt.Select("PartModel = '" + StaticRes.Global.MouldingModelType.Temperature + "'");
                    DataRow[] dr_Dryer = dt.Select("PartModel = '" + StaticRes.Global.MouldingModelType.Dryer + "'");

                    this.dg_Main.DataSource = DataRowToDataTable(dr_Main).DefaultView;
                    this.dg_Main.DataBind();
                    this.dg_Machine.DataSource = DataRowToDataTable(dr_Machine).DefaultView;
                    this.dg_Machine.DataBind();
                    this.dg_RobotArm.DataSource = DataRowToDataTable(dr_RobotArm).DefaultView;
                    this.dg_RobotArm.DataBind();
                    this.dg_Temp.DataSource = DataRowToDataTable(dr_Temp).DefaultView;
                    this.dg_Temp.DataBind();
                    this.dg_Dryer.DataSource = DataRowToDataTable(dr_Dryer).DefaultView;
                    this.dg_Dryer.DataBind();


                    string Result = Request.QueryString["Result"] == null ? "" : Request.QueryString["Result"].ToString();
                    if (Result != "" && Result == "FALSE")
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "", "alert('Update Fail!');", true);
                    }

                }

                this.dg_Main.ItemCommand += Dg_Main_ItemCommand;
                this.dg_Machine.ItemCommand += Dg_Machine_ItemCommand;
                this.dg_RobotArm.ItemCommand += Dg_RobotArm_ItemCommand;
                this.dg_Dryer.ItemCommand += Dg_Dryer_ItemCommand;
                this.dg_Temp.ItemCommand += Dg_Temp_ItemCommand;
            }
            catch (Exception ee)
            {
                DBHelp.Reports.LogFile.Log("MachineInformation", "PageLoad Exception: " + ee.ToString());
            }
        }

        private void Dg_Main_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            DataGridItem item = e.Item;

            string MachineID = item.Cells[1].Text;
            string Maker = item.Cells[2].Text;
            string Info = item.Cells[3].Text;
            string Model = item.Cells[4].Text;
            string DateOfManu = item.Cells[13].Text;
            string ScrewDiameter = item.Cells[6].Text;
            string MaxOPNStroke = item.Cells[7].Text;
            string EJTStroke = item.Cells[8].Text;
            string TiebarDistance = item.Cells[9].Text;
            string MinMoldSize = item.Cells[10].Text;
            string MinMoldThickness = item.Cells[11].Text;
            string Dimensions = item.Cells[12].Text;

            string URL = "./MachineInformationDetail.aspx?";
            URL += "ListType=" + StaticRes.Global.MouldingModelType.Main;

            URL += "&MachineID=" + MachineID;
            URL += "&Maker=" + Maker;
            URL += "&Info=" + Info;
            URL += "&Model=" + Model;
            URL += "&DateOfManu=" + DateOfManu;
            URL += "&ScrewDiameter=" + ScrewDiameter;
            URL += "&MaxOPNStroke=" + MaxOPNStroke;
            URL += "&EJTStroke=" + EJTStroke;
            URL += "&TiebarDistance=" + TiebarDistance;
            URL += "&MinMoldSize=" + MinMoldSize;
            URL += "&MinMoldThickness=" + MinMoldThickness;
            URL += "&Dimensions=" + Dimensions;

            Response.Redirect(URL, false);
        }

        private void Dg_Temp_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            DataGridItem item = e.Item;

            string MachineID = item.Cells[1].Text;
            string Maker = item.Cells[2].Text;
            string Model = item.Cells[3].Text;
            string Date = item.Cells[4].Text;

            string URL = "./MachineInformationDetail.aspx?";
            URL += "ListType=" + "Temperature";

            URL += "&MachineID=" + MachineID;
            URL += "&Maker=" + Maker;
            URL += "&Model=" + Model;
            URL += "&Date=" + Date;
            Response.Redirect(URL, false);
        }

        private void Dg_Dryer_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            DataGridItem item = e.Item;

            string MachineID = item.Cells[1].Text;
            string Maker = item.Cells[2].Text;
            string Model = item.Cells[3].Text;
            string Date = item.Cells[4].Text;

            string URL = "./MachineInformationDetail.aspx?";
            URL += "ListType=" + "Dryer";

            URL += "&MachineID=" + MachineID;
            URL += "&Maker=" + Maker;
            URL += "&Model=" + Model;
            URL += "&Date=" + Date;
            Response.Redirect(URL, false);
        }

        private void Dg_RobotArm_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            DataGridItem item = e.Item;

            string MachineID = item.Cells[1].Text;
            string Model = item.Cells[2].Text;
            string SerialNo = item.Cells[3].Text;
            string ControllerType = item.Cells[4].Text;
            string ControllerSerialNo = item.Cells[5].Text;
            string Date = item.Cells[6].Text;

            string URL = "./MachineInformationDetail.aspx?";
            URL += "ListType=" + "RobotArm";

            URL += "&MachineID=" + MachineID;
            URL += "&Model=" + Model;
            URL += "&SerialNo=" + SerialNo;
            URL += "&ControllerType=" + ControllerType;
            URL += "&ControllerSerialNo=" + ControllerSerialNo;
            URL += "&Date=" + Date;
            Response.Redirect(URL, false);
        }

        private void Dg_Machine_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            DataGridItem item = e.Item;

            string MachineID = item.Cells[1].Text;
            string Type = item.Cells[2].Text;
            string MakerModel = item.Cells[3].Text;
            string SerialNo = item.Cells[4].Text;
            string CTRL = item.Cells[6].Text;
            string DateOfManu = item.Cells[7].Text;

            string URL = "./MachineInformationDetail.aspx?";
            URL += "ListType=" + StaticRes.Global.MouldingModelType.Machine;

            URL += "&MachineID=" + MachineID;
            URL += "&Type=" + Type;
            URL += "&MakerModel=" + MakerModel;
            URL += "&SerialNo=" + SerialNo;
            URL += "&DateOfManu=" + DateOfManu;
            URL += "&CTRL=" + CTRL;
            Response.Redirect(URL, false);
        }



        private DataTable DataRowToDataTable(DataRow[] Rows)
        {
            if (Rows == null || Rows.Length == 0)
                return null;
            DataTable tmp = Rows[0].Table.Clone();
            foreach (DataRow row in Rows)
            {
                tmp.ImportRow(row);
            }
            return tmp;
        }


    }
}