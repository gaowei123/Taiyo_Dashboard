using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace DashboardTTS.Webform.Molding
{
    public partial class MaterialCalculation : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {


                    Common.Class.BLL.MouldingBom_BLL bll = new Common.Class.BLL.MouldingBom_BLL();
                    DataTable dtBudgetList = bll.GetListForMaterialBudget();

                    this.lbDate.Text = DateTime.Now.Month.ToString() + "/" + DateTime.Now.Year.ToString();
                    this.btnEdit.Visible = false;


                    if (dtBudgetList == null)
                    {
                        this.dgPartList.Visible = false;
                    }
                    else
                    {
                        this.dgPartList.Visible = true;
                        this.dgPartList.DataSource = dtBudgetList.DefaultView;
                        this.dgPartList.DataBind();

                        DataTable dt = InitResultTable();
                        this.dgMaterialResult.DataSource = dt.DefaultView;
                        this.dgMaterialResult.DataBind();
                        
                    }
                }
            }
            catch
            {

            }
         }

        protected void btn_Generate01_Click(object sender, EventArgs e)
        {
            if (Check01())
            {

            }
            else
            {
                this.lblResult.Text = "";
            }
        }

        protected void btn_Clean01_Click(object sender, EventArgs e)
        {
            this.txt_Quantity.Text = "";
            this.txt_cycletime.Text = "";
            this.txt_Mold.Text = "";
            this.lblResult.Text = "";
        }

        public bool Check01()
        {
            decimal _sValue = 0;
            if (this.txt_Quantity.Text.Trim() == "" || !decimal.TryParse(this.txt_Quantity.Text.Trim(), out _sValue))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "alert('Please input numeric  Quantity of parts required!');", true);
                this.txt_Quantity.Focus();
                return false;
            }
            if (this.txt_cycletime.Text.Trim() == "" || !decimal.TryParse(this.txt_cycletime.Text.Trim(), out _sValue))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "alert('Please input numeric Cycle Time (seconds) !');", true);
                this.txt_cycletime.Focus();
                return false;
            }
            if (this.txt_Mold.Text.Trim() == "" || !decimal.TryParse(this.txt_Mold.Text.Trim(), out _sValue))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "alert('Please input numeric Mold cavitation!');", true);
                this.txt_Mold.Focus();
                return false;
            }
            decimal result = decimal.Parse(this.txt_Quantity.Text) / (3600 / decimal.Parse(this.txt_cycletime.Text)) / decimal.Parse(this.txt_Mold.Text);
            this.lblResult.Text = "Hours required :" + Math.Round(result, 2).ToString();
            return true;
        }



        protected void btn_Generate02_Click(object sender, EventArgs e)
        {
            if (Check02())
            {

            }
            else
            {
                this.lblRelust02.Text = "";
            }
        }

        protected void btn_Clean02_Click(object sender, EventArgs e)
        {
            this.txt_Quantity02.Text = "";
            this.txt_weight.Text = "";
            this.txt_mold02.Text = "";
            this.txt_runnweight.Text = "";
            this.lblRelust02.Text = "";
        }

        public bool Check02()
        {
            decimal _sValue = 0;
            if (this.txt_Quantity02.Text.Trim() == "" || !decimal.TryParse(this.txt_Quantity02.Text.Trim(), out _sValue))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "alert('Please input numeric  Quantity of parts required!');", true);
                this.txt_Quantity02.Focus();
                return false;
            }
            if (this.txt_weight.Text.Trim() == "" || !decimal.TryParse(this.txt_weight.Text.Trim(), out _sValue))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "alert('Please input numeric Part weight(grams)  !');", true);
                this.txt_weight.Focus();
                return false;
            }
            if (this.txt_mold02.Text.Trim() == "" || !decimal.TryParse(this.txt_mold02.Text.Trim(), out _sValue))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "alert('Please input numeric Mold cavitation !');", true);
                this.txt_mold02.Focus();
                return false;
            }
            if (this.txt_runnweight.Text.Trim() == "" || !decimal.TryParse(this.txt_runnweight.Text.Trim(), out _sValue))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "alert('Please input numeric Runner weight per shot(zero for hotrunner)!');", true);
                this.txt_runnweight.Focus();
                return false;
            }
            decimal result = (decimal.Parse(this.txt_runnweight.Text) / decimal.Parse(this.txt_mold02.Text) + decimal.Parse(this.txt_weight.Text)) * decimal.Parse(this.txt_Quantity02.Text) / 1000;
            this.lblRelust02.Text = "Material requirement(Kg) :" + Math.Round(result, 2).ToString();
            return true;
        }


        protected void btn_Generate03_Click(object sender, EventArgs e)
        {
            if (Check03())
            {

            }
            else
            {
                this.lblresult03.Text = "";
            }
        }

        protected void btn_Clean03_Click(object sender, EventArgs e)
        {
            this.txt_cycletime03.Text = "";
            this.txt_mold03.Text = "";
            this.lblresult03.Text = "";
        }

        public bool Check03()
        {
            decimal _sValue = 0;
            if (this.txt_cycletime03.Text.Trim() == "" || !decimal.TryParse(this.txt_cycletime03.Text.Trim(), out _sValue))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "alert('Please input numeric  Cycle Time (seconds)!');", true);
                this.txt_cycletime03.Focus();
                return false;
            }
            if (this.txt_mold03.Text.Trim() == "" || !decimal.TryParse(this.txt_mold03.Text.Trim(), out _sValue))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "alert('Please input numeric Mold cavitation !');", true);
                this.txt_mold03.Focus();
                return false;
            }
            decimal result =  (3600 / decimal.Parse(this.txt_cycletime03.Text)) * decimal.Parse(this.txt_mold03.Text);
            this.lblresult03.Text = "Parts per hour :" + Math.Round(result, 2).ToString();
            return true;
        }



        protected void btnGenerateBudget_Click(object sender, EventArgs e)
        {
            this.btnEdit.Visible = true;

            DataTable dtResult = InitResultTable();

            List<MaterialResult> materialList = new List<MaterialResult>();


            foreach (DataGridItem item in dgPartList.Items)
            {
                int iCavityCount = 0;
                decimal dMaterial1stUnitWeight = 0;
                decimal dMaterial2ndUnitWeight = 0;
                decimal dMaterial1stUnitPrice = 0;
                decimal dMaterial2ndUnitPrice = 0;
                decimal dCycleTime = 0;

                
                //转化失败,并且值为0的都跳过,  bom没设定好.
                if (!int.TryParse(item.Cells[2].Text, out iCavityCount)   && iCavityCount==0)
                    continue;

                if (!decimal.TryParse(item.Cells[5].Text, out dMaterial1stUnitWeight))
                    continue;

                if (!decimal.TryParse(item.Cells[7].Text, out dMaterial1stUnitPrice))
                    continue;

                if (!decimal.TryParse(item.Cells[9].Text, out dCycleTime) && dCycleTime == 0)
                    continue;


                //有用到material 2的, 转化失败, 值为0的跳过.
                if (item.Cells[4].Text != "&nbsp;")
                {
                    if (!decimal.TryParse(item.Cells[6].Text, out dMaterial2ndUnitWeight))
                        continue;

                    if (!decimal.TryParse(item.Cells[8].Text, out dMaterial2ndUnitPrice))
                        continue;
                }
                
         
                // order qty框中没输入的隐藏, 并跳过
                TextBox tb = (TextBox)item.Cells[10].FindControl("txtOrderQty");
                if (tb.Text == "")
                {
                    item.Visible = false;
                    continue;
                }

                // order qty框中输入错误信息, 字体红色高亮提醒.
                if (tb.Text != "" && !Common.CommFunctions.isNumberic(tb.Text))
                {
                    tb.ForeColor = System.Drawing.Color.Red;
                    continue;
                }
                else
                {
                    ((TextBox)item.Cells[10].FindControl("txtOrderQty")).ForeColor = System.Drawing.Color.Black;
                }


                string sMaterialNo1st = item.Cells[3].Text;
                string sMaterialNo2nd = item.Cells[4].Text;

                decimal dOrderQty = decimal.Parse(tb.Text);
                decimal dMaterial1stWeight = dMaterial1stUnitWeight * dOrderQty / 1000;
                decimal dMaterial2ndWeight = dMaterial2ndUnitWeight * dOrderQty / 1000;
                decimal dMaterial1stPrice = dMaterial1stUnitPrice * dMaterial1stWeight;
                decimal dMaterial2ndPrice = dMaterial2ndUnitPrice * dMaterial2ndWeight;
                decimal dRunningTime = dOrderQty * dCycleTime / 3600;



                MaterialResult model1 = new MaterialResult();
                model1.MaterialNo = sMaterialNo1st;
                model1.MaterialKgs = dMaterial1stWeight;
                model1.MaterialCost = dMaterial1stPrice;
                model1.EstTime = dRunningTime;
                materialList.Add(model1);

                
                if (sMaterialNo2nd != "&nbsp;")
                {
                    MaterialResult model2 = new MaterialResult();
                    model2.MaterialNo = sMaterialNo2nd;
                    model2.MaterialKgs = dMaterial2ndWeight;
                    model2.MaterialCost = dMaterial2ndPrice;
                    model2.EstTime = dRunningTime;
                    materialList.Add(model2);
                }

            }





            this.dgMaterialResult.DataSource = ConvertDatatable(materialList).DefaultView;
            this.dgMaterialResult.DataBind();
        }


        public class MaterialResult
        {
            public string MaterialNo { get; set; }
            public decimal MaterialKgs { get; set; }
            public decimal MaterialCost { get; set; }
            public decimal EstTime { get; set; }
        }


        public DataTable InitResultTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("MaterialNo", typeof(string));
            dt.Columns.Add("MaterialKgs", typeof(decimal));
            dt.Columns.Add("MaterailCost", typeof(decimal));
            dt.Columns.Add("EstTime", typeof(string));
            return dt;
        }


        public DataTable ConvertDatatable(List<MaterialResult> materialList)
        {
            DataTable dt = InitResultTable();



            var summary = from a in materialList
                          group a by a.MaterialNo into materialsummary
                          select new
                          {
                              materialsummary.Key,
                              MaterialKgs = materialsummary.Sum(p => p.MaterialKgs).ToString("0.00"),
                              MaterialCost = materialsummary.Sum(p => p.MaterialCost).ToString("0.0000"),
                              EstTime = Common.CommFunctions.ConvertDateTimeShort(materialsummary.Sum(p => p.EstTime).ToString())
                          };


            foreach (var model in summary)
            {
                DataRow dr = dt.NewRow();
                dr[0] = model.Key;
                dr[1] = model.MaterialKgs;
                dr[2] = model.MaterialCost;
                dr[3] = model.EstTime;

                dt.Rows.Add(dr);

            }

            return dt;
        }


        protected void btnEdit_Click(object sender, EventArgs e)
        {
            foreach (DataGridItem item in this.dgPartList.Items)
            {
                item.Visible = true;
            }

            this.btnEdit.Visible = false;
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            foreach (DataGridItem item in this.dgPartList.Items)
            {
                item.Visible = true;

                ((TextBox)item.Cells[10].FindControl("txtOrderQty")).Text = "";
            }


            DataTable dt = InitResultTable();
            this.dgMaterialResult.DataSource = dt.DefaultView;
            this.dgMaterialResult.DataBind();
        }
    }

}