using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace DashboardTTS.Webform.Molding
{
    public partial class MachineRealStatus : System.Web.UI.Page
    {
        
        private const string _urlImg = "~/Resources/Images/MouldingMachine.png";
        private readonly Common.Class.BLL.MouldingMachineStatus_BLL _statusBLL = new Common.Class.BLL.MouldingMachineStatus_BLL();
        private readonly Common.Class.BLL.MouldingViHistory_BLL _trackingBLL = new Common.Class.BLL.MouldingViHistory_BLL();


        private UserControl.UcMoulding GetControl(int sMachineID)
        {
            UserControl.UcMoulding control = new UserControl.UcMoulding();
            switch (sMachineID)
            {
                case 1:
                    control = uc1;
                    break;
                case 2:
                    control = uc2;
                    break;
                case 3:
                    control = uc3;
                    break;
                case 4:
                    control = uc4;
                    break;
                case 5:
                    control = uc5;
                    break;
                case 6:
                    control = uc6;
                    break;
                case 7:
                    control = uc7;
                    break;
                case 8:
                    control = uc8;
                    break;
                case 9:
                    control = uc9;
                    break;
                default:
                    throw new NullReferenceException("no such MachineID " + sMachineID);                 
            }
            return control;
        }


        private Dictionary<int, string> GetUsedRate()
        {
            DataTable dtStatus = new DataTable();
            dtStatus.Columns.Add("MachineID");
            dtStatus.Columns.Add("Running");
            dtStatus.Columns.Add("Adjustment");
            dtStatus.Columns.Add("NoWIP");
            dtStatus.Columns.Add("Mould_Testing");
            dtStatus.Columns.Add("Material_Testing");
            dtStatus.Columns.Add("Change_Model");
            dtStatus.Columns.Add("No_Operator");
            dtStatus.Columns.Add("No_Material");
            dtStatus.Columns.Add("Break_Time");
            dtStatus.Columns.Add("ShutDown");
            dtStatus.Columns.Add("Damage_Mould");
            dtStatus.Columns.Add("Machine_Break");

            Dictionary<DateTime, StaticRes.Global.StatusType> Points;
            #region set points
            for (int i = 1; i < 10; i++)
            {
                DataRow dr = dtStatus.NewRow();

                Points = new Dictionary<DateTime, StaticRes.Global.StatusType>();
                Points = _statusBLL.getOEE(DateTime.Now.Date.AddHours(8), DateTime.Now.Date.AddHours(8), i.ToString(), "", "", false);//2018 12 04  by wei lijia , add date not in & except weekend


                if (Points == null || Points.Count == 0)
                {
                    #region default 0
                    dr["MachineID"] = "Machine" + i.ToString();
                    dr["Running"] = 0;
                    dr["Adjustment"] = 0;
                    dr["NoWIP"] = 0;
                    dr["Mould_Testing"] = 0;
                    dr["Material_Testing"] = 0;
                    dr["Change_Model"] = 0;
                    dr["No_Operator"] = 0;
                    dr["No_Material"] = 0;
                    dr["Break_Time"] = 0;
                    dr["ShutDown"] = 0;
                    dr["Damage_Mould"] = 0;
                    dr["Machine_Break"] = 0;
                    #endregion
                }
                else
                {
                    double Running_Count = 0;
                    double Adjustment_Count = 0;
                    double NoWIP_Count = 0;
                    double Mould_Testing_Count = 0;
                    double Material_Testing_Count = 0;
                    double Change_Model_Count = 0;
                    double No_Operator_Count = 0;
                    double No_Material_Count = 0;
                    double Break_Time_Count = 0;
                    double ShutDown_Count = 0;
                    double Damage_Mould_Count = 0;
                    double Machine_Break_Count = 0;

                    #region  catogery the points
                    foreach (KeyValuePair<DateTime, StaticRes.Global.StatusType> pPoint in Points)
                    {
                        try
                        {
                            switch (pPoint.Value)
                            {
                                case StaticRes.Global.StatusType.Running:
                                    {
                                        Running_Count++;
                                        break;
                                    }
                                case StaticRes.Global.StatusType.Adjustment:
                                    {
                                        Adjustment_Count++;
                                        break;
                                    }
                                case StaticRes.Global.StatusType.No_Schedule:
                                    {
                                        NoWIP_Count++;
                                        break;
                                    }
                                case StaticRes.Global.StatusType.Mould_Testing:
                                    {
                                        Mould_Testing_Count++;
                                        break;
                                    }
                                case StaticRes.Global.StatusType.Material_Testing:
                                    {
                                        Material_Testing_Count++;
                                        break;
                                    }
                                case StaticRes.Global.StatusType.Change_Model:
                                    {
                                        Change_Model_Count++;
                                        break;
                                    }
                                case StaticRes.Global.StatusType.No_Operator:
                                    {
                                        No_Operator_Count++;
                                        break;
                                    }
                                case StaticRes.Global.StatusType.No_Material:
                                    {
                                        No_Material_Count++;
                                        break;
                                    }
                                case StaticRes.Global.StatusType.Break_Time:
                                    {
                                        Break_Time_Count++;
                                        break;
                                    }
                                case StaticRes.Global.StatusType.DamageMould:
                                    {
                                        Damage_Mould_Count++;
                                        break;
                                    }
                                case StaticRes.Global.StatusType.ShutDown:
                                    {
                                        ShutDown_Count++;
                                        break;
                                    }
                                case StaticRes.Global.StatusType.MachineBreak:
                                    {
                                        Machine_Break_Count++;
                                        break;
                                    }
                                default:
                                    {
                                        Running_Count++;
                                        break;
                                    }
                            }
                        }
                        catch (Exception ee)
                        {

                        }
                    }
                    #endregion

                    dr["MachineID"] = "Machine" + i.ToString();
                    dr["Running"] = Math.Round(Running_Count / 60, 2);
                    dr["Adjustment"] = Math.Round(Adjustment_Count / 60, 2);
                    dr["NoWIP"] = Math.Round(NoWIP_Count / 60, 2);
                    dr["Mould_Testing"] = Math.Round(Mould_Testing_Count / 60, 2);
                    dr["Material_Testing"] = Math.Round(Material_Testing_Count / 60, 2);
                    dr["Change_Model"] = Math.Round(Change_Model_Count / 60, 2);
                    dr["No_Operator"] = Math.Round(No_Operator_Count / 60, 2);
                    dr["No_Material"] = Math.Round(No_Material_Count / 60, 2);
                    dr["Break_Time"] = Math.Round(Break_Time_Count / 60, 2);
                    dr["ShutDown"] = Math.Round(ShutDown_Count / 60, 2);
                    dr["Damage_Mould"] = Math.Round(Damage_Mould_Count / 60, 2);
                    dr["Machine_Break"] = Math.Round(Machine_Break_Count / 60, 2);
                }

                dtStatus.Rows.Add(dr);
            }
            #endregion




            TimeSpan ts = DateTime.Now - DateTime.Now.AddHours(-8).Date.AddHours(8);
            double totalHours = ts.TotalSeconds / 3600;



            Dictionary<int, string> dicUsedRate = new Dictionary<int, string>();


            foreach (DataRow dr in dtStatus.Rows)
            {
                string machineID = dr["MachineID"].ToString().Replace("Machine", "").Trim();
                double run = double.Parse(dr["Running"].ToString());
                double adjustment = double.Parse(dr["Adjustment"].ToString());
                double mouldTest = double.Parse(dr["Mould_Testing"].ToString());
                double materialTest = double.Parse(dr["Material_Testing"].ToString());
                double changeModel = double.Parse(dr["Change_Model"].ToString());



                double totalRun = run + adjustment + mouldTest + materialTest + changeModel;

                double usedRate = Math.Round(totalRun / totalHours * 100, 2);


                dicUsedRate.Add(int.Parse(machineID), usedRate.ToString("0.00") + "%");
            }

            return dicUsedRate;
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Dictionary<int, string> dicStatus = _statusBLL.GetCurrentStatus();
                Dictionary<int, string> dicUsedRate = GetUsedRate();
                DataTable dtProd = _trackingBLL.GetList(DateTime.Now.AddDays(-8).Date, DateTime.Now.AddDays(-8).Date.AddDays(1), "","","");

                
                for (int i = 1; i < 10; i++)
                {
                    UserControl.UcMoulding.UIModel model = new UserControl.UcMoulding.UIModel();
                    model.MachineID = "Machine " + i.ToString();
                    model.Status = dicStatus[i].ToUpper();
                    model.ImgURL = _urlImg;
                    model.UsedRate = dicUsedRate[i];
                    
                    DataRow[] arrDrTemp = dtProd.Select(" MachineID = '" + i.ToString() + "'");
                    if (arrDrTemp!= null && arrDrTemp.Count() != 0)
                    {
                        model.PartNo = arrDrTemp[0]["partNumber"].ToString();
                        model.Model = arrDrTemp[0]["model"].ToString();
                        model.JigNo = arrDrTemp[0]["jigNo"].ToString();
                        model.TotalQty = decimal.Parse(arrDrTemp[0]["acountReading"].ToString());
                        model.OKQty = decimal.Parse(arrDrTemp[0]["acceptQty"].ToString());
                        model.NGQty = decimal.Parse(arrDrTemp[0]["rejectQty"].ToString());
                        model.RejRate = Math.Round(model.NGQty / model.TotalQty * 100, 2).ToString("0.00") + "%";
                    }
                    else
                    {
                        model.PartNo = "";
                        model.Model = "";
                        model.JigNo = "";
                        model.TotalQty = 0;
                        model.OKQty = 0;
                        model.NGQty = 0;
                        model.RejRate = "0.00%";
                    }

                 

                    GetControl(i).SetUI(model);
                }
            }
            catch (Exception ee)
            {
                DBHelp.Reports.LogFile.Log("MouldingMachineRealStatus", "Page_Load Exception:" + ee.ToString());
            }
        }
    }
}