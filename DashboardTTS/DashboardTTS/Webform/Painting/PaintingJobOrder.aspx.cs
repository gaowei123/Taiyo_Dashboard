using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;


namespace DashboardTTS.Webform.Painting
{
    public partial class PaintingJobOrder : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    this.Response.Cache.SetNoStore();
                    this.txt_partNo.Focus();

                    btn_generate_Click(new object(), new EventArgs());
                }

                this.dg_inventoryDetail.ItemCommand += Dg_inventoryDetail_ItemCommand;
            }
            catch (Exception ee)
            {
                DBHelp.Reports.LogFile.Log("IventoryReport Exception", " Page_Load error -- " + ee.ToString());
                Common.CommFunctions.ShowWarning(lblResult, dg_inventoryDetail, StaticRes.Global.ErrorLevel.Exception, ee.ToString());
            }
        }

        private void Dg_inventoryDetail_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "LinkJobDetail")
                {
                    TableRow item1 = e.Item;
                    string partNo = "";
                    string Jobnumber = "";

                    if (item1.Cells[2].Text != "&nbsp;")
                    {
                        partNo = item1.Cells[2].Text;
                    }
                    if (item1.Cells[4].Text != "JOT###")
                    {
                        Jobnumber = item1.Cells[4].Text;
                    }

                    string str_url = "./InventoryDetail.aspx?";
                    str_url += "&Partnumber=" + partNo;
                    //str_url += "&DateFrom=" + infDchDate.CalendarLayout.SelectedDate.ToString();
                    str_url += "&Jobnumber=" + Jobnumber;


                    Response.Redirect(str_url.ToString(), false);
                }
            }
            catch (Exception ee)
            {
                DBHelp.Reports.LogFile.Log("Exception", "Dg_inventoryDetail_ItemCommand error--" + ee.Message);
                Common.CommFunctions.ShowWarning(lblResult, dg_inventoryDetail, StaticRes.Global.ErrorLevel.Exception, ee.ToString());
            }
        }



        protected void btn_generate_Click(object sender, EventArgs e)
        {
            try
            {
                Common.Class.BLL.PaintingInventory_BLL inventory_bll = new Common.Class.BLL.PaintingInventory_BLL();
                string sPartNumber = txt_partNo.Text;
                string sCustomer = txt_Customer.Text;


                DataTable dt = inventory_bll.Report(sPartNumber, sCustomer);
                if (dt == null)
                {
                    Common.CommFunctions.ShowWarning(lblResult, dg_inventoryDetail, StaticRes.Global.ErrorLevel.Warning, "");
                }
                else
                {

                    this.dg_inventoryDetail.Visible = true;
                    dg_inventoryDetail.DataSource = dt.DefaultView;
                    dg_inventoryDetail.DataBind();

                    Common.CommFunctions.HideWarning(lblResult, dg_inventoryDetail);
                }
            }
            catch (Exception ee)
            {
                DBHelp.Reports.LogFile.Log("IventoryReport Exception", " btn_generate_Click error -- " + ee.ToString());
                Common.CommFunctions.ShowWarning(lblResult, dg_inventoryDetail, StaticRes.Global.ErrorLevel.Exception, ee.ToString());
            }

        }



        protected void btn_endJob_Click(object sender, EventArgs e)
        {
            Response.Redirect("./InventoryRecord.aspx?ButtonType=EndJob");
        }

        protected void btn_scan_Click(object sender, EventArgs e)
        {
            Response.Redirect("./InventoryRecord.aspx");
        }




        #region Tooltip no use
        // Tooltip
        //private void AddToolTips(TableCell cell, string equipID)
        //{
        //    cell.Attributes.Clear();

        //    if (_ActiveData != null && cell.ForeColor != Color.LightGray)
        //    {
        //        DataRow[] selectedRow = _ActiveData.Select("Machineid ='" + equipID + "'");
        //        if (selectedRow != null && selectedRow.Length > 0)
        //        {
        //            string pmTag = string.Empty;
        //            string problemCodeTag = string.Empty;
        //            string duration = string.Empty;

        //            FloorMapLib.Sites site = SitesFactories.GetFactory();
        //            if (selectedRow[0]["OEE_STATE"].ToString() != "PD")
        //            {
        //                problemCodeTag = "  <tr>" +
        //              "     <td style=\"width: 100px;\">Problem Code</td>" +
        //              "     <td style=\"width: 10px;\">:</td>" +
        //              "     <td class=\"ToolTipsData\" style=\"color: #FF0000\"><b>" + selectedRow[0]["ChangeReason"].ToString().Replace("_", "&nbsp;").Replace("\n", "") + "</b></td>" +
        //              "     </tr>";
        //            }

        //            string tooltipContent = @"<h3>" + selectedRow[0]["MachineID"].ToString() + "</h3>" +
        //              "<table>" +
        //              " <tr>" +
        //              "     <td style=\"width: 100px;\">Maker</td>" +
        //              "     <td style=\"width: 10px;\">:</td>" +
        //              "     <td class=\"ToolTipsData\">" + selectedRow[0]["equip_make"].ToString() + "</td>" +
        //              " </tr>" +
        //              " <tr>" +
        //              "     <td style=\"width: 100px;\">Model</td>" +
        //              "     <td style=\"width: 10px;\">:</td>" +
        //              "     <td class=\"ToolTipsData\">" + selectedRow[0]["equip_model"].ToString() + "</td>" +
        //              " </tr>" +
        //              " <tr>" +
        //              "     <td style=\"width: 100px;\">Department</td>" +
        //              "     <td style=\"width: 10px;\">:</td>" +
        //              "     <td class=\"ToolTipsData\">" + selectedRow[0]["DeptID"].ToString().Replace("_", "&nbsp;") + "</td>" +
        //              " </tr>" +
        //              " <tr>" +
        //              "     <td style=\"width: 100px;\">OEE State</td>" +
        //              "     <td style=\"width: 10px;\">:</td>" +
        //              "     <td class=\"ToolTipsData\">" + selectedRow[0]["OEE_STATE"].ToString() + "</td>" +
        //              " </tr>" +
        //              " <tr>" +
        //              "     <td style=\"width: 100px;\">OEE</td>" +
        //              "     <td style=\"width: 10px;\">:</td>" +
        //              "     <td class=\"ToolTipsData\">" + GetNumberWithOneDecimal(selectedRow[0]["OEE"].ToString()) + "</td>" +
        //              " </tr>" +
        //              problemCodeTag +
        //              " <tr>" +
        //              "     <td style=\"width: 100px;\">Transaction Time</td>" +
        //              "     <td style=\"width: 10px;\">:</td>" +
        //              "     <td class=\"ToolTipsData\">" + selectedRow[0]["DateStamp"].ToString().Replace("_", "&nbsp;") + "</td>" +
        //              " </tr>" +
        //              " <tr>" +
        //              "     <td style=\"width: 100px;\">Duration</td>" +
        //              "     <td style=\"width: 10px;\">:</td>" +
        //              "     <td class=\"ToolTipsData\">" + selectedRow[0]["Duration"].ToString() + "</td>" +
        //              " </tr>" +
        //              " <tr>" +
        //              "     <td style=\"width: 100px;\">Transacted By</td>" +
        //              "     <td style=\"width: 10px;\">:</td>" +
        //              "     <td class=\"ToolTipsData\">" + selectedRow[0]["ChangeBy"].ToString().Replace("_", "&nbsp;") + "</td>" +
        //              " </tr>";

        //            FloorMapLib.Process process = ProcessFactories.GetFactory();
        //            tooltipContent += pmTag +
        //              "</table>" +
        //              "</br>" +
        //              "<h5>Quick Link</h5>" +
        //              "<li><a href=\"" + ApplicationSetting.GetString("OeeChartURL", "default.aspx") +
        //                               "\" target = \"_blank\">*  OEEChart</a></li>" +
        //              "<li><a href=\"" + ApplicationSetting.GetString("RMSEventURL", "default.aspx") +
        //                            "\" target = \"_blank\">*   RMS Event</a></li>";
        //            switch (process.GetType().Name.ToLower())
        //            {
        //                case "wirebondprocess":
        //                    tooltipContent += "?PROCESS=WB" +
        //"&DEPT=" + selectedRow[0]["EQUIP_DEPT"].ToString() +
        //"&MODULE=" + selectedRow[0]["CLUSTERID"].ToString(); break;
        //                    //    case "dieattachprocess": tooltipContent += "?PROCESS=DA" +
        //                    //        "&DEPT=" + selectedRow[0]["DeptID"].ToString(); break;
        //                    //    // "&MODULE=" + selectedRow[0]["CLUSTERID"].ToString(); break;
        //                    //    case "backgrindprocess": tooltipContent += "?PROCESS=BG" +
        //                    //        "&DEPT=" + selectedRow[0]["EQUIP_DEPT"].ToString() +
        //                    //        "&MODULE=" + selectedRow[0]["CLUSTERID"].ToString(); break;
        //                    //    case "vmprocess": tooltipContent += "?PROCESS=VM" +
        //                    //        "&DEPT=" + selectedRow[0]["EQUIP_DEPT"].ToString() +
        //                    //        "&MODULE=" + selectedRow[0]["CLUSTERID"].ToString(); break;
        //                    //    case "underfillprocess": tooltipContent += "?PROCESS=UF" +
        //                    //        "&DEPT=" + selectedRow[0]["EQUIP_DEPT"].ToString() +
        //                    //        "&MODULE=" + selectedRow[0]["CLUSTERID"].ToString(); break;

        //                    //    // JF/CM/WS/SP ADDED BY NANDINI ON 20091116 FOR OEE CHART IN RESPECTIVE PROCESSES.

        //                    //    case "jetfluxprocess": tooltipContent += "?PROCESS=JF" +
        //                    //        "&DEPT=" + selectedRow[0]["EQUIP_DEPT"].ToString() +
        //                    //        "&MODULE=" + selectedRow[0]["CLUSTERID"].ToString(); break;
        //                    //    case "capmountprocess": tooltipContent += "?PROCESS=CM" +
        //                    //        "&DEPT=" + selectedRow[0]["EQUIP_DEPT"].ToString() +
        //                    //        "&MODULE=" + selectedRow[0]["CLUSTERID"].ToString(); break;
        //                    //    case "wafersawprocess": tooltipContent += "?PROCESS=WS" +
        //                    //        "&DEPT=" + selectedRow[0]["EQUIP_DEPT"].ToString() +
        //                    //        "&MODULE=" + selectedRow[0]["CLUSTERID"].ToString(); break;
        //                    //    case "solderpasteprocess": tooltipContent += "?PROCESS=SP" +
        //                    //        "&DEPT=" + selectedRow[0]["EQUIP_DEPT"].ToString() +
        //                    //        "&MODULE=" + selectedRow[0]["CLUSTERID"].ToString(); break;
        //                    //    default: break;
        //            }
        //            cell.Attributes.Add("onmouseover", "Tip('" + tooltipContent + "' ,CLICKSTICKY, true);");
        //            cell.Attributes.Add("onmouseout", "UnTip();");
        //        }
        //    }
        //}
        #endregion

    }
}