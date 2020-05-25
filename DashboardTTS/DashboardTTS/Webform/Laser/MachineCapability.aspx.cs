using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.DataVisualization.Charting;
using System.Drawing;
namespace DashboardTTS.Webform
{
    public partial class MachineCapability : System.Web.UI.Page
    {
    
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {

                    setYearDDL();

                    Common.Class.BLL.LMMSBom_BLL LmmsBom_BLL = new Common.Class.BLL.LMMSBom_BLL();
                    DataTable dt = LmmsBom_BLL.GetMachineCapabilityList();

                    if (dt == null)
                    {
                        this.dg_partList.Visible = false;
                    }else
                    {
                        this.dg_partList.Visible = true;

                        dg_partList.DataSource = dt.DefaultView;
                        dg_partList.DataBind();
                    }



                    this.ddlYear.SelectedValue = DateTime.Now.Year.ToString();
                    this.ddlMonth.SelectedValue = DateTime.Now.Month.ToString();

                    ((TextBox)this.dg_partList.Items[this.dg_partList.Items.Count - 1].Cells[4].FindControl("txtOrderQty")).Enabled = false;
                    this.btn_Edit.Visible = false;
                    //this.ChartModelPie.Visible = false;
                }
                catch (Exception ex)
                {
                    DBHelp.Reports.LogFile.Log("MachineCapability", "Page_Load Exception--" + ex.Message);
                }


                #region old logic
                //try
                //{
                //    this.lblUserHeader.Text = " Laser Machine Capability ";
                //    //this.ProdChart.Visible = false;
                //    //this.chart_Time.Visible = false;
                //    //this.chart_TimePie.Visible = false;
                    
             
                //    Common.Class.BLL.LMMSBom_BLL LmmsBom_BLL = new Common.Class.BLL.LMMSBom_BLL();
                //    DataTable dt =  LmmsBom_BLL.GetOnefoldPartList();

