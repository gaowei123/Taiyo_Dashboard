using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Globalization;

namespace DashboardTTS.Webform
{
    public partial class Login : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.txtUserName.Text = "";
                this.txtPassword.Text = "";
                this.txtUserName.Focus();
            }
        }

        protected void btn_Login_Click(object sender, EventArgs e)
        {
            try
            {
                string Username = this.txtUserName.Text.Trim();
                string Password = this.txtPassword.Text.Trim();

                #region check textbox
                if (Username == "")
                {
                    this.txtUserName.Text = "";
                    this.txtUserName.Focus();
                    ClientScript.RegisterStartupScript(Page.GetType(), "", "alert('Please input Username');", true);
                    return;
                }

                if (Password == "")
                {
                    this.txtPassword.Text = "";
                    this.txtPassword.Focus();
                    ClientScript.RegisterStartupScript(Page.GetType(), "", "alert('Please input Password');", true);
                    return;
                }
                #endregion
                

                Common.Class.BLL.User_DB_BLL UserBll = new Common.Class.BLL.User_DB_BLL();
                string Department = Request.QueryString["Department"] == null ? "" : Request.QueryString["Department"].ToString();
                string errorStr = "";
                string strUrl = "";



                #region Moulding
                if (Department.ToUpper() == StaticRes.Global.Department.Moulding.ToUpper())
                {
                   
                    string Type = Request.QueryString["commandType"] == null ? "" : Request.QueryString["commandType"].ToString();

                    #region Get user group
                    string Group = "";
                    if (Type == "Moulding_AddBom" || Type == "Moulding_UpdateBom" ||
                        Type == "Moulding_DeleteBom" || Type == "UpdateMachineInfo" ||
                        Type == "AddUser" || Type == "UpdateUser" || Type == "DeleteUser" ||
                        Type == "Moulding_AddMaterialBom" || Type == "Moulding_UpdateMaterialBom" ||
                        Type == "Moulding_DeleteMaterialBom")
                    {
                        Group = StaticRes.Global.UserGroup.ADMIN;
                    }
                    else if (Type == "Maintain" || Type== "MaintainVerify" || Type == "CleanMould" || Type=="ChangeMould" ||
                             Type == "AddCheckItem" || Type == "UpdateCheckItem" || Type== "TransferMould" || Type== "Moulding_AddMouldingPartsMovement"
                             || Type == "Moulding_DeleteMouldingPartsMovement" || Type == "UpdateProduction" || Type == "DeleteProduction")
                    {
                        Group = StaticRes.Global.UserGroup.ENGINEER;
                    }
                    else if (Type == "AddMaterialInventory" || Type == "UpdateMaterialInventory" ||
                             Type == "ReturnMaterialInventory" || Type == "DeleteMaterial" )
                    {
                        Group = StaticRes.Global.UserGroup.MH;
                    }
                    else if (Type == "SubmitAttendance" || Type == "CheckDailyReport")
                    {
                        Group = StaticRes.Global.UserGroup.SUPERVISOR;
                    }
                    else if (Type == "UpdateSetUp" )
                    {
                        Group = StaticRes.Global.UserGroup.OPERATOR;
                    }
                    #endregion


                    bool result = UserBll.Login(Username, Password, out errorStr, Department, Group);
                    if (result)
                    {
                        //Bom
                        #region  add moulding  bom
                        if (Type == "Moulding_AddBom")
                        {
                           
                            Common.Class.Model.MouldingBom_Model model = (Common.Class.Model.MouldingBom_Model)Session["MouldingBom_Model"];
                            model.userName = Username;



                            Common.Class.BLL.MouldingBom_BLL bll = new Common.Class.BLL.MouldingBom_BLL();
                            if (bll.Add(model))
                            {
                                strUrl = "../Moulding/BomList.aspx?Result=TRUE";
                            }
                            else
                            {
                                strUrl = "../Moulding/BomDetail.aspx?Result=FALSE";
                            }


                      


                        }
                        #endregion

                        #region Update moulding bom
                        else if (Type == "Moulding_UpdateBom")
                        {
                          
                            Common.Class.Model.MouldingBom_Model model = (Common.Class.Model.MouldingBom_Model)Session["MouldingBom_Model"];
                            model.userName = Username;


                            Common.Class.BLL.MouldingBom_BLL bll = new Common.Class.BLL.MouldingBom_BLL();
                            if (model.refField05.Length > 0)
                            {
                                bool Result = bll.DeleteByPartAll(model.partNumberAll);
                                model.partNumberAll = model.refField05;
                                model.refField05 = "";
                                if (bll.Add(model))
                                {
                                    strUrl = "../Moulding/BomList.aspx?Result=TRUE";
                                }
                                else
                                {
                                    strUrl = "../Moulding/BomDetail.aspx?Result=FALSE";
                                }

                            }
                            else
                            {
                                if (bll.Update(model))
                                {
                                    strUrl = "../Moulding/BomList.aspx?Result=TRUE";
                                }
                                else
                                {
                                    strUrl = "../Moulding/BomDetail.aspx?Result=FALSE";
                                }
                            }
                        }
                        #endregion

                        #region Delete moulding bom
                        else if (Type == "Moulding_DeleteBom")
                        {
                          
                            string PartNumberAll = Request.QueryString["PartNumberAll"] == null ? "" : Request.QueryString["PartNumberAll"].ToString();
                            Common.Class.BLL.MouldingBom_BLL bll = new Common.Class.BLL.MouldingBom_BLL();

                            bool Result = bll.DeleteByPartAll(PartNumberAll);


                            if (Result)
                            {
                                strUrl = "../Moulding/BomList.aspx?Result=TRUE";
                            }
                            else
                            {
                                strUrl = "../Moulding/BomList.aspx?Result=FALSE";
                            }


                            #region user event log
                            Common.Class.BLL.LMMSUserEventLog_BLL userEventLogBLL = new Common.Class.BLL.LMMSUserEventLog_BLL();
                            Common.Class.Model.LMMSUserEventLog userEventModel = new Common.Class.Model.LMMSUserEventLog();
                            userEventModel.pageName = "Moulding BomList";
                            userEventModel.action = "Delete Part:" + PartNumberAll;
                            userEventModel.startTime = DateTime.Now;
                            userEventModel.endTime = DateTime.Now;
                            userEventModel.dateTime = DateTime.Now;
                            userEventModel.eventType = "delete";
                            userEventModel.userID = Username;

                            List<Common.Class.Model.LMMSUserEventLog> listModel = new List<Common.Class.Model.LMMSUserEventLog>();
                            listModel.Add(userEventModel);
                            userEventLogBLL.AddRollBack(listModel);
                            #endregion

                        }
                        #endregion

                        #region  add moulding material bom
                        else if (Type == "Moulding_AddMaterialBom")
                        {

                            Common.Model.Material_Inventory_Bom model = (Common.Model.Material_Inventory_Bom)Session["MouldingMaterialBom_Model"];
                            model.Updated_User = Username;



                            Common.Class.BLL.Material_Inventory_Bom_BLL bll = new Common.Class.BLL.Material_Inventory_Bom_BLL();
                            if (bll.Add(model))
                            {
                                strUrl = "../Moulding/MaterialInventoryBom.aspx?Result=TRUE";
                            }
                            else
                            {
                                strUrl = "../Moulding/MaterialInventoryBom.aspx?Result=FALSE";
                            }

                        }
                        #endregion

                        #region Update moulding material bom
                        else if (Type == "Moulding_UpdateMaterialBom")
                        {

                            Common.Model.Material_Inventory_Bom model = (Common.Model.Material_Inventory_Bom)Session["MouldingMaterialBom_Model"];
                            model.Updated_User = Username;



                            Common.Class.BLL.Material_Inventory_Bom_BLL bll = new Common.Class.BLL.Material_Inventory_Bom_BLL();
                            if (bll.Update(model))
                            {
                                strUrl = "../Moulding/MaterialInventoryBom.aspx?Result=TRUE";
                            }
                            else
                            {
                                strUrl = "../Moulding/MaterialInventoryBom.aspx?Result=FALSE";
                            }

                        }
                        #endregion

                        #region Delete moulding material bom
                        else if (Type == "Moulding_DeleteMaterialBom")
                        {
                            string MaterialPart = Request.QueryString["Material_Part"] == null ? "" : Request.QueryString["Material_Part"].ToString();
                            Common.Class.BLL.Material_Inventory_Bom_BLL bll = new Common.Class.BLL.Material_Inventory_Bom_BLL();

                            bool Result = bll.Delete(MaterialPart);


                            if (Result)
                            {
                                strUrl = "../Moulding/MaterialInventoryBom.aspx?Result=TRUE";
                            }
                            else
                            {
                                strUrl = "../Moulding/MaterialInventoryBom.aspx?Result=FALSE";
                            }


                        }
                        #endregion
                        //Bom


                        #region UpdateMachineInfo
                        else if (Type == "UpdateMachineInfo")
                        {
                            bool Result;

                            try
                            {
                                var model = (Common.Class.Model.MouldingMachineInformation_Model)Session["MouldingMachineInformation"];
                                var bll = new Common.Class.BLL.MouldingMachineInformation_BLL();
                                Result = bll.Update(model);
                            }
                            catch (Exception ex)
                            {
                                DBHelp.Reports.LogFile.Log("", "Catch exception: " + ex.ToString());
                                Result = false;
                            }

                            strUrl = Result ? "../Moulding/MachineInformation.aspx?Result=TRUE": "../Moulding/MachineInformation.aspx?Result=FALSE";
                        }
                        #endregion

                        #region Maintain  not use
                        else if (Type == "Maintain")
                        {
                            List<Common.Class.Model.MouldingMaintain_His_Model> List_Model = new List<Common.Class.Model.MouldingMaintain_His_Model>();

                            List_Model = (List<Common.Class.Model.MouldingMaintain_His_Model>)Session["List_MouldingMaintain_His_Model"];
                            if (List_Model == null)
                            {
                                ClientScript.RegisterStartupScript(Page.GetType(), "", "alert('Operation Timeout !');", true);
                                return;
                            }

                            foreach (Common.Class.Model.MouldingMaintain_His_Model model in List_Model)
                            {
                                model.VerifyBy = Username;
                                model.CheckDate = DateTime.Now;
                            }
                           


                            Common.Class.BLL.MouldingMaintain_His_BLL bll = new Common.Class.BLL.MouldingMaintain_His_BLL();
                            if (bll.Add(List_Model))
                            {
                                strUrl = "../Moulding/MouldingMaintain.aspx?Result=TRUE";
                            }
                            else
                            {
                                strUrl = "../Moulding/MouldingMaintain.aspx?Result=FALSE";
                            }
                            
                        }
                        #endregion 

                        #region MaintainVerify
                        else if (Type == "MaintainVerify")
                        {
                            Session["VerifyBy"] = Username;
                            strUrl = "../Moulding/MouldingMaintain_Verify.aspx";
                        }
                        #endregion 


                        #region Add Check Item
                        else if (Type == "AddCheckItem")
                        {

                            Common.Class.Model.MouldingMaintenanceInspection_Model model = (Common.Class.Model.MouldingMaintenanceInspection_Model)Session["MouldingMaintenanceInspection_Model"];
                            if (model == null)
                            {
                                ClientScript.RegisterStartupScript(Page.GetType(), "", "alert('Operation Timeout !');", true);
                                return;
                            }

                            Common.Class.BLL.MouldingMaintainenceInspection_BLL bll = new Common.Class.BLL.MouldingMaintainenceInspection_BLL();


                            if (bll.Add(model))
                            {
                                strUrl = "../Moulding/MouldingMaintainDetail.aspx?Result=TRUE";
                            }
                            else
                            {
                                strUrl = "../Moulding/MouldingMaintainDetail.aspx?Result=FALSE";
                            }
                        }
                        #endregion

                        #region Update Check Item
                        else if (Type == "UpdateCheckItem")
                        {
                            Common.Class.Model.MouldingMaintenanceInspection_Model model = (Common.Class.Model.MouldingMaintenanceInspection_Model)Session["MouldingMaintenanceInspection_Model"];
                            if (model == null)
                            {
                                ClientScript.RegisterStartupScript(Page.GetType(), "", "alert('Operation Timeout !');", true);
                                return;
                            }
                            model.UpdateTime = DateTime.Now;


                            Common.Class.BLL.MouldingMaintainenceInspection_BLL bll = new Common.Class.BLL.MouldingMaintainenceInspection_BLL();


                            if (bll.Update(model))
                            {
                                strUrl = "../Moulding/MouldingMaintainDetail.aspx?Result=TRUE";
                            }
                            else
                            {
                                strUrl = "../Moulding/MouldingMaintainDetail.aspx?Result=FALSE";
                            }

                        }
                        #endregion
                        

                        #region Delete moulding material
                        else if (Type == "DeleteMaterial")
                        {
                            Common.Class.Model.Material_Inventory model = (Common.Class.Model.Material_Inventory)Session["Material_Inventory_Model"];

                            if (model == null)
                            {
                                ClientScript.RegisterStartupScript(Page.GetType(), "", "alert('Operating timeout!');  window.location.href = \"../Moulding/MouldingPartsInventory.aspx\";", true);
                                return;
                            }
                            model.User_Name = Username;
                            model.Updated_Time = DateTime.Now;


                            Common.Class.BLL.Material_Inventory_BLL bll = new Common.Class.BLL.Material_Inventory_BLL();



                            bool Result = bll.DeleteTransaction(model);
                            if (Result)
                            {
                                strUrl = "../Moulding/MouldingPartsInventory.aspx?Result=TRUE";
                            }
                            else
                            {
                                strUrl = "../Moulding/MouldingPartsInventory.aspx?Result=FALSE";
                            }
                        }
                        #endregion

                        #region  add moulding  material
                        else if (Type == "AddMaterialInventory")
                        {
                           
                            List<Common.Class.Model.Material_Inventory> ModelList = (List<Common.Class.Model.Material_Inventory>)Session["Material_Model_List_Load"];

                            if (ModelList == null)
                            {
                                ClientScript.RegisterStartupScript(Page.GetType(), "", "alert('Operating timeout!');  window.location.href = \"../Moulding/MouldingPartsInventory.aspx\";", true);
                                return;
                            }


                            Common.Class.BLL.Material_Inventory_BLL bll = new Common.Class.BLL.Material_Inventory_BLL();

                            

                            if (bll.AddTransaction(ModelList, Username))
                            {
                                strUrl = "../Moulding/MouldingPartsInventory.aspx?Result=TRUE";
                            }
                            else
                            {
                                strUrl = "../Moulding/MouldingPartsInventory.aspx?Result=FALSE";
                            }
                            

                            #region not use 
                            //Common.Class.Model.Material_Inventory model = (Common.Class.Model.Material_Inventory)Session["Material_Inventory"];
                            //model.User_Name = this.txt_User.Text.Trim();

                            //Common.Class.BLL.Material_Inventory_BLL bll = new Common.Class.BLL.Material_Inventory_BLL();
                            //Common.Class.BLL.Material_Inventory_History_BLL _bll = new Common.Class.BLL.Material_Inventory_History_BLL();
                            


                            //if (bll.Add(model))
                            //{
                            //    if (_bll.Add(_bll.copyobj(model)))
                            //    {
                            //        strUrl = "../Moulding/MouldingPartsInventory.aspx?Result=TRUE";
                            //    }
                            //    else
                            //    {
                            //        strUrl = "../Moulding/MouldingPartsInventory.aspx?Result=FALSE";
                            //    }
                            //}
                            //else
                            //{
                            //    strUrl = "../Moulding/MouldingPartsInventory.aspx?Result=FALSE";
                            //}
                            #endregion
                        }
                        #endregion

                        #region  update moulding  material
                        else if (Type == "UpdateMaterialInventory")
                        {

                            List< Common.Class.Model.Material_Inventory> modelList = (List<Common.Class.Model.Material_Inventory>)Session["Material_Model_List_Unload"];

                            if (modelList == null)
                            {
                                ClientScript.RegisterStartupScript(Page.GetType(), "", "alert('Operating timeout!');  window.location.href = \"../Moulding/MouldingPartsInventory.aspx\";", true);
                                return;
                            }
                            

                            Common.Class.BLL.Material_Inventory_BLL bll = new Common.Class.BLL.Material_Inventory_BLL();

                            if (bll.UnloadTransaction(modelList,Username))
                            {
                                strUrl = "../Moulding/MouldingPartsInventory.aspx?Result=TRUE";
                            }
                            else
                            {
                                strUrl = "../Moulding/MouldingPartsInventory.aspx?Result=FALSE";
                            }
                            

                            #region no use
                            //Common.Class.BLL.Material_Inventory_History_BLL _bll = new Common.Class.BLL.Material_Inventory_History_BLL();

                            //if (model.Weight == decimal.Parse(model.Remarks))
                            //{
                            //    if (bll.DeleteByMaterialPartWeightLoadTime(model.Material_Part, model.Weight.ToString(), DateTime.Parse(model.Load_Time.ToString())))
                            //    {
                            //        if (_bll.Add(_bll.copyobj(model)))
                            //        {

                            //        }
                            //        else
                            //        {
                            //            strUrl = "../Moulding/MouldingPartsInventory.aspx?Result=FALSE";
                            //        }
                            //    }
                            //    else
                            //    {

                            //    }
                            //}
                            //else if (model.Weight < decimal.Parse(model.Remarks))
                            //{
                            //    decimal? transaction_weight = model.Weight;
                            //    model.Weight = decimal.Parse(model.Remarks) - model.Weight;
                            //    if (bll.Update(model))
                            //    {
                            //        model.Weight = transaction_weight;
                            //        if (_bll.Add(_bll.copyobj(model)))
                            //        {
                            //            strUrl = "../Moulding/MouldingPartsInventory.aspx?Result=TRUE";
                            //        }
                            //        else
                            //        {
                            //            strUrl = "../Moulding/MouldingPartsInventory.aspx?Result=FALSE";
                            //        }
                            //    }
                            //    else
                            //    {
                            //        strUrl = "../Moulding/MouldingPartsInventory.aspx?Result=FALSE";
                            //    }
                            //}
                            //else
                            //{
                            //    strUrl = "../Moulding/MouldingPartsInventory.aspx?Result=FALSE";
                            //}
                            #endregion

                        }
                        #endregion

                        #region  return moulding  material
                        else if (Type == "ReturnMaterialInventory")
                        {
                            List<Common.Class.Model.Material_Inventory> ModelList = (List<Common.Class.Model.Material_Inventory>)Session["Material_Model_List"];

                            if (ModelList == null)
                            {
                                ClientScript.RegisterStartupScript(Page.GetType(), "", "alert('Operating timeout!');  window.location.href = \"../Moulding/MouldingPartsInventory.aspx\";", true);
                                return;
                            }


                            Common.Class.BLL.Material_Inventory_BLL bll = new Common.Class.BLL.Material_Inventory_BLL();


                            if (bll.AddTransaction(ModelList, Username))
                            {
                                strUrl = "../Moulding/MouldingPartsInventory.aspx?Result=TRUE";
                            }
                            else
                            {
                                strUrl = "../Moulding/MouldingPartsInventory.aspx?Result=FALSE";
                            }


                            #region not use
                            //Common.Class.Model.Material_Inventory model = (Common.Class.Model.Material_Inventory)Session["Material_Inventory"];
                            //model.User_Name = this.txt_User.Text.Trim();



                            //Common.Class.BLL.Material_Inventory_BLL bll = new Common.Class.BLL.Material_Inventory_BLL();

                            //Common.Class.BLL.Material_Inventory_History_BLL _bll = new Common.Class.BLL.Material_Inventory_History_BLL();
                            //if (bll.Unload(model))
                            //{
                            //    if (_bll.Add(_bll.copyobj(model)))
                            //    {
                            //        strUrl = "../Moulding/MouldingPartsInventory.aspx?Result=TRUE";
                            //    }
                            //    else
                            //    {
                            //        strUrl = "../Moulding/MouldingPartsInventory.aspx?Result=FALSE";
                            //    }
                            //}
                            //else
                            //{
                            //    strUrl = "../Moulding/MouldingPartsInventory.aspx?Result=FALSE";
                            //}
                            #endregion 
                        }
                        #endregion

                        #region  CleanMode
                        else if (Type == "CleanMould")
                        {
                            bool Result = false;

                            Common.Class.Model.MouldingMoldLife_Model model = (Common.Class.Model.MouldingMoldLife_Model)Session["MouldingMoldLife_CLean_Model"];
                              
                            if (model == null )
                            {
                                ClientScript.RegisterStartupScript(Page.GetType(), "", "alert('Operating Timeout !');", true);
                                return;
                            }
                            DateTime dTime = DateTime.Now;
                            #region clean by 
                            model.Clean1TimeBy = Username;
                            model.Clean1Time = dTime;
                            model.CreateTime = dTime;
                            model.UpdatedTime = dTime;
                            #endregion


                            Common.Class.BLL.MouldingMoldLife_BLL bll = new Common.Class.BLL.MouldingMoldLife_BLL();
                            Result = bll.UpdateClean_wHis(model);

                            if (Result)
                            {
                                strUrl = "../Moulding/MoldLife.aspx?Result=TRUE";
                            }
                            else
                            {
                                strUrl = "../Moulding/MoldLife.aspx?Result=FALSE";
                            }
                        }
                        #endregion

                        #region TransferMould  no use
                        else if (Type == "TransferMould")
                        {
                            bool Result = false;

                            
                            Common.Class.Model.MouldingMoldLife_Model model_new = (Common.Class.Model.MouldingMoldLife_Model)Session["MouldingMoldLife_BLL_New"];
                            Common.Class.Model.MouldingMoldLife_Model model_His = (Common.Class.Model.MouldingMoldLife_Model)Session["MouldingMoldLife_BLL_His"];
                            Common.Class.Model.MouldingMoldLife_Model model_transfer = (Common.Class.Model.MouldingMoldLife_Model)Session["MouldingMoldLife_BLL_Transfer"];



                            if (model_new == null || model_His == null || model_transfer == null)
                            {
                                ClientScript.RegisterStartupScript(Page.GetType(), "", "alert('Operating Timeout !');", true);
                                return;
                            }

                            model_His.ChangeBy = Username;
                            model_His.ChangeTime = DateTime.Now;


                            Common.Class.BLL.MouldingMoldLife_BLL bll = new Common.Class.BLL.MouldingMoldLife_BLL();
                            Result = bll.UpdateTransfer(model_new,model_His,model_transfer);

                            if (Result)
                            {
                                strUrl = "../Moulding/MoldLife.aspx?Result=TRUE";
                            }
                            else
                            {
                                strUrl = "../Moulding/MoldLife.aspx?Result=FALSE";
                            }
                        }
                        #endregion

                        #region  ChangeMode
                        else if (Type=="ChangeMould")
                        {
                            Common.Class.Model.MouldingMoldLife_Model model_new = (Common.Class.Model.MouldingMoldLife_Model)Session["MouldingMoldLife_BLL_New"];
                            Common.Class.Model.MouldingMoldLife_Model model_old = (Common.Class.Model.MouldingMoldLife_Model)Session["MouldingMoldLife_BLL_Old"];
                            if (model_new == null || model_old== null)
                            {
                                ClientScript.RegisterStartupScript(Page.GetType(), "", "alert('Operating Timeout !');", true);
                                return;
                            }
                            model_old.ChangeBy = Username;
                            model_old.ChangeTime = DateTime.Now;

                            Common.Class.BLL.MouldingMoldLife_BLL bll = new Common.Class.BLL.MouldingMoldLife_BLL();
                            bool Result = bll.UpdateChange(model_new,model_old);


                            if (Result)
                            {
                                strUrl = "../Moulding/MoldLife.aspx?Result=TRUE";
                            }
                            else
                            {
                                strUrl = "../Moulding/MoldLife.aspx?Result=FALSE";
                            }
                        }
                        #endregion

                        

                        #region Submit Attendance
                        else if (Type == "SubmitAttendance")
                        {
                            List<System.Data.SqlClient.SqlCommand> lSqlCmd = new List<System.Data.SqlClient.SqlCommand>();
                            try
                            {
                                lSqlCmd = (List<System.Data.SqlClient.SqlCommand>)Session["MouldingSubmitAttendanceSqlList"];

                                if (lSqlCmd != null && lSqlCmd.Count > 0 && DBHelp.SqlDB.SetData_Rollback(lSqlCmd))
                                {
                                    //return successful;
                                    strUrl = "../Moulding/MouldingUserAttendance.aspx?Result=true";
                                }
                                else
                                {
                                    //show message;
                                    strUrl = "../Moulding/MouldingUserAttendance.aspx?Result=false";
                                }
                            }
                            catch (Exception ex)
                            {
                                strUrl = "../Moulding/MouldingUserAttendance.aspx?Result=false";
                            }
                        }
                        #endregion

                        #region Add Parts Moving
                        else if (Type == "Moulding_AddMouldingPartsMovement")
                        {
                            Common.Class.Model.MouldingTransfer model = (Common.Class.Model.MouldingTransfer) Session["MouldingPartsMovment_Model"];

                            if (model == null)
                            {
                                ClientScript.RegisterStartupScript(Page.GetType(), "", "alert('Operating timeout!');  window.location.href = \"../Moulding/MouldingPartsMoving.aspx\";", true);
                                return;
                            }

                            Common.Class.BLL.MouldingPartsMovment_BLL bll = new Common.Class.BLL.MouldingPartsMovment_BLL();

                            model.Update_User = Username;
                            model.Status = "1";

                            if (bll.Add(model))
                            {
                                strUrl = "../Moulding/MouldingPartsMoving.aspx?Result=TRUE";
                            }
                            else
                            {
                                strUrl = "../Moulding/MouldingPartsMoving.aspx?Result=FALSE";
                            }
                        }
                        #endregion

                        #region Add Parts Moving
                        else if (Type == "Moulding_DeleteMouldingPartsMovement")
                        {
                            Common.Class.Model.MouldingTransfer model = (Common.Class.Model.MouldingTransfer)Session["MouldingPartsMovment_Model"];

                            if (model == null)
                            {
                                ClientScript.RegisterStartupScript(Page.GetType(), "", "alert('Operating timeout!');  window.location.href = \"../Moulding/MouldingPartsMoving.aspx\";", true);
                                return;
                            }

                            Common.Class.BLL.MouldingPartsMovment_BLL bll = new Common.Class.BLL.MouldingPartsMovment_BLL();

                            model.Update_User = Username;
                            model.Status = "0";

                            if (bll.UpDdate(model))
                            {
                                strUrl = "../Moulding/MouldingPartsMoving.aspx?Result=TRUE";
                            }
                            else
                            {
                                strUrl = "../Moulding/MouldingPartsMoving.aspx?Result=FALSE";
                            }
                        }
                        #endregion

                        #region Update setup wastematerial
                        else if (Type == "UpdateSetUp")
                        {
                            //Common.Class.Model.MouldingViHistory_Model model = (Common.Class.Model.MouldingViHistory_Model)Session["MouldingViHistory_Model"];
                            List < Common.Class.Model.MouldingViHistory_Model > lmodel = (List<Common.Class.Model.MouldingViHistory_Model>)Session["MouldingViHistory_Model"];
                            if (lmodel == null)
                            {
                                ClientScript.RegisterStartupScript(Page.GetType(), "", "alert('Operating timeout!');  window.location.href = \"../Moulding/ProductionReport.aspx\";", true);
                                return;
                            }
                            //Common.Class.Model.MouldingViHistory_Model model = new Common.Class.Model.MouldingViHistory_Model();
                            Common.Class.BLL.MouldingViHistory_BLL bll = new Common.Class.BLL.MouldingViHistory_BLL();

                            List<System.Data.SqlClient.SqlCommand> lSqlCmd = new List<System.Data.SqlClient.SqlCommand>();
                            foreach (Common.Class.Model.MouldingViHistory_Model model in lmodel)
                            {
                                lSqlCmd.Add(bll.UpdateCommond(model));
                            }
                            if (DBHelp.SqlDB.SetData_Rollback(lSqlCmd,DBHelp.Connection.SqlServer.SqlConn_Moulding_Server))
                            {
                                strUrl = "../Moulding/ProductionReport.aspx?Result=TRUE";
                            }
                            else
                            {
                                strUrl = "../Moulding/ProductionReport.aspx?Result=FALSE";
                            }
                        }
                        #endregion

                        #region Update Production
                        else if (Type == "UpdateProduction")
                        {
                            //Common.Class.Model.MouldingViHistory_Model model = (Common.Class.Model.MouldingViHistory_Model)Session["MouldingViHistory_Model"];
                            List<Common.Class.Model.MouldingViHistory_Model> lmodel = (List<Common.Class.Model.MouldingViHistory_Model>)Session["MouldingViHistory_Model"];
                            if (lmodel == null)
                            {
                                ClientScript.RegisterStartupScript(Page.GetType(), "", "alert('Operating timeout!');  window.location.href = \"../Moulding/ProductionReport.aspx\";", true);
                                return;
                            }
                            //Common.Class.Model.MouldingViHistory_Model model = new Common.Class.Model.MouldingViHistory_Model();
                            Common.Class.BLL.MouldingViHistory_BLL bll = new Common.Class.BLL.MouldingViHistory_BLL();

                            List<System.Data.SqlClient.SqlCommand> lSqlCmd = new List<System.Data.SqlClient.SqlCommand>();
                            foreach (Common.Class.Model.MouldingViHistory_Model model in lmodel)
                            {
                                model.refField02 = DateTime.Now.ToString();
                                model.refField01 = Username;
                                lSqlCmd.Add(bll.UpdateProduction(model));
                                Common.Class.Model.MouldingViHistory_Model tempModel = new Common.Class.Model.MouldingViHistory_Model();
                                tempModel = bll.GetModel_ByDayShiftAllPartMachine(model);
                                tempModel.refField02 = DateTime.Now.ToString();
                                tempModel.refField01 = Username;
                                lSqlCmd.Add(bll.InsertProductionHistory(tempModel));
                            }
                            if (DBHelp.SqlDB.SetData_Rollback(lSqlCmd, DBHelp.Connection.SqlServer.SqlConn_Moulding_Server))
                            {
                                strUrl = "../Moulding/UpdateProduction.aspx?Result=TRUE";
                            }
                            else
                            {
                                strUrl = "../Moulding/UpdateProduction.aspx?Result=FALSE";
                            }
                        }
                        #endregion

                        #region Delete Production
                        else if (Type == "DeleteProduction")
                        {
                            //Common.Class.Model.MouldingViHistory_Model model = (Common.Class.Model.MouldingViHistory_Model)Session["MouldingViHistory_Model"];
                            List<Common.Class.Model.MouldingViHistory_Model> lmodel = (List<Common.Class.Model.MouldingViHistory_Model>)Session["MouldingViHistory_Model"];
                            if (lmodel == null)
                            {
                                ClientScript.RegisterStartupScript(Page.GetType(), "", "alert('Operating timeout!');  window.location.href = \"../Moulding/ProductionReport.aspx\";", true);
                                return;
                            }
                            //Common.Class.Model.MouldingViHistory_Model model = new Common.Class.Model.MouldingViHistory_Model();
                            Common.Class.BLL.MouldingViHistory_BLL bll = new Common.Class.BLL.MouldingViHistory_BLL();

                            List<System.Data.SqlClient.SqlCommand> lSqlCmd = new List<System.Data.SqlClient.SqlCommand>();
                            foreach (Common.Class.Model.MouldingViHistory_Model model in lmodel)
                            {
                                lSqlCmd.Add(bll.DeleteProduction(model));
                                Common.Class.Model.MouldingViHistory_Model tempModel = new Common.Class.Model.MouldingViHistory_Model();
                                tempModel = bll.GetModel_ByDayShiftAllPartMachine(model);
                                tempModel.refField02 = DateTime.Now.ToString();
                                tempModel.refField01 = Username;
                                tempModel.refField03 = Type;
                                lSqlCmd.Add(bll.InsertProductionHistory(tempModel));
                            }
                            if (DBHelp.SqlDB.SetData_Rollback(lSqlCmd, DBHelp.Connection.SqlServer.SqlConn_Moulding_Server))
                            {
                                strUrl = "../Moulding/UpdateProduction.aspx?Result=TRUE";
                            }
                            else
                            {
                                strUrl = "../Moulding/UpdateProduction.aspx?Result=FALSE";
                            }
                        }
                        #endregion

                        #region Add CheckReport Verify
                        else if (Type == "CheckDailyReport")
                        {
                            Common.Class.Model.MouldingCheckReport_Model model = (Common.Class.Model.MouldingCheckReport_Model)Session["MouldingCheckReport_Model"];
                            Common.Class.BLL.MouldingCheckReport_BLL bll = new Common.Class.BLL.MouldingCheckReport_BLL();
                            model.Verify_Time = DateTime.Now;
                            model.Verify_User = Username;
                            if (bll.Add(model))
                            {
                                strUrl = "../Moulding/MouldingDailyReport.aspx?Result=TRUE";
                            }
                            else
                            {
                                strUrl = "../Moulding/MouldingDailyReport.aspx?Result=FALSE";
                            }
                        }
                        #endregion
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(Page.GetType(), "", "alert('" + errorStr + "');", true);
                        return;
                    }
                }
                #endregion


                #region Painting
                else if (Department.ToUpper() == StaticRes.Global.Department.Painting.ToUpper())
                {
                  
                    string Type = Request.QueryString["commandType"] == null ? "" : Request.QueryString["commandType"].ToString();

                    #region get user group
                    string Group = "";
                    if (Type == "AddUser" || Type == "UpdateUser" || Type == "DeleteUser")
                    {
                        Group = StaticRes.Global.UserGroup.ADMIN;
                    }
                    else if (Type == "UpdateProductivity" || Type == "SubmitAttendance")
                    {
                        Group = StaticRes.Global.UserGroup.SUPERVISOR;
                    }
                    else if (Type == "")
                    {
                        Group = StaticRes.Global.UserGroup.ENGINEER;
                    }
                    else if (Type == "ScanJob")
                    {
                        Group = StaticRes.Global.UserGroup.OPERATOR;
                    }
                   
                    #endregion

                    bool result = UserBll.Login(Username, Password, out errorStr, Department, Group);
                    if (result)
                    {
                        #region Update Productivity
                        if (Type == "UpdateProductivity")
                        {
                            List<Common.Class.Model.TempProductivityData_Model> listProData = (List<Common.Class.Model.TempProductivityData_Model>)Session["ListTempProductivityData_Model"];

                            if (listProData == null)
                            {
                                ClientScript.RegisterStartupScript(Page.GetType(), "", "alert('Operating timeout! Please try again');  window.location.href = \"../Reports/Productivity.aspx\";", true);
                                return;
                            }

                            Common.Class.BLL.TempProductivityData_BLL bll = new Common.Class.BLL.TempProductivityData_BLL();

                            if (bll.AddAll(listProData,Username))
                            {
                                strUrl = "../Reports/Productivity.aspx?Result=TRUE";
                            }
                            else
                            {
                                strUrl = "../Reports/Productivity.aspx?Result=FALSE";
                            }



                        }
                        #endregion

               

                        #region Submit Attendance
                        else if (Type == "SubmitAttendance")
                        {
                            List<System.Data.SqlClient.SqlCommand> lSqlCmd = new List<System.Data.SqlClient.SqlCommand>();
                            try
                            {
                                lSqlCmd = (List<System.Data.SqlClient.SqlCommand>)Session["PaintingSubmitAttendanceSqlList"];

                                if (lSqlCmd != null && lSqlCmd.Count > 0 && DBHelp.SqlDB.SetData_Rollback(lSqlCmd))
                                {
                                    //return successful;
                                    strUrl = "../Painting/PaintingUserAttendance.aspx?Result=true";
                                }
                                else
                                {
                                    //show message;
                                    strUrl = "../Painting/PaintingUserAttendance.aspx?Result=false";
                                }
                            }
                            catch (Exception ex)
                            {
                                strUrl = "../Painting/PaintingUserAttendance.aspx?Result=false";
                            }
                        }
                        #endregion
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(Page.GetType(), "", "alert('" + errorStr + "');", true);
                        return;
                    }
                   
                }
                #endregion


                #region Laser
                else if (Department.ToUpper() == StaticRes.Global.Department.Laser.ToUpper())
                {
                    string Type = Request.QueryString["commandType"] == null ? "" : Request.QueryString["commandType"].ToString();
                    

                    #region get user group
                    string Group = "";
                    if (Type == "AddUser" || Type == "UpdateUser" || Type == "DeleteUser")
                    {
                        Group = StaticRes.Global.UserGroup.ADMIN;
                    }
                    else if (Type == "SubmitAttendance")
                    {
                        Group = StaticRes.Global.UserGroup.SUPERVISOR;
                    }
                    else if (Type == "Delete" || Type == "Submit" || Type =="AddBom" || Type=="UpdateBom" 
                        || Type == "DownTimeRecord" || Type== "deleteJob" || Type == "AddSparePart"|| Type == "DeleteSparePart")
                    {
                        Group = StaticRes.Global.UserGroup.ENGINEER;
                    }
                    else if ( Type == "UpdateSetQTY" || Type == "Addbuyoff" || 
                             Type == "Check" ||  Type== "AddSparePartsInventory" || Type == "UpdateSparePart")
                    {
                        Group = StaticRes.Global.UserGroup.OPERATOR;
                    }
                    
             
                    #endregion

                    bool result = UserBll.Login(Username, Password, out errorStr, Department, Group);
                    if (result)
                    {
                        #region Delete Bom
                        if (Type == "Delete")
                        {
                            string PartNo = Request.QueryString["partNumber"] == null ? "" : Request.QueryString["partNumber"].ToString();
                            string MachineID = Request.QueryString["machineID"] == null ? "" : Request.QueryString["machineID"].ToString();

                            if (string.IsNullOrEmpty(PartNo) || string.IsNullOrEmpty(MachineID))
                            {
                                string message = "PartNo, MachineID Empty, Please try again!";
                                string url = "./BOMList.aspx";
                                Common.CommFunctions.ShowMessageAndRedirect(this.Page, message, url);
                                return;
                            }
                            

                            Common.Class.BLL.LMMSBom_BLL LMMSBom_bll = new Common.Class.BLL.LMMSBom_BLL();
                            DataTable dt = LMMSBom_bll.GetList(PartNo,"");

                            if (dt.Rows.Count > 1) //多个part的情况下只删除一个主表信息
                            {
                                if (LMMSBom_bll.DeleteByPartNo(PartNo, MachineID))
                                    strUrl = "./BOMList.aspx?Result=TRUE";
                                else
                                    strUrl = "./BOMList.aspx?Result=FALSE";
                            }
                            else
                            {
                                if (LMMSBom_bll.DeleteRollback(PartNo))
                                    strUrl = "./BOMList.aspx?Result=TRUE";
                                else
                                    strUrl = "./BOMList.aspx?Result=FALSE";
                            }


                            #region user event log
                            Common.Class.BLL.LMMSUserEventLog_BLL userEventLogBLL = new Common.Class.BLL.LMMSUserEventLog_BLL();
                            Common.Class.Model.LMMSUserEventLog userEventModel = new Common.Class.Model.LMMSUserEventLog();
                            userEventModel.pageName = "BOMList";
                            userEventModel.action = "delete part:" + PartNo;
                            userEventModel.startTime = DateTime.Now;
                            userEventModel.endTime = DateTime.Now;
                            userEventModel.dateTime = DateTime.Now;
                            userEventModel.eventType = "Delete";
                            userEventModel.userID = Username;
                            userEventModel.temp1 = PartNo;

                            List<Common.Class.Model.LMMSUserEventLog> listModel = new List<Common.Class.Model.LMMSUserEventLog>();
                            listModel.Add(userEventModel);
                            userEventLogBLL.AddRollBack(listModel);
                            #endregion



                        }
                        #endregion
                        
                        #region AddBom
                        else if (Type == "AddBom")
                        {
                            Common.Class.Model.LMMSBom_Model Bom_model = (Common.Class.Model.LMMSBom_Model)Session["Bom_model"];
                            List<Common.Class.Model.LMMSBomDetail_Model> list_detailModel = (List<Common.Class.Model.LMMSBomDetail_Model>)Session["list_detailModel"];

                            if (Bom_model == null || list_detailModel == null)
                            {
                                string message = "Operating timeout, Please try again!";
                                string url = "./BOMList.aspx";
                                Common.CommFunctions.ShowMessageAndRedirect(this.Page, message, url);
                                return;
                            }

                            Common.Class.BLL.LMMSBom_BLL LMMSBom_bll = new Common.Class.BLL.LMMSBom_BLL();

                            if (LMMSBom_bll.AddBomDetailRollback(Bom_model, list_detailModel))
                            {
                                strUrl = "./BOMList.aspx?Result=TRUE";
                            }
                            else
                            {
                                strUrl = "./BOMList.aspx?Result=FALSE";
                            }
                        }
                        #endregion

                        #region UpdateBom
                        else if (Type == "UpdateBom")
                        {
                            Common.Class.Model.LMMSBom_Model Bom_model = (Common.Class.Model.LMMSBom_Model)Session["Bom_model"];
                            List<Common.Class.Model.LMMSBomDetail_Model> list_detailModel = (List<Common.Class.Model.LMMSBomDetail_Model>)Session["list_detailModel"];

                            if (Bom_model == null || list_detailModel == null)
                            {
                                Common.CommFunctions.ShowMessageAndRedirect(this.Page, "Operating timeout, Please try again!", "./BOMList.aspx");
                                return;
                            }

                            Common.Class.BLL.LMMSBom_BLL LMMSBom_bll = new Common.Class.BLL.LMMSBom_BLL();

                            if (LMMSBom_bll.UpdateBomDetailRollback(Bom_model, list_detailModel))
                            {
                                strUrl = "./BOMList.aspx?Result=TRUE";
                            }
                            else
                            {
                                strUrl = "./BOMList.aspx?Result=FALSE";
                            }

                            #region user event log
                            Common.Class.BLL.LMMSUserEventLog_BLL userEventLogBLL = new Common.Class.BLL.LMMSUserEventLog_BLL();
                            Common.Class.Model.LMMSUserEventLog userEventModel = new Common.Class.Model.LMMSUserEventLog();
                            userEventModel.pageName = "BomFormMenu";
                            userEventModel.action = "Update part:" + Bom_model.partNumber;
                            userEventModel.startTime = DateTime.Now;
                            userEventModel.endTime = DateTime.Now;
                            userEventModel.dateTime = DateTime.Now;
                            userEventModel.eventType = "Update";
                            userEventModel.userID = Username;
                            userEventModel.temp1 = Bom_model.partNumber;

                            List<Common.Class.Model.LMMSUserEventLog> listModel = new List<Common.Class.Model.LMMSUserEventLog>();
                            listModel.Add(userEventModel);
                            userEventLogBLL.AddRollBack(listModel);
                            #endregion

                        }
                        #endregion
                        
                        
                        #region Delete  Inventory
                        if (Type == "deleteJob")
                        {
                          
                            string sJobNumber = Request.QueryString["JobNumber"] == null ? "" : Request.QueryString["JobNumber"].ToString();

                            if (string.IsNullOrEmpty(sJobNumber) )
                            {
                                string message = "Get job no fail, Please try again!";
                                string url = "./InventoryDetail.aspx";
                                Common.CommFunctions.ShowMessageAndRedirect(this.Page, message, url);
                                return;
                            }


                            Common.Class.BLL.LMMSInventoty_BLL lmmsInventoryBLL = new Common.Class.BLL.LMMSInventoty_BLL();
                         

                            if (lmmsInventoryBLL.DeleteJob(sJobNumber)) 
                            {
                                strUrl = "./InventoryDetail.aspx?Result=TRUE";
                            }
                            else
                            {
                                strUrl = "./InventoryDetail.aspx?Result=FALSE";
                            }


                            #region user event log
                            Common.Class.BLL.LMMSUserEventLog_BLL userEventLogBLL = new Common.Class.BLL.LMMSUserEventLog_BLL();
                            Common.Class.Model.LMMSUserEventLog userEventModel = new Common.Class.Model.LMMSUserEventLog();
                            userEventModel.pageName = "InventoryDetail";
                            userEventModel.action = "delete job:" + sJobNumber;
                            userEventModel.startTime = DateTime.Now;
                            userEventModel.endTime = DateTime.Now;
                            userEventModel.dateTime = DateTime.Now;
                            userEventModel.eventType = "delete";
                            userEventModel.userID = Username;
                            userEventModel.jobnumber = sJobNumber;

                            List<Common.Class.Model.LMMSUserEventLog> listModel = new List<Common.Class.Model.LMMSUserEventLog>();
                            listModel.Add(userEventModel);
                            userEventLogBLL.AddRollBack(listModel);
                            #endregion

                        }
                        #endregion

                        #region AddSparePartsInventory
                        else if (Type == "AddSparePartsInventory")
                        {
                            string sparePartsName = Request.QueryString["sparePartsName"] == null ? "" : Request.QueryString["sparePartsName"].ToString();
                            string sInQty = Request.QueryString["inQty"] == null ? "" : Request.QueryString["inQty"].ToString();

                            if (sparePartsName == "" || sInQty == "")
                            {
                                string message = "Get quantity error, Please try again!";
                                string url = "./LaserEquipmentCheckList.aspx";
                                Common.CommFunctions.ShowMessageAndRedirect(this.Page, message, url);
                                return;
                            }
                            

                            Common.Class.BLL.LMMSSparePartsInventory_BLL bll = new Common.Class.BLL.LMMSSparePartsInventory_BLL();
                            if (bll.UpdateInventory_RollBack(sparePartsName, int.Parse(sInQty), Username))
                            {
                                strUrl = "./LaserEquipmentCheckList.aspx";
                            }
                            else
                            {
                                strUrl = "./LaserEquipmentCheckList.aspx";
                            }
                        }
                        #endregion
                        
                  
                        #region Daily Check 
                        else if (Type == "Check")
                        {
                            Common.Class.Model.LMMSCheckList_Model CheckListModel = (Common.Class.Model.LMMSCheckList_Model)Session["LMMSCheckList_Model"];

                            if (CheckListModel == null || CheckListModel == null)
                            {
                                string message = "Operating timeout, Please try again!";
                                string url = "./LaserEquipmentCheckList.aspx";
                                Common.CommFunctions.ShowMessageAndRedirect(this.Page, message, url);
                                return;
                            }

                            CheckListModel.VerifyBy = Username;

                            Common.Class.BLL.LMMSCheckList_BLL CheckListBLL = new Common.Class.BLL.LMMSCheckList_BLL();

                            if (CheckListBLL.Check_RollBack(CheckListModel))
                            {
                                strUrl = "./LaserEquipmentCheckList.aspx?Result=TRUE";
                            }
                            else
                            {
                                strUrl = "./LaserEquipmentCheckList.aspx?Result=FALSE";
                            }
                        }
                        #endregion

                        #region Machine Down Time Record 
                        else if (Type == "DownTimeRecord")
                        {
                            Common.Class.Model.LMMSMachineDownTime_Model model = (Common.Class.Model.LMMSMachineDownTime_Model)Session["LMMSMachineDownTime_Model"];

                            if (model == null)
                            {
                                string message = "Operating timeout, Please try again!";
                                string url = "./LaserMachineDownTimeList.aspx";
                                Common.CommFunctions.ShowMessageAndRedirect(this.Page, message, url);
                                return;
                            }

                            model.checker = Username;

                            Common.Class.BLL.LMMSMachineDownTime_BLL DownTime_BLL = new Common.Class.BLL.LMMSMachineDownTime_BLL();

                            if (DownTime_BLL.Add(model))
                            {
                                strUrl = "./LaserMachineDownTimeList.aspx?Result=TRUE";
                            }
                            else
                            {
                                strUrl = "./LaserMachineDownTimeList.aspx?Result=FALSE";
                            }
                        }
                        #endregion

                  
                        #region Submit Attendance
                        else if (Type == "SubmitAttendance")
                        {
                            List<System.Data.SqlClient.SqlCommand> lSqlCmd = new List<System.Data.SqlClient.SqlCommand>();
                            try
                            {
                                lSqlCmd = (List<System.Data.SqlClient.SqlCommand>)Session["LaserSubmitAttendanceSqlList"];

                                if (lSqlCmd != null && lSqlCmd.Count > 0 && DBHelp.SqlDB.SetData_Rollback(lSqlCmd))
                                {
                                    //return successful;
                                    strUrl = "./UserAttendance.aspx?Result=TRUE";
                                }
                                else
                                {
                                    //show message;
                                    strUrl = "./UserAttendance.aspx?Result=FALSE";
                                }
                            }
                            catch (Exception ex)
                            {
                                strUrl = "./UserAttendance.aspx?Result=FALSE";
                            }
                        }
                        #endregion

                        #region Addbuyoff
                        else if (Type == "Addbuyoff")
                        {
                            Common.Class.Model.LMMSBuyOffList_Mode Buyoff_model = (Common.Class.Model.LMMSBuyOffList_Mode)Session["Buyoff_model"];
                            Common.Class.Model.LMMSVisionMachineSettingHis_Model visionModel = (Common.Class.Model.LMMSVisionMachineSettingHis_Model)Session["LMMSVisionMachineSettingHis_Model"];

                          

                            if (Buyoff_model == null  || visionModel == null)
                            {
                                string message = "Operating timeout, Please try again!";
                                string url = "./Buy_Off_List.aspx";
                                Common.CommFunctions.ShowMessageAndRedirect(this.Page, message, url);
                                return;
                            }
                            Common.Class.BLL.LMMSBUYOFFLIST_BLL LMMSBuyofflist_bll = new Common.Class.BLL.LMMSBUYOFFLIST_BLL();
                            Buyoff_model.CHECK_BY = Username;



                            if (LMMSBuyofflist_bll.AddBomDetailRollback(Buyoff_model, visionModel))
                            {
                                strUrl = "./Buy_Off_List.aspx?Result=TRUE";
                            }
                            else
                            {
                                strUrl = "./Buy_Off_List.aspx?Result=FALSE";
                            }
                        }
                        #endregion

                        #region AddSparePart
                        else if (Type == "AddSparePart")
                        {
                            Common.Class.Model.LMMSSparePartsInventory_Model model = (Common.Class.Model.LMMSSparePartsInventory_Model)Session["LMMSSparePartsInventory_Model"];

                            if (model == null)
                            {
                                string message = "Operating timeout, Please try again!";
                                string url = "./LaserSparePartsInventory.aspx";
                                Common.CommFunctions.ShowMessageAndRedirect(this.Page, message, url);
                                return;
                            }
                            Common.Class.BLL.LMMSSparePartsInventory_BLL bll = new Common.Class.BLL.LMMSSparePartsInventory_BLL();
                            model.lastUpdatedBy = Username;


                            if (bll.AddRollBack(model))
                            {
                                strUrl = "./LaserSparePartsInventory.aspx?Result=TRUE";
                            }
                            else
                            {
                                strUrl = "./LaserSparePartsInventory.aspx?Result=FALSE";
                            }
                        }
                        #endregion


                        #region Update SparePart
                        else if (Type == "UpdateSparePart")
                        {
                            Common.Class.Model.LMMSSparePartsInventory_Model model = (Common.Class.Model.LMMSSparePartsInventory_Model)Session["LMMSSparePartsInventory_Model"];

                            if (model == null)
                            {
                                string message = "Operating timeout, Please try again!";
                                string url = "./LaserSparePartsInventory.aspx";
                                Common.CommFunctions.ShowMessageAndRedirect(this.Page, message, url);
                                return;
                            }
                            Common.Class.BLL.LMMSSparePartsInventory_BLL bll = new Common.Class.BLL.LMMSSparePartsInventory_BLL();
                            model.lastUpdatedBy = Username;


                            if (bll.UpdateRollBack(model))
                            {
                                strUrl = "./LaserSparePartsInventory.aspx?Result=TRUE";
                            }
                            else
                            {
                                strUrl = "./LaserSparePartsInventory.aspx?Result=FALSE";
                            }
                        }
                        #endregion


                        #region Delete SparePart
                        else if (Type == "DeleteSparePart")
                        {
                            Common.Class.Model.LMMSSparePartsInventory_Model model = (Common.Class.Model.LMMSSparePartsInventory_Model)Session["LMMSSparePartsInventory_Model"];

                            if (model == null)
                            {
                                string message = "Operating timeout, Please try again!";
                                string url = "./LaserSparePartsInventory.aspx";
                                Common.CommFunctions.ShowMessageAndRedirect(this.Page, message, url);
                                return;
                            }
                            Common.Class.BLL.LMMSSparePartsInventory_BLL bll = new Common.Class.BLL.LMMSSparePartsInventory_BLL();
                            model.lastUpdatedBy = Username;


                            if (bll.DeleteRollBack(model))
                            {
                                strUrl = "./LaserSparePartsInventory.aspx?Result=TRUE";
                            }
                            else
                            {
                                strUrl = "./LaserSparePartsInventory.aspx?Result=FALSE";
                            }

                            #region user event log
                            Common.Class.BLL.LMMSUserEventLog_BLL userEventLogBLL = new Common.Class.BLL.LMMSUserEventLog_BLL();
                            Common.Class.Model.LMMSUserEventLog userEventModel = new Common.Class.Model.LMMSUserEventLog();
                            userEventModel.pageName = "LaserSparePartsInventory";
                            userEventModel.action = "Delete Spare Part:" + model.sparePartsName;
                            userEventModel.startTime = DateTime.Now;
                            userEventModel.endTime = DateTime.Now;
                            userEventModel.dateTime = DateTime.Now;
                            userEventModel.eventType = "delete";
                            userEventModel.userID = Username;

                            List<Common.Class.Model.LMMSUserEventLog> listModel = new List<Common.Class.Model.LMMSUserEventLog>();
                            listModel.Add(userEventModel);
                            userEventLogBLL.AddRollBack(listModel);
                            #endregion
                        }
                        #endregion
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(Page.GetType(), "", "alert('" + errorStr + "');", true);
                        return;
                    }
                }
                #endregion


                #region PQC
                else if (Department.ToUpper() == StaticRes.Global.Department.PQC.ToUpper())
                {
                    string Type = Request.QueryString["commandType"] == null ? "" : Request.QueryString["commandType"].ToString();

                    #region get user group
                    string Group = "";
                    if (Type == "AddUser" || Type == "UpdateUser" || Type == "DeleteUser")
                    {
                        Group = StaticRes.Global.UserGroup.ADMIN;
                    }
                    else if (Type == "AddBom" || Type == "UpdateBom"|| Type == "DeleteBom" || Type == "SubmitAttendance" || Type == "UpdateProductivity" || Type == "deleteJob")
                    {
                        Group = StaticRes.Global.UserGroup.SUPERVISOR;
                    }
                    else if (Type == "")
                    {
                        Group = StaticRes.Global.UserGroup.ENGINEER;
                    }
                    else if (Type == "JobMaintenance"  || Type== "AddbuyoffPaintingPart")
                    {
                        Group = StaticRes.Global.UserGroup.OPERATOR;
                    }
                  
                    #endregion

                    bool result = UserBll.Login(Username, Password, out errorStr, Department, Group);
                    if (result)
                    {
                        #region Update Productivity
                        if (Type == "UpdateProductivity")
                        {
                            List<Common.Class.Model.TempProductivityData_Model> listProData = (List<Common.Class.Model.TempProductivityData_Model>)Session["ListTempProductivityData_Model"];

                            if (listProData == null)
                            {
                                ClientScript.RegisterStartupScript(Page.GetType(), "", "alert('Operating timeout! Please try again');  window.location.href = \"../Reports/Productivity.aspx\";", true);
                                return;
                            }

                            Common.Class.BLL.TempProductivityData_BLL bll = new Common.Class.BLL.TempProductivityData_BLL();

                            if (bll.AddAll(listProData, Username))
                            {
                                strUrl = "../Reports/Productivity.aspx?Result=TRUE";
                            }
                            else
                            {
                                strUrl = "../Reports/Productivity.aspx?Result=FALSE";
                            }

                        }
                        #endregion

                        #region PQC Job Maintenance
                        else  if (Type == "JobMaintenance")
                        {
                            Common.Class.BLL.PQCQaViTracking_BLL bll = new Common.Class.BLL.PQCQaViTracking_BLL();
                            Common.Class.Model.PQCQaViTracking model = (Common.Class.Model.PQCQaViTracking)Session["PQCQaViTracking"];
                            List<Common.Class.Model.PQCQaViDetailTracking_Model> listDetailModel = (List<Common.Class.Model.PQCQaViDetailTracking_Model>)Session["ListPQCQaViDetailTracking_Model"];


                            string errString = "";

                            if (bll.JobMaintenance(model, listDetailModel,Username))
                            {
                                strUrl = "../PQC/PQCProductivityDetailReport.aspx?Result=TRUE";
                            }
                            else
                            {
                                strUrl = "../PQC/PQCProductivityDetailReport.aspx?Result=FALSE&Description="+errString;
                            }

                        }
                        #endregion
                        
                        #region add bom
                        else if (Type== "AddBom")
                        {

                            Common.Class.Model.PQCBom_Model bomModel = (Common.Class.Model.PQCBom_Model)Session["PQCBom_Model"];
                            List<Common.Class.Model.PQCBomDetail_Model> detailModelList =   (List<Common.Class.Model.PQCBomDetail_Model>)Session["list_PQCdetailModel"];

                            if (bomModel == null || detailModelList == null)
                            {
                                ClientScript.RegisterStartupScript(Page.GetType(), "", "alert('Operating timeout! Please try again');  window.location.href = \"../PQC/PQCBomFromMenu.aspx\";", true);
                                return;
                            }
                            bomModel.dateTime = DateTime.Now;
                            bomModel.userName = Username;

                            Common.Class.BLL.PQCBom_BLL bll = new Common.Class.BLL.PQCBom_BLL();


                            if (bll.AddRollback(bomModel, detailModelList,Username))
                            {
                                strUrl = "../PQC/PQCBomList.aspx?Result=TRUE" ;
                            }
                            else
                            {
                                strUrl = "../PQC/PQCBomList.aspx?Result=FALSE";
                            }
                        }
                        #endregion

                        #region update bom
                        else if (Type == "UpdateBom")
                        {

                            Common.Class.Model.PQCBom_Model bomModel = (Common.Class.Model.PQCBom_Model)Session["PQCBom_Model"];
                            List<Common.Class.Model.PQCBomDetail_Model> detailModelList = (List<Common.Class.Model.PQCBomDetail_Model>)Session["list_PQCdetailModel"];

                            if (bomModel == null || detailModelList == null)
                            {
                                ClientScript.RegisterStartupScript(Page.GetType(), "", "alert('Operating timeout! Please try again');  window.location.href = \"../PQC/PQCBomFromMenu.aspx\";", true);
                                return;
                            }
                            bomModel.dateTime = DateTime.Now;
                            bomModel.userName = Username;

                            Common.Class.BLL.PQCBom_BLL bll = new Common.Class.BLL.PQCBom_BLL();


                            if (bll.UpdateRollback(bomModel, detailModelList,Username))
                            {
                                strUrl = "../PQC/PQCBomList.aspx?Result=TRUE";
                            }
                            else
                            {
                                strUrl = "../PQC/PQCBomList.aspx?Result=FALSE";
                            }

                            #region user event log
                            Common.Class.BLL.LMMSUserEventLog_BLL userEventLogBLL = new Common.Class.BLL.LMMSUserEventLog_BLL();
                            Common.Class.Model.LMMSUserEventLog userEventModel = new Common.Class.Model.LMMSUserEventLog();
                            userEventModel.pageName = "PQCBomFromMenu";
                            userEventModel.action = "Update Part:" + bomModel.partNumber;
                            userEventModel.startTime = DateTime.Now;
                            userEventModel.endTime = DateTime.Now;
                            userEventModel.dateTime = DateTime.Now;
                            userEventModel.eventType = "Update";
                            userEventModel.userID = Username;
                            userEventModel.temp1 = bomModel.partNumber;

                            List<Common.Class.Model.LMMSUserEventLog> listModel = new List<Common.Class.Model.LMMSUserEventLog>();
                            listModel.Add(userEventModel);
                            userEventLogBLL.AddRollBack(listModel);
                            #endregion
                        }
                        #endregion

                        #region delete bom
                        else if (Type == "DeleteBom")
                        {
                            string sPartNumber = Request.QueryString["partNumber"] == null ? "" : Request.QueryString["partNumber"].ToString();
                            if (sPartNumber == "")
                            {
                                ClientScript.RegisterStartupScript(Page.GetType(), "", "alert('Get partnumber error, please try again!');  window.location.href = \"../PQC/PQCBomList.aspx\";", true);
                                return;
                            }
                        
                            Common.Class.BLL.PQCBom_BLL bll = new Common.Class.BLL.PQCBom_BLL();
                            if (bll.DeleteRollback(sPartNumber))
                            {
                                strUrl = "../PQC/PQCBomList.aspx?Result=TRUE";
                            }
                            else
                            {
                                strUrl = "../PQC/PQCBomList.aspx?Result=FALSE";
                            }

                            #region user event log
                            Common.Class.BLL.LMMSUserEventLog_BLL userEventLogBLL = new Common.Class.BLL.LMMSUserEventLog_BLL();
                            Common.Class.Model.LMMSUserEventLog userEventModel = new Common.Class.Model.LMMSUserEventLog();
                            userEventModel.pageName = "PQCBomList";
                            userEventModel.action = "Delete Part:" + sPartNumber;
                            userEventModel.startTime = DateTime.Now;
                            userEventModel.endTime = DateTime.Now;
                            userEventModel.dateTime = DateTime.Now;
                            userEventModel.eventType = "delete";
                            userEventModel.userID = Username;
                            userEventModel.temp1 = sPartNumber;

                            List<Common.Class.Model.LMMSUserEventLog> listModel = new List<Common.Class.Model.LMMSUserEventLog>();
                            listModel.Add(userEventModel);
                            userEventLogBLL.AddRollBack(listModel);
                            #endregion
                        }
                        #endregion

                        #region delete job
                        else if (Type == "deleteJob")
                        {
                            string sJobNo = Request.QueryString["JobNumber"] == null ? "" : Request.QueryString["JobNumber"].ToString();
                            if (sJobNo == "")
                            {
                                ClientScript.RegisterStartupScript(Page.GetType(), "", "alert('Get JobNo error, please try again!');  window.location.href = \"../PQC/PQCInventoryDetailReport.aspx\";", true);
                                return;
                            }

                            Common.Class.BLL.PQCInventory_BLL bll = new Common.Class.BLL.PQCInventory_BLL();
                            if (bll.Delete(sJobNo))
                            {
                                strUrl = "../PQC/PQCInventoryDetailReport.aspx?Result=TRUE";
                            }
                            else
                            {
                                strUrl = "../PQC/PQCInventoryDetailReport.aspx?Result=FALSE";
                            }

                            #region user event log
                            Common.Class.BLL.LMMSUserEventLog_BLL userEventLogBLL = new Common.Class.BLL.LMMSUserEventLog_BLL();
                            Common.Class.Model.LMMSUserEventLog userEventModel = new Common.Class.Model.LMMSUserEventLog();
                            userEventModel.pageName = "PQCInventoryDetailReport";
                            userEventModel.action = "Delete JOb:" + sJobNo;
                            userEventModel.startTime = DateTime.Now;
                            userEventModel.endTime = DateTime.Now;
                            userEventModel.dateTime = DateTime.Now;
                            userEventModel.eventType = "delete";
                            userEventModel.userID = Username;
                            userEventModel.jobnumber = sJobNo;

                            List<Common.Class.Model.LMMSUserEventLog> listModel = new List<Common.Class.Model.LMMSUserEventLog>();
                            listModel.Add(userEventModel);
                            userEventLogBLL.AddRollBack(listModel);
                            #endregion

                        }
                        #endregion

                
                        #region Submit Attendance
                        else if (Type == "SubmitAttendance")
                        {
                            List<System.Data.SqlClient.SqlCommand> lSqlCmd = new List<System.Data.SqlClient.SqlCommand>();
                            try
                            {
                                lSqlCmd = (List<System.Data.SqlClient.SqlCommand>)Session["PQCSubmitAttendanceSqlList"];

                                if (lSqlCmd != null && lSqlCmd.Count > 0 && DBHelp.SqlDB.SetData_Rollback(lSqlCmd))
                                {
                                    //return successful;
                                    strUrl = "../PQC/PQCUserAttendance.aspx?Result=true";
                                }
                                else
                                {
                                    //show message;
                                    strUrl = "../PQC/PQCUserAttendance.aspx?Result=false";
                                }
                            }
                            catch (Exception ex)
                            {
                                strUrl = "../PQC/PQCUserAttendance.aspx?Result=false";
                            }
                        }
                        #endregion

                        #region AddbuyoffPaintingPart
                        else if (Type == "AddbuyoffPaintingPart")
                        {
                            
                            List<Common.Class.Model.PaintingTempInfo_Model> listPaintingTempInfo = (List<Common.Class.Model.PaintingTempInfo_Model>)Session["PaintingTempInfo_Model_List"];

                            if (listPaintingTempInfo == null)
                            {
                                string message = "Operating timeout, Please try again!";
                                string url = "../PQC/BuyOffRecord.aspx";
                                Common.CommFunctions.ShowMessageAndRedirect(this.Page, message, url);
                                return;
                            }

                            Common.Class.BLL.PaintingTempInfo bll = new Common.Class.BLL.PaintingTempInfo();

                            

                            if (bll.AddRollBack(listPaintingTempInfo, Username))
                            {
                                strUrl = "../PQC/BuyOffRecord.aspx?Result=TRUE";
                            }
                            else
                            {
                                strUrl = "../PQC/BuyOffRecord.aspx?Result=FALSE";
                            }
                        }
                        #endregion

                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(Page.GetType(), "", "alert('" + errorStr + "');", true);
                        return;
                    }
                  
                }
                #endregion


                #region Assemble
                else if (Department.ToUpper() == StaticRes.Global.Department.Assembly.ToUpper())
                {
                   
                    string Type = Request.QueryString["commandType"] == null ? "" : Request.QueryString["commandType"].ToString();

                    #region get user group
                    string Group = "";
                    if (Type == "AddUser" || Type == "UpdateUser" || Type == "DeleteUser")
                    {
                        Group = StaticRes.Global.UserGroup.ADMIN;
                    }
                    else if (Type == "UpdateProductivity")
                    {
                        Group = StaticRes.Global.UserGroup.SUPERVISOR;
                    }
                    else if (Type == "")
                    {
                        Group = StaticRes.Global.UserGroup.ENGINEER;
                    }
                    else if (Type == "")
                    {
                        Group = StaticRes.Global.UserGroup.OPERATOR;
                    }
                    else if (Type == "SubmitAttendance")
                    {
                        Group = StaticRes.Global.UserGroup.SUPERVISOR;
                    }
                    #endregion

                    bool result = UserBll.Login(Username, Password, out errorStr, Department, Group);
                    if (result)
                    {
                        #region Update Productivity
                        if (Type == "UpdateProductivity")
                        {
                            List<Common.Class.Model.TempProductivityData_Model> listProData = (List<Common.Class.Model.TempProductivityData_Model>)Session["ListTempProductivityData_Model"];

                            if (listProData == null)
                            {
                                ClientScript.RegisterStartupScript(Page.GetType(), "", "alert('Operating timeout! Please try again');  window.location.href = \"../Reports/Productivity.aspx\";", true);
                                return;
                            }

                            Common.Class.BLL.TempProductivityData_BLL bll = new Common.Class.BLL.TempProductivityData_BLL();

                            if (bll.AddAll(listProData, Username))
                            {
                                strUrl = "../Reports/Productivity.aspx?Result=TRUE";
                            }
                            else
                            {
                                strUrl = "../Reports/Productivity.aspx?Result=FALSE";
                            }

                        }
                        #endregion

                  

                        #region Submit Attendance
                        else if (Type == "SubmitAttendance")
                        {
                            List<System.Data.SqlClient.SqlCommand> lSqlCmd = new List<System.Data.SqlClient.SqlCommand>();
                            try
                            {
                                lSqlCmd = (List<System.Data.SqlClient.SqlCommand>)Session["AssySubmitAttendanceSqlList"];

                                if (lSqlCmd != null && lSqlCmd.Count > 0 && DBHelp.SqlDB.SetData_Rollback(lSqlCmd))
                                {
                                    //return successful;
                                    strUrl = "../Assy/AssyUserAttendance.aspx?Result=true";
                                }
                                else
                                {
                                    //show message;
                                    strUrl = "../Assy/AssyUserAttendance.aspx?Result=false";
                                }
                            }
                            catch (Exception ex)
                            {
                                strUrl = "../Assy/AssyUserAttendance.aspx?Result=false";
                            }
                        }
                        #endregion

                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(Page.GetType(), "", "alert('" + errorStr + "');", true);
                        return;
                    }
                   
                }
                #endregion


                #region Office
                else if (Department.ToUpper() == StaticRes.Global.Department.Office.ToUpper())
                {

                    string Type = Request.QueryString["commandType"] == null ? "" : Request.QueryString["commandType"].ToString();

                    #region get user group
                    string Group = "";
                    if (Type == "AddUser" || Type == "UpdateUser" || Type == "DeleteUser")
                    {
                        Group = StaticRes.Global.UserGroup.ADMIN;
                    }
                    else if (Type == "SubmitAttendance")
                    {
                        Group = StaticRes.Global.UserGroup.SUPERVISOR;
                    }
                    else if (Type == "")
                    {
                        Group = StaticRes.Global.UserGroup.ENGINEER;
                    }
                    else if (Type == "")
                    {
                        Group = StaticRes.Global.UserGroup.OPERATOR;
                    }
                  
                    #endregion

                    bool result = UserBll.Login(Username, Password, out errorStr, Department, Group);
                    if (result)
                    {
                        #region Submit Attendance
                        if (Type == "SubmitAttendance")
                        {
                            List<System.Data.SqlClient.SqlCommand> lSqlCmd = new List<System.Data.SqlClient.SqlCommand>();
                            try
                            {
                                lSqlCmd = (List<System.Data.SqlClient.SqlCommand>)Session["OfficeSubmitAttendanceSqlList"];

                                if (lSqlCmd != null && lSqlCmd.Count > 0 && DBHelp.SqlDB.SetData_Rollback(lSqlCmd))
                                {
                                    //return successful;
                                    strUrl = "../Office/OfficeUserAttendance.aspx?Result=true";
                                }
                                else
                                {
                                    //show message;
                                    strUrl = "../Office/OfficeUserAttendance.aspx?Result=false";
                                }
                            }
                            catch (Exception ex)
                            {
                                strUrl = "../Office/OfficeUserAttendance.aspx?Result=false";
                            }
                        }
                        #endregion
                        
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(Page.GetType(), "", "alert('" + errorStr + "');", true);
                        return;
                    }

                }
                #endregion


                #region else
                else
                {
                    DBHelp.Reports.LogFile.Log("Login", "Unknow Department : "+ Department);
                }
                #endregion



                Response.Redirect(strUrl, false);
            }
            catch (Exception ee)
            {
                DBHelp.Reports.LogFile.Log("UserLogin", "btn_Login_Click exception:" + ee.ToString());
                ClientScript.RegisterStartupScript(Page.GetType(), "", "alert('Catch Exception : "+ee.ToString()+"!');", true);
            }
        }
    }
}