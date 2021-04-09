using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;

namespace DashboardTTS.Webform
{
    public partial class TTSHomePage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            //不要删掉这个aspx界面, taiyo那边的人在游览器标签上都保存这个旧地址.
            //但新home page用mvc的模板重做了 在Home/Index里, 这里只做个跳转的功能.
            //如果删了, 发现网页404, taiyo那边又要BB个不停.

          

            //直接跳转到新home page
            string rootPath = Page.Request.ApplicationPath;
            if (rootPath == "/")
                Response.Redirect("/Home/Index");
            else
                Response.Redirect(rootPath + "/Home/Index");
            return;
        }
    }
}