                //    dg_partList.DataSource = dt.DefaultView;
                //    dg_partList.DataBind();
                //}
                //catch (Exception ex)
                //{
                //    DBHelp.Reports.LogFile.Log("MachineCapability_Debug", "Page_Load Exception--" + ex.Message);
                //}
                #endregion

            }
        }

        
      


        

        protected void btnGenerate_Click(object sender, EventArgs e)
        {
            try
            {
                int iMachineCount = int.Parse(this.ddlMachineNo.SelectedValue);
                string sWorkingDays = this.txtWorkingDays.Text.Trim();
                if (sWorkingDays != "" && !Common.CommFunctions.isNumberic(sWorkingDays))
                {
                    this.txtWorkingDays.Text = "";
                    this.txtWorkingDays.Focus();
                    Common.CommFunctions.ShowMessage(this.Page, "Must key in numer!");
                    return;
                }

              
                double dTotalPCS = 0;
                double dTotalSET = 0;
                double dTotalSpendHour = 0;

                foreach (DataGridItem item in this.dg_partList.Items)
                {
                    #region  一些没填的, 填错的 行跳过
                    //total row 跳过
                    string sModel = item.Cells[0].Text;
                    if (sModel == "Total :")
                        continue;


                    //no bom detail,  隐藏 跳过
                    string sMaterialCount = item.Cells[2].Text;
                    if (sMaterialCount == "0")
                    {
                        item.Visible = false;
                        continue;
                    }


                    //没填的行 隐藏
                    string sOrderQty = ((TextBox)item.Cells[5].FindControl("txtOrderQty")).Text.Trim();

                    sOrderQty = sOrderQty.Split('(')[0];


                    if (sOrderQty == "")
                    {
                        item.Visible = false;
                        continue;
                    }
                    //填错的, 红色高亮, 跳过
                    else if (!Common.CommFunctions.isNumberic(sOrderQty))
                    {
                        ((TextBox)item.Cells[5].FindControl("txtOrderQty")).ForeColor = Color.Red;
                        continue;
                    }
                    else
                    {
                        ((TextBox)item.Cells[5].FindControl("txtOrderQty")).ForeColor = Color.Black;
                    }
                    #endregion

                    
                    double dCycleTimePerPCS = double.Parse(item.Cells[6].Text);
                    double dMaterialCount = double.Parse(item.Cells[2].Text);
                    double dSetQty = double.Parse(sOrderQty);   //填入的是 SET数量


                    //程序内部计算全部按照PCS单位计算.
                    double dPcsQty = dSetQty * dMaterialCount;

                    //计算 所选机器数量来完成所有order pcs 需要消耗的小时数.
                    double dSpendHours = Math.Round(dPcsQty * dCycleTimePerPCS / 3600, 2);
                    
                    
                    ((TextBox)item.Cells[4].FindControl("txtOrderQty")).Text = string.Format("{0}({1})", dSetQty, dPcsQty);
                    ((Label)item.Cells[5].FindControl("lbSpendHours")).Text = dSpendHours.ToString("0.00");


                    ((TextBox)item.Cells[4].FindControl("txtOrderQty")).Enabled = false;


                    dTotalPCS += dPcsQty;
                    dTotalSET += dSetQty;
                    dTotalSpendHour += dSpendHours;
                }



                //赋值total row
                ((TextBox)this.dg_partList.Items[this.dg_partList.Items.Count - 1].Cells[4].FindControl("txtOrderQty")).Text = string.Format("{0}({1})", dTotalSET, dTotalPCS);
                ((Label)this.dg_partList.Items[this.dg_partList.Items.Count - 1].Cells[5].FindControl("lbSpendHours")).Text = dTotalSpendHour.ToString("0.00");
                
                ((TextBox)this.dg_partList.Items[this.dg_partList.Items.Count - 1].Cells[4].FindControl("txtOrderQty")).Enabled = false;


                if (sWorkingDays == "")
                {
                    this.lbCapacity8H.Text = "";
                    this.lbCapacity12H.Text = "";
                    this.lbCapacity24H.Text = "";
                }
                else
                {
                    double dWorkingDays = double.Parse(sWorkingDays);
                    
                    this.lbCapacity8H.Text = Math.Round(dTotalSpendHour / (dWorkingDays * 9.75) / iMachineCount * 100, 2).ToString("0.00") + "%";
                    this.lbCapacity12H.Text = Math.Round(dTotalSpendHour / (dWorkingDays * 12) / iMachineCount * 100, 2).ToString("0.00") + "%";
                    this.lbCapacity24H.Text = Math.Round(dTotalSpendHour / (dWorkingDays * 24) / iMachineCount * 100, 2).ToString("0.00") + "%";
                }
                

                this.btn_Edit.Visible = true;
            }
            catch (Exception ee)
            {
                DBHelp.Reports.LogFile.Log("MachineCapability", "btnGenerate_Click execption:" + ee.ToString());
            }

            return;



            #region old logic
            //try
            //{
            //    double Total_PCS = 0;
            //    double Total_Set = 0;
            //    TimeSpan Total_ProcessTime = new TimeSpan();


            //    foreach (DataGridItem item in this.dg_partList.Items)
            //    {
            //        string Module  = item.Cells[0].Text;
            //        if (Module == "Total :")
            //        {
            //            ((TextBox)item.Cells[5].FindControl("txt_loadingPlanSet")).Text = Total_Set.ToString();
            //            ((TextBox)item.Cells[5].FindControl("txt_loadingPlanSet")).Enabled = false;
            //            ((TextBox)item.Cells[6].FindControl("txt_loadingPlanPCS")).Text = Total_PCS.ToString();
            //            ((TextBox)item.Cells[6].FindControl("txt_loadingPlanPCS")).Enabled = false;
            //            ((Label)item.Cells[7].FindControl("lb_Time")).Text = Math.Round(Total_ProcessTime.TotalHours, 2).ToString() + "H";
            //        }
            //        else
            //        {
            //            string Partnumber = item.Cells[1].Text;
            //            double BlockCount;
            //            double UnitCount;
            //            double cycleTime;
            //            try
            //            {
            //                 BlockCount = double.Parse(item.Cells[2].Text);
            //                 UnitCount = double.Parse(item.Cells[3].Text);
            //                 cycleTime = double.Parse(item.Cells[4].Text);
            //            }
            //            catch (Exception)
            //            {
            //                continue;
            //            }
                       
            //            string OrderSet = ((TextBox)item.Cells[5].FindControl("txt_loadingPlanSet")).Text;
            //            string OrderPCS = ((TextBox)item.Cells[6].FindControl("txt_loadingPlanPCS")).Text;

            //            Common.Class.BLL.LMMSBomDetail_BLL BLL = new Common.Class.BLL.LMMSBomDetail_BLL();
            //            int MaterialCount = BLL.MaterialCountByPart(Partnumber);

            //            if (OrderPCS != "" && OrderSet != "")
            //            {
            //                if (Common.CommFunctions.isNumberic(OrderPCS))
            //                {
            //                    ((TextBox)item.Cells[5].FindControl("txt_loadingPlanSet")).Text = Math.Round(double.Parse(OrderPCS) / MaterialCount, 0).ToString();

            //                    Total_PCS += double.Parse(OrderPCS);
            //                    Total_Set += Math.Round(double.Parse(OrderPCS) / MaterialCount, 0);
            //                }
            //                else
            //                {
            //                    item.Cells[5].BackColor = Color.HotPink;
            //                }
            //            }

            //            if (OrderSet != "" && OrderPCS == "")
            //            {
            //                if (Common.CommFunctions.isNumberic(OrderSet))
            //                {
            //                    ((TextBox)item.Cells[6].FindControl("txt_loadingPlanPCS")).Text = (double.Parse(OrderSet) * MaterialCount).ToString();
            //                    Total_Set += double.Parse(OrderSet);
            //                    Total_PCS += double.Parse(OrderSet) * MaterialCount;
            //                }
            //                else
            //                {
            //                    item.Cells[5].BackColor = Color.HotPink;
            //                }
            //            }

            //            if (OrderPCS != "" && OrderSet == "")
            //            {
            //                if (Common.CommFunctions.isNumberic(OrderPCS))
            //                {
            //                    ((TextBox)item.Cells[5].FindControl("txt_loadingPlanSet")).Text = Math.Round(double.Parse(OrderPCS) / MaterialCount, 0).ToString();

            //                    Total_PCS += double.Parse(OrderPCS);
            //                    Total_Set += Math.Round(double.Parse(OrderPCS) / MaterialCount, 0);
            //                }
            //                else
            //                {
            //                    item.Cells[5].BackColor = Color.HotPink;
            //                }
            //            }

                       

                     
                       

            //            double ProcessTime = 0;
            //            string PCS = ((TextBox)item.Cells[6].FindControl("txt_loadingPlanPCS")).Text;
            //            if (Common.CommFunctions.isNumberic( PCS))
            //            {
            //                ProcessTime =double.Parse(PCS) * (cycleTime / BlockCount / UnitCount);
            //            }

            //            if (ProcessTime != 0)
            //            {
            //                ((Label)item.Cells[7].FindControl("lb_Time")).Text = Math.Round(ProcessTime / 3600, 2) + "H";

            //                Total_ProcessTime += TimeSpan.FromSeconds(ProcessTime);
            //            }
            //            else
            //            {
            //                ((Label)item.Cells[7].FindControl("lb_Time")).Text = "";
            //            }
                      
            //        }
            //    }


            //    //string ShiftType = ddl_ShiftType.SelectedValue;
            //    string MachineID = ddl_MachineID.Text;
            //    #region machine Count
            //    int MachineCount = 0;
            //    if (MachineID == "No.1" || MachineID == "No.2" ||
            //        MachineID == "No.3" || MachineID == "No.4" ||
            //        MachineID == "No.5" || MachineID == "No.6" ||
            //        MachineID == "No.7" || MachineID == "No.8" )
            //    {
            //        MachineCount = 1;
            //    }
            //    else if (MachineID == "No.1 - No.5" )
            //    {
            //        MachineCount = 5;
            //    }
            //    else if ( MachineID == "No.6 - No.8")
            //    {
            //        MachineCount = 3;
            //    }
            //    else if (MachineID == "ALL")
            //    {
            //        MachineCount = 8;
            //    }
            //    #endregion

            

            //    double Hours = Total_ProcessTime.TotalHours;
               
            //    this.lb_SpendDays_8.Text = Math.Round(Hours / MachineCount / 9.75, 2) + "Days";
            //    this.lb_SpendDays_12.Text = Math.Round(Hours / MachineCount / 12, 2) + "Days";
            //    this.lb_SpendDays_24.Text = Math.Round(Hours / MachineCount / 24, 2) + "Days";

                ///return;
               

                //not use
                #region  show planing time bar & Pie
                //DataTable dt_timeBar = new DataTable();
                //dt_timeBar.Columns.Add("partNumber");
                //dt_timeBar.Columns.Add("LoadingPlan");
                //dt_timeBar.Columns.Add("timeCost");
                
                //DataTable dt_timePie = new DataTable();
                //dt_timePie.Columns.Add("module");
                //dt_timePie.Columns.Add("partNumber");
                //dt_timePie.Columns.Add("timeCost");


                //List<string> module_list = new List<string>();

                //foreach (DataGridItem item in dg_partList.Items)
                //{
                //    //list datagrid info
                //    string module = item.Cells[0].Text;
                //    string partNumber = item.Cells[1].Text;
                //    string blockCount = item.Cells[2].Text;
                //    string unitCount = item.Cells[3].Text;
                //    string cycleTime = item.Cells[4].Text;
                //    TextBox txt_OrderQty = (TextBox)item.Cells[5].FindControl("txt_loadingPlan");
                //    string orderQty = txt_OrderQty.Text.Trim();
                //    Label lb_time = ((Label)item.Cells[6].FindControl("lb_Time"));
                //    string rowNo = "";
                //    Button btn = item.FindControl("btn_GetRowNo") as Button;
                //    rowNo = btn.Attributes["Index"].Trim();


                //    if (!Common.CommFunctions.isNumberic(orderQty))
                //    {
                //        #region orderqty == "" || is not Number
                //        if (orderQty != "")
                //        {
                //            txt_OrderQty.BackColor = Color.Red;
                //        }
                //        lb_time.Text = "";
                //        continue;
                //        #endregion
                //    }
                //    else
                //    {
                //        txt_OrderQty.BackColor = Color.White;

                //        //process time  for datagrid column  process time
                //        double totalSecond = double.Parse(orderQty) / int.Parse(blockCount) / int.Parse(unitCount) * double.Parse(cycleTime);
                //        double partCostTime = Math.Round(totalSecond / 3600, 2);
                //        lb_time.Text = partCostTime.ToString() + "hours";

                //        //Time bar dt 
                //        DataRow row = dt_timeBar.NewRow();
                //        row["partNumber"] = partNumber;
                //        row["timeCost"] = partCostTime;
                //        row["LoadingPlan"] = orderQty;
                        
                //        dt_timeBar.Rows.InsertAt(row, 0);


                //        //get module list
                //        try
                //        {
                //            if (!module_list.Exists(x => x == module))
                //            {
                //                module_list.Add(module);
                //            }
                //        }
                //        catch (Exception ee)
                //        {
                //            throw;
                //        }


                //        //Time Pie
                //        double eachPartTime = double.Parse(orderQty) * (double.Parse(cycleTime) / (int.Parse(blockCount) * (int.Parse(unitCount))) / (9.75 * 3600)) * 24;//hours

                //        DataRow row_pie = dt_timePie.NewRow();
                //        row_pie["module"] = module;
                //        row_pie["partNumber"] = partNumber;
                //        row_pie["timeCost"] = eachPartTime;
                //        dt_timePie.Rows.Add(row_pie);
                        
                //    }
                //}

                ////Time bar chart
                //Session["dt_timeBar"] = dt_timeBar;
                //if (dt_timeBar.Rows.Count >0)
                //{
                //    this.chart_Time.Visible = true;
                //   // ChartDisplay_SpendingTimeBar(dt_timeBar);
                //}
                //else
                //{
                //    this.chart_Time.Visible = false;
                //    this.ProdChart.Visible = false;

                //    ClientScript.RegisterStartupScript(ClientScript.GetType(), "", "alert('Order Quantity can not be all empty!')", true);
                //  //  Chart_Refresh();
                //    return;
                //}
                

                //#region calculator each module time
                //DataTable dt_module = new DataTable();
                //dt_module.Columns.Add("module");
                //dt_module.Columns.Add("timeCost");


                //foreach (string  module in module_list)
                //{
                //    DataRow row = dt_module.NewRow();
                //    double module_Time = 0.0;

                //    foreach (DataRow x in dt_timePie.Rows)
                //    {
                //        if (x["module"].ToString()== module)
                //        {
                //            module_Time += double.Parse(x["timeCost"].ToString());
                //        }
                //    }

                //    row["module"] = module;
                //    row["timeCost"] = module_Time;
                //    dt_module.Rows.Add(row);
                //}
                //#endregion
                
                ////Time Pie Chart
                //Session["dt_timePie"] = dt_module;
                //if (dt_module.Rows.Count > 0)
                //{
                //    this.chart_TimePie.Visible = true;
                //    ChartDisplay_SpendingTimePie(dt_module);
                //}
                //else
                //{
                //    this.chart_TimePie.Visible = false;
                //    this.ProdChart.Visible = false;
                    
                //    Chart_Refresh();
                //    return;
                //}
                #endregion
                
                #region Text confirm
                //if (dt_PartList.Rows.Count == 0)
                //{
                //    ClientScript.RegisterStartupScript(ClientScript.GetType(), "", "alert('Please add a Part first!')", true);
                //    Chart_Refresh();
                //    return;
                //}

                //if (txt_WorkingDays.Text == "")
                //{
                //    ClientScript.RegisterStartupScript(ClientScript.GetType(), "", "alert('Please input Working Days!')", true);
                //    Chart_Refresh();
                //    return;
                //}
                //else if (!Common.CommFunctions.isNumberic(txt_WorkingDays.Text))
                //{
                //    ClientScript.RegisterStartupScript(ClientScript.GetType(), "", "alert('Please input number in Working Days!')", true);
                //    Chart_Refresh();
                //    return;
                //}
                #endregion

                #region productivity capacity chart
                //int MachineCount = 0;
                //if (this.ddlMachineID.Text == "No.1" ||
                //    this.ddlMachineID.Text == "No.2" ||
                //    this.ddlMachineID.Text == "No.3" ||
                //    this.ddlMachineID.Text == "No.4" ||
                //    this.ddlMachineID.Text == "No.5" ||
                //    this.ddlMachineID.Text == "No.6" ||
                //    this.ddlMachineID.Text == "No.7" ||
                //    this.ddlMachineID.Text == "No.8")
                //{
                //    MachineCount = 1;
                //}
                //else if (this.ddlMachineID.Text == "No.1 - No.5")
                //{
                //    MachineCount = 5;
                //}
                //else if (this.ddlMachineID.Text == "No.6 - No.8")
                //{
                //    MachineCount = 3;
                //}
                //else if (this.ddlMachineID.Text == "ALL")
                //{
                //    MachineCount = 8;
                //}


                //double TotalPlanTime = 0;
                //foreach (DataRow row in dt_timeBar.Rows)
                //{
                //    TotalPlanTime += double.Parse(row["timeCost"].ToString());
                //}


                //DataTable dt_Capability = new DataTable();
                //dt_Capability.Columns.Add("Type");
                //dt_Capability.Columns.Add("TimeNormalPer");
                //dt_Capability.Columns.Add("TimeLackPer");
                //dt_Capability.Columns.Add("TimeBalPer");
               



                //DataRow dr_Normal = dt_Capability.NewRow();
                //DataRow dr_OT = dt_Capability.NewRow();
                //DataRow dr_ShiftOT = dt_Capability.NewRow();

                //dr_Normal[0] = "Normal";//"8:00-17:45 (Normal Shift)";
                //dr_OT[0] = "OT";//"8:00-20:00 (Normal Shift with OT)";
                //dr_ShiftOT[0] = "ShiftOT";//"8:00-7:59 (2 Shift with OT)";

                //double TotalTime = 0;

                //List<double> list_TotalTime = new List<double>();


                ////9.75
                //TotalTime = 9.75 * MachineCount * double.Parse(this.txt_WorkingDays.Text);
                //dr_Normal[1] = TotalPlanTime <= TotalTime ? TotalPlanTime : TotalTime;
                //dr_OT[1] = TotalPlanTime <= TotalTime ? 0 : TotalPlanTime - TotalTime;
                //dr_ShiftOT[1] = TotalPlanTime <= TotalTime ? TotalTime - TotalPlanTime : 0;
                //list_TotalTime.Add(TotalTime);




                ////dr_Normal[1] = TotalPlanTime >= TotalTime ? 100 : TotalPlanTime / TotalTime * 100;
                ////dr_OT[1] = TotalTime >= TotalPlanTime ? 0 : Math.Abs(TotalTime - TotalPlanTime) / TotalPlanTime * 100;//TotalTime >= TotalPlanTime ? 0 : Math.Abs(TotalTime - TotalPlanTime) / TotalPlanTime * 100;
                ////dr_ShiftOT[1] = TotalPlanTime >= TotalTime ? 0: (TotalTime - TotalPlanTime) / TotalTime * 100;
                ////list_TotalTime.Add(TotalTime);

                ////12
                //TotalTime = 12 * MachineCount * double.Parse(this.txt_WorkingDays.Text);
                //dr_Normal[2] = TotalPlanTime <= TotalTime ? TotalPlanTime : TotalTime;
                //dr_OT[2] = TotalPlanTime <= TotalTime ? 0 : TotalPlanTime - TotalTime;
                //dr_ShiftOT[2] = TotalPlanTime <= TotalTime ? TotalTime - TotalPlanTime : 0;
                //list_TotalTime.Add(TotalTime);


                ////dr_Normal[2] = TotalPlanTime >= TotalTime ? 100 : TotalPlanTime / TotalTime * 100;
                ////dr_OT[2] = TotalTime >= TotalPlanTime ? 0 : Math.Abs(TotalTime - TotalPlanTime) / TotalPlanTime * 100;// TotalTime >= TotalPlanTime ? 0 : Math.Abs(TotalTime - TotalPlanTime) / TotalTime * 100;
                ////dr_ShiftOT[2] = TotalPlanTime >= TotalTime ? 0 : (TotalTime - TotalPlanTime) / TotalTime * 100;
                ////list_TotalTime.Add(TotalTime);

                ////24
                //TotalTime = 24 * MachineCount * double.Parse(this.txt_WorkingDays.Text);
                //dr_Normal[3] = TotalPlanTime <= TotalTime ? TotalPlanTime : TotalTime;
                //dr_OT[3] = TotalPlanTime <= TotalTime ? 0 : TotalPlanTime - TotalTime;
                //dr_ShiftOT[3] = TotalPlanTime <= TotalTime ? TotalTime - TotalPlanTime : 0;
                //list_TotalTime.Add(TotalTime);


                ////dr_Normal[3] = TotalPlanTime >= TotalTime ? 100 : TotalPlanTime / TotalTime * 100;
                ////dr_OT[3] = TotalTime >= TotalPlanTime ? 0 : Math.Abs(TotalTime - TotalPlanTime) / TotalPlanTime * 100;// TotalTime >= TotalPlanTime ? 0 : Math.Abs(TotalTime - TotalPlanTime) / TotalTime * 100;
                ////dr_ShiftOT[3] = TotalPlanTime >= TotalTime ? 0 : (TotalTime - TotalPlanTime) / TotalTime * 100;
                ////list_TotalTime.Add(TotalTime);

                //dt_Capability.Rows.Add(dr_Normal);
                //dt_Capability.Rows.Add(dr_OT);
                //dt_Capability.Rows.Add(dr_ShiftOT);

                //list_TotalTime.Add(TotalPlanTime);
                //Session["List_TotalTime"] = list_TotalTime;

                //#region old logic not use

                ////get part list  & all order QTY
                ////double LoadingPlanCount = 0;
                ////List<string> list_PartNumber = new List<string>();
                ////foreach (DataRow row in dt_timeBar.Rows)
                ////{
                ////    string partNumber = row["partNumber"].ToString();
                ////    list_PartNumber.Add(partNumber);
                ////    LoadingPlanCount += double.Parse(row["LoadingPlan"].ToString());
                ////}
                ////Common.Class.BLL.LMMSBom_BLL bom_bll = new Common.Class.BLL.LMMSBom_BLL();
                ////double TimeCostPerUnit = bom_bll.GetCycTimeByMultPart(list_PartNumber, "machineID_notuse");
                ////TimeCostPerUnit = TimeCostPerUnit / MachineCount;

                ////DataTable dt = new DataTable();
                ////dt.Columns.Add("MachineID");            //0
                ////dt.Columns.Add("SEQ");                  //1
                ////dt.Columns.Add("ShiftName");            //2
                ////dt.Columns.Add("Planning");             //3
                ////dt.Columns.Add("MachineCapacity");      //4
                ////dt.Columns.Add("Compare0");             //5
                ////dt.Columns.Add("Compare1");             //6
                ////dt.Columns.Add("Compare1_Percentage");  //7
                ////dt.Columns.Add("Compare2");             //8
                ////dt.Columns.Add("Compare2_Percentage");  //9




                //////9.75小时 工时 产量
                ////DataRow dr;
                ////dr = dt.NewRow();
                ////dr[0] = ddlMachineID.Text;
                ////dr[1] = "1";
                ////dr[2] = "8:00-17:45 (Normal Shift)";
                ////dr[3] = LoadingPlanCount;
                ////dr[4] = double.Parse(txt_WorkingDays.Text) * 9.75 * 60 * 60 / TimeCostPerUnit; //正常工作时间 总产量
                ////if (LoadingPlanCount > double.Parse(txt_WorkingDays.Text) * 9.75 * 60 * 60 / TimeCostPerUnit) //计划产量 > 实际产量
                ////{
                ////    dr[5] = Math.Round(   double.Parse(txt_WorkingDays.Text) * 9.75 * 60 * 60 / TimeCostPerUnit      , 2)   ;
                ////    dr[6] = Math.Round(LoadingPlanCount - double.Parse(txt_WorkingDays.Text) * 9.75 * 60 * 60 / TimeCostPerUnit,2);//未完成产量
                ////    dr[7] = "-" + Math.Round((LoadingPlanCount - double.Parse(txt_WorkingDays.Text) * 9.75 * 60 * 60 / TimeCostPerUnit) * 100 / LoadingPlanCount, 2).ToString() + "%";//未完成产量占比
                ////    dr[8] = 0.0;
                ////    dr[9] = "0%";
                ////}
                ////else  //计划产量 < 实际产量    --产量剩余
                ////{
                ////    dr[5] = LoadingPlanCount;
                ////    dr[8] = Math.Round(double.Parse(txt_WorkingDays.Text) * 9.75 * 60 * 60 / TimeCostPerUnit - LoadingPlanCount,2); //多余产量
                ////    dr[9] = "Bal:" + Math.Round((double.Parse(txt_WorkingDays.Text) * 9.75 * 60 * 60 / TimeCostPerUnit - LoadingPlanCount) * 100 / LoadingPlanCount, 2).ToString() + "%";//多余产量占比
                ////    dr[6] = 0.0;
                ////    dr[7] = "0%";
                ////}
                ////dt.Rows.Add(dr);

                //////12小时 工时 产量
                ////dr = dt.NewRow();
                ////dr[0] = ddlMachineID.Text;
                ////dr[1] = "2";
                ////dr[2] = "8:00-20:00 (Normal Shift with OT)";
                ////dr[3] = LoadingPlanCount;
                ////dr[4] = LoadingPlanCount * 12 * 60 * 60 / TimeCostPerUnit;
                ////if (LoadingPlanCount > double.Parse(txt_WorkingDays.Text) * 12 * 60 * 60 / TimeCostPerUnit)
                ////{
                ////    dr[5] = Math.Round( double.Parse(txt_WorkingDays.Text) * 12 * 60 * 60 / TimeCostPerUnit  ,2);
                ////    dr[6] = Math.Round(LoadingPlanCount - double.Parse(txt_WorkingDays.Text) * 12 * 60 * 60 / TimeCostPerUnit, 2);
                ////    dr[7] = "-" + Math.Round((LoadingPlanCount - double.Parse(txt_WorkingDays.Text) * 12 * 60 * 60 / TimeCostPerUnit) * 100 / LoadingPlanCount, 2).ToString() + "%";
                ////    dr[8] = 0.0;
                ////    dr[9] = "0%";
                ////}
                ////else
                ////{
                ////    dr[5] = LoadingPlanCount;
                ////    dr[8] = Math.Round(double.Parse(txt_WorkingDays.Text) * 12 * 60 * 60 / TimeCostPerUnit - LoadingPlanCount,   2);
                ////    dr[9] = "Bal:" + Math.Round((double.Parse(txt_WorkingDays.Text) * 12 * 60 * 60 / TimeCostPerUnit - LoadingPlanCount) * 100 / LoadingPlanCount, 2).ToString() + "%";
                ////    dr[6] = 0.0;
                ////    dr[7] = "0%";
                ////}
                ////dt.Rows.Add(dr);

                //////24小时 工时 产量
                ////dr = dt.NewRow();
                ////dr[0] = ddlMachineID.Text;
                ////dr[1] = "3";
                ////dr[2] = "8:00-7:59 (2 Shift with OT)";
                ////dr[3] = LoadingPlanCount;
                ////dr[4] = double.Parse(txt_WorkingDays.Text) * 24 * 60 * 60 / TimeCostPerUnit;
                ////if (LoadingPlanCount > double.Parse(txt_WorkingDays.Text) * 24 * 60 * 60 / TimeCostPerUnit)
                ////{
                ////    dr[5] =Math.Round( double.Parse(txt_WorkingDays.Text) * 24 * 60 * 60 / TimeCostPerUnit,  2);
                ////    dr[6] = Math.Round(LoadingPlanCount - double.Parse(txt_WorkingDays.Text) * 24 * 60 * 60 / TimeCostPerUnit,2);
                ////    dr[7] = "-" + Math.Round((LoadingPlanCount - double.Parse(txt_WorkingDays.Text) * 24 * 60 * 60 / TimeCostPerUnit) * 100 / LoadingPlanCount, 2).ToString() + "%";
                ////    dr[8] = 0.0;
                ////    dr[9] = "0%";
                ////}
                ////else
                ////{
                ////    dr[5] = LoadingPlanCount;
                ////    dr[8] = Math.Round (double.Parse(txt_WorkingDays.Text) * 24 * 60 * 60 / TimeCostPerUnit - LoadingPlanCount,  2);
                ////    dr[9] = "Bal:" + Math.Round((double.Parse(txt_WorkingDays.Text) * 24 * 60 * 60 / TimeCostPerUnit - LoadingPlanCount) * 100 / LoadingPlanCount, 2).ToString() + "%";
                ////    dr[6] = 0.0;
                ////    dr[7] = "0%";
                ////}
                ////dt.Rows.Add(dr);
                ////#endregion 


                ////Session["dt_ForProdChart"] = dt;
                //#endregion


                //ChartDisplay_Job(dt_Capability, list_TotalTime);
                #endregion


            //}
            //catch ( Exception ee)
            //{
            //    DBHelp.Reports.LogFile.Log("MachineCapability_Debug", "btnGenerate_Click execption:"+ee.ToString());
            //}
            #endregion
        }


        //private class SearchParaCls
        //{
        //    public string partNumber;
        //    public string Days;
        //    public string MachineID;
        //    public string Planning;
        //    public int BlockCount;
        //    public int UnitCount;
        //    public Dictionary<string, double> dPartnCycTime = new Dictionary<string, double>();
        //}


        #region chart
        //void ChartDisplay_SpendingTimeBar(DataTable dt_partInfoList)
        //{
        //    this.chart_Time.Visible = true;
        //    this.chart_Time.Series.Clear();
        //    this.chart_Time.ChartAreas.Clear();
        //    this.chart_Time.ChartAreas.Add("Area1");

        //    #region chart CSS

        //    int count = dt_partInfoList.Rows.Count;
        //    this.chart_Time.Height = count * 40 + 100;

        //    chart_Time.BackColor = Color.FromArgb(245, 245, 250);
        //    chart_Time.BackGradientStyle = GradientStyle.None;

        //    //图表区背景
        //    chart_Time.ChartAreas[0].BackColor = Color.FromArgb(245, 245, 250);
        //    chart_Time.ChartAreas[0].BorderColor = Color.Transparent;
        //    //X轴标签间距
        //    chart_Time.ChartAreas[0].AxisX.IntervalAutoMode = IntervalAutoMode.VariableCount;
        //    chart_Time.ChartAreas[0].AxisX.LabelStyle.IsStaggered = false;
        //    chart_Time.ChartAreas[0].AxisX.LabelStyle.Angle = 0;
        //    chart_Time.ChartAreas[0].AxisX.TitleFont = new Font("Microsoft Sans Serif", 8f, FontStyle.Regular);
        //    chart_Time.ChartAreas[0].AxisX.TitleForeColor = Color.DarkBlue;
        //    //X坐标轴颜色
        //    chart_Time.ChartAreas[0].AxisX.LineColor = Color.DarkBlue;
        //    chart_Time.ChartAreas[0].AxisX.LabelStyle.ForeColor = Color.DarkBlue;
        //    chart_Time.ChartAreas[0].AxisX.LabelStyle.Font = new Font("Microsoft Sans Serif", 8f, FontStyle.Regular);
        //    chart_Time.ChartAreas[0].AxisX.LabelStyle.IsEndLabelVisible = true;
        //    chart_Time.ChartAreas[0].AxisX.LabelStyle.Enabled = true;
        //    //Y坐标轴标题
        //    chart_Time.ChartAreas[0].AxisX.Title = "Plan";
        //    chart_Time.ChartAreas[0].AxisX.TitleFont = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular);
        //    chart_Time.ChartAreas[0].AxisX.TitleForeColor = Color.DarkBlue;
        //    //chart_Time.ChartAreas[0].AxisY.TextOrientation = TextOrientation.Rotated270;
        //    //chart_Time.ChartAreas[0].AxisX.ToolTip = "axisX-tooltip";

        //    //X轴网络线条
        //    chart_Time.ChartAreas[0].AxisX.MajorGrid.Enabled = true;
        //    chart_Time.ChartAreas[0].AxisX.MajorGrid.LineColor = System.Drawing.Color.Gainsboro;

        //    //Y坐标轴颜色
        //    chart_Time.ChartAreas[0].AxisY.LineColor = System.Drawing.Color.Gainsboro;
        //    chart_Time.ChartAreas[0].AxisY.LabelStyle.ForeColor = Color.DarkBlue;
        //    chart_Time.ChartAreas[0].AxisY.LabelStyle.Font = new Font("Microsoft Sans Serif", 8f, FontStyle.Regular);
        //    //Y坐标轴标题
        //    //chart_Time.ChartAreas[0].AxisY.Title = "Spend Days";
        //    //chart_Time.ChartAreas[0].AxisY.TitleFont = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular);
        //    //chart_Time.ChartAreas[0].AxisY.TitleForeColor = Color.DarkBlue;
        //    //chart_Time.ChartAreas[0].AxisY.TextOrientation = TextOrientation.Rotated270;
        //    //chart_Time.ChartAreas[0].AxisY.ToolTip = "axisY-tooltip";
        //    //Y轴网格线条
        //    chart_Time.ChartAreas[0].AxisY.MajorGrid.Enabled = false;
        //    chart_Time.ChartAreas[0].AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Dash;
        //    chart_Time.ChartAreas[0].AxisY.MajorGrid.LineWidth = 1;
        //    chart_Time.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.Gainsboro;
        //    chart_Time.ChartAreas[0].AxisY.Enabled = AxisEnabled.False;

        //    chart_Time.ChartAreas[0].BackGradientStyle = GradientStyle.TopBottom;

        //    #endregion

        //    System.Web.UI.DataVisualization.Charting.Series series = new Series();

        //    #region Series CSS
        //    series.ChartType = SeriesChartType.Bar;//横向的条形柱状图
        //    series.ChartArea = this.chart_Time.ChartAreas[0].Name;
        //    // dataSeries_JobOutPut.L = true;
        //    series.Name = "Capacity";
        //    series.YAxisType = AxisType.Primary;
        //    series.XValueType = ChartValueType.Int32;
        //    series.YAxisType = AxisType.Primary;
        //    series.YValueType = ChartValueType.Int32;
        //    series.IsVisibleInLegend = true;

        //    series.LabelForeColor = Color.SteelBlue;
        //    series.ToolTip = "#VAL" + " hours";
        //    //series.ToolTip = "#VALX -- #VAL";     //鼠标移动到对应点显示数值

        //    Legend legend = new Legend("Capacity");
        //    legend.Title = "Capacity";

        //    series.Color = Color.Lime;
        //    series.LegendText = legend.Name;

        //    series.Palette = ChartColorPalette.Bright;
        //    #endregion

        //    try
        //    {


        //        foreach (DataRow row in dt_partInfoList.Rows)
        //        {
        //            DataPoint dataPoint = new DataPoint();

        //            string str_Time = row["timeCost"].ToString();
        //            double Time = double.Parse(str_Time);

        //            #region Point value CSS
        //            dataPoint.Label = str_Time + " hours";
        //            dataPoint.AxisLabel = row["partNumber"].ToString();//原本为txtLoadingPlan.Text;
        //            dataPoint.YValues[0] = Time;
        //            dataPoint.Color = System.Drawing.Color.SkyBlue;

        //            dataPoint.BackGradientStyle = GradientStyle.LeftRight;
        //            dataPoint.BackSecondaryColor = Color.SteelBlue;
        //            dataPoint.BorderColor = Color.Black;
        //            dataPoint.LabelForeColor = Color.Black;
        //            #endregion

        //            series.Points.Add(dataPoint);
        //        }
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }


        //    this.chart_Time.Series.Add(series);


        //    chart_Time.Titles.Clear();

        //    chart_Time.Titles.Add(" Spending Times Bar ");//Machine " + sp.MachineID + "
        //    chart_Time.Titles[0].ForeColor = Color.Black;
        //    chart_Time.Titles[0].Font = new Font("Microsoft Sans Serif", 16f, FontStyle.Bold);
        //    chart_Time.Titles[0].Alignment = ContentAlignment.TopCenter;


        //}

            

        //void ChartDisplay_Job(DataTable dt,List<double> list_TotalTime)
        //{
        //    this.ProdChart.Visible = true;

        //    #region "Drawing Chart"
        //    try
        //    {
        //        this.ProdChart.Series.Clear();
        //        this.ProdChart.ChartAreas.Clear();
        //        this.ProdChart.ChartAreas.Add("ChartArea1");

        //        if (dt.Rows.Count > 0)
        //        {
        //            #region chart css

        //            double iTotalCount = 0.0;


        //            ProdChart.BackColor =  Color.FromArgb( 245, 245, 250);
        //            //ProdChart.BackSecondaryColor = Color.Transparent;
        //            ProdChart.BackGradientStyle = GradientStyle.None;


        //            //图表区背景
        //            ProdChart.ChartAreas[0].BackColor =  Color.FromArgb(245, 245, 250);
        //            //ProdChart.ChartAreas[0].BackSecondaryColor = Color.Transparent;
        //            ProdChart.ChartAreas[0].BorderColor = Color.Transparent;
        //            //X轴标签间距
        //            ProdChart.ChartAreas[0].AxisX.IntervalAutoMode = IntervalAutoMode.VariableCount;
        //            // ProdChart.ChartAreas[0].AxisX.Interval = 0;
        //            ProdChart.ChartAreas[0].AxisX.LabelStyle.IsStaggered = false;
        //            ProdChart.ChartAreas[0].AxisX.LabelStyle.Angle = 0;
        //            ProdChart.ChartAreas[0].AxisX.TitleFont = new Font("Microsoft Sans Serif", 8f, FontStyle.Regular);
        //            ProdChart.ChartAreas[0].AxisX.TitleForeColor = Color.DarkBlue;

        //            //X坐标轴颜色
        //            ProdChart.ChartAreas[0].AxisX.LineColor = Color.DarkBlue;
        //            ProdChart.ChartAreas[0].AxisX.LabelStyle.ForeColor = Color.DarkBlue;
        //            ProdChart.ChartAreas[0].AxisX.LabelStyle.Font = new Font("Microsoft Sans Serif", 8f, FontStyle.Regular);
        //            ProdChart.ChartAreas[0].AxisX.LabelStyle.IsEndLabelVisible = true;
        //            ProdChart.ChartAreas[0].AxisX.LabelStyle.Enabled = true;

        //            //X轴网络线条
        //            ProdChart.ChartAreas[0].AxisX.MajorGrid.Enabled = true;
        //            ProdChart.ChartAreas[0].AxisX.MajorGrid.LineColor = System.Drawing.Color.Gainsboro;

        //            //Y坐标轴颜色
        //            ProdChart.ChartAreas[0].AxisY.LineColor = System.Drawing.Color.Gainsboro;
        //            ProdChart.ChartAreas[0].AxisY.LabelStyle.ForeColor = Color.DarkBlue;
        //            ProdChart.ChartAreas[0].AxisY.LabelStyle.Font = new Font("Microsoft Sans Serif", 8f, FontStyle.Regular);
        //            //Y坐标轴标题
        //            ProdChart.ChartAreas[0].AxisY.Title = "Time (hour)";
        //            ProdChart.ChartAreas[0].AxisY.TitleFont = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular);
        //            ProdChart.ChartAreas[0].AxisY.TitleForeColor = Color.DarkBlue;
        //            ProdChart.ChartAreas[0].AxisY.TextOrientation = TextOrientation.Rotated270;
        //            ProdChart.ChartAreas[0].AxisY.ToolTip = "Time (hour)";
        //            //Y轴网格线条
        //            ProdChart.ChartAreas[0].AxisY.MajorGrid.Enabled = true;
        //            ProdChart.ChartAreas[0].AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Dash;
        //            ProdChart.ChartAreas[0].AxisY.MajorGrid.LineWidth = 1;
        //            ProdChart.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.Gainsboro;


        //            //Y坐标轴颜色
        //            ProdChart.ChartAreas[0].AxisY2.LineColor = Color.OrangeRed;
        //            ProdChart.ChartAreas[0].AxisY2.LabelStyle.ForeColor = Color.Yellow;
        //            ProdChart.ChartAreas[0].AxisY2.LabelStyle.Font = new Font("Microsoft Sans Serif", 8f, FontStyle.Regular);
        //            //Y坐标轴标题
        //            ProdChart.ChartAreas[0].AxisY2.Title = " Loading Plan ";
        //            ProdChart.ChartAreas[0].AxisY2.TitleFont = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular);
        //            ProdChart.ChartAreas[0].AxisY2.TitleForeColor = Color.OrangeRed;
        //            ProdChart.ChartAreas[0].AxisY2.TextOrientation = TextOrientation.Rotated90;
        //            ProdChart.ChartAreas[0].AxisY2.ToolTip = " Loading Plan ";
        //            //Y轴网格线条
        //            ProdChart.ChartAreas[0].AxisY2.MajorGrid.Enabled = true;
        //            ProdChart.ChartAreas[0].AxisY2.MajorGrid.LineColor = Color.Yellow;
        //            ProdChart.ChartAreas[0].AxisY2.MajorGrid.LineDashStyle = ChartDashStyle.Dash;
        //            ProdChart.ChartAreas[0].AxisY2.MajorGrid.LineWidth = 1;

        //            ProdChart.ChartAreas[0].BackGradientStyle = GradientStyle.TopBottom;



        //            Legend legend = new Legend("Capacity");
        //            legend.Title = "Capacity";

        //            ProdChart.Legends.Add(legend);
        //            ProdChart.Legends[0].Position.Auto = false;
        //            #endregion


        //            //dt_Capability.Columns.Add("Type");
        //            //dt_Capability.Columns.Add("TimeNormalPer");
        //            //dt_Capability.Columns.Add("TimeLackPer");
        //            //dt_Capability.Columns.Add("TimeBalPer");


        //            double normalTime = list_TotalTime[0];
        //            double OTTime = list_TotalTime[1];
        //            double ShiftOTTime = list_TotalTime[2];
        //            double TotalPlanTime = list_TotalTime[3];


        //            foreach (DataRow dr in dt.Rows)
        //            {
        //                #region series css
        //                System.Web.UI.DataVisualization.Charting.Series dataSeries_JobOutPut = new Series();
        //                dataSeries_JobOutPut.ChartType = SeriesChartType.StackedColumn;

        //                dataSeries_JobOutPut.ChartArea = this.ProdChart.ChartAreas[0].Name;
        //                // dataSeries_JobOutPut.L = true;
        //                dataSeries_JobOutPut.Name = "Capacity " + dr[0].ToString();
        //                dataSeries_JobOutPut.XAxisType = AxisType.Primary;
        //                dataSeries_JobOutPut.XValueType = ChartValueType.String;
        //                dataSeries_JobOutPut.YAxisType = AxisType.Primary;
        //                dataSeries_JobOutPut.YValueType = ChartValueType.Double;
        //                dataSeries_JobOutPut.IsVisibleInLegend = true;


        //                //dataSeries_JobOutPut.Label = "#VAL";                //设置显示X Y的值    
        //                dataSeries_JobOutPut.LabelForeColor = Color.SteelBlue;
        //                dataSeries_JobOutPut.ToolTip = "#VALX -- #VAL " + "H";     //鼠标移动到对应点显示数值


        //                dataSeries_JobOutPut.Color = Color.Lime;
        //                dataSeries_JobOutPut.LegendText = legend.Name;
        //                dataSeries_JobOutPut.Palette = ChartColorPalette.Bright;
        //                #endregion


        //                 switch (dr[0].ToString())
        //                {
        //                    case "Normal"://blue
        //                        {
        //                            DataPoint dp = new DataPoint();
        //                            dp.Color = System.Drawing.Color.SkyBlue;
        //                            dp.BackGradientStyle = GradientStyle.LeftRight;
        //                            dp.BackSecondaryColor = Color.SteelBlue;
        //                            dp.BorderColor = Color.Black;
        //                            dp.LabelForeColor = Color.Black;

        //                            dp.AxisLabel = "8:00-17:45 (Normal Shift)"; 
        //                            dp.YValues[0] = double.Parse(dr[1].ToString());
        //                            dp.Label = ( TotalPlanTime <= normalTime ?   Math.Round(double.Parse(dr[1].ToString())/normalTime *100 ,2) :  Math.Round(double.Parse(dr[1].ToString()) / TotalPlanTime * 100, 2)).ToString() + "%";
        //                            dataSeries_JobOutPut.Points.Add(dp);



        //                            DataPoint dp1 = new DataPoint();
        //                            dp1.Color = System.Drawing.Color.SkyBlue;
        //                            dp1.BackGradientStyle = GradientStyle.LeftRight;
        //                            dp1.BackSecondaryColor = Color.SteelBlue;
        //                            dp1.BorderColor = Color.Black;
        //                            dp1.LabelForeColor = Color.Black;

        //                            dp1.AxisLabel = "8:00-20:00 (Normal Shift with OT)";
        //                            dp1.YValues[0] = double.Parse(dr[2].ToString());
        //                            //dp1.Label = Math.Round(double.Parse(dr[2].ToString()) / TotalPlanTime * 100, 2).ToString() + "%";
        //                            dp1.Label = (TotalPlanTime <= normalTime ? Math.Round(double.Parse(dr[2].ToString()) / OTTime * 100, 2) : Math.Round(double.Parse(dr[2].ToString()) / TotalPlanTime * 100, 2)).ToString() + "%";
        //                            dataSeries_JobOutPut.Points.Add(dp1); 



        //                            DataPoint dp2 = new DataPoint();
        //                            dp2.Color = System.Drawing.Color.SkyBlue;
        //                            dp2.BackGradientStyle = GradientStyle.LeftRight;
        //                            dp2.BackSecondaryColor = Color.SteelBlue;
        //                            dp2.BorderColor = Color.Black;
        //                            dp2.LabelForeColor = Color.Black;

        //                            dp2.AxisLabel = "8:00-7:59 (2 Shift with OT)";
        //                            dp2.YValues[0] = double.Parse(dr[3].ToString());
        //                            //dp2.Label = Math.Round(double.Parse(dr[3].ToString()) / TotalPlanTime * 100, 2).ToString() + "%";
        //                            dp2.Label = (TotalPlanTime <= normalTime ? Math.Round(double.Parse(dr[3].ToString()) / ShiftOTTime * 100, 2) : Math.Round(double.Parse(dr[3].ToString()) / TotalPlanTime * 100, 2)).ToString() + "%";
        //                            dataSeries_JobOutPut.Points.Add(dp2);

        //                        }
        //                        break;
        //                    case "OT":
        //                        {
        //                            DataPoint dp = new DataPoint();
        //                            dp.Color = System.Drawing.Color.Red;
        //                            dp.BackGradientStyle = GradientStyle.LeftRight;
        //                            dp.BackSecondaryColor = Color.DarkRed;
        //                            dp.BorderColor = Color.Black;
        //                            dp.LabelForeColor = Color.Black;
        //                            dp.AxisLabel = "8:00-17:45 (Normal Shift)";  //Job ID
        //                            dp.YValues[0] = double.Parse(dr[1].ToString());
        //                            dp.Label = double.Parse(dr[1].ToString()) > 0 ? (Math.Round(double.Parse(dr[1].ToString()) / normalTime * 100, 2).ToString() + "%") : "";
        //                            dataSeries_JobOutPut.Points.Add(dp);


        //                            DataPoint dp1 = new DataPoint();
        //                            dp1.Color = System.Drawing.Color.Red;
        //                            dp1.BackGradientStyle = GradientStyle.LeftRight;
        //                            dp1.BackSecondaryColor = Color.DarkRed;
        //                            dp1.BorderColor = Color.Black;
        //                            dp1.LabelForeColor = Color.Black;
        //                            dp1.AxisLabel = "8:00-20:00 (Normal Shift with OT)";
        //                            dp1.YValues[0] = double.Parse(dr[2].ToString());
        //                            dp1.Label = double.Parse(dr[2].ToString()) > 0 ? (Math.Round(double.Parse(dr[2].ToString()) / OTTime * 100, 2).ToString() + "%") : "";
        //                            dataSeries_JobOutPut.Points.Add(dp1);



        //                            DataPoint dp2 = new DataPoint();
        //                            dp2.Color = System.Drawing.Color.Red;
        //                            dp2.BackGradientStyle = GradientStyle.LeftRight;
        //                            dp2.BackSecondaryColor = Color.DarkRed;
        //                            dp2.BorderColor = Color.Black;
        //                            dp2.LabelForeColor = Color.Black;
        //                            dp2.AxisLabel = "8:00-7:59 (2 Shift with OT)";
        //                            dp2.YValues[0] = double.Parse(dr[3].ToString());
        //                            dp2.Label = double.Parse(dr[3].ToString()) > 0 ? (Math.Round(double.Parse(dr[3].ToString()) / ShiftOTTime * 100, 2).ToString() + "%") : "";
        //                            dataSeries_JobOutPut.Points.Add(dp2);
        //                        }
        //                        break;
        //                    case "ShiftOT":
        //                        {
        //                            DataPoint dp = new DataPoint();
        //                            dp.Color = System.Drawing.Color.Red;
        //                            dp.BackGradientStyle = GradientStyle.LeftRight;
        //                            dp.Color = System.Drawing.Color.LawnGreen;
        //                            dp.BackSecondaryColor = Color.Green;
        //                            dp.LabelForeColor = Color.Black;

        //                            dp.AxisLabel = "8:00-17:45 (Normal Shift)";  //Job ID
        //                            dp.YValues[0] = double.Parse(dr[1].ToString());
        //                            dp.Label = double.Parse(dr[1].ToString()) > 0 ? (Math.Round(double.Parse(dr[1].ToString()) / normalTime * 100, 2).ToString() + "%") : "";
        //                            dataSeries_JobOutPut.Points.Add(dp);


        //                            DataPoint dp1 = new DataPoint();
        //                            dp1.Color = System.Drawing.Color.Red;
        //                            dp1.BackGradientStyle = GradientStyle.LeftRight;
        //                            dp1.Color = System.Drawing.Color.LawnGreen;
        //                            dp1.BackSecondaryColor = Color.Green;
        //                            dp1.LabelForeColor = Color.Black;

        //                            dp1.AxisLabel = "8:00-20:00 (Normal Shift with OT)";
        //                            dp1.YValues[0] = double.Parse(dr[2].ToString());
        //                            dp1.Label = double.Parse(dr[2].ToString()) > 0 ? (Math.Round(double.Parse(dr[2].ToString()) / OTTime * 100, 2).ToString() + "%") : "";
        //                            dataSeries_JobOutPut.Points.Add(dp1);


        //                            DataPoint dp2 = new DataPoint();
        //                            dp2.Color = System.Drawing.Color.Red;
        //                            dp2.BackGradientStyle = GradientStyle.LeftRight;
        //                            dp2.Color = System.Drawing.Color.LawnGreen;
        //                            dp2.BackSecondaryColor = Color.Green;
        //                            dp2.LabelForeColor = Color.Black;

        //                            dp2.AxisLabel = "8:00-7:59 (2 Shift with OT)";
        //                            dp2.YValues[0] = double.Parse(dr[3].ToString());
        //                            dp2.Label = double.Parse(dr[3].ToString()) > 0 ? (Math.Round(double.Parse(dr[3].ToString()) / ShiftOTTime *100, 2).ToString() + "%") : "";
        //                            dataSeries_JobOutPut.Points.Add(dp2);
        //                        }
        //                        break;

        //                    default:
        //                        {

        //                        }
        //                        break;
        //                }

        //                this.ProdChart.Series.Add(dataSeries_JobOutPut);
        //            }



        //            #region old logic not use
        //            //for (int i = 0; i < 3; i++)
        //            //{


        //            //    bool hasData = false;
        //            //    foreach (DataRow x in dt.Rows)
        //            //    {
        //            //        switch (i)
        //            //        {
        //            //            case (0): //column 5 blue; capacity 
        //            //                {
        //            //                    DataPoint dataPoint = new DataPoint();
        //            //                    dataPoint.AxisLabel = x[2].ToString();  //Job ID
        //            //                    dataPoint.YValues[0] = double.Parse(x[5].ToString());
        //            //                    dataPoint.Color = System.Drawing.Color.SkyBlue;
        //            //                    dataPoint.Label = x[5].ToString();

        //            //                    dataPoint.BackGradientStyle = GradientStyle.LeftRight;
        //            //                    dataPoint.BackSecondaryColor = Color.SteelBlue;
        //            //                    dataPoint.BorderColor = Color.Black;
        //            //                    dataPoint.LabelForeColor = Color.Black;

        //            //                    if (dataPoint.YValues[0] > 0)
        //            //                    {
        //            //                        hasData = hasData || true;
        //            //                    }
        //            //                    dataSeries_JobOutPut.Points.Add(dataPoint);
        //            //                    break;
        //            //                }
        //            //            case (1): // column 6 red , less capacity
        //            //                {
        //            //                    DataPoint dataPoint = new DataPoint();
        //            //                    dataPoint.AxisLabel = x[2].ToString();  //Job ID
        //            //                    dataPoint.YValues[0] = double.Parse(x[6].ToString());
        //            //                    dataPoint.Color = System.Drawing.Color.Red;
        //            //                    if (double.Parse(x[6].ToString()) >0 )
        //            //                    dataPoint.Label = x[7].ToString();

        //            //                    dataPoint.BackGradientStyle = GradientStyle.LeftRight;
        //            //                    dataPoint.BackSecondaryColor = Color.DarkRed;
        //            //                    dataPoint.BorderColor = Color.Black;
        //            //                    dataPoint.LabelForeColor = Color.Black;


        //            //                    if (dataPoint.YValues[0] > 0)
        //            //                    {
        //            //                        hasData = hasData || true;
        //            //                    }
        //            //                    dataSeries_JobOutPut.Points.Add(dataPoint);


        //            //                    break;
        //            //                }
        //            //            case (2):
        //            //                { 
        //            //                    //column 8 green, more capacity
        //            //                    DataPoint dataPoint = new DataPoint();
        //            //                    dataPoint.AxisLabel = x[2].ToString();  //Job ID
        //            //                    dataPoint.YValues[0] = double.Parse(x[8].ToString());
        //            //                    dataPoint.Color = System.Drawing.Color.LawnGreen;
        //            //                    if (double.Parse(x[8].ToString()) > 0)
        //            //                        dataPoint.Label = x[9].ToString();

        //            //                    dataPoint.BackGradientStyle = GradientStyle.LeftRight;
        //            //                    dataPoint.BackSecondaryColor = Color.Green;
        //            //                    dataPoint.BorderColor = Color.Black;
        //            //                    dataPoint.LabelForeColor = Color.Black;


        //            //                    if (dataPoint.YValues[0] > 0)
        //            //                    {
        //            //                        hasData = hasData || true;
        //            //                    }
        //            //                    dataSeries_JobOutPut.Points.Add(dataPoint);
        //            //                    break;
        //            //                }
        //            //        }

        //            //    }
        //            //    if (hasData)
        //            //    {
        //            //        this.ProdChart.Series.Add(dataSeries_JobOutPut);
        //            //    }
        //            //}

        //            #endregion

        //            ProdChart.Titles.Clear();
        //            if (ddlMachineID.Text != "ALL")
        //            {
        //                ProdChart.Titles.Add(" Machine " + ddlMachineID.Text + " Capacity ");
        //            }else
        //            {
        //                ProdChart.Titles.Add(" Machine  Capacity ");
        //            }

        //            ProdChart.Titles[0].ForeColor = Color.Black;
        //            ProdChart.Titles[0].Font = new Font("Microsoft Sans Serif", 16f, FontStyle.Bold);
        //            ProdChart.Titles[0].Alignment = ContentAlignment.TopCenter;
        //        }
        //        else
        //        {
        //            this.ProdChart.Series.Clear();
        //            this.ProdChart.Visible = false;
        //        }
        //    }
        //    catch (Exception ee)
        //    {
        //        DBHelp.Reports.LogFile.Log("MachineCapability_Debug", "ChartDisplay_Job Exception : " + ee.Message);
        //    }
        //    #endregion
        //}




        void List_Refresh(DataTable dt)
        {
            dg_partList.DataSource = dt.DefaultView;
            dg_partList.DataBind();
        }

        //void Chart_Refresh()
        //{
        //    DataTable dt_timerBar = (DataTable)Session["dt_timeBar"];
        //    if (dt_timerBar.Rows.Count > 0)
        //    {
        //        ChartDisplay_SpendingTimeBar(dt_timerBar);
        //    }
        //    else
        //    {
        //        this.chart_Time.Visible = false;
        //    }

        //    DataTable dt_timePie = (DataTable)Session["dt_timePie"];
        //    if (dt_timePie.Rows.Count > 0)
        //    {
        //        ChartDisplay_SpendingTimePie(dt_timePie);
        //    }
        //    else
        //    {
        //        this.chart_Time.Visible = false;
        //    }

        //    DataTable dt = (DataTable)Session["dt_ForProdChart"];
        //    List<double> list = (List<double>)Session["List_TotalTime"];
        //    if (dt == null || dt.Rows.Count == 0)
        //    {
        //        this.ProdChart.Visible = false;
        //    }
        //    else
        //    {
        //        ChartDisplay_Job(dt, list);
        //    }
        //}


   

        #endregion

        protected void btn_Edit_Click(object sender, EventArgs e)
        {
            foreach (DataGridItem item in this.dg_partList.Items)
            {

                item.Visible = true;
                ((TextBox)item.Cells[4].FindControl("txtOrderQty")).Enabled = true;


                string sOrderQty = ((TextBox)item.Cells[5].FindControl("txtOrderQty")).Text.Trim();
                if (sOrderQty != "")
                {
                    string[] sOrderQtyArr = sOrderQty.Split('(');
                    ((TextBox)item.Cells[5].FindControl("txtOrderQty")).Text = sOrderQtyArr[0];
                }
            }

            ((TextBox)this.dg_partList.Items[this.dg_partList.Items.Count - 1].Cells[4].FindControl("txtOrderQty")).Enabled = false;
            ((TextBox)this.dg_partList.Items[this.dg_partList.Items.Count - 1].Cells[4].FindControl("txtOrderQty")).Text = "";
            ((Label)this.dg_partList.Items[this.dg_partList.Items.Count - 1].Cells[5].FindControl("lbSpendHours")).Text = "";

            this.btn_Edit.Visible = false;
        }


        void setYearDDL()
        {
            this.ddlYear.Items.Clear();


            int yearStart = 2017;
            int yearCurrent = DateTime.Now.Year;

            for (int i = yearStart; i < yearCurrent + 1; i++)
            {
                ListItem li = new ListItem();
                li.Text = i.ToString();
                li.Value = i.ToString();
                this.ddlYear.Items.Add(li);
            }

            this.ddlYear.SelectedIndex = this.ddlYear.Items.Count - 1;
        }



        public int GetWorkingDaysOFEachMonth()
        {
            int iWorkingDays = 0;

            int iMonth = DateTime.Now.Month;

            if (iMonth == 1 || iMonth == 3 || iMonth == 5 || iMonth == 7 || iMonth == 8 || iMonth == 10 || iMonth == 12)
            {
                iWorkingDays = 22;
            }
            else if (iMonth == 4 || iMonth == 6 || iMonth == 9 || iMonth == 11)
            {
                iWorkingDays = 21;
            }
            else if(iMonth ==2)
            {
                if (DateTime.IsLeapYear(DateTime.Now.Year))
                {
                    iWorkingDays = 21;
                }
                else
                {
                    iWorkingDays = 20;
                }
            }

            return iWorkingDays;
            
        }


        //void ShowChart_ModelPie(Dictionary<string, int> dicModel, double totalSet)
        //{
        //    try
        //    {
        //        if (dicModel == null || dicModel.Count == 0)
        //        {
        //            this.ChartModelPie.Visible = false;
        //        }else
        //        {
        //            this.ChartModelPie.Visible = true;
        //        }

        //        this.ChartModelPie.Visible = true;
        //        this.ChartModelPie.Series.Clear();
        //        this.ChartModelPie.ChartAreas.Clear();
        //        this.ChartModelPie.ChartAreas.Add("Area1");

        //        #region chart CSS

        //        ChartModelPie.BackColor = Color.FromArgb(245, 245, 250);
        //        ChartModelPie.BackGradientStyle = GradientStyle.None;

        //        //图表区背景
        //        ChartModelPie.ChartAreas[0].BackColor = Color.FromArgb(245, 245, 250);
        //        ChartModelPie.ChartAreas[0].BorderColor = Color.Transparent;
        //        //X轴标签间距
        //        ChartModelPie.ChartAreas[0].AxisX.IntervalAutoMode = IntervalAutoMode.VariableCount;
        //        ChartModelPie.ChartAreas[0].AxisX.LabelStyle.IsStaggered = false;
        //        ChartModelPie.ChartAreas[0].AxisX.LabelStyle.Angle = 0;
        //        ChartModelPie.ChartAreas[0].AxisX.TitleFont = new Font("Microsoft Sans Serif", 8f, FontStyle.Regular);
        //        ChartModelPie.ChartAreas[0].AxisX.TitleForeColor = Color.DarkBlue;
        //        //X坐标轴颜色
        //        ChartModelPie.ChartAreas[0].AxisX.LineColor = Color.DarkBlue;
        //        ChartModelPie.ChartAreas[0].AxisX.LabelStyle.ForeColor = Color.DarkBlue;
        //        ChartModelPie.ChartAreas[0].AxisX.LabelStyle.Font = new Font("Microsoft Sans Serif", 8f, FontStyle.Regular);
        //        ChartModelPie.ChartAreas[0].AxisX.LabelStyle.IsEndLabelVisible = true;
        //        ChartModelPie.ChartAreas[0].AxisX.LabelStyle.Enabled = true;
        //        //Y坐标轴标题
        //        ChartModelPie.ChartAreas[0].AxisX.Title = "Plan";
        //        ChartModelPie.ChartAreas[0].AxisX.TitleFont = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular);
        //        ChartModelPie.ChartAreas[0].AxisX.TitleForeColor = Color.DarkBlue;
        //        //chart_Time.ChartAreas[0].AxisY.TextOrientation = TextOrientation.Rotated270;
        //        //chart_Time.ChartAreas[0].AxisX.ToolTip = "axisX-tooltip";

        //        //X轴网络线条
        //        ChartModelPie.ChartAreas[0].AxisX.MajorGrid.Enabled = true;
        //        ChartModelPie.ChartAreas[0].AxisX.MajorGrid.LineColor = System.Drawing.Color.Gainsboro;

        //        //Y坐标轴颜色
        //        ChartModelPie.ChartAreas[0].AxisY.LineColor = System.Drawing.Color.Gainsboro;
        //        ChartModelPie.ChartAreas[0].AxisY.LabelStyle.ForeColor = Color.DarkBlue;
        //        ChartModelPie.ChartAreas[0].AxisY.LabelStyle.Font = new Font("Microsoft Sans Serif", 8f, FontStyle.Regular);
        //        //Y坐标轴标题
        //        //chart_Time.ChartAreas[0].AxisY.Title = "Spend Days";
        //        //chart_Time.ChartAreas[0].AxisY.TitleFont = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular);
        //        //chart_Time.ChartAreas[0].AxisY.TitleForeColor = Color.DarkBlue;
        //        //chart_Time.ChartAreas[0].AxisY.TextOrientation = TextOrientation.Rotated270;
        //        //chart_Time.ChartAreas[0].AxisY.ToolTip = "axisY-tooltip";
        //        //Y轴网格线条
        //        ChartModelPie.ChartAreas[0].AxisY.MajorGrid.Enabled = false;
        //        ChartModelPie.ChartAreas[0].AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Dash;
        //        ChartModelPie.ChartAreas[0].AxisY.MajorGrid.LineWidth = 1;
        //        ChartModelPie.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.Gainsboro;
        //        ChartModelPie.ChartAreas[0].AxisY.Enabled = AxisEnabled.False;

        //        ChartModelPie.ChartAreas[0].BackGradientStyle = GradientStyle.TopBottom;

        //        #endregion
                

        //        #region Series
        //        System.Web.UI.DataVisualization.Charting.Series series = new Series();
        //        series.ChartType = SeriesChartType.Pie;//饼图
        //        series.ChartArea = this.ChartModelPie.ChartAreas[0].Name;
        //        series["PieLabelStyle"] = "Outside";//饼图说明显示方式（外面）
        //        series.Name = "Series2";
        //        series.YAxisType = AxisType.Primary;
        //        series.XValueType = ChartValueType.Int32;
        //        series.YAxisType = AxisType.Primary;
        //        series.YValueType = ChartValueType.Int32;
        //        series.IsVisibleInLegend = true;
        //        series.LabelForeColor = Color.SteelBlue;
        //        series.ToolTip = "#VAL" + " SET";
        //        series.Color = Color.Lime;
        //        //series.LegendText = ""
        //        series.Palette = ChartColorPalette.Bright;
        //        #endregion
                

        //        foreach (KeyValuePair<string ,int> kv in dicModel)
        //        {
        //            string sModel = kv.Key;
        //            int iQty = kv.Value;


        //            DataPoint dataPoint = new DataPoint();
        //            dataPoint.Color = System.Drawing.Color.SkyBlue;
        //            dataPoint.BackSecondaryColor = Color.SteelBlue;
        //            dataPoint.BackGradientStyle = GradientStyle.Center;
        //            dataPoint.BorderColor = Color.Black;
        //            dataPoint.LabelForeColor = Color.Black;

        //            dataPoint.Label = sModel + "-" + Math.Round(iQty / totalSet * 100, 2).ToString("0.00") + "%";
        //            dataPoint.AxisLabel = sModel;
        //            dataPoint.YValues[0] = iQty;

        //            series.Points.Add(dataPoint);
        //        }


        //        this.ChartModelPie.Series.Add(series);


        //        ChartModelPie.Titles.Clear();
        //        ChartModelPie.Titles.Add(" Model Percentage ");
        //        ChartModelPie.Titles[0].ForeColor = Color.Black;
        //        ChartModelPie.Titles[0].Font = new Font("Microsoft Sans Serif", 16f, FontStyle.Bold);
        //        ChartModelPie.Titles[0].Alignment = ContentAlignment.TopCenter;

        //    }
        //    catch (Exception ee)
        //    {
        //        DBHelp.Reports.LogFile.Log("MachineCapability", "ShowChart_ModelPie Exception:" + ee.ToString());
        //    }
        //}

    }
}