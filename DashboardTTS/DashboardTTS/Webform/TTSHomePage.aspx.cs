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

            //直接跳转到新 home page, 旧home page地址保留,  taiyo多数人保存旧地址.
            string rootPath = Page.Request.ApplicationPath;

            if (rootPath == "/")
                Response.Redirect("/Home/Index");
            else
                Response.Redirect(rootPath + "/Home/Index");
            
            return;
        }
    }
}