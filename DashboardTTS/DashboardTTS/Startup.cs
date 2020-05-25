using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DashboardTTS.Startup))]
namespace DashboardTTS
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
