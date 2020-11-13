using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace DashboardTTS
{
    public class MvcApplication : System.Web.HttpApplication
    {

        Taiyo.JobSchedule.DemoSchedule schedule = new Taiyo.JobSchedule.DemoSchedule();

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);


            schedule.StartJob();
        }


        protected void Application_End(object sender, EventArgs e)
        {
            schedule.CloseJob();
        }
    }
}
